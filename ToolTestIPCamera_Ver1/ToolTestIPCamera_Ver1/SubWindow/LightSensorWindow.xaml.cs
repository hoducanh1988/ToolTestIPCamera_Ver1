using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using ToolTestIPCamera_Ver1.Function;

namespace ToolTestIPCamera_Ver1 {
    /// <summary>
    /// Interaction logic for LightSensorWindow.xaml
    /// </summary>
    public partial class LightSensorWindow : Window {

        class LightWindowContent : INotifyPropertyChanged {

            public event PropertyChangedEventHandler PropertyChanged;
            protected virtual void OnPropertyChanged(string propertyName = null) {
                PropertyChangedEventHandler handler = PropertyChanged;
                if (handler != null) {
                    handler(this, new PropertyChangedEventArgs(propertyName));
                }
            }

            string _steptitle;
            public string STEPTITLE {
                get { return _steptitle; }
                set {
                    _steptitle = value;
                    OnPropertyChanged(nameof(STEPTITLE));
                }
            }
            string _steplegend;
            public string STEPLEGEND {
                get { return _steplegend; }
                set {
                    _steplegend = value;
                    OnPropertyChanged(nameof(STEPLEGEND));
                }
            }
            int _timeout;
            public int TIMEOUT {
                get { return _timeout; }
                set {
                    _timeout = value;
                    OnPropertyChanged(nameof(TIMEOUT));
                }
            }
            string _adcvalue;
            public string ADCVALUE {
                get { return _adcvalue; }
                set {
                    _adcvalue = value;
                    OnPropertyChanged(nameof(ADCVALUE));
                }
            }

            public LightWindowContent() {
                STEPTITLE = "MỨC SÁNG:";
                STEPLEGEND = "Không che cảm biến ánh sáng.";
                TIMEOUT = 15;
                ADCVALUE = "0";
            }

            public void initLightLevel() {
                STEPTITLE = "MỨC SÁNG:";
                STEPLEGEND = "Không che cảm biến ánh sáng.";
                ADCVALUE = "0";
            }

            public void initDarkLevel() {
                STEPTITLE = "MỨC TỐI:";
                STEPLEGEND = "Che cảm biến ánh sáng.";
                ADCVALUE = "0";
            }

        }

        LightWindowContent window = new LightWindowContent();

        int _count = 0;
        DispatcherTimer timer = null;
        int _maxcount = 0;
        int _stepcheck = 0;

        public bool LightLevelResult = false;
        public bool DarkLevelResult = false;

        public LightSensorWindow(int _timeout) {
            InitializeComponent();
            window.TIMEOUT = _timeout;
            _maxcount = _timeout;
            this.DataContext = window;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) {
            timer = new DispatcherTimer();
            timer.Tick += new EventHandler(dispatcherTimer_Tick);
            timer.Interval = new TimeSpan(0, 0, 0, 0, 500);
            _count = 0;
            timer.Start();
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e) {
            this._count++;
            window.TIMEOUT = _maxcount - (this._count / 2);
            if (_count >= _maxcount * 2) {
                timer.Stop();
                this.Close();
            }

            //Check muc sang
            if (_stepcheck == 0) {
                GlobalData.testingDataDUT.SYSTEMLOG += "Kiểm tra mức sáng:\r\n";
                window.initLightLevel();
                bool ret = false;
                GlobalData.testingDataDUT.CAMERALOG = "";
                GlobalData.testingDataDUT.SYSTEMLOG += "cat /sys/devices/platform/rts_saradc.0/in0_input";
                GlobalData.camera.WriteLine("cat /sys/devices/platform/rts_saradc.0/in0_input");
                Thread.Sleep(300);
                GlobalData.testingDataDUT.SYSTEMLOG += "delay 500ms";
                string data = GlobalData.initSetting.station == "SAU-DONG-VO" ? GlobalData.camera.Read0() : GlobalData.testingDataDUT.CAMERALOG;
                GlobalData.testingDataDUT.SYSTEMLOG += string.Format("{0}\r\n", data);

                try {
                    data = data.Replace("\r", "").Replace("\n", "").Trim();
                    data = data.Replace("cat /sys/devices/platform/rts_saradc.0/in0_input", "");
                    data = data.Replace("~ #", "");
                    window.ADCVALUE = data;
                    ret = int.Parse(data) > int.Parse(GlobalData.initSetting.adcvalue);
                } catch {
                }

                
                LightLevelResult = ret;
                if (ret == true) { _stepcheck = 1; }
            }


            //Kiem tra muc toi
            if (_stepcheck == 1) {
                this.MainBorder.Background = this._count % 2 == 1 ? (SolidColorBrush)new BrushConverter().ConvertFrom("#EF692B") : (SolidColorBrush)new BrushConverter().ConvertFrom("#FFE738");
                GlobalData.testingDataDUT.SYSTEMLOG += "Kiểm tra mức tối:\r\n";
                window.initDarkLevel();
                bool ret = false;
                GlobalData.testingDataDUT.CAMERALOG = "";
                GlobalData.testingDataDUT.SYSTEMLOG += "cat /sys/devices/platform/rts_saradc.0/in0_input";
                GlobalData.camera.WriteLine("cat /sys/devices/platform/rts_saradc.0/in0_input");
                Thread.Sleep(300);
                GlobalData.testingDataDUT.SYSTEMLOG += "delay 500ms";
                string data = GlobalData.initSetting.station == "SAU-DONG-VO" ? GlobalData.camera.Read0() : GlobalData.testingDataDUT.CAMERALOG;
                GlobalData.testingDataDUT.SYSTEMLOG += string.Format("{0}\r\n", data);

                try {
                    data = data.Replace("\r", "").Replace("\n", "").Trim();
                    data = data.Replace("cat /sys/devices/platform/rts_saradc.0/in0_input", "");
                    data = data.Replace("~ #", "");
                    window.ADCVALUE = data;
                    ret = int.Parse(data) < int.Parse(GlobalData.initSetting.adcvalue);
                } catch { }
                DarkLevelResult = ret;
                if (ret == true) { this.Close(); }
            }
        }

        private void Window_Unloaded(object sender, RoutedEventArgs e) {
            timer.Stop();
        }

        private void Label_MouseDown(object sender, MouseButtonEventArgs e) {
            this.Close();
        }
    }
}
