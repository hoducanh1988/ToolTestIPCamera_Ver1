using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolTestIPCamera_Ver1.Function
{
    public class Parameters
    {

        public static List<string> ListStation = new List<string>() { "PCBA-LAYER2", "PCBA-LAYER3", "SAU-DONG-VO" };
        public enum testStatus { NONE = 0, Wait = 1, PASS = 2, FAIL = 3 };
    }
}
