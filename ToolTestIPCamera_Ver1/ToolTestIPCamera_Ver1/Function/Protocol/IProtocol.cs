using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolTestIPCamera_Ver1.Function {
    public interface IProtocol {
        //Serial
        bool Open(out string _message);
        bool Close();
        bool Write(string _cmd);
        bool WriteLine(string _cmd);
        bool WriteLineAndWaitComplete(string _cmd, ref string msg);
        string Read();

        //Telnet
        bool Connection(ref string _message);
        bool LoginToCamera(ref string _message);
        bool sendListCommand(params string[] _listcmd);
        bool sendListCommand(IEnumerable<string> _listcmd);
        bool sendListCommand(IEnumerable<string> _listcmd, out string msg);
        string Read0();
    }
}
