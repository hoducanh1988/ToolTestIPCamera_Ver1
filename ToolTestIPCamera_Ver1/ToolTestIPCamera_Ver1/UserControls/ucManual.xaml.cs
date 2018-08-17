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
    /// Interaction logic for ucManual.xaml
    /// </summary>
    public partial class ucManual : UserControl {
        public ucManual() {
            InitializeComponent();
            GlobalData.manualData.dutip = GlobalData.initSetting.dutip;
            GlobalData.manualData.dutuser = GlobalData.initSetting.dutuser;
            GlobalData.manualData.station = GlobalData.initSetting.station;
            GlobalData.manualData.usbdebug1 = GlobalData.initSetting.usbdebug1;

            this.DataContext = GlobalData.manualData;
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e) {

        }

        private void Button_Click(object sender, RoutedEventArgs e) {

        }
    }
}
