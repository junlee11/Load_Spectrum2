using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace Load_Spectrum2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //Drag And Drop         
        #region Data operations
        void SaveData(IList source, IDataObject target)
        {
            string Buffer = "";
            Local_path_all1 cast_item1 = null;
            Local_path_all2 cast_item2 = null;
            EQP_path_all1 cast_item3 = null;
            EQP_path_all2 cast_item4 = null;
            string result = "";

            //cast_item1 = (EQP_path_all1)item;
            foreach (var Item in source)
            {
                if (source_set_flag == 1)
                {
                    cast_item1 = (Local_path_all1)Item;
                    result = cast_item1.local_all_filename;
                }
                else if (source_set_flag == 2)
                {
                    cast_item2 = (Local_path_all2)Item;
                    result = cast_item2.local_all_filename;
                }
                else if (source_set_flag == 3)
                {
                    cast_item3 = (EQP_path_all1)Item;
                    result = cast_item3.eqp_all_filename;
                }
                else if (source_set_flag == 4)
                {
                    cast_item4 = (EQP_path_all2)Item;
                    result = cast_item4.eqp_all_filename;
                }

                Buffer += (Buffer == "" ? "" : "\r\n") + "" + result;
                target.SetData(typeof(string), Buffer);
            }
        }

        bool CanLoadData(IDataObject source)
        {
            return source.GetDataPresent(typeof(string));
        }
        public string[] Drag_Items;        
        
        void LoadData(IDataObject source)
        {
            //드롭 액션 하는중
            //필요한거 set_flag, items
            //로컬 투 로컬 (1,2) (1,2)
            //로컬 투 EQP (1,2) (3,4)
            //EQP 투 로컬 (3,4) (1,2)
            //EQP 투 EQP (3,4) (3,4)
            try
            {
                if (source_set_flag == target_set_flag) return;
                if (source_set_flag == 0 || target_set_flag == 0) return;
                if (uiList_textbox[target_set_flag - 1].Text == "") return;

                //case 분류
                int case_flag = 0;
                if (source_set_flag <= 2 && target_set_flag <= 2) case_flag = 1;
                else if (source_set_flag <= 2 && target_set_flag >= 3) case_flag = 2;
                else if (source_set_flag >= 3 && target_set_flag <= 2) case_flag = 3;
                else if (source_set_flag >= 3 && target_set_flag >= 3) case_flag = 4;

                string Buffer = (string)source.GetData(typeof(string));
                string[] Separators = new string[] { "\r\n" };

                //Items가 최종 데이터
                Drag_Items = Buffer.Split(Separators,
                StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < Drag_Items.Length; i++)
                {
                    if (source_set_flag < 3) Drag_Items[i] = uiList_textbox[source_set_flag - 1].Text + "\\" + Drag_Items[i];
                    else Drag_Items[i] = uiList_textbox[source_set_flag - 1].Text + "/" + Drag_Items[i];
                }

                //로컬 투 로컬            
                if (case_flag == 1)
                {
                    TrLocalToLocal(source_set_flag, target_set_flag, uiList_textbox[source_set_flag - 1].Text, uiList_textbox[target_set_flag - 1].Text);
                }

                //로컬 투 EQP
                else if (case_flag == 2)
                {
                    TrLocalToEQP(source_set_flag, target_set_flag, uiList_textbox[source_set_flag - 1].Text, uiList_textbox[target_set_flag - 1].Text);
                }
                //EQP 투 로컬
                else if (case_flag == 3)
                {
                    TrEQPToLocal(source_set_flag, target_set_flag, uiList_textbox[source_set_flag - 1].Text, uiList_textbox[target_set_flag - 1].Text);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        #region Drop operations
        void MakeDropEffect(DragEventArgs e)
        {
            if (!CanLoadData(e.Data))
                e.Effects = DragDropEffects.None;
            else if ((e.KeyStates & DragDropKeyStates.AltKey) == DragDropKeyStates.AltKey)
                e.Effects = DragDropEffects.None;
            else
            {
                if ((e.KeyStates & DragDropKeyStates.ControlKey) ==
                DragDropKeyStates.ControlKey)
                {
                    if ((e.AllowedEffects & DragDropEffects.Copy) ==
                    DragDropEffects.Copy)
                    {
                        e.Effects = DragDropEffects.Copy;
                        return;
                    }
                }

                if ((e.KeyStates & DragDropKeyStates.ShiftKey) == DragDropKeyStates.ShiftKey)
                {
                    if ((e.AllowedEffects & DragDropEffects.Move) ==
                    DragDropEffects.Move)
                    {
                        e.Effects = DragDropEffects.Move;
                        return;
                    }
                }

                if ((e.AllowedEffects & DragDropEffects.Move) == DragDropEffects.Move)
                {
                    e.Effects = DragDropEffects.Move;
                    return;
                }

                if ((e.AllowedEffects & DragDropEffects.Copy) == DragDropEffects.Copy)
                {
                    e.Effects = DragDropEffects.Copy;
                    return;
                }
                e.Effects = DragDropEffects.None;
            }
        }

        void listView_DragEnter(object sender, DragEventArgs e)
        {
            e.Handled = true;
            MakeDropEffect(e);
        }

        void listView_DragOver(object sender, DragEventArgs e)
        {
            listView_DragEnter(sender, e);
        }

        

        void listView_Drop(object sender, DragEventArgs e)
        {
            ListView listView = (ListView)sender;

            if (listView.Name == "list_set1_local_all") target_set_flag = 1;
            else if (listView.Name == "list_set2_local_all") target_set_flag = 2;
            else if (listView.Name == "list_set1_eqp_all") target_set_flag = 3;
            else if (listView.Name == "list_set2_eqp_all") target_set_flag = 4;

            if (target_set_flag != source_set_flag && uiList_listView[target_set_flag - 1].Items.Count != 0)
            {
                uiList_listView[target_set_flag - 1].IsHitTestVisible = false;
                uiList_StopBtn[target_set_flag - 1].IsHitTestVisible = true;
            }   

            e.Handled = true;
            MakeDropEffect(e);
            if (e.Effects == DragDropEffects.Copy || e.Effects == DragDropEffects.Move)
            {
                LoadData(e.Data);                
            }
                
        }
        #endregion

        #region Drag start operation
        void StartDrag(ListView listView)
        {
            IList Selection = listView.SelectedItems;
            
            //cast_item1 = (EQP_path_all1)item;

            if (Selection.Count == 0)
                return;

            DataObject Buffer = new DataObject();
            SaveData(Selection, Buffer);

            DragDropEffects Result = DragDrop.DoDragDrop(listView, Buffer, DragDropEffects.Copy | DragDropEffects.Move);
        }

        #endregion

        #region Mouse events handling for both multiple selection and drag start
        void listView_LostMouseCapture(object sender, MouseEventArgs e)
        {
            //Log("LostMouseCapture()");
            ListView listView = (ListView)sender;
            listView.Tag = null;
        }

        void listView_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            ListView listView = (ListView)sender;
            
            if (!listView.IsMouseCaptured)
                return;
            e.Handled = true;

            System.Windows.Point P = e.GetPosition(listView);

            //Log("" + PP.X + "/" + PP.Y + " - " + P.X + "/" + P.Y);

            int Limit = 4;

            if (P.X - Limit > PP.X ||
            P.X + Limit < PP.X ||
            P.Y - Limit > PP.Y ||
            P.Y + Limit < PP.Y)
            {                
                listView.ReleaseMouseCapture();
                //setup the drag adorner.                
                StartDrag(listView);
            }
        }

        System.Windows.Point PP; // Mouse position for last PreviewMouseDown event
        public int source_set_flag;
        public int target_set_flag;
        
        void listView_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            source_set_flag = 0;
            target_set_flag = 0;
            
            ListView listView = (ListView)sender;
            listView.Tag = null;

            if (listView.Name == "list_set1_local_all") source_set_flag = 1;
            else if (listView.Name == "list_set2_local_all") source_set_flag = 2;
            else if (listView.Name == "list_set1_eqp_all") source_set_flag = 3;
            else if (listView.Name == "list_set2_eqp_all") source_set_flag = 4;

            PP = e.GetPosition(listView);

            ListViewItem Item = (ListViewItem)VisualTree.GetParent(e.OriginalSource, typeof(ListViewItem));

            //ListViewItem Item = uiList_listView[source_set_flag - 1].SelectedItems;            
            if (Item == null)
                return;

            if (Item.IsSelected && listView.CaptureMouse())
            {
                Log("PreviewMouseDown() - Selected item mouse down.");
                e.Handled = true;
                listView.Tag = Item;
            }
        }

        void listView_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            Log("PreviewMouseUp()");
            ListView listView = (ListView)sender;
            ListViewItem Item = (ListViewItem)listView.Tag;
            listView.Tag = null;
            if (Item == null)
                return;

            if (!listView.IsMouseCaptured)
                return;

            if (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl))
                Item.IsSelected = !Item.IsSelected;
            else
            {
                listView.SelectedItems.Clear();
                listView.SelectionMode = SelectionMode.Single;
                Item.IsSelected = true;
                listView.SelectionMode = SelectionMode.Extended;
            }

            e.Handled = true;

            listView.ReleaseMouseCapture();
            Log("PreviewMouseUp(): Item = " + Item);

            if (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl))
                Item.IsSelected = !Item.IsSelected;
            else

            {
                listView.SelectedItems.Clear();
                Item.IsSelected = true;
            }
            if (!Item.IsKeyboardFocused)
                Item.Focus();

        }
        #endregion
        void Log(object message)
        {
            System.Diagnostics.Debug.WriteLine(message);
        }




        /// <summary>
        /// 
        /// </summary>

        #region bw function
        //local to local
        BackgroundWorker bw_local_local;
        BackgroundWorker bw_local_local2;
        private void TrLocalToLocal(int src_flag, int tar_flag, string src_path, string tar_path)
        {
            try
            {
                //인터락 조건
                if (tar_flag == 3 || tar_flag == 4) return;     //타겟이 로컬 리스트
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
                if (src_flag == 1 && tar_flag == 2)
                {
                    bw_local_local = new BackgroundWorker();
                    Dictionary<string, dynamic> bw_src = new Dictionary<string, dynamic>();
                    bw_src.Add("bw_main", bw_local_local);
                    bw_src.Add("src_flag", src_flag);
                    bw_src.Add("tar_flag", tar_flag);
                    bw_src.Add("src_path", src_path);
                    bw_src.Add("tar_path", tar_path);
                    bw_src.Add("src_items", src_items);
                    bw_local_local.DoWork += new DoWorkEventHandler(bw_local_local_DoWork);
                    bw_local_local.ProgressChanged += bw_local_local_ProgressChanged;
                    bw_local_local.RunWorkerCompleted += bw_local_local_RunWorkerCompleted;
                    bw_local_local.WorkerReportsProgress = true;
                    bw_local_local.WorkerSupportsCancellation = true;
                    bw_local_local.RunWorkerAsync(argument: bw_src);
                }
                else if (src_flag == 2 && tar_flag == 1)
                {
                    bw_local_local2 = new BackgroundWorker();
                    Dictionary<string, dynamic> bw_src = new Dictionary<string, dynamic>();
                    bw_src.Add("bw_main", bw_local_local2);
                    bw_src.Add("src_flag", src_flag);
                    bw_src.Add("tar_flag", tar_flag);
                    bw_src.Add("src_path", src_path);
                    bw_src.Add("tar_path", tar_path);
                    bw_src.Add("src_items", src_items);
                    bw_local_local2.DoWork += new DoWorkEventHandler(bw_local_local_DoWork);
                    bw_local_local2.ProgressChanged += bw_local_local_ProgressChanged;
                    bw_local_local2.RunWorkerCompleted += bw_local_local_RunWorkerCompleted;
                    bw_local_local2.WorkerReportsProgress = true;
                    bw_local_local2.WorkerSupportsCancellation = true;
                    bw_local_local2.RunWorkerAsync(argument: bw_src);
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
        
        private void bw_local_local_DoWork(object sender, DoWorkEventArgs e)
        {
            var bw_dic = (Dictionary<string, dynamic>)e.Argument;
            int src_flag = (int)bw_dic["src_flag"];
            int tar_flag = (int)bw_dic["tar_flag"];
            string src_path = (string)bw_dic["src_path"];
            string tar_path = (string)bw_dic["tar_path"];
            string[] src_items = (string[])bw_dic["src_items"];
            var bw_main = (BackgroundWorker)bw_dic["bw_main"];

            //전체 개수부터 파악
            //string[] src_items = Drag_Items;
            long result_cnt = 0;            
            int cnt = 0;
            int case_flag = 0;
            string raw_path = "";
            int interlock = 0;
            List<int> rst = new List<int>();

            Dispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate
            {
                src_path = uiList_textbox[src_flag - 1].Text;
                tar_path = uiList_textbox[tar_flag - 1].Text;
                uiList_pgb[tar_flag - 1].Value = 0;
                uiList_pgbTxt[tar_flag - 1].Text = "0 %";
                uiList_pgbSpinner[tar_flag - 1].Visibility = Visibility.Visible;
                uiList_StopBtn[tar_flag - 1].Visibility = Visibility.Visible;
            }));

            result_cnt = Global_FunSet.ReturnLocalFileFolderCount(src_items, result_cnt);

            foreach (var src_item in src_items)
            {
                if (bw_main.CancellationPending)
                {
                    e.Cancel = true;                    
                    return;                    
                }
                (cnt, raw_path, case_flag) = LocalToLocalFileDir(src_item, 0, bw_main, src_flag, tar_flag, src_path, tar_path, cnt, result_cnt, case_flag, raw_path, 0);
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

        private void bw_local_local_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            // Running on a UI thread
            var args = (Tuple<int, int>)e.UserState;
            uiList_pgb[args.Item2 - 1].Value = e.ProgressPercentage;
            uiList_pgbTxt[args.Item2 - 1].Text = e.ProgressPercentage.ToString() + " %";
        }

        private void bw_local_local_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
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
                    uiList_pgbTxt[arg - 1].Text = "접근 불가";
                    uiList_listView[arg - 1].IsHitTestVisible = true;
                }
                else if (interlock == -2)
                {
                    uiList_pgbSpinner[arg - 1].Visibility = Visibility.Collapsed;
                    uiList_StopBtn[arg - 1].Visibility = Visibility.Collapsed;
                    uiList_listView[arg - 1].Items.Refresh();
                    uiList_pgbTxt[arg - 1].Text = "중단됨";
                    uiList_listView[arg - 1].IsHitTestVisible = true;

                    if (src_flag == 1 && tar_flag == 2 && LTL_stop_flag == 1) LTL_stop_flag = 0;
                    else if (src_flag == 2 && tar_flag == 1 & LTL_stop_flag2 == 1) LTL_stop_flag2 = 0;
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
                //
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

        #endregion

        #region Drap Drop Function
        
        //public string raw_path;
        //public string real_path;
        //public int level_flag;

        //Local To Local (case1)
        private (int, string, int) LocalToLocalFileDir(string src_item, int level, BackgroundWorker bw_main, int src_flag, int tar_flag, string src_path, string tar_path, int cnt, long result_cnt, int case_flag, string raw_path, int level_flag)
        {
            if (cnt == -1) return (-1, "", -1);

            if (src_flag == 1 && tar_flag == 2 && LTL_stop_flag == 1) return (-2, "", -1);
            else if (src_flag == 2 && tar_flag == 1 & LTL_stop_flag2 == 1) return (-2, "", -1);            

            string t_path = "";

            //경로 인터락
            DirectoryInfo di_temp = new DirectoryInfo(src_item);
            DirectoryInfo di_temp2 = new DirectoryInfo(tar_path);

            if ((!di_temp.Exists && !File.Exists(src_item)) || (!di_temp2.Exists && !File.Exists(tar_path)))
            {
                return (-1, "", -1);
            }

            try
            {
                if (cnt == 0)
                {
                    raw_path = src_path;
                    level_flag = 0;
                    case_flag = 0;
                }
                level_flag += level;

                FileAttributes chkAtt = File.GetAttributes(src_item);
                if ((chkAtt & FileAttributes.Directory) == FileAttributes.Directory)
                {

                    //전달받은 디렉토리 생성
                    DirectoryInfo di = null;

                    string real_path = src_item.Substring(raw_path.Length + 1, src_item.Length - raw_path.Length - 1);
                    string real_filename = src_item.Substring(src_item.LastIndexOf("\\") + 1, src_item.Length - src_item.LastIndexOf("\\") - 1);

                    if (real_path == real_filename)
                    {
                        Dispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate
                        {
                            t_path = tar_path + "\\" + real_filename;
                            di = new DirectoryInfo(t_path);
                        }));
                    }
                    else
                    {
                        Dispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate
                        {
                            t_path = tar_path + "\\" + real_path;
                            di = new DirectoryInfo(t_path);
                        }));
                    }

                    if (di.Exists == false)         //폴더 생성
                    {
                        di.Create();
                    }
                    else //폴더 이미 있음
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
                            Dispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate
                            {
                                //di = new DirectoryInfo(uiList_textbox[tar_flag - 1].Text + "\\" + s.Substring(s.LastIndexOf("\\") + 1, s.Length - s.LastIndexOf("\\") - 1));
                                //di = new DirectoryInfo(t_path);
                            }));
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
                    string[] tmpPath = Directory.GetDirectories(src_item);
                    foreach (string s in tmpPath)
                    {
                        (cnt, raw_path, case_flag) = LocalToLocalFileDir(s, 1, bw_main, src_flag, tar_flag, src_path, tar_path, cnt, result_cnt, case_flag, raw_path, level_flag); // 폴더를 만나면 하위로 계속 탐색을 진행
                        if (level_flag != 0) level_flag -= 1;
                    }

                    //하위 파일일 경우
                    string[] tmpFiles = Directory.GetFiles(src_item);

                    foreach (string s in tmpFiles)
                    {
                        (cnt, raw_path, case_flag) = LocalToLocalFileDir(s, 1, bw_main, src_flag, tar_flag, src_path, tar_path, cnt, result_cnt, case_flag, raw_path, level_flag); // 파일을 만나도 동일한 함수로 실행 가능함                    
                        if (level_flag != 0) level_flag -= 1;
                    }
                }
                else
                {
                    // 파일 일 경우                                   
                    //같은 이름을 가진 파일
                    string real_path = src_item.Substring(raw_path.Length + 1, src_item.Length - raw_path.Length - 1);
                    string real_filename = src_item.Substring(src_item.LastIndexOf("\\") + 1, src_item.Length - src_item.LastIndexOf("\\") - 1);

                    if (real_path == real_filename)
                    {

                        t_path = tar_path + "\\" + real_filename;

                    }
                    else
                    {
                        t_path = tar_path + "\\" + real_path;
                    }

                    //FileInfo fileInfo = new FileInfo(t_path + "\\" + file_name);
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
                            File.Copy(src_item, t_path, true);
                        }
                        else
                        {
                            //건너뛰기
                        }


                    }
                    else
                    {
                        File.Copy(src_item, t_path, true);
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
                }
                return (cnt, raw_path, case_flag);
            }
            catch (System.UnauthorizedAccessException)
            {
                MessageBox.Show("액세스 거부");
                return (-1, "", -1);
            }


        }

        #endregion

    }

}
