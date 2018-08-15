using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ToolTestIPCamera_Ver1.Function.Excute
{
    public class ExcuteTestLayer2 : DUT.IPCamera {
        
        public bool Excute() {
            try {
                //open IP camera uart port
                GlobalData.testingDataDUT.SYSTEMLOG += "\r\nMỞ CỔNG COM IP CAMERA >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>\r\n";
                string message = "";
                bool ret = GlobalData.camera.Open(out message);
                GlobalData.testingDataDUT.SYSTEMLOG += message + "\r\n\r\n";
                if (!ret) GlobalData.testingDataDUT.SYSTEMLOG += "Không thể kết nối tới cổng COM\r\n";
                if (!ret) goto NG;

                //refresh hien thi
                GlobalData.testingDataDUT.InitControlForChecking();

                //wait camera boot complete
                //if (!WaitCameraBootComplete()) goto NG;

                //check LAN
                if (GlobalData.initSetting.checklanoption == true) {
                    if (!CheckLAN(ref message)) goto NG;
                }
                
                //check SD card
                if(GlobalData.initSetting.checksdcardoption == true) {
                    if (!CheckSDCard(ref message)) goto NG;
                }

                //check RGB LED
                if (GlobalData.initSetting.checkrgbledoption == true) {
                    if (!CheckRGBLED(ref message)) goto NG;
                }

                //Check WIFI
                if(GlobalData.initSetting.checkwifioption == true) {
                    if (!CheckWIFI(ref message)) goto NG;
                }
                //Check Button
                if (GlobalData.initSetting.checkbuttonoption == true) {
                    if (!CheckButton(ref message)) goto NG;
                }

                goto OK;
            } catch {
                goto NG;
            }

            OK:
            GlobalData.camera.Close();
            GlobalData.testingDataDUT.SYSTEMLOG += ">>>\r\n\r\n";
            GlobalData.testingDataDUT.SYSTEMLOG += "PHÁN ĐỊNH TỔNG LÀ : PASS\r\n";
            return true;

            NG:
            GlobalData.camera.Close();
            GlobalData.testingDataDUT.SYSTEMLOG += ">>>\r\n\r\n";
            GlobalData.testingDataDUT.SYSTEMLOG += "PHÁN ĐỊNH TỔNG LÀ : FAIL\r\n";
            return false;
        }


        #region SubFunction
        ButtonWindow btnwindow = null;


        /// <summary>
        /// CHECK KET NOI SD CARD CUA IP CAMERA
        /// TRUE = connect
        /// FALSE = disconnect
        /// </summary>
        /// <param name="_message"></param>
        /// <returns></returns>
        protected override bool CheckSDCard(ref string _message) {
            try {
                GlobalData.testingDataDUT.SYSTEMLOG += "\r\nKIỂM TRA THẺ NHỚ SD CARD CỦA IP CAMERA >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>\r\n";
                int count = 0;
                bool ret = false;
                REP:
                count++;
                GlobalData.testingDataDUT.SYSTEMLOG += string.Format("Phần mềm gửi lệnh: mount  LẦN {0}\r\n", count);
                GlobalData.testingDataDUT.CAMERALOG = "";
                GlobalData.camera.WriteLine("mount");
                GlobalData.testingDataDUT.SYSTEMLOG += "Delay 1000 ms \r\n";
                Thread.Sleep(1000);
                GlobalData.testingDataDUT.SYSTEMLOG += "CAMERA Feedback:\r\n" + GlobalData.testingDataDUT.CAMERALOG + "\r\n";
                ret = GlobalData.testingDataDUT.UARTLOG.Contains("/dev/mmcblk0p1 on /media/sdcard type vfat (rw,relatime,fmask=0000,dmask=0000");
                if (!ret) {
                    if (count < 5) {
                        Thread.Sleep(1000);
                        goto REP;
                    }
                }
                GlobalData.testingDataDUT.SDCARDRESULT = ret == true ? Parameters.testStatus.PASS.ToString() : Parameters.testStatus.FAIL.ToString();
                GlobalData.testingDataDUT.SYSTEMLOG += string.Format("KẾT QUẢ KIỂM TRA THẺ NHỚ SD card: {0}\r\n", ret == true ? "PASS" : "FAIL");
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


        protected override bool CheckWIFI(ref string _message) {
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
                    if (count < 20) {
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

        protected override bool CheckButton(ref string _message) {
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
                GlobalData.testingDataDUT.SYSTEMLOG += string.Format("Thời gian chờ {0} s ...  \r\n", count);
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


        protected override bool CheckSpeakerMIC(ref string _message) {
            throw new NotImplementedException();
        }


        #endregion


    }
}
