using System;
using System.Collections.Generic;
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
    /// Interaction logic for RGBLEDWindow.xaml
    /// </summary>
    public partial class RGBLEDWindow : Window {

        int _count = 0;
        DispatcherTimer timer = null;
        public bool GreenLED = false;
        public bool RedLED = false;

        public RGBLEDWindow() {
            InitializeComponent();
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
            this.MainBorder.Background = this._count % 2 == 1 ? (SolidColorBrush)new BrushConverter().ConvertFrom("#EEEEEE") : (SolidColorBrush)new BrushConverter().ConvertFrom("#FFE738");
            //------------------------------------------------//
            GlobalData.camera.WriteLine("killall vnptledindicator");
            Thread.Sleep(100);
            GlobalData.camera.WriteLine("echo 1> /sys/devices/platform/pwm_platform/settings/pwm/request");
            Thread.Sleep(100);
            //bat led xanh
            if (this._count % 2 == 1) {
                GlobalData.camera.WriteLine("echo 1000000 > /sys/devices/platform/pwm_platform/settings/pwm1/duty_ns");
                Thread.Sleep(10);
                GlobalData.camera.WriteLine("echo 1 > /sys/devices/platform/pwm_platform/settings/pwm1/enable");
                Thread.Sleep(10);
                GlobalData.camera.WriteLine("echo 0 > /sys/devices/platform/pwm_platform/settings/pwm2/duty_ns");
                Thread.Sleep(10);
                GlobalData.camera.WriteLine("echo 1 > /sys/devices/platform/pwm_platform/settings/pwm2/enable");
                Thread.Sleep(10);
            }
            //bat led do
            else {
                GlobalData.camera.WriteLine("echo 1000000 > /sys/devices/platform/pwm_platform/settings/pwm2/duty_ns");
                Thread.Sleep(10);
                GlobalData.camera.WriteLine("echo 1 > /sys/devices/platform/pwm_platform/settings/pwm2/enable");
                Thread.Sleep(10);
                GlobalData.camera.WriteLine("echo 0 > /sys/devices/platform/pwm_platform/settings/pwm1/duty_ns");
                Thread.Sleep(10);
                GlobalData.camera.WriteLine("echo 1 > /sys/devices/platform/pwm_platform/settings/pwm1/enable");
                Thread.Sleep(10);
            }
        }

        private void Window_Unloaded(object sender, RoutedEventArgs e) {
            timer.Stop();
        }

        private void Label_MouseDown(object sender, MouseButtonEventArgs e) {
            this.Close();
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e) {
            Border b = (Border)sender;
            if (b.Background == Brushes.Lime) b.Background = Brushes.Red;
            else b.Background = Brushes.Lime;
        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            Button b = (Button)sender;
            switch (b.Content) {
                case "OK": {
                        GreenLED = bdGreen.Background == Brushes.Lime ? true : false;
                        RedLED = bdRed.Background == Brushes.Lime ? true : false;
                        this.Close();
                        break;
                    }
            }
        }
    }
}
