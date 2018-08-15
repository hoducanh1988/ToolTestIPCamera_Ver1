using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;
using System.Diagnostics;

namespace ToolTestIPCamera_Ver1.Function.DUT {
    public abstract class IPCamera {

        RGBLEDWindow rgbwindow = null;
        LightSensorWindow lswindow = null;

        #region CHECK PCBA

        /// <summary>
        /// CHECK IP CAMERA BOOT COMPLETE
        /// TIMEOUT = 60s
        /// TRUE = CAMERA BOOT OK
        /// FALSE = CAMERA BOOT NG
        /// </summary>
        /// <returns></returns>
        protected bool WaitCameraBootComplete() {
            int count = 0;
            bool ret = false;
        REP:
            count++;
            ret = GlobalData.testingDataDUT.UARTLOG.Contains("Please press Enter to activate this console. md configuration done.");
            if (!ret) {
                if (count < 60) {
                    Thread.Sleep(1000);
                    goto REP;
                }
            }
            return ret;
        }

        //protected bool RebootCamera() {

        //}


        //Check Layer2
        #region CHECK PCBA LAYER2

        /// <summary>
        /// CHECK CONG LAN CUA IP CAMERA Link Up/Down 
        /// TRUE = connect
        /// FALSE = disconnect
        /// </summary>
        /// <param name="_message">Thong bao loi</param>
        /// <returns>TRUE/FALSE</returns>
        protected bool CheckLAN(ref string _message) {
            try {
                GlobalData.testingDataDUT.SYSTEMLOG += "\r\nKIỂM TRA CỔNG LAN CỦA IP CAMERA >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>\r\n";
                GlobalData.testingDataDUT.SYSTEMLOG += "Phần mềm gửi lệnh: ifconfig eth0 up\r\n";
                GlobalData.testingDataDUT.CAMERALOG = "";
                GlobalData.camera.WriteLine("ifconfig eth0 up");
                GlobalData.testingDataDUT.SYSTEMLOG += "Delay 500 ms\r\n";
                Thread.Sleep(500);
                GlobalData.testingDataDUT.SYSTEMLOG += "CAMERA Feedback:\r\n" + GlobalData.testingDataDUT.CAMERALOG + "\r\n";
                GlobalData.testingDataDUT.CAMERALOG = "";
                GlobalData.testingDataDUT.SYSTEMLOG += "Phần mềm gửi lệnh: ping -c 4 192.168.1.100\r\n";
                GlobalData.camera.WriteLine("ping -c 4 192.168.1.100");
                GlobalData.testingDataDUT.SYSTEMLOG += "Delay 5000 ms\r\n";
                Thread.Sleep(5000);
                GlobalData.testingDataDUT.SYSTEMLOG += "CAMERA Feedback:\r\n" + GlobalData.testingDataDUT.CAMERALOG + "\r\n";
                bool ret = GlobalData.testingDataDUT.UARTLOG.Contains("64 bytes from 192.168.1.100:");
                GlobalData.testingDataDUT.LANRESULT = ret == true ? Parameters.testStatus.PASS.ToString() : Parameters.testStatus.FAIL.ToString();
                GlobalData.testingDataDUT.SYSTEMLOG += string.Format("KẾT QUẢ KIỂM TRA CỔNG LAN: {0}\r\n", ret == true ? "PASS" : "FAIL");
                return ret;
            }
            catch (Exception ex) {
                _message = ex.ToString();
                GlobalData.testingDataDUT.SYSTEMLOG += _message + "\r\n";
                GlobalData.testingDataDUT.SYSTEMLOG += string.Format("KẾT QUẢ KIỂM TRA CỔNG LAN: FAIL\r\n");
                GlobalData.testingDataDUT.LANRESULT = Parameters.testStatus.FAIL.ToString();
                return false;
            }
        }


        /// <summary>
        /// CHECK KET NOI SD CARD CUA IP CAMERA
        /// TRUE = connect
        /// FALSE = disconnect
        /// </summary>
        /// <param name="_message"></param>
        /// <returns></returns>
        protected abstract bool CheckSDCard(ref string _message);


