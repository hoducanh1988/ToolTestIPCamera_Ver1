using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ToolTestIPCamera_Ver1.Function.DUT {
    public class IPCamera {

        #region CHECK PCBA



        //Check Layer2
        #region CHECK PCBA LAYER2

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


        /// <summary>
        /// CHECK CONG LAN CUA IP CAMERA Link Up/Down 
        /// TRUE = connect
        /// FALSE = disconnect
        /// </summary>
        /// <param name="_message">Thong bao loi</param>
        /// <returns>TRUE/FALSE</returns>
        protected bool CheckLAN(ref string _message) {
            try {
                bool ret = GlobalData.testingDataDUT.UARTLOG.Contains("rtl8168: eth0: link up");
                GlobalData.testingDataDUT.LANRESULT = ret == true ? Parameters.testStatus.PASS.ToString() : Parameters.testStatus.FAIL.ToString();
                return ret;
            }
            catch (Exception ex) {
                _message = ex.ToString();
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
                GlobalData.camera.WriteLine("");
                Thread.Sleep(100);
                GlobalData.camera.WriteLine("mount");
                int count = 0;
                bool ret = false;
                REP:
                count++;
                ret = GlobalData.testingDataDUT.UARTLOG.Contains("/dev/mmcblk0p1 on /media/sdcard type vfat (rw,relatime,fmask=0000,dmask=0000,allow_utime=00)");
                if (!ret) {
                    if(count < 5) {
                        Thread.Sleep(1000);
                        goto REP;
                    }
                }
                GlobalData.testingDataDUT.SDCARDRESULT = ret == true ? Parameters.testStatus.PASS.ToString() : Parameters.testStatus.FAIL.ToString();
                return ret;
            }
            catch (Exception ex) {
                _message = ex.ToString();
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
            try {
                //code here
                return true;
            }
            catch (Exception ex) {
                _message = ex.ToString();
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
                GlobalData.testingDataDUT.SYSTEMLOG += string.Format("CHECK WIFI...\r\n");
                GlobalData.camera.WriteLine("ifconfig eth0 down");
                Thread.Sleep(1000);
                GlobalData.camera.WriteLine("nm_cfg client IPCAM-Test-1");
                Thread.Sleep(1000);
                bool ret = false;
                int count = 0;
                REP:
                GlobalData.testingDataDUT.SYSTEMLOG += string.Format("LAN {0}\r\n", count);
                count++;
                GlobalData.camera.WriteLine("nm_cfg status");
                Thread.Sleep(500);
                ret = GlobalData.testingDataDUT.UARTLOG.Contains("status	= connect");
                GlobalData.testingDataDUT.SYSTEMLOG += string.Format("...{0}\r\n", ret == true ? "PASS" : "FAIL");
                if (!ret) {
                    if(count < 20) {
                        Thread.Sleep(500);
                        goto REP;
                    }
                }
                GlobalData.testingDataDUT.WIFIRESULT = ret == true ? Parameters.testStatus.PASS.ToString() : Parameters.testStatus.FAIL.ToString();
                return ret;
            }
            catch (Exception ex) {
                _message = ex.ToString();
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
                //code here
                return true;
            }
            catch (Exception ex) {
                _message = ex.ToString();
                return false;
            }
        }


        #endregion

        //Check Layer3
        #region CHECK PCBA LAYER3

        #endregion

        #endregion


        #region check Product

        #endregion

    }
}
