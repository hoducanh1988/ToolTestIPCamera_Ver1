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
                GlobalData.testingDataDUT.SYSTEMLOG = "";
                //Open IP camera UART port
                string message = "";
                bool ret = GlobalData.camera.Open(out message);
                GlobalData.testingDataDUT.SYSTEMLOG += message + "\r\n\r\n";
                if (!ret) GlobalData.testingDataDUT.SYSTEMLOG += "Không thể kết nối đến cổng COM \r\n";
                if (!ret) goto NG;

                //refresh display
                GlobalData.testingDataDUT.InitControlForChecking();

                //Check Write MAC

                //Check UpFW

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