        /// <summary>
        /// CHECK RGB LED CUA IP CAMERA
        /// TRUE = LED OK
        /// FALSE = LED NG
        /// </summary>
        /// <param name="_message"></param>
        /// <returns></returns>
        protected bool CheckRGBLED(ref string _message) {
            GlobalData.testingDataDUT.SYSTEMLOG += "\r\nKIỂM TRA RGB LED CỦA IP CAMERA >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>\r\n";
            try {
                bool ret = false;
                App.Current.Dispatcher.Invoke(new Action(() => {
                    rgbwindow = new RGBLEDWindow();
                    rgbwindow.ShowDialog();
                    ret = rgbwindow.GreenLED && rgbwindow.RedLED;
                    GlobalData.testingDataDUT.SYSTEMLOG += string.Format("GREEN LED: {0}\r\n", rgbwindow.GreenLED == true ? "PASS" : "FAIL");
                    GlobalData.testingDataDUT.SYSTEMLOG += string.Format("RED LED: {0}\r\n", rgbwindow.RedLED == true ? "PASS" : "FAIL");
                }));

                GlobalData.testingDataDUT.RGBLEDRESULT = ret == true ? Parameters.testStatus.PASS.ToString() : Parameters.testStatus.FAIL.ToString();
                GlobalData.testingDataDUT.SYSTEMLOG += string.Format("KẾT QUẢ KIỂM TRA RGB LED: {0}\r\n", ret == true ? "PASS" : "FAIL");
                return ret;
            }
            catch (Exception ex) {
                _message = ex.ToString();
                GlobalData.testingDataDUT.RGBLEDRESULT = Parameters.testStatus.FAIL.ToString();
                GlobalData.testingDataDUT.SYSTEMLOG += _message + "\r\n";
                GlobalData.testingDataDUT.SYSTEMLOG += string.Format("KẾT QUẢ KIỂM TRA RGB LED: FAIL\r\n");
                return false;
            }
        }


        /// <summary>
        /// CHECK KET NOI WIFI CUA IP CAMERA
        /// TRUE = connect
        /// FALSE = disconnect
        /// </summary>
        /// <param name="_message"></param>
        /// <returns></returns>

        protected abstract bool CheckWIFI(ref string _message);


        /// <summary>
        /// CHECK BUTTON CUA IP CAMERA
        /// TRUE = button is OK
        /// FALSE = button is NG
        /// </summary>
        /// <param name="_message"></param>
        /// <returns></returns>
        protected abstract bool CheckButton(ref string _message);

        #endregion

        //Check Layer3
        #region CHECK PCBA LAYER3

        /// <summary>
        /// Write MAC
        /// </summary>
        /// <param name="_message"></param>
        /// <returns></returns>
        protected bool WriteMAC(ref string _message) {
            try {
                bool ret = false;
                int count = 0, _max = 200;
                GlobalData.testingDataDUT.SYSTEMLOG += "\r\nĐANG GHI MAC VÀO IP CAMERA >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>\r\n";
            REP:
                //Access to uboot (timeout = 20s)
                GlobalData.testingDataDUT.SYSTEMLOG += "Đang chờ thiết bị khởi động...\r\n";
                count++;
                ret = GlobalData.testingDataDUT.CAMERALOG.Contains("Hit any key to stop autoboot:");
                if (!ret) {
                    if (count < _max) {
                        Thread.Sleep(100);
                        goto REP;
                    }
                    GlobalData.testingDataDUT.SYSTEMLOG += "Không đăng nhập được vào uboot\r\n";
                }
                //Write MAC
                if (ret) {
                    //Set MAC for Camera (retry 3 times)
                    GlobalData.camera.WriteLine("\n");
                    Thread.Sleep(500);
                    GlobalData.testingDataDUT.SYSTEMLOG += "Đã đăng nhập được vào uboot\r\n";
                    string _mac = GlobalData.testingDataDUT.MACADDRESS;
                    string _longmac = string.Format("{0}:{1}:{2}:{3}:{4}:{5}", _mac.Substring(0, 2), _mac.Substring(2, 2), _mac.Substring(4, 2), _mac.Substring(6, 2), _mac.Substring(8, 2), _mac.Substring(10, 2)).ToUpper();
                    GlobalData.testingDataDUT.SYSTEMLOG += string.Format("Địa chỉ MAC bắn vào: {0}\r\n", _longmac);
                    count = 0;
                REP1:
                    count++;
                    GlobalData.testingDataDUT.CAMERALOG = "";
                    GlobalData.testingDataDUT.SYSTEMLOG += string.Format("Phần mềm gửi lệnh: setethaddr {0}\r\n", _longmac);
                    GlobalData.camera.WriteLine(string.Format("setethaddr {0}", _longmac));
                    GlobalData.testingDataDUT.SYSTEMLOG += "Delay 500 ms\r\n";
                    Thread.Sleep(500);
                    GlobalData.testingDataDUT.SYSTEMLOG += "CAMERA Feedback:\r\n" + GlobalData.testingDataDUT.CAMERALOG + "\r\n";
                    ret = GlobalData.testingDataDUT.CAMERALOG.Contains(string.Format("New MAC address: {0}", _longmac));
                    if (!ret) {
                        if (count < 3) goto REP1;
                    }
                    else {
                        //Save MAC for Camera (retry 3 times)
                        count = 0;
                    REP2:
                        count++;
                        GlobalData.testingDataDUT.CAMERALOG = "";
                        GlobalData.testingDataDUT.SYSTEMLOG += "Phần mềm gửi lệnh: saveenv\r\n";
                        GlobalData.camera.WriteLine("saveenv");
                        GlobalData.testingDataDUT.SYSTEMLOG += "Delay 1000 ms\r\n";
                        Thread.Sleep(1000);
                        GlobalData.testingDataDUT.SYSTEMLOG += "CAMERA Feedback:\r\n" + GlobalData.testingDataDUT.CAMERALOG + "\r\n";
                        ret = GlobalData.testingDataDUT.CAMERALOG.Contains("saveenv done");
                        if (!ret) {
                            if (count < 3) goto REP2;
                            GlobalData.testingDataDUT.SYSTEMLOG += "Hệ thống chưa lưu lại MAC";
                        }
                    }
                }
                GlobalData.testingDataDUT.SYSTEMLOG += string.Format("KẾT QUẢ GHI MAC: {0}\r\n", ret == true ? "PASS" : "FAIL");
                GlobalData.testingDataDUT.WRITEMACRESULT = ret == true ? Parameters.testStatus.PASS.ToString() : Parameters.testStatus.FAIL.ToString();
                return ret;
            }
            catch (Exception ex) {
                _message = ex.ToString();
                GlobalData.testingDataDUT.WRITEMACRESULT = Parameters.testStatus.FAIL.ToString();
                GlobalData.testingDataDUT.SYSTEMLOG += "KẾT QUẢ GHI MAC: FAIL";
                return false;
            }
        }


