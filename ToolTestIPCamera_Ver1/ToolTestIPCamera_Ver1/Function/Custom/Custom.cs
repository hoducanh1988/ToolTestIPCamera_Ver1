using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolTestIPCamera_Ver1.Function {

    public class mainwindowinfo : INotifyPropertyChanged {

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName = null) {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }


        string _wtitle = "";
        public string windowtitle {
            get { return _wtitle; }
            set {
                _wtitle = value;
                OnPropertyChanged(nameof(windowtitle));
            }
        }

        public mainwindowinfo() {
            windowtitle = string.Format("TOOL TEST IP CAMERA / STATION : {0}", GlobalData.initSetting.station);
        }
    }

    public class defaultsetting : INotifyPropertyChanged {

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName = null) {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public defaultsetting() {

        }

        //CAI DAT HE THONG
        #region Cai dat he thong

        public string station {
            get { return Properties.Settings.Default.Station; }
            set {
                Properties.Settings.Default.Station = value;
                GlobalData.mainWindowInfo.windowtitle = string.Format("TOOL TEST IP CAMERA / STATION : {0}", value);
                GlobalData.testingDataDUT.station = value;
                OnPropertyChanged(nameof(station));
            }
        }
        public int maclen {
            get { return Properties.Settings.Default.MacLen; }
            set {
                Properties.Settings.Default.MacLen = value;
                OnPropertyChanged(nameof(maclen));
            }
        }
        public string macsixdigit {
            get { return Properties.Settings.Default.MacSixDigits; }
            set {
                Properties.Settings.Default.MacSixDigits = value;
                OnPropertyChanged(nameof(macsixdigit));
            }
        }
        public string linuxpath {
            get { return Properties.Settings.Default.LinuxPath; }
            set {
                Properties.Settings.Default.LinuxPath = value;
                OnPropertyChanged(nameof(linuxpath));
            }
        }
        public string mcupath {
            get { return Properties.Settings.Default.McuPath; }
            set {
                Properties.Settings.Default.McuPath = value;
                OnPropertyChanged(nameof(mcupath));
            }
        }
        public string ldcpath {
            get { return Properties.Settings.Default.LdcPath; }
            set {
                Properties.Settings.Default.LdcPath = value;
                OnPropertyChanged(nameof(ldcpath));
            }
        }
        public string dutip {
            get { return Properties.Settings.Default.DutIP; }
            set {
                Properties.Settings.Default.DutIP = value;
                OnPropertyChanged(nameof(dutip));
            }
        }
        public string dutuser {
            get { return Properties.Settings.Default.DutUser; }
            set {
                Properties.Settings.Default.DutUser = value;
                OnPropertyChanged(nameof(dutuser));
            }
        }
        public string dutpass {
            get { return Properties.Settings.Default.DutPass; }
            set {
                Properties.Settings.Default.DutPass = value;
                OnPropertyChanged(nameof(dutpass));
            }
        }
        public string routerip {
            get { return Properties.Settings.Default.RouterIP; }
            set {
                Properties.Settings.Default.RouterIP = value;
                OnPropertyChanged(nameof(routerip));
            }
        }
        public string routerssid {
            get { return Properties.Settings.Default.RouterSSID; }
            set {
                Properties.Settings.Default.RouterSSID = value;
                OnPropertyChanged(nameof(routerssid));
            }
        }

        #endregion

        //CAI DAT GIA TRI PHAN DINH
        #region Cai dat gia tri phan dinh

        public string fwversion {
            get { return Properties.Settings.Default.FwVersion; }
            set {
                Properties.Settings.Default.FwVersion = value;
                OnPropertyChanged(nameof(fwversion));
            }
        }
        public string adcvalue {
            get { return Properties.Settings.Default.AdcValue; }
            set {
                Properties.Settings.Default.AdcValue = value;
                OnPropertyChanged(nameof(adcvalue));
            }
        }
        #endregion

        //CAU HINH BAI TEST
        #region Cau hinh bai test

        public bool writemacoption {
            get { return Properties.Settings.Default.wmOption; }
            set {
                Properties.Settings.Default.wmOption = value;
                OnPropertyChanged(nameof(writemacoption));
            }
        }
        public bool uploadfirmwareoption {
            get { return Properties.Settings.Default.ufOption; }
            set {
                Properties.Settings.Default.ufOption = value;
                OnPropertyChanged(nameof(uploadfirmwareoption));
            }
        }
        public bool checkwifioption {
            get { return Properties.Settings.Default.cwOption; }
            set {
                Properties.Settings.Default.cwOption = value;
                OnPropertyChanged(nameof(checkwifioption));
            }
        }
        public bool checkfirmwareversionoption {
            get { return Properties.Settings.Default.cfvOption; }
            set {
                Properties.Settings.Default.cfvOption = value;
                OnPropertyChanged(nameof(checkfirmwareversionoption));
            }
        }
        public bool checklanoption {
            get { return Properties.Settings.Default.clOption; }
            set {
                Properties.Settings.Default.clOption = value;
                OnPropertyChanged(nameof(checklanoption));
            }
        }
        public bool checksdcardoption {
            get { return Properties.Settings.Default.cscOption; }
            set {
                Properties.Settings.Default.cscOption = value;
                OnPropertyChanged(nameof(checksdcardoption));
            }
        }
        public bool checkusboption {
            get { return Properties.Settings.Default.cuOption; }
            set {
                Properties.Settings.Default.cuOption = value;
                OnPropertyChanged(nameof(checkusboption));
            }
        }
        public bool checkrgbledoption {
            get { return Properties.Settings.Default.crlOption; }
            set {
                Properties.Settings.Default.crlOption = value;
                OnPropertyChanged(nameof(checkrgbledoption));
            }
        }
        public bool checklightsensoroption {
            get { return Properties.Settings.Default.clsOption; }
            set {
                Properties.Settings.Default.clsOption = value;
                OnPropertyChanged(nameof(checklightsensoroption));
            }
        }
        public bool checkspeakermicoption {
            get { return Properties.Settings.Default.csmOption; }
            set {
                Properties.Settings.Default.csmOption = value;
                OnPropertyChanged(nameof(checkspeakermicoption));
            }
        }
        public bool checkimagesensoroption {
            get { return Properties.Settings.Default.cisOption; }
            set {
                Properties.Settings.Default.cisOption = value;
                OnPropertyChanged(nameof(checkimagesensoroption));
            }
        }
        public bool checkbuttonoption {
            get { return Properties.Settings.Default.cbOption; }
            set {
                Properties.Settings.Default.cbOption = value;
                OnPropertyChanged(nameof(checkbuttonoption));
            }
        }
        #endregion

        //SAVE SETTING
        public bool SaveSetting() {
            try {
                Properties.Settings.Default.Save();
                return true;
            }
            catch {
                return false;
            }
        }
    }

    public class testinginfo : INotifyPropertyChanged {

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName = null) {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public testinginfo() {
            station = GlobalData.initSetting.station;
        }

        string _station;
        public string station {
            get { return _station; }
            set {
                _station = value;
                OnPropertyChanged(nameof(station));
            }
        }
    }

}
