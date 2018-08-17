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
    /// Interaction logic for ucLog.xaml
    /// </summary>
    public partial class ucLog : UserControl {
        public ucLog() {
            InitializeComponent();

            this.logtest_datagrid.ItemsSource = GlobalData.datagridlogtest;
            this.logdetail_datagrid.ItemsSource = GlobalData.datagridlogdetail;
            this.loguart_datagrid.ItemsSource = GlobalData.datagridloguart;

        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            Button b = sender as Button;
            string _name = b.Name;
            switch (_name) {
                //case "sqlGetData": {
                //        this.bosareport_datagrid.ItemsSource = GlobalData.listBosaInfo;
                //        break;
                //    }

                //LOG TEST
                case "logtestListAll": {
                        int _count;
                        Function.IO.TestLog.ListAllFile(out _count);
                        MessageBox.Show(string.Format("Success.\n...\nThere are {0} log files found.", _count), "Message", MessageBoxButton.OK, MessageBoxImage.Information);
                        break;
                    }
                case "logtestOpen": {
                        try {
                            var row = (logfileinfo)this.logtest_datagrid.SelectedItem;
                            Function.IO.TestLog.Open(row.FileName);
                        }
                        catch {
                            MessageBox.Show("Vui lòng chọn file cần mở trước.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                        break;
                    }
                case "logtestOpenFolder": {
                        Function.IO.TestLog.OpenFolder();
                        break;
                    }

                //LOG DETAIL
                case "logdetailListAll": {
                        int _count;
                        Function.IO.SystemLog.ListAllFile(out _count);
                        MessageBox.Show(string.Format("Success.\n...\nThere are {0} log files found.", _count), "Message", MessageBoxButton.OK, MessageBoxImage.Information);
                        break;
                    }
                case "logdetailOpen": {
                        try {
                            var row = (logfileinfo)this.logdetail_datagrid.SelectedItem;
                            Function.IO.SystemLog.Open(row.FileName);
                        }
                        catch {
                            MessageBox.Show("Vui lòng chọn file cần mở trước.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                        break;
                    }
                case "logdetailOpenFolder": {
                        Function.IO.SystemLog.OpenFolder();
                        break;
                    }

                //LOG UART
                case "loguartListAll": {
                        int _count;
                        Function.IO.UARTLog.ListAllFile(out _count);
                        MessageBox.Show(string.Format("Success.\n...\nThere are {0} log files found.", _count), "Message", MessageBoxButton.OK, MessageBoxImage.Information);
                        break;
                    }
                case "loguartOpen": {
                        try {
                            var row = (logfileinfo)this.logdetail_datagrid.SelectedItem;
                            Function.IO.UARTLog.Open(row.FileName);
                        }
                        catch {
                            MessageBox.Show("Vui lòng chọn file cần mở trước.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                        break;
                    }
                case "loguartOpenFolder": {
                        Function.IO.UARTLog.OpenFolder();
                        break;
                    }
                default: break;
            }
        }

    }
}