        /// <summary>
        /// UPLOAD FIRMWARE 
        /// </summary>
        /// <param name="_message"></param>
        /// <returns></returns>
        protected bool UpFirmWare(ref string _message) {
            try {
                bool ret = false;
                GlobalData.testingDataDUT.CAMERALOG = "";
                GlobalData.testingDataDUT.SYSTEMLOG += "\r\nĐANG NẠP FIRMWARE CHO IP CAMERA >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>\r\n";
                int _count = 0, _timeout = 90;
                string _cmdPath = string.Format("{0}UPLOAD\\cmd.txt", System.AppDomain.CurrentDomain.BaseDirectory);
                string _wpsPath = string.Format("{0}UPLOAD\\wps.txt", System.AppDomain.CurrentDomain.BaseDirectory);
                string _runPath = string.Format("{0}UPLOAD\\RunPowerShell.exe", System.AppDomain.CurrentDomain.BaseDirectory);

                //Set IP Camera ve che do Upload FW
                GlobalData.testingDataDUT.CAMERALOG = "";
                GlobalData.testingDataDUT.SYSTEMLOG += "Phần mềm gửi lệnh: setipaddr 192.168.1.253 \r\n";
                GlobalData.camera.WriteLine("setipaddr 192.168.1.253");
                GlobalData.testingDataDUT.SYSTEMLOG += "Delay 500 ms\r\n";
                Thread.Sleep(500);
                GlobalData.testingDataDUT.SYSTEMLOG += "CAMERA Feedback:\r\n" + GlobalData.testingDataDUT.CAMERALOG + "\r\n";
                GlobalData.testingDataDUT.CAMERALOG = "";
                GlobalData.testingDataDUT.SYSTEMLOG += "Phần mềm gửi lệnh: saveenv\r\n";
                GlobalData.camera.WriteLine("saveenv");
                GlobalData.testingDataDUT.SYSTEMLOG += "Delay 500 ms\r\n";
                Thread.Sleep(500);
                GlobalData.testingDataDUT.SYSTEMLOG += "CAMERA Feedback:\r\n" + GlobalData.testingDataDUT.CAMERALOG + "\r\n";
                GlobalData.testingDataDUT.SYSTEMLOG += "Phần mềm gửi lệnh: update all\r\n";
                GlobalData.camera.WriteLine("update all");
                GlobalData.testingDataDUT.SYSTEMLOG += "Delay 1000 ms\r\n";
                Thread.Sleep(1000);
                GlobalData.testingDataDUT.SYSTEMLOG += "Đang chờ client gửi FIRMWARE lên thiết bị......\r\n";
                //Upload file linux.bin
                //********************************************//
                //1. Set đường dẫn file FW
                File.WriteAllText(_cmdPath, string.Format("tftp -i 192.168.1.253 put {0}", GlobalData.initSetting.linuxpath));
                Thread.Sleep(100);
                Process.Start(_runPath);

                GlobalData.testingDataDUT.CAMERALOG = "";
            REP:
                _count++;
                ret = GlobalData.testingDataDUT.CAMERALOG.Contains("update all done");
                if (!ret) {
                    GlobalData.testingDataDUT.SYSTEMLOG += string.Format("Chờ {0}s\r\n", _count);
                    if (_count < _timeout) {
                        Thread.Sleep(1000);
                        goto REP;
                    }
                }
                GlobalData.testingDataDUT.SYSTEMLOG += "CAMERA Feedback: \r\n" + GlobalData.testingDataDUT.CAMERALOG + "\r\n";
                GlobalData.testingDataDUT.SYSTEMLOG += string.Format("KẾT QUẢ UPLOAD FILE linux.bin: {0}\r\n", ret == true ? "PASS" : "FAIL");

                //Upload file mcu.bin
                //********************************************//
                GlobalData.testingDataDUT.SYSTEMLOG += "Phần mềm gửi lệnh: update fw\r\n";
                GlobalData.camera.WriteLine("update fw");
                GlobalData.testingDataDUT.SYSTEMLOG += "Delay 1000 ms\r\n";
                Thread.Sleep(1000);
                GlobalData.testingDataDUT.SYSTEMLOG += "Đang chờ client gửi FIRMWARE lên thiết bị......\r\n";
                File.WriteAllText(_cmdPath, string.Format("tftp -i 192.168.1.253 put {0}", GlobalData.initSetting.mcupath));
                Thread.Sleep(100);
                Process.Start(_runPath);

                GlobalData.testingDataDUT.CAMERALOG = "";
                _count = 0;
            REP1:
                _count++;
                ret = GlobalData.testingDataDUT.CAMERALOG.Contains("update fw done");
                if (!ret) {
                    if (_count < _timeout) {
                        GlobalData.testingDataDUT.SYSTEMLOG += string.Format("Chờ {0}s\r\n", _count);
                        Thread.Sleep(1000);
                        goto REP1;
                    }
                }
                GlobalData.testingDataDUT.SYSTEMLOG += "CAMERA Feedback: \r\n" + GlobalData.testingDataDUT.CAMERALOG + "\r\n";
                GlobalData.testingDataDUT.SYSTEMLOG += string.Format("KẾT QUẢ UPLOAD FILE mcu.bin: {0}\r\n", ret == true ? "PASS" : "FAIL");


                //Upload file ldc.bin
                //********************************************//
                GlobalData.testingDataDUT.SYSTEMLOG += "Phần mềm gửi lệnh: update ldc\r\n";
                GlobalData.camera.WriteLine("update ldc");
                GlobalData.testingDataDUT.SYSTEMLOG += "Delay 1000 ms\r\n";
                Thread.Sleep(1000);
                GlobalData.testingDataDUT.SYSTEMLOG += "Đang chờ client gửi FIRMWARE lên thiết bị......\r\n";
                File.WriteAllText(_cmdPath, string.Format("tftp -i 192.168.1.253 put {0}", GlobalData.initSetting.ldcpath));
                Thread.Sleep(100);
                Process.Start(_runPath);

                GlobalData.testingDataDUT.CAMERALOG = "";
                _count = 0;
            REP2:
                _count++;
                ret = GlobalData.testingDataDUT.CAMERALOG.Contains("update ldc done");
                if (!ret) {
                    if (_count < _timeout) {
                        GlobalData.testingDataDUT.SYSTEMLOG += string.Format("Chờ {0}s\r\n", _count);
                        Thread.Sleep(1000);
                        goto REP2;
                    }
                }
                GlobalData.testingDataDUT.SYSTEMLOG += "CAMERA Feedback: \r\n" + GlobalData.testingDataDUT.CAMERALOG + "\r\n";
                GlobalData.testingDataDUT.SYSTEMLOG += string.Format("KẾT QUẢ UPLOAD FILE ldc.bin: {0}\r\n", ret == true ? "PASS" : "FAIL");


                GlobalData.testingDataDUT.SYSTEMLOG += string.Format("KẾT QUẢ UPLOAD FIRMWARE: {0}\r\n", ret == true ? "PASS" : "FAIL");
                GlobalData.testingDataDUT.UPLOADFWRESULT = ret == true ? Parameters.testStatus.PASS.ToString() : Parameters.testStatus.FAIL.ToString();
                return ret;
            }
            catch (Exception ex) {
                _message = ex.ToString();
                GlobalData.testingDataDUT.SYSTEMLOG += string.Format("KẾT QUẢ UPLOAD FIRMWARE: FAIL\r\n");
                GlobalData.testingDataDUT.UPLOADFWRESULT = Parameters.testStatus.FAIL.ToString();
                return false;
            }
        }


