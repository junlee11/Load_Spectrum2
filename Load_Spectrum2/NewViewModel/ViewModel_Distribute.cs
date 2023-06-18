using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;

namespace Load_Spectrum2.NewViewModel
{
    public class ViewModel_Distribute : INotifyPropertyChanged
    {        
        //global function set
        public static Global_FunSet gfun;
        
        public static List<dynamic> source_EQP_list3;
        public static List<dynamic> source_EQP_list4;
        public static List<dynamic> source_EQP_list5;
        public static Dictionary<string, string> source_EQP_dict = new Dictionary<string, string>();
        List<List<NewModel.Model>> uiList_source_all = new List<List<NewModel.Model>>();
        List<List<NewModel.Model>> uiList_source_EQPID = new List<List<NewModel.Model>>();

        public ViewModel_Distribute()
        {            
            gfun = new Global_FunSet();

            //DataTable dt_eqp_info = new DataTable();
            //dt_eqp_info = gfun.GetCSVData("Load_Spectrum2.Resources.Setting_EQP.csv", 2);
            
            source_EQP_Info_Line3 = new List<NewModel.Model>();
            source_EQP_Info_Line4 = new List<NewModel.Model>();
            source_EQP_Info_Line5 = new List<NewModel.Model>();
            source_EQP_Info_EQPMODEL3 = new List<NewModel.Model>();
            source_EQP_Info_EQPMODEL4 = new List<NewModel.Model>();
            source_EQP_Info_EQPMODEL5 = new List<NewModel.Model>();
            source_EQP_Info_EQPTYPE3 = new List<NewModel.Model>();
            source_EQP_Info_EQPTYPE4 = new List<NewModel.Model>();
            source_EQP_Info_EQPTYPE5 = new List<NewModel.Model>();
            source_EQP_Info_ZONE3 = new List<NewModel.Model>();
            source_EQP_Info_ZONE4 = new List<NewModel.Model>();
            source_EQP_Info_ZONE5 = new List<NewModel.Model>();
            source_EQP_Info_EQPID3 = new List<NewModel.Model>();
            source_EQP_Info_EQPID4 = new List<NewModel.Model>();
            source_EQP_Info_EQPID5 = new List<NewModel.Model>();
            source_EQP_Info_all3 = new List<NewModel.Model>();
            source_EQP_Info_all4 = new List<NewModel.Model>();
            source_EQP_Info_all5 = new List<NewModel.Model>();
            source_EQP_list3 = new List<dynamic> { source_EQP_Info_Line3, source_EQP_Info_EQPTYPE3, source_EQP_Info_EQPMODEL3, source_EQP_Info_ZONE3, source_EQP_Info_EQPID3 };
            source_EQP_list4 = new List<dynamic> { source_EQP_Info_Line4, source_EQP_Info_EQPTYPE4, source_EQP_Info_EQPMODEL4, source_EQP_Info_ZONE4, source_EQP_Info_EQPID4 };
            source_EQP_list5 = new List<dynamic> { source_EQP_Info_Line5, source_EQP_Info_EQPTYPE5, source_EQP_Info_EQPMODEL5, source_EQP_Info_ZONE5, source_EQP_Info_EQPID5 };

            uiList_source_all.Add(source_EQP_Info_all3);
            uiList_source_all.Add(source_EQP_Info_all4);
            uiList_source_all.Add(source_EQP_Info_all5);
            uiList_source_EQPID.Add(source_EQP_Info_EQPID3);
            uiList_source_EQPID.Add(source_EQP_Info_EQPID4);
            uiList_source_EQPID.Add(source_EQP_Info_EQPID5);

            source_drive = new List<NewModel.Model>();
            source_RNR_ItemLayer = new List<NewModel.Model>();

            for (int i = 0; i < source_EQP_list3.Count(); i++)
            {
                Distinct_ListClass(gfun.dt_eqp_info, i, 3);
                Distinct_ListClass(gfun.dt_eqp_info, i, 4);
                Distinct_ListClass(gfun.dt_eqp_info, i, 5);
            }

            source_drive.Add(new NewModel.Model() { strdrive = "C:\\" });
            source_drive.Add(new NewModel.Model() { strdrive = "D:\\" });
            source_drive.Add(new NewModel.Model() { strdrive = "E:\\" });
        }

        public List<object> LTE_AddListEQPID(IList items, ListView lv_src, ListView lv_tar, int set_flag)
        {
            //EQPID에서 빼기
            List<object> result_list = new List<object>();
                        
            foreach (var item in items)
            {
                //source_local_path1.Add(new Local_path1() { local_comment = row["Comment"].ToString(), local_path = row["Path"].ToString() });
                var cast_item = (NewModel.Model)item;                
                                
                if (set_flag == 3)
                {
                    uiList_source_EQPID[set_flag - 3].RemoveAll(x => x.strEQPID_Set3 == cast_item.strEQPID_Set3);
                    uiList_source_all[set_flag - 3].Add(new NewModel.Model() { strchkEQPID_Set3 = cast_item.strEQPID_Set3, strStatus_Set3 = "Refresh 대기", strRefresh_Set3 = " - ", strPassFail_Set3 = " - " });                    
                }
                else if (set_flag == 4)
                {
                    uiList_source_EQPID[set_flag - 3].RemoveAll(x => x.strEQPID_Set4 == cast_item.strEQPID_Set4);
                    uiList_source_all[set_flag - 3].Add(new NewModel.Model() { strchkEQPID_Set4 = cast_item.strEQPID_Set4, strStatus_Set4 = "Refresh 대기", strRefresh_Set4 = " - ", strPassFail_Set4 = " - ", strNum_Set4 = "1", strHit_Set4 = true });
                    
                }
                else if (set_flag == 5)
                {
                    uiList_source_EQPID[set_flag - 3].RemoveAll(x => x.strEQPID_Set5 == cast_item.strEQPID_Set5);
                    uiList_source_all[set_flag - 3].Add(new NewModel.Model() { strchkEQPID_Set5 = cast_item.strEQPID_Set5, strStatus_Set5 = "Refresh 대기", strRefresh_Set5 = " - ", strPassFail_Set5 = " - " });
                }
            }
            result_list.Add(uiList_source_EQPID[set_flag - 3]);
            result_list.Add(uiList_source_all[set_flag - 3]);

            return result_list;            
        }

        public List<object> LTE_SubListEQPID(IList items, ListView lv_src, ListView lv_tar, int set_flag)
        {
            //EQPID에서 빼기
            List<object> result_list = new List<object>();

            foreach (var item in items)
            {                
                var cast_item = (NewModel.Model)item;
                if (set_flag == 3)
                {
                    uiList_source_all[set_flag - 3].RemoveAll(x => x.strchkEQPID_Set3 == cast_item.strchkEQPID_Set3);
                    uiList_source_EQPID[set_flag - 3].Add(new NewModel.Model() { strEQPID_Set3 = cast_item.strchkEQPID_Set3 });
                }
                else if (set_flag == 4)
                {
                    uiList_source_all[set_flag - 3].RemoveAll(x => x.strchkEQPID_Set4 == cast_item.strchkEQPID_Set4);
                    uiList_source_EQPID[set_flag - 3].Add(new NewModel.Model() { strEQPID_Set4 = cast_item.strchkEQPID_Set4 });
                }
                else
                {
                    uiList_source_all[set_flag - 3].RemoveAll(x => x.strchkEQPID_Set5 == cast_item.strchkEQPID_Set5);
                    uiList_source_EQPID[set_flag - 3].Add(new NewModel.Model() { strEQPID_Set5 = cast_item.strchkEQPID_Set5 });
                }

            }

            result_list.Add(uiList_source_EQPID[set_flag - 3]);
            result_list.Add(uiList_source_all[set_flag - 3]);

            return result_list;
        }

