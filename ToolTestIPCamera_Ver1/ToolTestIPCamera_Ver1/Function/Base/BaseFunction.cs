using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolTestIPCamera_Ver1.Function
{
    public class BaseFunction
    {

        /// <summary>
        /// LIỆT KÊ TÊN TẤT CẢ CÁC CỔNG SERIAL PORT ĐANG KẾT NỐI VÀO MÁY TÍNH
        /// </summary>
        /// <returns></returns>
        public static List<string> get_Array_Of_SerialPort() {
            try {
                // Get a list of serial port names.
                //string[] ports = SerialPort.GetPortNames();
                List<string> list = new List<string>();
                list.Add("-");
                for (int i = 1; i < 100; i++) {
                    list.Add(string.Format("COM{0}", i));
                }
                //foreach (var item in ports) {
                //    list.Add(item);
                //}
                return list;
            }
            catch {
                return null;
            }
        }

        public static bool SelectCameraProtocol(string _station) {
            try {
                //select protocol of camera
                if (_station == "SAU-DONG-VO") GlobalData.camera = new Protocol.Telnet(GlobalData.initSetting.dutip, 23);
                else GlobalData.camera = new Protocol.Serial(GlobalData.initSetting.usbdebug1);
                return true;
            } catch {
                return false;
            }
        }

        public static bool IsMacAddress(string _mac, ref string _message) {
            try {
                //check chieu dai dia chi mac : OK = 12
                if (_mac.Length != 12) {
                    _message = string.Format("Số kí tự địa chỉ MAC không đúng. Tiêu chuẩn = 12, thực tế: {0}", _mac.Length);
                    return false;
                }
                //kiểm tra 6 kí tự đầu tiên của MAC có đúng với tiêu chuẩn VNPT hay không?
                List<string> listMAC = new List<string>();
                string _temp = GlobalData.initSetting.macsixdigit;
                if (!_temp.Contains(":")) listMAC.Add(_temp);
                else {
                    string[] buffer = _temp.Split(':');
                    for (int i = 0; i < buffer.Length; i++) {
                        listMAC.Add(buffer[i]);
                    }
                }
                bool MacInVNPT = false;
                foreach (var item in listMAC) {
                    if (listMAC.Contains(_mac.Substring(0, 6))) {
                        MacInVNPT = true;
                        break;
                    }
                }
                if (!MacInVNPT) {
                    _message = string.Format("Địa chỉ MAC không nằm trong dải VNPT. Tiêu chuẩn = {0}, thực tế: {1}", _temp, _mac.Substring(0,6));
                    return false;
                }
                //Kiểm tra các kí tự của mac có phải từ [0-9,A-F] hay không?
                string _pattern = "0123456789ABCDEF";
                bool MacIsValid = true;
                int j = 0;
                for(j = 0; j < 12; j++) {
                    if (_pattern.Contains(_mac.Substring(j,1)) == false) {
                        MacIsValid = false;
                        break;
                    }
                }
                if (!MacIsValid) {
                    _message = string.Format("Địa chỉ MAC sai. Kí tự thứ {0} không phải [0-9,A-F].", j + 1);
                    return false;
                }

                _message = string.Format("Địa chỉ MAC :{0} hợp lệ.", _mac);
                return true;
            } catch (Exception ex) {
                _message = ex.ToString();
                return false;
            }
        }

    }
}
