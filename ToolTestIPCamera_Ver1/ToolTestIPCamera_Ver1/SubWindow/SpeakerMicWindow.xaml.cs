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
    /// Interaction logic for SpeakerMicWindow.xaml
    /// </summary>
    public partial class SpeakerMicWindow : Window {

        
        class windowContent : INotifyPropertyChanged {

            public event PropertyChangedEventHandler PropertyChanged;
            protected virtual void OnPropertyChanged(string propertyName = null) {
                PropertyChangedEventHandler handler = PropertyChanged;
                if (handler != null) {
                    handler(this, new PropertyChangedEventArgs(propertyName));
                }
            }

            string _title;
            public string TITLE {
                get { return _title; }
                set {
                    _title = value;
                    OnPropertyChanged(nameof(TITLE));
                }
            }
            int _time;
            public int TIME {
                get { return _time; }
                set {
                    _time = value;
                    OnPropertyChanged(nameof(TIME));
                }
            }
            bool _failvisible;
            public bool FAILVISIBLE {
                get { return _failvisible; }
                set {
                    _failvisible = value;
                    OnPropertyChanged(nameof(FAILVISIBLE));
                }
            }
            bool _passvisible;
            public bool PASSVISIBLE {
                get { return _passvisible; }
                set {
                    _passvisible = value;
                    OnPropertyChanged(nameof(PASSVISIBLE));
                }
            }
            bool _retryvisible;
            public bool RETRYVISIBLE {
                get { return _retryvisible; }
                set {
                    _retryvisible = value;
                    OnPropertyChanged(nameof(RETRYVISIBLE));
                }
            }

            public windowContent() {
                TITLE = "ĐANG THU ÂM...";
                TIME = 0;
                FAILVISIBLE = false;
                PASSVISIBLE = false;
                RETRYVISIBLE = false;
            }

            public void StartRecord() {
                TITLE = "ĐANG THU ÂM...";
                TIME = 0;
                FAILVISIBLE = false;
                PASSVISIBLE = false;
                RETRYVISIBLE = false;
            }

            public void StartDelay() {
                TITLE = "DELAY 3 GIÂY...";
                TIME = 0;
                FAILVISIBLE = false;
                PASSVISIBLE = false;
                RETRYVISIBLE = false;
            }

            public void Relogin() {
                TITLE = "RELOGIN TO CAMERA...";
                TIME = 0;
                FAILVISIBLE = false;
                PASSVISIBLE = false;
                RETRYVISIBLE = false;
            }

            public void PlaySound() {
                TITLE = "ĐANG PHÁT ÂM...";
                TIME = 0;
                FAILVISIBLE = false;
                PASSVISIBLE = false;
                RETRYVISIBLE = false;
            }

            public void Judged() {
                TITLE = "PHÁN ĐỊNH...";
                TIME = 30;
                FAILVISIBLE = true;
                PASSVISIBLE = true;
                RETRYVISIBLE = true;
            }

        }

        windowContent wcontent = new windowContent();

        int count = 0;
        DispatcherTimer timer = null;
        int stepcheck = 0;

        public bool Result = false;

        public SpeakerMicWindow() {
            InitializeComponent();
            this.DataContext = wcontent;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) {
            timer = new DispatcherTimer();
            timer.Tick += new EventHandler(dispatcherTimer_Tick);
            timer.Interval = new TimeSpan(0, 0, 0, 0, 500);
            count = 0;
            timer.Start();
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e) {
            this.count++;
            string message = "";
            switch (stepcheck) {
                case 0: {
                        //Ghi am
                        wcontent.TIME = count / 2;
                        if (count == 1) {
                            Thread t = new Thread(new ThreadStart(() => {
                                wcontent.StartRecord();
                                GlobalData.testingDataDUT.SYSTEMLOG += "Gửi lệnh yêu cầu IP Camera thu âm:\r\n";
                                GlobalData.testingDataDUT.SYSTEMLOG += "arecord -D hw:0,1 /tmp/audio_record.wav\r\n";
                                GlobalData.camera.WriteLine("arecord -D hw:0,1 /tmp/audio_record.wav");
                                GlobalData.testingDataDUT.SYSTEMLOG += "Máy tính phát âm thanh ra loa:\r\n";
                                Speaker speaker = new Speaker();
                                speaker.PlaySound(ref message);
                                Thread.Sleep(3000);
                                GlobalData.testingDataDUT.SYSTEMLOG += "delay 3000 ms\r\n";
                            }));
                            t.IsBackground = true;
                            t.Start();
                        }
                        if (count > 10) {
                            count = 0;
                            stepcheck = 1;
                        }
                        break;
                    }
                case 1: {
                        //delay 3000ms
                        if (count == 1) {
                            wcontent.StartDelay();
                            GlobalData.camera.Close();
                        }
                        
                        if (count > 6) {
                            stepcheck = 2;
                            count = 0;
                        }
                        break;
                    }
                case 2: {
                        if (count == 1) {
                            wcontent.Relogin();
                            //Connection toi IP Camera
                            GlobalData.testingDataDUT.SYSTEMLOG += "\r\nRECONNECT TO IP CAMERA \r\n";

                            bool ret = GlobalData.camera.Connection(ref message);
                            GlobalData.testingDataDUT.SYSTEMLOG += message + "\r\n\r\n";
                            if (!ret) GlobalData.testingDataDUT.SYSTEMLOG += "Không thể kết nối telnet tới IP Camera\r\n";
                            //if (!ret) return false;

                            ////Login to IP camera
                            GlobalData.testingDataDUT.SYSTEMLOG += "\r\nRELOGIN TO IP CAMERA\r\n";
                            ret = GlobalData.camera.LoginToCamera(ref message);
                            GlobalData.testingDataDUT.SYSTEMLOG += message + "\r\n\r\n";
                            if (!ret) GlobalData.testingDataDUT.SYSTEMLOG += "Không thể login vào IP Camera\r\n";
                            //if (!ret) return false;

                            if (ret) {
                                count = 0;
                                stepcheck = 3;
                            }
                        }
                        
                        break;
                    }
                case 3: {
                        //Output ra loa
                        wcontent.TIME = count / 2;
                        if (count == 1) {
                            System.Threading.Thread t = new System.Threading.Thread(new System.Threading.ThreadStart(() => {
                                wcontent.PlaySound();
                                GlobalData.testingDataDUT.SYSTEMLOG += "Gửi lệnh phát âm thanh ra loa IP Camera:\r\n";
                                GlobalData.testingDataDUT.SYSTEMLOG += "aplay - D hw: 0,1 / tmp / audio_record.wav\r\n";
                                GlobalData.camera.WriteLine("aplay  -D hw:0,1 /tmp/audio_record.wav");
                                GlobalData.testingDataDUT.SYSTEMLOG += "delay 5000ms\r\n";
                                System.Threading.Thread.Sleep(5000);
                            }));
                            t.IsBackground = true;
                            t.Start();
                        }
                        
                        if (count >= 10) {
                            stepcheck = 4;
                            count = 0;
                        }
                       
                        break;
                    }
                case 4: {
                        //Phan dinh
                        wcontent.TIME = 30 -  (count / 2);
                        if (count == 1) {
                            wcontent.Judged();
                        }
                        if (wcontent.TIME == 0) {
                            GlobalData.testingDataDUT.SYSTEMLOG += "Vượt quá thời gian timeout : 30s\r\n";
                            timer.Stop();
                            this.Close();
                        }
                        else {
                            this.MainBorder.Background = this.count % 2 == 1 ? (SolidColorBrush)new BrushConverter().ConvertFrom("#EEEEEE") : (SolidColorBrush)new BrushConverter().ConvertFrom("#FFE738");
                        }
                        break;
                    }
            }

        }

        private void Window_Unloaded(object sender, RoutedEventArgs e) {
            timer.Stop();
        }

        private void Label_MouseDown(object sender, MouseButtonEventArgs e) {
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            Button b = (Button)sender;
            switch (b.Content) {
                case "PASS": {
                        GlobalData.testingDataDUT.SYSTEMLOG += "Người thao tác phán định: PASS\r\n";
                        Result = true;
                        this.Close();
                        break;
                    }
                case "FAIL": {
                        GlobalData.testingDataDUT.SYSTEMLOG += "Người thao tác phán định: FAIL\r\n";
                        Result = false;
                        this.Close();
                        break;
                    }
                case "RETRY": {
                        GlobalData.testingDataDUT.SYSTEMLOG += "Người thao tác thực hiện phát âm lại\r\n";
                        stepcheck = 3;
                        count = 0;
                        break;
                    }
            }
        }

        private void MainBorder_MouseDown(object sender, MouseButtonEventArgs e) {
            this.DragMove();
        }
    }
}
