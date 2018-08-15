using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ToolTestIPCamera_Ver1.Function.Excute
{
    public class ExcuteTestProduct : DUT.IPCamera {
        //Gọi Form???

        public bool Excute() {
            try {
                //Connect to IP camera
                GlobalData.testingDataDUT.SYSTEMLOG += "\r\nCONNECT TO IP CAMERA >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>\r\n";
                string message = "";
                bool ret = GlobalData.camera.Connection(ref message);
                GlobalData.testingDataDUT.SYSTEMLOG += message + "\r\n\r\n";
                if (!ret) GlobalData.testingDataDUT.SYSTEMLOG += "Không thể kết nối telnet tới IP Camera\r\n";
                if (!ret) goto NG;

                //Login to IP camera
                GlobalData.testingDataDUT.SYSTEMLOG += "\r\nLOGIN TO IP CAMERA >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>\r\n";
                ret = GlobalData.camera.LoginToCamera(ref message);
                GlobalData.testingDataDUT.SYSTEMLOG += message + "\r\n\r\n";
                if (!ret) GlobalData.testingDataDUT.SYSTEMLOG += "Không thể login vào IP Camera\r\n";
                if (!ret) goto NG;


                //refresh hien thi
                GlobalData.testingDataDUT.InitControlForChecking();

                //check FW version
                if (GlobalData.initSetting.checkfirmwareversionoption == true) {

                }

                //Check MAC Address


                //Check SD card
                if (GlobalData.initSetting.checksdcardoption == true) {
                    if (!CheckSDCard(ref message)) goto NG;
                }

                //Check Light Sensor
                if (GlobalData.initSetting.checklightsensoroption == true) {
                    if (!CheckLightSensor(ref message)) goto NG;
                }

                //Speaker, mic
                if(GlobalData.initSetting.checkspeakermicoption == true) {
                    if (!CheckSpeakerMIC(ref message)) goto NG;
                }

                //Image sensor
                if(GlobalData.initSetting.checkimagesensoroption == true) {
                    if (!CheckImageSensor(ref message)) goto NG;
                }

                //WIFI
                if(GlobalData.initSetting.checkwifioption == true) {
                    if (!CheckWIFI(ref message)) goto NG;
                }


                //Button
                if(GlobalData.initSetting.checkbuttonoption == true) {
                    if (!CheckButton(ref message)) goto NG;
                }

                goto OK;
            }
            catch {
                goto NG;
            }

        OK:
            GlobalData.camera.Close();
            GlobalData.testingDataDUT.SYSTEMLOG += ">>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>\r\n\r\n";
            GlobalData.testingDataDUT.SYSTEMLOG += "PHÁN ĐỊNH TỔNG LÀ : PASS\r\n";
            return true;

        NG:
            GlobalData.camera.Close();
            GlobalData.testingDataDUT.SYSTEMLOG += ">>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>\r\n\r\n";
            GlobalData.testingDataDUT.SYSTEMLOG += "PHÁN ĐỊNH TỔNG LÀ : FAIL\r\n";
            return false;
        }

        #region SubFunction

        SpeakerMicWindow smWindow = null;
        WIFIWindow wWindow = null;
        ReButtonWindow rebWindow = null;

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
                string data = "";
                GlobalData.camera.WriteLine("mount");
                GlobalData.testingDataDUT.SYSTEMLOG += "Delay 1000 ms \r\n";
                Thread.Sleep(1000);
                data = GlobalData.camera.Read0();
                GlobalData.testingDataDUT.SYSTEMLOG += "CAMERA Feedback:\r\n" + data + "\r\n";
                ret = data.Contains("/dev/mmcblk0p1 on /media/sdcard type vfat (rw,relatime,fmask=0000,dmask=0000");
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

        /// <summary>
        /// CHECK SPEAKER AND MIC CAMERA
        /// </summary>
        /// <param name="_message"></param>
        /// <returns></returns>
        protected override bool CheckSpeakerMIC(ref string _message) {
            try {
                //string message = "";
                GlobalData.testingDataDUT.SYSTEMLOG += "\r\nKIỂM TRA SPEAKER, MICROPHONE CỦA IP CAMERA >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>\r\n";

                bool ret = false;
                App.Current.Dispatcher.Invoke(new Action(() => {
                    smWindow = new SpeakerMicWindow();
                    smWindow.ShowDialog();
                    ret = smWindow.Result;
                }));

                GlobalData.testingDataDUT.SPEAKERMICRESULT = ret == true ? Parameters.testStatus.PASS.ToString() : Parameters.testStatus.FAIL.ToString();
                GlobalData.testingDataDUT.SYSTEMLOG += string.Format("KẾT QUẢ KIỂM TRA SPEAKER, MICROPHONE: {0}\r\n", ret == true ? "PASS" : "FAIL");
                return ret;
            }
            catch (Exception ex) {
                _message = ex.ToString();
                GlobalData.testingDataDUT.SYSTEMLOG += _message + "\r\n";
                GlobalData.testingDataDUT.SYSTEMLOG += string.Format("KẾT QUẢ KIỂM TRA SPEAKER, MICROPHONE: FAIL\r\n");
                GlobalData.testingDataDUT.SPEAKERMICRESULT = Parameters.testStatus.FAIL.ToString();
                return false;
            }
        }

        protected override bool CheckWIFI(ref string _message) {
            try {
                bool ret = false;
                GlobalData.testingDataDUT.SYSTEMLOG += "\r\nKIỂM TRA WIFI CỦA IP CAMERA >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>\r\n";
                GlobalData.testingDataDUT.SYSTEMLOG += "Gửi lệnh kết nối WIFI\r\n";
                GlobalData.testingDataDUT.SYSTEMLOG += "nm_cfg client IPCAM-Test-1\r\n";
                GlobalData.camera.WriteLine("nm_cfg client IPCAM-Test-1");
                Thread.Sleep(1000);
                GlobalData.camera.WriteLine("\n");
                GlobalData.testingDataDUT.SYSTEMLOG += "Gửi lệnh disable LAN\r\n";
                GlobalData.camera.WriteLine("ifconfig eth0 down");
                Thread.Sleep(100);

                App.Current.Dispatcher.Invoke(new Action(() => {
                    wWindow = new WIFIWindow();
                    wWindow.ShowDialog();
                    ret = wWindow.wifiResult;
                }));

                GlobalData.testingDataDUT.WIFIRESULT = ret == true ? Parameters.testStatus.PASS.ToString() : Parameters.testStatus.FAIL.ToString();
                GlobalData.testingDataDUT.SYSTEMLOG += string.Format("KẾT QUẢ KIỂM TRA WIFI: {0}\r\n", ret == true ? "PASS" : "FAIL");
                return ret;
            } catch (Exception ex) {
                _message = ex.ToString();
                GlobalData.testingDataDUT.SYSTEMLOG += _message + "\r\n";
                GlobalData.testingDataDUT.SYSTEMLOG += string.Format("KẾT QUẢ KIỂM TRA WIFI: FAIL\r\n");
                GlobalData.testingDataDUT.WIFIRESULT = Parameters.testStatus.FAIL.ToString();
                return false;
            }
        }

        protected override bool CheckButton(ref string _message) {
            try {
                Thread.Sleep(2000);
                bool ret = false;
                GlobalData.testingDataDUT.SYSTEMLOG += "\r\nKIỂM TRA NÚT NHẤN CỦA IP CAMERA >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>\r\n";

                App.Current.Dispatcher.Invoke(new Action(() => {
                    rebWindow = new ReButtonWindow();
                    rebWindow.ShowDialog();
                    ret = rebWindow.buttonResult;
                }));

                GlobalData.testingDataDUT.BUTTONRESULT = ret == true ? Parameters.testStatus.PASS.ToString() : Parameters.testStatus.FAIL.ToString();
                GlobalData.testingDataDUT.SYSTEMLOG += string.Format("KẾT QUẢ KIỂM TRA NÚT NHẤN: {0}\r\n", ret == true ? "PASS" : "FAIL");
                return ret;
            }
            catch (Exception ex) {
                _message = ex.ToString();
                GlobalData.testingDataDUT.SYSTEMLOG += _message + "\r\n";
                GlobalData.testingDataDUT.SYSTEMLOG += string.Format("KẾT QUẢ KIỂM TRA NÚT NHẤN: FAIL\r\n");
                GlobalData.testingDataDUT.BUTTONRESULT = Parameters.testStatus.FAIL.ToString();
                return false;
            }



        }

        #endregion


    }
}