        //Refresh
        public List<NewModel.Model> LTE_Refresh(NewModel.Model item, string txt_path, string id, string pw, int ftp_chk_flag, int set_flag)
        {
            string eqp = "";
            if (set_flag == 3) eqp = item.strchkEQPID_Set3;
            else if (set_flag == 4) eqp = item.strchkEQPID_Set4;
            else if (set_flag == 5) eqp = item.strchkEQPID_Set5;

            //날짜무시방법 고민
            //gfun 이용해서 src_path로부터 sim_path를 뽑아낸다.
            //sim_path 있으면 유사파일 존재라고 코멘트를 띄운다.

            string eqp_tar_path;
            string ftp_uri;
            string ip = gfun.source_EQP_dict[eqp];

            eqp_tar_path = String.Format("ftp://{0}:21/{1}", ip, txt_path);
            ftp_uri = string.Format("ftp://{0}:21", ip);

            //접속가능한지부터 검사
            //폴더가 있는지부터 검사                
            if (!Global_FunSet.IsFTPConnect(ftp_uri, id, pw))
            {
                //접속 불가                    
                if (set_flag == 3)
                {
                    var list_item = source_EQP_Info_all3.SingleOrDefault(x => x.strchkEQPID_Set3 == item.strchkEQPID_Set3);
                    list_item.strStatus_Set3 = "FTP 접속불가";
                    list_item.strRefresh_Set3 = "-";
                }
                else if (set_flag == 4)
                {
                    var list_item = source_EQP_Info_all4.SingleOrDefault(x => x.strchkEQPID_Set4 == item.strchkEQPID_Set4);
                    list_item.strStatus_Set4 = "FTP 접속불가";
                    list_item.strRefresh_Set4 = "-";
                    list_item.strHit_Set4 = false;
                    list_item.strColor_Set4 = new SolidColorBrush(Colors.Gray);
                }
                else if (set_flag == 5)
                {
                    var list_item = source_EQP_Info_all5.SingleOrDefault(x => x.strchkEQPID_Set5 == item.strchkEQPID_Set5);
                    list_item.strStatus_Set5 = "FTP 접속불가";
                    list_item.strRefresh_Set5 = "-";
                }

            }
            //ftp 체크만 하는 경우
            else if (ftp_chk_flag == 1)
            {
                if (set_flag == 3)
                {
                    var list_item = source_EQP_Info_all3.SingleOrDefault(x => x.strchkEQPID_Set3 == item.strchkEQPID_Set3);
                    list_item.strStatus_Set3 = "FTP 양호";
                    list_item.strRefresh_Set3 = "-";
                }
                else if (set_flag == 4)
                {
                    var list_item = source_EQP_Info_all4.SingleOrDefault(x => x.strchkEQPID_Set4 == item.strchkEQPID_Set4);
                    list_item.strStatus_Set4 = "FTP 양호";
                    list_item.strRefresh_Set4 = "-";
                    list_item.strHit_Set4 = false;
                    list_item.strColor_Set4 = new SolidColorBrush(Colors.Gray);
                }
                else if (set_flag == 5)
                {
                    var list_item = source_EQP_Info_all5.SingleOrDefault(x => x.strchkEQPID_Set5 == item.strchkEQPID_Set5);
                    list_item.strStatus_Set5 = "FTP 양호";
                    list_item.strRefresh_Set5 = "-";
                }

            }

            else if (set_flag == 4 && Global_FunSet.IsExistFile(eqp_tar_path, id, pw))
            {
                //ETL 이면서 파일일 때                

                var list_item = source_EQP_Info_all4.SingleOrDefault(x => x.strchkEQPID_Set4 == item.strchkEQPID_Set4);

                int path_num = Int32.Parse(list_item.strNum_Set4);
                List<string> path_list = Global_FunSet.ReturnSimilarPath(txt_path, ip, id, pw, path_num);
                if (path_list == null) return null;

                if (path_list.Count == 1 && path_list[0] == txt_path)
                {
                    //한개 파일만 일치
                    list_item.strStatus_Set4 = string.Format("다운 대기 {0}/{1}", path_list.Count, path_num);
                    list_item.strRefresh_Set4 = "파일 있음";
                }
                else if (path_list.Count > 1 && Global_FunSet.chk_list_Allin(path_list, txt_path))
                {
                    //여러개중 하나이상 일치
                    list_item.strStatus_Set4 = string.Format("다운 대기 {0}/{1}", path_list.Count, path_num);
                    list_item.strRefresh_Set4 = "파일 있음";
                }
                else if (path_list.Count > 1 && Global_FunSet.chk_list_in(path_list, txt_path))
                {
                    //여러개중 하나이상 일치
                    list_item.strStatus_Set4 = string.Format("다운 대기 {0}/{1}", path_list.Count, path_num);
                    list_item.strRefresh_Set4 = "유사파일 있음(원본포함)";
                }                
                else if (path_list.Count == 0)
                {
                    //일치 파일 없음
                    list_item.strStatus_Set4 = string.Format("다운 예외 {0}/{1}", path_list.Count, path_num);
                    list_item.strRefresh_Set4 = "파일 없음";
                }
                
                list_item.strHit_Set4 = false;
                list_item.strColor_Set4 = new SolidColorBrush(Colors.Gray);
                list_item.strColor_Set4.Freeze();

                for (int i=0; i<path_num; i++)
                {
                    Global_FunSet.ETL_Sim_pathDic.Add(list_item.strchkEQPID_Set4 + "(" + String.Format("{0}", i+1) + ")", txt_path);
                }                
            }

            else if (Global_FunSet.IsExistDirectory(eqp_tar_path, id, pw))
            {
                //폴더 있음
                if (set_flag == 3)
                {
                    var list_item = source_EQP_Info_all3.SingleOrDefault(x => x.strchkEQPID_Set3 == item.strchkEQPID_Set3);
                    list_item.strStatus_Set3 = "업로드 대기";
                    list_item.strRefresh_Set3 = "폴더 있음";
                }
                else if (set_flag == 4)
                {
                    var list_item = source_EQP_Info_all4.SingleOrDefault(x => x.strchkEQPID_Set4 == item.strchkEQPID_Set4);                                        
                    int path_num = Int32.Parse(list_item.strNum_Set4);

                    List<string> path_list = Global_FunSet.ReturnSimilarPath(txt_path, ip, id, pw, path_num);
                    if (path_list == null) return null;

                    if (path_list.Count == 1 && path_list[0] == txt_path)
                    {
                        //한개 폴더만 일치
                        list_item.strStatus_Set4 = string.Format("다운 대기 {0}/{1}", path_list.Count, path_num);
                        list_item.strRefresh_Set4 = "폴더 있음";
                    }
                    else if (path_list.Count > 1 && Global_FunSet.chk_list_Allin(path_list, txt_path))
                    {
                        //여러개중 하나이상 일치
                        list_item.strStatus_Set4 = string.Format("다운 대기 {0}/{1}", path_list.Count, path_num);
                        list_item.strRefresh_Set4 = "폴더 있음";
                    }
                    else if (path_list.Count > 1 && Global_FunSet.chk_list_in(path_list, txt_path))
                    {
                        //여러개중 하나이상 일치
                        list_item.strStatus_Set4 = string.Format("다운 대기 {0}/{1}", path_list.Count, path_num);
                        list_item.strRefresh_Set4 = "유사폴더 있음(원본포함)";
                    }                    
                    else if (path_list.Count == 0)
                    {
                        //일치 파일 없음
                        list_item.strStatus_Set4 = string.Format("다운 예외 {0}/{1}", path_list.Count, path_num);
                        list_item.strRefresh_Set4 = "폴더 없음";
                    }

                    list_item.strHit_Set4 = false;
                    list_item.strColor_Set4 = new SolidColorBrush(Colors.Gray);
                    list_item.strColor_Set4.Freeze();

                    for (int i = 0; i < path_num; i++)
                    {
                        Global_FunSet.ETL_Sim_pathDic.Add(list_item.strchkEQPID_Set4 + "(" + String.Format("{0}", i + 1) + ")", path_list[i]);
                    }
                }
                else if (set_flag == 5)
                {
                    var list_item = source_EQP_Info_all5.SingleOrDefault(x => x.strchkEQPID_Set5 == item.strchkEQPID_Set5);
                    list_item.strStatus_Set5 = "다운로드 대기";
                    list_item.strRefresh_Set5 = "폴더 있음";
                }
            }
            else
            {
                //접속 가능하나 폴더 없음
                if (set_flag == 3)
                {
                    var list_item = source_EQP_Info_all3.SingleOrDefault(x => x.strchkEQPID_Set3 == item.strchkEQPID_Set3);

                    Global_FunSet.MakeFTPFolder(ftp_uri, id, pw, txt_path);

                    list_item.strStatus_Set3 = "다운로드 대기";
                    list_item.strRefresh_Set3 = "폴더 생성됨";
                }
                else if (set_flag == 4)
                {                    
                    var list_item = source_EQP_Info_all4.SingleOrDefault(x => x.strchkEQPID_Set4 == item.strchkEQPID_Set4);
                    int path_num = Int32.Parse(list_item.strNum_Set4);

                    List<string> path_list = Global_FunSet.ReturnSimilarPath(txt_path, ip, id, pw, path_num);
                    if (path_list == null)
                    {
                        //파일(폴더) 없음
                        list_item.strStatus_Set4 = "다운 예외";
                        list_item.strRefresh_Set4 = "파일(폴더) 없음";
                    }
                    else if (path_list.Count > 0)
                    {
                        //일치하는 파일(폴더) 있음
                        list_item.strStatus_Set4 = string.Format("다운 대기 {0}/{1}", path_list.Count, path_num);
                        list_item.strRefresh_Set4 = "유사파일(폴더) 있음";
                    }                                        
                    else if (path_list.Count == 0)
                    {
                        //일치 파일 없음
                        list_item.strStatus_Set4 = string.Format("다운 예외 {0}/{1}", path_list.Count, path_num);
                        list_item.strRefresh_Set4 = "유사파일(폴더) 없음";
                    }

                    list_item.strHit_Set4 = false;
                    list_item.strColor_Set4 = new SolidColorBrush(Colors.Gray);
                    list_item.strColor_Set4.Freeze();

                    if (path_list != null)
                    {
                        for (int i = 0; i < path_num; i++)
                        {
                            Global_FunSet.ETL_Sim_pathDic.Add(list_item.strchkEQPID_Set4 + "(" + String.Format("{0}", i + 1) + ")", path_list[i]);
                        }
                    }
                    
                }
                else if (set_flag == 5)
                {
                    var list_item = source_EQP_Info_all5.SingleOrDefault(x => x.strchkEQPID_Set5 == item.strchkEQPID_Set5);

                    Global_FunSet.MakeFTPFolder(ftp_uri, id, pw, txt_path);

                    list_item.strStatus_Set5 = "다운로드 대기";
                    list_item.strRefresh_Set5 = "폴더 생성됨";
                }

            }
            
            return uiList_source_all[set_flag - 3];            
        }

