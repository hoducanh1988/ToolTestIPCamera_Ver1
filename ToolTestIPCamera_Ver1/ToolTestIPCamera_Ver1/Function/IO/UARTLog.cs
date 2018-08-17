using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ToolTestIPCamera_Ver1.Function.IO
{
    public class UARTLog
    {
        private static string _dir_logpath = AppDomain.CurrentDomain.BaseDirectory + "LogUART";
        private static string _dir_Layer2_logpath = AppDomain.CurrentDomain.BaseDirectory + "LogUART\\Layer2";
        private static string _dir_Layer3_logpath = AppDomain.CurrentDomain.BaseDirectory + "LogUART\\Layer3";
        private static string _dir_Product_logpath = AppDomain.CurrentDomain.BaseDirectory + "LogUART\\Product";

        static UARTLog() {
            if (Directory.Exists(_dir_logpath) == false) Directory.CreateDirectory(_dir_logpath);
            Thread.Sleep(100);
            if (Directory.Exists(_dir_Layer2_logpath) == false) Directory.CreateDirectory(_dir_Layer2_logpath);
            if (Directory.Exists(_dir_Layer3_logpath) == false) Directory.CreateDirectory(_dir_Layer3_logpath);
            if (Directory.Exists(_dir_Product_logpath) == false) Directory.CreateDirectory(_dir_Product_logpath);
        }

        public static bool Save() {
            try {
                string _dir = "";
                switch (GlobalData.initSetting.station) {
                    case "PCBA-LAYER2": {
                            _dir = _dir_Layer2_logpath;
                            break;
                        }
                    case "PCBA-LAYER3": {
                            _dir = _dir_Layer3_logpath;
                            break;
                        }
                    case "SAU-DONG-VO": {
                            _dir = _dir_Product_logpath;
                            break;
                        }
                }

                string _file = DateTime.Now.ToString("yyyyMMdd");

                StreamWriter st = new StreamWriter(string.Format("{0}\\{1}.txt", _dir, _file), true);
                st.WriteLine(string.Format("MAC ADDRESS: {0}\r\n---------------------------------------------------------\r\n{1}",GlobalData.testingDataDUT.MACADDRESS, GlobalData.testingDataDUT.UARTLOG));
                st.Dispose();
                return true;
            }
            catch {
                return false;
            }
        }

        public static bool Open(string _filename) {
            try {
                string _dir = "";
                switch (GlobalData.initSetting.station) {
                    case "PCBA-LAYER2": {
                            _dir = _dir_Layer2_logpath;
                            break;
                        }
                    case "PCBA-LAYER3": {
                            _dir = _dir_Layer3_logpath;
                            break;
                        }
                    case "SAU-DONG-VO": {
                            _dir = _dir_Product_logpath;
                            break;
                        }
                }

                Process proc = new Process();
                proc.StartInfo.FileName = string.Format("{0}\\{1}", _dir, _filename);
                proc.StartInfo.WorkingDirectory = Path.GetDirectoryName(string.Format("{0}\\{1}", _dir, _filename));
                proc.Start();
                return true;
            }
            catch {
                return false;
            }
        }


        public static bool OpenFolder() {
            try {
                string _dir = "";
                switch (GlobalData.initSetting.station) {
                    case "PCBA-LAYER2": {
                            _dir = _dir_Layer2_logpath;
                            break;
                        }
                    case "PCBA-LAYER3": {
                            _dir = _dir_Layer3_logpath;
                            break;
                        }
                    case "SAU-DONG-VO": {
                            _dir = _dir_Product_logpath;
                            break;
                        }
                }

                Process proc = new Process();
                proc.StartInfo.FileName = string.Format("{0}", _dir);
                proc.StartInfo.WorkingDirectory = Path.GetDirectoryName(string.Format("{0}", _dir));
                proc.Start();
                return true;
            }
            catch {
                return false;
            }
        }


        public static bool ListAllFile(out int _filecount) {
            string _dir = "";
            switch (GlobalData.initSetting.station) {
                case "PCBA-LAYER2": {
                        _dir = _dir_Layer2_logpath;
                        break;
                    }
                case "PCBA-LAYER3": {
                        _dir = _dir_Layer3_logpath;
                        break;
                    }
                case "SAU-DONG-VO": {
                        _dir = _dir_Product_logpath;
                        break;
                    }
            }

            _filecount = 0;
            try {
                GlobalData.datagridloguart.Clear();
                DirectoryInfo d = new DirectoryInfo(_dir);
                FileInfo[] Files = d.GetFiles("*.txt");
                int index = 0;
                foreach (FileInfo file in Files) {
                    index++;
                    logfileinfo log = new logfileinfo();
                    log.ID = index;
                    log.FileName = file.Name;
                    if (file.Length >= (1 << 10)) log.MemorySize = string.Format("{0}Kb", file.Length >> 10);
                    else log.MemorySize = "1Kb";
                    log.DateCreated = file.CreationTime.ToString();
                    log.DateModified = file.LastAccessTime.ToString();
                    GlobalData.datagridloguart.Add(log);
                }
                _filecount = index;
                return true;
            }
            catch {
                return false;
            }
        }
    }
}
