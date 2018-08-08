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

        }


        private void TextBox_TextChanged(object sender, TextChangedEventArgs e) {

        }
    }
}