        public void pgb_val_ini(IList items, int set_flag)
        {
            foreach (var item in items)
            {
                var cast_item = (NewModel.Model)item;
                if (set_flag == 3)
                {
                    cast_item.strPgbValue_Set3 = 0;
                    cast_item.strPgbTxt_Set3 = "0%";
                    cast_item.strPassFail_Set3 = "-";
                }
                else if (set_flag == 4)
                {
                    cast_item.strPgbValue_Set4 = 0;
                    cast_item.strPgbTxt_Set4 = "0%";
                    cast_item.strPassFail_Set4 = "-";
                }
                else if (set_flag == 5)
                {
                    cast_item.strPgbValue_Set5 = 0;
                    cast_item.strPgbTxt_Set5 = "0%";
                    cast_item.strPassFail_Set5 = "-";
                }

            }
        }


        //업로드
        //source, raw_path, result_cnt
        public (List<NewModel.Model>, int, int, string, string, int) LTE_Upload(NewModel.Model item, string src_path, string tar_path, string id, string pw, int cnt, long result_cnt, string raw_path, int fail_flag, int ete_flag)
        {
            //하나의 아이템에서 경로 다운
            string t_path = "";
            string real_tar_path = "";
            string eqp = "";

            if (cnt == 0) raw_path = src_path.Substring(0, src_path.LastIndexOf("\\"));

            if (ete_flag == 0) eqp = item.strchkEQPID_Set3;
            else eqp = item.strchkEQPID_Set5;

            string ftp_uri;

            real_tar_path = String.Format("ftp://{0}:21/{1}/", gfun.source_EQP_dict[eqp], tar_path);
            ftp_uri = string.Format("ftp://{0}:21", gfun.source_EQP_dict[eqp]);

            

            //폴더일때
            FileAttributes chkAtt = File.GetAttributes(src_path);
            if ((chkAtt & FileAttributes.Directory) == FileAttributes.Directory)
            {
                string real_path = src_path.Substring(raw_path.Length + 1, src_path.Length - raw_path.Length - 1).Replace("\\", "/");
                string real_filename = src_path.Substring(src_path.LastIndexOf("\\") + 1, src_path.Length - src_path.LastIndexOf("\\") - 1);

                if (real_path == real_filename)
                {
                    t_path = real_tar_path + real_filename;
                }
                else
                {
                    t_path = real_tar_path + real_path;
                }

                if (Global_FunSet.IsExistDirectory(t_path, id, pw))
                {
                    //폴더 이미 있음, 복사본으로 생성
                    t_path = t_path + " - 복사본";         
                }

                try
                {
                    //폴더 생성
                    var request = (FtpWebRequest)WebRequest.Create(new Uri(t_path.Replace("#", "%23")));
                    request.Credentials = new NetworkCredential(id, pw);
                    request.UseBinary = true;
                    request.UsePassive = true;
                    request.Method = WebRequestMethods.Ftp.MakeDirectory;
                    request.Timeout = Global_FunSet.ftpTimeout;
                    FtpWebResponse resFtp = (FtpWebResponse)request.GetResponse();
                    resFtp.Close();
                }
                catch
                {
                    fail_flag++;            
                    if (ete_flag == 0)
                    {
                        item.strPassFail_Set3 = String.Format("{0}개 실패", fail_flag);
                    }
                    else
                    {
                        item.strPassFail_Set5 = String.Format("{0}개 실패", fail_flag);
                    }
                    
                }

                //재귀함수
                cnt++;
                double ratio = ((double)cnt / (double)result_cnt) * 100.0;                
                int int_raio = (int)ratio;
                if (ete_flag == 0)
                {
                    item.strPgbValue_Set3 = int_raio;
                    item.strPgbTxt_Set3 = int_raio.ToString() + " %";
                    item.strStatus_Set3 = "업로드중";
                }
                else
                {
                    item.strPgbValue_Set5 = int_raio;
                    item.strPgbTxt_Set5 = int_raio.ToString() + " %";
                    item.strStatus_Set5 = "업로드중";
                }
                

                //하위 디렉토리 탐색
                string[] tmpPath = Directory.GetDirectories(src_path);
                foreach (string s in tmpPath)
                {
                    if (ete_flag == 0) (source_EQP_Info_all3, cnt, fail_flag, raw_path, tar_path, ete_flag) = LTE_Upload(item, s, tar_path, id, pw, cnt, result_cnt, raw_path, fail_flag, ete_flag);
                    else (source_EQP_Info_all5, cnt, fail_flag, raw_path, tar_path, ete_flag) = LTE_Upload(item, s, tar_path, id, pw, cnt, result_cnt, raw_path, fail_flag, ete_flag);

                }

                //하위 파일일 경우
                string[] tmpFiles = Directory.GetFiles(src_path);
                foreach (string s in tmpFiles)
                {
                    if (ete_flag == 0) (source_EQP_Info_all3, cnt, fail_flag, raw_path, tar_path, ete_flag) = LTE_Upload(item, s, tar_path, id, pw, cnt, result_cnt, raw_path, fail_flag, ete_flag);
                    else (source_EQP_Info_all5, cnt, fail_flag, raw_path, tar_path, ete_flag) = LTE_Upload(item, s, tar_path, id, pw, cnt, result_cnt, raw_path, fail_flag, ete_flag);

                }
            }
            //파일일때
            else
            {
                string real_path = src_path.Substring(raw_path.Length + 1, src_path.Length - raw_path.Length - 1).Replace("\\", "/");
                string real_filename = src_path.Substring(src_path.LastIndexOf("\\") + 1, src_path.Length - src_path.LastIndexOf("\\") - 1);

                if (real_path == real_filename)
                {
                    t_path = real_tar_path + real_filename;
                }
                else
                {
                    t_path = real_tar_path + real_path;
                }

                try
                {
                    var request = (FtpWebRequest)WebRequest.Create(new Uri(t_path.Replace("#", "%23")));
                    request.Credentials = new NetworkCredential(id, pw);
                    request.Method = WebRequestMethods.Ftp.UploadFile;
                    FileStream sourceFileStream = new FileStream(src_path, FileMode.Open, FileAccess.Read);
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
                    fail_flag++;
                    if (ete_flag == 0) item.strPassFail_Set3 = String.Format("{0}개 실패", fail_flag);
                    else item.strPassFail_Set5 = String.Format("{0}개 실패", fail_flag);

                }
                //재귀함수
                cnt++;
                double ratio = ((double)cnt / (double)result_cnt) * 100.0;
                //Debug.WriteLine(string.Format("cnt:{0}, result_cnt:{1}, ratio:{2}, t_path:{3}", cnt, result_cnt, ratio, t_path));
                int int_raio = (int)ratio;
                if (ete_flag == 0)
                {
                    item.strPgbValue_Set3 = int_raio;
                    item.strPgbTxt_Set3 = int_raio.ToString() + " %";
                    item.strStatus_Set3 = "업로드중";
                }
                else
                {
                    item.strPgbValue_Set5 = int_raio;
                    item.strPgbTxt_Set5 = int_raio.ToString() + " %";
                    item.strStatus_Set5 = "업로드중";
                }
                
            }

            if (cnt == result_cnt && fail_flag == 0)
            {
                if (ete_flag == 0)
                {
                    item.strStatus_Set3 = "완료";
                    item.strPassFail_Set3 = "성공";
                }
                else
                {
                    item.strStatus_Set5 = "완료";
                    item.strPassFail_Set5 = "성공";
                }
                
            }
            if (ete_flag == 0) return (source_EQP_Info_all3, cnt, fail_flag, raw_path, tar_path, ete_flag);
            else return (source_EQP_Info_all5, cnt, fail_flag, raw_path, tar_path, ete_flag);

        }

