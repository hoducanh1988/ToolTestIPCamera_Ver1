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
                GlobalData.testingDataDUT.SYSTEMLOG = "";

                //open IP camera uart port
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
        
    }
}
