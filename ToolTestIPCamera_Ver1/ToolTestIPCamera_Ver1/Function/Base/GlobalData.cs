using System;
using System.Collections.Generic;
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

        public static IProtocol camera = null;

    }
}
