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
    /// Interaction logic for ReButtonWindow.xaml
    /// </summary>
    public partial class ReButtonWindow : Window {
        public ReButtonWindow() {
            InitializeComponent();
        }

        int _count = 0;
        DispatcherTimer timer = null;
        public bool buttonResult = false;



        private void Window_Loaded(object sender, RoutedEventArgs e) {
            timer = new DispatcherTimer();
            timer.Tick += new EventHandler(dispatcherTimer_Tick);
            timer.Interval = new TimeSpan(0, 0, 0, 0, 500);
            _count = 0;
            timer.Start();
        }

        private void Window_Unloaded(object sender, RoutedEventArgs e) {
            timer.Stop();
        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            Button b = (Button)sender;
            switch (b.Content) {
                case "OK": {
                        buttonResult = bdResult.Background == Brushes.Lime ? true : false;
                        this.Close();
                        break;
                    }
            }
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e) {
            this._count++;
            this.MainBorder.Background = this._count % 2 == 1 ? (SolidColorBrush)new BrushConverter().ConvertFrom("#EEEEEE") : (SolidColorBrush)new BrushConverter().ConvertFrom("#FFE738");
            tbTimeOut.Text = string.Format("{0}", 30 - (this._count / 2));

            if (30 - (this._count / 2) < 0) {
                timer.Stop();
                this.Close();
            }
        }

        private void Label_MouseDown(object sender, MouseButtonEventArgs e) {
            this.Close();
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e) {
            Border b = (Border)sender;
            if (b.Background == Brushes.Lime) b.Background = Brushes.Red;
            else b.Background = Brushes.Lime;
        }

    }
}
