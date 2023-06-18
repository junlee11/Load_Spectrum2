using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace Load_Spectrum2.NewModel
{
    //set3 : Local To EQP 확산
    //set4 : EQP To Local 확산
    public class Model : INotifyPropertyChanged
    {
        private string _strdrive;
        public string strdrive
        {
            get { return _strdrive; }
            set { _strdrive = value; OnPropertyChanged("strdrive"); }
        }

        private string _strLine_Set3;
        public string strLine_Set3
        {
            get { return _strLine_Set3; }
            set { _strLine_Set3 = value; OnPropertyChanged("strLine_Set3"); }
        }

        private string _strLine_Set4;
        public string strLine_Set4
        {
            get { return _strLine_Set4; }
            set { _strLine_Set4 = value; OnPropertyChanged("strLine_Set4"); }
        }

        private string _strLine_Set5;
        public string strLine_Set5
        {
            get { return _strLine_Set5; }
            set { _strLine_Set5 = value; OnPropertyChanged("strLine_Set5"); }
        }

        private string _strEQPTYPE_Set3;
        public string strEQPTYPE_Set3
        {
            get { return _strEQPTYPE_Set3; }
            set { _strEQPTYPE_Set3 = value; OnPropertyChanged("strEQPTYPE_Set3"); }
        }

        private string _strEQPTYPE_Set4;
        public string strEQPTYPE_Set4
        {
            get { return _strEQPTYPE_Set4; }
            set { _strEQPTYPE_Set4 = value; OnPropertyChanged("strEQPTYPE_Set4"); }
        }

        private string _strEQPTYPE_Set5;
        public string strEQPTYPE_Set5
        {
            get { return _strEQPTYPE_Set5; }
            set { _strEQPTYPE_Set5 = value; OnPropertyChanged("strEQPTYPE_Set5"); }
        }

        private string _strEQPMODEL_Set3;
        public string strEQPMODEL_Set3
        {
            get { return _strEQPMODEL_Set3; }
            set { _strEQPMODEL_Set3 = value; OnPropertyChanged("strEQPMODEL_Set3"); }
        }

        private string _strEQPMODEL_Set4;
        public string strEQPMODEL_Set4
        {
            get { return _strEQPMODEL_Set4; }
            set { _strEQPMODEL_Set4 = value; OnPropertyChanged("strEQPMODEL_Set4"); }
        }

        private string _strEQPMODEL_Set5;
        public string strEQPMODEL_Set5
        {
            get { return _strEQPMODEL_Set5; }
            set { _strEQPMODEL_Set5 = value; OnPropertyChanged("strEQPMODEL_Set5"); }
        }

        private string _strZONE_Set3;
        public string strZONE_Set3
        {
            get { return _strZONE_Set3; }
            set { _strZONE_Set3 = value; OnPropertyChanged("strZONE_Set3"); }
        }

        private string _strZONE_Set4;
        public string strZONE_Set4
        {
            get { return _strZONE_Set4; }
            set { _strZONE_Set4 = value; OnPropertyChanged("strZONE_Set4"); }
        }

        private string _strZONE_Set5;
        public string strZONE_Set5
        {
            get { return _strZONE_Set5; }
            set { _strZONE_Set5 = value; OnPropertyChanged("strZONE_Set5"); }
        }

        private string _strEQPID_Set3;
        public string strEQPID_Set3
        {
            get { return _strEQPID_Set3; }
            set { _strEQPID_Set3 = value; OnPropertyChanged("strEQPID_Set3"); }
        }

        private string _strEQPID_Set4;
        public string strEQPID_Set4
        {
            get { return _strEQPID_Set4; }
            set { _strEQPID_Set4 = value; OnPropertyChanged("strEQPID_Set4"); }
        }

        private string _strEQPID_Set5;
        public string strEQPID_Set5
        {
            get { return _strEQPID_Set5; }
            set { _strEQPID_Set5 = value; OnPropertyChanged("strEQPID_Set5"); }
        }

        private string _strchkEQPID_Set3;
        public string strchkEQPID_Set3
        {
            get { return _strchkEQPID_Set3; }
            set { _strchkEQPID_Set3 = value; OnPropertyChanged("strchkEQPID_Set3"); }
        }
        private string _strchkEQPID_Set4;
        public string strchkEQPID_Set4
        {
            get { return _strchkEQPID_Set4; }
            set { _strchkEQPID_Set4 = value; OnPropertyChanged("strchkEQPID_Set4"); }
        }

        private string _strchkEQPID_Set5;
        public string strchkEQPID_Set5
        {
            get { return _strchkEQPID_Set5; }
            set { _strchkEQPID_Set5 = value; OnPropertyChanged("strchkEQPID_Set5"); }
        }

        private string _strRefresh_Set3;
        public string strRefresh_Set3
        {
            get { return _strRefresh_Set3; }
            set { _strRefresh_Set3 = value; OnPropertyChanged("strRefresh_Set3"); }
        }
        private string _strRefresh_Set4;
        public string strRefresh_Set4
        {
            get { return _strRefresh_Set4; }
            set { _strRefresh_Set4 = value; OnPropertyChanged("strRefresh_Set4"); }
        }

        private string _strRefresh_Set5;
        public string strRefresh_Set5
        {
            get { return _strRefresh_Set5; }
            set { _strRefresh_Set5 = value; OnPropertyChanged("strRefresh_Set5"); }
        }

        private string _strPassFail_Set3;
        public string strPassFail_Set3
        {
            get { return _strPassFail_Set3; }
            set { _strPassFail_Set3 = value; OnPropertyChanged("strPassFail_Set3"); }
        }
        private string _strPassFail_Set4;
        public string strPassFail_Set4
        {
            get { return _strPassFail_Set4; }
            set { _strPassFail_Set4 = value; OnPropertyChanged("strPassFail_Set4"); }
        }
        private string _strPassFail_Set5;
        public string strPassFail_Set5
        {
            get { return _strPassFail_Set5; }
            set { _strPassFail_Set5 = value; OnPropertyChanged("strPassFail_Set5"); }
        }

        private string _strStatus_Set3;
        public string strStatus_Set3
        {
            get { return _strStatus_Set3; }
            set { _strStatus_Set3 = value; OnPropertyChanged("strStatus_Set3"); }
        }
        private string _strStatus_Set4;
        public string strStatus_Set4
        {
            get { return _strStatus_Set4; }
            set { _strStatus_Set4 = value; OnPropertyChanged("strStatus_Set4"); }
        }
        private string _strStatus_Set5;
        public string strStatus_Set5
        {
            get { return _strStatus_Set5; }
            set { _strStatus_Set5 = value; OnPropertyChanged("strStatus_Set5"); }
        }

        private int _strPgbValue_Set3;
        public int strPgbValue_Set3
        {
            get { return _strPgbValue_Set3; }
            set { _strPgbValue_Set3 = value; OnPropertyChanged("strPgbValue_Set3"); }
        }
        private int _strPgbValue_Set4;
        public int strPgbValue_Set4
        {
            get { return _strPgbValue_Set4; }
            set { _strPgbValue_Set4 = value; OnPropertyChanged("strPgbValue_Set4"); }
        }
        private int _strPgbValue_Set5;
        public int strPgbValue_Set5
        {
            get { return _strPgbValue_Set5; }
            set { _strPgbValue_Set5 = value; OnPropertyChanged("strPgbValue_Set5"); }
        }
        private string _strPgbTxt_Set3;
        public string strPgbTxt_Set3
        {
            get { return _strPgbTxt_Set3; }
            set { _strPgbTxt_Set3 = value; OnPropertyChanged("strPgbTxt_Set3"); }
        }
        private string _strPgbTxt_Set4;
        public string strPgbTxt_Set4
        {
            get { return _strPgbTxt_Set4; }
            set { _strPgbTxt_Set4 = value; OnPropertyChanged("strPgbTxt_Set4"); }
        }
        private string _strPgbTxt_Set5;
        public string strPgbTxt_Set5
        {
            get { return _strPgbTxt_Set5; }
            set { _strPgbTxt_Set5 = value; OnPropertyChanged("strPgbTxt_Set5"); }
        }
        private string _strNum_Set4;
        public string strNum_Set4
        {
            get { return _strNum_Set4; }
            set { _strNum_Set4 = value; OnPropertyChanged("strNum_Set4"); }
        }
        private bool _strHit_Set4;
        public bool strHit_Set4
        {
            get { return _strHit_Set4; }
            set { _strHit_Set4 = value; OnPropertyChanged("strHit_Set4"); }
        }
        private Brush _strColor_Set4;
        public Brush strColor_Set4
        {
            get { return _strColor_Set4; }
            set { _strColor_Set4 = value; OnPropertyChanged("strColor_Set4"); }
        }

        //FTP Dialog
        private string _strEQPID_ftpdia;
        public string strEQPID_ftpdia
        {
            get { return _strEQPID_ftpdia; }
            set { _strEQPID_ftpdia = value; OnPropertyChanged("strEQPID_ftpdia"); }
        }

        private string _ftp_all_filename;
        public string ftp_all_filename
        {
            get { return _ftp_all_filename; }
            set { _ftp_all_filename = value; OnPropertyChanged("ftp_all_filename"); }
        }

        private DateTime _ftp_all_date;
        public DateTime ftp_all_date
        {
            get { return _ftp_all_date; }
            set { _ftp_all_date = value; OnPropertyChanged("ftp_all_date"); }
        }

        private string _ftp_all_extension;
        public string ftp_all_extension
        {
            get { return _ftp_all_extension; }
            set { _ftp_all_extension = value; OnPropertyChanged("ftp_all_extension"); }
        }

        private string _ftp_all_size;
        public string ftp_all_size
        {
            get { return _ftp_all_size; }
            set { _ftp_all_size = value; OnPropertyChanged("ftp_all_size"); }
        }

        private string _ftp_all_src;
        public string ftp_all_src
        {
            get { return _ftp_all_src; }
            set { _ftp_all_src = value; OnPropertyChanged("ftp_all_src"); }
        }


        //InfoEQP
        private string _strEQP_grid_LINE;
        public string strEQP_grid_LINE
        {
            get { return _strEQP_grid_LINE; }
            set { _strEQP_grid_LINE = value; OnPropertyChanged("strEQP_grid_LINE"); }
        }
        private string _strEQP_grid_EQPTYPE;
        public string strEQP_grid_EQPTYPE
        {
            get { return _strEQP_grid_EQPTYPE; }
            set { _strEQP_grid_EQPTYPE = value; OnPropertyChanged("strEQP_grid_EQPTYPE"); }
        }
        private string _strEQP_grid_EQPMODEL;
        public string strEQP_grid_EQPMODEL
        {
            get { return _strEQP_grid_EQPMODEL; }
            set { _strEQP_grid_EQPMODEL = value; OnPropertyChanged("strEQP_grid_EQPMODEL"); }
        }
        private string _strEQP_grid_ZONE;
        public string strEQP_grid_ZONE
        {
            get { return _strEQP_grid_ZONE; }
            set { _strEQP_grid_ZONE = value; OnPropertyChanged("strEQP_grid_ZONE"); }
        }
        private string _strEQP_grid_EQPID;
        public string strEQP_grid_EQPID
        {
            get { return _strEQP_grid_EQPID; }
            set { _strEQP_grid_EQPID = value; OnPropertyChanged("strEQP_grid_EQPID"); }
        }
        private string _strEQP_grid_IP;
        public string strEQP_grid_IP
        {
            get { return _strEQP_grid_IP; }
            set { _strEQP_grid_IP = value; OnPropertyChanged("strEQP_grid_IP"); }
        }


        //RNR Item, Layer
        private int _strRNR_Item;
        public int strRNR_Item
        {
            get { return _strRNR_Item; }
            set { _strRNR_Item = value; OnPropertyChanged("strRNR_Item"); }
        }
        private string _strRNR_Layer;
        public string strRNR_Layer
        {
            get { return _strRNR_Layer; }
            set { _strRNR_Layer = value; OnPropertyChanged("strRNR_Layer"); }
        }
        private bool _strRNR_Chk;
        public bool strRNR_Chk
        {
            get { return _strRNR_Chk; }
            set { _strRNR_Chk = value; OnPropertyChanged("strRNR_Chk"); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    //구버전 클래스로 이용한거
    #region 구버전
    public class EQP_Info_Line3 : INotifyPropertyChanged
    {
        public string _strLine_Set3;
        public string strLine_Set3
        {
            get { return _strLine_Set3; }
            set { _strLine_Set3 = value; RaisePropertyChangedEvent("strLine_Set3"); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        void RaisePropertyChangedEvent(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
    public class EQP_Info_Line4 : INotifyPropertyChanged
    {
        public string _strLine_Set4;
        public string strLine_Set4
        {
            get { return _strLine_Set4; }
            set { _strLine_Set4 = value; RaisePropertyChangedEvent("strLine_Set4"); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        void RaisePropertyChangedEvent(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }

    public class EQP_Info_EQPTYPE3
    {
        public string strEQPTYPE_Set3 { get; set; }
    }
    public class EQP_Info_EQPTYPE4
    {
        public string strEQPTYPE_Set4 { get; set; }
    }
    public class EQP_Info_EQPMODEL3
    {
        public string strEQPMODEL_Set3 { get; set; }
    }
    public class EQP_Info_EQPMODEL4
    {
        public string strEQPMODEL_Set4 { get; set; }
    }
    public class EQP_Info_ZONE3
    {
        public string strZONE_Set3 { get; set; }
    }
    public class EQP_Info_ZONE4
    {
        public string strZONE_Set4 { get; set; }
    }

    public class EQP_Info_EQPID3 : INotifyPropertyChanged
    {
        public string _strEQPID_Set3;
        public string strEQPID_Set3
        {
            get { return _strEQPID_Set3; }
            set { _strEQPID_Set3 = value; RaisePropertyChangedEvent("strEQPID_Set3"); }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        void RaisePropertyChangedEvent(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
    public class EQP_Info_EQPID4 : INotifyPropertyChanged
    {
        public string _strEQPID_Set4;
        public string strEQPID_Set4
        {
            get { return _strEQPID_Set4; }
            set { _strEQPID_Set4 = value; RaisePropertyChangedEvent("strEQPID_Set4"); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        void RaisePropertyChangedEvent(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }

    public class EQP_Info_chkEQPID3 : INotifyPropertyChanged
    {
        public string _strchkEQPID_Set3;
        public string strchkEQPID_Set3
        {
            get { return _strchkEQPID_Set3; }
            set { _strchkEQPID_Set3 = value; RaisePropertyChangedEvent("strchkEQPID_Set3"); }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        void RaisePropertyChangedEvent(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
    #endregion
}
