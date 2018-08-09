using Microsoft.Win32;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using ToolTestIPCamera_Ver1.Function;

namespace ToolTestIPCamera_Ver1.UserControls {
    /// <summary>
    /// Interaction logic for ucSetting.xaml
    /// </summary>
    public partial class ucSetting : UserControl {
        public ucSetting() {
            InitializeComponent();
            this.cbbStation.ItemsSource = Parameters.ListStation;
            this.cbbUsbdebug1.ItemsSource = Parameters.ListUsbComport;
            this.DataContext = GlobalData.initSetting;
            BaseFunction.SelectCameraProtocol(GlobalData.initSetting.station);
        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            Button b = sender as Button;
            switch (b.Content) {
                case "SAVE SETTING": {
                        GlobalData.initSetting.SaveSetting();
                        BaseFunction.SelectCameraProtocol(GlobalData.initSetting.station);
                        MessageBox.Show("Success.","SAVE SETTING", MessageBoxButton.OK, MessageBoxImage.Information);
                        break;
                    }
            }

            switch (b.Name) {
                case "btnselectlinux": {
                        OpenFileDialog openFileDialog = new OpenFileDialog();
                        openFileDialog.Filter = "firmware *.bin|*.bin";
                        openFileDialog.Title = "Select path of file 'linux.bin'";
                        openFileDialog.FileName = "linux.bin";
                        if (openFileDialog.ShowDialog() == true) {
                            GlobalData.initSetting.linuxpath = openFileDialog.FileName;
                        }
                        break;
                    }
                case "btnselectmcu": {
                        OpenFileDialog openFileDialog = new OpenFileDialog();
                        openFileDialog.Filter = "firmware *.bin|*.bin";
                        openFileDialog.Title = "Select path of file 'mcu.bin'";
                        openFileDialog.FileName = "mcu.bin";
                        if (openFileDialog.ShowDialog() == true) {
                            GlobalData.initSetting.mcupath = openFileDialog.FileName;
                        }
                        break;
                    }
                case "btnselectldc": {
                        OpenFileDialog openFileDialog = new OpenFileDialog();
                        openFileDialog.Filter = "firmware *.bin|*.bin";
                        openFileDialog.Title = "Select path of file 'ldc.bin'";
                        openFileDialog.FileName = "ldc.bin";
                        if (openFileDialog.ShowDialog() == true) {
                            GlobalData.initSetting.ldcpath = openFileDialog.FileName;
                        }
                        break;
                    }
            }
        }
    }
}
