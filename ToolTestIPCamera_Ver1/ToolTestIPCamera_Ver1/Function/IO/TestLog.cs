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
    public class TestLog
    {
        private static string _dir_logpath = AppDomain.CurrentDomain.BaseDirectory + "LogTest";
        private static string _dir_Layer2_logpath = AppDomain.CurrentDomain.BaseDirectory + "LogTest\\Layer2";
        private static string _dir_Layer3_logpath = AppDomain.CurrentDomain.BaseDirectory + "LogTest\\Layer3";
        private static string _dir_Product_logpath = AppDomain.CurrentDomain.BaseDirectory + "LogTest\\Product";

        static TestLog() {
            if (Directory.Exists(_dir_logpath) == false) Directory.CreateDirectory(_dir_logpath);
            Thread.Sleep(100);
            if (Directory.Exists(_dir_Layer2_logpath) == false) Directory.CreateDirectory(_dir_Layer2_logpath);
            if (Directory.Exists(_dir_Layer3_logpath) == false) Directory.CreateDirectory(_dir_Layer3_logpath);
            if (Directory.Exists(_dir_Product_logpath) == false) Directory.CreateDirectory(_dir_Product_logpath);
        }

        public static bool Save() {
            try {
                string _dir = "",_title = "", _content = "";

                switch (GlobalData.initSetting.station) {
                    case "PCBA-LAYER2": {
                            _dir = _dir_Layer2_logpath;
                            _title = "DateTime,MacAddress,Station,LAN,SDCard,RGBLed,WIFI,Button,ErrCode,ErrMessage,Judged";
                            _content = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10}",
                                                     DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"),
                                                     GlobalData.testingDataDUT.MACADDRESS,
                                                     GlobalData.initSetting.station,
                                                     GlobalData.testingDataDUT.LANRESULT,
                                                     GlobalData.testingDataDUT.SDCARDRESULT,
                                                     GlobalData.testingDataDUT.RGBLEDRESULT,
                                                     GlobalData.testingDataDUT.WIFIRESULT,
                                                     GlobalData.testingDataDUT.BUTTONRESULT,
                                                     "-",
                                                     "-",
                                                     GlobalData.testingDataDUT.TOTALRESULT
                                                     );
                            break;
                        }
                    case "PCBA-LAYER3": {
                            _dir = _dir_Layer3_logpath;
                            _title = "DateTime,MacAddress,Station,WriteMac,UploadFW,USB,LightSensor,SpeakerMic,ImageSensor,ErrCode,ErrMessage,Judged";
                            _content = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11}",
                                                     DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"),
                                                     GlobalData.testingDataDUT.MACADDRESS,
                                                     GlobalData.initSetting.station,
                                                     GlobalData.testingDataDUT.WRITEMACRESULT,
                                                     GlobalData.testingDataDUT.UPLOADFWRESULT,
                                                     GlobalData.testingDataDUT.USBRESULT,
                                                     GlobalData.testingDataDUT.LIGHTSENSORRESULT,
                                                     GlobalData.testingDataDUT.SPEAKERMICRESULT,
                                                     GlobalData.testingDataDUT.IMAGESENSORRESULT,
                                                     "-",
                                                     "-",
                                                     GlobalData.testingDataDUT.TOTALRESULT
                                                     );
                            break;
                        }
                    case "SAU-DONG-VO": {
                            _dir = _dir_Product_logpath;
                            _title = "DateTime,MacAddress,Station,FWVersion,MAC,SDCard,LightSensor,SpeakerMic,ImageSensor,WIFI,Button,ErrCode,ErrMessage,Judged";
                            _content = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13}",
                                                     DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"),
                                                     GlobalData.testingDataDUT.MACADDRESS,
                                                     GlobalData.initSetting.station,
                                                     GlobalData.testingDataDUT.FWVERSIONRESULT,
                                                     GlobalData.testingDataDUT.MACRESULT,
                                                     GlobalData.testingDataDUT.SDCARDRESULT,
                                                     GlobalData.testingDataDUT.LIGHTSENSORRESULT,
                                                     GlobalData.testingDataDUT.SPEAKERMICRESULT,
                                                     GlobalData.testingDataDUT.IMAGESENSORRESULT,
                                                     GlobalData.testingDataDUT.WIFIRESULT,
                                                     GlobalData.testingDataDUT.BUTTONRESULT,
                                                     "-",
                                                     "-",
                                                     GlobalData.testingDataDUT.TOTALRESULT
                                                     );
                            break;
                        }
                }

                string _file = DateTime.Now.ToString("yyyyMMdd");
                StreamWriter st = null;
                if (!File.Exists(string.Format("{0}\\{1}.csv", _dir, _file))) {
                    st = new StreamWriter(string.Format("{0}\\{1}.csv", _dir, _file), true);
                    st.WriteLine(_title);
                    st.WriteLine(_content);
                }
                else {
                    st = new StreamWriter(string.Format("{0}\\{1}.csv", _dir, _file), true);
                    st.WriteLine(_content);
                }
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
                GlobalData.datagridlogtest.Clear();
                DirectoryInfo d = new DirectoryInfo(_dir);
                FileInfo[] Files = d.GetFiles("*.csv");
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
                    GlobalData.datagridlogtest.Add(log);
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
