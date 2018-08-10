using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace ToolTestIPCamera_Ver1 {
    /// <summary>
    /// Interaction logic for ButtonWindow.xaml
    /// </summary>
    public partial class ButtonWindow : Window {

        int _count = 0;
        DispatcherTimer timer = null;
        int _maxcount = 0;

        public ButtonWindow(int _maxInSecond) {
            InitializeComponent();
            _maxcount = _maxInSecond;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) {
            //GlobalData.connectionManagement.WINDOWOPACITY = 0.5;
            timer = new DispatcherTimer();
            timer.Tick += new EventHandler(dispatcherTimer_Tick);
            timer.Interval = new TimeSpan(0, 0, 0, 0, 500);
            _count = 0;
            timer.Start();
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e) {
            this._count++;
            this.MainBorder.Background = this._count % 2 == 1 ? (SolidColorBrush)new BrushConverter().ConvertFrom("#EF692B") : (SolidColorBrush)new BrushConverter().ConvertFrom("#FFE738");
            tbTimeOut.Text = (_maxcount - (this._count / 2)).ToString();
            if (_count == _maxcount * 2) {
                timer.Stop();
                this.Close();
            }
        }


        private void Window_Unloaded(object sender, RoutedEventArgs e) {
            timer.Stop();
            //GlobalData.connectionManagement.WINDOWOPACITY = 1;
        }

        private void Label_MouseDown(object sender, MouseButtonEventArgs e) {
            this.Close();
        }
    }
}
