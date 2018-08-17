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
        public string usbdebug1 {
            get { return Properties.Settings.Default.Usbdebug1; }
            set {
                Properties.Settings.Default.Usbdebug1 = value;
                OnPropertyChanged(nameof(usbdebug1));
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
                GlobalData.testingDataDUT.WRITEMACRESULT = value == true ? Parameters.testStatus.NONE.ToString() : Parameters.testStatus.X.ToString();
                //GlobalData.testingDataDUT.writemacoption = value;
                OnPropertyChanged(nameof(writemacoption));
            }
        }
        public bool uploadfirmwareoption {
            get { return Properties.Settings.Default.ufOption; }
            set {
                Properties.Settings.Default.ufOption = value;
                GlobalData.testingDataDUT.UPLOADFWRESULT = value == true ? Parameters.testStatus.NONE.ToString() : Parameters.testStatus.X.ToString();
                //GlobalData.testingDataDUT.uploadfirmwareoption = value;
                OnPropertyChanged(nameof(uploadfirmwareoption));
            }
        }
        public bool checkwifioption {
            get { return Properties.Settings.Default.cwOption; }
            set {
                Properties.Settings.Default.cwOption = value;
                GlobalData.testingDataDUT.WIFIRESULT = value == true ? Parameters.testStatus.NONE.ToString() : Parameters.testStatus.X.ToString();
                //GlobalData.testingDataDUT.checkwifioption = value;
                OnPropertyChanged(nameof(checkwifioption));
            }
        }
        public bool checkfirmwareversionoption {
            get { return Properties.Settings.Default.cfvOption; }
            set {
                Properties.Settings.Default.cfvOption = value;
                GlobalData.testingDataDUT.FWVERSIONRESULT = value == true ? Parameters.testStatus.NONE.ToString() : Parameters.testStatus.X.ToString();
                //GlobalData.testingDataDUT.checkfirmwareversionoption = value;
                OnPropertyChanged(nameof(checkfirmwareversionoption));
            }
        }
        public bool checkmacoption {
            get { return Properties.Settings.Default.cmOption; }
            set {
                Properties.Settings.Default.cmOption = value;
                GlobalData.testingDataDUT.MACRESULT = value == true ? Parameters.testStatus.NONE.ToString() : Parameters.testStatus.X.ToString();
                OnPropertyChanged(nameof(checkmacoption));
            }
        }

        public bool checklanoption {
            get { return Properties.Settings.Default.clOption; }
            set {
                Properties.Settings.Default.clOption = value;
                GlobalData.testingDataDUT.LANRESULT = value == true ? Parameters.testStatus.NONE.ToString() : Parameters.testStatus.X.ToString();
                //GlobalData.testingDataDUT.checklanoption = value;
                OnPropertyChanged(nameof(checklanoption));
            }
        }
        public bool checksdcardoption {
            get { return Properties.Settings.Default.cscOption; }
            set {
                Properties.Settings.Default.cscOption = value;
                GlobalData.testingDataDUT.SDCARDRESULT = value == true ? Parameters.testStatus.NONE.ToString() : Parameters.testStatus.X.ToString();
                //GlobalData.testingDataDUT.checksdcardoption = value;
                OnPropertyChanged(nameof(checksdcardoption));
            }
        }
        public bool checkusboption {
            get { return Properties.Settings.Default.cuOption; }
            set {
                Properties.Settings.Default.cuOption = value;
                GlobalData.testingDataDUT.USBRESULT = value == true ? Parameters.testStatus.NONE.ToString() : Parameters.testStatus.X.ToString();
                //GlobalData.testingDataDUT.checkusboption = value;
                OnPropertyChanged(nameof(checkusboption));
            }
        }
        public bool checkrgbledoption {
            get { return Properties.Settings.Default.crlOption; }
            set {
                Properties.Settings.Default.crlOption = value;
                GlobalData.testingDataDUT.RGBLEDRESULT = value == true ? Parameters.testStatus.NONE.ToString() : Parameters.testStatus.X.ToString();
                //GlobalData.testingDataDUT.checkrgbledoption = value;
                OnPropertyChanged(nameof(checkrgbledoption));
            }
        }
        public bool checklightsensoroption {
            get { return Properties.Settings.Default.clsOption; }
            set {
                Properties.Settings.Default.clsOption = value;
                GlobalData.testingDataDUT.LIGHTSENSORRESULT = value == true ? Parameters.testStatus.NONE.ToString() : Parameters.testStatus.X.ToString();
                //GlobalData.testingDataDUT.checklightsensoroption = value;
                OnPropertyChanged(nameof(checklightsensoroption));
            }
        }
        public bool checkspeakermicoption {
            get { return Properties.Settings.Default.csmOption; }
            set {
                Properties.Settings.Default.csmOption = value;
                GlobalData.testingDataDUT.SPEAKERMICRESULT = value == true ? Parameters.testStatus.NONE.ToString() : Parameters.testStatus.X.ToString();
                //GlobalData.testingDataDUT.checkspeakermicoption = value;
                OnPropertyChanged(nameof(checkspeakermicoption));
            }
        }
        public bool checkimagesensoroption {
            get { return Properties.Settings.Default.cisOption; }
            set {
                Properties.Settings.Default.cisOption = value;
                GlobalData.testingDataDUT.IMAGESENSORRESULT = value == true ? Parameters.testStatus.NONE.ToString() : Parameters.testStatus.X.ToString();
                
                //GlobalData.testingDataDUT.checkimagesensoroption = value;
                OnPropertyChanged(nameof(checkimagesensoroption));
            }
        }
        public bool checkbuttonoption {
            get { return Properties.Settings.Default.cbOption; }
            set {
                Properties.Settings.Default.cbOption = value;
                GlobalData.testingDataDUT.BUTTONRESULT = value == true ? Parameters.testStatus.NONE.ToString() : Parameters.testStatus.X.ToString();
                //GlobalData.testingDataDUT.checkbuttonoption = value;
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

        //INotifyPropertyChanged implement
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName = null) {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        //Constructor
        public testinginfo() {
            station = GlobalData.initSetting.station;
            
            Initialization();
        }

        //station
        string _station;
        public string station {
            get { return _station; }
            set {
                _station = value;
                OnPropertyChanged(nameof(station));
            }
        }

        //mac address
        string _macaddress;
        public string MACADDRESS {
            get { return _macaddress; }
            set {
                _macaddress = value;
                OnPropertyChanged(nameof(MACADDRESS));
            }
        }
        bool _enabletextbox;
        public bool ENABLETEXTBOX {
            get { return _enabletextbox; }
            set {
                _enabletextbox = value;
                OnPropertyChanged(nameof(ENABLETEXTBOX));
            }
        }

        string _oldmac;
        public string OLDMAC {
            get { return _oldmac; }
            set {
                _oldmac = value;
                OnPropertyChanged(nameof(OLDMAC));
            }
        }

        //result
        #region RESULT

        string _writemac;
        public string WRITEMACRESULT {
            get { return _writemac; }
            set {
                _writemac = value;
                OnPropertyChanged(nameof(WRITEMACRESULT));
            }
        }
        string _uploadfirmware;
        public string UPLOADFWRESULT {
            get { return _uploadfirmware; }
            set {
                _uploadfirmware = value;
                OnPropertyChanged(nameof(UPLOADFWRESULT));
            }
        }
        string _wifi;
        public string WIFIRESULT {
            get { return _wifi; }
            set {
                _wifi = value;
                OnPropertyChanged(nameof(WIFIRESULT));
            }
        }
        string _firmwareversion;
        public string FWVERSIONRESULT {
            get { return _firmwareversion; }
            set {
                _firmwareversion = value;
                OnPropertyChanged(nameof(FWVERSIONRESULT));
            }
        }
        string _macresult;
        public string MACRESULT {
            get { return _macresult; }
            set {
                _macresult = value;
                OnPropertyChanged(nameof(MACRESULT));
            }
        }
        string _lan;
        public string LANRESULT {
            get { return _lan; }
            set {
                _lan = value;
                OnPropertyChanged(nameof(LANRESULT));
            }
        }
        string _sdcard;
        public string SDCARDRESULT {
            get { return _sdcard; }
            set {
                _sdcard = value;
                OnPropertyChanged(nameof(SDCARDRESULT));
            }
        }
        string _usb;
        public string USBRESULT {
            get { return _usb; }
            set {
                _usb = value;
                OnPropertyChanged(nameof(USBRESULT));
            }
        }
        string _rgbled;
        public string RGBLEDRESULT {
            get { return _rgbled; }
            set {
                _rgbled = value;
                OnPropertyChanged(nameof(RGBLEDRESULT));
            }
        }
        string _lightsensor;
        public string LIGHTSENSORRESULT {
            get { return _lightsensor; }
            set {
                _lightsensor = value;
                OnPropertyChanged(nameof(LIGHTSENSORRESULT));
            }
        }
        string _speakermic;
        public string SPEAKERMICRESULT {
            get { return _speakermic; }
            set {
                _speakermic = value;
                OnPropertyChanged(nameof(SPEAKERMICRESULT));
            }
        }
        string _imagesensor;
        public string IMAGESENSORRESULT {
            get { return _imagesensor; }
            set {
                _imagesensor = value;
                OnPropertyChanged(nameof(IMAGESENSORRESULT));
            }
        }
        string _button;
        public string BUTTONRESULT {
            get { return _button; }
            set {
                _button = value;
                OnPropertyChanged(nameof(BUTTONRESULT));
            }
        }
        string _totalresult;
        public string TOTALRESULT {
            get { return _totalresult; }
            set {
                _totalresult = value;
                OnPropertyChanged(nameof(TOTALRESULT));
            }
        }

        #endregion

        //log
        #region LOG

        string _systemlog;
        public string SYSTEMLOG {
            get { return _systemlog; }
            set {
                _systemlog = value;
                OnPropertyChanged(nameof(SYSTEMLOG));
            }
        }
        string _errorcode;
        public string ERRORCODE {
            get { return _errorcode; }
            set {
                _errorcode = value;
                OnPropertyChanged(nameof(ERRORCODE));
            }
        }
        string _uartlog;
        public string UARTLOG {
            get { return _uartlog; }
            set {
                _uartlog = value;
                OnPropertyChanged(nameof(UARTLOG));
            }
        }
        string _cameralog;
        public string CAMERALOG {
            get { return _cameralog; }
            set {
                _cameralog = value;
                OnPropertyChanged(nameof(CAMERALOG));
            }
        }
        string _totaltime;
        public string TOTALTIME {
            get { return _totaltime; }
            set {
                _totaltime = value;
                OnPropertyChanged(nameof(TOTALTIME));
            }
        }

        #endregion

        public void Initialization() {
            this.TOTALTIME = "0";
            this.MACADDRESS = "";
            this.OLDMAC = "";
            this.ENABLETEXTBOX = true;

            this.WRITEMACRESULT =  GlobalData.initSetting.writemacoption == true ? Parameters.testStatus.NONE.ToString() : Parameters.testStatus.X.ToString();
            this.UPLOADFWRESULT = GlobalData.initSetting.uploadfirmwareoption == true ? Parameters.testStatus.NONE.ToString() : Parameters.testStatus.X.ToString();
            this.WIFIRESULT = GlobalData.initSetting.checkwifioption == true ? Parameters.testStatus.NONE.ToString() : Parameters.testStatus.X.ToString();
            this.FWVERSIONRESULT = GlobalData.initSetting.checkfirmwareversionoption == true ? Parameters.testStatus.NONE.ToString() : Parameters.testStatus.X.ToString();
            this.MACRESULT = GlobalData.initSetting.checkmacoption == true ? Parameters.testStatus.NONE.ToString() : Parameters.testStatus.X.ToString();
            this.LANRESULT = GlobalData.initSetting.checklanoption == true ? Parameters.testStatus.NONE.ToString() : Parameters.testStatus.X.ToString();
            this.SDCARDRESULT = GlobalData.initSetting.checksdcardoption == true ? Parameters.testStatus.NONE.ToString() : Parameters.testStatus.X.ToString();
            this.USBRESULT = GlobalData.initSetting.checkusboption == true ? Parameters.testStatus.NONE.ToString() : Parameters.testStatus.X.ToString();
            this.RGBLEDRESULT = GlobalData.initSetting.checkrgbledoption == true ? Parameters.testStatus.NONE.ToString() : Parameters.testStatus.X.ToString();
            this.LIGHTSENSORRESULT = GlobalData.initSetting.checklightsensoroption == true ? Parameters.testStatus.NONE.ToString() : Parameters.testStatus.X.ToString();
            this.SPEAKERMICRESULT = GlobalData.initSetting.checkspeakermicoption == true ? Parameters.testStatus.NONE.ToString() : Parameters.testStatus.X.ToString();
            this.IMAGESENSORRESULT = GlobalData.initSetting.checkimagesensoroption == true ? Parameters.testStatus.NONE.ToString() : Parameters.testStatus.X.ToString();
            this.BUTTONRESULT = GlobalData.initSetting.checkbuttonoption == true ? Parameters.testStatus.NONE.ToString() : Parameters.testStatus.X.ToString();
            this.TOTALRESULT = Parameters.testStatus.NONE.ToString();

            this.SYSTEMLOG = "";
            this.UARTLOG = "";
            this.CAMERALOG = "";
            this.ERRORCODE = "";
        }


        public void InitControlForChecking() {
            this.TOTALTIME = "0";
            //this.MACADDRESS = "";
            this.OLDMAC = "";
            this.ENABLETEXTBOX = false;

            this.WRITEMACRESULT = GlobalData.initSetting.writemacoption == true ? Parameters.testStatus.Wait.ToString() : Parameters.testStatus.X.ToString();
            this.UPLOADFWRESULT = GlobalData.initSetting.uploadfirmwareoption == true ? Parameters.testStatus.Wait.ToString() : Parameters.testStatus.X.ToString();
            this.WIFIRESULT = GlobalData.initSetting.checkwifioption == true ? Parameters.testStatus.Wait.ToString() : Parameters.testStatus.X.ToString();
            this.FWVERSIONRESULT = GlobalData.initSetting.checkfirmwareversionoption == true ? Parameters.testStatus.Wait.ToString() : Parameters.testStatus.X.ToString();
            this.MACRESULT = GlobalData.initSetting.checkmacoption == true ? Parameters.testStatus.Wait.ToString() : Parameters.testStatus.X.ToString();
            this.LANRESULT = GlobalData.initSetting.checklanoption == true ? Parameters.testStatus.Wait.ToString() : Parameters.testStatus.X.ToString();
            this.SDCARDRESULT = GlobalData.initSetting.checksdcardoption == true ? Parameters.testStatus.Wait.ToString() : Parameters.testStatus.X.ToString();
            this.USBRESULT = GlobalData.initSetting.checkusboption == true ? Parameters.testStatus.Wait.ToString() : Parameters.testStatus.X.ToString();
            this.RGBLEDRESULT = GlobalData.initSetting.checkrgbledoption == true ? Parameters.testStatus.Wait.ToString() : Parameters.testStatus.X.ToString();
            this.LIGHTSENSORRESULT = GlobalData.initSetting.checklightsensoroption == true ? Parameters.testStatus.Wait.ToString() : Parameters.testStatus.X.ToString();
            this.SPEAKERMICRESULT = GlobalData.initSetting.checkspeakermicoption == true ? Parameters.testStatus.Wait.ToString() : Parameters.testStatus.X.ToString();
            this.IMAGESENSORRESULT = GlobalData.initSetting.checkimagesensoroption == true ? Parameters.testStatus.Wait.ToString() : Parameters.testStatus.X.ToString();
            this.BUTTONRESULT = GlobalData.initSetting.checkbuttonoption == true ? Parameters.testStatus.Wait.ToString() : Parameters.testStatus.X.ToString();
            this.TOTALRESULT = Parameters.testStatus.Wait.ToString();

            this.UARTLOG = "";
            this.CAMERALOG = "";
            this.ERRORCODE = "";
        }

        public void FinishCheck() {

            if (this.WRITEMACRESULT == Parameters.testStatus.Wait.ToString()) {
                this.WRITEMACRESULT = Parameters.testStatus.NONE.ToString();
            }

            if (this.UPLOADFWRESULT == Parameters.testStatus.Wait.ToString()) {
                this.UPLOADFWRESULT = Parameters.testStatus.NONE.ToString();
            }

            if (this.WIFIRESULT == Parameters.testStatus.Wait.ToString()) {
                this.WIFIRESULT = Parameters.testStatus.NONE.ToString();
            }

            if (this.FWVERSIONRESULT == Parameters.testStatus.Wait.ToString()) {
                this.FWVERSIONRESULT = Parameters.testStatus.NONE.ToString();
            }

            if (this.MACRESULT == Parameters.testStatus.Wait.ToString()) {
                this.MACRESULT = Parameters.testStatus.NONE.ToString();
            }

            if (this.LANRESULT == Parameters.testStatus.Wait.ToString()) {
                this.LANRESULT = Parameters.testStatus.NONE.ToString();
            }

            if (this.SDCARDRESULT == Parameters.testStatus.Wait.ToString()) {
                this.SDCARDRESULT = Parameters.testStatus.NONE.ToString();
            }

            if (this.USBRESULT == Parameters.testStatus.Wait.ToString()) {
                this.USBRESULT = Parameters.testStatus.NONE.ToString();
            }

            if (this.RGBLEDRESULT == Parameters.testStatus.Wait.ToString()) {
                this.RGBLEDRESULT = Parameters.testStatus.NONE.ToString();
            }

            if (this.LIGHTSENSORRESULT == Parameters.testStatus.Wait.ToString()) {
                this.LIGHTSENSORRESULT = Parameters.testStatus.NONE.ToString();
            }

            if (this.SPEAKERMICRESULT == Parameters.testStatus.Wait.ToString()) {
                this.SPEAKERMICRESULT = Parameters.testStatus.NONE.ToString();
            }

            if (this.IMAGESENSORRESULT == Parameters.testStatus.Wait.ToString()) {
                this.IMAGESENSORRESULT = Parameters.testStatus.NONE.ToString();
            }

            if (this.BUTTONRESULT == Parameters.testStatus.Wait.ToString()) {
                this.BUTTONRESULT = Parameters.testStatus.NONE.ToString();
            }
        }

    }

    public class manualtestinfo : INotifyPropertyChanged {
        //INotifyPropertyChanged implement
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName = null) {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        string _dutip;
        public string dutip {
            get { return _dutip; }
            set {
                _dutip = value;
                OnPropertyChanged(nameof(dutip));
            }
        }
        string _dutuser;
        public string dutuser {
            get { return _dutuser; }
            set {
                _dutuser = value;
                OnPropertyChanged(nameof(dutuser));
            }
        }
        string _usbdebug1;
        public string usbdebug1 {
            get { return _usbdebug1; }
            set {
                _usbdebug1 = value;
                OnPropertyChanged(nameof(usbdebug1));
            }
        }
        string _station;
        public string station {
            get { return _station; }
            set {
                _station = value;
                OnPropertyChanged(nameof(station));
            }
        }

        string _fw_manuallog;
        public string fw_manuallog {
            get { return _fw_manuallog; }
            set {
                _fw_manuallog = value;
                OnPropertyChanged(nameof(fw_manuallog));
            }
        }
        string _mac_manuallog;
        public string mac_manuallog {
            get { return _mac_manuallog; }
            set {
                _mac_manuallog = value;
                OnPropertyChanged(nameof(mac_manuallog));
            }
        }
        string _wifi_manuallog;
        public string wifi_manuallog {
            get { return _wifi_manuallog; }
            set {
                _wifi_manuallog = value;
                OnPropertyChanged(nameof(wifi_manuallog));
            }
        }
        string _sdcard_manuallog;
        public string sdcard_manuallog {
            get { return _sdcard_manuallog; }
            set {
                _sdcard_manuallog = value;
                OnPropertyChanged(nameof(sdcard_manuallog));
            }
        }
        string _lan_manuallog;
        public string lan_manuallog {
            get { return _lan_manuallog; }
            set {
                _lan_manuallog = value;
                OnPropertyChanged(nameof(lan_manuallog));
            }
        }
        string _audio_manuallog;
        public string audio_manuallog {
            get { return _audio_manuallog; }
            set {
                _audio_manuallog = value;
                OnPropertyChanged(nameof(audio_manuallog));
            }
        }
        string _rgbled_manuallog;
        public string rgbled_manuallog {
            get { return _rgbled_manuallog; }
            set {
                _rgbled_manuallog = value;
                OnPropertyChanged(nameof(rgbled_manuallog));
            }
        }
        string _button_manuallog;
        public string button_manuallog {
            get { return _button_manuallog; }
            set {
                _button_manuallog = value;
                OnPropertyChanged(nameof(button_manuallog));
            }
        }
        string _lightsensor_manuallog;
        public string lightsensor_manuallog {
            get { return _lightsensor_manuallog; }
            set {
                _lightsensor_manuallog = value;
                OnPropertyChanged(nameof(lightsensor_manuallog));
            }
        }
    }

    public class logfileinfo {
        public int ID { get; set; }
        public string FileName { get; set; }
        public string MemorySize { get; set; }
        public string DateCreated { get; set; }
        public string DateModified { get; set; }
    }
}