        /// <summary>
        /// CHECK CONNECT USB
        /// TRUE = CONNECT USB SUCSSCES
        /// FALSE = CONNECT USB FAIL
        /// </summary>
        /// <param name="_message"></param>
        /// <returns></returns>
        protected bool CheckUSB(ref string _message) {
            try {
                bool ret = false;
                int count = 0;
                GlobalData.testingDataDUT.SYSTEMLOG += "\r\nKIỂM TRA KẾT NỐI USB CỦA IP CAMERA >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>\r\n";
            REP:
                count++;
                GlobalData.testingDataDUT.SYSTEMLOG += string.Format("Kiểm tra USB lần thứ {0}\r\n", count + 1);
                GlobalData.testingDataDUT.CAMERALOG = "";
                GlobalData.testingDataDUT.SYSTEMLOG += "Phần mềm gửi lệnh: ifconfig\r\n";
                GlobalData.camera.WriteLine("ifconfig");
                GlobalData.testingDataDUT.SYSTEMLOG += "Delay 3000 ms\r\n";
                Thread.Sleep(3000);
                ret = GlobalData.testingDataDUT.CAMERALOG.Contains("wlan0     Link encap:Ethernet  HWaddr") && GlobalData.testingDataDUT.CAMERALOG.Contains("wlan1     Link encap:Ethernet  HWaddr");
                if (!ret) {
                    if (count < 3) {
                        goto REP;
                    }
                }
                GlobalData.testingDataDUT.SYSTEMLOG += "CAMERA Feedback:\r\n" + GlobalData.testingDataDUT.CAMERALOG + "\r\n";
                GlobalData.testingDataDUT.SYSTEMLOG += string.Format("KẾT QUẢ KIỂM TRA USB: {0}\r\n", ret == true ? "PASS" : "FAIL");
                GlobalData.testingDataDUT.USBRESULT = ret == true ? Parameters.testStatus.PASS.ToString() : Parameters.testStatus.FAIL.ToString();
                return ret;
            }
            catch (Exception ex) {
                _message = ex.ToString();
                GlobalData.testingDataDUT.SYSTEMLOG += _message;
                GlobalData.testingDataDUT.SYSTEMLOG += "KẾT QUẢ KIỂM TRA USB: FAIL\r\n";
                GlobalData.testingDataDUT.USBRESULT = Parameters.testStatus.FAIL.ToString();
                return false;
            }
        }


