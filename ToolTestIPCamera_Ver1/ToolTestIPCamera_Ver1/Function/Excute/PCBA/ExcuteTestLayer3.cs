using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

                //Check Light Sensor

                //Check Speaker & MIC

                //Check Image Sensor

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

    }
}
