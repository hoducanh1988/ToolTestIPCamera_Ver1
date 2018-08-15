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

                //refresh hien thi
                GlobalData.testingDataDUT.InitControlForChecking();

                //Write MAC
                if (GlobalData.initSetting.writemacoption == true) {
                    if (!WriteMAC(ref message)) goto NG;
                }

                //Upload Firmware
                if (GlobalData.initSetting.uploadfirmwareoption == true) {
                    if (!UpFirmWare(ref message)) goto NG;
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

                //Check Image Sensor
                if (GlobalData.initSetting.checkimagesensoroption == true) {
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

        protected override bool CheckSDCard(ref string _message) {
            throw new NotImplementedException();
        }

        protected override bool CheckSpeakerMIC(ref string _message) {
            throw new NotImplementedException();
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