        /// <summary>
        /// CHECK LINGT SENSOR 
        /// </summary>
        /// <param name="_message"></param>
        /// <returns></returns>
        protected bool CheckLightSensor(ref string _message) {
            GlobalData.testingDataDUT.SYSTEMLOG += "\r\nKIỂM TRA CẢM BIẾN ÁNH SÁNG CỦA IP CAMERA >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>\r\n";
            try {
                int _max = 30;
                bool ret = false;
                App.Current.Dispatcher.Invoke(new Action(() => {
                    lswindow = new LightSensorWindow(_max);
                    lswindow.ShowDialog();
                    ret = lswindow.LightLevelResult && lswindow.DarkLevelResult;
                    GlobalData.testingDataDUT.SYSTEMLOG += string.Format("Light Level: {0}\r\n", lswindow.LightLevelResult == true ? "PASS" : "FAIL");
                    GlobalData.testingDataDUT.SYSTEMLOG += string.Format("Dark Level: {0}\r\n", lswindow.DarkLevelResult == true ? "PASS" : "FAIL");
                }));

                GlobalData.testingDataDUT.LIGHTSENSORRESULT = ret == true ? Parameters.testStatus.PASS.ToString() : Parameters.testStatus.FAIL.ToString();
                GlobalData.testingDataDUT.SYSTEMLOG += string.Format("KẾT QUẢ KIỂM TRA CẢM BIẾN ÁNH SÁNG: {0}\r\n", ret == true ? "PASS" : "FAIL");
                return ret;
            }
            catch (Exception ex) {
                _message = ex.ToString();
                GlobalData.testingDataDUT.LIGHTSENSORRESULT = Parameters.testStatus.FAIL.ToString();
                GlobalData.testingDataDUT.SYSTEMLOG += _message + "\r\n";
                GlobalData.testingDataDUT.SYSTEMLOG += string.Format("KẾT QUẢ KIỂM TRA CẢM BIẾN ÁNH SÁNG: FAIL\r\n");
                return false;
            }

        }


