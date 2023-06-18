using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.IO;
using System.Data;
using System.Reflection;
using System.Diagnostics;
using System.Collections;
using System.ComponentModel;
using System.Globalization;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using System.Net;
using System.Threading;
using System.Windows.Threading;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;
using System.Windows.Media.Effects;
using System.Drawing;
using System.Windows.Shapes;
using System.Windows.Resources;
using System.Resources;
using Excel = Microsoft.Office.Interop.Excel;

namespace Load_Spectrum2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //Global Function Set
        public static Global_FunSet gfun;
        //Local 변수
        GridViewColumnHeader _lastHeaderClicked = null;
        ListSortDirection _lastDirection = ListSortDirection.Ascending;
        Line_text1 line_text1 = null;
        Line_text2 line_text2 = null;
        public static List<String> Local1_Back_Path;
        public static List<String> Local1_Foward_Path;
        public static List<String> Local2_Back_Path;
        public static List<String> Local2_Foward_Path;
        public static List<int> Local1_Back_idx;
        public static List<int> Local1_Foward_idx;
        public static List<int> Local2_Back_idx;
        public static List<int> Local2_Foward_idx;
        public static List<Local_path_all1> source_Local_path_all1 = new List<Local_path_all1>();
        public static List<Local_path_all2> source_Local_path_all2 = new List<Local_path_all2>();

        //EQP변수
        Line_text_eqp line_text_eqp = null;
        public static List<EQP_Info_Line1> source_EQP_Info_Line1 = new List<EQP_Info_Line1>();
        public static List<EQP_Info_Line2> source_EQP_Info_Line2 = new List<EQP_Info_Line2>();
        public static List<EQP_Info_Line3> source_EQP_Info_Line3 = new List<EQP_Info_Line3>();
        public static List<EQP_Info_EQPTYPE1> source_EQP_Info_EQPTYPE1 = new List<EQP_Info_EQPTYPE1>();
        public static List<EQP_Info_EQPTYPE2> source_EQP_Info_EQPTYPE2 = new List<EQP_Info_EQPTYPE2>();
        public static List<EQP_Info_EQPTYPE3> source_EQP_Info_EQPTYPE3 = new List<EQP_Info_EQPTYPE3>();
        public static List<EQP_Info_EQPMODEL1> source_EQP_Info_EQPMODEL1 = new List<EQP_Info_EQPMODEL1>();
        public static List<EQP_Info_EQPMODEL2> source_EQP_Info_EQPMODEL2 = new List<EQP_Info_EQPMODEL2>();
        public static List<EQP_Info_EQPMODEL3> source_EQP_Info_EQPMODEL3 = new List<EQP_Info_EQPMODEL3>();
        public static List<EQP_Info_ZONE1> source_EQP_Info_ZONE1 = new List<EQP_Info_ZONE1>();
        public static List<EQP_Info_ZONE2> source_EQP_Info_ZONE2 = new List<EQP_Info_ZONE2>();
        public static List<EQP_Info_ZONE3> source_EQP_Info_ZONE3 = new List<EQP_Info_ZONE3>();
        public static List<EQP_Info_EQPID1> source_EQP_Info_EQPID1 = new List<EQP_Info_EQPID1>();
        public static List<EQP_Info_EQPID2> source_EQP_Info_EQPID2 = new List<EQP_Info_EQPID2>();
        public static List<EQP_Info_EQPID3> source_EQP_Info_EQPID3 = new List<EQP_Info_EQPID3>();
        public static List<EQP_Info_EISelect> source_EQP_Info_EQPID_Select = new List<EQP_Info_EISelect>();
        public static List<dynamic> source_EQP_list1 = new List<dynamic> { source_EQP_Info_Line1, source_EQP_Info_EQPTYPE1, source_EQP_Info_EQPMODEL1, source_EQP_Info_ZONE1, source_EQP_Info_EQPID1 };
        public static List<dynamic> source_EQP_list2 = new List<dynamic> { source_EQP_Info_Line2, source_EQP_Info_EQPTYPE2, source_EQP_Info_EQPMODEL2, source_EQP_Info_ZONE2, source_EQP_Info_EQPID2 };
        public static List<dynamic> source_EQP_list3 = new List<dynamic> { source_EQP_Info_Line3, source_EQP_Info_EQPTYPE3, source_EQP_Info_EQPMODEL3, source_EQP_Info_ZONE3, source_EQP_Info_EQPID3 };

        public static Dictionary<string, string> source_EQP_dict = new Dictionary<string, string>();
        public static Dictionary<string, string> source_drive_dict = new Dictionary<string, string>();
        public static List<EQP_path_all1> source_EQP_path_all1 = new List<EQP_path_all1>();        
        public static List<EQP_path_all2> source_EQP_path_all2 = new List<EQP_path_all2>();
        public static List<Control_Vis> source_EQP_EI_all = new List<Control_Vis>();

        public static Dictionary<string, string> source_ftp_info1 = new Dictionary<string, string>();
        public static Dictionary<string, string> source_ftp_info2 = new Dictionary<string, string>();
        public static Dictionary<string, string> source_ftp_info3 = new Dictionary<string, string>();
        public static List<String> EQP1_Back_Path;
        public static List<String> EQP1_Foward_Path;
        public static List<String> EQP2_Back_Path;
        public static List<String> EQP2_Foward_Path;
        public static List<String> EQP_EI_Back_path;
        public static List<String> EQP_EI_Foward_path;
        public static List<int> EQP1_Back_idx;
        public static List<int> EQP1_Foward_idx;
        public static List<int> EQP2_Back_idx;
        public static List<int> EQP2_Foward_idx;
        public static List<int> EQP_EI_Back_idx;
        public static List<int> EQP_EI_Foward_idx;

        //UI object list
        public static List<ProgressBar> uiList_pgb;
        public static List<TextBox> uiList_textbox;
        public static List<ListView> uiList_listView;
        public static List<TextBlock> uiList_pgbTxt;
        public static List<perBusySpinner> uiList_pgbSpinner;
        public static List<Button> uiList_StopBtn;
        public static List<dynamic> uiList_listsource;
        public static List<StackPanel> uiList_stack;        
        
        public static int ftp_timeout = 10000;
        public static int ETL_stop_flag = 0;
        public static int ETL_stop_flag2 = 0;
        public static int ETL_stop_flag3 = 0;
        public static int ETL_stop_flag4 = 0;
        public static int LTE_stop_flag = 0;
        public static int LTE_stop_flag2 = 0;
        public static int LTE_stop_flag3 = 0;
        public static int LTE_stop_flag4 = 0;
        public static int LTL_stop_flag = 0;
        public static int LTL_stop_flag2 = 0;

        private int header_flag = 0;

        Control_Vis control_Vis = new Control_Vis();

        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(MainPage_Loaded);
            this.PreviewKeyDown += new KeyEventHandler(Key_Handler);

            //img visible
            this.DataContext = control_Vis;

            //Info EQP
            //Info EQP 다운로드 
            //string info_uri = @"FTP://192.168.0.103:21/S5/Load_Spectrum2/InfoEQP/Setting_EQP.csv";            
            //int info_eqp_passfail;
            //info_eqp_passfail = FTPOneFileDown(Global_FunSet.info_uri, Global_FunSet.info_t_path, Global_FunSet.info_id, Global_FunSet.info_pw);

            //if (info_eqp_passfail == -1)
            //{
            //    this.Info_EQP_chk.Text = "내장 EQP Info | ";
            //    //MessageBox.Show("서버 접속 불가, 내장된 EQP Info 활용");
            //}
            //else if (info_eqp_passfail == 1)
            //{
            //    this.Info_EQP_chk.Text = "서버 EQP Info | ";
            //}

            //global fun
            gfun = new Global_FunSet();

            source_drive_dict.Add("C:\\", "LS_C");
            source_drive_dict.Add("D:\\", "LS_D");
            source_drive_dict.Add("E:\\", "LS_E");

            source_ftp_info1.Add("ip", "");
            source_ftp_info1.Add("id", "");
            source_ftp_info1.Add("pw", "");
            source_ftp_info1.Add("uri", "");
            source_ftp_info1.Add("set_flag", "");
            source_ftp_info1.Add("eqpid", "");
            source_ftp_info1.Add("foback_flag", "");
            source_ftp_info1.Add("drive", "");

            source_ftp_info2.Add("ip", "");
            source_ftp_info2.Add("id", "");
            source_ftp_info2.Add("pw", "");
            source_ftp_info2.Add("uri", "");
            source_ftp_info2.Add("set_flag", "");
            source_ftp_info2.Add("eqpid", "");
            source_ftp_info2.Add("foback_flag", "");
            source_ftp_info2.Add("drive", "");

            source_ftp_info3.Add("ip", "");
            source_ftp_info3.Add("id", "");
            source_ftp_info3.Add("pw", "");
            source_ftp_info3.Add("uri", "");
            source_ftp_info3.Add("set_flag", "");
            source_ftp_info3.Add("eqpid", "");
            source_ftp_info3.Add("foback_flag", "");
            source_ftp_info3.Add("drive", "");

            Local1_Back_Path = new List<String>();
            Local1_Foward_Path = new List<String>();
            Local2_Back_Path = new List<String>();
            Local2_Foward_Path = new List<String>();
            Local1_Back_idx = new List<int>();
            Local1_Foward_idx = new List<int>();
            Local2_Back_idx = new List<int>();
            Local2_Foward_idx = new List<int>();

            EQP1_Back_Path = new List<String>();
            EQP1_Foward_Path = new List<String>();
            EQP2_Back_Path = new List<String>();
            EQP2_Foward_Path = new List<String>();
            EQP_EI_Back_path = new List<String>();
            EQP_EI_Foward_path = new List<String>();
            EQP1_Back_idx = new List<int>();
            EQP1_Foward_idx = new List<int>();
            EQP2_Back_idx = new List<int>();
            EQP2_Foward_idx = new List<int>();
            EQP_EI_Back_idx = new List<int>();
            EQP_EI_Foward_idx = new List<int>();

            line_text1 = new Line_text1();
            line_text2 = new Line_text2();
            line_text_eqp = new Line_text_eqp();

            uiList_pgb = new List<ProgressBar>();
            uiList_textbox = new List<TextBox>();
            uiList_listView = new List<ListView>();
            uiList_pgbTxt = new List<TextBlock>();
            uiList_pgbSpinner = new List<perBusySpinner>();
            uiList_StopBtn = new List<Button>();
            uiList_listsource = new List<dynamic>();
            uiList_stack = new List<StackPanel>();

            uiList_pgb.Add(Pgb_local1);
            uiList_pgb.Add(Pgb_local2);
            uiList_pgb.Add(Pgb_eqp1);
            uiList_pgb.Add(Pgb_eqp2);

            uiList_textbox.Add(line_set1_local);
            uiList_textbox.Add(line_set2_local);
            uiList_textbox.Add(line_set1_eqp);
            uiList_textbox.Add(line_set2_eqp);

            uiList_listView.Add(list_set1_local_all);
            uiList_listView.Add(list_set2_local_all);
            uiList_listView.Add(list_set1_eqp_all);
            uiList_listView.Add(list_set2_eqp_all);

            uiList_pgbTxt.Add(Pgb_local1_text);
            uiList_pgbTxt.Add(Pgb_local2_text);
            uiList_pgbTxt.Add(Pgb_eqp1_text);
            uiList_pgbTxt.Add(Pgb_eqp2_text);

            uiList_pgbSpinner.Add(Spin_local1);
            uiList_pgbSpinner.Add(Spin_local2);
            uiList_pgbSpinner.Add(Spin_eqp1);
            uiList_pgbSpinner.Add(Spin_eqp2);

            uiList_StopBtn.Add(Local1_StopBtn);
            uiList_StopBtn.Add(Local2_StopBtn);
            uiList_StopBtn.Add(EQP1_StopBtn);
            uiList_StopBtn.Add(EQP2_StopBtn);

            uiList_listsource.Add(source_Local_path_all1);
            uiList_listsource.Add(source_Local_path_all2);
            uiList_listsource.Add(source_EQP_path_all1);
            uiList_listsource.Add(source_EQP_path_all2);

            uiList_stack.Add(this.stack1_local);
            uiList_stack.Add(this.stack2_local);
            uiList_stack.Add(this.stack1_eqp);
            uiList_stack.Add(this.stack2_eqp);

            control_Vis.Vimg1_local_src = "";
            control_Vis.Vimg2_local_src = "";
            control_Vis.Vimg1_eqp_src = "";
            control_Vis.Vimg2_eqp_src = "";

            this.Spin_local1.Visibility = Visibility.Collapsed;
            this.Spin_local2.Visibility = Visibility.Collapsed;
            this.Spin_eqp1.Visibility = Visibility.Collapsed;
            this.Spin_eqp2.Visibility = Visibility.Collapsed;

            this.Local1_StopBtn.Visibility = Visibility.Collapsed;
            this.Local2_StopBtn.Visibility = Visibility.Collapsed;
            this.EQP1_StopBtn.Visibility = Visibility.Collapsed;
            this.EQP2_StopBtn.Visibility = Visibility.Collapsed;

            List<Local_path1> source_local_path1 = new List<Local_path1>();
            List<Local_path2> source_local_path2 = new List<Local_path2>();
            //List<Combo1_path> source_combo1_path = new List<Combo1_path>();


            //나중에 경로 추가할 수 있는 방법 공부하자 FTP 이용해야 함, 윤부장님 코드 참고
            //string appFolderPath = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            //string resourcesFolderPath = System.IO.Path.Combine(Directory.GetParent(appFolderPath).Parent.FullName, "Resources");

            //처음에는 기본 드라이브만 세팅
            //C>Load_Spectrum2>CustomPathList.csv 가 있는지 확인부터
            DirectoryInfo ini_di = new DirectoryInfo(@"C:\\Load_Spectrum2");
            DirectoryInfo ini_di_img = new DirectoryInfo(@"C:\\Load_Spectrum2\\Image");
            DirectoryInfo ini_di_mguide = new DirectoryInfo(@"C:\\Load_Spectrum2\\Mguide");
            DirectoryInfo ini_di_temp = new DirectoryInfo(@"C:\\Load_Spectrum2\\Temp");
            DirectoryInfo ini_di_rnr = new DirectoryInfo(@"C:\\Load_Spectrum2\\RNR");
            DataTable dt_local_path = new DataTable();
            if (ini_di.Exists)
            {
                if (File.Exists(@"C:\\Load_Spectrum2\\CustomPathList.csv"))
                {
                    dt_local_path = Global_FunSet.GetCSVData(@"C:\\Load_Spectrum2\\CustomPathList.csv", 1);
                }
                else
                {
                    using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"C:\\Load_Spectrum2\\CustomPathList.csv", false, System.Text.Encoding.GetEncoding("utf-8")))
                    {
                        file.WriteLine("Comment,Path");
                        DriveInfo[] alld = DriveInfo.GetDrives();
                        foreach (DriveInfo d in alld)
                        {
                            file.WriteLine("{0},{1}", "기본경로", d.ToString());
                        }
                    }
                    dt_local_path = Global_FunSet.GetCSVData(@"C:\\Load_Spectrum2\\CustomPathList.csv", 1);

                }
            }
            else
            {
                ini_di.Create();
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"C:\\Load_Spectrum2\\CustomPathList.csv", false, System.Text.Encoding.GetEncoding("utf-8")))
                {
                    file.WriteLine("Comment,Path");
                    DriveInfo[] alld = DriveInfo.GetDrives();
                    foreach (DriveInfo d in alld)
                    {
                        file.WriteLine("{0},{1}", "기본경로", d.ToString());
                    }
                }
                dt_local_path = Global_FunSet.GetCSVData(@"C:\\Load_Spectrum2\\CustomPathList.csv", 1);
            }

            if (!ini_di_img.Exists) ini_di_img.Create();
            else
            {
                try
                {
                    //존재하면 내부의 이미지, 폴더 모두 삭제
                    foreach (FileInfo file in ini_di_img.GetFiles())
                    {
                        File.Delete(@"C:\\Load_Spectrum2\\Image\\" + file);
                    }
                    foreach (DirectoryInfo dir in ini_di_img.GetDirectories())
                    {
                        Directory.Delete(ini_di_img + "\\" + dir, true);
                    }
                }
                catch
                {
                    //예외 무시
                }

            }

            if (!ini_di_temp.Exists) ini_di_temp.Create();
            else
            {
                try
                {
                    //Temp파일 모두 삭제 (ETE 임시 다운 파일)
                    foreach (FileInfo file in ini_di_temp.GetFiles())
                    {
                        File.Delete(@"C:\\Load_Spectrum2\\Temp\\" + file);
                    }
                    foreach (DirectoryInfo dir in ini_di_temp.GetDirectories())
                    {
                        Directory.Delete(ini_di_temp + "\\" + dir, true);
                    }
                }
                catch
                {
                    //예외 무시
                }
            }

            if (!ini_di_mguide.Exists) ini_di_mguide.Create();
            if (!ini_di_rnr.Exists) ini_di_rnr.Create();

            foreach (DataRow row in dt_local_path.Rows)
            {
                source_local_path1.Add(new Local_path1() { local_comment = row["Comment"].ToString(), local_path = row["Path"].ToString() });
                source_local_path2.Add(new Local_path2() { local_comment = row["Comment"].ToString(), local_path = row["Path"].ToString() });
            }

            foreach (DataRow row in gfun.dt_eqp_info.Rows)
            {
                source_EQP_dict.Add(row["EQPID"].ToString(), row["IP"].ToString());
            }

            this.list_set1_local_path.ItemsSource = source_local_path1;
            this.list_set2_local_path.ItemsSource = source_local_path2;

            this.line_set1_local.Focus();
            this.line_set1_local.Select(this.line_set1_local.Text.Length, 0);

            this.line_set2_local.Focus();
            this.line_set2_local.Select(this.line_set1_local.Text.Length, 0);

            //EQP
            for (int i = 0; i < source_EQP_list1.Count(); i++)
            {
                Distinct_ListClass(gfun.dt_eqp_info, i, 1);
                Distinct_ListClass(gfun.dt_eqp_info, i, 2);
                Distinct_ListClass(gfun.dt_eqp_info, i, 3);
            }
            //리스트박스 Element 정렬
            this.List_Line_eqpSet1.Items.SortDescriptions.Add(new SortDescription("strLine_Set1", ListSortDirection.Ascending));
            this.List_EQPTYPE_eqpSet1.Items.SortDescriptions.Add(new SortDescription("strEQPTYPE_Set1", ListSortDirection.Ascending));
            this.List_EQPMODEL_eqpSet1.Items.SortDescriptions.Add(new SortDescription("strEQPMODEL_Set1", ListSortDirection.Ascending));
            this.List_ZONE_eqpSet1.Items.SortDescriptions.Add(new SortDescription("strZONE_Set1", ListSortDirection.Ascending));
            this.List_EQPID_eqpSet1.Items.SortDescriptions.Add(new SortDescription("strEQPID_Set1", ListSortDirection.Ascending));

            this.List_Line_eqpSet2.Items.SortDescriptions.Add(new SortDescription("strLine_Set2", ListSortDirection.Ascending));
            this.List_EQPTYPE_eqpSet2.Items.SortDescriptions.Add(new SortDescription("strEQPTYPE_Set2", ListSortDirection.Ascending));
            this.List_EQPMODEL_eqpSet2.Items.SortDescriptions.Add(new SortDescription("strEQPMODEL_Set2", ListSortDirection.Ascending));
            this.List_ZONE_eqpSet2.Items.SortDescriptions.Add(new SortDescription("strZONE_Set2", ListSortDirection.Ascending));
            this.List_EQPID_eqpSet2.Items.SortDescriptions.Add(new SortDescription("strEQPID_Set2", ListSortDirection.Ascending));

            this.List_Line_EI.Items.SortDescriptions.Add(new SortDescription("strLine_Set3", ListSortDirection.Ascending));
            this.List_EQPTYPE_EI.Items.SortDescriptions.Add(new SortDescription("strEQPTYPE_Set3", ListSortDirection.Ascending));
            this.List_EQPMODEL_EI.Items.SortDescriptions.Add(new SortDescription("strEQPMODEL_Set3", ListSortDirection.Ascending));
            this.List_ZONE_EI.Items.SortDescriptions.Add(new SortDescription("strZONE_Set3", ListSortDirection.Ascending));
            this.List_EQPID_EI.Items.SortDescriptions.Add(new SortDescription("strEQPID_Set3", ListSortDirection.Ascending));

            this.List_Line_eqpSet1.DataContext = source_EQP_Info_Line1;
            this.List_EQPTYPE_eqpSet1.DataContext = source_EQP_Info_EQPTYPE1;
            this.List_EQPMODEL_eqpSet1.DataContext = source_EQP_Info_EQPMODEL1;
            this.List_ZONE_eqpSet1.DataContext = source_EQP_Info_ZONE1;
            this.List_EQPID_eqpSet1.DataContext = source_EQP_Info_EQPID1;
            this.List_Drive_eqpSet1.Items.Add("C:\\");
            this.List_Drive_eqpSet1.Items.Add("D:\\");
            this.List_Drive_eqpSet1.Items.Add("E:\\");

            this.List_Line_eqpSet2.DataContext = source_EQP_Info_Line2;
            this.List_EQPTYPE_eqpSet2.DataContext = source_EQP_Info_EQPTYPE2;
            this.List_EQPMODEL_eqpSet2.DataContext = source_EQP_Info_EQPMODEL2;
            this.List_ZONE_eqpSet2.DataContext = source_EQP_Info_ZONE2;
            this.List_EQPID_eqpSet2.DataContext = source_EQP_Info_EQPID2;
            this.List_Drive_eqpSet2.Items.Add("C:\\");
            this.List_Drive_eqpSet2.Items.Add("D:\\");
            this.List_Drive_eqpSet2.Items.Add("E:\\");

            this.List_Line_EI.DataContext = source_EQP_Info_Line3;
            this.List_EQPTYPE_EI.DataContext = source_EQP_Info_EQPTYPE3;
            this.List_EQPMODEL_EI.DataContext = source_EQP_Info_EQPMODEL3;
            this.List_ZONE_EI.DataContext = source_EQP_Info_ZONE3;
            this.List_EQPID_EI.DataContext = source_EQP_Info_EQPID3;

            //뷰어 기본 설정            
            Vis_Normal();

            //설비별 이미지 Tab 설정
            this.combo_EI.SelectedIndex = 1;
        }


        void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            
        }
                
        private void RenameFun(int set_flag)
        {            
            if (uiList_listView[set_flag - 1].SelectedItems.Count != 1) return;
            var s_items = uiList_listView[set_flag - 1].SelectedItem;
            dynamic items = null;
            if (set_flag == 1) items = (Local_path_all1)s_items;
            else if (set_flag == 2) items = (Local_path_all2)s_items;
            else if (set_flag == 3) items = (EQP_path_all1)s_items;
            else if (set_flag == 4) items = (EQP_path_all2)s_items;

            try
            {                    
                if (set_flag < 3)
                {
                    renamebox = new RenameBox(items.local_all_filename, items.local_all_extension);
                }
                else
                {
                    renamebox = new RenameBox(items.eqp_all_filename, items.eqp_all_extension);
                }


                //엔터 누르면, 확인누르면 파일명 변경해야함
                if (renamebox.ShowDialog() == true)
                {
                    var new_filename = renamebox.Text_Rename.Text;

                    if (set_flag < 3)
                    {
                        //폴더인경우 
                        if (items.local_all_extension == "폴더")
                        {
                            try
                            {
                                if (set_flag == 1)
                                {
                                    Directory.Move(this.line_set1_local.Text + "\\" + items.local_all_filename, this.line_set1_local.Text + "\\" + new_filename);
                                    DateTime dirDate = Directory.GetLastWriteTime(this.line_set1_local.Text + "\\" + new_filename);

                                    var list_item = source_Local_path_all1.SingleOrDefault(x => x.local_all_filename == items.local_all_filename);
                                    list_item.local_all_filename = new_filename;
                                    list_item.local_all_date = dirDate;

                                    list_set1_local_all.DataContext = source_Local_path_all1;
                                    list_set1_local_all.ItemsSource = source_Local_path_all1;                                    
                                    list_set1_local_all.Items.Refresh();
                                }
                                else if (set_flag == 2)
                                {
                                    Directory.Move(this.line_set2_local.Text + "\\" + items.local_all_filename, this.line_set2_local.Text + "\\" + new_filename);
                                    DateTime dirDate = Directory.GetLastWriteTime(this.line_set2_local.Text + "\\" + new_filename);

                                    var list_item = source_Local_path_all2.SingleOrDefault(x => x.local_all_filename == items.local_all_filename);
                                    list_item.local_all_filename = new_filename;
                                    list_item.local_all_date = dirDate;

                                    list_set2_local_all.DataContext = source_Local_path_all2;
                                    list_set2_local_all.ItemsSource = source_Local_path_all2;
                                    list_set2_local_all.Items.Refresh();
                                }
                            }
                            catch
                            {
                                MessageBox.Show("이미 존재하는 폴더입니다.");
                                return;
                            }

                        }

                        //파일인경우
                        else
                        {
                            try
                            {
                                if (set_flag == 1) File.Move(this.line_set1_local.Text + "\\" + items.local_all_filename, this.line_set1_local.Text + "\\" + new_filename);
                                else if (set_flag == 2) File.Move(this.line_set2_local.Text + "\\" + items.local_all_filename, this.line_set2_local.Text + "\\" + new_filename);

                            }
                            catch
                            {
                                MessageBox.Show("이미 존재하는 파일입니다.");
                                return;
                            }
                            
                            string fileExetension = new FileInfo(new_filename).Extension;
                            fileIcon = Global_FunSet.ReturnIconPathStr(fileExetension);

                            if (set_flag == 1)
                            {
                                var list_item = source_Local_path_all1.SingleOrDefault(x => x.local_all_filename == items.local_all_filename);
                                list_item.local_all_filename = new_filename;
                                list_item.local_all_extension = fileExetension;
                                list_item.local_all_src = fileIcon;

                                list_set1_local_all.DataContext = source_Local_path_all1;
                                list_set1_local_all.ItemsSource = source_Local_path_all1;
                                list_set1_local_all.Items.Refresh();
                            }
                            else if (set_flag == 2)
                            {
                                var list_item = source_Local_path_all2.SingleOrDefault(x => x.local_all_filename == items.local_all_filename);
                                list_item.local_all_filename = new_filename;
                                list_item.local_all_extension = fileExetension;
                                list_item.local_all_src = fileIcon;

                                list_set2_local_all.DataContext = source_Local_path_all2;
                                list_set2_local_all.ItemsSource = source_Local_path_all2;
                                list_set2_local_all.Items.Refresh();
                            }
                        }
                    }
                    else
                    {
                        //폴더인경우 
                        if (items.eqp_all_extension == "폴더")
                        {
                            try
                            {
                                if (set_flag == 3)
                                {
                                    string uri = this.line_set1_eqp.Text + "/" + items.eqp_all_filename;
                                    FtpWebRequest ftpReq = (FtpWebRequest)WebRequest.Create(new Uri(uri.Replace("#", "%23")));
                                    ftpReq.Credentials = new NetworkCredential(source_ftp_info1["id"], source_ftp_info1["pw"]);
                                    ftpReq.Method = WebRequestMethods.Ftp.Rename;
                                    ftpReq.RenameTo = new_filename;
                                    ftpReq.Timeout = Global_FunSet.ftpTimeout;
                                    FtpWebResponse resFtp = (FtpWebResponse)ftpReq.GetResponse();
                                    resFtp.Close();

                                    var list_item = source_EQP_path_all1.SingleOrDefault(x => x.eqp_all_filename == items.eqp_all_filename);
                                    list_item.eqp_all_filename = new_filename;

                                    list_set1_eqp_all.DataContext = source_EQP_path_all1;
                                    list_set1_eqp_all.ItemsSource = source_EQP_path_all1;
                                    list_set1_eqp_all.Items.Refresh();
                                }
                                else if (set_flag == 4)
                                {
                                    string uri = this.line_set2_eqp.Text + "/" + items.eqp_all_filename;
                                    FtpWebRequest ftpReq = (FtpWebRequest)WebRequest.Create(new Uri(uri.Replace("#", "%23")));                   //ftp 생성
                                    ftpReq.Credentials = new NetworkCredential(source_ftp_info2["id"], source_ftp_info2["pw"]);
                                    ftpReq.Method = WebRequestMethods.Ftp.Rename;
                                    ftpReq.RenameTo = new_filename;
                                    ftpReq.Timeout = Global_FunSet.ftpTimeout;
                                    FtpWebResponse resFtp = (FtpWebResponse)ftpReq.GetResponse();
                                    resFtp.Close();

                                    var list_item = source_EQP_path_all2.SingleOrDefault(x => x.eqp_all_filename == items.eqp_all_filename);
                                    list_item.eqp_all_filename = new_filename;

                                    list_set2_eqp_all.DataContext = source_EQP_path_all2;
                                    list_set2_eqp_all.ItemsSource = source_EQP_path_all2;
                                    list_set2_eqp_all.Items.Refresh();
                                }
                            }
                            catch
                            {
                                MessageBox.Show("이미 존재하는 폴더입니다.");
                                return;
                            }

                        }

                        //파일인경우
                        else
                        {
                            try
                            {
                                if (set_flag == 3)
                                {
                                    string uri = this.line_set1_eqp.Text + "/" + items.eqp_all_filename;
                                    FtpWebRequest ftpReq = (FtpWebRequest)WebRequest.Create(new Uri(uri.Replace("#", "%23")));                   //ftp 생성
                                    ftpReq.Credentials = new NetworkCredential(source_ftp_info1["id"], source_ftp_info1["pw"]);
                                    ftpReq.Method = WebRequestMethods.Ftp.Rename;
                                    ftpReq.RenameTo = new_filename;
                                    ftpReq.Timeout = Global_FunSet.ftpTimeout;
                                    FtpWebResponse resFtp = (FtpWebResponse)ftpReq.GetResponse();
                                    resFtp.Close();

                                    var list_item = source_EQP_path_all1.SingleOrDefault(x => x.eqp_all_filename == items.eqp_all_filename);
                                    list_item.eqp_all_filename = new_filename;
                                    //list_item.eqp_all_date = dirDate;

                                    list_set1_eqp_all.DataContext = source_EQP_path_all1;
                                    list_set1_eqp_all.ItemsSource = source_EQP_path_all1;
                                    list_set1_eqp_all.Items.Refresh();
                                }
                                else if (set_flag == 4)
                                {
                                    string uri = this.line_set2_eqp.Text + "/" + items.eqp_all_filename;
                                    FtpWebRequest ftpReq = (FtpWebRequest)WebRequest.Create(new Uri(uri.Replace("#", "%23")));                   //ftp 생성
                                    ftpReq.Credentials = new NetworkCredential(source_ftp_info2["id"], source_ftp_info2["pw"]);
                                    ftpReq.Method = WebRequestMethods.Ftp.Rename;
                                    ftpReq.RenameTo = new_filename;
                                    ftpReq.Timeout = Global_FunSet.ftpTimeout;
                                    FtpWebResponse resFtp = (FtpWebResponse)ftpReq.GetResponse();
                                    resFtp.Close();

                                    var list_item = source_EQP_path_all2.SingleOrDefault(x => x.eqp_all_filename == items.eqp_all_filename);
                                    list_item.eqp_all_filename = new_filename;
                                    //list_item.eqp_all_date = dirDate;

                                    list_set2_eqp_all.DataContext = source_EQP_path_all2;
                                    list_set2_eqp_all.ItemsSource = source_EQP_path_all2;
                                    list_set2_eqp_all.Items.Refresh();
                                }
                            }
                            catch
                            {
                                MessageBox.Show("이미 존재하는 파일입니다.");
                                return;
                            }
                            
                            string fileExetension = new_filename.Substring(new_filename.LastIndexOf("."), new_filename.Length - new_filename.LastIndexOf("."));

                            fileIcon = Global_FunSet.ReturnIconPathStr(fileExetension);

                            if (set_flag == 3)
                            {
                                var list_item = source_EQP_path_all1.SingleOrDefault(x => x.eqp_all_filename == items.eqp_all_filename);
                                list_item.eqp_all_filename = new_filename;
                                list_item.eqp_all_extension = fileExetension;
                                list_item.eqp_all_src = fileIcon;

                                list_set1_eqp_all.DataContext = source_EQP_path_all1;
                                list_set1_eqp_all.ItemsSource = source_EQP_path_all1;
                                list_set1_eqp_all.Items.Refresh();
                            }
                            else if (set_flag == 4)
                            {
                                var list_item = source_EQP_path_all2.SingleOrDefault(x => x.eqp_all_filename == items.eqp_all_filename);
                                list_item.eqp_all_filename = new_filename;
                                list_item.eqp_all_extension = fileExetension;
                                list_item.eqp_all_src = fileIcon;

                                list_set2_eqp_all.DataContext = source_EQP_path_all2;
                                list_set2_eqp_all.ItemsSource = source_EQP_path_all2;
                                list_set2_eqp_all.Items.Refresh();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);                
            }
        }

        #region Local, EQP Path, List
        //List 중복 제거 함수
        private void Distinct_ListClass(DataTable dt, int list_idx, int set_flag)
        {
            int i = 0;
            foreach (DataRow row in dt.Rows)
            {
                if (i == 0)
                {
                    if (set_flag == 1)
                    {
                        if (list_idx == 0) source_EQP_list1[0].Add(new EQP_Info_Line1() { strLine_Set1 = row["LINE"].ToString() });
                        else if (list_idx == 1) source_EQP_list1[1].Add(new EQP_Info_EQPTYPE1() { strEQPTYPE_Set1 = row["EQPTYPE"].ToString() });
                        else if (list_idx == 2) source_EQP_list1[2].Add(new EQP_Info_EQPMODEL1() { strEQPMODEL_Set1 = row["EQPMODEL"].ToString() });
                        else if (list_idx == 3) source_EQP_list1[3].Add(new EQP_Info_ZONE1() { strZONE_Set1 = row["ZONE"].ToString() });
                        else if (list_idx == 4) source_EQP_list1[4].Add(new EQP_Info_EQPID1() { strEQPID_Set1 = row["EQPID"].ToString() });
                    }
                    else if (set_flag == 2)
                    {
                        if (list_idx == 0) source_EQP_list2[0].Add(new EQP_Info_Line2() { strLine_Set2 = row["LINE"].ToString() });
                        else if (list_idx == 1) source_EQP_list2[1].Add(new EQP_Info_EQPTYPE2() { strEQPTYPE_Set2 = row["EQPTYPE"].ToString() });
                        else if (list_idx == 2) source_EQP_list2[2].Add(new EQP_Info_EQPMODEL2() { strEQPMODEL_Set2 = row["EQPMODEL"].ToString() });
                        else if (list_idx == 3) source_EQP_list2[3].Add(new EQP_Info_ZONE2() { strZONE_Set2 = row["ZONE"].ToString() });
                        else if (list_idx == 4) source_EQP_list2[4].Add(new EQP_Info_EQPID2() { strEQPID_Set2 = row["EQPID"].ToString() });
                    }
                    else if (set_flag == 3)
                    {
                        if (list_idx == 0) source_EQP_list3[0].Add(new EQP_Info_Line3() { strLine_Set3 = row["LINE"].ToString() });
                        else if (list_idx == 1) source_EQP_list3[1].Add(new EQP_Info_EQPTYPE3() { strEQPTYPE_Set3 = row["EQPTYPE"].ToString() });
                        else if (list_idx == 2) source_EQP_list3[2].Add(new EQP_Info_EQPMODEL3() { strEQPMODEL_Set3 = row["EQPMODEL"].ToString() });
                        else if (list_idx == 3) source_EQP_list3[3].Add(new EQP_Info_ZONE3() { strZONE_Set3 = row["ZONE"].ToString() });
                        else if (list_idx == 4) source_EQP_list3[4].Add(new EQP_Info_EQPID3() { strEQPID_Set3 = row["EQPID"].ToString() });
                    }
                }
                else
                //두번째 이상
                {
                    //Line
                    if (list_idx == 0)
                    {
                        if (set_flag == 1)
                        {
                            int flag = 0;
                            foreach (var item in source_EQP_list1[list_idx])
                            {
                                if (item.strLine_Set1 == row["LINE"].ToString())
                                {
                                    flag = 1;              //중복 발견
                                    break;
                                }
                            }

                            if (flag == 0)      //중복 없어야 아이템 추가
                            {
                                source_EQP_list1[list_idx].Add(new EQP_Info_Line1() { strLine_Set1 = row["LINE"].ToString() });
                            }
                        }
                        else if (set_flag == 2)
                        {
                            int flag = 0;
                            foreach (var item in source_EQP_list2[list_idx])
                            {
                                if (item.strLine_Set2 == row["LINE"].ToString())
                                {
                                    flag = 1;              //중복 발견
                                    break;
                                }
                            }

                            if (flag == 0)      //중복 없어야 아이템 추가
                            {
                                source_EQP_list2[list_idx].Add(new EQP_Info_Line2() { strLine_Set2 = row["LINE"].ToString() });
                            }
                        }
                        else if (set_flag == 3)
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
                                source_EQP_list3[list_idx].Add(new EQP_Info_Line3() { strLine_Set3 = row["LINE"].ToString() });
                            }
                        }
                    }
                    //EQPTYPE
                    else if (list_idx == 1)
                    {
                        if (set_flag == 1)
                        {
                            int flag = 0;
                            foreach (var item in source_EQP_list1[list_idx])
                            {
                                if (item.strEQPTYPE_Set1 == row["EQPTYPE"].ToString())
                                {
                                    flag = 1;              //중복 발견
                                    break;
                                }
                            }

                            if (flag == 0)      //중복 없어야 아이템 추가
                            {
                                source_EQP_list1[list_idx].Add(new EQP_Info_EQPTYPE1() { strEQPTYPE_Set1 = row["EQPTYPE"].ToString() });
                            }
                        }
                        else if (set_flag == 2)
                        {
                            int flag = 0;
                            foreach (var item in source_EQP_list2[list_idx])
                            {
                                if (item.strEQPTYPE_Set2 == row["EQPTYPE"].ToString())
                                {
                                    flag = 1;              //중복 발견
                                    break;
                                }
                            }

                            if (flag == 0)      //중복 없어야 아이템 추가
                            {
                                source_EQP_list2[list_idx].Add(new EQP_Info_EQPTYPE2() { strEQPTYPE_Set2 = row["EQPTYPE"].ToString() });
                            }
                        }
                        else if (set_flag == 3)
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
                                source_EQP_list3[list_idx].Add(new EQP_Info_EQPTYPE3() { strEQPTYPE_Set3 = row["EQPTYPE"].ToString() });
                            }
                        }

                    }
                    //EQPMODEL
                    else if (list_idx == 2)
                    {
                        if (set_flag == 1)
                        {
                            int flag = 0;
                            foreach (var item in source_EQP_list1[list_idx])
                            {
                                if (item.strEQPMODEL_Set1 == row["EQPMODEL"].ToString())
                                {
                                    flag = 1;              //중복 발견
                                    break;
                                }
                            }

                            if (flag == 0)      //중복 없어야 아이템 추가
                            {
                                source_EQP_list1[list_idx].Add(new EQP_Info_EQPMODEL1() { strEQPMODEL_Set1 = row["EQPMODEL"].ToString() });
                            }
                        }
                        else if (set_flag == 2)
                        {
                            int flag = 0;
                            foreach (var item in source_EQP_list2[list_idx])
                            {
                                if (item.strEQPMODEL_Set2 == row["EQPMODEL"].ToString())
                                {
                                    flag = 1;              //중복 발견
                                    break;
                                }
                            }

                            if (flag == 0)      //중복 없어야 아이템 추가
                            {
                                source_EQP_list2[list_idx].Add(new EQP_Info_EQPMODEL2() { strEQPMODEL_Set2 = row["EQPMODEL"].ToString() });
                            }
                        }
                        else if (set_flag == 3)
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
                                source_EQP_list3[list_idx].Add(new EQP_Info_EQPMODEL3() { strEQPMODEL_Set3 = row["EQPMODEL"].ToString() });
                            }
                        }

                    }
                    //ZONE
                    else if (list_idx == 3)
                    {
                        if (set_flag == 1)
                        {
                            int flag = 0;
                            foreach (var item in source_EQP_list1[list_idx])
                            {
                                if (item.strZONE_Set1 == row["ZONE"].ToString())
                                {
                                    flag = 1;              //중복 발견
                                    break;
                                }
                            }

                            if (flag == 0)      //중복 없어야 아이템 추가
                            {
                                source_EQP_list1[list_idx].Add(new EQP_Info_ZONE1() { strZONE_Set1 = row["ZONE"].ToString() });
                            }
                        }
                        else if (set_flag == 2)
                        {
                            int flag = 0;
                            foreach (var item in source_EQP_list2[list_idx])
                            {
                                if (item.strZONE_Set2 == row["ZONE"].ToString())
                                {
                                    flag = 1;              //중복 발견
                                    break;
                                }
                            }

                            if (flag == 0)      //중복 없어야 아이템 추가
                            {
                                source_EQP_list2[list_idx].Add(new EQP_Info_ZONE2() { strZONE_Set2 = row["ZONE"].ToString() });
                            }
                        }
                        else if (set_flag == 3)
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
                                source_EQP_list3[list_idx].Add(new EQP_Info_ZONE3() { strZONE_Set3 = row["ZONE"].ToString() });
                            }
                        }

                    }
                    //EQPID
                    else if (list_idx == 4)
                    {
                        if (set_flag == 1)
                        {
                            int flag = 0;
                            foreach (var item in source_EQP_list1[list_idx])
                            {
                                if (item.strEQPID_Set1 == row["EQPID"].ToString())
                                {
                                    flag = 1;              //중복 발견
                                    break;
                                }
                            }

                            if (flag == 0)      //중복 없어야 아이템 추가
                            {
                                source_EQP_list1[list_idx].Add(new EQP_Info_EQPID1() { strEQPID_Set1 = row["EQPID"].ToString() });

                            }
                        }
                        else if (set_flag == 2)
                        {
                            int flag = 0;
                            foreach (var item in source_EQP_list2[list_idx])
                            {
                                if (item.strEQPID_Set2 == row["EQPID"].ToString())
                                {
                                    flag = 1;              //중복 발견
                                    break;
                                }
                            }

                            if (flag == 0)      //중복 없어야 아이템 추가
                            {
                                source_EQP_list2[list_idx].Add(new EQP_Info_EQPID2() { strEQPID_Set2 = row["EQPID"].ToString() });

                            }
                        }
                        else if (set_flag == 3)
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
                                source_EQP_list3[list_idx].Add(new EQP_Info_EQPID3() { strEQPID_Set3 = row["EQPID"].ToString() });

                            }
                        }

                    }
                }
                i++;
            }
        }
        #region 단축키 설정
        //단축키 설정
        public dynamic lv;
        public dynamic items;
        public dynamic ob_focusedControl;
        public int set_flag;
        public RenameBox renamebox;
        private void Key_Handler(object sender, KeyEventArgs e)
        {            
            if (e.Key == Key.F2)
            {
                IInputElement focusedControl = FocusManager.GetFocusedElement(this);
                //var str_focus = focusedControl.ToString();
                ListViewItem lvitem = null;
                try
                {
                    lvitem = (ListViewItem)focusedControl;
                }
                catch
                {
                    return;
                }
                
                var str_focus = lvitem.Content.ToString();
                var str_chk = str_focus.Substring(str_focus.LastIndexOf(".") + 1, str_focus.Length - str_focus.LastIndexOf(".") - 1);
                if (str_chk == "Local_path_all1") set_flag = 1;
                else if (str_chk == "Local_path_all2") set_flag = 2;
                else if (str_chk == "EQP_path_all1") set_flag = 3;
                else if (str_chk == "EQP_path_all2") set_flag = 4;
                RenameFun(set_flag);                
            }
            else if (e.Key == Key.Delete)
            {
                IInputElement focusedControl = FocusManager.GetFocusedElement(this);
                var f_listview = focusedControl as ListViewItem;
                if (f_listview == null)
                {
                    MessageBox.Show("다시 선택하세요");
                    return;
                }

                var str_chk = f_listview.Content.ToString();
                if (str_chk == null)
                {
                    MessageBox.Show("다시 선택하세요");
                    return;
                }
                //var str_focus = focusedControl.ToString();                
                //var str_chk = str_focus.Substring(str_focus.LastIndexOf(".") + 1, str_focus.Length - str_focus.LastIndexOf(".") - 1);

                //if (str_chk == "Local_path_all1") set_flag = 1;
                //else if (str_chk == "Local_path_all2") set_flag = 2;
                //else if (str_chk == "EQP_path_all1") set_flag = 3;
                //else if (str_chk == "EQP_path_all2") set_flag = 4;

                if (str_chk.Contains("Local_path_all1")) set_flag = 1;
                else if (str_chk.Contains("Local_path_all2")) set_flag = 2;
                else if (str_chk.Contains("EQP_path_all1")) set_flag = 3;
                else if (str_chk.Contains("EQP_path_all2")) set_flag = 4;

                if (set_flag == 1)
                {
                    set_flag = 1;
                    bw_local_del = new BackgroundWorker();
                    bw_local_del.DoWork += new DoWorkEventHandler(bw_local_del_DoWork);
                    bw_local_del.ProgressChanged += bw_local_del_ProgressChanged;
                    bw_local_del.RunWorkerCompleted += bw_local_del_RunWorkerCompleted;
                    bw_local_del.WorkerReportsProgress = true;
                    bw_local_del.RunWorkerAsync(argument: set_flag);
                    //DeleteLocalFileFolder(1);
                }
                else if (set_flag == 2)
                {
                    set_flag = 2;
                    bw_local_del = new BackgroundWorker();
                    bw_local_del.DoWork += new DoWorkEventHandler(bw_local_del_DoWork);
                    bw_local_del.ProgressChanged += bw_local_del_ProgressChanged;
                    bw_local_del.RunWorkerCompleted += bw_local_del_RunWorkerCompleted;
                    bw_local_del.WorkerReportsProgress = true;
                    bw_local_del.RunWorkerAsync(argument: set_flag);
                    //DeleteLocalFileFolder(1);
                }
                else if (set_flag == 3) DeleteEQPFileFoler(1);
                else if (set_flag == 4) DeleteEQPFileFoler(2);
            }
            else if (e.Key == Key.Enter || e.Key == Key.Space)
            {
                IInputElement focusedControl = FocusManager.GetFocusedElement(this);
                                
                if (focusedControl.GetType().ToString().Contains("TextBox"))
                {
                    //지침서 다운
                    var fbox = focusedControl as TextBox;
                    if (fbox.Name == "line_partid")
                    {
                        Thread thread_mguide = new Thread(new ParameterizedThreadStart(search_mguide));
                        thread_mguide.IsBackground = true;
                        thread_mguide.Start();
                    }
                    else if (fbox.Name == "tb_EQPID_EI")
                    {
                        if (this.tb_EQPID_EI.Text == "") return;
                        //설비별 이미지, EQPID 접속
                        //FobackFlag만 전달
                        try
                        {
                            string eqp = this.tb_EQPID_EI.Text;
                            string id = "";
                            string pw = "";
                            string ip = gfun.source_EQP_dict[eqp];
                            string uri = string.Format(@"FTP://{0}:{1}", ip, 21);
                            string drive = "";

                            EQP_EI_Back_path.Clear();
                            EQP_EI_Back_idx.Clear();

                            if (this.combo_EI.SelectedIndex == 0)
                            {
                                id = "LS_C";
                                pw = "LS_C";
                                drive = "C:\\";
                            }
                            else if (this.combo_EI.SelectedIndex == 1)
                            {
                                id = "LS_D";
                                pw = "LS_D";
                                drive = "D:\\";
                            }
                            else if (this.combo_EI.SelectedIndex == 2)
                            {
                                id = "LS_E";
                                pw = "LS_E";
                                drive = "E:\\";
                            }
                            source_ftp_info3["id"] = id;
                            source_ftp_info3["pw"] = pw;
                            source_ftp_info3["uri"] = uri;
                            source_ftp_info3["set_flag"] = "3";
                            source_ftp_info3["eqpid"] = eqp;
                            source_ftp_info3["foback_flag"] = "1";
                            source_ftp_info3["select_idx"] = "0";
                            source_ftp_info3["drive"] = drive;

                            Thread thread_ftp_login = new Thread(new ParameterizedThreadStart(FTP_Login));
                            this.pgb_EI.Value = 0;
                            this.pgb_EI.IsIndeterminate = true;
                            this.pgbTxt_EI.Text = "접속중";
                            this.list_EI.IsHitTestVisible = false;
                            this.line_EI.IsHitTestVisible = false;
                            thread_ftp_login.IsBackground = true;
                            thread_ftp_login.Start(source_ftp_info3);
                            return;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                        
                    }
                }

                var f_listview = focusedControl as ListViewItem;
                if (f_listview == null) return;
                var str_chk = f_listview.Content.ToString();

                if (str_chk.Contains("Local_path_all1"))
                {
                    ListView lv = this.list_set1_local_all;

                    var items = (Local_path_all1)lv.SelectedItem;

                    if (items == null || lv.SelectedItems.Count > 1)
                    {
                        return;
                    }

                    //폴더 더블클릭
                    if (items.local_all_extension == "폴더")
                    {
                        var items_local1 = (Local_path_all1)this.list_set1_local_all.SelectedItem;
                        var src_path = this.line_set1_local.Text + "\\" + items_local1.local_all_filename;

                        MakeLocalListView(src_path, this.list_set1_local_all, this.line_set1_local, 0, 1, 0);

                        //line_text.text_set1_local = this.line_set1_local.Text + "\\" + items.local_all_filename;
                    }

                    //파일 실행
                    else
                    {
                        var run_file = new Process();
                        run_file.StartInfo.FileName = this.line_set1_local.Text + "\\" + items.local_all_filename;
                        run_file.StartInfo.UseShellExecute = true;
                        run_file.Start();
                    }

                    line_text1.text_set1_local = this.line_set1_local.Text;
                    //this.line_set1_local.DataContext = line_text;

                    this.line_set1_local.Focus();
                    this.line_set1_local.Select(this.line_set1_local.Text.Length, 0);
                    this.list_set1_local_all.Focus();
                    try
                    {
                        this.list_set1_local_all.SelectedIndex = 0;
                    }
                    catch { }

                }
                else if (str_chk.Contains("Local_path_all2"))
                {
                    ListView lv = this.list_set2_local_all;

                    var items = (Local_path_all2)lv.SelectedItem;

                    if (items == null || lv.SelectedItems.Count > 1)
                    {
                        return;
                    }

                    //폴더 더블클릭
                    if (items.local_all_extension == "폴더")
                    {
                        var items_local2 = (Local_path_all2)this.list_set2_local_all.SelectedItem;
                        var src_path = this.line_set2_local.Text + "\\" + items_local2.local_all_filename;

                        MakeLocalListView(src_path, this.list_set2_local_all, this.line_set2_local, 0, 2, 0);

                        //line_text.text_set1_local = this.line_set1_local.Text + "\\" + items.local_all_filename;
                    }

                    //파일 실행
                    else
                    {
                        var run_file = new Process();
                        run_file.StartInfo.FileName = this.line_set2_local.Text + "\\" + items.local_all_filename;
                        run_file.StartInfo.UseShellExecute = true;
                        run_file.Start();
                    }

                    line_text2.text_set2_local = this.line_set2_local.Text;
                    //this.line_set1_local.DataContext = line_text;

                    this.line_set2_local.Focus();
                    this.line_set2_local.Select(this.line_set2_local.Text.Length, 0);
                    this.list_set2_local_all.Focus();
                    try
                    {
                        this.list_set2_local_all.SelectedIndex = 0;
                    }
                    catch { }
                }
                else if (str_chk.Contains("EQP_path_all1"))
                {
                    ListView lv = this.list_set1_eqp_all;
                    var items = (EQP_path_all1)lv.SelectedItem;

                    if (items == null || lv.SelectedItems.Count > 1)
                    {
                        return;
                    }

                    if (items.eqp_all_extension == "폴더")
                    {
                        var items_eqp1 = (EQP_path_all1)this.list_set1_eqp_all.SelectedItem;
                        string eqpid = this.cur_eqp_set1.Text.Substring(0, 7);

                        //source_ftp_info1["ip"] = source_EQP_dict[eqpid];
                        //source_ftp_info1["id"] = source_drive_dict[drive];
                        //source_ftp_info1["pw"] = source_drive_dict[drive];
                        //source_ftp_info1["uri"] = string.Format(@"FTP://{0}:{1}/", source_EQP_dict[eqpid], 21);
                        source_ftp_info1["uri"] += "/" + items_eqp1.eqp_all_filename;
                        source_ftp_info1["set_flag"] = "1";
                        source_ftp_info1["foback_flag"] = "1";
                        source_ftp_info1["select_idx"] = "0";

                        Thread thread_ftp_login = new Thread(new ParameterizedThreadStart(FTP_Login));
                        this.Pgb_eqp1.Value = 0;
                        this.Pgb_eqp1.IsIndeterminate = true;
                        this.Spin_eqp1.Visibility = Visibility.Visible;

                        thread_ftp_login.IsBackground = true;
                        thread_ftp_login.Start(source_ftp_info1);
                    }
                    else
                    {
                        MessageBox.Show("FTP에서는 파일을 실행할 수 없습니다.");
                    }
                }
                else if (str_chk.Contains("EQP_path_all2"))
                {
                    ListView lv = this.list_set2_eqp_all;
                    var items = (EQP_path_all2)lv.SelectedItem;

                    if (items == null || lv.SelectedItems.Count > 1)
                    {
                        return;
                    }

                    if (items.eqp_all_extension == "폴더")
                    {
                        var items_eqp2 = (EQP_path_all2)this.list_set2_eqp_all.SelectedItem;
                        string eqpid = this.cur_eqp_set2.Text.Substring(0, 7);

                        //source_ftp_info1["ip"] = source_EQP_dict[eqpid];
                        //source_ftp_info1["id"] = source_drive_dict[drive];
                        //source_ftp_info1["pw"] = source_drive_dict[drive];
                        //source_ftp_info1["uri"] = string.Format(@"FTP://{0}:{1}/", source_EQP_dict[eqpid], 21);
                        source_ftp_info2["uri"] += "/" + items_eqp2.eqp_all_filename;
                        source_ftp_info2["set_flag"] = "2";
                        source_ftp_info2["foback_flag"] = "1";
                        source_ftp_info2["select_idx"] = "0";

                        Thread thread_ftp_login = new Thread(new ParameterizedThreadStart(FTP_Login));
                        this.Pgb_eqp2.Value = 0;
                        this.Pgb_eqp2.IsIndeterminate = true;

                        thread_ftp_login.IsBackground = true;
                        thread_ftp_login.Start(source_ftp_info2);
                    }
                    else
                    {
                        MessageBox.Show("FTP에서는 파일을 실행할 수 없습니다.");
                    }
                }
                else if (str_chk.Contains("Control_Vis"))
                {
                    try
                    {
                        ListView lv = this.list_EI;
                        var items = (Control_Vis)lv.SelectedItem;

                        if (items == null)
                        {
                            return;
                        }

                        if (items.eqp_all_extension == "폴더")
                        {
                            var items_eqp1 = (Control_Vis)this.list_EI.SelectedItem;
                            string eqpid = this.cur_EI_text.Text.Substring(0, 7);

                            source_ftp_info3["uri"] += "/" + items_eqp1.eqp_all_filename;
                            source_ftp_info3["set_flag"] = "3";
                            source_ftp_info3["foback_flag"] = "1";
                            source_ftp_info3["select_idx"] = "0";

                            Thread thread_ftp_login = new Thread(new ParameterizedThreadStart(FTP_Login));
                            this.pgb_EI.Value = 0;
                            this.pgb_EI.IsIndeterminate = true;
                            this.list_EI.IsHitTestVisible = false;
                            thread_ftp_login.IsBackground = true;
                            thread_ftp_login.Start(source_ftp_info3);                            
                        }
                        else
                        {
                            EI_Mk_Img();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    
                }
            }
            else if (e.Key == Key.Back || (Keyboard.IsKeyDown(Key.LeftAlt) && (Keyboard.IsKeyDown(Key.Left))))             //백스페이스 버튼 고민
            {
                IInputElement focusedControl = FocusManager.GetFocusedElement(this);
                var f_listviewitem = focusedControl as ListViewItem;
                var f = focusedControl as ListView;

                if (f_listviewitem == null & f == null) return;
                
                if (f_listviewitem != null)
                {
                    //아이템 선택하였을 때
                    if (f_listviewitem.ToString().Contains("Local_path_all1")) BackListview(1);
                    else if (f_listviewitem.ToString().Contains("Local_path_all2")) BackListview(2);
                    else if (f_listviewitem.ToString().Contains("EQP_path_all1")) BackListview_EQP(1);
                    else if (f_listviewitem.ToString().Contains("EQP_path_all2")) BackListview_EQP(2);
                    else if (f_listviewitem.ToString().Contains("Control_Vis")) BackListview_EQP(3);                    
                }
                else if (f_listviewitem == null)
                {
                    //아이템 선택안했을 때
                    if (f.Name == "list_set1_local_all") BackListview(1);
                    else if (f.Name == "list_set2_local_all") BackListview(2);
                    else if (f.Name == "list_set1_eqp_all") BackListview_EQP(1);
                    else if (f.Name == "list_set2_eqp_all") BackListview_EQP(2);
                    else if (f.Name == "list_EI") BackListview_EQP(3);
                }
                
            }
            else if (Keyboard.IsKeyDown(Key.LeftAlt) && (Keyboard.IsKeyDown(Key.Right)))
            {
                IInputElement focusedControl = FocusManager.GetFocusedElement(this);
                var f_listviewitem = focusedControl as ListViewItem;
                var f = focusedControl as ListView;

                if (f_listviewitem != null)
                {
                    //아이템 선택하였을 때
                    if (f_listviewitem.ToString().Contains("Local_path_all1")) Local_FowardFun(1);
                    else if (f_listviewitem.ToString().Contains("Local_path_all2")) Local_FowardFun(2);
                    else if (f_listviewitem.ToString().Contains("EQP_path_all1")) EQP_FowardFun(1);
                    else if (f_listviewitem.ToString().Contains("EQP_path_all2")) EQP_FowardFun(2);

                }
                else if (f_listviewitem == null)
                {
                    //아이템 선택안했을 때
                    if (f.Name == "list_set1_local_all") Local_FowardFun(1);
                    else if (f.Name == "list_set2_local_all") Local_FowardFun(2);
                    else if (f.Name == "list_set1_eqp_all") EQP_FowardFun(1);
                    else if (f.Name == "list_set2_eqp_all") EQP_FowardFun(2);
                }
                e.Handled = true;
            }
            else if (Keyboard.IsKeyDown(Key.LeftAlt) && (Keyboard.IsKeyDown(Key.T)))
            {
                e.Handled = true;
                IInputElement focusedControl = FocusManager.GetFocusedElement(this);
                var f_listview = focusedControl as ListViewItem;
                if (f_listview == null)
                {
                    var f_listview2 = focusedControl as ListView;
                    string dismem = f_listview2.DisplayMemberPath;

                    if (f_listview2.Name == "List_EQPID_EI") AddListEQPID();
                    else if (f_listview2.Name == "List_EQPID_EI_Rst") SubListEQPID();
                    return;
                }
                var str_chk = f_listview.Content.ToString();

                if (str_chk.Contains("Local_path_all1"))
                {
                    TrLocalToEQP(1, 3, uiList_textbox[0].Text, uiList_textbox[2].Text);
                }
                else if (str_chk.Contains("Local_path_all2"))
                {
                    TrLocalToEQP(2, 4, uiList_textbox[1].Text, uiList_textbox[3].Text);
                }
                else if (str_chk.Contains("EQP_path_all1"))
                {
                    TrEQPToLocal(3, 1, uiList_textbox[2].Text, uiList_textbox[0].Text);
                }
                else if (str_chk.Contains("EQP_path_all2"))
                {
                    TrEQPToLocal(4, 2, uiList_textbox[3].Text, uiList_textbox[1].Text);
                }
                else if (str_chk.Contains("EQP_Info_EQPID3"))
                {
                    AddListEQPID();
                }
                else if (str_chk.Contains("EQP_Info_EISelect"))
                {
                    SubListEQPID();
                }
            }
            else if (Keyboard.IsKeyDown(Key.LeftAlt) && (Keyboard.IsKeyDown(Key.E)))
            {
                e.Handled = true;
                if ((bool)this.chk_set1_extend.IsChecked) this.chk_set1_extend.IsChecked = false;
                else if (!(bool)this.chk_set1_extend.IsChecked) this.chk_set1_extend.IsChecked = true;

                set_extendFun();
            }
            else if (Keyboard.IsKeyDown(Key.LeftAlt) && (Keyboard.IsKeyDown(Key.D)))
            {
                e.Handled = true;
                NewView.ViewDistruibute viewDistruibute = new NewView.ViewDistruibute("LTE_default", "", "default_drive", "dummy_eqp");
                viewDistruibute.Show();
            }
            else if (Keyboard.IsKeyDown(Key.LeftAlt) && (Keyboard.IsKeyDown(Key.D1)))
            {
                e.Handled = true;
                //EQP 이미지 뷰어 
                if ((bool)this.chk_localset1_image.IsChecked) this.chk_localset1_image.IsChecked = false;
                else if (!(bool)this.chk_localset1_image.IsChecked) this.chk_localset1_image.IsChecked = true;

                if ((bool)this.chk_localset1_image.IsChecked)
                {
                    Vis_Img(1);
                }
                else
                {
                    Vis_Img_off(1);
                }
            }
            else if (Keyboard.IsKeyDown(Key.LeftAlt) && (Keyboard.IsKeyDown(Key.D2)))
            {
                //EQP 이미지 뷰어 
                e.Handled = true;
                if ((bool)this.chk_eqpset1_image.IsChecked) this.chk_eqpset1_image.IsChecked = false;
                else if (!(bool)this.chk_eqpset1_image.IsChecked) this.chk_eqpset1_image.IsChecked = true;

                if ((bool)this.chk_eqpset1_image.IsChecked)
                {
                    Vis_Img(3);
                }
                else
                {
                    Vis_Img_off(3);
                }
            }
            else if (Keyboard.IsKeyDown(Key.LeftAlt) && (Keyboard.IsKeyDown(Key.D3)))
            {
                //EQP 이미지 뷰어 
                e.Handled = true;
                if ((bool)this.chk_localset2_image.IsChecked) this.chk_localset2_image.IsChecked = false;
                else if (!(bool)this.chk_localset2_image.IsChecked) this.chk_localset2_image.IsChecked = true;

                if ((bool)this.chk_localset2_image.IsChecked)
                {
                    Vis_Img(2);
                }
                else
                {
                    Vis_Img_off(2);
                }
            }
            else if (Keyboard.IsKeyDown(Key.LeftAlt) && (Keyboard.IsKeyDown(Key.D4)))
            {
                //EQP 이미지 뷰어 
                e.Handled = true;
                if ((bool)this.chk_eqpset2_image.IsChecked) this.chk_eqpset2_image.IsChecked = false;
                else if (!(bool)this.chk_eqpset2_image.IsChecked) this.chk_eqpset2_image.IsChecked = true;

                if ((bool)this.chk_eqpset2_image.IsChecked)
                {
                    Vis_Img(4);
                }
                else
                {
                    Vis_Img_off(4);
                }
            }
            else if (Keyboard.IsKeyDown(Key.LeftAlt) && (Keyboard.IsKeyDown(Key.W)))
            {
                e.Handled = true;
                if (this.tab_main.SelectedIndex + 1 == this.tab_main.Items.Count) this.tab_main.SelectedIndex = 0;
                else this.tab_main.SelectedIndex = this.tab_main.SelectedIndex + 1;
            }
            else if (Keyboard.IsKeyDown(Key.LeftAlt) && (Keyboard.IsKeyDown(Key.Q)))
            {
                e.Handled = true;
                NewView.ViewInfoEQP viewInfoEQP = new NewView.ViewInfoEQP();
                viewInfoEQP.Show();
            }

        }
        #endregion

        //로컬경로 리스트 칼럼 클릭 정렬
        private void list_set1_local_path_Click(object sender, RoutedEventArgs e)
        {
            ListviewColumnSort(sender, e);
        }

        private void list_set2_local_path_Click(object sender, RoutedEventArgs e)
        {
            ListviewColumnSort(sender, e);
        }

        //로컬경로 All 리스트 칼럼 클릭 정렬
        private void list_set1_local_all_Click(object sender, RoutedEventArgs e)
        {            
            NewListColumnSort(sender, e, 1);
            header_flag = 1;
        }

        private void list_set2_local_all_Click(object sender, RoutedEventArgs e)
        {
            NewListColumnSort(sender, e, 2);
            header_flag = 1;
        }

        private void list_set1_eqp_all_Click(object sender, RoutedEventArgs e)
        {
            NewListColumnSort(sender, e, 3);
            header_flag = 1;
        }
        private void list_set2_eqp_all_Click(object sender, RoutedEventArgs e)
        {
            NewListColumnSort(sender, e, 4);
            header_flag = 1;
        }

        private void NewListColumnSort(object sender, RoutedEventArgs e, int set_flag)
        {
            var headerClicked = e.OriginalSource as GridViewColumnHeader;
            string col_name = headerClicked.Content.ToString();
            ListView lv = sender as ListView;
            //이름, 수정날짜, 유형, 크기
            ListSortDirection direction;

            if (headerClicked != null)
            {
                if (headerClicked.Role != GridViewColumnHeaderRole.Padding)
                {
                    if (headerClicked != _lastHeaderClicked)
                    {
                        direction = ListSortDirection.Ascending;
                        //최초 눌렀을 때 오름차순
                        MainListSortAscending(lv, col_name, set_flag);
                    }
                    else
                    {
                        if (_lastDirection == ListSortDirection.Ascending)
                        {
                            direction = ListSortDirection.Descending;
                            //다시 눌렀을 때 내림차순
                            MainListSortDscending(lv, col_name, set_flag);
                        }
                        else
                        {
                            direction = ListSortDirection.Ascending;
                            //다시 눌렀을 때 오름차순
                            MainListSortAscending(lv, col_name, set_flag);
                        }
                    }

                    _lastHeaderClicked = headerClicked;
                    _lastDirection = direction;
                }
            }
        }        
        #endregion

        

        #region list_local_path_SelectionChanged
        static string fileSize;
        static string fileIcon;
        static string[] allfiles;
        public void list_set1_local_path_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            eqp_set_flag = 0;
            line_text1 = new Line_text1();
            var items = (Local_path1)this.list_set1_local_path.SelectedItem;
            var src_path = items.local_path.ToString();

            Local1_Back_Path.Clear();
            Local1_Back_idx.Clear();

            MakeLocalListView(src_path, this.list_set1_local_all, this.line_set1_local, 1, 1, 0);

            eqp_set_flag = 1;
            
        }

        private void list_set2_local_path_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            eqp_set_flag = 0;
            line_text2 = new Line_text2();
            var items = (Local_path2)this.list_set2_local_path.SelectedItem;
            var src_path = items.local_path.ToString();

            Local2_Back_Path.Clear();
            Local2_Back_idx.Clear();

            MakeLocalListView(src_path, this.list_set2_local_all, this.line_set2_local, 1, 2, 0);

            eqp_set_flag = 1;
        }

        private void list_set1_local_path_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (eqp_set_flag == 1)
            {
                eqp_set_flag = 0;
                return;
            }


            line_text1 = new Line_text1();
            var items = (Local_path1)this.list_set1_local_path.SelectedItem;

            if (items == null)
            {
                return;
            }

            var src_path = items.local_path.ToString();

            Local1_Back_Path.Clear();
            Local1_Back_idx.Clear();
            MakeLocalListView(src_path, this.list_set1_local_all, this.line_set1_local, 1, 1, 0);

            eqp_set_flag = 0;
        }

        private void list_set2_local_path_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (eqp_set_flag == 1)
            {
                eqp_set_flag = 0;
                return;
            }


            line_text2 = new Line_text2();
            var items = (Local_path2)this.list_set2_local_path.SelectedItem;

            if (items == null)
            {
                return;
            }

            var src_path = items.local_path.ToString();

            Local2_Back_Path.Clear();
            Local2_Back_idx.Clear();
            MakeLocalListView(src_path, this.list_set2_local_all, this.line_set2_local, 1, 2, 0);

            eqp_set_flag = 0;
        }

        #endregion

        public void list_set1_local_all_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (header_flag == 1)
            {
                header_flag = 0;
                return;
            }

            ListView lv = sender as ListView;

            //var items = (Line_text)this.list_set1_local_all.SelectedItem;
            var items = (Local_path_all1)lv.SelectedItem;

            if (items == null)
            {
                return;
            }

            //폴더 더블클릭
            if (items.local_all_extension == "폴더")
            {
                var items_local1 = (Local_path_all1)this.list_set1_local_all.SelectedItem;
                var src_path = this.line_set1_local.Text + "\\" + items_local1.local_all_filename;

                MakeLocalListView(src_path, this.list_set1_local_all, this.line_set1_local, 0, 1, 0);

                //line_text.text_set1_local = this.line_set1_local.Text + "\\" + items.local_all_filename;
            }

            //파일 실행s
            else
            {
                var run_file = new Process();
                run_file.StartInfo.FileName = this.line_set1_local.Text + "\\" + items.local_all_filename;
                run_file.StartInfo.UseShellExecute = true;
                run_file.Start();
            }

            line_text1.text_set1_local = this.line_set1_local.Text;
            //this.line_set1_local.DataContext = line_text;

            this.line_set1_local.Focus();
            this.line_set1_local.Select(this.line_set1_local.Text.Length, 0);

            
        }

        private void list_set2_local_all_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (header_flag == 1)
            {
                header_flag = 0;
                return;
            }

            ListView lv = sender as ListView;

            //var items = (Line_text)this.list_set1_local_all.SelectedItem;
            var items = (Local_path_all2)lv.SelectedItem;

            if (items == null)
            {
                return;
            }

            //폴더 더블클릭
            if (items.local_all_extension == "폴더")
            {
                var items_local2 = (Local_path_all2)this.list_set2_local_all.SelectedItem;
                var src_path = this.line_set2_local.Text + "\\" + items_local2.local_all_filename;

                MakeLocalListView(src_path, this.list_set2_local_all, this.line_set2_local, 0, 2, 0);

                //line_text.text_set1_local = this.line_set1_local.Text + "\\" + items.local_all_filename;
            }

            //파일 실행
            else
            {
                var run_file = new Process();
                run_file.StartInfo.FileName = this.line_set2_local.Text + "\\" + items.local_all_filename;
                run_file.StartInfo.UseShellExecute = true;
                run_file.Start();
            }

            line_text2.text_set2_local = this.line_set2_local.Text;
            //this.line_set1_local.DataContext = line_text;

            this.line_set2_local.Focus();
            this.line_set2_local.Select(this.line_set2_local.Text.Length, 0);
        }

        BackgroundWorker bw_mkLocalList;
        //로컬경로생성
        public void MakeLocalListView(string src_path, ListView target_lv, TextBox target_tb, int reset_flag, int set_flag, int select_idx)
        {
            //인터락부터
            DirectoryInfo di_temp = new DirectoryInfo(src_path);
            if (!File.Exists(src_path) && !di_temp.Exists)
            {
                MessageBox.Show("경로 이상");
                return;
            }

            uiList_pgb[set_flag - 1].Value = 0;
            uiList_pgb[set_flag - 1].IsIndeterminate = true;
            uiList_pgbTxt[set_flag - 1].Text = "불러오는중";
            uiList_pgbSpinner[set_flag - 1].Visibility = Visibility.Visible;
            uiList_stack[set_flag - 1].IsHitTestVisible = false;

            Dictionary<string, object> bw_dic = new Dictionary<string, object>();
            bw_dic.Add("src_path", src_path);
            bw_dic.Add("target_lv", target_lv);
            bw_dic.Add("target_tb", target_tb);
            bw_dic.Add("reset_flag", reset_flag);
            bw_dic.Add("set_flag", set_flag);
            bw_dic.Add("select_idx", select_idx);

            bw_mkLocalList = new BackgroundWorker();
            bw_mkLocalList.DoWork += new DoWorkEventHandler(bw_mkLocalList_Dowork);
            bw_mkLocalList.ProgressChanged += bw_mkLocalList_ProgressChanged;
            bw_mkLocalList.RunWorkerCompleted += bw_mkLocalList_RunWorkerCompleted;
            bw_mkLocalList.WorkerReportsProgress = true;
            bw_mkLocalList.RunWorkerAsync(argument: bw_dic);
        }

        int bw_mkLocalList_RunWorkerCompleted_flag;
        public void bw_mkLocalList_Dowork(object sender, DoWorkEventArgs e)
        {
            bw_mkLocalList_RunWorkerCompleted_flag = 0;
            //string src_path, ListView target_lv, TextBox target_tb, int reset_flag, int set_flag
            Dictionary<string, object> bw_dic = (Dictionary<string, object>)e.Argument;
            string src_path = bw_dic["src_path"] as string;
            ListView target_lv = bw_dic["target_lv"] as ListView;
            TextBox target_tb = bw_dic["target_tb"] as TextBox;
            int reset_flag = (int)bw_dic["reset_flag"];
            int set_flag = (int)bw_dic["set_flag"];
            int select_idx = (int)bw_dic["select_idx"];

            bw_mkLocalList_RunWorkerCompleted_flag = set_flag;

            if (set_flag == 1)
            {
                line_text1 = new Line_text1();
                source_Local_path_all1.Clear();
            }

            else if (set_flag == 2)
            {
                line_text2 = new Line_text2();
                source_Local_path_all2.Clear();
            }


            try
            {
                //앞뒤 버튼용 경로 리스트                
                if (reset_flag < 2)
                {
                    Dispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate
                    {
                        if (set_flag == 1 && this.line_set1_local.Text != "")
                        {
                            Local1_Back_Path.Add(this.line_set1_local.Text);
                            Local1_Back_idx.Add(this.list_set1_local_all.SelectedIndex);
                        }                            
                        else if (set_flag == 2 && this.line_set2_local.Text != "")
                        {
                            Local2_Back_Path.Add(this.line_set2_local.Text);
                            Local2_Back_idx.Add(this.list_set2_local_all.SelectedIndex);
                        }
                            
                    }));

                }
                allfiles = Directory.GetFiles(src_path);
                if (set_flag == 1) line_text1.text_set1_local = src_path;
                else if (set_flag == 2) line_text2.text_set2_local = src_path;

                //this.line_set1_local.DataContext = line_text;
                Dispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate
                {
                    if (set_flag == 1) target_tb.DataContext = line_text1;
                    else if (set_flag == 2) target_tb.DataContext = line_text2;

                }));

                var alldir = Directory.GetDirectories(src_path);

                foreach (var dir in alldir)
                {
                    var back_idx = dir.LastIndexOf("\\", dir.Length - 1);
                    string dirName = dir.Substring(back_idx + 1, dir.Length - back_idx - 1);
                    string dirSize = "";

                    if (dirName.Substring(0, 1) != "$")
                    {
                        //var dirDate = new DirectoryInfo(dir).GetDirectories("*", SearchOption.AllDirectories).OrderByDescending(d => d.LastWriteTimeUtc).First();
                        DateTime dirDate = Directory.GetLastWriteTime(dir);

                        //var dirDate = new FileInfo(dir).LastWriteTime;
                        String fileExetension = "폴더";
                        Dispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate
                        {
                            if (set_flag == 1) source_Local_path_all1.Add(new Local_path_all1() { local_all_extension = fileExetension, local_all_filename = dirName, local_all_size = dirSize, local_all_date = dirDate, local_all_src = "Resources/icon_folder.png" });
                            else if (set_flag == 2) source_Local_path_all2.Add(new Local_path_all2() { local_all_extension = fileExetension, local_all_filename = dirName, local_all_size = dirSize, local_all_date = dirDate, local_all_src = "Resources/icon_folder.png" });
                        }));

                    }
                }

                foreach (var file in allfiles)
                {
                    var back_idx = file.LastIndexOf("\\", file.Length - 1);
                    string fileName = file.Substring(back_idx + 1, file.Length - back_idx - 1);
                    long raw_fileSize = new FileInfo(file).Length;
                    fileSize = Global_FunSet.ReturnFileSizeStr(raw_fileSize);

                    DateTime fileDate = new FileInfo(file).LastWriteTime;
                    string fileExetension = new FileInfo(file).Extension;

                    fileIcon = Global_FunSet.ReturnIconPathStr(fileExetension);
                    Dispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate
                    {
                        if (set_flag == 1) source_Local_path_all1.Add(new Local_path_all1() { local_all_date = fileDate, local_all_extension = fileExetension, local_all_filename = fileName, local_all_size = fileSize, local_all_src = fileIcon });
                        else if (set_flag == 2) source_Local_path_all2.Add(new Local_path_all2() { local_all_date = fileDate, local_all_extension = fileExetension, local_all_filename = fileName, local_all_size = fileSize, local_all_src = fileIcon });
                    }));

                }

                List<int> temp_flag = new List<int>();
                temp_flag.Add(set_flag);
                temp_flag.Add(reset_flag);
                temp_flag.Add(select_idx);
                e.Result = temp_flag;
            }
            catch (System.UnauthorizedAccessException)
            {
                List<int> temp_flag = new List<int>();
                temp_flag.Add(set_flag);
                temp_flag.Add(-1);
                temp_flag.Add(select_idx);
                e.Result = temp_flag;
            }

            catch
            {
                if (reset_flag == 1)
                {
                    Dispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate
                    {
                        target_lv.ItemsSource = null;
                        target_tb.Text = "";
                    }));
                    List<int> temp_flag = new List<int>();
                    temp_flag.Add(set_flag);
                    temp_flag.Add(reset_flag);
                    temp_flag.Add(select_idx);
                    e.Result = temp_flag;
                }

                
            }

            

        }

        private void bw_mkLocalList_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            // Running on a UI thread            
        }

        private void bw_mkLocalList_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            List<int> temp_flag = (List<int>)e.Result;
            if (temp_flag == null) return;
            int set_flag = temp_flag[0];
            if (set_flag == 0) return;
            int reset_flag = temp_flag[1];
            int select_idx = temp_flag[2];
            
            // Running on a UI thread            
            if (set_flag == 0) return;

            if (reset_flag != -1)
            {
                uiList_pgb[set_flag - 1].Value = 100;
                uiList_pgb[set_flag - 1].IsIndeterminate = false;
                uiList_pgbTxt[set_flag - 1].Text = "완료";
                uiList_pgbSpinner[set_flag - 1].Visibility = Visibility.Collapsed;
                uiList_stack[set_flag - 1].IsHitTestVisible = true;

                if (set_flag == 1)
                {
                    uiList_listView[set_flag - 1].DataContext = source_Local_path_all1;
                    uiList_listView[set_flag - 1].ItemsSource = source_Local_path_all1;
                    source_Local_path_all1.Sort(new SortDefault());
                    uiList_listView[set_flag - 1].Items.Refresh();
                }
                else
                {
                    uiList_listView[set_flag - 1].DataContext = source_Local_path_all2;
                    uiList_listView[set_flag - 1].ItemsSource = source_Local_path_all2;
                    source_Local_path_all2.Sort(new SortDefault());
                    uiList_listView[set_flag - 1].Items.Refresh();
                }

                try
                {
                    uiList_textbox[set_flag - 1].Focus();
                    uiList_textbox[set_flag - 1].Select(uiList_textbox[set_flag - 1].Text.Length, 0);

                    uiList_listView[set_flag - 1].SelectedIndex = select_idx;
                    uiList_listView[set_flag - 1].Focus();
                    uiList_listView[set_flag - 1].ScrollIntoView(uiList_listView[set_flag - 1].Items[select_idx]);

                    ListBoxItem lbi = uiList_listView[set_flag - 1].ItemContainerGenerator.ContainerFromIndex(uiList_listView[set_flag - 1].SelectedIndex) as ListBoxItem;                    

                    if (lbi != null)
                    {

                        lbi.Focus();
                    }

                }
                catch { }
            }
            else
            {
                uiList_pgb[set_flag - 1].Value = 0;
                uiList_pgb[set_flag - 1].IsIndeterminate = false;
                uiList_pgbTxt[set_flag - 1].Text = "권한거부";
                uiList_pgbSpinner[set_flag - 1].Visibility = Visibility.Collapsed;
                uiList_stack[set_flag - 1].IsHitTestVisible = true;
            }
            

            bw_mkLocalList_RunWorkerCompleted_flag = 0;
        }

        //버튼 클릭 이벤트
        private void Local1_BackBtn_Click(object sender, RoutedEventArgs e)
        {
            
            BackListview(1);
        }

        private void Local2_BackBtn_Click(object sender, RoutedEventArgs e)
        {
            BackListview(2);
        }

        private void BackListview(int set_flag)
        {
            if (set_flag == 1 && Local1_Back_Path.Count == 0) return;
            if (set_flag == 2 && Local2_Back_Path.Count == 0) return;
            
            try
            {
                if (set_flag == 1)
                {
                    if (Local1_Back_Path[Local1_Back_Path.Count() - 1] == this.line_set1_local.Text)
                    {
                        Local1_Back_Path.Remove(Local1_Back_Path[Local1_Back_Path.Count() - 1]);
                        Local1_Back_idx.Remove(Local1_Back_idx[Local1_Back_idx.Count() - 1]);
                    }
                        
                }
                else if (set_flag == 2)
                {
                    if (Local2_Back_Path[Local2_Back_Path.Count() - 1] == this.line_set2_local.Text)
                    {
                        Local2_Back_Path.Remove(Local2_Back_Path[Local2_Back_Path.Count() - 1]);
                        Local2_Back_idx.Remove(Local2_Back_idx[Local2_Back_idx.Count() - 1]);
                    }
                        
                }

            }
            catch { }

            try
            {
                if (set_flag == 1)
                {
                    if (Local1_Back_Path == null || Local1_Back_Path.Count() == 0)
                    {
                        return;
                    }
                    else
                    {
                        Local1_Foward_Path.Add(this.line_set1_local.Text);
                        Local1_Foward_idx.Add(this.list_set1_local_all.SelectedIndex);
                        MakeLocalListView(Local1_Back_Path[Local1_Back_Path.Count() - 1], this.list_set1_local_all, this.line_set1_local, 2, 1, Local1_Back_idx[Local1_Back_idx.Count() - 1]);
                        Local1_Back_Path.Remove(Local1_Back_Path[Local1_Back_Path.Count() - 1]);
                        Local1_Back_idx.Remove(Local1_Back_idx[Local1_Back_idx.Count() - 1]);
                    }
                }
                else if (set_flag == 2)
                {
                    if (Local2_Back_Path == null || Local2_Back_Path.Count() == 0)
                    {
                        return;
                    }
                    else
                    {
                        Local2_Foward_Path.Add(this.line_set2_local.Text);
                        Local2_Foward_idx.Add(this.list_set2_local_all.SelectedIndex);
                        MakeLocalListView(Local2_Back_Path[Local2_Back_Path.Count() - 1], this.list_set2_local_all, this.line_set2_local, 2, 2, Local2_Back_idx[Local2_Back_idx.Count() - 1]);
                        Local2_Back_Path.Remove(Local2_Back_Path[Local2_Back_Path.Count() - 1]);
                        Local2_Back_idx.Remove(Local2_Back_idx[Local2_Back_idx.Count() - 1]);
                    }
                }
            }
            catch { }
            
        }

        

        private void Local1_FowardBtn_Click(object sender, RoutedEventArgs e)
        {
            Local_FowardFun(1);
        }

        private void Local2_FowardBtn_Click(object sender, RoutedEventArgs e)
        {
            Local_FowardFun(2);
        }

        private void Local_FowardFun(int set_flag)
        {
            if (set_flag == 1)
            {
                if (Local1_Foward_Path == null || Local1_Foward_Path.Count() == 0)
                {
                    return;
                }
                else
                {
                    Local1_Back_Path.Add(this.line_set1_local.Text);
                    Local1_Back_idx.Add(this.list_set1_local_all.SelectedIndex);
                    MakeLocalListView(Local1_Foward_Path[Local1_Foward_Path.Count() - 1], this.list_set1_local_all, this.line_set1_local, 3, 1, Local1_Foward_idx[Local1_Foward_idx.Count() - 1]);
                    Local1_Foward_Path.Remove(Local1_Foward_Path[Local1_Foward_Path.Count() - 1]);
                    Local1_Foward_idx.Remove(Local1_Foward_idx[Local1_Foward_idx.Count() - 1]);
                }
            }
            else if (set_flag == 2)
            {
                if (Local2_Foward_Path == null || Local2_Foward_Path.Count() == 0)
                {
                    return;
                }
                else
                {
                    Local2_Back_Path.Add(this.line_set2_local.Text);
                    Local2_Back_idx.Add(this.list_set2_local_all.SelectedIndex);
                    MakeLocalListView(Local2_Foward_Path[Local2_Foward_Path.Count() - 1], this.list_set2_local_all, this.line_set2_local, 3, 2, Local1_Foward_idx[Local1_Foward_idx.Count() - 1]);
                    Local2_Foward_Path.Remove(Local2_Foward_Path[Local2_Foward_Path.Count() - 1]);
                    Local2_Foward_idx.Remove(Local2_Foward_idx[Local2_Foward_idx.Count() - 1]);
                }
            }
        }

        private void Local1_UpperBtn_Click(object sender, RoutedEventArgs e)
        {
            var str_path = this.line_set1_local.Text;
            if (str_path.LastIndexOf("\\") != -1)
            {
                str_path = str_path.Substring(0, str_path.LastIndexOf("\\"));
                if (str_path.Substring(str_path.Length - 1, 1) == ":" && str_path.Length < 4)
                {
                    str_path = str_path + "\\";
                }
                MakeLocalListView(str_path, this.list_set1_local_all, this.line_set1_local, 2, 1, 0);
            }

        }

        private void Local2_UpperBtn_Click(object sender, RoutedEventArgs e)
        {
            var str_path = this.line_set2_local.Text;
            if (str_path.LastIndexOf("\\") != -1)
            {
                str_path = str_path.Substring(0, str_path.LastIndexOf("\\"));
                if (str_path.Substring(str_path.Length - 1, 1) == ":" && str_path.Length < 4)
                {
                    str_path = str_path + "\\";
                }
                MakeLocalListView(str_path, this.list_set2_local_all, this.line_set2_local, 2, 2, 0);
            }
        }

        private void Local1_NewFolderBtn_Click(object sender, RoutedEventArgs e)
        {
            LocalNewFolder(1);
        }

        private void Local2_NewFolderBtn_Click(object sender, RoutedEventArgs e)
        {
            LocalNewFolder(2);
        }

        private void LocalNewFolder(int set_flag)
        {
            if (set_flag > 2) return;
            if (uiList_textbox[set_flag - 1].Text == "") return;
            try
            {
                var str_newfolder = "";
                NewFolderInputBox Local_Mk_NewFolder = new NewFolderInputBox();
                if (Local_Mk_NewFolder.ShowDialog() == true)
                {
                    str_newfolder = Local_Mk_NewFolder.Text_NewFolder.Text;

                    if (str_newfolder == "")
                        return;
                }
                else
                {
                    return;
                }
                string path_newfolder = uiList_textbox[set_flag - 1].Text + "\\" + str_newfolder;
                //string path_newfolder = this.line_set1_local.Text + "\\" + str_newfolder;

                if (!Directory.Exists(path_newfolder))
                {
                    Directory.CreateDirectory(path_newfolder);
                }
                else
                {
                    MessageBox.Show("이미 존재하는 폴더입니다.");
                    return;
                }

                var folderdate = Directory.GetLastWriteTime(path_newfolder);

                if (set_flag == 1)
                {
                    source_Local_path_all1.Insert(0, new Local_path_all1()
                    {
                        local_all_date = folderdate,
                        local_all_extension = "폴더",
                        local_all_filename = str_newfolder,
                        local_all_size = "",
                        local_all_src = "Resources/icon_folder.png",
                        IsNew = true
                    }); ;
                    //this.list_set1_local_all.Items.SortDescriptions.Clear();
                    //var view = (ListCollectionView)CollectionViewSource.GetDefaultView(uiList_listView[set_flag - 1].ItemsSource); //Items is an ObservableCollection<T> 
                    //view.CustomSort = new NewCustomSort("IsNew"); //MyComparable implements IComparer
                    this.list_set1_local_all.Items.Refresh();
                    var list_items = from a in source_Local_path_all1
                                     where a.IsNew == true
                                     select a;

                    int i = 0;
                    uiList_listView[set_flag - 1].Focus();
                    uiList_listView[set_flag - 1].SelectedItems.Clear();
                    foreach (var item in list_items)
                    {
                        item.IsNew = false;
                        var j = uiList_listView[set_flag - 1].Items[i];
                        uiList_listView[set_flag - 1].SelectedItems.Add(j);
                        i++;
                    }
                }
                else if (set_flag == 2)
                {
                    source_Local_path_all2.Insert(0, new Local_path_all2()
                    {
                        local_all_date = folderdate,
                        local_all_extension = "폴더",
                        local_all_filename = str_newfolder,
                        local_all_size = "",
                        local_all_src = "Resources/icon_folder.png",
                        IsNew = true
                    });
                    //this.list_set2_local_all.Items.SortDescriptions.Clear();
                    //var view = (ListCollectionView)CollectionViewSource.GetDefaultView(uiList_listView[set_flag - 1].ItemsSource); //Items is an ObservableCollection<T> 
                    //view.CustomSort = new NewCustomSort("IsNew"); //MyComparable implements IComparer
                    this.list_set2_local_all.Items.Refresh();
                    var list_items = from a in source_Local_path_all2
                                     where a.IsNew == true
                                     select a;

                    int i = 0;
                    uiList_listView[set_flag - 1].Focus();
                    uiList_listView[set_flag - 1].SelectedItems.Clear();
                    foreach (var item in list_items)
                    {
                        item.IsNew = false;
                        var j = uiList_listView[set_flag - 1].Items[i];
                        uiList_listView[set_flag - 1].SelectedItems.Add(j);
                        i++;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
                        
        }

        BackgroundWorker bw_local_del;
        private void Local1_DelBtn_Click(object sender, RoutedEventArgs e)
        {
            int set_flag = 1;
            if (uiList_listView[set_flag - 1].SelectedItems.Count == 0) return;
            try
            {
                
                bw_local_del = new BackgroundWorker();
                bw_local_del.DoWork += new DoWorkEventHandler(bw_local_del_DoWork);
                bw_local_del.ProgressChanged += bw_local_del_ProgressChanged;
                bw_local_del.RunWorkerCompleted += bw_local_del_RunWorkerCompleted;
                bw_local_del.WorkerReportsProgress = true;
                bw_local_del.RunWorkerAsync(argument: set_flag);
                //DeleteLocalFileFolder(1);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Local2_DelBtn_Click(object sender, RoutedEventArgs e)
        {
            int set_flag = 1;
            if (uiList_listView[set_flag - 1].SelectedItems.Count == 0) return;
            try
            {                
                bw_local_del = new BackgroundWorker();
                bw_local_del.DoWork += new DoWorkEventHandler(bw_local_del_DoWork);
                bw_local_del.ProgressChanged += bw_local_del_ProgressChanged;
                bw_local_del.RunWorkerCompleted += bw_local_del_RunWorkerCompleted;
                bw_local_del.WorkerReportsProgress = true;
                bw_local_del.RunWorkerAsync(argument: set_flag);
                //DeleteLocalFileFolder(2);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public IList items_local;
        public string str_path;
        public void bw_local_del_DoWork(object sender, DoWorkEventArgs e)
        {
            int set_flag = (int)e.Argument;
            DeleteYesNoBox Local_DelBox = null; 
            Dispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate
            {
                Local_DelBox = new DeleteYesNoBox();
                if (Local_DelBox.ShowDialog() != true) return;

                uiList_pgbSpinner[set_flag - 1].Visibility = Visibility.Visible;
                uiList_stack[set_flag - 1].IsHitTestVisible = false;
                if (set_flag == 1) items_local = this.list_set1_local_all.SelectedItems;
                else if (set_flag == 2) items_local = this.list_set2_local_all.SelectedItems;
            }));

            int del_flag;            

            Local_path_all1 cast_item1 = null;
            Local_path_all2 cast_item2 = null;

            if (items_local == null) return;

            int cnt = items_local.Count;
            int i = 0;

            foreach (var item in items_local)
            {
                i++;
                del_flag = 0;
                if (set_flag == 1)
                {
                    cast_item1 = (Local_path_all1)item;
                    Dispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate
                    {
                        str_path = this.line_set1_local.Text + "\\" + cast_item1.local_all_filename;
                    }));
                }
                else if (set_flag == 2)
                {
                    cast_item2 = (Local_path_all2)item;
                    Dispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate
                    {
                        str_path = this.line_set2_local.Text + "\\" + cast_item2.local_all_filename;
                    }));
                }

                //파일 삭제
                if (File.Exists(str_path))
                {
                    try
                    {
                        File.Delete(str_path);
                    }
                    catch
                    {
                        MessageBox.Show(str_path + "\n삭제불가, 사용중일 수 있습니다.");
                        del_flag = 1;
                        // handle exception
                    }
                }

                //폴더 삭제
                if (Directory.Exists(str_path))
                {
                    try
                    {
                        Directory.Delete(str_path, true);
                    }
                    catch
                    {
                        MessageBox.Show(str_path + "\n삭제불가, 사용중일 수 있습니다.");
                        del_flag = 1;
                        // handle exception
                    }
                }
                if (set_flag == 1 && del_flag != 1)
                {
                    var list_item = source_Local_path_all1.SingleOrDefault(x => x.local_all_filename == cast_item1.local_all_filename);
                    if (list_item != null)
                        source_Local_path_all1.Remove(list_item);
                }
                else if (set_flag == 2 && del_flag != 1)
                {
                    var list_item = source_Local_path_all2.SingleOrDefault(x => x.local_all_filename == cast_item2.local_all_filename);
                    if (list_item != null)
                        source_Local_path_all2.Remove(list_item);
                }
                bw_local_del.ReportProgress((100 / cnt) * i);
            }
            
            e.Result = set_flag;
        }

        private void bw_local_del_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            // Running on a UI thread
            if (set_flag == 1)
            {
                this.Pgb_local1.Value = e.ProgressPercentage;
                this.Pgb_local1_text.Text = e.ProgressPercentage.ToString() + " %";
            }
            else if (set_flag == 2)
            {
                this.Pgb_local2.Value = e.ProgressPercentage;
                this.Pgb_local2_text.Text = e.ProgressPercentage.ToString() + " %";
            }
        }

        private void bw_local_del_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Result == null)
            {
                return;
            }
                
            int set_flag = (int)e.Result;
            // Running on a UI thread
            if (set_flag == 1)
            {
                this.Pgb_local1.Value = 100;
                this.Spin_local1.Visibility = Visibility.Collapsed;                
                this.Pgb_local1_text.Text = "100 %";                                              
                this.list_set1_local_all.Items.Refresh();
            }
            else if (set_flag == 2)
            {
                this.Pgb_local2.Value = 100;
                this.Spin_local2.Visibility = Visibility.Collapsed;                
                this.Pgb_local2_text.Text = "100 %";                                
                this.list_set2_local_all.Items.Refresh();
            }
            uiList_stack[set_flag - 1].IsHitTestVisible = true;
            uiList_pgbSpinner[set_flag - 1].Visibility = Visibility.Collapsed;            
            set_flag = 0;
        }


        private void List_Line_eqpSet1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            EQPID_Filter(1);
        }
        private void List_Line_eqpSet2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            EQPID_Filter(2);
        }
        private void List_EQPTYPE_eqpSet1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            EQPID_Filter(1);
        }
        private void List_EQPTYPE_eqpSet2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            EQPID_Filter(2);
        }
        private void List_EQPMODEL_eqpSet1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            EQPID_Filter(1);
        }
        private void List_EQPMODEL_eqpSet2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            EQPID_Filter(2);
        }
        private void List_ZONE_eqpSet1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            EQPID_Filter(1);
        }
        private void List_ZONE_eqpSet2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            EQPID_Filter(2);
        }
        private void tb_EQPID_eqp1_TextChanged(object sender, TextChangedEventArgs e)
        {
            EQPID_Filter(1);
        }
        private void tb_EQPID_eqp2_TextChanged(object sender, TextChangedEventArgs e)
        {
            EQPID_Filter(2);
        }

        //private void EQPID_Filter(ListBox cur_list, int idx)
        public IList items_Line;
        public IList items_EQPTYPE;
        public IList items_EQPMODEL;
        public IList items_ZONE;
        bool flag_Line;
        bool flag_EQPTYPE;
        bool flag_EQPMODEL;
        bool flag_ZONE;
        bool flag_EQPID;
        private void EQPID_Filter(int set_flag)
        {
            if (set_flag == 1)
            {
                items_Line = this.List_Line_eqpSet1.SelectedItems;
                items_EQPTYPE = this.List_EQPTYPE_eqpSet1.SelectedItems;
                items_EQPMODEL = this.List_EQPMODEL_eqpSet1.SelectedItems;
                items_ZONE = this.List_ZONE_eqpSet1.SelectedItems;
            }
            else if (set_flag == 2)
            {
                items_Line = this.List_Line_eqpSet2.SelectedItems;
                items_EQPTYPE = this.List_EQPTYPE_eqpSet2.SelectedItems;
                items_EQPMODEL = this.List_EQPMODEL_eqpSet2.SelectedItems;
                items_ZONE = this.List_ZONE_eqpSet2.SelectedItems;
            }
            else if (set_flag == 3)
            {
                items_Line = this.List_Line_EI.SelectedItems;
                items_EQPTYPE = this.List_EQPTYPE_EI.SelectedItems;
                items_EQPMODEL = this.List_EQPMODEL_EI.SelectedItems;
                items_ZONE = this.List_ZONE_EI.SelectedItems;
            }

            List<String> list_Line = new List<String>();
            List<String> list_EQPTYPE = new List<String>();
            List<String> list_EQPMODEL = new List<String>();
            List<String> list_ZONE = new List<String>();
            string str_EQPID1 = this.tb_EQPID_eqp1.Text;
            string str_EQPID2 = this.tb_EQPID_eqp2.Text;
            string str_EQPID3 = this.tb_EQPID_EI2.Text;

            for (int i = 0; i < 4; i++)
            {
                if (i == 0)
                {
                    foreach (var item in items_Line)
                    {
                        if (set_flag == 1)
                        {
                            var cast_item = (EQP_Info_Line1)item;
                            list_Line.Add(cast_item.strLine_Set1);
                        }
                        else if (set_flag == 2)
                        {
                            var cast_item = (EQP_Info_Line2)item;
                            list_Line.Add(cast_item.strLine_Set2);
                        }
                        else if (set_flag == 3)
                        {
                            var cast_item = (EQP_Info_Line3)item;
                            list_Line.Add(cast_item.strLine_Set3);
                        }
                    }
                }
                else if (i == 1)
                {
                    foreach (var item in items_EQPTYPE)
                    {
                        if (set_flag == 1)
                        {
                            var cast_item = (EQP_Info_EQPTYPE1)item;
                            list_EQPTYPE.Add(cast_item.strEQPTYPE_Set1);
                        }
                        else if (set_flag == 2)
                        {
                            var cast_item = (EQP_Info_EQPTYPE2)item;
                            list_EQPTYPE.Add(cast_item.strEQPTYPE_Set2);
                        }
                        else if (set_flag == 3)
                        {
                            var cast_item = (EQP_Info_EQPTYPE3)item;
                            list_EQPTYPE.Add(cast_item.strEQPTYPE_Set3);
                        }
                    }
                }
                else if (i == 2)
                {
                    foreach (var item in items_EQPMODEL)
                    {
                        if (set_flag == 1)
                        {
                            var cast_item = (EQP_Info_EQPMODEL1)item;
                            list_EQPMODEL.Add(cast_item.strEQPMODEL_Set1);
                        }
                        else if (set_flag == 2)
                        {
                            var cast_item = (EQP_Info_EQPMODEL2)item;
                            list_EQPMODEL.Add(cast_item.strEQPMODEL_Set2);
                        }
                        else if (set_flag == 3)
                        {
                            var cast_item = (EQP_Info_EQPMODEL3)item;
                            list_EQPMODEL.Add(cast_item.strEQPMODEL_Set3);
                        }
                    }
                }
                else if (i == 3)
                {
                    foreach (var item in items_ZONE)
                    {
                        if (set_flag == 1)
                        {
                            var cast_item = (EQP_Info_ZONE1)item;
                            list_ZONE.Add(cast_item.strZONE_Set1);
                        }
                        else if (set_flag == 2)
                        {
                            var cast_item = (EQP_Info_ZONE2)item;
                            list_ZONE.Add(cast_item.strZONE_Set2);
                        }
                        else if (set_flag == 3)
                        {
                            var cast_item = (EQP_Info_ZONE3)item;
                            list_ZONE.Add(cast_item.strZONE_Set3);
                        }
                    }
                }
            }

            //EQPID 필터링
            //DataTable dt_eqp_info = new DataTable();
            //dt_eqp_info = gfun.GetCSVData("Load_Spectrum2.Resources.Setting_EQP.csv", 2);
            if (set_flag == 1) source_EQP_Info_EQPID1.Clear();
            else if (set_flag == 2) source_EQP_Info_EQPID2.Clear();
            else if (set_flag == 3) source_EQP_Info_EQPID3.Clear();

            foreach (DataRow row in gfun.dt_eqp_info.Rows)
            {

                if (set_flag == 1)
                {
                    flag_Line = this.List_Line_eqpSet1.SelectedItems.Count == 0 ? true : false;
                    flag_EQPTYPE = this.List_EQPTYPE_eqpSet1.SelectedItems.Count == 0 ? true : false;
                    flag_EQPMODEL = this.List_EQPMODEL_eqpSet1.SelectedItems.Count == 0 ? true : false;
                    flag_ZONE = this.List_ZONE_eqpSet1.SelectedItems.Count == 0 ? true : false;
                    flag_EQPID = this.tb_EQPID_eqp1.Text.Length == 0 ? true : false;
                }
                else if (set_flag == 2)
                {
                    flag_Line = this.List_Line_eqpSet2.SelectedItems.Count == 0 ? true : false;
                    flag_EQPTYPE = this.List_EQPTYPE_eqpSet2.SelectedItems.Count == 0 ? true : false;
                    flag_EQPMODEL = this.List_EQPMODEL_eqpSet2.SelectedItems.Count == 0 ? true : false;
                    flag_ZONE = this.List_ZONE_eqpSet2.SelectedItems.Count == 0 ? true : false;
                    flag_EQPID = this.tb_EQPID_eqp2.Text.Length == 0 ? true : false;
                }
                else if (set_flag == 3)
                {
                    flag_Line = this.List_Line_EI.SelectedItems.Count == 0 ? true : false;
                    flag_EQPTYPE = this.List_EQPTYPE_EI.SelectedItems.Count == 0 ? true : false;
                    flag_EQPMODEL = this.List_EQPMODEL_EI.SelectedItems.Count == 0 ? true : false;
                    flag_ZONE = this.List_ZONE_EI.SelectedItems.Count == 0 ? true : false;
                    flag_EQPID = this.tb_EQPID_EI2.Text.Length == 0 ? true : false;
                }

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
                    if (set_flag == 1)
                    {
                        if (this.tb_EQPID_eqp1.Text == row["EQPID"].ToString().Substring(0, this.tb_EQPID_eqp1.Text.Length))
                        {
                            flag_EQPID = true;
                        }
                    }
                    else if (set_flag == 2)
                    {
                        if (this.tb_EQPID_eqp2.Text == row["EQPID"].ToString().Substring(0, this.tb_EQPID_eqp2.Text.Length))
                        {
                            flag_EQPID = true;
                        }
                    }
                    else if (set_flag == 3)
                    {
                        if (this.tb_EQPID_EI2.Text == row["EQPID"].ToString().Substring(0, this.tb_EQPID_EI2.Text.Length))
                        {
                            flag_EQPID = true;
                        }
                    }
                }
                catch
                {
                    return;
                }

                if (flag_Line && flag_EQPTYPE && flag_EQPMODEL && flag_ZONE && flag_EQPID && set_flag == 1)
                {
                    source_EQP_Info_EQPID1.Add(new EQP_Info_EQPID1() { strEQPID_Set1 = row["EQPID"].ToString() });
                }
                else if (flag_Line && flag_EQPTYPE && flag_EQPMODEL && flag_ZONE && flag_EQPID && set_flag == 2)
                {
                    source_EQP_Info_EQPID2.Add(new EQP_Info_EQPID2() { strEQPID_Set2 = row["EQPID"].ToString() });
                }
                else if (flag_Line && flag_EQPTYPE && flag_EQPMODEL && flag_ZONE && flag_EQPID && set_flag == 3)
                {
                    source_EQP_Info_EQPID3.Add(new EQP_Info_EQPID3() { strEQPID_Set3 = row["EQPID"].ToString() });
                }

            }
            if (set_flag == 1)
            {
                this.List_EQPID_eqpSet1.ItemsSource = null;
                this.List_EQPID_eqpSet1.ItemsSource = source_EQP_Info_EQPID1;
            }
            else if (set_flag == 2)
            {
                this.List_EQPID_eqpSet2.ItemsSource = null;
                this.List_EQPID_eqpSet2.ItemsSource = source_EQP_Info_EQPID2;
            }
            else if (set_flag == 3)
            {
                this.List_EQPID_EI.ItemsSource = null;
                this.List_EQPID_EI.ItemsSource = source_EQP_Info_EQPID3;
            }
        }

        private void BackListview_EQP(int set_flag)
        {
            if (set_flag == 1 && EQP1_Back_Path.Count == 0) return;
            if (set_flag == 2 && EQP2_Back_Path.Count == 0) return;
            if (set_flag == 3 && EQP_EI_Back_path.Count == 0) return;
            
            try
            {
                if (set_flag == 1)
                {
                    if (EQP1_Back_Path[EQP1_Back_Path.Count() - 1] == this.line_set1_eqp.Text)
                    {
                        EQP1_Back_Path.Remove(EQP1_Back_Path[EQP1_Back_Path.Count() - 1]);
                        EQP1_Back_idx.Remove(EQP1_Back_idx[EQP1_Back_idx.Count() - 1]);
                    }
                        
                }
                if (set_flag == 2)
                {
                    if (EQP2_Back_Path[EQP2_Back_Path.Count() - 1] == this.line_set2_eqp.Text)
                    {
                        EQP2_Back_Path.Remove(EQP2_Back_Path[EQP2_Back_Path.Count() - 1]);
                        EQP2_Back_idx.Remove(EQP2_Back_idx[EQP2_Back_idx.Count() - 1]);
                    }                        
                }
                if (set_flag == 3)
                {
                    if (EQP_EI_Back_path[EQP_EI_Back_path.Count() - 1] == this.line_EI.Text)
                    {
                        EQP_EI_Back_path.Remove(EQP_EI_Back_path[EQP_EI_Back_path.Count() - 1]);
                        EQP_EI_Back_idx.Remove(EQP_EI_Back_idx[EQP_EI_Back_idx.Count() - 1]);
                    }
                }

            }
            catch { }
            
            try
            {
                if (set_flag == 1)
                {
                    if (EQP1_Back_Path == null || EQP1_Back_Path.Count() == 0 || EQP1_Back_Path[EQP1_Back_Path.Count() - 1] == "")
                    {
                        return;
                    }
                    else
                    {
                        EQP1_Foward_Path.Add(this.line_set1_eqp.Text);
                        EQP1_Foward_idx.Add(this.list_set1_eqp_all.SelectedIndex);

                        source_ftp_info1["uri"] = EQP1_Back_Path[EQP1_Back_Path.Count() - 1];
                        source_ftp_info1["set_flag"] = "1";
                        source_ftp_info1["foback_flag"] = "2";
                        source_ftp_info1["select_idx"] = EQP1_Back_idx[EQP1_Back_idx.Count() - 1].ToString();

                        Thread thread_ftp_login = new Thread(new ParameterizedThreadStart(FTP_Login));
                        this.Pgb_eqp1.Value = 0;
                        this.Pgb_eqp1.IsIndeterminate = true;
                        this.Spin_eqp1.Visibility = Visibility.Visible;

                        thread_ftp_login.IsBackground = true;
                        thread_ftp_login.Start(source_ftp_info1);

                        EQP1_Back_Path.Remove(EQP1_Back_Path[EQP1_Back_Path.Count() - 1]);
                        EQP1_Back_idx.Remove(EQP1_Back_idx[EQP1_Back_idx.Count() - 1]);
                    }
                }
                else if (set_flag == 2)
                {
                    if (EQP2_Back_Path == null || EQP2_Back_Path.Count() == 0 || EQP2_Back_Path[EQP2_Back_Path.Count() - 1] == "")
                    {
                        return;
                    }
                    else
                    {
                        EQP2_Foward_Path.Add(this.line_set2_eqp.Text);
                        EQP2_Foward_idx.Add(this.list_set2_eqp_all.SelectedIndex);

                        source_ftp_info2["uri"] = EQP2_Back_Path[EQP2_Back_Path.Count() - 1];
                        source_ftp_info2["set_flag"] = "2";
                        source_ftp_info2["foback_flag"] = "2";
                        source_ftp_info2["select_idx"] = EQP2_Back_idx[EQP2_Back_idx.Count() - 1].ToString();

                        Thread thread_ftp_login = new Thread(new ParameterizedThreadStart(FTP_Login));
                        this.Pgb_eqp2.Value = 0;
                        this.Pgb_eqp2.IsIndeterminate = true;
                        this.Spin_eqp2.Visibility = Visibility.Visible;

                        thread_ftp_login.IsBackground = true;
                        thread_ftp_login.Start(source_ftp_info2);

                        EQP2_Back_Path.Remove(EQP2_Back_Path[EQP2_Back_Path.Count() - 1]);
                        EQP2_Back_idx.Remove(EQP2_Back_idx[EQP2_Back_idx.Count() - 1]);
                    }
                }
                else if (set_flag == 3)
                {
                    if (EQP_EI_Back_path == null || EQP_EI_Back_path.Count() == 0 || EQP_EI_Back_path[EQP_EI_Back_path.Count() - 1] == "")
                    {
                        return;
                    }
                    else
                    {
                        EQP_EI_Foward_path.Add(this.line_EI.Text);
                        EQP_EI_Foward_idx.Add(this.list_EI.SelectedIndex);

                        source_ftp_info3["uri"] = EQP_EI_Back_path[EQP_EI_Back_path.Count() - 1];
                        source_ftp_info3["set_flag"] = "3";
                        source_ftp_info3["foback_flag"] = "2";
                        source_ftp_info3["select_idx"] = EQP_EI_Back_idx[EQP_EI_Back_idx.Count() - 1].ToString();

                        Thread thread_ftp_login = new Thread(new ParameterizedThreadStart(FTP_Login));
                        this.pgb_EI.Value = 0;
                        this.pgb_EI.IsIndeterminate = true;
                        this.list_EI.IsHitTestVisible = false;
                        thread_ftp_login.IsBackground = true;
                        thread_ftp_login.Start(source_ftp_info3);

                        EQP_EI_Back_path.Remove(EQP_EI_Back_path[EQP_EI_Back_path.Count() - 1]);
                        EQP_EI_Back_idx.Remove(EQP_EI_Back_idx[EQP_EI_Back_idx.Count() - 1]);
                    }
                }
            }
            catch { }            
        }

        private void LineResetBtn_EQP1_Click(object sender, RoutedEventArgs e)
        {
            this.List_Line_eqpSet1.SelectedIndex = -1;
        }
        private void LineResetBtn_EQP2_Click(object sender, RoutedEventArgs e)
        {
            this.List_Line_eqpSet2.SelectedIndex = -1;
        }
        private void EQPTYPEResetBtn_EQP1_Click(object sender, RoutedEventArgs e)
        {
            this.List_EQPTYPE_eqpSet1.SelectedIndex = -1;
        }
        private void EQPTYPEResetBtn_EQP2_Click(object sender, RoutedEventArgs e)
        {
            this.List_EQPTYPE_eqpSet2.SelectedIndex = -1;
        }
        private void EQPMODELResetBtn_EQP1_Click(object sender, RoutedEventArgs e)
        {
            this.List_EQPMODEL_eqpSet1.SelectedIndex = -1;
        }
        private void EQPMODELResetBtn_EQP2_Click(object sender, RoutedEventArgs e)
        {
            this.List_EQPMODEL_eqpSet2.SelectedIndex = -1;
        }
        private void ZONEResetBtn_EQP1_Click(object sender, RoutedEventArgs e)
        {
            this.List_ZONE_eqpSet1.SelectedIndex = -1;
        }
        private void ZONEResetBtn_EQP2_Click(object sender, RoutedEventArgs e)
        {
            this.List_ZONE_eqpSet2.SelectedIndex = -1;
        }
        private void EQPIDResetBtn_EQP1_Click(object sender, RoutedEventArgs e)
        {
            this.List_Line_eqpSet1.SelectedIndex = -1;
            this.List_EQPTYPE_eqpSet1.SelectedIndex = -1;
            this.List_EQPMODEL_eqpSet1.SelectedIndex = -1;
            this.List_ZONE_eqpSet1.SelectedIndex = -1;
            this.tb_EQPID_eqp1.Text = "";
        }
        private void EQPIDResetBtn_EQP2_Click(object sender, RoutedEventArgs e)
        {
            this.List_Line_eqpSet2.SelectedIndex = -1;
            this.List_EQPTYPE_eqpSet2.SelectedIndex = -1;
            this.List_EQPMODEL_eqpSet2.SelectedIndex = -1;
            this.List_ZONE_eqpSet2.SelectedIndex = -1;
            this.tb_EQPID_eqp2.Text = "";
        }
        

        private void Eqp1_BackBtn_Click(object sender, RoutedEventArgs e)
        {
            BackListview_EQP(1);
        }
        private void Eqp2_BackBtn_Click(object sender, RoutedEventArgs e)
        {
            BackListview_EQP(2);
        }
        private void Eqp1_FowardBtn_Click(object sender, RoutedEventArgs e)
        {
            EQP_FowardFun(1);
        }
        private void Eqp2_FowardBtn_Click(object sender, RoutedEventArgs e)
        {
            EQP_FowardFun(2);
        }

        private void EQP_FowardFun(int set_flag)
        {
            if (set_flag == 1)
            {
                if (EQP1_Foward_Path == null || EQP1_Foward_Path.Count() == 0)
                {
                    return;
                }
                else
                {
                    EQP1_Back_Path.Add(this.line_set1_eqp.Text);
                    EQP1_Back_idx.Add(this.list_set1_eqp_all.SelectedIndex);

                    source_ftp_info1["uri"] = EQP1_Foward_Path[EQP1_Foward_Path.Count() - 1];
                    source_ftp_info1["set_flag"] = "1";
                    source_ftp_info1["foback_flag"] = "2";
                    source_ftp_info1["select_idx"] = EQP1_Foward_idx[EQP1_Foward_idx.Count() - 1].ToString();

                    Thread thread_ftp_login = new Thread(new ParameterizedThreadStart(FTP_Login));
                    this.Pgb_eqp1.Value = 0;
                    this.Pgb_eqp1.IsIndeterminate = true;
                    this.Spin_eqp1.Visibility = Visibility.Visible;
                    thread_ftp_login.IsBackground = true;
                    thread_ftp_login.Start(source_ftp_info1);

                    EQP1_Foward_Path.Remove(EQP1_Foward_Path[EQP1_Foward_Path.Count() - 1]);
                    EQP1_Foward_idx.Remove(EQP1_Foward_idx[EQP1_Foward_idx.Count() - 1]);
                }
            }
            else if (set_flag == 2)
            {
                if (EQP2_Foward_Path == null || EQP2_Foward_Path.Count() == 0)
                {
                    return;
                }
                else
                {
                    EQP2_Back_Path.Add(this.line_set2_eqp.Text);
                    EQP2_Back_idx.Add(this.list_set2_eqp_all.SelectedIndex);

                    source_ftp_info2["uri"] = EQP2_Foward_Path[EQP2_Foward_Path.Count() - 1];
                    source_ftp_info2["set_flag"] = "2";
                    source_ftp_info2["foback_flag"] = "2";
                    source_ftp_info2["select_idx"] = EQP2_Foward_idx[EQP2_Foward_idx.Count() - 1].ToString();

                    Thread thread_ftp_login = new Thread(new ParameterizedThreadStart(FTP_Login));
                    this.Pgb_eqp2.Value = 0;
                    this.Pgb_eqp2.IsIndeterminate = true;
                    this.Spin_eqp2.Visibility = Visibility.Visible;
                    thread_ftp_login.IsBackground = true;
                    thread_ftp_login.Start(source_ftp_info2);

                    EQP2_Foward_Path.Remove(EQP2_Foward_Path[EQP2_Foward_Path.Count() - 1]);
                    EQP2_Foward_idx.Remove(EQP2_Foward_idx[EQP2_Foward_idx.Count() - 1]);
                }
            }
            else if (set_flag == 3)
            {
                if (EQP_EI_Foward_path == null || EQP_EI_Foward_path.Count() == 0)
                {
                    return;
                }
                else
                {
                    EQP_EI_Back_path.Add(this.line_EI.Text);
                    EQP_EI_Back_idx.Add(this.list_EI.SelectedIndex);

                    source_ftp_info3["uri"] = EQP_EI_Foward_path[EQP_EI_Foward_path.Count() - 1];
                    source_ftp_info3["set_flag"] = "3";
                    source_ftp_info3["foback_flag"] = "2";
                    source_ftp_info3["select_idx"] = EQP_EI_Foward_idx[EQP_EI_Foward_idx.Count() - 1].ToString();

                    Thread thread_ftp_login = new Thread(new ParameterizedThreadStart(FTP_Login));
                    this.pgb_EI.Value = 0;
                    this.pgb_EI.IsIndeterminate = true;
                    thread_ftp_login.IsBackground = true;
                    thread_ftp_login.Start(source_ftp_info3);

                    EQP_EI_Foward_path.Remove(EQP_EI_Foward_path[EQP_EI_Foward_path.Count() - 1]);
                    EQP_EI_Foward_idx.Remove(EQP_EI_Foward_idx[EQP_EI_Foward_idx.Count() - 1]);
                }
            }
        }

        private void Eqp1_UpperBtn_Click(object sender, RoutedEventArgs e)
        {
            var str_path = this.line_set1_eqp.Text;
            int count = str_path.Count(f => f == '/');
            if (count <= 2) return;

            if (str_path.LastIndexOf("/") != -1)
            {
                str_path = str_path.Substring(0, str_path.LastIndexOf("/"));
                if (str_path.Substring(str_path.Length - 1, 1) == ":" && str_path.Length < 7)
                {
                    str_path = str_path + "/";
                }

                //폴더 생성하고 난 다음 리스트 업데이트
                source_ftp_info1["uri"] = str_path;
                source_ftp_info1["set_flag"] = "1";
                source_ftp_info1["foback_flag"] = "2";

                Thread thread_ftp_login = new Thread(new ParameterizedThreadStart(FTP_Login));
                this.Pgb_eqp1.Value = 0;
                this.Pgb_eqp1.IsIndeterminate = true;
                this.Spin_eqp1.Visibility = Visibility.Visible;                

                thread_ftp_login.IsBackground = true;
                thread_ftp_login.Start(source_ftp_info1);

            }
        }
        private void Eqp2_UpperBtn_Click(object sender, RoutedEventArgs e)
        {
            var str_path = this.line_set2_eqp.Text;
            int count = str_path.Count(f => f == '/');
            if (count <= 2) return;

            if (str_path.LastIndexOf("/") != -1)
            {
                str_path = str_path.Substring(0, str_path.LastIndexOf("/"));
                if (str_path.Substring(str_path.Length - 1, 1) == ":" && str_path.Length < 7)
                {
                    str_path = str_path + "/";
                }

                //폴더 생성하고 난 다음 리스트 업데이트
                source_ftp_info2["uri"] = str_path;
                source_ftp_info2["set_flag"] = "2";
                source_ftp_info2["foback_flag"] = "2";

                Thread thread_ftp_login = new Thread(new ParameterizedThreadStart(FTP_Login));
                this.Pgb_eqp2.Value = 0;
                this.Pgb_eqp2.IsIndeterminate = true;
                this.Spin_eqp2.Visibility = Visibility.Visible;                

                thread_ftp_login.IsBackground = true;
                thread_ftp_login.Start(source_ftp_info2);
            }
        }

        private void Eqp1_NewFolderBtn_Click(object sender, RoutedEventArgs e)
        {
            NewFolderEQP(1);
        }
        private void Eqp2_NewFolderBtn_Click(object sender, RoutedEventArgs e)
        {
            NewFolderEQP(2);
        }

        private void NewFolderEQP(int set_flag)
        {
            var str_newfolder = "";

            if (set_flag == 1 && this.line_set1_eqp.Text == "") return;
            else if (set_flag == 2 && this.line_set2_eqp.Text == "") return;

            string id = "";
            string pw = "";

            if (set_flag - 1 == 2)
            {
                id = source_ftp_info1["id"];
                pw = source_ftp_info1["pw"];
            }
            else if (set_flag - 1 == 3)
            {
                id = source_ftp_info2["id"];
                pw = source_ftp_info2["pw"];
            }

            NewFolderInputBox EQP_Mk_NewFolder = new NewFolderInputBox();
            if (EQP_Mk_NewFolder.ShowDialog() == true)
            {
                str_newfolder = EQP_Mk_NewFolder.Text_NewFolder.Text;

                if (str_newfolder == "")
                    return;
            }
            else
            {
                return;
            }
            string path_newfolder = "";
            if (set_flag == 1) path_newfolder = this.line_set1_eqp.Text + "/" + str_newfolder;
            else if (set_flag == 2) path_newfolder = this.line_set2_eqp.Text + "/" + str_newfolder;


            //ftp 폴더 생성            
            if (!Global_FunSet.IsExistDirectory(path_newfolder, id, pw))
            {
                try
                {
                    //폴더 생성
                    var request = (FtpWebRequest)WebRequest.Create(new Uri(path_newfolder.Replace("#", "%23")));
                    if (set_flag == 1) request.Credentials = new NetworkCredential(source_ftp_info1["id"], source_ftp_info1["pw"]);
                    else if (set_flag == 2) request.Credentials = new NetworkCredential(source_ftp_info2["id"], source_ftp_info2["pw"]);
                    request.UseBinary = true;
                    request.UsePassive = true;
                    request.Method = WebRequestMethods.Ftp.MakeDirectory;
                    request.Timeout = Global_FunSet.ftpTimeout;
                    FtpWebResponse resFtp = (FtpWebResponse)request.GetResponse();

                    if (set_flag == 1)
                    {
                        source_EQP_path_all1.Insert(0, new EQP_path_all1()
                        {
                            eqp_all_date = DateTime.Now,
                            eqp_all_extension = "폴더",
                            eqp_all_filename = str_newfolder,
                            eqp_all_size = "",
                            eqp_all_src = "Resources/icon_folder.png",
                            IsNew = true
                        });
                        resFtp.Close();

                        //this.list_set1_eqp_all.Items.SortDescriptions.Clear();
                        //var view = (ListCollectionView)CollectionViewSource.GetDefaultView(uiList_listView[set_flag + 1].ItemsSource); //Items is an ObservableCollection<T> 
                        //view.CustomSort = new NewCustomSort("IsNew"); //MyComparable implements IComparer
                        this.list_set1_eqp_all.Items.Refresh();
                        var list_items = from a in source_EQP_path_all1
                                         where a.IsNew == true
                                         select a;

                        int i = 0;
                        uiList_listView[set_flag + 1].Focus();
                        uiList_listView[set_flag + 1].SelectedItems.Clear();
                        foreach (var item in list_items)
                        {
                            item.IsNew = false;
                            var j = uiList_listView[set_flag + 1].Items[i];
                            uiList_listView[set_flag + 1].SelectedItems.Add(j);
                            i++;
                        }
                    }
                    else if (set_flag == 2)
                    {
                        source_EQP_path_all2.Insert(0, new EQP_path_all2()
                        {
                            eqp_all_date = DateTime.Now,
                            eqp_all_extension = "폴더",
                            eqp_all_filename = str_newfolder,
                            eqp_all_size = "",
                            eqp_all_src = "Resources/icon_folder.png",
                            IsNew = true
                        });
                        resFtp.Close();

                        //this.list_set2_eqp_all.Items.SortDescriptions.Clear();
                        //var view = (ListCollectionView)CollectionViewSource.GetDefaultView(uiList_listView[set_flag + 1].ItemsSource); //Items is an ObservableCollection<T> 
                        //view.CustomSort = new NewCustomSort("IsNew"); //MyComparable implements IComparer
                        this.list_set2_eqp_all.Items.Refresh();
                        var list_items = from a in source_EQP_path_all2
                                         where a.IsNew == true
                                         select a;

                        int i = 0;
                        uiList_listView[set_flag + 1].Focus();
                        uiList_listView[set_flag + 1].SelectedItems.Clear();
                        foreach (var item in list_items)
                        {
                            item.IsNew = false;
                            var j = uiList_listView[set_flag + 1].Items[i];
                            uiList_listView[set_flag + 1].SelectedItems.Add(j);
                            i++;
                        }
                    }
                }
                catch
                {
                    MessageBox.Show("폴더 생성 실패, 연결 상태 확인 필요");
                    return;
                }
            }
        }

        private void Eqp1_DelBtn_Click(object sender, RoutedEventArgs e)
        {
            DeleteEQPFileFoler(1);
        }
        private void Eqp2_DelBtn_Click(object sender, RoutedEventArgs e)
        {
            DeleteEQPFileFoler(2);
        }

        public IList items_eqp;
        public string str_path_eqp;
        BackgroundWorker bw;
        int eqp_set_flag;

        private void DeleteEQPFileFoler(object o)
        {
            int set_flag = (int)o;
            if (set_flag == 1) eqp_set_flag = 1;
            else if (set_flag == 2) eqp_set_flag = 2;
            try
            {
                DeleteYesNoBox EQP_DelBox = new DeleteYesNoBox();
                if (EQP_DelBox.ShowDialog() == true)
                {

                }
                else
                {
                    return;
                }
                bw = new BackgroundWorker();
                bw.DoWork += new DoWorkEventHandler(bw_DoWork);
                bw.ProgressChanged += bw_ProgressChanged;
                bw.RunWorkerCompleted += bw_RunWorkerCompleted;
                bw.WorkerReportsProgress = true;
                bw.RunWorkerAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            // Running on a worker thread
            Dispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate
            {

                if (eqp_set_flag == 1)
                {
                    this.Pgb_eqp1.Value = 0;
                    this.Pgb_eqp1_text.Text = "삭제중";
                    uiList_pgbSpinner[2].Visibility = Visibility.Visible;
                    uiList_stack[2].IsHitTestVisible = false;
                    items_eqp = this.list_set1_eqp_all.SelectedItems;
                }

                else if (eqp_set_flag == 2)
                {
                    this.Pgb_eqp2.Value = 0;
                    this.Pgb_eqp2_text.Text = "삭제중";
                    uiList_pgbSpinner[3].Visibility = Visibility.Visible;
                    uiList_stack[3].IsHitTestVisible = false;
                    items_eqp = this.list_set2_eqp_all.SelectedItems;
                }

            }));

            string id = "";
            string pw = "";

            if (eqp_set_flag == 1)
            {
                id = source_ftp_info1["id"];
                pw = source_ftp_info1["pw"];
            }
            else if (eqp_set_flag == 2)
            {
                id = source_ftp_info2["id"];
                pw = source_ftp_info2["pw"];
            }

            EQP_path_all1 cast_item1 = null;
            EQP_path_all2 cast_item2 = null;

            int cnt = items_eqp.Count;
            int i = 0;
            foreach (var item in items_eqp)
            {
                i++;
                if (eqp_set_flag == 1)
                {
                    cast_item1 = (EQP_path_all1)item;
                    Dispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate
                    {
                        str_path_eqp = this.line_set1_eqp.Text + "/" + cast_item1.eqp_all_filename;
                    }));
                }
                if (eqp_set_flag == 2)
                {
                    cast_item2 = (EQP_path_all2)item;
                    Dispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate
                    {
                        str_path_eqp = this.line_set2_eqp.Text + "/" + cast_item2.eqp_all_filename;
                    }));
                }

                //파일 삭제 FTP경로에서                
                if (eqp_set_flag == 1 && Global_FunSet.IsExistFile(str_path_eqp, id, pw))
                {
                    try
                    {
                        var request = (FtpWebRequest)WebRequest.Create(new Uri(str_path_eqp.Replace("#","%23")));
                        request.Credentials = new NetworkCredential(source_ftp_info1["id"], source_ftp_info1["pw"]);
                        request.Method = WebRequestMethods.Ftp.DeleteFile;
                        request.Timeout = Global_FunSet.ftpTimeout;
                        FtpWebResponse resFtp = (FtpWebResponse)request.GetResponse();
                        resFtp.Close();
                        //Thread thread_ftp_del = new Thread(new ParameterizedThreadStart(DeleteFTPFile));
                        //thread_ftp_del.IsBackground = true;
                        //thread_ftp_del.Start(str_path_eqp);
                    }
                    catch
                    {
                        MessageBox.Show("파일 삭제 실패 : " + cast_item1.eqp_all_filename);
                        continue;
                    }
                }
                else if (eqp_set_flag == 2 && Global_FunSet.IsExistFile(str_path_eqp, id, pw))
                {
                    try
                    {
                        var request = (FtpWebRequest)WebRequest.Create(new Uri(str_path_eqp.Replace("#","%23")));
                        request.Credentials = new NetworkCredential(source_ftp_info2["id"], source_ftp_info2["pw"]);
                        request.Method = WebRequestMethods.Ftp.DeleteFile;
                        request.Timeout = Global_FunSet.ftpTimeout;
                        FtpWebResponse resFtp = (FtpWebResponse)request.GetResponse();
                        resFtp.Close();
                        //Thread thread_ftp_del = new Thread(new ParameterizedThreadStart(DeleteFTPFile));
                        //thread_ftp_del.IsBackground = true;
                        //thread_ftp_del.Start(str_path_eqp);
                    }
                    catch
                    {
                        MessageBox.Show("파일 삭제 실패 : " + cast_item2.eqp_all_filename);
                        continue;
                    }
                }

                //폴더 삭제                
                if (eqp_set_flag == 1 && Global_FunSet.IsExistDirectory(str_path_eqp, id, pw))
                {
                    try
                    {
                        NetworkCredential credentials = new NetworkCredential(source_ftp_info1["id"], source_ftp_info1["pw"]);
                        List<object> list_temp = new List<object>();
                        list_temp.Add(credentials);
                        list_temp.Add(str_path_eqp);
                        list_temp.Add(eqp_set_flag);

                        DeleteFtpDirectory(list_temp);

                        //Thread thread_ftp_del = new Thread(new ParameterizedThreadStart(DeleteFtpDirectory));
                        //thread_ftp_del.IsBackground = true;
                        //thread_ftp_del.Start(list_temp);

                    }
                    catch
                    {
                        MessageBox.Show("폴더 삭제 실패 : " + cast_item1.eqp_all_filename);
                        continue;
                    }
                }
                else if (eqp_set_flag == 2 && Global_FunSet.IsExistDirectory(str_path_eqp, id, pw))
                {
                    try
                    {
                        NetworkCredential credentials = new NetworkCredential(source_ftp_info2["id"], source_ftp_info2["pw"]);
                        List<object> list_temp = new List<object>();
                        list_temp.Add(credentials);
                        list_temp.Add(str_path_eqp);
                        list_temp.Add(eqp_set_flag);

                        DeleteFtpDirectory(list_temp);

                        //Thread thread_ftp_del = new Thread(new ParameterizedThreadStart(DeleteFtpDirectory));
                        //thread_ftp_del.IsBackground = true;
                        //thread_ftp_del.Start(list_temp);

                    }
                    catch
                    {
                        MessageBox.Show("폴더 삭제 실패 : " + cast_item2.eqp_all_filename);
                        continue;
                    }
                }

                if (eqp_set_flag == 1)
                {
                    var list_item1 = source_EQP_path_all1.SingleOrDefault(x => x.eqp_all_filename == cast_item1.eqp_all_filename);
                    if (list_item1 != null)
                        source_EQP_path_all1.Remove(list_item1);
                }
                else if (eqp_set_flag == 2)
                {
                    var list_item2 = source_EQP_path_all2.SingleOrDefault(x => x.eqp_all_filename == cast_item2.eqp_all_filename);
                    if (list_item2 != null)
                        source_EQP_path_all2.Remove(list_item2);
                }
                bw.ReportProgress((100 / cnt) * i);
            }
        }

        private void bw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            // Running on a UI thread
            if (eqp_set_flag == 1)
            {
                this.Pgb_eqp1.Value = e.ProgressPercentage;
                this.Pgb_eqp1_text.Text = e.ProgressPercentage.ToString() + " %";
            }
            else if (eqp_set_flag == 2)
            {
                this.Pgb_eqp2.Value = e.ProgressPercentage;
                this.Pgb_eqp2_text.Text = e.ProgressPercentage.ToString() + " %";
            }
        }

        private void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // Running on a UI thread
            if (eqp_set_flag == 1)
            {
                this.Pgb_eqp1.Value = 100;
                this.Spin_eqp1.Visibility = Visibility.Collapsed;                
                this.Pgb_eqp1_text.Text = "100 %";
                if (eqp_set_flag == 1)
                {
                    this.list_set1_eqp_all.Items.SortDescriptions.Clear();
                    //this.list_set1_eqp_all.Items.SortDescriptions.Add(new SortDescription("eqp_all_extension", ListSortDirection.Descending));
                    //this.list_set1_eqp_all.Items.SortDescriptions.Add(new SortDescription("eqp_all_filename", ListSortDirection.Ascending));
                    this.list_set1_eqp_all.Items.Refresh();
                }
                uiList_stack[2].IsHitTestVisible = true;

            }
            else if (eqp_set_flag == 2)
            {
                this.Pgb_eqp2.Value = 100;
                this.Spin_eqp2.Visibility = Visibility.Collapsed;                
                this.Pgb_eqp2_text.Text = "100 %";
                if (eqp_set_flag == 2)
                {
                    this.list_set2_eqp_all.Items.SortDescriptions.Clear();
                    //this.list_set2_eqp_all.Items.SortDescriptions.Add(new SortDescription("eqp_all_extension", ListSortDirection.Descending));
                    //this.list_set2_eqp_all.Items.SortDescriptions.Add(new SortDescription("eqp_all_filename", ListSortDirection.Ascending));
                    this.list_set2_eqp_all.Items.Refresh();
                }
                uiList_stack[3].IsHitTestVisible = true;

            }
            eqp_set_flag = 0;
        }

        private void DeleteFTPFile(object o)
        {
            string str_path_eqp = (string)o;
            var request = (FtpWebRequest)WebRequest.Create(new Uri(str_path_eqp.Replace("#","%23")));
            if (eqp_set_flag == 1) request.Credentials = new NetworkCredential(source_ftp_info1["id"], source_ftp_info1["pw"]);
            if (eqp_set_flag == 2) request.Credentials = new NetworkCredential(source_ftp_info2["id"], source_ftp_info2["pw"]);
            request.Timeout = Global_FunSet.ftpTimeout;
            request.Method = WebRequestMethods.Ftp.DeleteFile;
            FtpWebResponse resFtp = (FtpWebResponse)request.GetResponse();
            resFtp.Close();
        }

        //ftp 폴더 삭제 함수
        private void DeleteFtpDirectory(object o)
        {
            List<object> list_temp = new List<object>();
            list_temp = (List<object>)o;

            NetworkCredential credentials = (NetworkCredential)list_temp[0];
            string url = (string)list_temp[1];

            var listRequest = (FtpWebRequest)WebRequest.Create(new Uri(url.Replace("#","%23")));
            listRequest.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
            listRequest.Timeout = Global_FunSet.ftpTimeout;
            listRequest.Credentials = credentials;

            List<string> lines = new List<string>();

            using (var listResponse = (FtpWebResponse)listRequest.GetResponse())
            using (Stream listStream = listResponse.GetResponseStream())
            using (var listReader = new StreamReader(listStream))
            {
                try
                {
                    while (!listReader.EndOfStream)
                    {

                        lines.Add(listReader.ReadLine());
                    }
                    listResponse.Close();
                }
                catch { }
            }

            foreach (string line in lines)
            {
                string[] tokens =
                    line.Split(new[] { ' ' }, 9, StringSplitOptions.RemoveEmptyEntries);
                string name = tokens[8];
                string permissions = tokens[0];

                string fileUrl = url + "/" + name;

                if (permissions[0] == 'd')
                {
                    list_temp.Clear();
                    list_temp.Add(credentials);
                    list_temp.Add(fileUrl + "/");
                    DeleteFtpDirectory(list_temp);
                }
                else
                {
                    var deleteRequest = (FtpWebRequest)WebRequest.Create(new Uri(fileUrl.Replace("#","%23")));
                    deleteRequest.Method = WebRequestMethods.Ftp.DeleteFile;
                    deleteRequest.Credentials = credentials;
                    deleteRequest.Timeout = Global_FunSet.ftpTimeout;
                    deleteRequest.GetResponse();
                    deleteRequest.GetResponse().Close();
                }
            }

            var removeRequest = (FtpWebRequest)WebRequest.Create(new Uri(url.Replace("#","%23")));
            removeRequest.Method = WebRequestMethods.Ftp.RemoveDirectory;
            removeRequest.Credentials = credentials;
            removeRequest.Timeout = Global_FunSet.ftpTimeout;
            removeRequest.GetResponse();
            removeRequest.GetResponse().Close();
        }

        public static int select_flag = 0;
        private void List_Drive_eqpSet1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var cast_items = (EQP_Info_EQPID1)this.List_EQPID_eqpSet1.SelectedItem;

            if (cast_items == null)
            {
                MessageBox.Show("EQPID를 선택하세요");
                return;
            }
            //리스트 선택 못하도록
            this.List_Drive_eqpSet1.IsHitTestVisible = false;
            this.List_EQPID_eqpSet1.IsHitTestVisible = false;

            string eqpid = cast_items.strEQPID_Set1;
            string drive = (string)this.List_Drive_eqpSet1.SelectedItem;
            EQP1_Back_Path.Clear();
            EQP1_Back_idx.Clear();

            //ftp 쓰레드 처리
            source_ftp_info1["ip"] = source_EQP_dict[eqpid];
            source_ftp_info1["id"] = source_drive_dict[drive];
            source_ftp_info1["pw"] = source_drive_dict[drive];

            //경로저장되어 있으면 저장된 경로로 이동
            //if ((bool)this.chk_set1_eqp.IsChecked)
            //{
            //    source_ftp_info1["uri"] = string.Format(@"FTP://{0}:{1}/{2}", gfun.source_EQP_dict[eqpid], 21, this.line_SavePath1.Text);
            //}
            //else
            //{
            //    source_ftp_info1["uri"] = string.Format(@"FTP://{0}:{1}", gfun.source_EQP_dict[eqpid], 21);
            //}
            source_ftp_info1["uri"] = string.Format(@"FTP://{0}:{1}", gfun.source_EQP_dict[eqpid], 21);
            source_ftp_info1["set_flag"] = "1";
            source_ftp_info1["eqpid"] = eqpid;
            source_ftp_info1["foback_flag"] = "1";
            source_ftp_info1["drive"] = drive;
            source_ftp_info1["select_idx"] = "0";

            Thread thread_ftp_login = new Thread(new ParameterizedThreadStart(FTP_Login));
            this.Pgb_eqp1.Value = 0;
            this.Spin_eqp1.Visibility = Visibility.Visible;            
            this.Pgb_eqp1.IsIndeterminate = true;

            this.Pgb_eqp1_text.Text = "접속중";

            thread_ftp_login.IsBackground = true;
            thread_ftp_login.Start(source_ftp_info1);
            select_flag = 1;
            //FTP_Login(source_EQP_dict[eqpid], source_drive_dict[drive], source_drive_dict[drive]);            

            //this.List_Drive_eqpSet1.IsHitTestVisible = true;
            //this.List_EQPID_eqpSet1.IsHitTestVisible = true;
        }

        private void List_Drive_eqpSet2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var cast_items = (EQP_Info_EQPID2)this.List_EQPID_eqpSet2.SelectedItem;

            if (cast_items == null)
            {
                MessageBox.Show("EQPID를 선택하세요");
                return;
            }

            this.List_Drive_eqpSet2.IsHitTestVisible = false;
            this.List_EQPID_eqpSet2.IsHitTestVisible = false;

            string eqpid = cast_items.strEQPID_Set2;
            string drive = (string)this.List_Drive_eqpSet2.SelectedItem;
            EQP2_Back_Path.Clear();
            EQP2_Back_idx.Clear();

            //ftp 쓰레드 처리
            source_ftp_info2["ip"] = source_EQP_dict[eqpid];
            source_ftp_info2["id"] = source_drive_dict[drive];
            source_ftp_info2["pw"] = source_drive_dict[drive];

            //경로저장되어 있으면 저장된 경로로 이동
            //if ((bool)this.chk_set2_eqp.IsChecked)
            //{
            //    source_ftp_info2["uri"] = string.Format(@"FTP://{0}:{1}/{2}", gfun.source_EQP_dict[eqpid], 21, this.line_SavePath2.Text);
            //}
            //else
            //{
            //    source_ftp_info2["uri"] = string.Format(@"FTP://{0}:{1}", gfun.source_EQP_dict[eqpid], 21);
            //}            
            source_ftp_info2["uri"] = string.Format(@"FTP://{0}:{1}", gfun.source_EQP_dict[eqpid], 21);
            source_ftp_info2["set_flag"] = "2";
            source_ftp_info2["eqpid"] = eqpid;
            source_ftp_info2["foback_flag"] = "2";
            source_ftp_info2["drive"] = drive;
            source_ftp_info2["select_idx"] = "0";

            Thread thread_ftp_login = new Thread(new ParameterizedThreadStart(FTP_Login));
            this.Pgb_eqp2.Value = 0;
            this.Spin_eqp2.Visibility = Visibility.Visible;            
            this.Pgb_eqp2.IsIndeterminate = true;

            this.Pgb_eqp2_text.Text = "접속중";

            thread_ftp_login.IsBackground = true;
            thread_ftp_login.Start(source_ftp_info2);
            select_flag = 2;
            //FTP_Login(source_EQP_dict[eqpid], source_drive_dict[drive], source_drive_dict[drive]);            
        }

        private void List_Drive_eqpSet1_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            //select, 클릭 중복 방지
            if (select_flag == 1)
            {
                select_flag = 0;
                return;
            }

            var cast_items = (EQP_Info_EQPID1)this.List_EQPID_eqpSet1.SelectedItem;

            if (cast_items == null)
            {
                MessageBox.Show("EQPID를 선택하세요");
                return;
            }

            string eqpid = cast_items.strEQPID_Set1;
            string drive = (string)this.List_Drive_eqpSet1.SelectedItem;
            if (drive == null) return;

            this.List_Drive_eqpSet1.IsHitTestVisible = false;
            this.List_EQPID_eqpSet1.IsHitTestVisible = false;

            EQP1_Back_Path.Clear();
            EQP1_Back_idx.Clear();

            //ftp 쓰레드 처리
            source_ftp_info1["ip"] = source_EQP_dict[eqpid];
            source_ftp_info1["id"] = source_drive_dict[drive];
            source_ftp_info1["pw"] = source_drive_dict[drive];

            //경로저장되어 있으면 저장된 경로로 이동
            //if ((bool)this.chk_set1_eqp.IsChecked)
            //{
            //    source_ftp_info1["uri"] = string.Format(@"FTP://{0}:{1}/{2}", gfun.source_EQP_dict[eqpid], 21, this.line_SavePath1.Text);
            //}
            //else
            //{
            //    source_ftp_info1["uri"] = string.Format(@"FTP://{0}:{1}", gfun.source_EQP_dict[eqpid], 21);
            //}
            source_ftp_info1["uri"] = string.Format(@"FTP://{0}:{1}", gfun.source_EQP_dict[eqpid], 21);
            source_ftp_info1["set_flag"] = "1";
            source_ftp_info1["eqpid"] = eqpid;
            source_ftp_info1["foback_flag"] = "1";
            source_ftp_info1["drive"] = drive;
            source_ftp_info1["select_idx"] = "0";

            Thread thread_ftp_login = new Thread(new ParameterizedThreadStart(FTP_Login));
            this.Pgb_eqp1.Value = 0;
            this.Spin_eqp1.Visibility = Visibility.Visible;            
            this.Pgb_eqp1.IsIndeterminate = true;

            this.Pgb_eqp1_text.Text = "접속중";

            thread_ftp_login.IsBackground = true;
            thread_ftp_login.Start(source_ftp_info1);
        }

        private void List_EQPID_eqpSet1_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if ((bool)this.chk_set1_eqp.IsChecked)
            {
                var cast_items = (EQP_Info_EQPID1)this.List_EQPID_eqpSet1.SelectedItem;

                if (cast_items == null)
                {
                    MessageBox.Show("EQPID를 선택하세요");
                    return;
                }

                string eqpid = cast_items.strEQPID_Set1;
                string drive = (string)this.List_Drive_eqpSet1.SelectedItem;
                if (drive == null) return;

                this.List_Drive_eqpSet1.IsHitTestVisible = false;
                this.List_EQPID_eqpSet1.IsHitTestVisible = false;

                EQP1_Back_Path.Clear();
                EQP1_Back_idx.Clear();

                //ftp 쓰레드 처리
                source_ftp_info1["ip"] = source_EQP_dict[eqpid];

                if (this.line_SavePath1_2.Text.Contains("C"))
                {
                    source_ftp_info1["id"] = "LS_C";
                    source_ftp_info1["pw"] = "LS_C";
                }
                else if (this.line_SavePath1_2.Text.Contains("D"))
                {
                    source_ftp_info1["id"] = "LS_D";
                    source_ftp_info1["pw"] = "LS_D";
                }
                else if (this.line_SavePath1_2.Text.Contains("E"))
                {
                    source_ftp_info1["id"] = "LS_E";
                    source_ftp_info1["pw"] = "LS_E";
                }
                source_ftp_info1["uri"] = string.Format(@"FTP://{0}:{1}/{2}", gfun.source_EQP_dict[eqpid], 21, this.line_SavePath1.Text);
                source_ftp_info1["set_flag"] = "1";
                source_ftp_info1["eqpid"] = eqpid;
                source_ftp_info1["foback_flag"] = "2";
                source_ftp_info1["drive"] = drive;
                source_ftp_info1["select_idx"] = "0";

                Thread thread_ftp_login = new Thread(new ParameterizedThreadStart(FTP_Login));
                this.Pgb_eqp1.Value = 0;
                this.Spin_eqp1.Visibility = Visibility.Visible;
                this.Pgb_eqp1.IsIndeterminate = true;

                this.Pgb_eqp1_text.Text = "접속중";

                thread_ftp_login.IsBackground = true;
                thread_ftp_login.Start(source_ftp_info1);
            }            
        }

        private void List_EQPID_eqpSet2_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if ((bool)this.chk_set2_eqp.IsChecked)
            {
                var cast_items = (EQP_Info_EQPID2)this.List_EQPID_eqpSet2.SelectedItem;

                if (cast_items == null)
                {
                    MessageBox.Show("EQPID를 선택하세요");
                    return;
                }

                string eqpid = cast_items.strEQPID_Set2;
                string drive = (string)this.List_Drive_eqpSet2.SelectedItem;
                if (drive == null) return;

                this.List_Drive_eqpSet2.IsHitTestVisible = false;
                this.List_EQPID_eqpSet2.IsHitTestVisible = false;

                EQP2_Back_Path.Clear();
                EQP2_Back_idx.Clear();

                //ftp 쓰레드 처리
                source_ftp_info2["ip"] = source_EQP_dict[eqpid];

                if (this.line_SavePath2_2.Text.Contains("C"))
                {
                    source_ftp_info2["id"] = "LS_C";
                    source_ftp_info2["pw"] = "LS_C";
                }
                else if (this.line_SavePath2_2.Text.Contains("D"))
                {
                    source_ftp_info2["id"] = "LS_D";
                    source_ftp_info2["pw"] = "LS_D";
                }
                else if (this.line_SavePath2_2.Text.Contains("E"))
                {
                    source_ftp_info2["id"] = "LS_E";
                    source_ftp_info2["pw"] = "LS_E";
                }
                source_ftp_info2["uri"] = string.Format(@"FTP://{0}:{1}/{2}", gfun.source_EQP_dict[eqpid], 21, this.line_SavePath2.Text);
                source_ftp_info2["set_flag"] = "2";
                source_ftp_info2["eqpid"] = eqpid;
                source_ftp_info2["foback_flag"] = "1";
                source_ftp_info2["drive"] = drive;
                source_ftp_info2["select_idx"] = "0";

                Thread thread_ftp_login = new Thread(new ParameterizedThreadStart(FTP_Login));
                this.Pgb_eqp2.Value = 0;
                this.Spin_eqp2.Visibility = Visibility.Visible;
                this.Pgb_eqp2.IsIndeterminate = true;

                this.Pgb_eqp2_text.Text = "접속중";

                thread_ftp_login.IsBackground = true;
                thread_ftp_login.Start(source_ftp_info2);
            }
        }

        private void List_Drive_eqpSet2_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            //select, 클릭 중복 방지
            if (select_flag == 2)
            {
                select_flag = 0;
                return;
            }

            var cast_items = (EQP_Info_EQPID2)this.List_EQPID_eqpSet2.SelectedItem;

            if (cast_items == null)
            {
                MessageBox.Show("EQPID를 선택하세요");
                return;
            }

            string eqpid = cast_items.strEQPID_Set2;
            string drive = (string)this.List_Drive_eqpSet2.SelectedItem;
            if (drive == null) return;

            this.List_Drive_eqpSet2.IsHitTestVisible = false;
            this.List_EQPID_eqpSet2.IsHitTestVisible = false;

            EQP2_Back_Path.Clear();
            EQP2_Back_idx.Clear();

            //ftp 쓰레드 처리
            source_ftp_info2["ip"] = source_EQP_dict[eqpid];
            source_ftp_info2["id"] = source_drive_dict[drive];
            source_ftp_info2["pw"] = source_drive_dict[drive];
            //경로저장되어 있으면 저장된 경로로 이동
            //if ((bool)this.chk_set2_eqp.IsChecked)
            //{
            //    source_ftp_info2["uri"] = string.Format(@"FTP://{0}:{1}/{2}", gfun.source_EQP_dict[eqpid], 21, this.line_SavePath2.Text);
            //}
            //else
            //{
            //    source_ftp_info2["uri"] = string.Format(@"FTP://{0}:{1}", gfun.source_EQP_dict[eqpid], 21);
            //}
            source_ftp_info2["uri"] = string.Format(@"FTP://{0}:{1}", gfun.source_EQP_dict[eqpid], 21);
            source_ftp_info2["set_flag"] = "2";
            source_ftp_info2["eqpid"] = eqpid;
            source_ftp_info2["foback_flag"] = "1";
            source_ftp_info2["drive"] = drive;
            source_ftp_info2["select_idx"] = "0";

            Thread thread_ftp_login = new Thread(new ParameterizedThreadStart(FTP_Login));
            this.Pgb_eqp2.Value = 0;
            this.Spin_eqp2.Visibility = Visibility.Visible;            
            this.Pgb_eqp2.IsIndeterminate = true;
            this.Pgb_eqp2_text.Text = "접속중";

            thread_ftp_login.IsBackground = true;
            thread_ftp_login.Start(source_ftp_info2);
        }

        //에프티피
        private void FTP_Login(object dic)
        {
            //아래는 ftp 접속
            var dic_ftp = (Dictionary<string, string>)dic;
            var uri = (string)dic_ftp["uri"];
            line_text_eqp = new Line_text_eqp();
            int set_flag = Int32.Parse(dic_ftp["set_flag"]);
            int reset_flag = Int32.Parse(dic_ftp["foback_flag"]);
            int select_idx = Int32.Parse(dic_ftp["select_idx"]);

            //uri 검사부터, file folder 둘다 오류이면 return
            if (!Global_FunSet.IsExistDirectory(uri, dic_ftp["id"], dic_ftp["pw"]) && !Global_FunSet.IsExistFile(uri, dic_ftp["id"], dic_ftp["pw"]))
            {
                MessageBox.Show("경로 오류");
                if (set_flag == 1)
                {
                    Dispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate
                    {
                        this.Pgb_eqp1.Value = 0;
                        this.Spin_eqp1.Visibility = Visibility.Collapsed;
                        this.Pgb_eqp1.IsIndeterminate = false;
                        this.Pgb_eqp1_text.Text = "";
                        this.List_Drive_eqpSet1.IsHitTestVisible = true;
                        this.List_EQPID_eqpSet1.IsHitTestVisible = true;
                    }));
                }
                else if (set_flag == 2)
                {
                    Dispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate
                    {
                        this.Pgb_eqp2.Value = 0;
                        this.Spin_eqp2.Visibility = Visibility.Collapsed;
                        this.Pgb_eqp2.IsIndeterminate = false;
                        this.Pgb_eqp2_text.Text = "";
                        this.List_Drive_eqpSet2.IsHitTestVisible = true;
                        this.List_EQPID_eqpSet2.IsHitTestVisible = true;
                    }));
                }

                else if (set_flag == 3)
                {
                    Dispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate
                    {                        
                        this.pgb_EI.Value = 0;
                        this.pgb_EI.IsIndeterminate = false;
                        this.pgbTxt_EI.Text = "";
                        this.line_EI.IsHitTestVisible = true;
                        this.list_EI.IsHitTestVisible = true;
                    }));
                }
                return;
            }

            //알수없는 버그로 딜레이 100ms 추가 (set_flag 3일때만 버그걸림)
            //if (set_flag == 3)
            //{
            //    //Thread.Sleep(200);
            //    for (int i=0; i<100; i++)
            //    {
            //        Debug.WriteLine("dummy");
            //    }
            //}
                

            if (set_flag < 3)
            {
                Dispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate
                {
                    uiList_stack[set_flag + 1].IsHitTestVisible = false;
                }));
            }

            if (dic_ftp["set_flag"] == "1")
            {
                source_EQP_path_all1.Clear();                
            }

            else if (dic_ftp["set_flag"] == "2")
            {
                source_EQP_path_all2.Clear();                
            }

            else if (dic_ftp["set_flag"] == "3")
            {
                source_EQP_EI_all.Clear();
            }

            //MessageBox.Show("여기");

            try
            {
                FtpWebRequest ftpReq = (FtpWebRequest)WebRequest.Create(new Uri(uri.Replace("#","%23")));                   //ftp 생성                
                ftpReq.Credentials = new NetworkCredential(dic_ftp["id"], dic_ftp["pw"]);
                ftpReq.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
                ftpReq.Timeout = Global_FunSet.ftpTimeout;
                //ftpReq.Method = WebRequestMethods.Ftp.GetDateTimestamp;            
                FtpWebResponse resFtp = (FtpWebResponse)ftpReq.GetResponse();

                //앞뒤 버튼용 경로 리스트
                Dispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate
                {
                    if (dic_ftp["foback_flag"] == "1")
                    {
                        if (dic_ftp["set_flag"] == "1" && this.line_set1_eqp.Text != "")
                        {
                            EQP1_Back_Path.Add(this.line_set1_eqp.Text);
                            EQP1_Back_idx.Add(this.list_set1_eqp_all.SelectedIndex);
                        }                            
                        else if (dic_ftp["set_flag"] == "2" && this.line_set2_eqp.Text != "")
                        {
                            EQP2_Back_Path.Add(this.line_set2_eqp.Text);
                            EQP2_Back_idx.Add(this.list_set2_eqp_all.SelectedIndex);
                        }
                        else if (dic_ftp["set_flag"] == "3" && this.line_EI.Text != "")
                        {
                            EQP_EI_Back_path.Add(this.line_EI.Text);
                            EQP_EI_Back_idx.Add(this.list_EI.SelectedIndex);                            
                        }
                    }
                }));

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
                        if (dic_ftp["set_flag"] == "1")
                        {
                            source_EQP_path_all1.Add(new EQP_path_all1()
                            {
                                eqp_all_filename = name,
                                eqp_all_date = modified,
                                eqp_all_extension = extension,
                                eqp_all_size = Global_FunSet.ReturnFileSizeStr(size),
                                eqp_all_src = Global_FunSet.ReturnIconPathStr(extension)
                            });
                            
                        }

                        else if (dic_ftp["set_flag"] == "2")
                        {
                            source_EQP_path_all2.Add(new EQP_path_all2()
                            {
                                eqp_all_filename = name,
                                eqp_all_date = modified,
                                eqp_all_extension = extension,
                                eqp_all_size = Global_FunSet.ReturnFileSizeStr(size),
                                eqp_all_src = Global_FunSet.ReturnIconPathStr(extension)
                            });
                        }
                        else if (dic_ftp["set_flag"] == "3")
                        {
                            source_EQP_EI_all.Add(new Control_Vis()
                            {
                                eqp_all_filename = name,
                                eqp_all_date = modified,
                                eqp_all_extension = extension,
                                eqp_all_size = Global_FunSet.ReturnFileSizeStr(size),
                                eqp_all_src = Global_FunSet.ReturnIconPathStr(extension)
                            });
                        }
                    }
                    catch
                    {

                    }
                }
                
                if (dic_ftp["set_flag"] == "1")
                {
                    line_text_eqp.text_set1_eqp = uri;
                    Dispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate
                    {
                        this.line_set1_eqp.DataContext = line_text_eqp;
                        this.cur_eqp_set1.Text = dic_ftp["eqpid"] + " 연결: " + dic_ftp["drive"];
                        this.list_set1_eqp_all.DataContext = source_EQP_path_all1;
                        this.list_set1_eqp_all.ItemsSource = source_EQP_path_all1;
                        source_EQP_path_all1.Sort(new SortDefault());
                        this.list_set1_eqp_all.Items.Refresh();

                        this.Pgb_eqp1.IsIndeterminate = false;
                        this.Spin_eqp1.Visibility = Visibility.Collapsed;
                        this.Pgb_eqp1_text.Text = "접속 완료";
                        this.Pgb_eqp1.Value = 100;
                    }));
                    
                    resFtp.Close();

                    Dispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate
                    {
                        this.List_Drive_eqpSet1.IsHitTestVisible = true;
                        this.List_EQPID_eqpSet1.IsHitTestVisible = true;
                    }));
                    
                }
                else if (dic_ftp["set_flag"] == "2")
                {
                    line_text_eqp.text_set2_eqp = uri;
                    Dispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate
                    {
                        this.line_set2_eqp.DataContext = line_text_eqp;
                        this.cur_eqp_set2.Text = dic_ftp["eqpid"] + " 연결: " + dic_ftp["drive"];                        
                        this.list_set2_eqp_all.DataContext = source_EQP_path_all2;
                        this.list_set2_eqp_all.ItemsSource = source_EQP_path_all2;
                        source_EQP_path_all2.Sort(new SortDefault());
                        this.list_set2_eqp_all.Items.Refresh();
                        this.Pgb_eqp2.IsIndeterminate = false;

                        this.Spin_eqp2.Visibility = Visibility.Collapsed;
                        this.Pgb_eqp2_text.Text = "접속 완료";
                        this.Pgb_eqp2.Value = 100;
                    }));
                    
                    resFtp.Close();

                    Dispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate
                    {
                        this.List_Drive_eqpSet2.IsHitTestVisible = true;
                        this.List_EQPID_eqpSet2.IsHitTestVisible = true;
                    }));                    
                }
                else if (dic_ftp["set_flag"] == "3")
                {                    
                    Dispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate
                    {
                        this.line_EI.Text = uri;
                        this.cur_EI_text.Text = dic_ftp["eqpid"] + " 연결: " + dic_ftp["drive"];
                        this.list_EI.DataContext = source_EQP_EI_all;
                        this.list_EI.ItemsSource = source_EQP_EI_all;
                        source_EQP_EI_all.Sort(new SortDefault());
                        this.list_EI.Items.Refresh();
                        this.pgb_EI.IsIndeterminate = false;
                        this.pgb_EI.Value = 100;
                        this.pgbTxt_EI.Text = "접속 완료";
                    }));

                    resFtp.Close();

                    Dispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate
                    {
                        this.list_EI.IsHitTestVisible = true;                        
                    }));
                }

                if (set_flag < 3)
                {
                    Dispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate
                    {
                        uiList_stack[set_flag + 1].IsHitTestVisible = true;
                    }));

                    Dispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate
                    {
                        try
                        {
                            uiList_textbox[set_flag + 1].Focus();
                            uiList_textbox[set_flag + 1].Select(uiList_textbox[set_flag + 1].Text.Length, 0);

                            //list_set1_eqp_all.SelectedIndex = select_idx;
                            uiList_listView[set_flag + 1].SelectedIndex = select_idx;
                            uiList_listView[set_flag + 1].Focus();
                            //Debug.WriteLine(select_idx);

                            uiList_listView[set_flag + 1].ScrollIntoView(uiList_listView[set_flag + 1].Items[select_idx]);
                            ListBoxItem lbi = uiList_listView[set_flag + 1].ItemContainerGenerator.ContainerFromIndex(uiList_listView[set_flag + 1].SelectedIndex) as ListBoxItem;

                            if (lbi != null)
                            {

                                lbi.Focus();
                            }
                        }
                        catch { }

                        
                    }));
                }
                else
                {
                    Dispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate
                    {
                        try
                        {
                            this.line_EI.Focus();
                            this.line_EI.Select(this.line_EI.Text.Length, 0);

                            this.list_EI.SelectedIndex = select_idx;
                            this.list_EI.Focus();

                            this.list_EI.ScrollIntoView(this.list_EI.Items[select_idx]);
                            ListBoxItem lbi = this.list_EI.ItemContainerGenerator.ContainerFromIndex(this.list_EI.SelectedIndex) as ListBoxItem;

                            if (lbi != null)
                            {

                                lbi.Focus();
                            }
                        }
                        catch { }

                        
                    }));
                }
                
                
            }

            //ftp 접속 실패
            catch (Exception ex)
            {
                Dispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate
                {
                    if (dic_ftp["set_flag"] == "1")
                    {
                        this.cur_eqp_set1.Text = "";
                        this.line_set1_eqp.Text = "";
                        this.list_set1_eqp_all.ItemsSource = null;
                        this.Pgb_eqp1.IsIndeterminate = false;
                        this.Pgb_eqp1.Value = 0;
                        this.Pgb_eqp1_text.Text = "접속 실패";
                        this.Spin_eqp1.Visibility = Visibility.Collapsed;
                        this.List_Drive_eqpSet1.IsHitTestVisible = true;
                        this.List_EQPID_eqpSet1.IsHitTestVisible = true;
                        MessageBox.Show(ex.Message);
                    }
                    else if (dic_ftp["set_flag"] == "2")
                    {
                        this.cur_eqp_set2.Text = "";
                        this.line_set2_eqp.Text = "";
                        this.list_set2_eqp_all.ItemsSource = null;
                        this.Pgb_eqp2.IsIndeterminate = false;
                        this.Pgb_eqp2.Value = 0;
                        this.Pgb_eqp2_text.Text = "접속 실패";
                        this.Spin_eqp2.Visibility = Visibility.Collapsed;
                        this.List_Drive_eqpSet2.IsHitTestVisible = true;
                        this.List_EQPID_eqpSet2.IsHitTestVisible = true;
                        MessageBox.Show(ex.Message);
                    }
                    else if (dic_ftp["set_flag"] == "3")
                    {
                        this.cur_EI_text.Text = "";
                        this.line_EI.Text = "";
                        this.list_EI.ItemsSource = null;
                        this.pgb_EI.IsIndeterminate = false;
                        this.pgb_EI.Value = 0;
                        this.pgbTxt_EI.Text = "접속 실패";
                        this.line_EI.IsHitTestVisible = true;
                        this.list_EI.IsHitTestVisible = true;
                        MessageBox.Show(ex.Message);
                    }
                }));

                if (set_flag < 3)
                {
                    Dispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate
                    {
                        uiList_stack[set_flag + 1].IsHitTestVisible = true;
                    }));
                }
                
            }

            
        }

        private void list_set1_eqp_all_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (header_flag == 1)
            {
                header_flag = 0;
                return;
            }

            ListView lv = sender as ListView;
            var items = (EQP_path_all1)lv.SelectedItem;

            if (items == null)
            {
                return;
            }

            if (items.eqp_all_extension == "폴더")
            {
                var items_eqp1 = (EQP_path_all1)this.list_set1_eqp_all.SelectedItem;
                string eqpid = this.cur_eqp_set1.Text.Substring(0, 7);

                //source_ftp_info1["ip"] = source_EQP_dict[eqpid];
                //source_ftp_info1["id"] = source_drive_dict[drive];
                //source_ftp_info1["pw"] = source_drive_dict[drive];
                //source_ftp_info1["uri"] = string.Format(@"FTP://{0}:{1}/", source_EQP_dict[eqpid], 21);
                source_ftp_info1["uri"] += "/" + items_eqp1.eqp_all_filename;
                source_ftp_info1["set_flag"] = "1";
                source_ftp_info1["foback_flag"] = "1";

                Thread thread_ftp_login = new Thread(new ParameterizedThreadStart(FTP_Login));
                this.Pgb_eqp1.Value = 0;
                this.Pgb_eqp1.IsIndeterminate = true;
                this.Spin_eqp1.Visibility = Visibility.Visible;                

                thread_ftp_login.IsBackground = true;
                thread_ftp_login.Start(source_ftp_info1);
            }
            else
            {
                MessageBox.Show("FTP에서는 파일을 실행할 수 없습니다.");
            }
        }

        private void list_set2_eqp_all_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (header_flag == 1)
            {
                header_flag = 0;
                return;
            }

            ListView lv = sender as ListView;
            var items = (EQP_path_all2)lv.SelectedItem;

            if (items == null)
            {
                return;
            }

            if (items.eqp_all_extension == "폴더")
            {
                var items_eqp2 = (EQP_path_all2)this.list_set2_eqp_all.SelectedItem;
                string eqpid = this.cur_eqp_set2.Text.Substring(0, 7);

                //source_ftp_info1["ip"] = source_EQP_dict[eqpid];
                //source_ftp_info1["id"] = source_drive_dict[drive];
                //source_ftp_info1["pw"] = source_drive_dict[drive];
                //source_ftp_info1["uri"] = string.Format(@"FTP://{0}:{1}/", source_EQP_dict[eqpid], 21);
                source_ftp_info2["uri"] += "/" + items_eqp2.eqp_all_filename;
                source_ftp_info2["set_flag"] = "2";
                source_ftp_info2["foback_flag"] = "1";

                Thread thread_ftp_login = new Thread(new ParameterizedThreadStart(FTP_Login));
                this.Pgb_eqp2.Value = 0;
                this.Pgb_eqp2.IsIndeterminate = true;
                this.Spin_eqp2.Visibility = Visibility.Visible;                

                thread_ftp_login.IsBackground = true;
                thread_ftp_login.Start(source_ftp_info2);
            }
            else
            {
                MessageBox.Show("FTP에서는 파일을 실행할 수 없습니다.");
            }
        }

        private void list_GiveFeedback(object sender, GiveFeedbackEventArgs e)
        {
            if (e.Effects == DragDropEffects.Move)
            {
                e.UseDefaultCursors = false;
                Mouse.SetCursor(Cursors.Hand);
            }
            else if (e.Effects == DragDropEffects.Copy)
            {
                e.UseDefaultCursors = false;
                Mouse.SetCursor(Cursors.Pen);
            }
            else
                e.UseDefaultCursors = true;

            e.Handled = true;
        }

           

        private void eqp_save_Click(object sender, RoutedEventArgs e)
        {
            NewView.ViewInfoEQP viewInfoEQP = new NewView.ViewInfoEQP();
            viewInfoEQP.Show();
        }

        private void local_save_Click(object sender, RoutedEventArgs e)
        {
            var run_file = new Process();
            ProcessStartInfo processStartInfo = new ProcessStartInfo("notepad.exe", @"C:\\Load_Spectrum2\\CustomPathList.csv");
            run_file.StartInfo = processStartInfo;
            //run_file.StartInfo.FileName = @"C:\\Load_Spectrum2\\CustomPathList.csv";
            
            run_file.StartInfo.UseShellExecute = true;
            run_file.Start();
        }

        private void TransferData(int set_flag)
        {
            var items = uiList_listView[set_flag - 1].SelectedItems;
            string[] d_Items = new string[items.Count];

            int i = 0;
            foreach (var item in items)
            {
                if (set_flag == 1)
                {
                    if (this.line_set1_eqp.Text == "") return;
                    var item_str = (Local_path_all1)item;
                    d_Items[i] = this.line_set1_local.Text + "\\" + item_str.local_all_filename;
                    i++;
                }
                else if (set_flag == 2)
                {
                    if (this.line_set2_eqp.Text == "") return;
                    var item_str = (Local_path_all2)item;
                    d_Items[i] = this.line_set2_local.Text + "\\" + item_str.local_all_filename;
                    i++;
                }
                else if (set_flag == 3)
                {
                    if (this.line_set1_local.Text == "") return;
                    var item_str = (EQP_path_all1)item;
                    d_Items[i] = this.line_set1_eqp.Text + "\\" + item_str.eqp_all_filename;
                    i++;
                }
                else if (set_flag == 4)
                {
                    if (this.line_set2_local.Text == "") return;
                    var item_str = (EQP_path_all2)item;
                    d_Items[i] = this.line_set2_eqp.Text + "\\" + item_str.eqp_all_filename;
                    i++;
                }
            }

            if (set_flag == 1)
            {
                bw_local_eqp = new BackgroundWorker();
                Dictionary<string, dynamic> bw_src = new Dictionary<string, dynamic>();
                bw_src.Add("bw_main", bw_local_eqp);
                bw_src.Add("source_set_flag", 1);
                bw_src.Add("target_set_flag", 3);
                bw_src.Add("items", d_Items);
                bw_local_eqp.DoWork += new DoWorkEventHandler(bw_local_eqp_DoWork);
                bw_local_eqp.ProgressChanged += bw_local_eqp_ProgressChanged;
                bw_local_eqp.RunWorkerCompleted += bw_local_eqp_RunWorkerCompleted;
                bw_local_eqp.WorkerReportsProgress = true;
                bw_local_eqp.WorkerSupportsCancellation = true;
                bw_local_eqp.RunWorkerAsync(argument: bw_src);
            }
            else if (set_flag == 2)
            {
                bw_local_eqp4 = new BackgroundWorker();
                Dictionary<string, dynamic> bw_src = new Dictionary<string, dynamic>();
                bw_src.Add("bw_main", bw_local_eqp4);
                bw_src.Add("source_set_flag", 2);
                bw_src.Add("target_set_flag", 4);
                bw_src.Add("items", d_Items);
                bw_local_eqp4.DoWork += new DoWorkEventHandler(bw_local_eqp_DoWork);
                bw_local_eqp4.ProgressChanged += bw_local_eqp_ProgressChanged;
                bw_local_eqp4.RunWorkerCompleted += bw_local_eqp_RunWorkerCompleted;
                bw_local_eqp4.WorkerReportsProgress = true;
                bw_local_eqp4.WorkerSupportsCancellation = true;
                bw_local_eqp4.RunWorkerAsync(argument: bw_src);
            }
            else if (set_flag == 3)
            {
                bw_eqp_local = new BackgroundWorker();
                Dictionary<string, dynamic> bw_src = new Dictionary<string, dynamic>();
                bw_src.Add("bw_main", bw_eqp_local);
                bw_src.Add("source_set_flag", 3);
                bw_src.Add("target_set_flag", 1);
                bw_src.Add("items", d_Items);
                bw_eqp_local.DoWork += new DoWorkEventHandler(bw_eqp_local_DoWork);
                bw_eqp_local.ProgressChanged += bw_eqp_local_ProgressChanged;
                bw_eqp_local.RunWorkerCompleted += bw_eqp_local_RunWorkerCompleted;
                bw_eqp_local.WorkerReportsProgress = true;
                bw_eqp_local.WorkerSupportsCancellation = true;
                bw_eqp_local.RunWorkerAsync(argument: bw_src);
            }
            else if (set_flag == 4)
            {
                bw_eqp_local4 = new BackgroundWorker();
                Dictionary<string, dynamic> bw_src = new Dictionary<string, dynamic>();
                bw_src.Add("bw_main", bw_eqp_local4);
                bw_src.Add("source_set_flag", 4);
                bw_src.Add("target_set_flag", 2);
                bw_src.Add("items", d_Items);
                bw_eqp_local4.DoWork += new DoWorkEventHandler(bw_eqp_local_DoWork);
                bw_eqp_local4.ProgressChanged += bw_eqp_local_ProgressChanged;
                bw_eqp_local4.RunWorkerCompleted += bw_eqp_local_RunWorkerCompleted;
                bw_eqp_local4.WorkerReportsProgress = true;
                bw_eqp_local4.WorkerSupportsCancellation = true;
                bw_eqp_local4.RunWorkerAsync(argument: bw_src);
            }
            d_Items = null;
            items = null;

        }

        private void Local1_TrBtn_Click(object sender, RoutedEventArgs e)
        {
            TrLocalToEQP(1, 3, uiList_textbox[0].Text, uiList_textbox[2].Text);
        }

        private void Local2_TrBtn_Click(object sender, RoutedEventArgs e)
        {
            TrLocalToEQP(2, 4, uiList_textbox[1].Text, uiList_textbox[3].Text);
        }

        private void Eqp1_TrBtn_Click(object sender, RoutedEventArgs e)
        {
            TrEQPToLocal(3, 1, uiList_textbox[2].Text, uiList_textbox[0].Text);
        }

        private void Eqp2_TrBtn_Click(object sender, RoutedEventArgs e)
        {
            TrEQPToLocal(4, 2, uiList_textbox[3].Text, uiList_textbox[1].Text);
        }

        private void EQP1_StopBtn_Click(object sender, RoutedEventArgs e)
        {            
            if (bw_local_eqp != null)
            {
                LTE_stop_flag = 1;
            }                
            else if (bw_local_eqp2 != null)
            {
                LTE_stop_flag2 = 1;
            }
            else
            {
                return;
            }
        }
        private void EQP2_StopBtn_Click(object sender, RoutedEventArgs e)
        {            
            if (bw_local_eqp3 != null)
            {
                LTE_stop_flag3 = 1;
            }
            else if (bw_local_eqp4 != null)
            {
                LTE_stop_flag4 = 1;
            }
            else
            {
                return;
            }
        }
        private void Local1_StopBtn_Click(object sender, RoutedEventArgs e)
        {            
            if (bw_eqp_local != null)
            {
                ETL_stop_flag = 1;
            }            
            else if (bw_eqp_local3 != null)
            {
                ETL_stop_flag3 = 1;
            }            
            else if (bw_local_local2 != null)
            {
                LTL_stop_flag2 = 1;                
            }
            else
            {
                return;
            }
        }
        private void Local2_StopBtn_Click(object sender, RoutedEventArgs e)
        {
            if (bw_eqp_local2 != null)
            {
                ETL_stop_flag2 = 1;
            }
            else if (bw_eqp_local4 != null)
            {
                ETL_stop_flag4 = 1;
            }
            else if (bw_local_local2 != null)
            {
                LTL_stop_flag = 1;
            }
            else
            {
                return;
            }
        }

        private void manual_Click(object sender, RoutedEventArgs e)
        {            
            Process.Start(@"http://edm2.sec.samsung.net/cc/link/verLink/165464837958004287/7");
            //string fileName = "Load_Spectrum2.Resources.LS2_Manual.xlsx";
            //string filePath = @"C:\\Load_Spectrum2\\LS2_Manual.xlsx";

            //Assembly asm = Assembly.GetExecutingAssembly();
            //string file = string.Format("{0}.UTReportTemplate.xls", asm.GetName().Name);
            //Stream fileStream = asm.GetManifestResourceStream(fileName);
            //SaveStreamToFile(filePath, fileStream);  //<--here is where to save to disk

            //var run_file = new Process();
            //ProcessStartInfo processStartInfo = new ProcessStartInfo(filePath);
            //run_file.StartInfo = processStartInfo;
            ////run_file.StartInfo.FileName = @"C:\\Load_Spectrum2\\CustomPathList.csv";

            //run_file.StartInfo.UseShellExecute = true;
            //run_file.Start();
            //Excel.Application xlApp = new Excel.Application();
            //var xlWorkBook = xlApp.Workbooks.Open(filePath);
            //if (xlWorkBook == null)
            //{
            //    MessageBox.Show("Error: Unable to open Excel file.");
            //    return;
            //}
        }
        private void SaveStreamToFile(string fileFullPath, Stream stream)
        {
            if (stream.Length == 0) return;

            // Create a FileStream object to write a stream to a file
            using (FileStream fileStream = System.IO.File.Create(fileFullPath, (int)stream.Length))
            {
                // Fill the bytes[] array with the stream data
                byte[] bytesInStream = new byte[stream.Length];
                stream.Read(bytesInStream, 0, (int)bytesInStream.Length);

                // Use FileStream object to write to the specified file
                fileStream.Write(bytesInStream, 0, bytesInStream.Length);
            }
        }

        //Context 메뉴
        private void list_set1_local_all_PreviewMouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            var items = this.list_set1_local_all.SelectedItems;
            ContextMenu pMenu = null;
            if (items.Count == 0)
            {
                pMenu = (ContextMenu)this.Resources["Local1_Unselect_ctxt"];
                pMenu.IsOpen = true;
            }
            else
            {
                pMenu = (ContextMenu)this.Resources["Local1_Select_ctxt"];
                pMenu.IsOpen = true;
            }
        }

        private void lu1_Newfolder_Click(object sender, RoutedEventArgs e)
        {
            LocalNewFolder(1);
        }

        private void lu1_Openfolder_Click(object sender, RoutedEventArgs e)
        {
            if (this.line_set1_local.Text == "") return;
            var run_file = new Process();
            run_file.StartInfo.FileName = this.line_set1_local.Text;
            run_file.StartInfo.UseShellExecute = true;
            run_file.Start();
        }

        private void ls1_Newfolder_Click(object sender, RoutedEventArgs e)
        {
            LocalNewFolder(1);
        }

        private void ls1_Openfolder_Click(object sender, RoutedEventArgs e)
        {
            if (this.line_set1_local.Text == "") return;
            var run_file = new Process();
            run_file.StartInfo.FileName = this.line_set1_local.Text;
            run_file.StartInfo.UseShellExecute = true;
            run_file.Start();
        }

        private void ls1_Rename_Click(object sender, RoutedEventArgs e)
        {
            RenameFun(1);
        }

        private void ls1_Del_Click(object sender, RoutedEventArgs e)
        {
            int set_flag = 1;
            if (uiList_listView[set_flag - 1].SelectedItems.Count == 0) return;
            try
            {                
                bw_local_del = new BackgroundWorker();
                bw_local_del.DoWork += new DoWorkEventHandler(bw_local_del_DoWork);
                bw_local_del.ProgressChanged += bw_local_del_ProgressChanged;
                bw_local_del.RunWorkerCompleted += bw_local_del_RunWorkerCompleted;
                bw_local_del.WorkerReportsProgress = true;
                bw_local_del.RunWorkerAsync(argument: set_flag);                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void list_set2_local_all_PreviewMouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            var items = this.list_set2_local_all.SelectedItems;
            ContextMenu pMenu = null;
            if (items.Count == 0)
            {
                pMenu = (ContextMenu)this.Resources["Local2_Unselect_ctxt"];
                pMenu.IsOpen = true;
            }
            else
            {
                pMenu = (ContextMenu)this.Resources["Local2_Select_ctxt"];
                pMenu.IsOpen = true;
            }
        }

        private void lu2_Newfolder_Click(object sender, RoutedEventArgs e)
        {
            LocalNewFolder(2);
        }

        private void lu2_Openfolder_Click(object sender, RoutedEventArgs e)
        {
            if (this.line_set2_local.Text == "") return;
            var run_file = new Process();
            run_file.StartInfo.FileName = this.line_set2_local.Text;
            run_file.StartInfo.UseShellExecute = true;
            run_file.Start();
        }

        private void ls2_Newfolder_Click(object sender, RoutedEventArgs e)
        {
            LocalNewFolder(2);
        }

        private void ls2_Openfolder_Click(object sender, RoutedEventArgs e)
        {
            if (this.line_set2_local.Text == "") return;
            var run_file = new Process();
            run_file.StartInfo.FileName = this.line_set2_local.Text;
            run_file.StartInfo.UseShellExecute = true;
            run_file.Start();
        }

        private void ls2_Rename_Click(object sender, RoutedEventArgs e)
        {
            RenameFun(2);
        }

        private void ls2_Del_Click(object sender, RoutedEventArgs e)
        {
            int set_flag = 2;
            if (uiList_listView[set_flag - 1].SelectedItems.Count == 0) return;
            try
            {
                bw_local_del = new BackgroundWorker();
                bw_local_del.DoWork += new DoWorkEventHandler(bw_local_del_DoWork);
                bw_local_del.ProgressChanged += bw_local_del_ProgressChanged;
                bw_local_del.RunWorkerCompleted += bw_local_del_RunWorkerCompleted;
                bw_local_del.WorkerReportsProgress = true;
                bw_local_del.RunWorkerAsync(argument: set_flag);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //EQP Context
        private void list_set1_eqp_all_PreviewMouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            var items = this.list_set1_eqp_all.SelectedItems;
            ContextMenu pMenu = null;
            if (items.Count == 0)
            {
                pMenu = (ContextMenu)this.Resources["EQP1_Unselect_ctxt"];
                pMenu.IsOpen = true;
            }
            else
            {
                pMenu = (ContextMenu)this.Resources["EQP1_Select_ctxt"];
                pMenu.IsOpen = true;
            }
        }

        private void eu1_Newfolder_Click(object sender, RoutedEventArgs e)
        {
            NewFolderEQP(1);
        }

        private void eu1_Openfolder_Click(object sender, RoutedEventArgs e)
        {
            if (this.line_set1_eqp.Text == "") return;
            string temp = this.line_set1_eqp.Text;
            string ftp_path = string.Format("ftp://{0}:{1}@{2}:{3}", source_ftp_info1["id"], source_ftp_info1["pw"],
                source_ftp_info1["ip"], temp.Substring(7+source_ftp_info1["ip"].Length, temp.Length - source_ftp_info1["ip"].Length-7));            
            
            var run_file = new Process();
            ProcessStartInfo processStartInfo = new ProcessStartInfo("explorer.exe", ftp_path);
            run_file.StartInfo = processStartInfo;
            run_file.StartInfo.UseShellExecute = true;
            run_file.Start();
        }

        private void es1_Newfolder_Click(object sender, RoutedEventArgs e)
        {
            NewFolderEQP(1);
        }

        private void es1_Openfolder_Click(object sender, RoutedEventArgs e)
        {
            if (this.line_set1_eqp.Text == "") return;
            string temp = this.line_set1_eqp.Text;
            string ftp_path = string.Format("ftp://{0}:{1}@{2}:{3}", source_ftp_info1["id"], source_ftp_info1["pw"],
                source_ftp_info1["ip"], temp.Substring(7 + source_ftp_info1["ip"].Length, temp.Length - source_ftp_info1["ip"].Length - 7));            

            var run_file = new Process();
            ProcessStartInfo processStartInfo = new ProcessStartInfo("explorer.exe", ftp_path);
            run_file.StartInfo = processStartInfo;
            run_file.StartInfo.UseShellExecute = true;
            run_file.Start();
        }

        private void es1_Rename_Click(object sender, RoutedEventArgs e)
        {
            RenameFun(3);
        }

        private void es1_Del_Click(object sender, RoutedEventArgs e)
        {
            DeleteEQPFileFoler(1);
        }

        //EQP Context        
        private void list_set2_eqp_all_PreviewMouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            var items = this.list_set2_eqp_all.SelectedItems;
            ContextMenu pMenu = null;
            if (items.Count == 0)
            {
                pMenu = (ContextMenu)this.Resources["EQP2_Unselect_ctxt"];
                pMenu.IsOpen = true;
            }
            else
            {
                pMenu = (ContextMenu)this.Resources["EQP2_Select_ctxt"];
                pMenu.IsOpen = true;
            }
        }

        private void eu2_Newfolder_Click(object sender, RoutedEventArgs e)
        {
            NewFolderEQP(2);
        }

        private void eu2_Openfolder_Click(object sender, RoutedEventArgs e)
        {
            if (this.line_set2_eqp.Text == "") return;
            string temp = this.line_set2_eqp.Text;
            string ftp_path = string.Format("ftp://{0}:{1}@{2}:{3}", source_ftp_info2["id"], source_ftp_info2["pw"],
                source_ftp_info2["ip"], temp.Substring(7 + source_ftp_info2["ip"].Length, temp.Length - source_ftp_info2["ip"].Length - 7));

            var run_file = new Process();
            ProcessStartInfo processStartInfo = new ProcessStartInfo("explorer.exe", ftp_path);
            run_file.StartInfo = processStartInfo;
            run_file.StartInfo.UseShellExecute = true;
            run_file.Start();
        }

        private void es2_Newfolder_Click(object sender, RoutedEventArgs e)
        {
            NewFolderEQP(2);
        }

        private void es2_Openfolder_Click(object sender, RoutedEventArgs e)
        {
            if (this.line_set2_eqp.Text == "") return;
            string temp = this.line_set2_eqp.Text;
            string ftp_path = string.Format("ftp://{0}:{1}@{2}:{3}", source_ftp_info2["id"], source_ftp_info2["pw"],
                source_ftp_info2["ip"], temp.Substring(7 + source_ftp_info2["ip"].Length, temp.Length - source_ftp_info2["ip"].Length - 7));

            var run_file = new Process();
            ProcessStartInfo processStartInfo = new ProcessStartInfo("explorer.exe", ftp_path);
            run_file.StartInfo = processStartInfo;
            run_file.StartInfo.UseShellExecute = true;
            run_file.Start();
        }

        private void es2_Rename_Click(object sender, RoutedEventArgs e)
        {
            RenameFun(4);
        }

        private void es2_Del_Click(object sender, RoutedEventArgs e)
        {
            DeleteEQPFileFoler(2);
        }

        private void ls1_Upload_Click(object sender, RoutedEventArgs e)
        {
            //설비로 확산하기
            if (uiList_listView[0].SelectedItems.Count > 1)
            {
                MessageBox.Show("1개의 폴더(파일)만 확산 가능합니다.");
                return;
            }
            var item = uiList_listView[0].SelectedItem;
            var cast_item = (Local_path_all1)item;
            string real_path = uiList_textbox[0].Text + "\\" + cast_item.local_all_filename;            
            NewView.ViewDistruibute viewDistruibute = new NewView.ViewDistruibute("LTE", real_path, "default_drive", "dummy_eqp");
            viewDistruibute.Show();            
        }

        private void ls2_Upload_Click(object sender, RoutedEventArgs e)
        {
            //설비로 확산하기
            if (uiList_listView[1].SelectedItems.Count > 1)
            {
                MessageBox.Show("1개의 폴더(파일)만 확산 가능합니다.");
                return;
            }
            var item = uiList_listView[1].SelectedItem;
            var cast_item = (Local_path_all2)item;
            string real_path = uiList_textbox[1].Text + "\\" + cast_item.local_all_filename;
            NewView.ViewDistruibute viewDistruibute = new NewView.ViewDistruibute("LTE", real_path, "default_drive", "dummy_eqp");
            viewDistruibute.Show();
        }

        private void dis_localtoeqp_Click(object sender, RoutedEventArgs e)
        {
            NewView.ViewDistruibute viewDistruibute = new NewView.ViewDistruibute("LTE_default", "", "default_drive", "dummy_eqp");
            viewDistruibute.Show();
        }

        private void chk_set1_eqp_Click(object sender, RoutedEventArgs e)
        {
            if ((bool)this.chk_set1_eqp.IsChecked)
            {
                string str_ref = this.line_set1_eqp.Text;
                if (this.line_set1_eqp.Text == "") return;
                int idx = this.line_set1_eqp.Text.LastIndexOf(":21");                
                if (str_ref.Substring(str_ref.Length - 3, 3) == ":21") return;

                //Debug.WriteLine(str_ref.Substring(idx + 4, str_ref.Length - idx - 4));
                this.line_SavePath1.Text = str_ref.Substring(idx + 4, str_ref.Length - idx - 4);

                string drive = this.cur_eqp_set1.Text.Substring(this.cur_eqp_set1.Text.Length - 3, 3);

                this.line_SavePath1_2.Text = drive;
            }
        }

        private void chk_set2_eqp_Click(object sender, RoutedEventArgs e)
        {
            if ((bool)this.chk_set2_eqp.IsChecked)
            {
                string str_ref = this.line_set2_eqp.Text;
                if (this.line_set2_eqp.Text == "") return;
                int idx = this.line_set2_eqp.Text.LastIndexOf(":21");
                if (str_ref.Substring(str_ref.Length - 3, 3) == ":21") return;

                //Debug.WriteLine(str_ref.Substring(idx + 4, str_ref.Length - idx - 4));
                this.line_SavePath2.Text = str_ref.Substring(idx + 4, str_ref.Length - idx - 4);

                string drive = this.cur_eqp_set2.Text.Substring(this.cur_eqp_set2.Text.Length - 3, 3);

                this.line_SavePath2_2.Text = drive;
            }
        }

        private void es1_Upload_Click(object sender, RoutedEventArgs e)
        {
            //설비로 확산하기
            if (uiList_listView[2].SelectedItems.Count > 1)
            {
                MessageBox.Show("1개의 폴더(파일)만 확산 가능합니다.");
                return;
            }
            string real_path = "";
            var item = uiList_listView[2].SelectedItem;
            var cast_item = (EQP_path_all1)item;
            string str_ref = this.line_set1_eqp.Text;
            int idx = this.line_set1_eqp.Text.LastIndexOf(":21");
                        
            var drive_item = this.List_Drive_eqpSet1.SelectedItem;
            string drive = (string)drive_item;
            
            if (idx + 3 != str_ref.Length) real_path = str_ref.Substring(idx + 4, str_ref.Length - idx - 4) + "/" + cast_item.eqp_all_filename;
            else real_path = cast_item.eqp_all_filename;
            NewView.ViewDistruibute viewDistruibute = new NewView.ViewDistruibute("ETL", real_path, drive, "dummy_eqp");
            viewDistruibute.Show();
        }

        private void es2_Upload_Click(object sender, RoutedEventArgs e)
        {
            //설비로 확산하기
            if (uiList_listView[3].SelectedItems.Count > 1)
            {
                MessageBox.Show("1개의 폴더(파일)만 확산 가능합니다.");
                return;
            }
            string real_path = "";
            var item = uiList_listView[3].SelectedItem;
            var cast_item = (EQP_path_all2)item;
            string str_ref = this.line_set2_eqp.Text;
            int idx = this.line_set2_eqp.Text.LastIndexOf(":21");

            var drive_item = this.List_Drive_eqpSet2.SelectedItem;
            string drive = (string)drive_item;

            if (idx + 3 != str_ref.Length) real_path = str_ref.Substring(idx + 4, str_ref.Length - idx - 4) + "/" + cast_item.eqp_all_filename;
            else real_path = cast_item.eqp_all_filename;
            NewView.ViewDistruibute viewDistruibute = new NewView.ViewDistruibute("ETL", real_path, drive, "dummy_eqp");
            viewDistruibute.Show();
        }
        
        

        private void ImageShowRun(int set_flag, int src_flag)
        {
            //이미지뷰어 활성화되어있을 때만 작동            
            
            if (set_flag == 1 && control_Vis.Vimg1_local == Visibility.Hidden) return;
            else if (set_flag == 2 && control_Vis.Vimg2_local == Visibility.Hidden) return;
            else if (set_flag == 3 && control_Vis.Vimg1_eqp == Visibility.Hidden) return;
            else if (set_flag == 4 && control_Vis.Vimg2_eqp == Visibility.Hidden) return;

            if (uiList_listView[src_flag - 1].SelectedItems.Count != 1) return;

            Dictionary<string, string> dic_img = new Dictionary<string, string>();

            dynamic cast_item = null;
            string extension = "";
            string id = "";
            string pw = "";

            if (set_flag == 1)
            {
                cast_item = (EQP_path_all1)this.list_set1_eqp_all.SelectedItem;
                extension = cast_item.eqp_all_extension;
                if (this.List_Drive_eqpSet1.SelectedItem.ToString().Contains("C"))
                {
                    id = "LS_C";
                    pw = "LS_C";
                }
                else if (this.List_Drive_eqpSet1.SelectedItem.ToString().Contains("D"))
                {
                    id = "LS_D";
                    pw = "LS_D";
                }
                else
                {
                    id = "LS_E";
                    pw = "LS_E";
                }
            }
            else if (set_flag == 2)
            {
                cast_item = (EQP_path_all2)this.list_set2_eqp_all.SelectedItem;
                extension = cast_item.eqp_all_extension;
                if (this.List_Drive_eqpSet2.SelectedItem.ToString().Contains("C"))
                {
                    id = "LS_C";
                    pw = "LS_C";
                }
                else if (this.List_Drive_eqpSet2.SelectedItem.ToString().Contains("D"))
                {
                    id = "LS_D";
                    pw = "LS_D";
                }
                else
                {
                    id = "LS_E";
                    pw = "LS_E";
                }
            }
            else if (set_flag == 3)
            {
                cast_item = (Local_path_all1)this.list_set1_local_all.SelectedItem;
                extension = cast_item.local_all_extension;
            }
            else if (set_flag == 4)
            {
                cast_item = (Local_path_all2)this.list_set2_local_all.SelectedItem;
                extension = cast_item.local_all_extension;
            }

            if (!Global_FunSet.IsImageExtension(extension)) return;

            string source = "";
            string target = "";            

            if (set_flag < 3)
            {
                source = uiList_textbox[src_flag - 1].Text + "/" + cast_item.eqp_all_filename;
                target = @"C:\Load_Spectrum2\Image\" + cast_item.eqp_all_filename;
                extension = cast_item.eqp_all_filename;
            }
            else
            {
                source = uiList_textbox[src_flag - 1].Text + "\\" + cast_item.local_all_filename;
                target = source;
                extension = cast_item.local_all_filename;
            }

            dic_img.Add("id", id);
            dic_img.Add("pw", pw);
            dic_img.Add("source", source);
            dic_img.Add("target", target);
            dic_img.Add("extension", extension);
            dic_img.Add("set_flag", set_flag.ToString());
            dic_img.Add("src_flag", src_flag.ToString());

            Thread img_show = new Thread(new ParameterizedThreadStart(ImageViewerShow));
            uiList_pgb[src_flag - 1].Value = 0;
            uiList_pgb[src_flag - 1].IsIndeterminate = true;
            uiList_pgbTxt[src_flag - 1].Text = "이미지 로드중";            

            img_show.IsBackground = true;
            img_show.Start(dic_img);
        }

        public void ImageViewerShow(object dic)
        {
            //이미지이면 다운로드
            var dic_img = (Dictionary<string, string>)dic;
            string id = dic_img["id"].ToString();
            string pw = dic_img["pw"].ToString();
            string source = dic_img["source"].ToString();
            string target = dic_img["target"].ToString();
            string extension = dic_img["extension"].ToString();
            int set_flag = Int32.Parse(dic_img["set_flag"].ToString());
            int src_flag = Int32.Parse(dic_img["src_flag"].ToString());

            if (File.Exists(target))
            {
                //파일 있으면
                Dispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate
                {
                    if (set_flag == 1) control_Vis.Vimg1_local_src = target;
                    else if (set_flag == 2) control_Vis.Vimg2_local_src = target;
                    else if (set_flag == 3) control_Vis.Vimg1_eqp_src = target;
                    else if (set_flag == 4) control_Vis.Vimg2_eqp_src = target;
                    uiList_pgb[src_flag - 1].Value = 100;
                    uiList_pgb[src_flag - 1].IsIndeterminate = false;
                    uiList_pgbTxt[src_flag - 1].Text = "이미지 완료";
                }));
                
            }
            else
            {
                //파일 없으면

                //소스가 이미지라면
                if (Global_FunSet.IsImageExtension(extension))
                {
                    //소스가 EQP라면 다운 필요
                    if (src_flag > 2)
                    {
                        Global_FunSet.DownImage(source, target, id, pw);
                        Dispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate
                        {   
                            if (src_flag == 3) control_Vis.Vimg1_local_src = target;
                            else control_Vis.Vimg2_local_src = target;
                            uiList_pgb[src_flag - 1].Value = 100;
                            uiList_pgb[src_flag - 1].IsIndeterminate = false;
                            uiList_pgbTxt[src_flag - 1].Text = "이미지 완료";                            
                        }));
                    }
                    //소스가 Local
                    else
                    {
                        Dispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate
                        {
                            if (set_flag == 3) control_Vis.Vimg1_eqp_src = target;
                            else if (set_flag == 4) control_Vis.Vimg2_eqp_src = target;
                            uiList_pgb[src_flag - 1].Value = 100;
                            uiList_pgb[src_flag - 1].IsIndeterminate = false;
                            uiList_pgbTxt[src_flag - 1].Text = "이미지 완료";
                        }));
                        
                    }
                }
            }            
        }

        private void set_extendFun()
        {
            //set1 확장하기
            /*
                stack1_local
                stack2_local
                stack1_eqp
                stack2_eqp

                imgstack1_local
                imgstack2_local
                imgstack1_eqp
                imgstack2_eqp

             */
            if ((bool)chk_set1_extend.IsChecked)
            {
                this.chk_localset1_image.IsChecked = false;
                this.chk_localset2_image.IsChecked = false;
                this.chk_eqpset1_image.IsChecked = false;
                this.chk_eqpset2_image.IsChecked = false;

                control_Vis.Vstack1_local = Visibility.Visible;
                control_Vis.Vstack2_local = Visibility.Hidden;
                control_Vis.Vstack1_eqp = Visibility.Visible;
                control_Vis.Vstack2_eqp = Visibility.Hidden;
                control_Vis.Vlocal2_path = Visibility.Hidden;
                control_Vis.VEQP2_path = Visibility.Hidden;

                this.list_set1_local_all.Height = 816;
                this.list_set1_eqp_all.Height = 816;
                control_Vis.Vrowspan_local = 2;
                control_Vis.Vrowspan_eqp = 2;

                control_Vis.Vimg1_local = Visibility.Hidden;
                control_Vis.Vimg2_local = Visibility.Hidden;
                control_Vis.Vimg1_eqp = Visibility.Hidden;
                control_Vis.Vimg2_eqp = Visibility.Hidden;

                control_Vis.Vimg1_local_src = "";
                control_Vis.Vimg2_local_src = "";
                control_Vis.Vimg1_eqp_src = "";
                control_Vis.Vimg2_eqp_src = "";

                control_Vis.canvas_vis = Visibility.Visible;
            }
            else
            {
                Vis_Normal();
            }
        }

        private void chk_set1_extend_Click(object sender, RoutedEventArgs e)
        {
            set_extendFun();
        }

        public void Vis_Normal()
        {
            control_Vis.Vstack1_local = Visibility.Visible;
            control_Vis.Vstack2_local = Visibility.Visible;
            control_Vis.Vstack1_eqp = Visibility.Visible;
            control_Vis.Vstack2_eqp = Visibility.Visible;            
            control_Vis.Vlocal2_path = Visibility.Visible;
            control_Vis.VEQP2_path = Visibility.Visible;

            this.list_set1_local_all.Height = 375;
            this.list_set1_eqp_all.Height = 375;
            control_Vis.Vrowspan_local = 1;
            control_Vis.Vrowspan_eqp = 1;

            control_Vis.Vimg1_local = Visibility.Hidden;
            control_Vis.Vimg2_local = Visibility.Hidden;
            control_Vis.Vimg1_eqp = Visibility.Hidden;
            control_Vis.Vimg2_eqp = Visibility.Hidden;

            control_Vis.Vimg1_local_src = "";
            control_Vis.Vimg2_local_src = "";
            control_Vis.Vimg1_eqp_src = "";
            control_Vis.Vimg2_eqp_src = "";

            control_Vis.canvas_vis = Visibility.Hidden;
        }

        private void Vis_Img(int set_flag)
        {
            //확장 켜져 있으면 확장 원복 먼저
            if ((bool)this.chk_set1_extend.IsChecked)
            {
                this.chk_set1_extend.IsChecked = false;
                Vis_Normal();
            }

            //set_flag는 이미지가 켜지는 set
            if (set_flag == 1)
            {
                control_Vis.Vstack1_local = Visibility.Hidden;                

                this.list_set1_local_all.Height = 375;
                this.list_set1_eqp_all.Height = 375;
                control_Vis.Vrowspan_local = 1;
                control_Vis.Vrowspan_eqp = 1;

                control_Vis.Vimg1_local = Visibility.Visible;                
            }
            else if (set_flag == 2)
            {
                control_Vis.Vstack2_local = Visibility.Hidden;

                this.list_set1_local_all.Height = 375;
                this.list_set1_eqp_all.Height = 375;
                control_Vis.Vrowspan_local = 1;
                control_Vis.Vrowspan_eqp = 1;

                control_Vis.Vimg2_local = Visibility.Visible;
            }
            else if (set_flag == 3)
            {
                control_Vis.Vstack1_eqp = Visibility.Hidden;

                this.list_set1_local_all.Height = 375;
                this.list_set1_eqp_all.Height = 375;
                control_Vis.Vrowspan_local = 1;
                control_Vis.Vrowspan_eqp = 1;

                control_Vis.Vimg1_eqp = Visibility.Visible;
            }
            else if (set_flag == 4)
            {
                control_Vis.Vstack2_eqp = Visibility.Hidden;

                this.list_set1_local_all.Height = 375;
                this.list_set1_eqp_all.Height = 375;
                control_Vis.Vrowspan_local = 1;
                control_Vis.Vrowspan_eqp = 1;

                control_Vis.Vimg2_eqp = Visibility.Visible;
            }

        }

        private void Vis_Img_off(int set_flag)
        {
            //set_flag는 이미지가 켜지는 set
            if (set_flag == 1)
            {
                control_Vis.Vstack1_local = Visibility.Visible;

                this.list_set1_local_all.Height = 375;
                this.list_set1_eqp_all.Height = 375;
                control_Vis.Vrowspan_local = 1;
                control_Vis.Vrowspan_eqp = 1;

                control_Vis.Vimg1_local = Visibility.Hidden;
            }
            else if (set_flag == 2)
            {
                control_Vis.Vstack2_local = Visibility.Visible;

                this.list_set1_local_all.Height = 375;
                this.list_set1_eqp_all.Height = 375;
                control_Vis.Vrowspan_local = 1;
                control_Vis.Vrowspan_eqp = 1;

                control_Vis.Vimg2_local = Visibility.Hidden;
            }
            else if (set_flag == 3)
            {
                control_Vis.Vstack1_eqp = Visibility.Visible;

                this.list_set1_local_all.Height = 375;
                this.list_set1_eqp_all.Height = 375;
                control_Vis.Vrowspan_local = 1;
                control_Vis.Vrowspan_eqp = 1;

                control_Vis.Vimg1_eqp = Visibility.Hidden;
            }
            else if (set_flag == 4)
            {
                control_Vis.Vstack2_eqp = Visibility.Visible;

                this.list_set1_local_all.Height = 375;
                this.list_set1_eqp_all.Height = 375;
                control_Vis.Vrowspan_local = 1;
                control_Vis.Vrowspan_eqp = 1;

                control_Vis.Vimg2_eqp = Visibility.Hidden;
            }

        }

        private void chk_localset2_image_Click(object sender, RoutedEventArgs e)
        {
            if ((bool)this.chk_localset2_image.IsChecked)
            {
                Vis_Img(2);
            }
            else
            {
                Vis_Img_off(2);
            }
        }

        private void chk_eqpset1_image_Click(object sender, RoutedEventArgs e)
        {
            if ((bool)this.chk_eqpset1_image.IsChecked)
            {
                Vis_Img(3);
            }
            else
            {
                Vis_Img_off(3);
            }
        }

        private void chk_eqpset2_image_Click(object sender, RoutedEventArgs e)
        {
            if ((bool)this.chk_eqpset2_image.IsChecked)
            {
                Vis_Img(4);
            }
            else
            {
                Vis_Img_off(4);
            }
        }

        private void chk_localset1_image_Click(object sender, RoutedEventArgs e)
        {
            //EQP 이미지 뷰어 
            if ((bool)this.chk_localset1_image.IsChecked)
            {
                Vis_Img(1);
            }
            else
            {
                Vis_Img_off(1);
            }

        }

        private void list_set1_eqp_all_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ImageShowRun(1, 3);
        }

        private void list_set1_local_all_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ImageShowRun(3, 1);
        }

        private void list_set2_local_all_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ImageShowRun(4, 2);
        }

        private void list_set2_eqp_all_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ImageShowRun(2, 4);
        }


        //지침서 다운로드 부가기능
        private void btn_enter_Click(object sender, RoutedEventArgs e)
        {
            Thread thread_mguide = new Thread(new ParameterizedThreadStart(search_mguide));
            thread_mguide.IsBackground = true;
            thread_mguide.Start();
        }

        private void search_mguide(object o)
        {
            string partid = "";
            Dispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate
            {
                this.pgb_mguide.IsIndeterminate = true;
                partid = this.line_partid.Text;
            }));            

            List<Control_Vis> source_mguide = new List<Control_Vis>();            
            string uri = "";
            string s_line = "";
            int return_flag = 0;
            //지침서 서버 정보

            Dispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate
            {
                if (this.combo_mguide.SelectedItem == null)
                {
                    MessageBox.Show("라인을 선택하세요");
                    return_flag = 1;
                    return;
                }
                else
                {
                    s_line = ((ComboBoxItem)this.combo_mguide.SelectedItem).Content.ToString();
                }
            }));

            if (return_flag == 1)
            {
                Dispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate
                {
                    this.pgb_mguide.IsIndeterminate = false;
                }));                
                return;
            }
            
            uri = gfun.source_mguide_LineURI[s_line] + "/" + partid;
            string id = gfun.mguide_id;
            string pw = gfun.mguide_pw;

            //FTP 접속
            try
            {
                FtpWebRequest ftpReq = (FtpWebRequest)WebRequest.Create(new Uri(uri.Replace("#", "%23")));                   //ftp 생성                
                ftpReq.Credentials = new NetworkCredential(id, pw);
                ftpReq.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
                ftpReq.Timeout = Global_FunSet.ftpTimeout;
                FtpWebResponse resFtp = (FtpWebResponse)ftpReq.GetResponse();

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
                    string full_name = "";
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

                        string ini_name = name;
                        full_name = name;
                        full_name = full_name.Substring(full_name.IndexOf("-") + 1, full_name.Length - full_name.IndexOf("-") - 1);
                        full_name = full_name.Substring(full_name.IndexOf("-") + 1, full_name.Length - full_name.IndexOf("-") - 1);
                        full_name = full_name.Substring(full_name.IndexOf("-") + 1, full_name.Length - full_name.IndexOf("-") - 1);
                        full_name = full_name.Substring(0, full_name.IndexOf("-"));
                        name = full_name;

                        if (ini_name.Contains("TOXMaster") && !ini_name.Contains("Before"))
                        {
                            source_mguide.Add(new Control_Vis()
                            {
                                mguide_ver = name,
                                mguide_date = modified
                            });
                        }

                    }
                    catch
                    {

                    }
                }

                Dispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate
                {
                    this.list_mguide.DataContext = source_mguide;
                    this.list_mguide.ItemsSource = source_mguide;                    
                    source_mguide.Sort(new SortStringNumber());                    
                    this.list_mguide.Items.Refresh();
                    this.pgb_mguide.IsIndeterminate = false;
                }));
                
                resFtp.Close();

            }
            catch
            {
                MessageBox.Show("PartID를 다시 확인하세요");
                Dispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate
                {
                    this.pgb_mguide.IsIndeterminate = false;
                }));
                
                return;
            }
        }

        private void btn_ToxMaster_Click(object sender, RoutedEventArgs e)
        {
            download_mguide("TOXMaster");
        }

        private void btn_IMMaster_Click(object sender, RoutedEventArgs e)
        {
            download_mguide("IM_Master");
        }

        private void btn_ToxXY_Click(object sender, RoutedEventArgs e)
        {
            download_mguide("TOXCoord");
        }

        private void btn_WFMap_Click(object sender, RoutedEventArgs e)
        {
            download_mguide("WaferMap");
        }

        private void download_mguide(string m_str)
        {
            //TOXMaster 다운 클릭
            if (this.list_mguide.Items.Count == 0) return;
            var items = this.list_mguide.SelectedItems;
            if (items.Count == 0) return;
            string partid = this.line_partid.Text;

            Dictionary<string, dynamic> source = new Dictionary<string, dynamic>();

            source.Add("items", items);
            source.Add("m_str", m_str);
            source.Add("partid", partid);
            source.Add("id", gfun.mguide_id);
            source.Add("pw", gfun.mguide_pw);            

            Thread thread_mguide = new Thread(new ParameterizedThreadStart(download_mguide_th));
            thread_mguide.IsBackground = true;
            thread_mguide.Start(source);
        }

        private void download_mguide_th(object o)
        {
            var dic = (Dictionary<string, dynamic>)o;            
            IList items = (IList)dic["items"];
            string m_str = (string)dic["m_str"];
            string partid = (string)dic["partid"];
            string id = (string)dic["id"];
            string pw = (string)dic["pw"];
            string s_line = "";

            Dispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate
            {                
                this.pgb_mguide.IsIndeterminate = true;
                s_line = ((ComboBoxItem)this.combo_mguide.SelectedItem).Content.ToString();
            }));            

            string uri = gfun.source_mguide_LineURI[s_line] + "/" + partid + "/";

            //partid 폴더 생성
            DirectoryInfo di_partid = new DirectoryInfo(gfun.default_path + "\\Mguide\\" + partid);
            if (di_partid.Exists == false) di_partid.Create();
            

            foreach (var item in items)
            {
                try
                {
                    var cast_item = (Control_Vis)item;
                    string version = cast_item.mguide_ver;

                    //ftp 파일 검색 
                    FtpWebRequest ftpReq = (FtpWebRequest)WebRequest.Create(new Uri(uri.Replace("#", "%23")));                   //ftp 생성                
                    ftpReq.Credentials = new NetworkCredential(id, pw);
                    ftpReq.Method = WebRequestMethods.Ftp.ListDirectory;
                    ftpReq.Timeout = Global_FunSet.ftpTimeout;
                    FtpWebResponse resFtp = (FtpWebResponse)ftpReq.GetResponse();

                    StreamReader reader;
                    reader = new StreamReader(resFtp.GetResponseStream());

                    string strData;
                    strData = reader.ReadToEnd();
                    string[] filesInDirectory = strData.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                    resFtp.Close();

                    foreach (string file in filesInDirectory)
                    {
                        string tar_str = partid + "-" + version + "-" + m_str;

                        if (file.Contains(tar_str) && !file.Contains("Before"))
                        {
                            string src_path = gfun.source_mguide_LineURI[s_line] + "/" + partid + "/" + file;
                            string tar_path = gfun.default_path + "\\" + "Mguide\\" + partid +"/" + file.Replace("/", "\\");

                            var request2 = (FtpWebRequest)WebRequest.Create(new Uri(src_path.Replace("#", "%23")));
                            request2.Credentials = new NetworkCredential(id, pw);
                            request2.Method = WebRequestMethods.Ftp.DownloadFile;
                            request2.Timeout = Global_FunSet.ftpTimeout;
                            FtpWebResponse resFtp2 = (FtpWebResponse)request2.GetResponse();

                            Stream sourceStream2 = resFtp2.GetResponseStream();
                            FileStream targetFileStream2 = new FileStream(tar_path, FileMode.Create, FileAccess.Write);
                            byte[] bufferByteArray = new byte[1024];

                            while (true)
                            {
                                int byteCount = sourceStream2.Read(bufferByteArray, 0, bufferByteArray.Length);
                                if (byteCount == 0)
                                {
                                    break;
                                }
                                targetFileStream2.Write(bufferByteArray, 0, byteCount);
                            }
                            targetFileStream2.Close();
                            sourceStream2.Close();
                        }
                    }
                }
                catch
                {
                    MessageBox.Show("다운로드중 오류발생");
                    Dispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate
                    {
                        this.pgb_mguide.IsIndeterminate = false;
                        this.pgb_mguide.Value = 0;
                    }));
                    return;
                }
            }
            Dispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate
            {
                this.pgb_mguide.IsIndeterminate = false;
                this.pgb_mguide.Value = 100;
            }));

        }

        private void combo_mguide_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.list_mguide.SelectedIndex = -1;
        }

        private void set1_olsa_Click(object sender, RoutedEventArgs e)
        {
            run_olsa(1);
        }

        private void run_olsa(int set_flag)
        {
            var run_file = new Process();
            run_file.StartInfo.FileName = @"C:\Load_Spectrum2\olsa_test.txt";
            run_file.StartInfo.UseShellExecute = true;
            run_file.Start();

            foreach(Window window in Application.Current.Windows)
            {
                Debug.WriteLine(window.Title);
            }


        }

        private void btn_openMguide_Click(object sender, RoutedEventArgs e)
        {
            Process.Start(@"C:\Load_Spectrum2\Mguide");
        }

        private void es1_ETE_Click(object sender, RoutedEventArgs e)
        {
            //설비로 확산하기
            if (uiList_listView[2].SelectedItems.Count > 1)
            {
                MessageBox.Show("1개의 폴더(파일)만 확산 가능합니다.");
                return;
            }
            string real_path = "";
            var item = uiList_listView[2].SelectedItem;
            var cast_item = (EQP_path_all1)item;
            string str_ref = this.line_set1_eqp.Text;
            int idx = this.line_set1_eqp.Text.LastIndexOf(":21");

            var drive_item = this.List_Drive_eqpSet1.SelectedItem;
            string drive = (string)drive_item;
            string src_eqp = this.cur_eqp_set1.Text.Substring(0, this.cur_eqp_set1.Text.IndexOf(" "));            

            if (idx + 3 != str_ref.Length) real_path = str_ref.Substring(idx + 4, str_ref.Length - idx - 4) + "/" + cast_item.eqp_all_filename;
            else real_path = cast_item.eqp_all_filename;
            NewView.ViewDistruibute viewDistruibute = new NewView.ViewDistruibute("ETE", real_path, drive, src_eqp);
            viewDistruibute.Show();
        }

        private void es2_ETE_Click(object sender, RoutedEventArgs e)
        {
            //설비로 확산하기
            if (uiList_listView[3].SelectedItems.Count > 1)
            {
                MessageBox.Show("1개의 폴더(파일)만 확산 가능합니다.");
                return;
            }
            string real_path = "";
            var item = uiList_listView[3].SelectedItem;
            var cast_item = (EQP_path_all2)item;
            string str_ref = this.line_set2_eqp.Text;
            int idx = this.line_set2_eqp.Text.LastIndexOf(":21");

            var drive_item = this.List_Drive_eqpSet2.SelectedItem;
            string drive = (string)drive_item;
            string src_eqp = this.cur_eqp_set2.Text.Substring(0, this.cur_eqp_set2.Text.IndexOf(" "));

            if (idx + 3 != str_ref.Length) real_path = str_ref.Substring(idx + 4, str_ref.Length - idx - 4) + "/" + cast_item.eqp_all_filename;
            else real_path = cast_item.eqp_all_filename;
            NewView.ViewDistruibute viewDistruibute = new NewView.ViewDistruibute("ETE", real_path, drive, src_eqp);
            viewDistruibute.Show();
        }

        public int FTPOneFileDown(string uri, string t_path, string id, string pw)
        {
            try
            {
                //최초 다운로드
                var request = (FtpWebRequest)WebRequest.Create(new Uri(uri.Replace("#", "%23")));
                request.Credentials = new NetworkCredential(id, pw);
                request.Method = WebRequestMethods.Ftp.DownloadFile;
                request.Timeout = 10000;     //5초안에 접속 안되면 종료
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

                //다운로드 성공시 1 반환
                return 1;
            }
            catch
            {
                //다운로드 실패시 -1 반환
                return -1;
            }

        }

        //////////////////////////////////////////////////////////////////////////
        ///설비별 이미지 Tab
        private void BackBtn_EI_Click(object sender, RoutedEventArgs e)
        {
            BackListview_EQP(3);
        }

        private void FowardBtn_EI_Click(object sender, RoutedEventArgs e)
        {
            EQP_FowardFun(3);
        }

        private void list_EI_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (header_flag == 1)
            {
                header_flag = 0;
                return;
            }
            EI_Mk_Img();
        }

        private void EI_Mk_Img()
        {
            ListView lv = this.list_EI;
            var items = (Control_Vis)lv.SelectedItem;

            if (items == null)
            {
                return;
            }

            control_Vis.strEI_src1 = "";
            control_Vis.strEI_src2 = "";
            control_Vis.strEI_src3 = "";
            control_Vis.strEI_src4 = "";
            control_Vis.strEI_src5 = "";
            control_Vis.strEI_src6 = "";
            control_Vis.strEI_src7 = "";
            control_Vis.strEI_src8 = "";
            control_Vis.strEI_src9 = "";
            control_Vis.strEI_src10 = "";
            control_Vis.strEI_src11 = "";
            control_Vis.strEI_src12 = "";

            if (items.eqp_all_extension == "폴더")
            {
                var items_eqp1 = (Control_Vis)this.list_EI.SelectedItem;
                string eqpid = this.cur_EI_text.Text.Substring(0, 7);

                source_ftp_info3["uri"] += "/" + items_eqp1.eqp_all_filename;
                source_ftp_info3["set_flag"] = "3";
                source_ftp_info3["foback_flag"] = "1";

                Thread thread_ftp_login = new Thread(new ParameterizedThreadStart(FTP_Login));
                this.pgb_EI.Value = 0;
                this.pgb_EI.IsIndeterminate = true;
                this.list_EI.IsHitTestVisible = false;
                thread_ftp_login.IsBackground = true;
                thread_ftp_login.Start(source_ftp_info3);
            }
            else
            {
                //파일이라면
                if (this.List_EQPID_EI_Rst.Items.Count == 0)
                {
                    MessageBox.Show("설비 이미지를 보려면 아래에서 설비를 선택해야 합니다.");
                    return;
                }
                else
                {
                    //설비별로 유사파일 다운로드
                    //경로 바인딩
                    Dictionary<string, object> th_dic = new Dictionary<string, object>();
                    List<string> selected_eqp = new List<string>();
                    foreach (var item in this.List_EQPID_EI_Rst.Items)
                    {
                        var cast_item = (EQP_Info_EISelect)item;
                        selected_eqp.Add(cast_item.strEQPID_EIselect);
                    }
                    th_dic.Add("s_eqp", selected_eqp);
                    th_dic.Add("s_file", ((Control_Vis)this.list_EI.SelectedItem).eqp_all_filename);

                    Thread thread_EI = new Thread(new ParameterizedThreadStart(Thread_EI));
                    this.pgb_EI.Value = 0;
                    this.pgb_EI.IsIndeterminate = true;
                    this.list_EI.IsHitTestVisible = false;
                    thread_EI.IsBackground = true;
                    thread_EI.Start(th_dic);
                }
            }
        }

        private void Thread_EI(object o)
        {
            Dictionary<string, object> th_dic = (Dictionary<string, object>)o;
            List<string> s_eqp = (List<string>)th_dic["s_eqp"];
            string s_file = th_dic["s_file"].ToString();
            string id = "";
            string pw = "";
            string ip = "";
            string real_path = "";
            Dispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate
            {
                if (this.cur_EI_text.Text.Contains("C"))
                {
                    id = "LS_C";
                    pw = "LS_C";
                }
                else if (this.cur_EI_text.Text.Contains("D"))
                {
                    id = "LS_D";
                    pw = "LS_D";
                }
                else if (this.cur_EI_text.Text.Contains("E"))
                {
                    id = "LS_E";
                    pw = "LS_E";
                }
            }));
            
            int cnt = 0;
            foreach (var eqp in s_eqp)
            {                
                string new_path = "";
                ip = gfun.source_EQP_dict[eqp];
                Dispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate
                {                       
                    string str_ref = this.line_EI.Text;
                    int idx = this.line_EI.Text.LastIndexOf(":21");
                    real_path = str_ref.Substring(idx + 4, str_ref.Length - idx - 4) + "/" + s_file;
                }));
                string eqp_img_src = Global_FunSet.ReturnSimilarImagePath(real_path, ip, id, pw);
                string eqp_file = eqp_img_src.Substring(eqp_img_src.LastIndexOf("/") + 1, eqp_img_src.Length - eqp_img_src.LastIndexOf("/") - 1);
                if (eqp_img_src != "")
                {
                    FTPOneFileDown(string.Format(@"FTP://{0}:21/{1}", ip, eqp_img_src), string.Format(@"C:\Load_Spectrum2\Temp\{0}", eqp_file) , id, pw);
                    new_path = @"C:\Load_Spectrum2\Temp\" + eqp_file;                                        
                }

                if (cnt == 0) control_Vis.strEI_src1 = new_path;
                else if (cnt == 1) control_Vis.strEI_src2 = new_path;
                else if (cnt == 2) control_Vis.strEI_src3 = new_path;
                else if (cnt == 3) control_Vis.strEI_src4 = new_path;
                else if (cnt == 4) control_Vis.strEI_src5 = new_path;
                else if (cnt == 5) control_Vis.strEI_src6 = new_path;
                else if (cnt == 6) control_Vis.strEI_src7 = new_path;
                else if (cnt == 7) control_Vis.strEI_src8 = new_path;
                else if (cnt == 8) control_Vis.strEI_src9 = new_path;
                else if (cnt == 9) control_Vis.strEI_src10 = new_path;
                else if (cnt == 10) control_Vis.strEI_src11 = new_path;
                else if (cnt == 11) control_Vis.strEI_src12 = new_path;
                cnt++;
            }

            Dispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate
            {
                this.pgb_EI.Value = 100;
                this.pgb_EI.IsIndeterminate = false;
                this.list_EI.IsHitTestVisible = true;
            }));
        }

        private void list_EI_Click(object sender, RoutedEventArgs e)
        {
            NewListColumnSort(sender, e, 5);
            header_flag = 1;
        }

        private void EQPIDResetBtn_EI_Click(object sender, RoutedEventArgs e)
        {
            this.List_Line_EI.SelectedIndex = -1;
            this.List_ZONE_EI.SelectedIndex = -1;
            this.List_EQPTYPE_EI.SelectedIndex = -1;
            this.List_EQPMODEL_EI.SelectedIndex = -1;
        }        

        private void LineResetBtn_EI_Click(object sender, RoutedEventArgs e)
        {
            this.List_Line_EI.SelectedIndex = -1;
        }

        private void List_Line_EI_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            EQPID_Filter(3);
        }

        private void ZONEResetBtn_EI_Click(object sender, RoutedEventArgs e)
        {
            this.List_ZONE_EI.SelectedIndex = -1;
        }

        private void List_ZONE_EI_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            EQPID_Filter(3);
        }

        private void EQPTYPEResetBtn_EI_Click(object sender, RoutedEventArgs e)
        {
            this.List_EQPTYPE_EI.SelectedIndex = -1;
        }

        private void List_EQPTYPE_EI_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            EQPID_Filter(3);
        }

        private void EQPMODELRsesetBtn_EI_Click(object sender, RoutedEventArgs e)
        {
            this.List_EQPMODEL_EI.SelectedIndex = -1;
        }

        private void List_EQPMODEL_EI_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            EQPID_Filter(3);
        }

        private void tb_EQPID_EI2_TextChanged(object sender, TextChangedEventArgs e)
        {
            EQPID_Filter(3);
        }

        private void List_EQPID_EI_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            AddListEQPID();
        }

        private void EI_TrBtn_Click(object sender, RoutedEventArgs e)
        {
            AddListEQPID();
        }

        private void List_EQPID_EI_Rst_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            SubListEQPID();
        }

        private void EI_TrbackBtn_Click(object sender, RoutedEventArgs e)
        {
            SubListEQPID();
        }

        //ADD 
        private void AddListEQPID()
        {
            ListView lv_src = this.List_EQPID_EI;
            ListView lv_tar = this.List_EQPID_EI_Rst;

            int idx = this.List_EQPID_EI.SelectedIndex;
            var items = this.List_EQPID_EI.SelectedItems;

            int cnt = 0;
            foreach (var item in items)
            {                
                var cast_item = (EQP_Info_EQPID3)item;
                source_EQP_Info_EQPID3.RemoveAll(x => x.strEQPID_Set3 == cast_item.strEQPID_Set3);
                source_EQP_Info_EQPID_Select.Add(new EQP_Info_EISelect() { strEQPID_EIselect = cast_item.strEQPID_Set3 });
                if (cnt > 12) break;
                cnt++;
            }

            lv_src.ItemsSource = null;
            lv_src.DataContext = source_EQP_Info_EQPID3;            
            lv_src.ItemsSource = source_EQP_Info_EQPID3;
            lv_src.Items.Refresh();

            lv_tar.ItemsSource = null;
            lv_tar.DataContext = source_EQP_Info_EQPID_Select;
            lv_tar.ItemsSource = source_EQP_Info_EQPID_Select;
            lv_tar.Items.Refresh();

            lv_src.Focus();
            try
            {
                lv_src.SelectedIndex = idx;
                MK_EI_Text();
            }
            catch { }            
        }

        //ADD 
        private void SubListEQPID()
        {
            ListView lv_src = this.List_EQPID_EI_Rst;
            ListView lv_tar = this.List_EQPID_EI;

            int idx = this.List_EQPID_EI_Rst.SelectedIndex;
            var items = this.List_EQPID_EI_Rst.SelectedItems;

            foreach (var item in items)
            {                
                var cast_item = (EQP_Info_EISelect)item;
                source_EQP_Info_EQPID_Select.RemoveAll(x => x.strEQPID_EIselect == cast_item.strEQPID_EIselect);
                source_EQP_Info_EQPID3.Add(new EQP_Info_EQPID3() { strEQPID_Set3 = cast_item.strEQPID_EIselect });
            }

            lv_src.ItemsSource = null;
            lv_src.DataContext = source_EQP_Info_EQPID_Select;
            lv_src.ItemsSource = source_EQP_Info_EQPID_Select;
            lv_src.Items.Refresh();

            lv_tar.ItemsSource = null;
            lv_tar.DataContext = source_EQP_Info_EQPID3;
            lv_tar.ItemsSource = source_EQP_Info_EQPID3;
            lv_tar.Items.Refresh();

            lv_src.Focus();
            try
            {
                lv_src.SelectedIndex = idx;
                MK_EI_Text();
            }
            catch { }
        }

        private void MK_EI_Text()
        {
            try { this.EI1.Text = ((EQP_Info_EISelect)this.List_EQPID_EI_Rst.Items[0]).strEQPID_EIselect; }            
            catch 
            {
                this.EI1.Text = "EQP1";
                control_Vis.strEI_src1 = "";
            }            

            try { this.EI2.Text = ((EQP_Info_EISelect)this.List_EQPID_EI_Rst.Items[1]).strEQPID_EIselect; }            
            catch 
            {
                this.EI2.Text = "EQP2";
                control_Vis.strEI_src2 = "";
            }            

            try { this.EI3.Text = ((EQP_Info_EISelect)this.List_EQPID_EI_Rst.Items[2]).strEQPID_EIselect; }            
            catch 
            {
                this.EI3.Text = "EQP3";
                control_Vis.strEI_src3 = "";
            }
            
            try { this.EI4.Text = ((EQP_Info_EISelect)this.List_EQPID_EI_Rst.Items[3]).strEQPID_EIselect; }            
            catch 
            {
                this.EI4.Text = "EQP4";
                control_Vis.strEI_src4 = "";
            }            

            try { this.EI5.Text = ((EQP_Info_EISelect)this.List_EQPID_EI_Rst.Items[4]).strEQPID_EIselect; }            
            catch 
            {
                this.EI5.Text = "EQP5";
                control_Vis.strEI_src5 = "";
            }            

            try { this.EI6.Text = ((EQP_Info_EISelect)this.List_EQPID_EI_Rst.Items[5]).strEQPID_EIselect; }            
            catch 
            {
                this.EI6.Text = "EQP6";
                control_Vis.strEI_src6 = "";
            }            

            try { this.EI7.Text = ((EQP_Info_EISelect)this.List_EQPID_EI_Rst.Items[6]).strEQPID_EIselect; }
            catch 
            {
                this.EI7.Text = "EQP7";
                control_Vis.strEI_src7 = "";
            }

            try { this.EI8.Text = ((EQP_Info_EISelect)this.List_EQPID_EI_Rst.Items[7]).strEQPID_EIselect; }
            catch 
            {
                this.EI8.Text = "EQP8";
                control_Vis.strEI_src8 = "";
            }

            try { this.EI9.Text = ((EQP_Info_EISelect)this.List_EQPID_EI_Rst.Items[8]).strEQPID_EIselect; }
            catch 
            {
                this.EI9.Text = "EQP9";
                control_Vis.strEI_src9 = "";
            }

            try { this.EI10.Text = ((EQP_Info_EISelect)this.List_EQPID_EI_Rst.Items[9]).strEQPID_EIselect; }
            catch 
            {
                this.EI10.Text = "EQP10";
                control_Vis.strEI_src10 = "";
            }

            try { this.EI11.Text = ((EQP_Info_EISelect)this.List_EQPID_EI_Rst.Items[10]).strEQPID_EIselect; }
            catch 
            {
                this.EI11.Text = "EQP11";
                control_Vis.strEI_src11 = "";
            }

            try { this.EI12.Text = ((EQP_Info_EISelect)this.List_EQPID_EI_Rst.Items[11]).strEQPID_EIselect; }
            catch 
            {
                this.EI12.Text = "EQP12";
                control_Vis.strEI_src12 = "";
            }
            
        }

        private void dis_LotSearch_Click(object sender, RoutedEventArgs e)
        {            
            Lot_Search lot_Search = new Lot_Search();
            lot_Search.Show();
        }
    }
}