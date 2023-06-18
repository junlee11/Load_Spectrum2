using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Threading;

namespace Load_Spectrum2
{
    public partial class MainWindow : Window
    {
        BackgroundWorker bw_local_eqp;
        BackgroundWorker bw_local_eqp2;
        BackgroundWorker bw_local_eqp3;
        BackgroundWorker bw_local_eqp4;
        private void TrLocalToEQP(int src_flag, int tar_flag, string src_path, string tar_path)
        {
            try
            {
                //인터락 조건
                if (tar_flag == 1 || tar_flag == 2) return;     //타겟이 로컬 리스트
                if (src_flag == 3 || src_flag == 4) return;     //소스가 설비 리스트
                if (uiList_textbox[tar_flag - 1].Text == "") return;    //타겟 텍스트 공란
                if (uiList_listView[src_flag - 1].SelectedItems.Count == 0) return;     //소스 아이템 선택안됨

                string[] src_items = new string[uiList_listView[src_flag - 1].SelectedItems.Count];
                int i = 0;
                dynamic item_str;
                foreach (var item in uiList_listView[src_flag - 1].SelectedItems)
                {
                    if (src_flag == 1) item_str = (Local_path_all1)item;
                    else item_str = (Local_path_all2)item;
                    src_items[i] = src_path + "\\" + item_str.local_all_filename;
                    i++;
                }

                if (src_flag == 1 && tar_flag == 3)
                {
                    bw_local_eqp = new BackgroundWorker();
                    Dictionary<string, dynamic> bw_src = new Dictionary<string, dynamic>();
                    bw_src.Add("bw_main", bw_local_eqp);
                    bw_src.Add("src_flag", src_flag);
                    bw_src.Add("tar_flag", tar_flag);
                    bw_src.Add("src_path", src_path);
                    bw_src.Add("tar_path", tar_path);
                    bw_src.Add("src_items", src_items);
                    bw_local_eqp.DoWork += new DoWorkEventHandler(bw_local_eqp_DoWork);
                    bw_local_eqp.ProgressChanged += bw_local_eqp_ProgressChanged;
                    bw_local_eqp.RunWorkerCompleted += bw_local_eqp_RunWorkerCompleted;
                    bw_local_eqp.WorkerReportsProgress = true;
                    bw_local_eqp.WorkerSupportsCancellation = true;
                    bw_local_eqp.RunWorkerAsync(argument: bw_src);
                }
                else if (src_flag == 2 && tar_flag == 3)
                {
                    bw_local_eqp2 = new BackgroundWorker();
                    Dictionary<string, dynamic> bw_src = new Dictionary<string, dynamic>();
                    bw_src.Add("bw_main", bw_local_eqp2);
                    bw_src.Add("src_flag", src_flag);
                    bw_src.Add("tar_flag", tar_flag);
                    bw_src.Add("src_path", src_path);
                    bw_src.Add("tar_path", tar_path);
                    bw_src.Add("src_items", src_items);
                    bw_local_eqp2.DoWork += new DoWorkEventHandler(bw_local_eqp_DoWork);
                    bw_local_eqp2.ProgressChanged += bw_local_eqp_ProgressChanged;
                    bw_local_eqp2.RunWorkerCompleted += bw_local_eqp_RunWorkerCompleted;
                    bw_local_eqp2.WorkerReportsProgress = true;
                    bw_local_eqp2.WorkerSupportsCancellation = true;
                    bw_local_eqp2.RunWorkerAsync(argument: bw_src);
                }
                else if (src_flag == 1 && tar_flag == 4)
                {
                    bw_local_eqp3 = new BackgroundWorker();
                    Dictionary<string, dynamic> bw_src = new Dictionary<string, dynamic>();
                    bw_src.Add("bw_main", bw_local_eqp3);
                    bw_src.Add("src_flag", src_flag);
                    bw_src.Add("tar_flag", tar_flag);
                    bw_src.Add("src_path", src_path);
                    bw_src.Add("tar_path", tar_path);
                    bw_src.Add("src_items", src_items);
                    bw_local_eqp3.DoWork += new DoWorkEventHandler(bw_local_eqp_DoWork);
                    bw_local_eqp3.ProgressChanged += bw_local_eqp_ProgressChanged;
                    bw_local_eqp3.RunWorkerCompleted += bw_local_eqp_RunWorkerCompleted;
                    bw_local_eqp3.WorkerReportsProgress = true;
                    bw_local_eqp3.WorkerSupportsCancellation = true;
                    bw_local_eqp3.RunWorkerAsync(argument: bw_src);
                }
                else if (src_flag == 2 && tar_flag == 4)
                {
                    bw_local_eqp4 = new BackgroundWorker();
                    Dictionary<string, dynamic> bw_src = new Dictionary<string, dynamic>();
                    bw_src.Add("bw_main", bw_local_eqp4);
                    bw_src.Add("src_flag", src_flag);
                    bw_src.Add("tar_flag", tar_flag);
                    bw_src.Add("src_path", src_path);
                    bw_src.Add("tar_path", tar_path);
                    bw_src.Add("src_items", src_items);
                    bw_local_eqp4.DoWork += new DoWorkEventHandler(bw_local_eqp_DoWork);
                    bw_local_eqp4.ProgressChanged += bw_local_eqp_ProgressChanged;
                    bw_local_eqp4.RunWorkerCompleted += bw_local_eqp_RunWorkerCompleted;
                    bw_local_eqp4.WorkerReportsProgress = true;
                    bw_local_eqp4.WorkerSupportsCancellation = true;
                    bw_local_eqp4.RunWorkerAsync(argument: bw_src);
                }
                src_flag = 0;
                tar_flag = 0;
                src_path = "";
                tar_path = "";
                src_items = null;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }            
        }