        /// <summary>
        /// CHECK SPEAKER AND MIC CAMERA
        /// </summary>
        /// <param name="_message"></param>
        /// <returns></returns>
        protected abstract bool CheckSpeakerMIC(ref string _message);

        /// <summary>
        /// CHECK IMAGE "IP CAMERA"
        /// </summary>
        /// <param name="_message"></param>
        /// <returns></returns>
        protected bool CheckImageSensor(ref string _message) {
            try {
                GlobalData.testingDataDUT.SYSTEMLOG += "\r\nKIỂM TRA IMAGE SENSOR CỦA IP CAMERA >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>\r\n";
                bool ret = false;
                int count = 0, timeout = 40;
                GlobalData.testingDataDUT.SYSTEMLOG += "Lưu địa chỉ MAC vào MAC.txt\r\n";
                File.WriteAllText("MAC.txt", GlobalData.testingDataDUT.MACADDRESS);
                GlobalData.testingDataDUT.SYSTEMLOG += "Lọc file Result.txt\r\n";
                if (File.Exists("Result.txt")) File.Delete("Result.txt");
                GlobalData.testingDataDUT.SYSTEMLOG += "Mở chương trình LiveCamera.exe\r\n";
                Process.Start("LiveCam\\LiveCamera.exe");
                GlobalData.testingDataDUT.SYSTEMLOG += "Delay 100 ms\r\n";
                Thread.Sleep(100);
                //
                GlobalData.testingDataDUT.SYSTEMLOG += "Kiểm tra đã Image sensor.....\r\n";
                REP:
                count++;
                if (!File.Exists("Result.txt")) {
                    GlobalData.testingDataDUT.SYSTEMLOG += string.Format("Chờ {0} s \r\n",count);
                    if (count < timeout) {
                        Thread.Sleep(1000);
                        goto REP;
                    }
                }
                GlobalData.testingDataDUT.SYSTEMLOG += "Đã lưu ảnh của Image Sensor\r\n";
                string text = File.ReadAllText("Result.txt");
                ret = text.Contains("PASS,PASS") ? true : false;
                //
                GlobalData.testingDataDUT.IMAGESENSORRESULT = ret == true ? Parameters.testStatus.PASS.ToString() : Parameters.testStatus.FAIL.ToString();
                GlobalData.testingDataDUT.SYSTEMLOG += string.Format("KẾT QUẢ KIỂM TRA IMAGE SENSOR: {0} \r\n", ret == true ? "PASS" : "FAIL");
                return ret;
            }
            catch (Exception ex) {
                _message = ex.ToString();
                GlobalData.testingDataDUT.IMAGESENSORRESULT = Parameters.testStatus.FAIL.ToString();
                GlobalData.testingDataDUT.SYSTEMLOG += _message + "\r\n";
                GlobalData.testingDataDUT.SYSTEMLOG += string.Format("KẾT QUẢ KIỂM TRA IMAGE SENSOR: FAIL\r\n");
                return false;
            }
        }
        #endregion

        #endregion


        #region check Product

        /// <summary>
        /// Kiểm tra phiên bản FW version
        /// </summary>
        /// <param name="_message"></param>
        /// <returns></returns>
        protected bool CheckFormware(ref string _message) {
            try {

                return true;
            }
            catch(Exception ex) {
                _message = ex.ToString();
                return false;
            }
        }


        /// <summary>
        /// Kiểm tra đèn nhấp nháy xanh
        /// </summary>
        /// <param name="_message"></param>
        /// <returns></returns>
        protected bool WifiSauDongVo(ref string _message) {
            try {

                return true;
            }
            catch (Exception ex) {
                _message = ex.ToString();
                return false;
            }
        }

        #endregion

    }
}
