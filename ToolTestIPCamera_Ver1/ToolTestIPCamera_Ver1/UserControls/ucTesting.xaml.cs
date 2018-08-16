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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using ToolTestIPCamera_Ver1.Function;
using System.Diagnostics;

namespace ToolTestIPCamera_Ver1.UserControls {
    /// <summary>
    /// Interaction logic for ucTesting.xaml
    /// </summary>
    public partial class ucTesting : UserControl {

        public ucTesting() {
            InitializeComponent();
            this.DataContext = GlobalData.testingDataDUT;

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Tick += ((sender, e) => {
                if (GlobalData.testingDataDUT.TOTALRESULT == Parameters.testStatus.Wait.ToString()) {
                    _scrollViewer1.ScrollToEnd();
                    _scrollViewer2.ScrollToEnd();
                }
                else {
                    txtMAC.Focus();
                }
                    
            });
            timer.Start();
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e) {
            TextBox txt = (TextBox)sender;
            if (e.Key == Key.Enter) {
                bool ret = false;
                Stopwatch st = new Stopwatch();
                st.Start();

                //Check MAC Address is valid or not
                GlobalData.testingDataDUT.SYSTEMLOG = "";
                GlobalData.testingDataDUT.SYSTEMLOG += "\r\nKIỂM TRA ĐỊA CHỈ MAC ADDRESS >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>\r\n";
                string _mac = txt.Text.Replace(":","").Trim();
                GlobalData.testingDataDUT.SYSTEMLOG +=  string.Format("MAC: {0}\r\n", _mac);
                string message = "";
                if (!BaseFunction.IsMacAddress(_mac, ref message)) {
                    GlobalData.testingDataDUT.SYSTEMLOG += message + "\r\n";
                    GlobalData.testingDataDUT.TOTALRESULT = Parameters.testStatus.FAIL.ToString();
                    GlobalData.testingDataDUT.SYSTEMLOG += string.Format("KẾT QUẢ KIỂM TRA MAC ADDRESS: FAIL\r\n");
                    GlobalData.testingDataDUT.OLDMAC = string.Format("Old MAC: {0}, FAIL", _mac);
                    txt.Clear();
                    txt.Focus();
                    return;
                }
                GlobalData.testingDataDUT.SYSTEMLOG += string.Format("KẾT QUẢ KIỂM TRA MAC ADDRESS: PASS\r\n");

                Thread t = new Thread(new ThreadStart(() => {
                   
                    //Test Function
                    switch (GlobalData.initSetting.station) {
                        case "PCBA-LAYER2": {
                                ret = new Function.Excute.ExcuteTestLayer2().Excute();
                                break;
                            }
                        case "PCBA-LAYER3": {
                                ret = new Function.Excute.ExcuteTestLayer3().Excute();
                                break;
                            }
                        case "SAU-DONG-VO": {
                                ret = new Function.Excute.ExcuteTestProduct().Excute();
                                break;
                            }
                    }
                    GlobalData.testingDataDUT.MACADDRESS = "";
                    GlobalData.testingDataDUT.ENABLETEXTBOX = true;
                    GlobalData.testingDataDUT.OLDMAC = string.Format("Old MAC: {0}, {1}", _mac, ret == true ? "PASS" : "FAIL");
                    GlobalData.testingDataDUT.TOTALRESULT = ret == true ? Parameters.testStatus.PASS.ToString() : Parameters.testStatus.FAIL.ToString();
                    GlobalData.testingDataDUT.FinishCheck();
                    st.Stop();
                    GlobalData.testingDataDUT.SYSTEMLOG += string.Format("\r\nTổng thời gian test là: {0} giây\r\n", st.ElapsedMilliseconds / 1000);
                }));
                t.IsBackground = true;
                t.Start();
                
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e) {
            this.txtMAC.Focus();
        }
    }

}
