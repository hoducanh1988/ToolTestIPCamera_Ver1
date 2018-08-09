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

    }
}