        //Local To EQP
        private void bw_local_eqp_DoWork(object sender, DoWorkEventArgs e)
        {
            var bw_dic = (Dictionary<string, dynamic>)e.Argument;
            int src_flag = (int)bw_dic["src_flag"];
            int tar_flag = (int)bw_dic["tar_flag"];
            string src_path = (string)bw_dic["src_path"];
            string tar_path = (string)bw_dic["tar_path"];
            string[] src_items = (string[])bw_dic["src_items"];
            var bw_main = (BackgroundWorker)bw_dic["bw_main"];

            Dispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate
            {
                uiList_pgb[tar_flag - 1].Value = 0;
                uiList_pgbTxt[tar_flag - 1].Text = "0 %";
                uiList_pgbSpinner[tar_flag - 1].Visibility = Visibility.Visible;
                uiList_StopBtn[tar_flag - 1].Visibility = Visibility.Visible;
            }));

            long result_cnt = 0;
            result_cnt = Global_FunSet.ReturnLocalFileFolderCount(src_items, result_cnt);
            int cnt = 0;
            int case_flag = 0;
            string raw_path = "";
            int interlock = 0;            
            List<int> rst = new List<int>();

            foreach (var src_item in src_items)
            {
                
                (cnt, raw_path, case_flag) = LocalToEQPFileDir(src_item, 0, bw_main, src_flag, tar_flag, src_path, tar_path, cnt, result_cnt, case_flag, raw_path, 0);
                if (cnt == -1)
                {
                    //rst.Add(-1);
                    interlock = -1;
                    break;
                }
                if (cnt == -2)
                {
                    //rst.Add(-2);
                    interlock = -2;
                    break;
                }
            }
            if (interlock == 0) interlock = 1;

            rst.Add(interlock);
            rst.Add(tar_flag);
            rst.Add(src_flag);

