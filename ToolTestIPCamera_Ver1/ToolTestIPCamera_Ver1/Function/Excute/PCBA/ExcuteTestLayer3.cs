using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ToolTestIPCamera_Ver1.Function.Excute
{
    public class ExcuteTestLayer3 : DUT.IPCamera {

        public bool Excute() {
            try {
                //open IP camera uart port
                GlobalData.testingDataDUT.SYSTEMLOG += "\r\nMỞ CỔNG COM IP CAMERA >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>\r\n";
                string message = "";
                bool ret = GlobalData.camera.Open(out message);
                GlobalData.testingDataDUT.SYSTEMLOG += message + "\r\n\r\n";
                if (!ret) GlobalData.testingDataDUT.SYSTEMLOG += "Không thể kết nối tới cổng COM\r\n";
                if (!ret) goto NG;

                bool IsReboot = false;

                //refresh hien thi
                GlobalData.testingDataDUT.InitControlForChecking();

                //Write MAC
                if (GlobalData.initSetting.writemacoption == true) {
                    IsReboot = true;
                    if (!WriteMAC(ref message)) goto NG;
                }

                //Upload Firmware
                if (GlobalData.initSetting.uploadfirmwareoption == true) {
                    IsReboot = true;
                    if (!UpFirmWare(ref message)) goto NG;
                }

                //Reboot sau nap firmware
                if (IsReboot && (GlobalData.initSetting.checkusboption == true || GlobalData.initSetting.checklightsensoroption == true || GlobalData.initSetting.checkimagesensoroption == true || GlobalData.initSetting.checkspeakermicoption == true)) {
                    base.RebootCamera();
                    base.WaitCameraBootComplete();
                    Thread.Sleep(3000);
                }

                //Check USB
                if(GlobalData.initSetting.checkusboption == true) {
                    if (!CheckUSB(ref message)) goto NG;
                }

                //Check Light Sensor
                if (GlobalData.initSetting.checklightsensoroption == true) {
                    if (!CheckLightSensor(ref message)) goto NG;
                }

                //Check Speaker & MIC
                if(GlobalData.initSetting.checkspeakermicoption == true) {
                    if (!CheckSpeakerMIC(ref message)) goto NG;
                }

                //Check Image Sensor
                if (GlobalData.initSetting.checkimagesensoroption == true) {
                    //Write Static IP For Camera
                    if (!WriteStaticIP()) goto NG;
                    //Check Image sensor
                    if (!CheckImageSensor(ref message)) goto NG;
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

        protected override bool CheckSDCard(ref string _message) {
            throw new NotImplementedException();
        }

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
            throw new NotImplementedException();
        }

        protected override bool CheckButton(ref string _message) {
            throw new NotImplementedException();
        }

        #endregion

    }
}
