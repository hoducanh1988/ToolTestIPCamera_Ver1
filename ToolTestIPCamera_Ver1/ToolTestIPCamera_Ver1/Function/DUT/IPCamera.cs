using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;
using System.Diagnostics;

namespace ToolTestIPCamera_Ver1.Function.DUT {
    public class IPCamera {

        #region CHECK PCBA



        //Check Layer2
        #region CHECK PCBA LAYER2
        ButtonWindow btnwindow = null;
        RGBLEDWindow rgbwindow = null;

        /// <summary>
        /// CHECK IP CAMERA BOOT COMPLETE
        /// TIMEOUT = 60s
        /// TRUE = CAMERA BOOT OK
        /// FALSE = CAMERA BOOT NG
        /// </summary>
        /// <returns></returns>
        //protected bool WaitCameraBootComplete() {
        //    int count = 0;
        //    bool ret = false;
        //    REP:
        //    count++;
        //    ret = GlobalData.testingDataDUT.UARTLOG.Contains("Please press Enter to activate this console. md configuration done.");
        //    if (!ret) {
        //        if (count < 60) {
        //            Thread.Sleep(1000);
        //            goto REP;
        //        }
        //    }
        //    return ret;
        //}


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
                GlobalData.testingDataDUT.SYSTEMLOG += string.Format("KẾT QUẢ KIỂM TRA CỔNG LAN: {0}\r\n",ret == true ? "PASS": "FAIL");
                return ret;
            }
            catch (Exception ex) {
                _message = ex.ToString();
                GlobalData.testingDataDUT.SYSTEMLOG +=_message + "\r\n";
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
        protected bool CheckSDCard(ref string _message) {
            try {
                GlobalData.testingDataDUT.SYSTEMLOG += "\r\nKIỂM TRA THẺ NHỚ SD CARD CỦA IP CAMERA >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>\r\n";
                int count = 0;
                bool ret = false;
                REP:
                count++;
                GlobalData.testingDataDUT.SYSTEMLOG +=string.Format("Phần mềm gửi lệnh: mount  LẦN {0}\r\n",count);
                GlobalData.testingDataDUT.CAMERALOG = "";
                GlobalData.camera.WriteLine("mount");
                GlobalData.testingDataDUT.SYSTEMLOG += "Delay 1000 ms \r\n";
                Thread.Sleep(1000);
                GlobalData.testingDataDUT.SYSTEMLOG += "CAMERA Feedback:\r\n" + GlobalData.testingDataDUT.CAMERALOG + "\r\n";
                ret = GlobalData.testingDataDUT.UARTLOG.Contains("/dev/mmcblk0p1 on /media/sdcard type vfat (rw,relatime,fmask=0000,dmask=0000");
                if (!ret) {
                    if(count < 5) {
                        Thread.Sleep(1000);
                        goto REP;
                    }
                }
                GlobalData.testingDataDUT.SDCARDRESULT = ret == true ? Parameters.testStatus.PASS.ToString() : Parameters.testStatus.FAIL.ToString();
                GlobalData.testingDataDUT.SDCARDRESULT += string.Format("KẾT QUẢ KIỂM TRA THẺ NHỚ SD card: {0}\r\n", ret == true ? "PASS" : "FAIL");
                return ret;
            }
            catch (Exception ex) {
                _message = ex.ToString();
                GlobalData.testingDataDUT.SYSTEMLOG += _message + "\r\n";
                GlobalData.testingDataDUT.SYSTEMLOG += string.Format("KẾT QUẢ KIỂM TRA THẺ NHỚ SD card: FAIL\r\n");
                GlobalData.testingDataDUT.SDCARDRESULT = Parameters.testStatus.FAIL.ToString();
                return false;
            }
        }


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
        protected bool CheckWIFI(ref string _message) {
            try {
                GlobalData.testingDataDUT.SYSTEMLOG += "\r\nKIỂM TRA KẾT NỐI WIFI CỦA IP CAMERA >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>\r\n";
                GlobalData.testingDataDUT.SYSTEMLOG += string.Format("Phần mềm gửi lệnh: ifconfig eth0 down  \r\n");
                GlobalData.testingDataDUT.CAMERALOG = "";
                GlobalData.camera.WriteLine("ifconfig eth0 down");
                GlobalData.testingDataDUT.SYSTEMLOG += "Delay 1000 ms \r\n";
                Thread.Sleep(1000);
                GlobalData.testingDataDUT.SYSTEMLOG += "CAMERA Feedback:\r\n" + GlobalData.testingDataDUT.CAMERALOG + "\r\n";
                GlobalData.testingDataDUT.SYSTEMLOG += string.Format("Phần mềm gửi lệnh: nm_cfg client IPCAM-Test-1  \r\n");
                GlobalData.testingDataDUT.CAMERALOG = "";
                GlobalData.camera.WriteLine("nm_cfg client IPCAM-Test-1");
                GlobalData.testingDataDUT.SYSTEMLOG += "Delay 1000 ms \r\n";
                Thread.Sleep(1000);
                GlobalData.testingDataDUT.SYSTEMLOG += "CAMERA Feedback:\r\n" + GlobalData.testingDataDUT.CAMERALOG + "\r\n";
                bool ret = false;
                int count = 0;
                REP:
                GlobalData.testingDataDUT.SYSTEMLOG += string.Format("Kiểm tra kết nối LẦN {0}\r\n", count);
                count++;
                GlobalData.testingDataDUT.SYSTEMLOG += string.Format("Phần mềm gửi lệnh: nm_cfg status  \r\n");
                GlobalData.camera.WriteLine("nm_cfg status");
                GlobalData.testingDataDUT.CAMERALOG = "";
                GlobalData.testingDataDUT.SYSTEMLOG += "Delay 500 ms \r\n";
                Thread.Sleep(500);
                GlobalData.testingDataDUT.SYSTEMLOG += "CAMERA Feedback:\r\n" + GlobalData.testingDataDUT.CAMERALOG + "\r\n";
                ret = GlobalData.testingDataDUT.UARTLOG.Contains("status	= connect");
                if (!ret) {
                    if(count < 20) {
                        Thread.Sleep(500);
                        goto REP;
                    }
                }
                GlobalData.testingDataDUT.SYSTEMLOG += string.Format("KẾT QUẢ KIỂM TRA WIFI: {0}\r\n", ret == true ? "PASS" : "FAIL");
                GlobalData.testingDataDUT.WIFIRESULT = ret == true ? Parameters.testStatus.PASS.ToString() : Parameters.testStatus.FAIL.ToString();
                return ret;
            }
            catch (Exception ex) {
                _message = ex.ToString();
                GlobalData.testingDataDUT.SYSTEMLOG += _message + "\r\n";
                GlobalData.testingDataDUT.SYSTEMLOG += string.Format("KẾT QUẢ KIỂM TRA WIFI: FAIL\r\n");
                GlobalData.testingDataDUT.WIFIRESULT = Parameters.testStatus.FAIL.ToString();

                return false;
            }
        }


        /// <summary>
        /// CHECK BUTTON CUA IP CAMERA
        /// TRUE = button is OK
        /// FALSE = button is NG
        /// </summary>
        /// <param name="_message"></param>
        /// <returns></returns>
        protected bool CheckButton(ref string _message) {
            try {
                int _max = 15;
                App.Current.Dispatcher.Invoke(new Action(() => {
                    btnwindow = new ButtonWindow(_max);
                    btnwindow.Show();
                }));
                GlobalData.testingDataDUT.SYSTEMLOG += "\r\nKIỂM TRA NÚT BẤM CỦA IP CAMERA >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>\r\n";
                GlobalData.testingDataDUT.SYSTEMLOG += string.Format("Đang chờ người dùng thực hiện nhấn nút bấm...  \r\n");
                GlobalData.testingDataDUT.CAMERALOG = "";
                int count = 0;
                REP:
                count++;
                GlobalData.testingDataDUT.SYSTEMLOG += string.Format("Thời gian chờ {0} s ...  \r\n",count);
                GlobalData.testingDataDUT.SYSTEMLOG += "CAMERA Feedback:\r\n" + GlobalData.testingDataDUT.CAMERALOG + "\r\n";
                bool ret = GlobalData.testingDataDUT.UARTLOG.Contains("time_process = 0") || GlobalData.testingDataDUT.UARTLOG.Contains("time_process = 1");
                if (!ret) {
                    if (count <= _max) {
                        Thread.Sleep(1000);
                        goto REP;
                    }
                }
                else {
                    App.Current.Dispatcher.Invoke(new Action(() => { btnwindow.Close(); }));
                }
                GlobalData.testingDataDUT.SYSTEMLOG += string.Format("KẾT QUẢ KIỂM TRA NÚT BẤM: {0}\r\n", ret == true ? "PASS" : "FAIL");
                GlobalData.testingDataDUT.BUTTONRESULT = ret == true ? Parameters.testStatus.PASS.ToString() : Parameters.testStatus.FAIL.ToString();
                return ret;
            }
            catch (Exception ex) {
                _message = ex.ToString();
                GlobalData.testingDataDUT.SYSTEMLOG += _message + "\r\n";
                GlobalData.testingDataDUT.BUTTONRESULT = Parameters.testStatus.FAIL.ToString();
                GlobalData.testingDataDUT.SYSTEMLOG += string.Format("KẾT QUẢ KIỂM TRA NÚT BẤM: FAIL\r\n");
                return false;
            }
        }


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
                REP:
                //Access to uboot (timeout = 20s)
                count++;
                ret = GlobalData.testingDataDUT.CAMERALOG.Contains("Hit any key to stop autoboot:");
                if (!ret) {
                    if (count < _max) {
                        Thread.Sleep(100);
                        goto REP;
                    }
                }
                //Write MAC
                if (ret) {
                    //Set MAC for Camera (retry 3 times)
                    GlobalData.camera.WriteLine("\n");
                    Thread.Sleep(500);
                    string _mac = GlobalData.testingDataDUT.MACADDRESS;
                    string _longmac = string.Format("{0}:{1}:{2}:{3}:{4}:{5}", _mac.Substring(0,2), _mac.Substring(2, 2), _mac.Substring(4, 2), _mac.Substring(6, 2), _mac.Substring(8, 2), _mac.Substring(10, 2)).ToUpper();
                    count = 0;
                    REP1:
                    count++;
                    GlobalData.testingDataDUT.CAMERALOG = "";
                    GlobalData.camera.WriteLine(string.Format("setethaddr {0}", _longmac));
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
                        GlobalData.camera.WriteLine("saveenv");
                        Thread.Sleep(1000);
                        GlobalData.testingDataDUT.SYSTEMLOG += "CAMERA Feedback:\r\n" + GlobalData.testingDataDUT.CAMERALOG + "\r\n";
                        ret = GlobalData.testingDataDUT.CAMERALOG.Contains("saveenv done");
                        if (!ret) {
                            if (count < 3) goto REP2;
                        }
                    }
                }
                GlobalData.testingDataDUT.WRITEMACRESULT = ret == true ? Parameters.testStatus.PASS.ToString() : Parameters.testStatus.FAIL.ToString();
                return ret;
            }
            catch(Exception ex) {
                _message = ex.ToString();
                GlobalData.testingDataDUT.WRITEMACRESULT = Parameters.testStatus.FAIL.ToString();
                return false;
            }
        }


        protected bool UpFirmWare(ref string _message) {
            try {
                bool ret = false;
                GlobalData.testingDataDUT.CAMERALOG = "";
                int _count = 0, _timeout = 90;
                string _cmdPath = string.Format("{0}UPLOAD\\cmd.txt", System.AppDomain.CurrentDomain.BaseDirectory);
                string _wpsPath = string.Format("{0}UPLOAD\\wps.txt", System.AppDomain.CurrentDomain.BaseDirectory);
                string _runPath = string.Format("{0}UPLOAD\\RunPowerShell.exe", System.AppDomain.CurrentDomain.BaseDirectory);

                //Set IP Camera ve che do Upload FW
                GlobalData.camera.WriteLine("setipaddr 192.168.1.253");
                Thread.Sleep(500);
                GlobalData.camera.WriteLine("saveenv");
                Thread.Sleep(500);
                GlobalData.camera.WriteLine("update all");
                Thread.Sleep(1000);

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
                    if (_count < _timeout) {
                        Thread.Sleep(1000);
                        goto REP;
                    }
                }
                GlobalData.testingDataDUT.SYSTEMLOG += string.Format("KẾT QUẢ UPLOAD FILE linux.bin: {0}\r\n", ret == true ? "PASS" : "FAIL");

                //Upload file mcu.bin
                //********************************************//
                GlobalData.camera.WriteLine("update fw");
                Thread.Sleep(1000);
                File.WriteAllText(_cmdPath, string.Format("tftp -i 192.168.1.253 put {0}", GlobalData.initSetting.mcupath));
                Thread.Sleep(100);
                Process.Start(_runPath);

                GlobalData.testingDataDUT.CAMERALOG = "";
                REP1:
                _count++;
                ret = GlobalData.testingDataDUT.CAMERALOG.Contains("update fw done");
                if (!ret) {
                    if (_count < _timeout) {
                        Thread.Sleep(1000);
                        goto REP1;
                    }
                }
                GlobalData.testingDataDUT.SYSTEMLOG += string.Format("KẾT QUẢ UPLOAD FILE mcu.bin: {0}\r\n", ret == true ? "PASS" : "FAIL");


                //Upload file ldc.bin
                //********************************************//
                GlobalData.camera.WriteLine("update ldc");
                Thread.Sleep(1000);
                File.WriteAllText(_cmdPath, string.Format("tftp -i 192.168.1.253 put {0}", GlobalData.initSetting.ldcpath));
                Thread.Sleep(100);
                Process.Start(_runPath);

                GlobalData.testingDataDUT.CAMERALOG = "";
                REP2:
                _count++;
                ret = GlobalData.testingDataDUT.CAMERALOG.Contains("update ldc done");
                if (!ret) {
                    if (_count < _timeout) {
                        Thread.Sleep(1000);
                        goto REP2;
                    }
                }
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
                return true;
            }
            catch(Exception ex) {
                _message = ex.ToString();
                return false;
            }
        }


        /// <summary>
        /// CHECK LINGT SENSOR 
        /// </summary>
        /// <param name="_message"></param>
        /// <returns></returns>
        protected bool CheckLingtSensor(ref string _message) {
            try {
                return true;
            }
            catch(Exception ex) {
                _message = ex.ToString();
                return false;
            }
        }


        /// <summary>
        /// CHECK SPEAKER AND MIC CAMERA
        /// </summary>
        /// <param name="_message"></param>
        /// <returns></returns>
        protected bool CheckSpeakerMIC(ref string _message) {
            try {
                return true;
            }
            catch(Exception ex) {
                _message = ex.ToString();
                return false;
            }
        }


        /// <summary>
        /// CHECK IMAGE "IP CAMERA"
        /// </summary>
        /// <param name="_message"></param>
        /// <returns></returns>
        protected bool CheckImageSensor(ref string _message) {
            try {
                return true;
            }
            catch(Exception ex) {
                _message = ex.ToString();
                return false;
            }
        }
        #endregion

        #endregion


        #region check Product


        /// <summary>
        /// CHECK WIFI LAYER3
        /// TRUE = LED GREEN BLINKING
        /// FALSE = LED NO BILINKING
        /// </summary>
        /// <param name="_message"></param>
        /// <returns></returns>
        protected bool CheckWifiLayer3(ref string _message) {
            try {
                return true;
            }
            catch {
                return false;
            }
        }


        /// <summary>
        /// CHECK BUTTON LAYER 3
        /// </summary>
        /// <param name="_message"></param>
        /// <returns></returns>
        protected bool CheckButtonLayer3(ref string _message) {
            try {
                return true;
            }
            catch {
                return false;
            }
        }

        #endregion

    }
}
