using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using ToolTestIPCamera_Ver1.Function;

namespace ToolTestIPCamera_Ver1.UserControls {
    /// <summary>
    /// Interaction logic for ucManual.xaml
    /// </summary>
    public partial class ucManual : UserControl {
        public ucManual() {
            InitializeComponent();

            GlobalData.manualData.dutip = GlobalData.initSetting.dutip;
            GlobalData.manualData.dutuser = GlobalData.initSetting.dutuser;
            GlobalData.manualData.station = GlobalData.initSetting.station;
            GlobalData.manualData.usbdebug1 = GlobalData.initSetting.usbdebug1;
            GlobalData.manualData.routerssid = GlobalData.initSetting.routerssid;

            this.DataContext = GlobalData.manualData;

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Tick += ((sender, e) => {

                if (btncheckmac.Background == Brushes.Lime) {
                    _macScrollview.ScrollToEnd();
                }

                if (btncheckwifi.Background == Brushes.Lime) {
                    _wifiScrollview.ScrollToEnd();
                }

                if (btnchecksdcard.Background == Brushes.Lime) {
                    _sdcardScrollview.ScrollToEnd();
                }

                if (btnuploadfw.Background == Brushes.Lime) {
                    _fwScrollview.ScrollToEnd();
                }

            });
            timer.Start();
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e) {

        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            Button b = (Button)sender;
            string _btnContent = b.Content.ToString();

            Thread t = new Thread(new ThreadStart(() => {

                App.Current.Dispatcher.Invoke(new Action(() => {
                    b.Background = Brushes.Lime;
                    b.IsEnabled = false;
                }));

                GlobalData.camera.Close();
                bool ret = false;
                string message = "";

                //CONNECT TO CAMERA
                switch (GlobalData.initSetting.station) {
                    case "PCBA-LAYER2":
                    case "PCBA-LAYER3": {
                            //open IP camera uart port            
                            ret = GlobalData.camera.Open(out message);
                            break;
                        }
                    case "SAU-DONG-VO": {
                            //Connect to IP camera
                            ret = GlobalData.camera.Connection(ref message);
                            ret = GlobalData.camera.LoginToCamera(ref message);
                            break;
                        }
                }


                //MANUAL CHECK
                switch (_btnContent) {

                    //MAC ADDRESS
                    case "ĐỌC MAC TỪ IP CAMERA": {

                            GlobalData.manualData.mac_manuallog = "";
                            GlobalData.testingDataDUT.CAMERALOG = "";
                            if (!ret) {
                                GlobalData.manualData.mac_manuallog += "Không thể kết nối tới IP Camera.\r\n";
                                GlobalData.manualData.mac_manuallog += "Kết thúc.\r\n";
                                MessageBox.Show("Finished.", "Check MAC", MessageBoxButton.OK, MessageBoxImage.Error);
                                break;
                            }
                            GlobalData.manualData.mac_manuallog += "Connect to IP Camera success.\r\n";
                            GlobalData.manualData.mac_manuallog += "Địa chỉ MAC trên tem......................\r\n";
                            App.Current.Dispatcher.Invoke(new Action(() => {
                                GlobalData.manualData.mac_manuallog += txtMACout.Text + "\r\n";
                                txtMACin.Clear();
                                MACResult.Content = "--";
                            }));
                            GlobalData.manualData.mac_manuallog += "Gửi lệnh đọc địa chỉ MAC..................\r\n";
                            GlobalData.camera.WriteLine("ifconfig");
                            Thread.Sleep(1000);
                            string data = GlobalData.initSetting.station == "SAU-DONG-VO" ? GlobalData.camera.Read0() : GlobalData.testingDataDUT.CAMERALOG;
                            GlobalData.manualData.mac_manuallog += string.Format("{0}\r\n", data);
                            string _macincamera = "";

                            if (!data.Contains("eth0") || !data.Contains("lo")) {
                                GlobalData.manualData.mac_manuallog += "Dữ liệu đọc về sai định dạng.\r\n";
                                GlobalData.manualData.mac_manuallog += "Kết thúc.\r\n";
                                MessageBox.Show("Finished.", "Check MAC", MessageBoxButton.OK, MessageBoxImage.Error);
                                break;
                            }
                            else {
                                data = data.Split(new string[] { "eth0" }, StringSplitOptions.None)[1];
                                data = data.Split(new string[] { "lo" }, StringSplitOptions.None)[0];
                                data = data.Split(new string[] { "Link encap:Ethernet  HWaddr" }, StringSplitOptions.None)[1];
                                data = data.Trim();
                                _macincamera = data.Substring(0, 17).Trim();
                                App.Current.Dispatcher.Invoke(new Action(() => {
                                    txtMACin.Text = _macincamera.Replace(":", "");
                                }));
                                GlobalData.manualData.mac_manuallog += string.Format("\r\n\r\nĐỊA CHỈ MAC ĐỌC VỀ TỪ IP CAMERA: {0}\r\n", _macincamera);

                                GlobalData.manualData.mac_manuallog += "So sánh địa chỉ MAC.......................\r\n";
                                App.Current.Dispatcher.Invoke(new Action(() => {
                                    ret = txtMACin.Text == txtMACout.Text;
                                    MACResult.Content = ret == true ? "PASS" : "FAIL";

                                }));
                                GlobalData.manualData.mac_manuallog += string.Format("{0}\r\n", ret == true ? "PASS" : "FAIL");
                                GlobalData.manualData.mac_manuallog += "Kết thúc.\r\n";
                                MessageBox.Show("Finished.", "Check MAC", MessageBoxButton.OK, MessageBoxImage.Information);
                            }

                            break;
                        }

                    //CHECK WIFI
                    case "KIỂM TRA KẾT NỐI WIFI": {
                            GlobalData.manualData.wifi_manuallog = "";
                            ret = false;
                            try {
                                App.Current.Dispatcher.Invoke(new Action(() => {
                                    WIFIResult.Content = "--";
                                }));
                                GlobalData.manualData.wifi_manuallog += "\r\nKIỂM TRA KẾT NỐI WIFI CỦA IP CAMERA >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>\r\n";
                                GlobalData.manualData.wifi_manuallog += string.Format("Phần mềm gửi lệnh: ifconfig eth0 down  \r\n");
                                GlobalData.testingDataDUT.CAMERALOG = "";
                                GlobalData.camera.WriteLine("ifconfig eth0 down");
                                GlobalData.manualData.wifi_manuallog += "Delay 1000 ms \r\n";
                                Thread.Sleep(1000);
                                GlobalData.manualData.wifi_manuallog += "CAMERA Feedback:\r\n" + GlobalData.testingDataDUT.CAMERALOG + "\r\n";
                                GlobalData.manualData.wifi_manuallog += string.Format("Phần mềm gửi lệnh: nm_cfg client IPCAM-Test-1  \r\n");
                                GlobalData.testingDataDUT.CAMERALOG = "";

                                App.Current.Dispatcher.Invoke(new Action(() => {
                                    GlobalData.camera.WriteLine(string.Format("nm_cfg client {0}", txtSSID.Text));
                                }));

                                GlobalData.manualData.wifi_manuallog += "Delay 1000 ms \r\n";
                                Thread.Sleep(1000);
                                GlobalData.manualData.wifi_manuallog += "CAMERA Feedback:\r\n" + GlobalData.testingDataDUT.CAMERALOG + "\r\n";
                                int count = 0;
                                REP:
                                GlobalData.manualData.wifi_manuallog += string.Format("Kiểm tra kết nối LẦN {0}\r\n", count);
                                count++;
                                GlobalData.manualData.wifi_manuallog += string.Format("Phần mềm gửi lệnh: nm_cfg status  \r\n");
                                GlobalData.camera.WriteLine("nm_cfg status");
                                GlobalData.testingDataDUT.CAMERALOG = "";
                                GlobalData.manualData.wifi_manuallog += "Delay 500 ms \r\n";
                                Thread.Sleep(500);
                                GlobalData.manualData.wifi_manuallog += "CAMERA Feedback:\r\n" + GlobalData.testingDataDUT.CAMERALOG + "\r\n";
                                ret = GlobalData.testingDataDUT.UARTLOG.Contains("status	= connect");
                                if (!ret) {
                                    if (count < 20) {
                                        Thread.Sleep(500);
                                        goto REP;
                                    }
                                }
                                GlobalData.manualData.wifi_manuallog += string.Format("KẾT QUẢ KIỂM TRA WIFI: {0}\r\n", ret == true ? "PASS" : "FAIL");
                                App.Current.Dispatcher.Invoke(new Action(() => {
                                    WIFIResult.Content = ret == true ? Parameters.testStatus.PASS.ToString() : Parameters.testStatus.FAIL.ToString();
                                }));

                                MessageBox.Show("Finished.", "Check WIFI", MessageBoxButton.OK, ret == true ? MessageBoxImage.Information : MessageBoxImage.Error);
                            }
                            catch (Exception ex) {
                                App.Current.Dispatcher.Invoke(new Action(() => {
                                    WIFIResult.Content = Parameters.testStatus.FAIL.ToString();
                                }));
                                GlobalData.manualData.wifi_manuallog += ex.ToString() + "\r\n";
                                GlobalData.manualData.wifi_manuallog += string.Format("KẾT QUẢ KIỂM TRA WIFI: FAIL\r\n");
                                MessageBox.Show("Finished.", "Check WIFI", MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                            break;
                        }

                    //CHECK SD CARD
                    case "KIỂM TRA SD CARD": {
                            GlobalData.manualData.sdcard_manuallog = "";
                            ret = false;
                            GlobalData.testingDataDUT.CAMERALOG = "";
                            try {
                                App.Current.Dispatcher.Invoke(new Action(() => {
                                    SDCardResult.Content = "--";
                                }));
                                GlobalData.manualData.sdcard_manuallog += "\r\nKIỂM TRA THẺ NHỚ SD CARD CỦA IP CAMERA >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>\r\n";
                                int count = 0;
                                REP:
                                count++;
                                GlobalData.manualData.sdcard_manuallog += string.Format("Phần mềm gửi lệnh: mount  LẦN {0}\r\n", count);
                                string data = "";
                                GlobalData.camera.WriteLine("mount");
                                GlobalData.manualData.sdcard_manuallog += "Delay 1000 ms \r\n";
                                Thread.Sleep(1000);
                                data = GlobalData.initSetting.station == "SAU-DONG-VO" ? GlobalData.camera.Read0() : GlobalData.testingDataDUT.CAMERALOG;
                                GlobalData.manualData.sdcard_manuallog += "CAMERA Feedback:\r\n" + data + "\r\n";
                                ret = data.Contains("/dev/mmcblk0p1 on /media/sdcard type vfat (rw,relatime,fmask=0000,dmask=0000");
                                if (!ret) {
                                    if (count < 5) {
                                        Thread.Sleep(1000);
                                        goto REP;
                                    }
                                }
                                App.Current.Dispatcher.Invoke(new Action(() => {
                                    SDCardResult.Content = ret == true ? Parameters.testStatus.PASS.ToString() : Parameters.testStatus.FAIL.ToString();
                                }));
                                GlobalData.manualData.sdcard_manuallog += string.Format("KẾT QUẢ KIỂM TRA THẺ NHỚ SD card: {0}\r\n", ret == true ? "PASS" : "FAIL");
                                MessageBox.Show("Finished.", "Check SDCard", MessageBoxButton.OK, ret == true ? MessageBoxImage.Information : MessageBoxImage.Error);
                            }
                            catch (Exception ex) {
                                GlobalData.manualData.sdcard_manuallog += ex.ToString() + "\r\n";
                                GlobalData.manualData.sdcard_manuallog += string.Format("KẾT QUẢ KIỂM TRA THẺ NHỚ SD card: FAIL\r\n");
                                App.Current.Dispatcher.Invoke(new Action(() => {
                                    SDCardResult.Content = Parameters.testStatus.FAIL.ToString();
                                }));
                                MessageBox.Show("Finished.", "Check SDCard", MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                            break;
                        }

                    //CHECK LAN
                    case "KIỂM TRA CỔNG LAN": {
                            GlobalData.manualData.lan_manuallog = "";
                            ret = false;
                            GlobalData.testingDataDUT.CAMERALOG = "";
                            try {
                                App.Current.Dispatcher.Invoke(new Action(() => {
                                    LANResult.Content = "--";
                                }));
                                GlobalData.manualData.lan_manuallog += "\r\nKIỂM TRA CỔNG LAN CỦA IP CAMERA >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>\r\n";
                                GlobalData.manualData.lan_manuallog += "Phần mềm gửi lệnh: ifconfig eth0 up\r\n";
                                GlobalData.testingDataDUT.CAMERALOG = "";
                                GlobalData.camera.WriteLine("ifconfig eth0 up");
                                GlobalData.manualData.lan_manuallog += "Delay 500 ms\r\n";
                                Thread.Sleep(500);
                                GlobalData.manualData.lan_manuallog += "CAMERA Feedback:\r\n" + GlobalData.testingDataDUT.CAMERALOG + "\r\n";
                                GlobalData.testingDataDUT.CAMERALOG = "";
                                GlobalData.manualData.lan_manuallog += "Phần mềm gửi lệnh: ping -c 4 192.168.1.100\r\n";
                                GlobalData.camera.WriteLine("ping -c 4 192.168.1.100");
                                GlobalData.manualData.lan_manuallog += "Delay 5000 ms\r\n";
                                Thread.Sleep(5000);
                                GlobalData.manualData.lan_manuallog += "CAMERA Feedback:\r\n" + GlobalData.testingDataDUT.CAMERALOG + "\r\n";
                                ret = GlobalData.testingDataDUT.CAMERALOG.Contains("64 bytes from 192.168.1.100:");
                                App.Current.Dispatcher.Invoke(new Action(() => {
                                    LANResult.Content = ret == true ? Parameters.testStatus.PASS.ToString() : Parameters.testStatus.FAIL.ToString();
                                }));
                                GlobalData.manualData.lan_manuallog += string.Format("KẾT QUẢ KIỂM TRA CỔNG LAN: {0}\r\n", ret == true ? "PASS" : "FAIL");
                                MessageBox.Show("Finished.", "Check LAN", MessageBoxButton.OK, ret == true ? MessageBoxImage.Information : MessageBoxImage.Error);
                            }
                            catch (Exception ex) {
                                GlobalData.manualData.lan_manuallog += ex.ToString() + "\r\n";
                                GlobalData.manualData.lan_manuallog += string.Format("KẾT QUẢ KIỂM TRA CỔNG LAN: FAIL\r\n");
                                App.Current.Dispatcher.Invoke(new Action(() => {
                                    LANResult.Content = Parameters.testStatus.FAIL.ToString();
                                }));
                                MessageBox.Show("Finished.", "Check LAN", MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                            break;
                        }

                    //CHECK SPEAKER, MIC
                    case "THU ÂM VÀO CAMERA": {
                            GlobalData.manualData.audio_manuallog = "";
                            GlobalData.camera.WriteLine("\n");
                            GlobalData.manualData.audio_manuallog += "Phần mềm gửi lệnh: killall lark\r\n";
                            GlobalData.camera.WriteLine("killall lark");
                            Thread.Sleep(500);
                            GlobalData.manualData.audio_manuallog += "Gửi lệnh yêu cầu IP Camera thu âm:\r\n";
                            GlobalData.manualData.audio_manuallog += "arecord -D hw:0,1 /tmp/audio_record.wav & \r\n";
                            GlobalData.camera.WriteLine("arecord -D hw:0,1 /tmp/audio_record.wav &");
                            Thread.Sleep(500);
                            GlobalData.camera.WriteLine("\n");
                            GlobalData.manualData.audio_manuallog += "Máy tính phát âm thanh ra loa:\r\n";
                            Speaker speaker = new Speaker();
                            speaker.PlaySound(ref message);
                            Thread.Sleep(3000);
                            GlobalData.manualData.audio_manuallog += "delay 3000 ms\r\n";
                            GlobalData.manualData.audio_manuallog += "Phần mềm gửi lệnh dừng thu âm: killall arecord \r\n";
                            GlobalData.camera.WriteLine("killall arecord");
                            MessageBox.Show("Finished.", "Record Sound", MessageBoxButton.OK, MessageBoxImage.Information);
                            break;
                        }

                    case "PHÁT ÂM CỦA CAMERA": {
                            GlobalData.manualData.audio_manuallog = "";
                            GlobalData.manualData.audio_manuallog += "Gửi lệnh phát âm thanh ra loa IP Camera:\r\n";
                            GlobalData.manualData.audio_manuallog += "aplay - D hw: 0,1 / tmp / audio_record.wav\r\n";
                            GlobalData.camera.WriteLine("aplay  -D hw:0,1 /tmp/audio_record.wav");
                            GlobalData.manualData.audio_manuallog += "delay 5000ms\r\n";
                            System.Threading.Thread.Sleep(4000);
                            MessageBox.Show("Finished.", "Play Sound", MessageBoxButton.OK, MessageBoxImage.Information);
                            break;
                        }

                    //CHECK IMAGE SENSOR
                    case "LIVE STREAM CAMERA": {
                            Process.Start("LiveCam\\LiveCamera.exe");
                            break;
                        }

                    //CHECK RGB LED
                    case "BẬT LED XANH": {
                            GlobalData.manualData.rgbled_manuallog = "";
                            GlobalData.manualData.rgbled_manuallog += "killall vnptledindicator\r\n";
                            GlobalData.camera.WriteLine("killall vnptledindicator");
                            GlobalData.manualData.rgbled_manuallog += "Delay 100ms\r\n";
                            Thread.Sleep(100);
                            GlobalData.manualData.rgbled_manuallog += "echo 1> /sys/devices/platform/pwm_platform/settings/pwm/request\r\n";
                            GlobalData.camera.WriteLine("echo 1> /sys/devices/platform/pwm_platform/settings/pwm/request");
                            GlobalData.manualData.rgbled_manuallog += "Delay 100ms\r\n";
                            Thread.Sleep(100);
                            GlobalData.manualData.rgbled_manuallog += "echo 1000000 > /sys/devices/platform/pwm_platform/settings/pwm1/duty_ns\r\n";
                            GlobalData.camera.WriteLine("echo 1000000 > /sys/devices/platform/pwm_platform/settings/pwm1/duty_ns");
                            GlobalData.manualData.rgbled_manuallog += "Delay 10ms\r\n";
                            Thread.Sleep(10);
                            GlobalData.manualData.rgbled_manuallog += "echo 1 > /sys/devices/platform/pwm_platform/settings/pwm1/enable\r\n";
                            GlobalData.camera.WriteLine("echo 1 > /sys/devices/platform/pwm_platform/settings/pwm1/enable");
                            GlobalData.manualData.rgbled_manuallog += "Delay 10ms\r\n";
                            Thread.Sleep(10);
                            GlobalData.manualData.rgbled_manuallog += "echo 0 > /sys/devices/platform/pwm_platform/settings/pwm2/duty_ns\r\n";
                            GlobalData.camera.WriteLine("echo 0 > /sys/devices/platform/pwm_platform/settings/pwm2/duty_ns");
                            GlobalData.manualData.rgbled_manuallog += "Delay 10ms\r\n";
                            Thread.Sleep(10);
                            GlobalData.manualData.rgbled_manuallog += "echo 1 > /sys/devices/platform/pwm_platform/settings/pwm2/enable\r\n";
                            GlobalData.camera.WriteLine("echo 1 > /sys/devices/platform/pwm_platform/settings/pwm2/enable");
                            GlobalData.manualData.rgbled_manuallog += "Delay 10ms\r\n";
                            Thread.Sleep(10);
                            MessageBox.Show("Finished.", "Turn on Green LED", MessageBoxButton.OK, MessageBoxImage.Information);
                            break;
                        }

                    case "BẬT LED ĐỎ": {
                            GlobalData.manualData.rgbled_manuallog = "";
                            GlobalData.manualData.rgbled_manuallog += "killall vnptledindicator\r\n";
                            GlobalData.camera.WriteLine("killall vnptledindicator");
                            GlobalData.manualData.rgbled_manuallog += "Delay 100ms\r\n";
                            Thread.Sleep(100);
                            GlobalData.manualData.rgbled_manuallog += "echo 1> /sys/devices/platform/pwm_platform/settings/pwm/request\r\n";
                            GlobalData.camera.WriteLine("echo 1> /sys/devices/platform/pwm_platform/settings/pwm/request");
                            GlobalData.manualData.rgbled_manuallog += "Delay 100ms\r\n";
                            Thread.Sleep(100);
                            GlobalData.manualData.rgbled_manuallog += "echo 1000000 > /sys/devices/platform/pwm_platform/settings/pwm2/duty_ns\r\n";
                            GlobalData.camera.WriteLine("echo 1000000 > /sys/devices/platform/pwm_platform/settings/pwm2/duty_ns");
                            GlobalData.manualData.rgbled_manuallog += "Delay 10ms\r\n";
                            Thread.Sleep(10);
                            GlobalData.manualData.rgbled_manuallog += "echo 1 > /sys/devices/platform/pwm_platform/settings/pwm2/enable\r\n";
                            GlobalData.camera.WriteLine("echo 1 > /sys/devices/platform/pwm_platform/settings/pwm2/enable");
                            GlobalData.manualData.rgbled_manuallog += "Delay 10ms\r\n";
                            Thread.Sleep(10);
                            GlobalData.manualData.rgbled_manuallog += "echo 0 > /sys/devices/platform/pwm_platform/settings/pwm1/duty_ns\r\n";
                            GlobalData.camera.WriteLine("echo 0 > /sys/devices/platform/pwm_platform/settings/pwm1/duty_ns");
                            GlobalData.manualData.rgbled_manuallog += "Delay 10ms\r\n";
                            Thread.Sleep(10);
                            GlobalData.manualData.rgbled_manuallog += "echo 1 > /sys/devices/platform/pwm_platform/settings/pwm1/enable\r\n";
                            GlobalData.camera.WriteLine("echo 1 > /sys/devices/platform/pwm_platform/settings/pwm1/enable");
                            GlobalData.manualData.rgbled_manuallog += "Delay 10ms\r\n";
                            Thread.Sleep(10);
                            MessageBox.Show("Finished.", "Turn on RED LED", MessageBoxButton.OK, MessageBoxImage.Information);
                            break;
                        }

                    //CHECK LIGHT SENSOR
                    case "KIỂM TRA MỨC SÁNG": {
                            GlobalData.manualData.lightsensor_manuallog = "";
                            App.Current.Dispatcher.Invoke(new Action(() => {
                                LightSensorResult.Content = "--";
                                txtADC.Clear();
                            }));
                            MessageBox.Show("VUI LÒNG KHÔNG CHE CẢM BIẾN ÁNH SÁNG.", "Check Light Sensor", MessageBoxButton.OK, MessageBoxImage.Warning);
                            GlobalData.manualData.lightsensor_manuallog += "Kiểm tra mức sáng:\r\n";
                            int count = 0;
                            REP:
                            count++;
                            GlobalData.testingDataDUT.CAMERALOG = "";
                            GlobalData.manualData.lightsensor_manuallog += "cat /sys/devices/platform/rts_saradc.0/in0_input";
                            GlobalData.camera.WriteLine("cat /sys/devices/platform/rts_saradc.0/in0_input");
                            Thread.Sleep(500);
                            GlobalData.manualData.lightsensor_manuallog += "delay 500ms";
                            string data = GlobalData.initSetting.station == "SAU-DONG-VO" ? GlobalData.camera.Read0() : GlobalData.testingDataDUT.CAMERALOG;
                            GlobalData.manualData.lightsensor_manuallog += string.Format("{0}\r\n", data);

                            data = data.Replace("\r", "").Replace("\n", "").Trim();
                            data = data.Replace("cat /sys/devices/platform/rts_saradc.0/in0_input", "");
                            data = data.Replace("~ #", "");
                            App.Current.Dispatcher.Invoke(new Action(() => {
                                txtADC.Text = data;
                            }));
                            ret = int.Parse(data) > int.Parse(GlobalData.initSetting.adcvalue);
                            if (!ret) {
                                if (count < 10) {
                                    Thread.Sleep(1000);
                                    goto REP;
                                }
                            }

                            App.Current.Dispatcher.Invoke(new Action(() => {
                                LightSensorResult.Content = ret == true ? Parameters.testStatus.PASS.ToString() : Parameters.testStatus.FAIL.ToString();
                            }));
                            GlobalData.manualData.lightsensor_manuallog += string.Format("KẾT QUẢ KIỂM TRA MỨC SÁNG LIGHT SENSOR: {0}\r\n", ret == true ? "PASS" : "FAIL");
                            MessageBox.Show("Finished.", "Check Light Sensor", MessageBoxButton.OK, ret == true ? MessageBoxImage.Information : MessageBoxImage.Error);
                            break;
                        }

                    case "KIỂM TRA MỨC TỐI": {
                            GlobalData.manualData.lightsensor_manuallog = "";
                            App.Current.Dispatcher.Invoke(new Action(() => {
                                LightSensorResult.Content = "--";
                                txtADC.Clear();
                            }));
                            MessageBox.Show("VUI LÒNG CHE CẢM BIẾN ÁNH SÁNG.", "Check Light Sensor", MessageBoxButton.OK, MessageBoxImage.Warning);
                            GlobalData.manualData.lightsensor_manuallog += "Kiểm tra mức tối:\r\n";
                            int count = 0;
                            REP:
                            count++;
                            GlobalData.testingDataDUT.CAMERALOG = "";
                            GlobalData.manualData.lightsensor_manuallog += "cat /sys/devices/platform/rts_saradc.0/in0_input";
                            GlobalData.camera.WriteLine("cat /sys/devices/platform/rts_saradc.0/in0_input");
                            Thread.Sleep(500);
                            GlobalData.manualData.lightsensor_manuallog += "delay 500ms";
                            string data = GlobalData.initSetting.station == "SAU-DONG-VO" ? GlobalData.camera.Read0() : GlobalData.testingDataDUT.CAMERALOG;
                            GlobalData.manualData.lightsensor_manuallog += string.Format("{0}\r\n", data);

                            data = data.Replace("\r", "").Replace("\n", "").Trim();
                            data = data.Replace("cat /sys/devices/platform/rts_saradc.0/in0_input", "");
                            data = data.Replace("~ #", "");
                            App.Current.Dispatcher.Invoke(new Action(() => {
                                txtADC.Text = data;
                            }));
                            ret = int.Parse(data) < int.Parse(GlobalData.initSetting.adcvalue);
                            if (!ret) {
                                if (count < 10) {
                                    Thread.Sleep(1000);
                                    goto REP;
                                }
                            }

                            App.Current.Dispatcher.Invoke(new Action(() => {
                                LightSensorResult.Content = ret == true ? Parameters.testStatus.PASS.ToString() : Parameters.testStatus.FAIL.ToString();
                            }));
                            GlobalData.manualData.lightsensor_manuallog += string.Format("KẾT QUẢ KIỂM TRA MỨC TỐI LIGHT SENSOR: {0}\r\n", ret == true ? "PASS" : "FAIL");
                            MessageBox.Show("Finished.", "Check Light Sensor", MessageBoxButton.OK, ret == true ? MessageBoxImage.Information : MessageBoxImage.Error);
                            break;
                        }

                    //CHECK BUTTON
                    case "KIỂM TRA NÚT NHẤN IP CAMERA": {
                            GlobalData.manualData.button_manuallog = "";
                            App.Current.Dispatcher.Invoke(new Action(() => {
                                ButtonResult.Content = "--";
                            }));
                            GlobalData.manualData.button_manuallog += "\r\nKIỂM TRA NÚT BẤM CỦA IP CAMERA >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>\r\n";
                            GlobalData.manualData.button_manuallog += string.Format("Đang chờ người dùng thực hiện nhấn nút bấm...  \r\n");
                            GlobalData.testingDataDUT.CAMERALOG = "";
                            int count = 0;
                            REP:
                            count++;
                            GlobalData.manualData.button_manuallog += string.Format("Thời gian chờ {0} s ...  \r\n", count);
                            GlobalData.manualData.button_manuallog += "CAMERA Feedback:\r\n" + GlobalData.testingDataDUT.CAMERALOG + "\r\n";
                            ret = GlobalData.testingDataDUT.UARTLOG.Contains("time_process = 0") || GlobalData.testingDataDUT.UARTLOG.Contains("time_process = 1");
                            if (!ret) {
                                if (count <= 30) {
                                    Thread.Sleep(1000);
                                    goto REP;
                                }
                            }
                            GlobalData.manualData.button_manuallog += string.Format("KẾT QUẢ KIỂM TRA NÚT BẤM: {0}\r\n", ret == true ? "PASS" : "FAIL");
                            App.Current.Dispatcher.Invoke(new Action(() => {
                                ButtonResult.Content = ret == true ? Parameters.testStatus.PASS.ToString() : Parameters.testStatus.FAIL.ToString();
                            }));
                            MessageBox.Show("Finished.", "Check Button", MessageBoxButton.OK, ret == true ? MessageBoxImage.Information : MessageBoxImage.Error);
                            break;
                        }

                    //UPLOAD FW
                    case "UPLOAD FIRMWARE": {
                            GlobalData.manualData.fw_manuallog = "";
                            App.Current.Dispatcher.Invoke(new Action(() => {
                                UploadFWResult.Content = "--";
                            }));
                            int count = 0, _max = 200;
                            REP:
                            //Access to uboot (timeout = 20s)
                            GlobalData.manualData.fw_manuallog += "Đang chờ thiết bị khởi động...\r\n";
                            count++;
                            ret = GlobalData.testingDataDUT.CAMERALOG.Contains("Hit any key to stop autoboot:");
                            if (!ret) {
                                if (count < _max) {
                                    Thread.Sleep(100);
                                    goto REP;
                                }
                                GlobalData.manualData.fw_manuallog += "Không đăng nhập được vào uboot\r\n";
                                break;
                            }


                            GlobalData.testingDataDUT.CAMERALOG = "";
                            GlobalData.manualData.fw_manuallog += "\r\nĐANG NẠP FIRMWARE CHO IP CAMERA >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>\r\n";
                            int _count = 0, _timeout = 90;
                            string _cmdPath = string.Format("{0}UPLOAD\\cmd.txt", System.AppDomain.CurrentDomain.BaseDirectory);
                            string _wpsPath = string.Format("{0}UPLOAD\\wps.txt", System.AppDomain.CurrentDomain.BaseDirectory);
                            string _runPath = string.Format("{0}UPLOAD\\RunPowerShell.exe", System.AppDomain.CurrentDomain.BaseDirectory);

                            //Set IP Camera ve che do Upload FW
                            GlobalData.testingDataDUT.CAMERALOG = "";
                            GlobalData.manualData.fw_manuallog += "Phần mềm gửi lệnh: setipaddr 192.168.1.253 \r\n";
                            GlobalData.camera.WriteLine("setipaddr 192.168.1.253");
                            GlobalData.manualData.fw_manuallog += "Delay 500 ms\r\n";
                            Thread.Sleep(500);
                            GlobalData.manualData.fw_manuallog += "CAMERA Feedback:\r\n" + GlobalData.testingDataDUT.CAMERALOG + "\r\n";
                            GlobalData.testingDataDUT.CAMERALOG = "";
                            GlobalData.manualData.fw_manuallog += "Phần mềm gửi lệnh: saveenv\r\n";
                            GlobalData.camera.WriteLine("saveenv");
                            GlobalData.manualData.fw_manuallog += "Delay 500 ms\r\n";
                            Thread.Sleep(500);
                            GlobalData.manualData.fw_manuallog += "CAMERA Feedback:\r\n" + GlobalData.testingDataDUT.CAMERALOG + "\r\n";
                            GlobalData.manualData.fw_manuallog += "Phần mềm gửi lệnh: update all\r\n";
                            GlobalData.camera.WriteLine("update all");
                            GlobalData.manualData.fw_manuallog += "Delay 1000 ms\r\n";
                            Thread.Sleep(1000);
                            GlobalData.manualData.fw_manuallog += "Đang chờ client gửi FIRMWARE lên thiết bị......\r\n";
                            //Upload file linux.bin
                            //********************************************//
                            //1. Set đường dẫn file FW
                            File.WriteAllText(_cmdPath, string.Format("tftp -i 192.168.1.253 put {0}", GlobalData.initSetting.linuxpath));
                            Thread.Sleep(100);
                            Process.Start(_runPath);

                            GlobalData.testingDataDUT.CAMERALOG = "";
                            REP1:
                            _count++;
                            ret = GlobalData.testingDataDUT.CAMERALOG.Contains("update all done");
                            if (!ret) {
                                GlobalData.manualData.fw_manuallog += string.Format("Chờ {0}s\r\n", _count);
                                if (_count < _timeout) {
                                    Thread.Sleep(1000);
                                    goto REP1;
                                }
                            }
                            GlobalData.manualData.fw_manuallog += "CAMERA Feedback: \r\n" + GlobalData.testingDataDUT.CAMERALOG + "\r\n";
                            GlobalData.manualData.fw_manuallog += string.Format("KẾT QUẢ UPLOAD FILE linux.bin: {0}\r\n", ret == true ? "PASS" : "FAIL");

                            //Upload file mcu.bin
                            //********************************************//
                            GlobalData.manualData.fw_manuallog += "Phần mềm gửi lệnh: update fw\r\n";
                            GlobalData.camera.WriteLine("update fw");
                            GlobalData.manualData.fw_manuallog += "Delay 1000 ms\r\n";
                            Thread.Sleep(1000);
                            GlobalData.manualData.fw_manuallog += "Đang chờ client gửi FIRMWARE lên thiết bị......\r\n";
                            File.WriteAllText(_cmdPath, string.Format("tftp -i 192.168.1.253 put {0}", GlobalData.initSetting.mcupath));
                            Thread.Sleep(100);
                            Process.Start(_runPath);

                            GlobalData.testingDataDUT.CAMERALOG = "";
                            _count = 0;
                            REP2:
                            _count++;
                            ret = GlobalData.testingDataDUT.CAMERALOG.Contains("update fw done");
                            if (!ret) {
                                if (_count < _timeout) {
                                    GlobalData.manualData.fw_manuallog += string.Format("Chờ {0}s\r\n", _count);
                                    Thread.Sleep(1000);
                                    goto REP2;
                                }
                            }
                            GlobalData.manualData.fw_manuallog += "CAMERA Feedback: \r\n" + GlobalData.testingDataDUT.CAMERALOG + "\r\n";
                            GlobalData.manualData.fw_manuallog += string.Format("KẾT QUẢ UPLOAD FILE mcu.bin: {0}\r\n", ret == true ? "PASS" : "FAIL");


                            //Upload file ldc.bin
                            //********************************************//
                            GlobalData.manualData.fw_manuallog += "Phần mềm gửi lệnh: update ldc\r\n";
                            GlobalData.camera.WriteLine("update ldc");
                            GlobalData.manualData.fw_manuallog += "Delay 1000 ms\r\n";
                            Thread.Sleep(1000);
                            GlobalData.manualData.fw_manuallog += "Đang chờ client gửi FIRMWARE lên thiết bị......\r\n";
                            File.WriteAllText(_cmdPath, string.Format("tftp -i 192.168.1.253 put {0}", GlobalData.initSetting.ldcpath));
                            Thread.Sleep(100);
                            Process.Start(_runPath);

                            GlobalData.testingDataDUT.CAMERALOG = "";
                            _count = 0;
                            REP3:
                            _count++;
                            ret = GlobalData.testingDataDUT.CAMERALOG.Contains("update ldc done");
                            if (!ret) {
                                if (_count < _timeout) {
                                    GlobalData.manualData.fw_manuallog += string.Format("Chờ {0}s\r\n", _count);
                                    Thread.Sleep(1000);
                                    goto REP3;
                                }
                            }
                            GlobalData.manualData.fw_manuallog += "CAMERA Feedback: \r\n" + GlobalData.testingDataDUT.CAMERALOG + "\r\n";
                            GlobalData.manualData.fw_manuallog += string.Format("KẾT QUẢ UPLOAD FILE ldc.bin: {0}\r\n", ret == true ? "PASS" : "FAIL");


                            GlobalData.manualData.fw_manuallog += string.Format("KẾT QUẢ UPLOAD FIRMWARE: {0}\r\n", ret == true ? "PASS" : "FAIL");
                            App.Current.Dispatcher.Invoke(new Action(() => {
                                UploadFWResult.Content = ret == true ? Parameters.testStatus.PASS.ToString() : Parameters.testStatus.FAIL.ToString();
                            }));
                            MessageBox.Show("Finished.", "Upload Firmware", MessageBoxButton.OK, ret == true ? MessageBoxImage.Information : MessageBoxImage.Error);
                            break;
                        }
                        
                }

                GlobalData.camera.Close();
                App.Current.Dispatcher.Invoke(new Action(() => {
                    b.Background = (SolidColorBrush)new BrushConverter().ConvertFrom("#FF039BE5");
                    b.IsEnabled = true;
                }));
            }));
            t.IsBackground = true;
            t.Start();
        }
    }
}
