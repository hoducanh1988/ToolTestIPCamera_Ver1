using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolTestIPCamera_Ver1.Function
{
    public class GlobalData
    {
        public static defaultsetting initSetting = new defaultsetting();
        public static mainwindowinfo mainWindowInfo = new mainwindowinfo();
        public static testinginfo testingDataDUT = new testinginfo();
        public static manualtestinfo manualData = new manualtestinfo();

        public static IProtocol camera = null;

        public static ObservableCollection<logfileinfo> datagridlogtest = new ObservableCollection<logfileinfo>();
        public static ObservableCollection<logfileinfo> datagridlogdetail = new ObservableCollection<logfileinfo>();
        public static ObservableCollection<logfileinfo> datagridloguart = new ObservableCollection<logfileinfo>();

    }
}
