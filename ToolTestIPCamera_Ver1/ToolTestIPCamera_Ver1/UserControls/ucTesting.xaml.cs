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
                _scrollViewer2.ScrollToEnd();
            });
            timer.Start();
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e) {
            if (e.Key == Key.Enter) {
                //Check MAC Address is valid or not

                Thread t = new Thread(new ThreadStart(() => {
                   
                    //Test Function
                    bool ret = false;
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
                    GlobalData.testingDataDUT.TOTALRESULT = ret == true ? Parameters.testStatus.PASS.ToString() : Parameters.testStatus.FAIL.ToString();
                }));
                t.IsBackground = true;
                t.Start();
                
            }
        }
    }
}