            e.Result = rst;
            //e.Result = tar_flag;
        }

        private void bw_local_eqp_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            // Running on a UI thread
            var args = (Tuple<int, int>)e.UserState;
            uiList_pgb[args.Item2 - 1].Value = e.ProgressPercentage;
            uiList_pgbTxt[args.Item2 - 1].Text = e.ProgressPercentage.ToString() + " %";
        }

        private void bw_local_eqp_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // Running on a UI thread
            List<int> rst = (List<int>)e.Result;

            int interlock = rst[0];
            int tar_flag = rst[1];
            int src_flag = rst[2];

            try
            {
                var arg = rst[1];

                if (interlock == -1)
                {
                    uiList_pgb[arg - 1].Value = 0;
                    uiList_pgbSpinner[arg - 1].Visibility = Visibility.Collapsed;
                    uiList_StopBtn[arg - 1].Visibility = Visibility.Collapsed;                    
                    uiList_listView[arg - 1].Items.Refresh();
                    uiList_pgbTxt[arg - 1].Text = "권한거부";
                    uiList_listView[arg - 1].IsHitTestVisible = true;
                }
                else if (interlock == -2)
                {
                    uiList_pgbSpinner[arg - 1].Visibility = Visibility.Collapsed;
                    uiList_StopBtn[arg - 1].Visibility = Visibility.Collapsed;
                    uiList_listView[arg - 1].Items.Refresh();
                    uiList_pgbTxt[arg - 1].Text = "중단됨";
                    uiList_listView[arg - 1].IsHitTestVisible = true;

                    if (src_flag == 1 && tar_flag == 3 && LTE_stop_flag == 1) LTE_stop_flag = 0;
                    else if (src_flag == 2 && tar_flag == 3 & LTE_stop_flag2 == 1) LTE_stop_flag2 = 0;
                    else if (src_flag == 1 && tar_flag == 4 & LTE_stop_flag3 == 1) LTE_stop_flag3 = 0;
                    else if (src_flag == 2 && tar_flag == 4 & LTE_stop_flag4 == 1) LTE_stop_flag4 = 0;
                }
                else
                {
                    uiList_pgb[arg - 1].Value = 100;
                    uiList_pgbSpinner[arg - 1].Visibility = Visibility.Collapsed;
                    uiList_StopBtn[arg - 1].Visibility = Visibility.Collapsed;
                    uiList_listView[arg - 1].Items.Refresh();
                    uiList_pgbTxt[arg - 1].Text = "100 %";
                    uiList_listView[arg - 1].IsHitTestVisible = true;
                }

                

                //IsNew 다시 false로 만들어주기                

                if (arg == 3)
                {                    
                    var list_items = from a in source_EQP_path_all1
                                     where a.IsNew == true
                                     select a;

                    int i = 0;
                    uiList_listView[arg - 1].Focus();
                    uiList_listView[arg - 1].SelectedItems.Clear();
                    foreach (var item in list_items)
                    {
                        item.IsNew = false;
                        var j = uiList_listView[arg - 1].Items[i];
                        uiList_listView[arg - 1].SelectedItems.Add(j);                        
                        i++;
                    }
                }                    
                else if (arg == 4)
                {
                    var list_items = from a in source_EQP_path_all2
                                     where a.IsNew == true
                                     select a;

                    int i = 0;
                    uiList_listView[arg - 1].Focus();
                    uiList_listView[arg - 1].SelectedItems.Clear();
                    foreach (var item in list_items)
                    {
                        item.IsNew = false;
                        var j = uiList_listView[arg - 1].Items[i];
                        uiList_listView[arg - 1].SelectedItems.Add(j);
                        i++;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }            
        }

        //Local To EQP (case2)
        private (int, string, int) LocalToEQPFileDir(string src_item, int level, BackgroundWorker bw_main, int src_flag, int tar_flag, string src_path, string tar_path, int cnt, long result_cnt, int case_flag, string raw_path, int level_flag)
        {
            if (cnt == -1) return (-1, "", -1);

            if (src_flag == 1 && tar_flag == 3 && LTE_stop_flag == 1) return (-2, "", -1);
            else if (src_flag == 2 && tar_flag == 3 & LTE_stop_flag2 == 1) return (-2, "", -1);
            else if (src_flag == 1 && tar_flag == 4 & LTE_stop_flag3 == 1) return (-2, "", -1);
            else if (src_flag == 2 && tar_flag == 4 & LTE_stop_flag4 == 1) return (-2, "", -1);

            string id = "";
            string pw = "";

            if (tar_flag - 1 == 2)
            {
                id = source_ftp_info1["id"];
                pw = source_ftp_info1["pw"];
            }
            else if (tar_flag - 1 == 3)
            {
                id = source_ftp_info2["id"];
                pw = source_ftp_info2["pw"];
            }

            //경로 인터락
            DirectoryInfo di_temp = new DirectoryInfo(src_item);
            if ((!Global_FunSet.IsExistDirectory(tar_path, id, pw) && !Global_FunSet.IsExistFile(tar_path, id, pw)) || (!di_temp.Exists && !File.Exists(src_item)))
            {
                return (-1, "", -1);
            }

            try
            {
                string t_path = "";
                if (cnt == 0)
                {
                    //raw_path = uiList_textbox[source_set_flag - 1].Text;
                    raw_path = src_path;
                    level_flag = 0;
                    case_flag = 0;
                }
                level_flag += level;

                FileAttributes chkAtt = File.GetAttributes(src_item);
                if ((chkAtt & FileAttributes.Directory) == FileAttributes.Directory)
                {
                    //폴더일때
                    string real_path = src_item.Substring(raw_path.Length + 1, src_item.Length - raw_path.Length - 1).Replace("\\", "/");
                    string real_filename = src_item.Substring(src_item.LastIndexOf("\\") + 1, src_item.Length - src_item.LastIndexOf("\\") - 1);

                    if (real_path == real_filename)
                    {
                        Dispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate
                        {
                            t_path = tar_path + "/" + real_filename;
                        }));
                    }
                    else
                    {
                        Dispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate
                        {
                            t_path = tar_path + "/" + real_path;
                        }));
                    }

                    //이미 폴더가 있다면
                    if (Global_FunSet.IsExistDirectory(t_path, id, pw))
                    {
                        Dispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate
                        {
                            NewOverRideChk newOverRideChk = new NewOverRideChk(t_path.Substring(t_path.LastIndexOf("/") + 1, t_path.Length - t_path.LastIndexOf("/") - 1));

                            if (case_flag != 2 && case_flag != 4)
                            {
                                if (newOverRideChk.ShowDialog() == true)
                                {
                                    if (newOverRideChk.temp_txt.Text == "") case_flag = 1;
                                    else if (newOverRideChk.temp_txt.Text == "all") case_flag = 2;
                                }
                                else
                                {
                                    if (newOverRideChk.temp_txt.Text == "") case_flag = 3;
                                    else if (newOverRideChk.temp_txt.Text == "all") case_flag = 4;
                                }
                            }
                        }));
                        //하나만 덮어씌울 때
                        if (case_flag == 1 || case_flag == 2)
                        {

                        }
                        else
                        {
                            //모두 건너뛰기
                        }

                    }
                    //폴더 없으면 폴더 생성
                    else
                    {
                        try
                        {
                            //폴더 생성
                            var request = (FtpWebRequest)WebRequest.Create(new Uri(t_path.Replace("#", "%23")));
                            if (tar_flag == 3) request.Credentials = new NetworkCredential(source_ftp_info1["id"], source_ftp_info1["pw"]);
                            else if (tar_flag == 4) request.Credentials = new NetworkCredential(source_ftp_info2["id"], source_ftp_info2["pw"]);

                            request.UseBinary = true;
                            request.UsePassive = true;
                            request.Method = WebRequestMethods.Ftp.MakeDirectory;
                            request.Timeout = Global_FunSet.ftpTimeout;
                            FtpWebResponse resFtp = (FtpWebResponse)request.GetResponse();
                            resFtp.Close();
                        }
                        catch
                        {
                            MessageBox.Show("폴더 생성 실패, 연결 상태 확인 필요");
                        }
                    }

                    if (tar_flag == 3 && level_flag == 0)
                    {
                        var list_item = source_EQP_path_all1.SingleOrDefault(x => x.eqp_all_filename == t_path.Substring(t_path.LastIndexOf("/") + 1, t_path.Length - t_path.LastIndexOf("/") - 1));
                        if (list_item == null)
                        {
                            long raw_fileSize = 0;
                            string fileExetension = "폴더";
                            source_EQP_path_all1.Insert(0, new EQP_path_all1()
                            {
                                eqp_all_filename = t_path.Substring(t_path.LastIndexOf("/") + 1, t_path.Length - t_path.LastIndexOf("/") - 1),
                                eqp_all_date = DateTime.Now,
                                eqp_all_size = Global_FunSet.ReturnFileSizeStr(raw_fileSize),
                                eqp_all_extension = fileExetension,
                                eqp_all_src = Global_FunSet.ReturnIconPathStr(fileExetension),
                                IsNew = true
                            });
                        }

                    }
                    else if (tar_flag == 4 && level_flag == 0)
                    {
                        var list_item = source_EQP_path_all2.SingleOrDefault(x => x.eqp_all_filename == t_path.Substring(t_path.LastIndexOf("/") + 1, t_path.Length - t_path.LastIndexOf("/") - 1));
                        if (list_item == null)
                        {
                            long raw_fileSize = 0;
                            string fileExetension = "폴더";
                            source_EQP_path_all2.Insert(0, new EQP_path_all2()
                            {
                                eqp_all_filename = t_path.Substring(t_path.LastIndexOf("/") + 1, t_path.Length - t_path.LastIndexOf("/") - 1),
                                eqp_all_date = DateTime.Now,
                                eqp_all_size = Global_FunSet.ReturnFileSizeStr(raw_fileSize),
                                eqp_all_extension = fileExetension,
                                eqp_all_src = Global_FunSet.ReturnIconPathStr(fileExetension),
                                IsNew = true
                            });
                        }
                    }

                    cnt++;
                    double ratio = ((double)cnt / (double)result_cnt) * 100.0;
                    bw_main.ReportProgress((int)ratio, new Tuple<int, int>(src_flag, tar_flag));

                    //하위 디렉토리 탐색
                    string[] tmpPath = Directory.GetDirectories(src_item);
                    foreach (string s in tmpPath)
                    {
                        (cnt, raw_path, case_flag) = LocalToEQPFileDir(s, 1, bw_main, src_flag, tar_flag, src_path, tar_path, cnt, result_cnt, case_flag, raw_path, level_flag);
                        if (level_flag != 0) level_flag -= 1;
                    }

                    //하위 파일일 경우
                    string[] tmpFiles = Directory.GetFiles(src_item);
                    foreach (string s in tmpFiles)
                    {
                        (cnt, raw_path, case_flag) = LocalToEQPFileDir(s, 1, bw_main, src_flag, tar_flag, src_path, tar_path, cnt, result_cnt, case_flag, raw_path, level_flag);
                        if (level_flag != 0) level_flag -= 1;
                    }
                }
                else
                {
                    //파일일때
                    string real_path = src_item.Substring(raw_path.Length + 1, src_item.Length - raw_path.Length - 1).Replace("\\", "/");
                    string real_filename = src_item.Substring(src_item.LastIndexOf("\\") + 1, src_item.Length - src_item.LastIndexOf("\\") - 1);

                    if (real_path == real_filename)
                    {
                        t_path = tar_path + "/" + real_filename;
                    }
                    else
                    {
                        t_path = tar_path + "/" + real_path;
                    }
                    
                    //같은 이름의 파일 존재할때
                    //if (IsExistFile(t_path, tar_flag - 2))
                    if (Global_FunSet.IsExistFile(t_path, id, pw))
                    {
                        //이미 존재하면
                        Dispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate
                        {
                            NewOverRideChk newOverRideChk = new NewOverRideChk(real_filename);

                            if (case_flag != 2 && case_flag != 4)
                            {
                                if (newOverRideChk.ShowDialog() == true)
                                {
                                    if (newOverRideChk.temp_txt.Text == "") case_flag = 1;
                                    else if (newOverRideChk.temp_txt.Text == "all") case_flag = 2;

                                }
                                else
                                {
                                    if (newOverRideChk.temp_txt.Text == "") case_flag = 3;
                                    else if (newOverRideChk.temp_txt.Text == "all") case_flag = 4;
                                }
                            }
                        }));

                        //하나만 덮어씌울 때
                        if (case_flag == 1 || case_flag == 2)
                        {
                            //파일 덮어쓰기
                            try
                            {
                                var request = (FtpWebRequest)WebRequest.Create(new Uri(t_path.Replace("#", "%23")));
                                if (tar_flag == 3) request.Credentials = new NetworkCredential(source_ftp_info1["id"], source_ftp_info1["pw"]);
                                else if (tar_flag == 4) request.Credentials = new NetworkCredential(source_ftp_info2["id"], source_ftp_info2["pw"]);
                                request.Method = WebRequestMethods.Ftp.UploadFile;
                                request.Timeout = Global_FunSet.ftpTimeout;
                                FileStream sourceFileStream = new FileStream(src_item, FileMode.Open, FileAccess.Read);
                                Stream targetStream = request.GetRequestStream();
                                //FtpWebResponse resFtp = (FtpWebResponse)request.GetResponse();
                                byte[] bufferByteArray = new byte[1024];
                                while (true)
                                {
                                    int byteCount = sourceFileStream.Read(bufferByteArray, 0, bufferByteArray.Length);
                                    if (byteCount == 0)
                                    {
                                        break;
                                    }
                                    targetStream.Write(bufferByteArray, 0, byteCount);
                                }
                                targetStream.Close();
                                sourceFileStream.Close();
                            }
                            catch
                            {

                            }
                        }
                        else
                        {
                            //건너뛰기
                        }

                    }
                    //새로운 파일 다운로드
                    else
                    {
                        try
                        {
                            var request = (FtpWebRequest)WebRequest.Create(new Uri(t_path.Replace("#", "%23")));
                            if (tar_flag == 3) request.Credentials = new NetworkCredential(source_ftp_info1["id"], source_ftp_info1["pw"]);
                            else if (tar_flag == 4) request.Credentials = new NetworkCredential(source_ftp_info2["id"], source_ftp_info2["pw"]);
                            request.Method = WebRequestMethods.Ftp.UploadFile;
                            FileStream sourceFileStream = new FileStream(src_item, FileMode.Open, FileAccess.Read);
                            Stream targetStream = request.GetRequestStream();
                            byte[] bufferByteArray = new byte[1024];
                            while (true)
                            {
                                int byteCount = sourceFileStream.Read(bufferByteArray, 0, bufferByteArray.Length);
                                if (byteCount == 0)
                                {
                                    break;
                                }
                                targetStream.Write(bufferByteArray, 0, byteCount);
                            }
                            targetStream.Close();
                            sourceFileStream.Close();
                        }
                        catch
                        {

                        }
                    }

                    if (tar_flag == 3 && level_flag == 0)
                    {
                        var list_item = source_EQP_path_all1.SingleOrDefault(x => x.eqp_all_filename == real_filename);
                        if (list_item == null)
                        {
                            FtpWebRequest ftpReq = (FtpWebRequest)WebRequest.Create(new Uri(t_path.Replace("#", "%23")));                   //ftp 생성
                            ftpReq.Credentials = new NetworkCredential(source_ftp_info1["id"], source_ftp_info1["pw"]);
                            ftpReq.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
                            ftpReq.Timeout = Global_FunSet.ftpTimeout;
                            FtpWebResponse resFtp = (FtpWebResponse)ftpReq.GetResponse();
                            resFtp.Close();

                            long raw_fileSize = new FileInfo(src_item).Length;
                            string fileExetension = new FileInfo(src_item).Extension;
                            source_EQP_path_all1.Insert(0, new EQP_path_all1()
                            {
                                eqp_all_filename = t_path.Substring(t_path.LastIndexOf("/") + 1, t_path.Length - t_path.LastIndexOf("/") - 1),
                                eqp_all_date = DateTime.Now,
                                eqp_all_size = Global_FunSet.ReturnFileSizeStr(raw_fileSize),
                                eqp_all_extension = fileExetension,
                                eqp_all_src = Global_FunSet.ReturnIconPathStr(fileExetension),
                                IsNew = true
                            });
                        }
                    }
                    else if (tar_flag == 4 && level_flag == 0)
                    {
                        var list_item = source_EQP_path_all2.SingleOrDefault(x => x.eqp_all_filename == real_filename);
                        if (list_item == null)
                        {
                            FtpWebRequest ftpReq = (FtpWebRequest)WebRequest.Create(new Uri(t_path.Replace("#", "%23")));                   //ftp 생성
                            ftpReq.Credentials = new NetworkCredential(source_ftp_info2["id"], source_ftp_info2["pw"]);
                            ftpReq.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
                            ftpReq.Timeout = Global_FunSet.ftpTimeout;
                            FtpWebResponse resFtp = (FtpWebResponse)ftpReq.GetResponse();
                            resFtp.Close();

                            long raw_fileSize = new FileInfo(src_item).Length;
                            string fileExetension = new FileInfo(src_item).Extension;
                            source_EQP_path_all2.Insert(0, new EQP_path_all2()
                            {
                                eqp_all_filename = t_path.Substring(t_path.LastIndexOf("/") + 1, t_path.Length - t_path.LastIndexOf("/") - 1),
                                eqp_all_date = DateTime.Now,
                                eqp_all_size = Global_FunSet.ReturnFileSizeStr(raw_fileSize),
                                eqp_all_extension = fileExetension,
                                eqp_all_src = Global_FunSet.ReturnIconPathStr(fileExetension),
                                IsNew = true
                            });
                        }
                    }
                    cnt++;
                    double ratio = ((double)cnt / (double)result_cnt) * 100.0;
                    bw_main.ReportProgress((int)ratio, new Tuple<int, int>(src_flag, tar_flag));
                }
                return (cnt, raw_path, case_flag);
            }
            catch
            {
                return (-1, "", -1);
            }
            
        }
    }
}
