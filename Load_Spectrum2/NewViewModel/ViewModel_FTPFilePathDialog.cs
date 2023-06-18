using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace Load_Spectrum2.NewViewModel
{
    public class ViewModel_FTPFilePathDialog : INotifyPropertyChanged
    {
        //global function set
        public static Global_FunSet gfun;        
        public static Dictionary<string, string> source_EQP_dict = new Dictionary<string, string>();

        public ViewModel_FTPFilePathDialog()
        {
            gfun = new Global_FunSet();

            source_EQP_Info_ftpdia = new List<NewModel.Model>();
            source_ftpdia_all = new List<NewModel.Model>();

            foreach (DataRow row in gfun.dt_eqp_info.Rows)
            {
                source_EQP_Info_ftpdia.Add(new NewModel.Model() { strEQPID_ftpdia = row["EQPID"].ToString() });
            }
        }

        public List<NewModel.Model> FTPDialogEQPFilter(string str_ftpdia)
        {
            source_EQP_Info_ftpdia.Clear();
            foreach (DataRow row in gfun.dt_eqp_info.Rows)
            {
                if (row["EQPID"].ToString().StartsWith(str_ftpdia))
                {
                    source_EQP_Info_ftpdia.Add(new NewModel.Model() { strEQPID_ftpdia = row["EQPID"].ToString() });
                }
            }

            return source_EQP_Info_ftpdia;
        }

        
        public (List<NewModel.Model>,string) ReturnPathSrc_ftpdia(string uri, string id, string pw)
        {
            //경로 및 리스트 소스 반환
            source_ftpdia_all.Clear();
            try
            {
                FtpWebRequest ftpReq = (FtpWebRequest)WebRequest.Create(new Uri(uri.Replace("#", "%23")));                   //ftp 생성                
                ftpReq.Credentials = new NetworkCredential(id, pw);
                ftpReq.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
                ftpReq.Timeout = Global_FunSet.ftpTimeout;                
                FtpWebResponse resFtp = (FtpWebResponse)ftpReq.GetResponse();

                //앞뒤 버튼용 경로 리스트
                //Dispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate
                //{
                //    if (dic_ftp["foback_flag"] == "1")
                //    {
                //        if (dic_ftp["set_flag"] == "1") EQP1_Back_Path.Add(this.line_set1_eqp.Text);
                //        else if (dic_ftp["set_flag"] == "2") EQP2_Back_Path.Add(this.line_set2_eqp.Text);

                //    }
                //}));

                StreamReader reader;
                reader = new StreamReader(resFtp.GetResponseStream());

                string pattern = @"^([\w-]+)\s+(\d+)\s+(\w+)\s+(\w+)\s+(\d+)\s+" + @"(\w+\s+\d+\s+\d+|\w+\s+\d+\s+\d+:\d+)\s+(.+)$";        //line 매칭 가능한 패턴            

                Regex regex = new Regex(pattern);
                IFormatProvider culture = CultureInfo.GetCultureInfo("en-us");
                string[] hourMinFormats =
                    new[] { "MMM dd HH:mm", "MMM dd H:mm", "MMM d HH:mm", "MMM d H:mm" };
                string[] yearFormats =
                    new[] { "MMM dd yyyy", "MMM d yyyy" };

                while (!reader.EndOfStream)
                {
                    try
                    {
                        string line = reader.ReadLine();
                        Match match = regex.Match(line);
                        string permissions = match.Groups[1].Value;
                        int inode = int.Parse(match.Groups[2].Value, culture);
                        string owner = match.Groups[3].Value;
                        string group = match.Groups[4].Value;
                        long size = long.Parse(match.Groups[5].Value, culture);
                        string s = Regex.Replace(match.Groups[6].Value, @"\s+", " ");

                        string[] formats = (s.IndexOf(':') >= 0) ? hourMinFormats : yearFormats;
                        var modified = DateTime.ParseExact(s, formats, culture, DateTimeStyles.None);
                        string name = match.Groups[7].Value;
                        string extension = permissions.Substring(0, 1) == "d" ? "폴더" : "file";

                        if (name.Substring(0, 1) == "$")
                        {
                            continue;
                        }

                        if (extension == "file")
                        {
                            if (size == 0)
                            {
                                continue;
                            }
                            try
                            {
                                extension = name.Substring(name.LastIndexOf("."), name.Length - name.LastIndexOf("."));
                                if (extension == ".sys")
                                {
                                    continue;
                                }
                            }
                            catch
                            {
                                extension = "알수없는파일";
                            }
                        }

                        source_ftpdia_all.Add(new NewModel.Model()
                        {
                            ftp_all_filename = name,
                            ftp_all_date = modified,
                            ftp_all_extension = extension,
                            ftp_all_size = Global_FunSet.ReturnFileSizeStr(size),
                            ftp_all_src = Global_FunSet.ReturnIconPathStr(extension)
                        });                        
                    }
                    catch
                    {

                    }
                }
                return (source_ftpdia_all, "양호");
            }

            //ftp 접속 실패
            catch (Exception ex)
            {
                return (null, ex.Message);
            }
        }

        //Binding Properties
        #region Properties       

        private List<NewModel.Model> _source_EQP_Info_ftpdia;
        public List<NewModel.Model> source_EQP_Info_ftpdia
        {
            get { return _source_EQP_Info_ftpdia; }
            set { _source_EQP_Info_ftpdia = value; OnPropertyChanged("source_EQP_Info_ftpdia"); }
        }

        private List<NewModel.Model> _source_ftpdia_all;
        public List<NewModel.Model> source_ftpdia_all
        {
            get { return _source_ftpdia_all; }
            set { _source_ftpdia_all = value; OnPropertyChanged("source_ftpdia_all"); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        //[NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
#endregion