        //업로드
        //source, raw_path, result_cnt
        public (List<NewModel.Model>, int, int, string, string, long, bool, int, string, int, int) ETL_Download(NewModel.Model item, string src_path, string tar_path, string id, string pw, int cnt, long result_cnt, string raw_path, int fail_flag, bool imgchk, int ete_flag, string ete_eqp, int path_num, int path_num_flag)
        {
            //하나의 아이템에서 경로 다운
            //ete path로 구분
            string t_path = "";
            string real_tar_path = "";

            string eqp = "";
            string raw_eqp = "";
            if (ete_eqp == "eqp")
            {
                if (path_num_flag == 1)
                {
                    eqp = item.strchkEQPID_Set4;
                    raw_eqp = item.strchkEQPID_Set4;
                    real_tar_path = tar_path;
                }
                else
                {
                    eqp = item.strchkEQPID_Set4 + "(" + path_num + ")";
                    raw_eqp = item.strchkEQPID_Set4;
                    real_tar_path = tar_path;
                }                
            }                
            else
            {
                eqp = ete_eqp;
                raw_eqp = ete_eqp;
                real_tar_path = @"C:\Load_Spectrum2\Temp";                
            }
                

            if (cnt == 0)
            {
                src_path = String.Format("ftp://{0}:21/{1}", gfun.source_EQP_dict[raw_eqp], src_path);
                raw_path = src_path.Substring(0, src_path.LastIndexOf("/"));
                string[] src_items = new string[1];
                src_items[0] = src_path;
                result_cnt = Global_FunSet.ReturnEQPFileFolderCount(src_items, id, pw, result_cnt);
            }                

            //string real_last_name = eqp + "_" + tar_path.Substring(tar_path.LastIndexOf("\\") + 1, tar_path.Length - tar_path.LastIndexOf("\\") - 1); //eqp id가 앞에 들어간 파일네임
            //string real_before_path = tar_path.Substring(0, tar_path.LastIndexOf("\\")); //파일 네임전 경로
            //real_tar_path = real_before_path + "\\" + real_last_name;

            //폴더일때
            if (!Global_FunSet.IsExistFile(src_path, id, pw))
            {
                DirectoryInfo di = null;

                string real_path = "";
                string real_filename = "";
                if (ete_flag == 0)
                {
                    real_path = eqp + "_" + src_path.Substring(raw_path.Length + 1, src_path.Length - raw_path.Length - 1).Replace("/", "\\");
                    real_filename = eqp + "_" + src_path.Substring(src_path.LastIndexOf("/") + 1, src_path.Length - src_path.LastIndexOf("/") - 1);
                }
                else
                {
                    real_path = src_path.Substring(raw_path.Length + 1, src_path.Length - raw_path.Length - 1).Replace("/", "\\");
                    real_filename = src_path.Substring(src_path.LastIndexOf("/") + 1, src_path.Length - src_path.LastIndexOf("/") - 1);
                }
                

                if (real_path == real_filename)
                {
                    t_path = real_tar_path + "\\" + real_filename;
                }
                else
                {
                    t_path = real_tar_path + "\\" + real_path;
                }

                //폴더 Similar Path 추가                
                if (cnt == 0 && path_num_flag == 1) Global_FunSet.ETL_Sim_LocalPath.Add(eqp + "(" + path_num + ")", t_path);
                else if (cnt == 0 && path_num_flag > 1) Global_FunSet.ETL_Sim_LocalPath.Add(eqp, t_path);


                di = new DirectoryInfo(t_path);
                if (di.Exists == true)
                {
                    //폴더 이미 있음, 복사본으로 생성
                    t_path = t_path + " - 복사본";
                }

                try
                {
                    di.Create();
                }
                catch
                {
                    if (ete_flag == 0)
                    {
                        fail_flag++;
                        item.strPassFail_Set4 = String.Format("{0}개 실패", fail_flag);
                    }                    
                }

                //재귀함수
                if (ete_flag == 0)
                {                    
                    cnt++;
                    double ratio = (((double)cnt / (double)result_cnt) * 100.0) * (path_num / path_num_flag);
                    int int_raio = (int)ratio;
                    item.strPgbValue_Set4 = int_raio;
                    item.strPgbTxt_Set4 = int_raio.ToString() + " %";
                    item.strStatus_Set4 = "다운로드중";
                }
                else
                {
                    cnt++;
                    double ratio = ((double)cnt / (double)result_cnt) * 100.0;
                    int int_raio = (int)ratio;
                    pgbVal_Set5 = int_raio;
                }


                //하위 디렉토리 탐색 / 파일도 같이 검색       
                FtpWebRequest ftpReq = (FtpWebRequest)WebRequest.Create(new Uri(src_path.Replace("#", "%23")));
                ftpReq.Credentials = new NetworkCredential(id, pw);
                ftpReq.Method = WebRequestMethods.Ftp.ListDirectory;
                ftpReq.Timeout = Global_FunSet.ftpTimeout;
                FtpWebResponse resFtp = (FtpWebResponse)ftpReq.GetResponse();
                StreamReader reader;
                reader = new StreamReader(resFtp.GetResponseStream());

                string strData;
                strData = reader.ReadToEnd();

                resFtp.Close();

                string[] tmpPath = strData.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

                for (int i = 0; i < tmpPath.Length; i++)
                {
                    //tmpPath[i] = this.line_set1_eqp.Text + "/" + tmpPath[i];
                    tmpPath[i] = src_path + tmpPath[i].Substring(tmpPath[i].LastIndexOf("/"), tmpPath[i].Length - tmpPath[i].LastIndexOf("/"));
                }


                foreach (string s in tmpPath)
                {
                    (source_EQP_Info_all4, cnt, fail_flag, raw_path, tar_path, result_cnt, imgchk, ete_flag, ete_eqp, path_num, path_num_flag) = ETL_Download(item, s, tar_path, id, pw, cnt, result_cnt, raw_path, fail_flag, imgchk, ete_flag, ete_eqp, path_num, path_num_flag);
                }

                //하위 파일일 경우
                //위의 동일한 함수로 재귀 가능

            }
            //파일일때
            else
            {
                string real_path = "";
                string real_filename = "";
                if (ete_flag == 0)
                {   
                    real_path = eqp + "_" + src_path.Substring(raw_path.Length + 1, src_path.Length - raw_path.Length - 1).Replace("/", "\\");
                    real_filename = eqp + "_" + src_path.Substring(src_path.LastIndexOf("/") + 1, src_path.Length - src_path.LastIndexOf("/") - 1);
                }
                else
                {
                    real_path = src_path.Substring(raw_path.Length + 1, src_path.Length - raw_path.Length - 1).Replace("/", "\\");
                    real_filename = src_path.Substring(src_path.LastIndexOf("/") + 1, src_path.Length - src_path.LastIndexOf("/") - 1);
                }


                if (real_path == real_filename)
                {
                    t_path = real_tar_path + "\\" + real_filename;
                }
                else
                {
                    t_path = real_tar_path + "\\" + real_path;
                }

                //폴더 Similar Path 추가                
                if (cnt == 0 && path_num_flag == 1) Global_FunSet.ETL_Sim_LocalPath.Add(eqp + "(" + path_num + ")", t_path);
                else if (cnt == 0 && path_num_flag > 1) Global_FunSet.ETL_Sim_LocalPath.Add(eqp, t_path);

                string img_ex = t_path.Substring(t_path.LastIndexOf("."), t_path.Length - t_path.LastIndexOf("."));
                //파일 이미지 여부 체크                
                if (imgchk && Global_FunSet.IsImageExtension(img_ex))
                {
                    //이미지체크에 체크되어 있고 확장자가 이미지일때 skip
                }
                else
                {
                    try
                    {
                        //다운로드
                        var request = (FtpWebRequest)WebRequest.Create(new Uri(src_path.Replace("#", "%23")));
                        request.Credentials = new NetworkCredential(id, pw);
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
                    catch
                    {
                        fail_flag++;
                        if (ete_flag == 0) item.strPassFail_Set4 = String.Format("{0}개 실패", fail_flag);
                        else item.strPassFail_Set5 = String.Format("{0}개 실패", fail_flag);

                    }
                }

                //재귀함수
                if (ete_flag == 0)
                {
                    cnt++;
                    double ratio = (((double)cnt / (double)result_cnt) * 100.0) * (path_num / path_num_flag);
                    int int_raio = (int)ratio;
                    item.strPgbValue_Set4 = int_raio;
                    item.strPgbTxt_Set4 = int_raio.ToString() + " %";
                    item.strStatus_Set4 = "다운로드중";
                }
                else
                {
                    cnt++;
                    double ratio = ((double)cnt / (double)result_cnt) * 100.0;
                    int int_raio = (int)ratio;
                    pgbVal_Set5 = int_raio;
                }
                
            }

            if (cnt == result_cnt && fail_flag == 0 && ete_flag ==0 && path_num_flag == 1)
            {
                item.strStatus_Set4 = "완료";
                item.strPassFail_Set4 = "성공";
            }
            else if (cnt == result_cnt && fail_flag == 0 && ete_flag == 0 && path_num_flag == path_num)
            {
                item.strStatus_Set4 = "완료";
                item.strPassFail_Set4 = "성공";
            }
            else if (cnt == result_cnt && fail_flag == 0 && ete_flag == 1)
            {
                item.strStatus_Set5 = "완료";
                item.strPassFail_Set5 = "성공";
            }
            return (source_EQP_Info_all4, cnt, fail_flag, raw_path, tar_path, result_cnt, imgchk, ete_flag, ete_eqp, path_num, path_num_flag);
        }

        

        //중복되는 함수        
        //private void EQPID_Filter(ListBox cur_list, int idx)


        public object EQPID_Filter(int set_flag, IList items_Line, IList items_EQPTYPE, IList items_EQPMODEL, IList items_ZONE, string str_EQP)
        {
            bool flag_Line;
            bool flag_EQPTYPE;
            bool flag_EQPMODEL;
            bool flag_ZONE;
            bool flag_EQPID;

            List<String> list_Line = new List<String>();
            List<String> list_EQPTYPE = new List<String>();
            List<String> list_EQPMODEL = new List<String>();
            List<String> list_ZONE = new List<String>();

            for (int i = 0; i < 4; i++)
            {
                if (i == 0)
                {
                    foreach (var item in items_Line)
                    {
                        if (set_flag == 3)
                        {
                            //var cast_item = (NewModel.EQP_Info_Line3)item;
                            var cast_item = (NewModel.Model)item;
                            list_Line.Add(cast_item.strLine_Set3);
                        }
                        else if (set_flag == 4)
                        {
                            //var cast_item = (NewModel.EQP_Info_Line4)item;
                            var cast_item = (NewModel.Model)item;
                            list_Line.Add(cast_item.strLine_Set4);
                        }
                        else if (set_flag == 5)
                        {
                            //var cast_item = (NewModel.EQP_Info_Line4)item;
                            var cast_item = (NewModel.Model)item;
                            list_Line.Add(cast_item.strLine_Set5);
                        }
                    }
                }
                else if (i == 1)
                {
                    foreach (var item in items_EQPTYPE)
                    {
                        if (set_flag == 3)
                        {
                            //var cast_item = (NewModel.EQP_Info_EQPTYPE3)item;
                            var cast_item = (NewModel.Model)item;
                            list_EQPTYPE.Add(cast_item.strEQPTYPE_Set3);
                        }
                        else if (set_flag == 4)
                        {
                            //var cast_item = (NewModel.EQP_Info_EQPTYPE4)item;
                            var cast_item = (NewModel.Model)item;
                            list_EQPTYPE.Add(cast_item.strEQPTYPE_Set4);
                        }
                        else if (set_flag == 5)
                        {
                            //var cast_item = (NewModel.EQP_Info_EQPTYPE4)item;
                            var cast_item = (NewModel.Model)item;
                            list_EQPTYPE.Add(cast_item.strEQPTYPE_Set5);
                        }
                    }
                }
                else if (i == 2)
                {
                    foreach (var item in items_EQPMODEL)
                    {
                        if (set_flag == 3)
                        {
                            //var cast_item = (NewModel.EQP_Info_EQPMODEL3)item;
                            var cast_item = (NewModel.Model)item;
                            list_EQPMODEL.Add(cast_item.strEQPMODEL_Set3);
                        }
                        else if (set_flag == 4)
                        {
                            //var cast_item = (NewModel.EQP_Info_EQPMODEL4)item;
                            var cast_item = (NewModel.Model)item;
                            list_EQPMODEL.Add(cast_item.strEQPMODEL_Set4);
                        }
                        else if (set_flag == 5)
                        {
                            //var cast_item = (NewModel.EQP_Info_EQPMODEL4)item;
                            var cast_item = (NewModel.Model)item;
                            list_EQPMODEL.Add(cast_item.strEQPMODEL_Set5);
                        }
                    }
                }
                else if (i == 3)
                {
                    foreach (var item in items_ZONE)
                    {
                        if (set_flag == 3)
                        {
                            //var cast_item = (NewModel.EQP_Info_ZONE3)item;
                            var cast_item = (NewModel.Model)item;
                            list_ZONE.Add(cast_item.strZONE_Set3);
                        }
                        else if (set_flag == 4)
                        {
                            //var cast_item = (NewModel.EQP_Info_ZONE4)item;
                            var cast_item = (NewModel.Model)item;
                            list_ZONE.Add(cast_item.strZONE_Set4);
                        }
                        else if (set_flag == 5)
                        {
                            //var cast_item = (NewModel.EQP_Info_ZONE4)item;
                            var cast_item = (NewModel.Model)item;
                            list_ZONE.Add(cast_item.strZONE_Set5);
                        }
                    }
                }
            }

            //EQPID 필터링
            //DataTable dt_eqp_info = new DataTable();
            //dt_eqp_info = gfun.GetCSVData("Load_Spectrum2.Resources.Setting_EQP.csv", 2);
            if (set_flag == 3) source_EQP_Info_EQPID3.Clear();
            else if (set_flag == 4) source_EQP_Info_EQPID4.Clear();
            else if (set_flag == 5) source_EQP_Info_EQPID5.Clear();

            foreach (DataRow row in gfun.dt_eqp_info.Rows)
            {                
                flag_Line = items_Line.Count == 0 ? true : false;
                flag_EQPTYPE = items_EQPTYPE.Count == 0 ? true : false;
                flag_EQPMODEL = items_EQPMODEL.Count == 0 ? true : false;
                flag_ZONE = items_ZONE.Count == 0 ? true : false;
                flag_EQPID = str_EQP.Length == 0 ? true : false;

                foreach (string str_line in list_Line)
                {
                    if (row["LINE"].ToString() == str_line) flag_Line = true;              //필터와 다른 항목
                }
                foreach (string str_EQPTYPE in list_EQPTYPE)
                {
                    if (row["EQPTYPE"].ToString() == str_EQPTYPE) flag_EQPTYPE = true;              //필터와 다른 항목
                }
                foreach (string str_EQPMODEL in list_EQPMODEL)
                {
                    if (row["EQPMODEL"].ToString() == str_EQPMODEL) flag_EQPMODEL = true;              //필터와 다른 항목
                }
                foreach (string str_ZONE in list_ZONE)
                {
                    if (row["ZONE"].ToString() == str_ZONE) flag_ZONE = true;              //필터와 다른 항목
                }

                try
                {
                    if (str_EQP == row["EQPID"].ToString().Substring(0, str_EQP.Length))
                    {
                        flag_EQPID = true;
                    }
                }
                catch
                {
                    
                }

                if (flag_Line && flag_EQPTYPE && flag_EQPMODEL && flag_ZONE && flag_EQPID && set_flag == 3)
                {
                    //source_EQP_Info_EQPID3.Add(new NewModel.EQP_Info_EQPID3() { strEQPID_Set3 = row["EQPID"].ToString() });
                    source_EQP_Info_EQPID3.Add(new NewModel.Model() { strEQPID_Set3 = row["EQPID"].ToString() });
                }
                else if (flag_Line && flag_EQPTYPE && flag_EQPMODEL && flag_ZONE && flag_EQPID && set_flag == 4)
                {
                    //source_EQP_Info_EQPID4.Add(new NewModel.EQP_Info_EQPID4() { strEQPID_Set4 = row["EQPID"].ToString() });
                    source_EQP_Info_EQPID4.Add(new NewModel.Model() { strEQPID_Set4 = row["EQPID"].ToString() });
                }
                else if (flag_Line && flag_EQPTYPE && flag_EQPMODEL && flag_ZONE && flag_EQPID && set_flag == 5)
                {
                    //source_EQP_Info_EQPID4.Add(new NewModel.EQP_Info_EQPID4() { strEQPID_Set4 = row["EQPID"].ToString() });
                    source_EQP_Info_EQPID5.Add(new NewModel.Model() { strEQPID_Set5 = row["EQPID"].ToString() });
                }
            }
            if (set_flag == 3)
            {
                return source_EQP_Info_EQPID3;
                //this.List_EQPID_eqpSet1.ItemsSource = null;
                //this.List_EQPID_eqpSet1.ItemsSource = source_EQP_Info_EQPID1;
            }
            else if (set_flag == 4)
            {
                return source_EQP_Info_EQPID4;
                //this.List_EQPID_eqpSet2.ItemsSource = null;
                //this.List_EQPID_eqpSet2.ItemsSource = source_EQP_Info_EQPID2;
            }
            else
            {
                return source_EQP_Info_EQPID5;
                //this.List_EQPID_eqpSet2.ItemsSource = null;
                //this.List_EQPID_eqpSet2.ItemsSource = source_EQP_Info_EQPID2;
            }
        }


        private void Distinct_ListClass(DataTable dt, int list_idx, int set_flag)
        {
            int i = 0;
            foreach (DataRow row in dt.Rows)
            {
                if (i == 0)
                {
                    if (set_flag == 1)
                    {
                        if (list_idx == 0) source_EQP_list3[0].Add(new NewModel.Model() { strLine_Set3 = row["LINE"].ToString() });
                        else if (list_idx == 1) source_EQP_list3[1].Add(new NewModel.Model() { strEQPTYPE_Set3 = row["EQPTYPE"].ToString() });
                        else if (list_idx == 2) source_EQP_list3[2].Add(new NewModel.Model() { strEQPMODEL_Set3 = row["EQPMODEL"].ToString() });
                        else if (list_idx == 3) source_EQP_list3[3].Add(new NewModel.Model() { strZONE_Set3 = row["ZONE"].ToString() });
                        else if (list_idx == 4) source_EQP_list3[4].Add(new NewModel.Model() { strEQPID_Set3 = row["EQPID"].ToString() });
                    }
                    else if (set_flag == 2)
                    {
                        if (list_idx == 0) source_EQP_list4[0].Add(new NewModel.Model() { strLine_Set4 = row["LINE"].ToString() });
                        else if (list_idx == 1) source_EQP_list4[1].Add(new NewModel.Model() { strEQPTYPE_Set4 = row["EQPTYPE"].ToString() });
                        else if (list_idx == 2) source_EQP_list4[2].Add(new NewModel.Model() { strEQPMODEL_Set4 = row["EQPMODEL"].ToString() });
                        else if (list_idx == 3) source_EQP_list4[3].Add(new NewModel.Model() { strZONE_Set4 = row["ZONE"].ToString() });
                        else if (list_idx == 4) source_EQP_list4[4].Add(new NewModel.Model() { strEQPID_Set4 = row["EQPID"].ToString() });
                    }
                    else if (set_flag == 3)
                    {
                        if (list_idx == 0) source_EQP_list5[0].Add(new NewModel.Model() { strLine_Set5 = row["LINE"].ToString() });
                        else if (list_idx == 1) source_EQP_list5[1].Add(new NewModel.Model() { strEQPTYPE_Set5 = row["EQPTYPE"].ToString() });
                        else if (list_idx == 2) source_EQP_list5[2].Add(new NewModel.Model() { strEQPMODEL_Set5 = row["EQPMODEL"].ToString() });
                        else if (list_idx == 3) source_EQP_list5[3].Add(new NewModel.Model() { strZONE_Set5 = row["ZONE"].ToString() });
                        else if (list_idx == 4) source_EQP_list5[4].Add(new NewModel.Model() { strEQPID_Set5 = row["EQPID"].ToString() });
                    }
                }
                else
                //두번째 이상
                {
                    //Line
                    if (list_idx == 0)
                    {
                        if (set_flag == 3)
                        {
                            int flag = 0;
                            foreach (var item in source_EQP_list3[list_idx])
                            {
                                if (item.strLine_Set3 == row["LINE"].ToString())
                                {
                                    flag = 1;              //중복 발견
                                    break;
                                }
                            }

                            if (flag == 0)      //중복 없어야 아이템 추가
                            {
                                //source_EQP_list3[list_idx].Add(new NewModel.EQP_Info_Line3() { strLine_Set3 = row["LINE"].ToString() });
                                source_EQP_list3[list_idx].Add(new NewModel.Model() { strLine_Set3 = row["LINE"].ToString() });
                            }
                        }
                        else if (set_flag == 4)
                        {
                            int flag = 0;
                            foreach (var item in source_EQP_list4[list_idx])
                            {
                                if (item.strLine_Set4 == row["LINE"].ToString())
                                {
                                    flag = 1;              //중복 발견
                                    break;
                                }
                            }

                            if (flag == 0)      //중복 없어야 아이템 추가
                            {
                                //source_EQP_list4[list_idx].Add(new NewModel.EQP_Info_Line4() { strLine_Set4 = row["LINE"].ToString() });
                                source_EQP_list4[list_idx].Add(new NewModel.Model() { strLine_Set4 = row["LINE"].ToString() });
                            }
                        }
                        else if (set_flag == 5)
                        {
                            int flag = 0;
                            foreach (var item in source_EQP_list5[list_idx])
                            {
                                if (item.strLine_Set5 == row["LINE"].ToString())
                                {
                                    flag = 1;              //중복 발견
                                    break;
                                }
                            }

                            if (flag == 0)      //중복 없어야 아이템 추가
                            {
                                //source_EQP_list4[list_idx].Add(new NewModel.EQP_Info_Line4() { strLine_Set4 = row["LINE"].ToString() });
                                source_EQP_list5[list_idx].Add(new NewModel.Model() { strLine_Set5 = row["LINE"].ToString() });
                            }
                        }
                    }
                    //EQPTYPE
                    else if (list_idx == 1)
                    {
                        if (set_flag == 3)
                        {
                            int flag = 0;
                            foreach (var item in source_EQP_list3[list_idx])
                            {
                                if (item.strEQPTYPE_Set3 == row["EQPTYPE"].ToString())
                                {
                                    flag = 1;              //중복 발견
                                    break;
                                }
                            }

                            if (flag == 0)      //중복 없어야 아이템 추가
                            {
                                //source_EQP_list3[list_idx].Add(new NewModel.EQP_Info_EQPTYPE3() { strEQPTYPE_Set3 = row["EQPTYPE"].ToString() });
                                source_EQP_list3[list_idx].Add(new NewModel.Model() { strEQPTYPE_Set3 = row["EQPTYPE"].ToString() });
                            }
                        }
                        else if (set_flag == 4)
                        {
                            int flag = 0;
                            foreach (var item in source_EQP_list4[list_idx])
                            {
                                if (item.strEQPTYPE_Set4 == row["EQPTYPE"].ToString())
                                {
                                    flag = 1;              //중복 발견
                                    break;
                                }
                            }

                            if (flag == 0)      //중복 없어야 아이템 추가
                            {
                                //source_EQP_list4[list_idx].Add(new NewModel.EQP_Info_EQPTYPE4() { strEQPTYPE_Set4 = row["EQPTYPE"].ToString() });
                                source_EQP_list4[list_idx].Add(new NewModel.Model() { strEQPTYPE_Set4 = row["EQPTYPE"].ToString() });
                            }
                        }
                        else if (set_flag == 5)
                        {
                            int flag = 0;
                            foreach (var item in source_EQP_list5[list_idx])
                            {
                                if (item.strEQPTYPE_Set5 == row["EQPTYPE"].ToString())
                                {
                                    flag = 1;              //중복 발견
                                    break;
                                }
                            }

                            if (flag == 0)      //중복 없어야 아이템 추가
                            {
                                //source_EQP_list4[list_idx].Add(new NewModel.EQP_Info_EQPTYPE4() { strEQPTYPE_Set4 = row["EQPTYPE"].ToString() });
                                source_EQP_list5[list_idx].Add(new NewModel.Model() { strEQPTYPE_Set5 = row["EQPTYPE"].ToString() });
                            }
                        }

                    }
                    //EQPMODEL
                    else if (list_idx == 2)
                    {
                        if (set_flag == 3)
                        {
                            int flag = 0;
                            foreach (var item in source_EQP_list3[list_idx])
                            {
                                if (item.strEQPMODEL_Set3 == row["EQPMODEL"].ToString())
                                {
                                    flag = 1;              //중복 발견
                                    break;
                                }
                            }

                            if (flag == 0)      //중복 없어야 아이템 추가
                            {
                                //source_EQP_list3[list_idx].Add(new NewModel.EQP_Info_EQPMODEL3() { strEQPMODEL_Set3 = row["EQPMODEL"].ToString() });
                                source_EQP_list3[list_idx].Add(new NewModel.Model() { strEQPMODEL_Set3 = row["EQPMODEL"].ToString() });
                            }
                        }
                        else if (set_flag == 4)
                        {
                            int flag = 0;
                            foreach (var item in source_EQP_list4[list_idx])
                            {
                                if (item.strEQPMODEL_Set4 == row["EQPMODEL"].ToString())
                                {
                                    flag = 1;              //중복 발견
                                    break;
                                }
                            }

                            if (flag == 0)      //중복 없어야 아이템 추가
                            {
                                //source_EQP_list4[list_idx].Add(new NewModel.EQP_Info_EQPMODEL4() { strEQPMODEL_Set4 = row["EQPMODEL"].ToString() });
                                source_EQP_list4[list_idx].Add(new NewModel.Model() { strEQPMODEL_Set4 = row["EQPMODEL"].ToString() });
                            }
                        }
                        else if (set_flag == 5)
                        {
                            int flag = 0;
                            foreach (var item in source_EQP_list5[list_idx])
                            {
                                if (item.strEQPMODEL_Set5 == row["EQPMODEL"].ToString())
                                {
                                    flag = 1;              //중복 발견
                                    break;
                                }
                            }

                            if (flag == 0)      //중복 없어야 아이템 추가
                            {
                                //source_EQP_list4[list_idx].Add(new NewModel.EQP_Info_EQPMODEL4() { strEQPMODEL_Set4 = row["EQPMODEL"].ToString() });
                                source_EQP_list5[list_idx].Add(new NewModel.Model() { strEQPMODEL_Set5 = row["EQPMODEL"].ToString() });
                            }
                        }

                    }
                    //ZONE
                    else if (list_idx == 3)
                    {
                        if (set_flag == 3)
                        {
                            int flag = 0;
                            foreach (var item in source_EQP_list3[list_idx])
                            {
                                if (item.strZONE_Set3 == row["ZONE"].ToString())
                                {
                                    flag = 1;              //중복 발견
                                    break;
                                }
                            }

                            if (flag == 0)      //중복 없어야 아이템 추가
                            {
                                //source_EQP_list3[list_idx].Add(new NewModel.EQP_Info_ZONE3() { strZONE_Set3 = row["ZONE"].ToString() });
                                source_EQP_list3[list_idx].Add(new NewModel.Model() { strZONE_Set3 = row["ZONE"].ToString() });
                            }
                        }
                        else if (set_flag == 4)
                        {
                            int flag = 0;
                            foreach (var item in source_EQP_list4[list_idx])
                            {
                                if (item.strZONE_Set4 == row["ZONE"].ToString())
                                {
                                    flag = 1;              //중복 발견
                                    break;
                                }
                            }

                            if (flag == 0)      //중복 없어야 아이템 추가
                            {
                                //source_EQP_list4[list_idx].Add(new NewModel.EQP_Info_ZONE4() { strZONE_Set4 = row["ZONE"].ToString() });
                                source_EQP_list4[list_idx].Add(new NewModel.Model() { strZONE_Set4 = row["ZONE"].ToString() });
                            }
                        }
                        else if (set_flag == 5)
                        {
                            int flag = 0;
                            foreach (var item in source_EQP_list5[list_idx])
                            {
                                if (item.strZONE_Set5 == row["ZONE"].ToString())
                                {
                                    flag = 1;              //중복 발견
                                    break;
                                }
                            }

                            if (flag == 0)      //중복 없어야 아이템 추가
                            {
                                //source_EQP_list4[list_idx].Add(new NewModel.EQP_Info_ZONE4() { strZONE_Set4 = row["ZONE"].ToString() });
                                source_EQP_list5[list_idx].Add(new NewModel.Model() { strZONE_Set5 = row["ZONE"].ToString() });
                            }
                        }

                    }
                    //EQPID
                    else if (list_idx == 4)
                    {
                        if (set_flag == 3)
                        {
                            int flag = 0;
                            foreach (var item in source_EQP_list3[list_idx])
                            {
                                if (item.strEQPID_Set3 == row["EQPID"].ToString())
                                {
                                    flag = 1;              //중복 발견
                                    break;
                                }
                            }

                            if (flag == 0)      //중복 없어야 아이템 추가
                            {
                                //source_EQP_list3[list_idx].Add(new NewModel.EQP_Info_EQPID3() { strEQPID_Set3 = row["EQPID"].ToString() });
                                source_EQP_list3[list_idx].Add(new NewModel.Model() { strEQPID_Set3 = row["EQPID"].ToString() });

                            }
                        }
                        else if (set_flag == 4)
                        {
                            int flag = 0;
                            foreach (var item in source_EQP_list4[list_idx])
                            {
                                if (item.strEQPID_Set4 == row["EQPID"].ToString())
                                {
                                    flag = 1;              //중복 발견
                                    break;
                                }
                            }

                            if (flag == 0)      //중복 없어야 아이템 추가
                            {
                                //source_EQP_list4[list_idx].Add(new NewModel.EQP_Info_EQPID4() { strEQPID_Set4 = row["EQPID"].ToString() });
                                source_EQP_list4[list_idx].Add(new NewModel.Model() { strEQPID_Set4 = row["EQPID"].ToString() });

                            }
                        }
                        else if (set_flag == 5)
                        {
                            int flag = 0;
                            foreach (var item in source_EQP_list5[list_idx])
                            {
                                if (item.strEQPID_Set5 == row["EQPID"].ToString())
                                {
                                    flag = 1;              //중복 발견
                                    break;
                                }
                            }

                            if (flag == 0)      //중복 없어야 아이템 추가
                            {
                                //source_EQP_list4[list_idx].Add(new NewModel.EQP_Info_EQPID4() { strEQPID_Set4 = row["EQPID"].ToString() });
                                source_EQP_list5[list_idx].Add(new NewModel.Model() { strEQPID_Set5 = row["EQPID"].ToString() });

                            }
                        }

                    }
                }
                i++;
            }
        }

        public object MkSrc_ItemLayer(string[] txt_part)
        {
            //txt_part는 csv파일을 행으로 쪼갠 조각들
            //Item, Layer 소스 바인딩 작업
            int cnt1 = 0;
            int cnt2 = 0;
            List<string> RNR_Item = new List<string>();
            List<string> RNR_Layer = new List<string>();
            Dictionary<int, List<string>> RNR_Layer_dic = new Dictionary<int, List<string>>();
            source_RNR_ItemLayer.Clear();

            foreach (string part in txt_part)       //part는 한 개의 행 의미
            {
                //uiList_source_EQPID[set_flag - 3].Add(new NewModel.Model() { strEQPID_Set3 = cast_item.strchkEQPID_Set3 });
                //Item
                if (part.StartsWith("TEST ID"))
                {
                    RNR_Item.Add(part.Split(',')[1]);
                    cnt1++;
                }
                    
                //Layer
                //SUM 쉼표 구분 필요, 상부3개만 취합 필요
                if (part.ToUpper().Contains("SITE #"))                
                {
                    List<string> New_part = Global_FunSet.ReturnSumStrCommaDel(part.Split(',').ToList());
                    RNR_Layer.Clear();
                    foreach (string layer_part in New_part)
                    {
                        //SUM(1,2,3,4) 구분 필요
                        if (!layer_part.ToUpper().Contains("SITE") && !layer_part.ToUpper().Contains("NOSC") && !layer_part.ToUpper().Contains("X") && !layer_part.ToUpper().Contains("Y") && !layer_part.ToUpper().Contains("EG") && !layer_part.ToUpper().Contains("EN") && (layer_part != ""))
                        {
                            RNR_Layer.Add(layer_part);
                        }
                    }                    

                    List<string> copy_list = ReturnDelBotLayer(RNR_Layer).ToList();
                    RNR_Layer_dic.Add(cnt2+1, copy_list);
                    cnt2++;
                }
            }

            //상부 3개 Layer만 
            RNR_Item = RNR_Item.Distinct().ToList();
            foreach (var ele in RNR_Item)
            {
                int cast_ele = Int32.Parse(ele);

                foreach (var entry in RNR_Layer_dic[cast_ele])
                {
                    source_RNR_ItemLayer.Add(new NewModel.Model { strRNR_Item = cast_ele, strRNR_Layer = entry, strRNR_Chk = false });
                }
            }

            return source_RNR_ItemLayer;
        }

        private List<string> ReturnDelBotLayer(List<string> RNR_Layer)
        {
            List<string> temp_list = new List<string>();

            foreach (string ele in RNR_Layer)
            {
                int n;
                if (ele.Substring(0,1) == "T" && int.TryParse(ele.Substring(1, ele.Length - 1), out n))
                {
                    //앞이 T이고 뒤가 숫자이면
                    temp_list.Add(ele);
                }
            }

            temp_list.Sort();
            if (temp_list.Count > 3)
            {
                temp_list.RemoveRange(temp_list.Count - 3, 3);
                foreach (var temp in temp_list)
                {
                    RNR_Layer.Remove(temp);
                }
            }

            return RNR_Layer;
        }



        //Binding Properties
        #region Properties       
        private List<NewModel.Model> _source_RNR_ItemLayer;
        public List<NewModel.Model> source_RNR_ItemLayer
        {
            get { return _source_RNR_ItemLayer; }
            set { _source_RNR_ItemLayer = value; OnPropertyChanged("source_RNR_ItemLayer"); }
        }

        private int _pgbVal_Set5;
        public int pgbVal_Set5
        {
            get { return _pgbVal_Set5; }
            set { _pgbVal_Set5 = value; OnPropertyChanged("pgbVal_Set5"); }
        }

        private List<NewModel.Model> _source_drive;
        public List<NewModel.Model> source_drive
        {
            get { return _source_drive; }
            set { _source_drive = value; OnPropertyChanged("source_drive"); }
        }

        private List<NewModel.Model> _source_EQP_Info_all3;
        public List<NewModel.Model> source_EQP_Info_all3
        {
            get { return _source_EQP_Info_all3; }
            set { _source_EQP_Info_all3 = value; OnPropertyChanged("source_EQP_Info_all3"); }
        }
        private List<NewModel.Model> _source_EQP_Info_all4;
        public List<NewModel.Model> source_EQP_Info_all4
        {
            get { return _source_EQP_Info_all4; }
            set { _source_EQP_Info_all4 = value; OnPropertyChanged("source_EQP_Info_all4"); }
        }
        private List<NewModel.Model> _source_EQP_Info_all5;
        public List<NewModel.Model> source_EQP_Info_all5
        {
            get { return _source_EQP_Info_all5; }
            set { _source_EQP_Info_all5 = value; OnPropertyChanged("source_EQP_Info_all5"); }
        }

        private List<NewModel.Model> _source_EQP_Info_Line3;
        public List<NewModel.Model> source_EQP_Info_Line3
        {
            get { return _source_EQP_Info_Line3; }
            set { _source_EQP_Info_Line3 = value; OnPropertyChanged("source_EQP_Info_Line3"); }
        }
        private List<NewModel.Model> _source_EQP_Info_Line4;
        public List<NewModel.Model> source_EQP_Info_Line4
        {
            get { return _source_EQP_Info_Line4; }
            set { _source_EQP_Info_Line4 = value; OnPropertyChanged("source_EQP_Info_Line4"); }
        }
        private List<NewModel.Model> _source_EQP_Info_Line5;
        public List<NewModel.Model> source_EQP_Info_Line5
        {
            get { return _source_EQP_Info_Line5; }
            set { _source_EQP_Info_Line5 = value; OnPropertyChanged("source_EQP_Info_Line5"); }
        }

        private List<NewModel.Model> _source_EQP_Info_EQPMODEL3;
        public List<NewModel.Model> source_EQP_Info_EQPMODEL3
        {
            get { return _source_EQP_Info_EQPMODEL3; }
            set { _source_EQP_Info_EQPMODEL3 = value; OnPropertyChanged("source_EQP_Info_EQPMODEL3"); }
        }
        private List<NewModel.Model> _source_EQP_Info_EQPMODEL4;
        public List<NewModel.Model> source_EQP_Info_EQPMODEL4
        {
            get { return _source_EQP_Info_EQPMODEL4; }
            set { _source_EQP_Info_EQPMODEL4 = value; OnPropertyChanged("source_EQP_Info_EQPMODEL4"); }
        }
        private List<NewModel.Model> _source_EQP_Info_EQPMODEL5;
        public List<NewModel.Model> source_EQP_Info_EQPMODEL5
        {
            get { return _source_EQP_Info_EQPMODEL5; }
            set { _source_EQP_Info_EQPMODEL5 = value; OnPropertyChanged("source_EQP_Info_EQPMODEL5"); }
        }

        private List<NewModel.Model> _source_EQP_Info_EQPTYPE3;
        public List<NewModel.Model> source_EQP_Info_EQPTYPE3
        {
            get { return _source_EQP_Info_EQPTYPE3; }
            set { _source_EQP_Info_EQPTYPE3 = value; OnPropertyChanged("source_EQP_Info_EQPTYPE3"); }
        }
        private List<NewModel.Model> _source_EQP_Info_EQPTYPE4;
        public List<NewModel.Model> source_EQP_Info_EQPTYPE4
        {
            get { return _source_EQP_Info_EQPTYPE4; }
            set { _source_EQP_Info_EQPTYPE4 = value; OnPropertyChanged("source_EQP_Info_EQPTYPE4"); }
        }
        private List<NewModel.Model> _source_EQP_Info_EQPTYPE5;
        public List<NewModel.Model> source_EQP_Info_EQPTYPE5
        {
            get { return _source_EQP_Info_EQPTYPE5; }
            set { _source_EQP_Info_EQPTYPE5 = value; OnPropertyChanged("source_EQP_Info_EQPTYPE5"); }
        }

        private List<NewModel.Model> _source_EQP_Info_ZONE3;
        public List<NewModel.Model> source_EQP_Info_ZONE3
        {
            get { return _source_EQP_Info_ZONE3; }
            set { _source_EQP_Info_ZONE3 = value; OnPropertyChanged("source_EQP_Info_ZONE3"); }
        }
        private List<NewModel.Model> _source_EQP_Info_ZONE4;
        public List<NewModel.Model> source_EQP_Info_ZONE4
        {
            get { return _source_EQP_Info_ZONE4; }
            set { _source_EQP_Info_ZONE4 = value; OnPropertyChanged("source_EQP_Info_ZONE4"); }
        }
        private List<NewModel.Model> _source_EQP_Info_ZONE5;
        public List<NewModel.Model> source_EQP_Info_ZONE5
        {
            get { return _source_EQP_Info_ZONE5; }
            set { _source_EQP_Info_ZONE5 = value; OnPropertyChanged("source_EQP_Info_ZONE5"); }
        }

        private List<NewModel.Model> _source_EQP_Info_EQPID3;
        public List<NewModel.Model> source_EQP_Info_EQPID3
        {
            get { return _source_EQP_Info_EQPID3; }
            set { _source_EQP_Info_EQPID3 = value; OnPropertyChanged("source_EQP_Info_EQPID3"); }
        }
        private List<NewModel.Model> _source_EQP_Info_EQPID4;
        public List<NewModel.Model> source_EQP_Info_EQPID4
        {
            get { return _source_EQP_Info_EQPID4; }
            set { _source_EQP_Info_EQPID4 = value; OnPropertyChanged("source_EQP_Info_EQPID4"); }
        }
        private List<NewModel.Model> _source_EQP_Info_EQPID5;
        public List<NewModel.Model> source_EQP_Info_EQPID5
        {
            get { return _source_EQP_Info_EQPID5; }
            set { _source_EQP_Info_EQPID5 = value; OnPropertyChanged("source_EQP_Info_EQPID5"); }
        }

        private DataSet _dataset;
        public DataSet dataset
        {
            get { return _dataset; }
            set { _dataset = value; OnPropertyChanged("dataset"); }
        }
        #endregion



        public event PropertyChangedEventHandler PropertyChanged;

        //[NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
