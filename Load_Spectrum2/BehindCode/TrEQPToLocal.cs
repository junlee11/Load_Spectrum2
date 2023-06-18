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
        BackgroundWorker bw_eqp_local;
        BackgroundWorker bw_eqp_local2;
        BackgroundWorker bw_eqp_local3;
        BackgroundWorker bw_eqp_local4;
        private void TrEQPToLocal(int src_flag, int tar_flag, string src_path, string tar_path)
        {
            try
            {
                //인터락 조건
                if (tar_flag == 3 || tar_flag == 4) return;     //타겟이 설비 리스트
                if (src_flag == 1 || src_flag == 2) return;     //소스가 소스 리스트
                if (uiList_textbox[tar_flag - 1].Text == "") return;    //타겟 텍스트 공란
                if (uiList_listView[src_flag - 1].SelectedItems.Count == 0) return;     //소스 아이템 선택안됨

                string[] src_items = new string[uiList_listView[src_flag - 1].SelectedItems.Count];
                int i = 0;
                dynamic item_str;
                foreach (var item in uiList_listView[src_flag - 1].SelectedItems)
                {
                    if (src_flag == 3) item_str = (EQP_path_all1)item;
                    else item_str = (EQP_path_all2)item;
                    src_items[i] = src_path + "/" + item_str.eqp_all_filename;
                    i++;
                }

                if (tar_flag == 1 && src_flag == 3)
                {
                    bw_eqp_local = new BackgroundWorker();
                    Dictionary<string, dynamic> bw_src = new Dictionary<string, dynamic>();
                    bw_src.Add("bw_main", bw_eqp_local);
                    bw_src.Add("src_flag", src_flag);
                    bw_src.Add("tar_flag", tar_flag);
                    bw_src.Add("src_path", src_path);
                    bw_src.Add("tar_path", tar_path);
                    bw_src.Add("src_items", src_items);
                    bw_eqp_local.DoWork += new DoWorkEventHandler(bw_eqp_local_DoWork);
                    bw_eqp_local.ProgressChanged += bw_eqp_local_ProgressChanged;
                    bw_eqp_local.RunWorkerCompleted += bw_eqp_local_RunWorkerCompleted;
                    bw_eqp_local.WorkerReportsProgress = true;
                    bw_eqp_local.WorkerSupportsCancellation = true;
                    bw_eqp_local.RunWorkerAsync(argument: bw_src);
                }
                else if (tar_flag == 2 && src_flag == 3)
                {
                    bw_eqp_local2 = new BackgroundWorker();
                    Dictionary<string, dynamic> bw_src = new Dictionary<string, dynamic>();
                    bw_src.Add("bw_main", bw_eqp_local2);
                    bw_src.Add("src_flag", src_flag);
                    bw_src.Add("tar_flag", tar_flag);
                    bw_src.Add("src_path", src_path);
                    bw_src.Add("tar_path", tar_path);
                    bw_src.Add("src_items", src_items);
                    bw_eqp_local2.DoWork += new DoWorkEventHandler(bw_eqp_local_DoWork);
                    bw_eqp_local2.ProgressChanged += bw_eqp_local_ProgressChanged;
                    bw_eqp_local2.RunWorkerCompleted += bw_eqp_local_RunWorkerCompleted;
                    bw_eqp_local2.WorkerReportsProgress = true;
                    bw_eqp_local2.WorkerSupportsCancellation = true;
                    bw_eqp_local2.RunWorkerAsync(argument: bw_src);
                }
                else if (tar_flag == 1 && src_flag == 4)
                {
                    bw_eqp_local3 = new BackgroundWorker();
                    Dictionary<string, dynamic> bw_src = new Dictionary<string, dynamic>();
                    bw_src.Add("bw_main", bw_eqp_local3);
                    bw_src.Add("src_flag", src_flag);
                    bw_src.Add("tar_flag", tar_flag);
                    bw_src.Add("src_path", src_path);
                    bw_src.Add("tar_path", tar_path);
                    bw_src.Add("src_items", src_items);
                    bw_eqp_local3.DoWork += new DoWorkEventHandler(bw_eqp_local_DoWork);
                    bw_eqp_local3.ProgressChanged += bw_eqp_local_ProgressChanged;
                    bw_eqp_local3.RunWorkerCompleted += bw_eqp_local_RunWorkerCompleted;
                    bw_eqp_local3.WorkerReportsProgress = true;
                    bw_eqp_local3.WorkerSupportsCancellation = true;
                    bw_eqp_local3.RunWorkerAsync(argument: bw_src);
                }
                else if (tar_flag == 2 && src_flag == 4)
                {
                    bw_eqp_local4 = new BackgroundWorker();
                    Dictionary<string, dynamic> bw_src = new Dictionary<string, dynamic>();
                    bw_src.Add("bw_main", bw_eqp_local4);
                    bw_src.Add("src_flag", src_flag);
                    bw_src.Add("tar_flag", tar_flag);
                    bw_src.Add("src_path", src_path);
                    bw_src.Add("tar_path", tar_path);
                    bw_src.Add("src_items", src_items);
                    bw_eqp_local4.DoWork += new DoWorkEventHandler(bw_eqp_local_DoWork);
                    bw_eqp_local4.ProgressChanged += bw_eqp_local_ProgressChanged;
                    bw_eqp_local4.RunWorkerCompleted += bw_eqp_local_RunWorkerCompleted;
                    bw_eqp_local4.WorkerReportsProgress = true;
                    bw_eqp_local4.WorkerSupportsCancellation = true;
                    bw_eqp_local4.RunWorkerAsync(argument: bw_src);
                }
                src_flag = 0;
                tar_flag = 0;
                src_path = "";
                tar_path = "";
                src_items = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //EQP To Local
        private void bw_eqp_local_DoWork(object sender, DoWorkEventArgs e)
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

            string id = "";
            string pw = "";

            if (src_flag == 3)
            {
                id = source_ftp_info1["id"];
                pw = source_ftp_info1["pw"];
            }
            else if (src_flag == 4)
            {
                id = source_ftp_info2["id"];
                pw = source_ftp_info2["pw"];
            }

            long result_cnt = 0;
            result_cnt = Global_FunSet.ReturnEQPFileFolderCount(src_items, id, pw, result_cnt);
            int cnt = 0;
            int case_flag = 0;
            string raw_path = "";
            int interlock = 0;

            List<int> rst = new List<int>();

            foreach (var src_item in src_items)
            {
                if (bw_main.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }
                (cnt, raw_path, case_flag) = EQPToLocalFileDir(src_item, 0, bw_main, src_flag, tar_flag, src_path, tar_path, cnt, result_cnt, case_flag, raw_path, 0);
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
                //LocalToEQPFileDir(src_item, 0, bw_main, src_flag, tar_flag, src_path, tar_path, cnt, result_cnt, case_flag, raw_path, 0);
            }

            if (interlock == 0) interlock = 1;

            rst.Add(interlock);
            rst.Add(tar_flag);
            rst.Add(src_flag);

            e.Result = rst;
            //e.Result = tar_flag;

        }

        private void bw_eqp_local_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            // Running on a UI thread
            var args = (Tuple<int, int>)e.UserState;
            uiList_pgb[args.Item2 - 1].Value = e.ProgressPercentage;
            uiList_pgbTxt[args.Item2 - 1].Text = e.ProgressPercentage.ToString() + " %";
        }

        private void bw_eqp_local_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // Running on a UI thread            
            List<int> rst = (List<int>)e.Result;

            int interlock = rst[0];
            int tar_flag = rst[1];
            int src_flag = rst[2];
                
            try
            {
                var arg = tar_flag;

                if (interlock == -1)
                {
                    uiList_pgb[arg - 1].Value = 0;
                    uiList_pgbSpinner[arg - 1].Visibility = Visibility.Collapsed;
                    uiList_StopBtn[arg - 1].Visibility = Visibility.Collapsed;
                    uiList_listView[arg - 1].Items.Refresh();
                    uiList_pgbTxt[arg - 1].Text = "접속 불가";
                    uiList_listView[arg - 1].IsHitTestVisible = true;
                }
                else if (interlock == -2)
                {                    
                    uiList_pgbSpinner[arg - 1].Visibility = Visibility.Collapsed;
                    uiList_StopBtn[arg - 1].Visibility = Visibility.Collapsed;
                    uiList_listView[arg - 1].Items.Refresh();
                    uiList_pgbTxt[arg - 1].Text = "중단됨";
                    uiList_listView[arg - 1].IsHitTestVisible = true;

                    if (src_flag == 3 && tar_flag == 1 && ETL_stop_flag == 1) ETL_stop_flag = 0;
                    else if (src_flag == 3 && tar_flag == 2 & ETL_stop_flag2 == 1) ETL_stop_flag2 = 0;
                    else if (src_flag == 4 && tar_flag == 1 & ETL_stop_flag3 == 1) ETL_stop_flag3 = 0;
                    else if (src_flag == 4 && tar_flag == 2 & ETL_stop_flag4 == 1) ETL_stop_flag4 = 0;
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

                if (arg == 1)
                {
                    var list_items = from a in source_Local_path_all1
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
                else if (arg == 2)
                {
                    var list_items = from a in source_Local_path_all2
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

        //EQP To Local
        private (int, string, int) EQPToLocalFileDir(string src_item, int level, BackgroundWorker bw_main, int src_flag, int tar_flag, string src_path, string tar_path, int cnt, long result_cnt, int case_flag, string raw_path, int level_flag)
        {   

            if (cnt == -1) return (-1, "", -1);

            if (src_flag == 3 && tar_flag == 1 && ETL_stop_flag == 1) return (-2, "", -1);
            else if (src_flag == 3 && tar_flag == 2 & ETL_stop_flag2 == 1) return (-2, "", -1);
            else if (src_flag == 4 && tar_flag == 1 & ETL_stop_flag3 == 1) return (-2, "", -1);
            else if (src_flag == 4 && tar_flag == 2 & ETL_stop_flag4 == 1) return (-2, "", -1);

            string id = "";
            string pw = "";

            if (src_flag == 3)
            {
                id = source_ftp_info1["id"];
                pw = source_ftp_info1["pw"];
            }
            else if (src_flag == 4)
            {
                id = source_ftp_info2["id"];
                pw = source_ftp_info2["pw"];
            }

            //경로 인터락
            DirectoryInfo di_temp = new DirectoryInfo(tar_path);
            if ((!Global_FunSet.IsExistDirectory(src_item, id, pw) && !Global_FunSet.IsExistFile(src_item, id, pw)) || (!di_temp.Exists && !File.Exists(tar_path)))
            {                
                return (-1, "", -1);
            }

            string t_path = "";

            if (cnt == 0)
            {
                raw_path = src_path;
                level_flag = 0;
                case_flag = 0;
            }            
            level_flag += level;
            try
            {
                if (!Global_FunSet.IsExistFile(src_item, id, pw))
                {
                    //폴더라면 , 재귀
                    DirectoryInfo di = null;

                    string real_path = src_item.Substring(raw_path.Length + 1, src_item.Length - raw_path.Length - 1).Replace("/", "\\");
                    string real_filename = src_item.Substring(src_item.LastIndexOf("/") + 1, src_item.Length - src_item.LastIndexOf("/") - 1);

                    if (real_path == real_filename)
                    {
                        Dispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate
                        {
                            t_path = tar_path + "\\" + real_filename;
                        }));
                    }
                    else
                    {
                        Dispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate
                        {
                            t_path = tar_path + "\\" + real_path;
                        }));
                    }

                    di = new DirectoryInfo(t_path);
                    if (di.Exists == false)         //폴더 생성
                    {
                        di.Create();
                    }
                    else
                    {
                        Dispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate
                        {
                            NewOverRideChk newOverRideChk = new NewOverRideChk(t_path.Substring(t_path.LastIndexOf("\\") + 1, t_path.Length - t_path.LastIndexOf("\\") - 1));

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
                            //di = new DirectoryInfo(uiList_textbox[target_set_flag - 1].Text + "\\" + s.Substring(s.LastIndexOf("\\") + 1, s.Length - s.LastIndexOf("\\") - 1));
                            //di = new DirectoryInfo(t_path);                        
                        }
                        else
                        {
                            //모두 건너뛰기
                        }
                    }
                    if (tar_flag == 1 && level_flag == 0)
                    {
                        var list_item = source_Local_path_all1.SingleOrDefault(x => x.local_all_filename == t_path.Substring(t_path.LastIndexOf("\\") + 1, t_path.Length - t_path.LastIndexOf("\\") - 1));
                        if (list_item == null)
                        {
                            long raw_fileSize = 0;
                            string fileExetension = "폴더";
                            Dispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate
                            {
                                source_Local_path_all1.Insert(0, new Local_path_all1()
                                {
                                    local_all_filename = t_path.Substring(t_path.LastIndexOf("\\") + 1, t_path.Length - t_path.LastIndexOf("\\") - 1),
                                    local_all_date = Directory.GetLastWriteTime(t_path),
                                    local_all_size = Global_FunSet.ReturnFileSizeStr(raw_fileSize),
                                    local_all_extension = fileExetension,
                                    local_all_src = Global_FunSet.ReturnIconPathStr(fileExetension),
                                    IsNew = true
                                });

                            }));

                        }

                    }
                    else if (tar_flag == 2 && level_flag == 0)
                    {
                        var list_item = source_Local_path_all2.SingleOrDefault(x => x.local_all_filename == t_path.Substring(t_path.LastIndexOf("\\") + 1, t_path.Length - t_path.LastIndexOf("\\") - 1));
                        if (list_item == null)
                        {
                            long raw_fileSize = 0;
                            string fileExetension = "폴더";
                            Dispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate
                            {
                                source_Local_path_all2.Insert(0, new Local_path_all2()
                                {
                                    local_all_filename = t_path.Substring(t_path.LastIndexOf("\\") + 1, t_path.Length - t_path.LastIndexOf("\\") - 1),
                                    local_all_date = Directory.GetLastWriteTime(t_path),
                                    local_all_size = Global_FunSet.ReturnFileSizeStr(raw_fileSize),
                                    local_all_extension = fileExetension,
                                    local_all_src = Global_FunSet.ReturnIconPathStr(fileExetension),
                                    IsNew = true
                                });
                            }));
                        }
                    }
                    cnt++;
                    double ratio = ((double)cnt / (double)result_cnt) * 100.0;
                    bw_main.ReportProgress((int)ratio, new Tuple<int, int>(src_flag, tar_flag));
                    //Thread.Sleep(500);

                    //하위 디렉토리일 경우
                    FtpWebRequest ftpReq = (FtpWebRequest)WebRequest.Create(new Uri(src_item.Replace("#", "%23")));
                    if (src_flag == 3) ftpReq.Credentials = new NetworkCredential(source_ftp_info1["id"], source_ftp_info1["pw"]);
                    else if (src_flag == 4) ftpReq.Credentials = new NetworkCredential(source_ftp_info2["id"], source_ftp_info2["pw"]);
                    ftpReq.Method = WebRequestMethods.Ftp.ListDirectory;
                    ftpReq.Timeout = Global_FunSet.ftpTimeout;
                    FtpWebResponse resFtp = (FtpWebResponse)ftpReq.GetResponse();
                    StreamReader reader;
                    reader = new StreamReader(resFtp.GetResponseStream());

                    string strData;
                    strData = reader.ReadToEnd();

                    string[] tmpPath = strData.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                    for (int i = 0; i < tmpPath.Length; i++)
                    {
                        Dispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate
                        {
                            //tmpPath[i] = this.line_set1_eqp.Text + "/" + tmpPath[i];
                            tmpPath[i] = src_item + tmpPath[i].Substring(tmpPath[i].LastIndexOf("/"), tmpPath[i].Length - tmpPath[i].LastIndexOf("/"));
                        }));
                    }
                    foreach (string s in tmpPath)
                    {
                        (cnt, raw_path, case_flag) = EQPToLocalFileDir(s, 1, bw_main, src_flag, tar_flag, src_path, tar_path, cnt, result_cnt, case_flag, raw_path, level_flag); // 폴더를 만나면 하위로 계속 탐색을 진행
                        if (level_flag != 0) level_flag -= 1;
                    }

                    //하위 파일일 경우
                    //string[] tmpFiles = Directory.GetFiles(_path);
                    //foreach (string s in tmpFiles)
                    //{
                    //    EQPToLocalFileDir(s, 1); // 파일을 만나도 동일한 함수로 실행 가능함                    
                    //    if (level_flag != 0) level_flag -= 1;
                    //}

                }
                else
                {
                    //파일이라면                
                    string real_path = src_item.Substring(raw_path.Length + 1, src_item.Length - raw_path.Length - 1).Replace("/", "\\");
                    string real_filename = src_item.Substring(src_item.LastIndexOf("/") + 1, src_item.Length - src_item.LastIndexOf("/") - 1);

                    if (real_path == real_filename)
                    {
                        Dispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate
                        {
                            t_path = tar_path + "\\" + real_filename;
                        }));
                    }
                    else
                    {
                        Dispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate
                        {
                            t_path = tar_path + "\\" + real_path;
                        }));
                    }

                    //이미 존재하는 이름의 파일이라면
                    FileInfo fileInfo = new FileInfo(t_path);
                    if (fileInfo.Exists)
                    {
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
                            var request = (FtpWebRequest)WebRequest.Create(new Uri(src_item.Replace("#", "%23")));
                            if (src_flag == 3) request.Credentials = new NetworkCredential(source_ftp_info1["id"], source_ftp_info1["pw"]);
                            else if (src_flag == 4) request.Credentials = new NetworkCredential(source_ftp_info2["id"], source_ftp_info2["pw"]);
                            request.Method = WebRequestMethods.Ftp.DownloadFile;
                            request.Timeout = Global_FunSet.ftpTimeout;
                            FtpWebResponse resFtp = (FtpWebResponse)request.GetResponse();

                            Stream sourceStream = resFtp.GetResponseStream();
                            FileStream targetFileStream = new FileStream(t_path, FileMode.Create, FileAccess.Write);
                            byte[] bufferByteArray = new byte[1024];

                            while (true)
                            {
                                int byteCount = sourceStream.Read(bufferByteArray, 0, bufferByteArray.Length);
                                if (byteCount == 0)
                                {
                                    break;
                                }
                                targetFileStream.Write(bufferByteArray, 0, byteCount);
                            }
                            targetFileStream.Close();
                            sourceStream.Close();
                        }
                        else
                        {
                            //건너뛰기
                        }

                    }
                    else
                    {
                        var request = (FtpWebRequest)WebRequest.Create(new Uri(src_item.Replace("#", "%23")));
                        if (src_flag == 3) request.Credentials = new NetworkCredential(source_ftp_info1["id"], source_ftp_info1["pw"]);
                        else if (src_flag == 4) request.Credentials = new NetworkCredential(source_ftp_info2["id"], source_ftp_info2["pw"]);
                        request.Method = WebRequestMethods.Ftp.DownloadFile;
                        request.Timeout = Global_FunSet.ftpTimeout;
                        FtpWebResponse resFtp = (FtpWebResponse)request.GetResponse();

                        Stream sourceStream = resFtp.GetResponseStream();
                        FileStream targetFileStream = new FileStream(t_path, FileMode.Create, FileAccess.Write);
                        byte[] bufferByteArray = new byte[1024];

                        while (true)
                        {
                            int byteCount = sourceStream.Read(bufferByteArray, 0, bufferByteArray.Length);
                            if (byteCount == 0)
                            {
                                break;
                            }
                            targetFileStream.Write(bufferByteArray, 0, byteCount);
                        }
                        targetFileStream.Close();
                        sourceStream.Close();
                    }

                    if (tar_flag == 1 && level_flag == 0)
                    {
                        var list_item = source_Local_path_all1.SingleOrDefault(x => x.local_all_filename == real_filename);
                        if (list_item == null)
                        {
                            long raw_fileSize = new FileInfo(t_path).Length;
                            string fileExetension = new FileInfo(t_path).Extension;
                            Dispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate
                            {
                                source_Local_path_all1.Insert(0, new Local_path_all1()
                                {
                                    local_all_filename = real_filename,
                                    local_all_date = DateTime.Now,
                                    local_all_size = Global_FunSet.ReturnFileSizeStr(raw_fileSize),
                                    local_all_extension = fileExetension,
                                    local_all_src = Global_FunSet.ReturnIconPathStr(fileExetension),
                                    IsNew = true

                                });
                            }));

                        }
                    }
                    else if (tar_flag == 2 && level_flag == 0)
                    {
                        var list_item = source_Local_path_all2.SingleOrDefault(x => x.local_all_filename == real_filename);
                        if (list_item == null)
                        {
                            long raw_fileSize = new FileInfo(t_path).Length;
                            string fileExetension = new FileInfo(t_path).Extension;
                            Dispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate
                            {
                                source_Local_path_all2.Insert(0, new Local_path_all2()
                                {
                                    local_all_filename = real_filename,
                                    local_all_date = DateTime.Now,
                                    local_all_size = Global_FunSet.ReturnFileSizeStr(raw_fileSize),
                                    local_all_extension = fileExetension,
                                    local_all_src = Global_FunSet.ReturnIconPathStr(fileExetension),
                                    IsNew = true
                                });
                            }));
                        }
                    }
                    cnt++;
                    double ratio = ((double)cnt / (double)result_cnt) * 100.0;
                    bw_main.ReportProgress((int)ratio, new Tuple<int, int>(src_flag, tar_flag));
                    //Thread.Sleep(500);    

                }
                return (cnt, raw_path, case_flag);
            }
            catch (System.UnauthorizedAccessException)
            {
                MessageBox.Show("액세스 거부");
                return (-1, "", -1);
            }
            
            
        }
    }
}
