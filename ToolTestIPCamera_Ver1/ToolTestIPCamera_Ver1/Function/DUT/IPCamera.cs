using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolTestIPCamera_Ver1.Function.DUT
{
    public class IPCamera
    {

        #region CHECK PCBA



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
                //code here
                return true;
            } catch (Exception ex) {
                _message = ex.ToString();
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
                //code here
                return true;
            }
            catch (Exception ex) {
                _message = ex.ToString();
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
                //code here
                return true;
            }
            catch (Exception ex) {
                _message = ex.ToString();
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
        protected bool CheckButton (ref string _message) {
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
