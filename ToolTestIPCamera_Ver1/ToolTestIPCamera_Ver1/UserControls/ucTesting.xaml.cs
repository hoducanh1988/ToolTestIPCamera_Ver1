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
    /// Interaction logic for ucTesting.xaml
    /// </summary>
    public partial class ucTesting : UserControl {
        public ucTesting() {
            InitializeComponent();
            this.DataContext = GlobalData.testingDataDUT;
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e) {
            if (e.Key == Key.Enter) {
                //Check MAC Address is valid or not
                
                //Test Function
                switch (GlobalData.initSetting.station) {
                    case "PCBA-LAYER2": {
                            bool ret = Function.Excute.ExcuteTestLayer2.Excute();
                            break;
                        }
                    case "PCBA-LAYER3": {
                            bool ret = Function.Excute.ExcuteTestLayer3.Excute();
                            break;
                        }
                    case "SAU-DONG-VO": {
                            bool ret = Function.Excute.ExcuteTestProduct.Excute();
                            break;
                        }
                }
            }
        }
    }
}
