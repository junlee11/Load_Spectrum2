using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Load_Spectrum2.NewView
{
    /// <summary>
    /// Interaction logic for ViewDistruibute.xaml
    /// </summary>
    
    public partial class ViewDistruibute : Window
    {
        private NewViewModel.ViewModel_Distribute viewModel_distribute;
        public static Global_FunSet gfun;

        private ListView lv_eqp3;
        private ListView lv_eqp4;
        private ListView lv_eqp5;
        private ListView lv_all3;
        private ListView lv_all4;
        private ListView lv_all5;

        private Button disbtn3;
        private Button disbtn4;
        private Button disbtn5;

        public ViewDistruibute(string tab_str, string src_path, string drive, string src_eqp)
        {
            InitializeComponent();
            
            this.Loaded += new RoutedEventHandler(MainPage_Loaded);
            this.PreviewKeyDown += new KeyEventHandler(Key_Handler);
            gfun = new Global_FunSet();            

            if (tab_str == "LTE_default")
            {   
                this.tab_distribute.SelectedIndex = 0;                
                this.LTE_eqp_path.Text = "MI";
                this.LTE_combo_drive.SelectedIndex = 0;
                this.ETL_combo_drive.SelectedIndex = 0;
                this.ETE_src_combo_drive.SelectedIndex = 0;
                this.ETE_tar_combo_drive.SelectedIndex = 0;
            }
            else if (tab_str == "LTE")
            {
                this.tab_distribute.SelectedIndex = 0;
                this.LTE_Local_path.Text = src_path;
                this.LTE_eqp_path.Text = "MI";
                this.LTE_combo_drive.SelectedIndex = 0;
                this.ETL_combo_drive.SelectedIndex = 0;
                this.ETE_src_combo_drive.SelectedIndex = 0;
                this.ETE_tar_combo_drive.SelectedIndex = 0;
            }   
            else if (tab_str == "ETL")
            {
                this.tab_distribute.SelectedIndex = 1;
                this.ETL_eqp_path.Text = src_path;                
                
                if (drive.Contains("C")) this.ETL_combo_drive.SelectedIndex = 0;
                else if (drive.Contains("D")) this.ETL_combo_drive.SelectedIndex = 1;
                else if (drive.Contains("E")) this.ETL_combo_drive.SelectedIndex = 2;
                this.LTE_combo_drive.SelectedIndex = 0;
                this.ETE_src_combo_drive.SelectedIndex = 0;
                this.ETE_tar_combo_drive.SelectedIndex = 0;
            }
            else if (tab_str == "ETE")
            {
                this.tab_distribute.SelectedIndex = 2;
                this.ETE_src_path.Text = src_path;

                this.LTE_combo_drive.SelectedIndex = 0;
                this.ETL_combo_drive.SelectedIndex = 0;
                if (drive.Contains("C")) this.ETE_src_combo_drive.SelectedIndex = 0;
                else if (drive.Contains("D")) this.ETE_src_combo_drive.SelectedIndex = 1;
                else if (drive.Contains("E")) this.ETE_src_combo_drive.SelectedIndex = 2;                
                this.ETE_tar_combo_drive.SelectedIndex = 0;
                this.ETE_src_eqp.Text = src_eqp;
            }

            lv_eqp3 = this.List_EQPID_eqpSet3;
            lv_eqp4 = this.List_EQPID_eqpSet4;
            lv_eqp5 = this.List_EQPID_eqpSet5;
            lv_all3 = this.list_LTE;
            lv_all4 = this.list_ETL;
            lv_all5 = this.list_ETE;
            disbtn3 = this.LTE_distribute;
            disbtn4 = this.ETL_distribute;
            disbtn5 = this.ETE_distribute;

            this.LTE_distribute.IsEnabled = false;
            this.ETL_distribute.IsEnabled = false;
            this.ETE_distribute.IsEnabled = false;

            viewModel_distribute = new NewViewModel.ViewModel_Distribute();
            this.DataContext = viewModel_distribute;
            this.LTE_Spin.Visibility = Visibility.Collapsed;
            this.ETL_Spin.Visibility = Visibility.Collapsed;
            this.ETE_Spin.Visibility = Visibility.Collapsed;

            this.List_Line_eqpSet3.Items.SortDescriptions.Add(new SortDescription("strLine_Set3", ListSortDirection.Ascending));
            this.List_EQPTYPE_eqpSet3.Items.SortDescriptions.Add(new SortDescription("strEQPTYPE_Set3", ListSortDirection.Ascending));
            this.List_EQPMODEL_eqpSet3.Items.SortDescriptions.Add(new SortDescription("strEQPMODEL_Set3", ListSortDirection.Ascending));
            this.List_ZONE_eqpSet3.Items.SortDescriptions.Add(new SortDescription("strZONE_Set3", ListSortDirection.Ascending));
            this.List_EQPID_eqpSet3.Items.SortDescriptions.Add(new SortDescription("strEQPID_Set3", ListSortDirection.Ascending));

            this.List_Line_eqpSet4.Items.SortDescriptions.Add(new SortDescription("strLine_Set4", ListSortDirection.Ascending));
            this.List_EQPTYPE_eqpSet4.Items.SortDescriptions.Add(new SortDescription("strEQPTYPE_Set4", ListSortDirection.Ascending));
            this.List_EQPMODEL_eqpSet4.Items.SortDescriptions.Add(new SortDescription("strEQPMODEL_Set4", ListSortDirection.Ascending));
            this.List_ZONE_eqpSet4.Items.SortDescriptions.Add(new SortDescription("strZONE_Set4", ListSortDirection.Ascending));
            this.List_EQPID_eqpSet4.Items.SortDescriptions.Add(new SortDescription("strEQPID_Set4", ListSortDirection.Ascending));

            this.List_Line_eqpSet5.Items.SortDescriptions.Add(new SortDescription("strLine_Set5", ListSortDirection.Ascending));
            this.List_EQPTYPE_eqpSet5.Items.SortDescriptions.Add(new SortDescription("strEQPTYPE_Set5", ListSortDirection.Ascending));
            this.List_EQPMODEL_eqpSet5.Items.SortDescriptions.Add(new SortDescription("strEQPMODEL_Set5", ListSortDirection.Ascending));
            this.List_ZONE_eqpSet5.Items.SortDescriptions.Add(new SortDescription("strZONE_Set5", ListSortDirection.Ascending));
            this.List_EQPID_eqpSet5.Items.SortDescriptions.Add(new SortDescription("strEQPID_Set5", ListSortDirection.Ascending));

            this.list_LTE.Items.SortDescriptions.Add(new SortDescription("strchkEQPID_Set3", ListSortDirection.Ascending));
            this.list_ETL.Items.SortDescriptions.Add(new SortDescription("strchkEQPID_Set4", ListSortDirection.Ascending));
            this.list_ETE.Items.SortDescriptions.Add(new SortDescription("strchkEQPID_Set5", ListSortDirection.Ascending));

            //this.LTE_combo_drive.SelectedIndex = 0;
            //this.ETL_combo_drive.SelectedIndex = 0;
        }

        private void Key_Handler(object sender, KeyEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.LeftAlt) && (Keyboard.IsKeyDown(Key.T)))
            {
                e.Handled = true;
                try
                {
                    IInputElement focusedControl = FocusManager.GetFocusedElement(this);
                    var f_listview = focusedControl as ListViewItem;
                    ListView listView = ItemsControl.ItemsControlFromItemContainer(f_listview) as ListView;

                    if (listView != null)
                    {
                        if (listView.Name == "list_LTE") SubListEQPID(lv_all3, lv_eqp3, disbtn3);
                        else if (listView.Name == "list_ETL") SubListEQPID(lv_all4, lv_eqp4, disbtn4);
                        else if (listView.Name == "list_ETE") SubListEQPID(lv_all5, lv_eqp5, disbtn5);
                        else if (listView.Name == "List_EQPID_eqpSet3") AddListEQPID(lv_eqp3, lv_all3, disbtn3);
                        else if (listView.Name == "List_EQPID_eqpSet4") AddListEQPID(lv_eqp4, lv_all4, disbtn4);
                        else if (listView.Name == "List_EQPID_eqpSet5") AddListEQPID(lv_eqp5, lv_all5, disbtn5);
                    }
                    else
                    {                        
                        var f_listview2 = focusedControl as ListView;
                        string dismem = f_listview2.DisplayMemberPath;

                        if (f_listview2.Name == "list_LTE") SubListEQPID(lv_all3, lv_eqp3, disbtn3);
                        else if (f_listview2.Name == "list_ETL") SubListEQPID(lv_all4, lv_eqp4, disbtn4);
                        else if (f_listview2.Name == "list_ETE") SubListEQPID(lv_all5, lv_eqp5, disbtn5);
                        else if (f_listview2.Name == "List_EQPID_eqpSet3") AddListEQPID(lv_eqp3, lv_all3, disbtn3);
                        else if (f_listview2.Name == "List_EQPID_eqpSet4") AddListEQPID(lv_eqp4, lv_all4, disbtn4);
                        else if (f_listview2.Name == "List_EQPID_eqpSet5") AddListEQPID(lv_eqp5, lv_all5, disbtn5);
                    }
                    
                }
                catch
                {
                    
                }
                
            }
            else if (Keyboard.IsKeyDown(Key.LeftAlt) && (Keyboard.IsKeyDown(Key.W)))
            {
                e.Handled = true;
                if (this.tab_distribute.SelectedIndex + 1 == this.tab_distribute.Items.Count) this.tab_distribute.SelectedIndex = 0;
                else this.tab_distribute.SelectedIndex = this.tab_distribute.SelectedIndex + 1;
            }
        }


        void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void EPQFilter_View(int set_flag)
        {
            if (set_flag == 3)
            {
                var src = (List<NewModel.Model>)viewModel_distribute.EQPID_Filter(set_flag, this.List_Line_eqpSet3.SelectedItems, this.List_EQPTYPE_eqpSet3.SelectedItems,
                this.List_EQPMODEL_eqpSet3.SelectedItems, this.List_ZONE_eqpSet3.SelectedItems, this.tb_EQPID_eqp3.Text);
                this.List_EQPID_eqpSet3.ItemsSource = null;
                this.List_EQPID_eqpSet3.ItemsSource = src;
                this.List_EQPID_eqpSet3.Items.Refresh();
            }
            else if (set_flag == 4)
            {
                var src = (List<NewModel.Model>)viewModel_distribute.EQPID_Filter(set_flag, this.List_Line_eqpSet4.SelectedItems, this.List_EQPTYPE_eqpSet4.SelectedItems,
                this.List_EQPMODEL_eqpSet4.SelectedItems, this.List_ZONE_eqpSet4.SelectedItems, this.tb_EQPID_eqp4.Text);
                this.List_EQPID_eqpSet4.ItemsSource = null;
                this.List_EQPID_eqpSet4.ItemsSource = src;
                this.List_EQPID_eqpSet4.Items.Refresh();
            }
            else if (set_flag == 5)
            {                
                var src = (List<NewModel.Model>)viewModel_distribute.EQPID_Filter(set_flag, this.List_Line_eqpSet5.SelectedItems, this.List_EQPTYPE_eqpSet5.SelectedItems,
                this.List_EQPMODEL_eqpSet5.SelectedItems, this.List_ZONE_eqpSet5.SelectedItems, this.tb_EQPID_eqp5.Text);
                this.List_EQPID_eqpSet5.ItemsSource = null;
                this.List_EQPID_eqpSet5.ItemsSource = src;
                this.List_EQPID_eqpSet5.Items.Refresh();
            }

        }

        private void List_Line_eqpSet3_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            EPQFilter_View(3);
        }

        private void List_ZONE_eqpSet3_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            EPQFilter_View(3);
        }

        private void List_EQPTYPE_eqpSet3_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            EPQFilter_View(3);
        }

        private void List_EQPMODEL_eqpSet3_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            EPQFilter_View(3);
        }

        private void tb_EQPID_eqp3_TextChanged(object sender, TextChangedEventArgs e)
        {
            EPQFilter_View(3);
        }

        private void LineResetBtn_EQP1_Click(object sender, RoutedEventArgs e)
        {
            this.List_Line_eqpSet3.SelectedIndex = -1;
        }

        private void ZONEResetBtn_EQP1_Click(object sender, RoutedEventArgs e)
        {
            this.List_ZONE_eqpSet3.SelectedIndex = -1;
        }

        private void EQPIDResetBtn_EQP1_Click(object sender, RoutedEventArgs e)
        {
            this.List_Line_eqpSet3.SelectedIndex = -1;
            this.List_ZONE_eqpSet3.SelectedIndex = -1;
            this.List_EQPTYPE_eqpSet3.SelectedIndex = -1;            
            this.List_EQPID_eqpSet3.SelectedIndex = -1;
            this.List_EQPMODEL_eqpSet3.SelectedIndex = -1;
            this.tb_EQPID_eqp3.Text = "";
        }

        private void EQPTYPEResetBtn_EQP1_Click(object sender, RoutedEventArgs e)
        {
            this.List_EQPTYPE_eqpSet3.SelectedIndex = -1;
        }

        private void EQPMODELResetBtn_EQP1_Click(object sender, RoutedEventArgs e)
        {
            this.List_EQPTYPE_eqpSet3.SelectedIndex = -1;
        }

        private void select_LTE_localfile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();            

            if (openFileDialog.ShowDialog() == true)
            {                
                this.LTE_Local_path.Text = openFileDialog.FileName;
            }            
        }

        private void select_LTE_localpath_Click(object sender, RoutedEventArgs e)
        {
            
            var dialog = new Ookii.Dialogs.Wpf.VistaFolderBrowserDialog();
            if (dialog.ShowDialog(this).GetValueOrDefault())
            {
                this.LTE_Local_path.Text = dialog.SelectedPath;
            }

        }

        private void List_EQPID_eqpSet3_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            AddListEQPID(lv_eqp3, lv_all3, disbtn3);            
        }
        private void List_EQPID_eqpSet4_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            AddListEQPID(lv_eqp4, lv_all4, disbtn4);
        }
        private void List_EQPID_eqpSet5_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            AddListEQPID(lv_eqp5, lv_all5, disbtn5);
        }

        private void AddListEQPID(ListView lv_src, ListView lv_tar, Button dis_btn)
        {            
            int idx = lv_src.SelectedIndex;
            var items = lv_src.SelectedItems;

            int set_flag = 0;

            if (lv_src.Name == "List_EQPID_eqpSet3") set_flag = 3;
            else if(lv_src.Name == "List_EQPID_eqpSet4") set_flag = 4;
            else if (lv_src.Name == "List_EQPID_eqpSet5") set_flag = 5;
            List<object> src = viewModel_distribute.LTE_AddListEQPID(items, lv_src, lv_tar, set_flag);
            var src_eqpid = (List<NewModel.Model>)src[0];
            var src_all = (List<NewModel.Model>)src[1];

            lv_src.ItemsSource = null;
            lv_src.ItemsSource = src_eqpid;
            lv_src.Items.Refresh();

            lv_tar.ItemsSource = null;
            lv_tar.ItemsSource = src_all;
            lv_tar.Items.Refresh();

            lv_src.Focus();            
            try
            {                
                lv_src.SelectedIndex = idx;
            }
            catch { }

            dis_btn.IsEnabled = false;
        }

        private void SubListEQPID(ListView lv_src, ListView lv_tar, Button dis_btn)
        {
            int idx = lv_src.SelectedIndex;
            IList items = null;
            if (dis_btn.Name == "btn_RNR_Reset" || dis_btn.Name == "btn_LTE_Reset" || dis_btn.Name == "btn_ETE_Reset")
            {
                items = lv_src.Items;
            }
            else
            {
                items = lv_src.SelectedItems;
            }
            
            int set_flag = 0;

            if (lv_src.Name == "list_LTE") set_flag = 3;
            else if (lv_src.Name == "list_ETL") set_flag = 4;
            else if (lv_src.Name == "list_ETE") set_flag = 5;
            List<object> src = viewModel_distribute.LTE_SubListEQPID(items, lv_src, lv_tar, set_flag);
            var src_eqpid = (List<NewModel.Model>)src[0];
            var src_all = (List<NewModel.Model>)src[1];

            lv_tar.ItemsSource = null;
            lv_tar.ItemsSource = src_eqpid;
            lv_tar.Items.Refresh();

            lv_src.ItemsSource = null;
            lv_src.ItemsSource = src_all;
            lv_src.Items.Refresh();

            lv_src.Focus();
            try
            {
                lv_src.SelectedIndex = idx;
            }
            catch { }

        }

        private void list_LTE_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            SubListEQPID(lv_all3, lv_eqp3, disbtn3);
        }

        private void list_ETL_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            SubListEQPID(lv_all4, lv_eqp4, disbtn4);
        }

        private void list_ETE_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            SubListEQPID(lv_all5, lv_eqp5, disbtn5);
        }

        //Drag And Drop
        #region Drag and Drop
        #region Data operations
        void SaveData(IList source, IDataObject target)
        {
            string Buffer = "";
            
            string result = "";

            //cast_item1 = (EQP_path_all1)item;
            foreach (var Item in source)
            {           
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
            string Buffer = (string)source.GetData(typeof(string));
            string[] Separators = new string[] { "\r\n" };

            //Items가 최종 데이터
            //Drag_Items = Buffer.Split(Separators,
            //StringSplitOptions.RemoveEmptyEntries);            

            if (source_set_flag == 1 && target_set_flag == 2) SubListEQPID(lv_all3, lv_eqp3, disbtn3);
            else if (source_set_flag == 2 && target_set_flag == 1) AddListEQPID(lv_eqp3, lv_all3, disbtn3);
            else if (source_set_flag == 3 && target_set_flag == 4) SubListEQPID(lv_all4, lv_eqp4, disbtn4);
            else if (source_set_flag == 4 && target_set_flag == 3) AddListEQPID(lv_eqp4, lv_all4, disbtn4);
            else if (source_set_flag == 6 && target_set_flag == 5) AddListEQPID(lv_eqp5, lv_all5, disbtn5);
            else if (source_set_flag == 5 && target_set_flag == 6) SubListEQPID(lv_all5, lv_eqp5, disbtn5);

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

            if (listView.Name == "list_LTE") target_set_flag = 1;
            else if (listView.Name == "List_EQPID_eqpSet3") target_set_flag = 2;
            else if (listView.Name == "list_ETL") target_set_flag = 3;
            else if (listView.Name == "List_EQPID_eqpSet4") target_set_flag = 4;
            else if (listView.Name == "list_ETE") target_set_flag = 5;
            else if (listView.Name == "List_EQPID_eqpSet5") target_set_flag = 6;

            e.Handled = true;
            MakeDropEffect(e);
            if (e.Effects == DragDropEffects.Copy || e.Effects == DragDropEffects.Move)
                LoadData(e.Data);
        }
        #endregion

        #region Drag start operation
        void StartDrag(ListView listView)
        {
            IList Selection = listView.SelectedItems;
            Debug.WriteLine(Selection);
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
            //Log("PreviewMouseDown()");
            ListView listView = (ListView)sender;
            listView.Tag = null;

            if (listView.Name == "list_LTE") source_set_flag = 1;
            else if (listView.Name == "List_EQPID_eqpSet3") source_set_flag = 2;
            else if (listView.Name == "list_ETL") source_set_flag = 3;
            else if (listView.Name == "List_EQPID_eqpSet4") source_set_flag = 4;
            else if (listView.Name == "list_ETE") source_set_flag = 5;
            else if (listView.Name == "List_EQPID_eqpSet5") source_set_flag = 6;

            PP = e.GetPosition(listView);

            ListViewItem Item = (ListViewItem)VisualTree.GetParent(e.OriginalSource, typeof(ListViewItem));

            //ListViewItem Item = uiList_listView[source_set_flag - 1].SelectedItems;            
            if (Item == null)
                return;
            
            if (Item.IsSelected && listView.CaptureMouse())
            {
                //Log("PreviewMouseDown() - Selected item mouse down.");
                e.Handled = true;
                listView.Tag = Item;
            }
        }

        void listView_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            //Log("PreviewMouseUp()");
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
        #endregion

        private void Eqp3_TrbackBtn_Click(object sender, RoutedEventArgs e)
        {
            if (this.List_EQPID_eqpSet3.SelectedItems.Count == 0) return;

            AddListEQPID(lv_eqp3, lv_all3, disbtn3);

        }

        private void Eqp3_TrBtn_Click(object sender, RoutedEventArgs e)
        {
            if (this.list_LTE.SelectedItems.Count == 0) return;

            SubListEQPID(lv_all3, lv_eqp3, disbtn3);

        }

        private void Eqp4_TrbackBtn_Click(object sender, RoutedEventArgs e)
        {
            if (this.List_EQPID_eqpSet4.SelectedItems.Count == 0) return;

            AddListEQPID(lv_eqp4, lv_all4, disbtn4);
        }

        private void Eqp5_TrbackBtn_Click(object sender, RoutedEventArgs e)
        {
            if (this.List_EQPID_eqpSet5.SelectedItems.Count == 0) return;

            AddListEQPID(lv_eqp5, lv_all5, disbtn5);
        }

        private void Eqp4_TrBtn_Click(object sender, RoutedEventArgs e)
        {
            if (this.list_ETL.SelectedItems.Count == 0) return;

            SubListEQPID(lv_all4, lv_eqp4, disbtn4);
        }

        private void Eqp5_TrBtn_Click(object sender, RoutedEventArgs e)
        {
            if (this.list_ETE.SelectedItems.Count == 0) return;

            SubListEQPID(lv_all5, lv_eqp5, disbtn5);
        }

        #region bw_Refresh
        BackgroundWorker bw_LTE_Refresh;
        private void LTE_Refresh_Click(object sender, RoutedEventArgs e)
        {
            //parameter : 리스트내 모든 아이템
            //result : 상태 최신화된 아이템
            //인터락            
            if (this.list_LTE.Items.Count == 0)
            {
                MessageBox.Show("설비를 추가해야합니다.");
                return;
            }
            int ftp_chk_flag = 0;

            if (this.LTE_eqp_path.Text == "")
            {
                MessageBox.Show("EQP 경로가 입력되지 않으면 FTP 연결여부만 확인합니다.");
                ftp_chk_flag = 1;
                //return;
            }
            string drive = ((NewModel.Model)this.LTE_combo_drive.SelectedItem).strdrive;
            
            string id = "";
            string pw = "";
            if (drive.Contains("C"))
            {
                id = "LS_C";
                pw = "LS_C";
            }
            else if (drive.Contains("D"))
            {
                id = "LS_D";
                pw = "LS_D";
            }
            else if (drive.Contains("E"))
            {
                id = "LS_E";
                pw = "LS_E";
            }
                        
            this.LTE_Spin.Visibility = Visibility.Visible;
            this.LTE_pgb.Value = 0;
            this.LTE_pgb_txt.Text = "0%";
            this.LTE_Refresh.IsEnabled = false;
            

            bw_LTE_Refresh = new BackgroundWorker();
            Dictionary<string, dynamic> bw_src = new Dictionary<string, dynamic>();
            bw_src.Add("items", this.list_LTE.Items);
            bw_src.Add("txt_path", this.LTE_eqp_path.Text);
            bw_src.Add("drive", ((NewModel.Model)this.LTE_combo_drive.SelectedItem).strdrive);
            bw_src.Add("id", id);
            bw_src.Add("pw", pw);
            bw_src.Add("sum", this.list_LTE.Items.Count);
            bw_src.Add("ftp_chk_flag", ftp_chk_flag);
            bw_src.Add("set_flag", 3);
            bw_src.Add("bw_main", bw_LTE_Refresh);
            bw_LTE_Refresh.DoWork += new DoWorkEventHandler(bw_LTE_Refresh_DoWork);
            bw_LTE_Refresh.ProgressChanged += bw_LTE_Refresh_ProgressChanged;
            bw_LTE_Refresh.RunWorkerCompleted += bw_LTE_Refresh_RunWorkerCompleted;
            bw_LTE_Refresh.WorkerReportsProgress = true;
            bw_LTE_Refresh.WorkerSupportsCancellation = true;
            bw_LTE_Refresh.RunWorkerAsync(argument: bw_src);

            id = "";
            pw = "";
            bw_src = null;
        }

        private void bw_LTE_Refresh_DoWork(object sender, DoWorkEventArgs e)
        {
            var bw_dic = (Dictionary<string, dynamic>)e.Argument;
            IList items = (IList)bw_dic["items"];
            IList src_items = null;
            string txt_path = (string)bw_dic["txt_path"];
            string drive = (string)bw_dic["drive"];
            string id = (string)bw_dic["id"];
            string pw = (string)bw_dic["pw"];
            int sum = (int)bw_dic["sum"];
            int ftp_chk_flag = (int)bw_dic["ftp_chk_flag"];
            int set_flag = (int)bw_dic["set_flag"];
            BackgroundWorker bw_main = (BackgroundWorker)bw_dic["bw_main"];

            if (txt_path != "")
            {
                if (txt_path.Substring(txt_path.Length - 1, 1) == "/") txt_path = txt_path.Substring(0, txt_path.Length - 1);
            }            

            viewModel_distribute.pgb_val_ini(items, set_flag);            

            int cnt = 1;
            foreach (var item in items)
            {
                var cast_item = (NewModel.Model)item;
                src_items = viewModel_distribute.LTE_Refresh(cast_item, txt_path, id, pw, ftp_chk_flag, set_flag);
                if (src_items == null) return;
                
                double ratio = ((double)cnt / (double)sum) * 100.0;
                //bw_LTE_Refresh.ReportProgress((int)ratio);
                bw_main.ReportProgress((int)ratio, (int)set_flag);
                cnt++;
            }
            List<object> list_result = new List<object>();
            list_result.Add(src_items);
            list_result.Add(ftp_chk_flag);
            list_result.Add(set_flag);
            e.Result = list_result;
        }

        private void bw_LTE_Refresh_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            // Running on a UI thread            
            var args = (int)e.UserState;            
            if (args == 3)
            {
                this.LTE_pgb.Value = e.ProgressPercentage;
                this.LTE_pgb_txt.Text = e.ProgressPercentage.ToString() + " %";
            }
            else if (args == 4)
            {
                this.ETL_pgb.Value = e.ProgressPercentage;
                this.ETL_pgb_txt.Text = e.ProgressPercentage.ToString() + " %";
            }
            else if (args == 5)
            {
                this.ETE_pgb.Value = e.ProgressPercentage;
                this.ETE_pgb_txt.Text = e.ProgressPercentage.ToString() + " %";
            }

        }

        private void bw_LTE_Refresh_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            var list_rst = e.Result as List<object>;
            
            if (e.Result == null)
            {
                this.ETL_Spin.Visibility = Visibility.Collapsed;
                this.ETL_pgb.Value = 0;
                this.ETL_Refresh.IsEnabled = true;
                return;
            }
            var arg = list_rst[0] as IList;
            int ftp_chk_flag = Int32.Parse(list_rst[1].ToString());
            int set_flag = Int32.Parse(list_rst[2].ToString());            
            
            if (set_flag == 3)
            {
                this.LTE_Spin.Visibility = Visibility.Collapsed;
                this.LTE_pgb.Value = 100;
                this.LTE_pgb_txt.Text = "100%";

                this.list_LTE.ItemsSource = arg;
                this.list_LTE.Items.Refresh();
                this.LTE_Refresh.IsEnabled = true;

                if (ftp_chk_flag == 0) this.LTE_distribute.IsEnabled = true;
                else this.LTE_distribute.IsEnabled = false;
            }
            else if (set_flag == 4)
            {
                this.ETL_Spin.Visibility = Visibility.Collapsed;
                this.ETL_pgb.Value = 100;
                this.ETL_pgb_txt.Text = "100%";

                this.list_ETL.ItemsSource = arg;
                this.list_ETL.Items.Refresh();
                this.ETL_Refresh.IsEnabled = true;

                if (ftp_chk_flag == 0) this.ETL_distribute.IsEnabled = true;
                else this.ETL_distribute.IsEnabled = false;
            }
            else if (set_flag == 5)
            {
                this.ETE_Spin.Visibility = Visibility.Collapsed;
                this.ETE_pgb.Value = 100;
                this.ETE_pgb_txt.Text = "100%";

                this.list_ETE.ItemsSource = arg;
                this.list_ETE.Items.Refresh();
                this.ETE_Refresh.IsEnabled = true;

                if (ftp_chk_flag == 0) this.ETE_distribute.IsEnabled = true;
                else this.ETE_distribute.IsEnabled = false;
            }
        }
        #endregion

        BackgroundWorker bw_ETL_Refresh;
        private void ETL_Refresh_Click(object sender, RoutedEventArgs e)
        {
            if (this.list_ETL.Items.Count == 0)
            {
                MessageBox.Show("설비를 추가해야합니다.");
                return;
            }
            int ftp_chk_flag = 0;

            if (this.ETL_eqp_path.Text == "")
            {
                MessageBox.Show("EQP 경로가 입력되지 않으면 FTP 연결여부만 확인합니다.");
                ftp_chk_flag = 1;
                //return;
            }
            string drive = ((NewModel.Model)this.ETL_combo_drive.SelectedItem).strdrive;

            string id = "";
            string pw = "";
            if (drive.Contains("C"))
            {
                id = "LS_C";
                pw = "LS_C";
            }
            else if (drive.Contains("D"))
            {
                id = "LS_D";
                pw = "LS_D";
            }
            else if (drive.Contains("E"))
            {
                id = "LS_E";
                pw = "LS_E";
            }

            this.ETL_Spin.Visibility = Visibility.Visible;
            this.ETL_pgb.Value = 0;
            this.ETL_pgb_txt.Text = "0%";
            this.ETL_Refresh.IsEnabled = false;

            //날짜 무시하는 방법 구상
            //txt_path 형식에 따라
            string first_path = this.ETL_eqp_path.Text;
            this.List_RNR_Wafer.Items.Clear();            
            this.List_RNR_INEG.Items.Clear();
            this.List_RNR_Item_Layer.ItemsSource = null;  
            Global_FunSet.ETL_Sim_pathDic.Clear();
            Global_FunSet.ETL_Sim_LocalPath.Clear();

            bw_ETL_Refresh = new BackgroundWorker();
            Dictionary<string, dynamic> bw_src = new Dictionary<string, dynamic>();
            bw_src.Add("items", this.list_ETL.Items);
            bw_src.Add("txt_path", this.ETL_eqp_path.Text);
            bw_src.Add("drive", ((NewModel.Model)this.ETL_combo_drive.SelectedItem).strdrive);
            bw_src.Add("id", id);
            bw_src.Add("pw", pw);
            bw_src.Add("sum", this.list_ETL.Items.Count);
            bw_src.Add("ftp_chk_flag", ftp_chk_flag);
            bw_src.Add("set_flag", 4);
            bw_src.Add("bw_main", bw_ETL_Refresh);
            bw_ETL_Refresh.DoWork += new DoWorkEventHandler(bw_LTE_Refresh_DoWork);
            bw_ETL_Refresh.ProgressChanged += bw_LTE_Refresh_ProgressChanged;
            bw_ETL_Refresh.RunWorkerCompleted += bw_LTE_Refresh_RunWorkerCompleted;
            bw_ETL_Refresh.WorkerReportsProgress = true;
            bw_ETL_Refresh.WorkerSupportsCancellation = true;
            bw_ETL_Refresh.RunWorkerAsync(argument: bw_src);
        }


        BackgroundWorker bw_ETE_Refresh;
        private void ETE_Refresh_Click(object sender, RoutedEventArgs e)
        {
            if (this.list_ETE.Items.Count == 0)
            {
                MessageBox.Show("설비를 추가해야합니다.");
                return;
            }
            int ftp_chk_flag = 0;

            if (this.ETE_src_path.Text == "")
            {
                MessageBox.Show("EQP 경로가 입력되지 않으면 FTP 연결여부만 확인합니다.");
                ftp_chk_flag = 1;
                //return;
            }
            string drive = ((NewModel.Model)this.ETE_tar_combo_drive.SelectedItem).strdrive;

            string id = "";
            string pw = "";
            if (drive.Contains("C"))
            {
                id = "LS_C";
                pw = "LS_C";
            }
            else if (drive.Contains("D"))
            {
                id = "LS_D";
                pw = "LS_D";
            }
            else if (drive.Contains("E"))
            {
                id = "LS_E";
                pw = "LS_E";
            }

            this.ETE_Spin.Visibility = Visibility.Visible;
            this.ETE_pgb.Value = 0;
            this.ETE_pgb_txt.Text = "0%";
            this.ETE_Refresh.IsEnabled = false;

            bw_ETE_Refresh = new BackgroundWorker();
            Dictionary<string, dynamic> bw_src = new Dictionary<string, dynamic>();
            bw_src.Add("items", this.list_ETE.Items);
            bw_src.Add("txt_path", this.ETE_tar_path.Text);
            bw_src.Add("drive", ((NewModel.Model)this.ETE_tar_combo_drive.SelectedItem).strdrive);
            bw_src.Add("id", id);
            bw_src.Add("pw", pw);
            bw_src.Add("sum", this.list_ETE.Items.Count);
            bw_src.Add("ftp_chk_flag", ftp_chk_flag);
            bw_src.Add("set_flag", 5);
            bw_src.Add("bw_main", bw_ETE_Refresh);
            bw_ETE_Refresh.DoWork += new DoWorkEventHandler(bw_LTE_Refresh_DoWork);
            bw_ETE_Refresh.ProgressChanged += bw_LTE_Refresh_ProgressChanged;
            bw_ETE_Refresh.RunWorkerCompleted += bw_LTE_Refresh_RunWorkerCompleted;
            bw_ETE_Refresh.WorkerReportsProgress = true;
            bw_ETE_Refresh.WorkerSupportsCancellation = true;
            bw_ETE_Refresh.RunWorkerAsync(argument: bw_src);

            id = "";
            pw = "";
            bw_src = null;
        }


        private void LTE_eqp_path_TextChanged(object sender, TextChangedEventArgs e)
        {            
            if (this.LTE_eqp_path.Text.Contains("\\") || this.LTE_eqp_path.Text.Contains(":") || this.LTE_eqp_path.Text.Contains("*") || this.LTE_eqp_path.Text.Contains("?") ||
                this.LTE_eqp_path.Text.Contains("\"") || this.LTE_eqp_path.Text.Contains("<") || this.LTE_eqp_path.Text.Contains(">") || this.LTE_eqp_path.Text.Contains("|"))
            {
                this.LTE_eqp_path.Text = this.LTE_eqp_path.Text.Substring(0, this.LTE_eqp_path.Text.Length - 1);
                this.LTE_eqp_path.Select(this.LTE_eqp_path.Text.Length, 0);
            }

            try
            {
                if (this.LTE_eqp_path.Text.Substring(this.LTE_eqp_path.Text.Length - 1, 1) == "/" && this.LTE_eqp_path.Text.Substring(this.LTE_eqp_path.Text.Length - 2, 1) == "/")
                {
                    this.LTE_eqp_path.Text = this.LTE_eqp_path.Text.Substring(0, this.LTE_eqp_path.Text.Length - 1);
                    this.LTE_eqp_path.Select(this.LTE_eqp_path.Text.Length, 0);
                }
            }
            catch
            {

            }   

            if (this.LTE_eqp_path.Text.StartsWith("/") || this.LTE_eqp_path.Text.StartsWith("\\")) this.LTE_eqp_path.Text = "";
        }

        //bw 업로드 
        BackgroundWorker bw_LTE_Upload;
        private void LTE_distribute_Click(object sender, RoutedEventArgs e)
        {
            //인터락
            if (this.LTE_Local_path.Text == "")
            {
                MessageBox.Show("로컬 파일(폴더)를 선택하세요");
                return;
            }

            if (this.list_LTE.Items.Count == 0)
            {
                MessageBox.Show("설비를 추가해야합니다.");
                return;
            }
                
            if (this.LTE_eqp_path.Text == "")
            {
                MessageBox.Show("EQP 경로를 입력하세요");
                return;
            }

            //여기
            string drive = ((NewModel.Model)this.LTE_combo_drive.SelectedItem).strdrive;
            string id = "";
            string pw = "";
            if (drive.Contains("C"))
            {
                id = "LS_C";
                pw = "LS_C";
            }
            else if (drive.Contains("D"))
            {
                id = "LS_D";
                pw = "LS_D";
            }
            else if (drive.Contains("E"))
            {
                id = "LS_E";
                pw = "LS_E";
            }

            //status가 down대기이면 다운로드
            

            string[] src_items = new string[1];
            src_items[0] = this.LTE_Local_path.Text;

            ObservableCollection<object> items = new ObservableCollection<object>();            
            
            foreach (var item in this.list_LTE.Items)
            {
                var cast_item = (NewModel.Model)item;
                if (!cast_item.strStatus_Set3.Contains("접속불가"))
                {
                    items.Add(item);
                } 
            }      
            
            if (items.Count == 0)
            {
                MessageBox.Show("접속 가능한 설비가 없습니다.");
                return;
            }

            this.LTE_Spin.Visibility = Visibility.Visible;
            this.LTE_pgb.Value = 0;
            this.LTE_pgb_txt.Text = "0%";
            this.LTE_Refresh.IsEnabled = false;
            this.LTE_distribute.IsEnabled = false;
            long result_cnt = 0;

            result_cnt = Global_FunSet.ReturnLocalFileFolderCount(src_items, result_cnt);            
            bw_LTE_Upload = new BackgroundWorker();
            Dictionary<string, dynamic> bw_src = new Dictionary<string, dynamic>();
            bw_src.Add("items", items);
            bw_src.Add("src_path", this.LTE_Local_path.Text);
            bw_src.Add("tar_path", this.LTE_eqp_path.Text);
            bw_src.Add("drive", ((NewModel.Model)this.LTE_combo_drive.SelectedItem).strdrive);
            bw_src.Add("id", id);
            bw_src.Add("pw", pw);
            bw_src.Add("sum", items.Count);
            bw_src.Add("result_cnt", result_cnt);
            bw_src.Add("ete_flag", 0);
            bw_LTE_Upload.DoWork += new DoWorkEventHandler(bw_LTE_Upload_DoWork);
            bw_LTE_Upload.ProgressChanged += bw_LTE_Upload_ProgressChanged;
            bw_LTE_Upload.RunWorkerCompleted += bw_LTE_Upload_RunWorkerCompleted;
            bw_LTE_Upload.WorkerReportsProgress = true;
            bw_LTE_Upload.WorkerSupportsCancellation = true;
            bw_LTE_Upload.RunWorkerAsync(argument: bw_src);
        }

        private void bw_LTE_Upload_DoWork(object sender, DoWorkEventArgs e)
        {
            IList src_items = null;
            var bw_dic = (Dictionary<string, dynamic>)e.Argument;
            IList items = (IList)bw_dic["items"];
            string src_path = (string)bw_dic["src_path"];
            string tar_path = (string)bw_dic["tar_path"];
            string drive = (string)bw_dic["drive"];
            string id = (string)bw_dic["id"];
            string pw = (string)bw_dic["pw"];
            int sum = (int)bw_dic["sum"];
            long result_cnt = (long)bw_dic["result_cnt"];
            int ete_flag = (int)bw_dic["ete_flag"];

            if (tar_path.Substring(tar_path.Length - 1, 1) == "/") tar_path = tar_path.Substring(0, tar_path.Length - 1);

            int cnt_all = 0;
            
            int fail_flag = 0;
            string raw_path = "";

            viewModel_distribute.pgb_val_ini(items, 3);

            foreach (var item in items)
            {
                int cnt = 0;
                var cast_item = (NewModel.Model)item;
                (src_items, cnt, fail_flag, raw_path, tar_path, ete_flag) = viewModel_distribute.LTE_Upload(cast_item, src_path, tar_path, id, pw, cnt, result_cnt, raw_path, fail_flag, ete_flag);

                cnt_all++;
                double ratio = ((double)cnt_all / (double)sum) * 100.0;
                bw_LTE_Upload.ReportProgress((int)ratio);                
            }
            e.Result = src_items;
        }

        private void bw_LTE_Upload_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            // Running on a UI thread            
            this.LTE_pgb.Value = e.ProgressPercentage;
            this.LTE_pgb_txt.Text = e.ProgressPercentage.ToString() + " %";
        }

        private void bw_LTE_Upload_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            var arg = (IList)e.Result;
            this.LTE_Spin.Visibility = Visibility.Collapsed;
            this.LTE_pgb.Value = 100;
            this.LTE_pgb_txt.Text = "100%";

            this.list_LTE.ItemsSource = arg;
            this.list_LTE.Items.Refresh();
            this.LTE_Refresh.IsEnabled = true;
            this.LTE_distribute.IsEnabled = true;
        }


        //다운로드 ETL
        BackgroundWorker bw_ETL_Download;
        private void ETL_distribute_Click(object sender, RoutedEventArgs e)
        {
            //인터락
            if (this.ETL_Local_path.Text == "")
            {
                MessageBox.Show("로컬 파일(폴더)를 선택하세요");
                return;
            }

            if (this.list_ETL.Items.Count == 0)
            {
                MessageBox.Show("설비를 추가해야합니다.");
                return;
            }

            if (this.ETL_eqp_path.Text == "")
            {
                MessageBox.Show("EQP 경로를 입력하세요");
                return;
            }

            //여기
            string drive = ((NewModel.Model)this.ETL_combo_drive.SelectedItem).strdrive;
            string id = "";
            string pw = "";
            if (drive.Contains("C"))
            {
                id = "LS_C";
                pw = "LS_C";
            }
            else if (drive.Contains("D"))
            {
                id = "LS_D";
                pw = "LS_D";
            }
            else if (drive.Contains("E"))
            {
                id = "LS_E";
                pw = "LS_E";
            }

            //status가 down대기이면 다운로드
            

            //string[] src_items = new string[1];
            ////src_items[0] = String.Format("ftp://{0}:21/{1}", gfun.source_EQP_dict[eqp], tar_path);
            //src_items[0] = this.ETL_eqp_path.Text;

            ObservableCollection<object> items = new ObservableCollection<object>();

            foreach (var item in this.list_ETL.Items)
            {
                var cast_item = (NewModel.Model)item;
                if (!cast_item.strStatus_Set4.Contains("접속불가") && !cast_item.strStatus_Set4.Contains("예외") && !cast_item.strStatus_Set4.Contains("없음"))
                {
                    items.Add(item);
                }
            }

            if (items.Count == 0)
            {
                MessageBox.Show("접속 가능한 설비가 없습니다.");
                return;
            }

            this.List_RNR_Wafer.Items.Clear();
            this.List_RNR_Item_Layer.ItemsSource = null;
            this.List_RNR_INEG.Items.Clear();
            
            this.ETL_Spin.Visibility = Visibility.Visible;
            this.ETL_pgb.Value = 0;
            this.ETL_pgb_txt.Text = "0%";
            this.ETL_Refresh.IsEnabled = false;
            this.ETL_distribute.IsEnabled = false;
            long result_cnt = 0;
            bool imgchk = (bool)this.ETL_imgChk.IsChecked;

            //result_cnt = gfun.ReturnEQPFileFolderCount(src_items, id, pw, result_cnt);
            bw_ETL_Download = new BackgroundWorker();
            Dictionary<string, dynamic> bw_src = new Dictionary<string, dynamic>();
            bw_src.Add("items", items);
            bw_src.Add("src_path", this.ETL_eqp_path.Text);
            bw_src.Add("tar_path", this.ETL_Local_path.Text);
            bw_src.Add("drive", ((NewModel.Model)this.ETL_combo_drive.SelectedItem).strdrive);
            bw_src.Add("id", id);
            bw_src.Add("pw", pw);
            bw_src.Add("sum", items.Count);
            bw_src.Add("result_cnt", result_cnt);
            bw_src.Add("imgchk", imgchk);
            bw_src.Add("bw_main", bw_ETL_Download);
            bw_src.Add("eqp", "eqp");
            bw_src.Add("ete_flag", 0);
            bw_ETL_Download.DoWork += new DoWorkEventHandler(bw_ETL_Download_DoWork);
            bw_ETL_Download.ProgressChanged += bw_ETL_Download_ProgressChanged;
            bw_ETL_Download.RunWorkerCompleted += bw_ETL_Download_RunWorkerCompleted;
            bw_ETL_Download.WorkerReportsProgress = true;
            bw_ETL_Download.WorkerSupportsCancellation = true;
            bw_ETL_Download.RunWorkerAsync(argument: bw_src);
            this.ETL_distribute.IsEnabled = false;
        }

        BackgroundWorker bw_ETE_Download;
        private void ETE_distribute_Click(object sender, RoutedEventArgs e)
        {
            //인터락
            if (this.ETE_src_path.Text == "")
            {
                MessageBox.Show("EQP 파일(폴더)를 선택하세요");
                return;
            }

            if (this.list_ETE.Items.Count == 0)
            {
                MessageBox.Show("설비를 추가해야합니다.");
                return;
            }

            if (this.ETE_tar_path.Text == "")
            {
                MessageBox.Show("EQP 경로를 입력하세요");
                return;
            }

            //여기
            string drive = ((NewModel.Model)this.ETE_src_combo_drive.SelectedItem).strdrive;
            string id = "";
            string pw = "";
            if (drive.Contains("C"))
            {
                id = "LS_C";
                pw = "LS_C";
            }
            else if (drive.Contains("D"))
            {
                id = "LS_D";
                pw = "LS_D";
            }
            else if (drive.Contains("E"))
            {
                id = "LS_E";
                pw = "LS_E";
            }

            //status가 down대기이면 다운로드
                       

            //string[] src_items = new string[1];
            ////src_items[0] = String.Format("ftp://{0}:21/{1}", gfun.source_EQP_dict[eqp], tar_path);
            //src_items[0] = this.ETL_eqp_path.Text;

            ObservableCollection<object> items = new ObservableCollection<object>();

            foreach (var item in this.list_ETE.Items)
            {
                var cast_item = (NewModel.Model)item;
                if (!cast_item.strStatus_Set5.Contains("접속불가") && !cast_item.strStatus_Set5.Contains("예외"))
                {
                    items.Add(item);
                }
            }

            if (items.Count == 0)
            {
                MessageBox.Show("접속 가능한 설비가 없습니다.");
                return;
            }

            this.ETE_Spin.Visibility = Visibility.Visible;
            this.ETE_pgb.Value = 0;
            this.ETE_pgb_txt.Text = "0%";
            this.ETE_Refresh.IsEnabled = false;
            this.ETE_distribute.IsEnabled = false;
            long result_cnt = 0;

            //result_cnt = gfun.ReturnEQPFileFolderCount(src_items, id, pw, result_cnt);
            bw_ETE_Download = new BackgroundWorker();
            Dictionary<string, dynamic> bw_src = new Dictionary<string, dynamic>();
            bw_src.Add("items", items);
            bw_src.Add("src_path", this.ETE_src_path.Text);
            bw_src.Add("tar_path", this.ETE_tar_path.Text);
            bw_src.Add("drive", ((NewModel.Model)this.ETE_src_combo_drive.SelectedItem).strdrive);
            bw_src.Add("id", id);
            bw_src.Add("pw", pw);
            bw_src.Add("sum", items.Count + 1);
            bw_src.Add("result_cnt", result_cnt);
            bw_src.Add("imgchk", false);
            bw_src.Add("bw_main", bw_ETE_Download);
            bw_src.Add("eqp", this.ETE_src_eqp.Text);
            bw_src.Add("ete_flag", 1);
            bw_ETE_Download.DoWork += new DoWorkEventHandler(bw_ETL_Download_DoWork);
            bw_ETE_Download.ProgressChanged += bw_ETL_Download_ProgressChanged;
            bw_ETE_Download.RunWorkerCompleted += bw_ETL_Download_RunWorkerCompleted;
            bw_ETE_Download.WorkerReportsProgress = true;
            bw_ETE_Download.WorkerSupportsCancellation = true;
            bw_ETE_Download.RunWorkerAsync(argument: bw_src);
        }

        private void bw_ETL_Download_DoWork(object sender, DoWorkEventArgs e)
        {
            IList src_items = null;
            var bw_dic = (Dictionary<string, dynamic>)e.Argument;
            IList items = (IList)bw_dic["items"];
            string src_path = (string)bw_dic["src_path"];
            string tar_path = (string)bw_dic["tar_path"];
            string drive = (string)bw_dic["drive"];
            string id = (string)bw_dic["id"];
            string pw = (string)bw_dic["pw"];
            int sum = (int)bw_dic["sum"];
            long result_cnt = (long)bw_dic["result_cnt"];   //바로 못구함
            bool imgchk = (bool)bw_dic["imgchk"];
            BackgroundWorker bw_main = (BackgroundWorker)bw_dic["bw_main"];
            string eqp = (string)bw_dic["eqp"];
            int ete_flag = (int)bw_dic["ete_flag"];

            if (tar_path.Substring(tar_path.Length - 1, 1) == "/") tar_path = tar_path.Substring(0, tar_path.Length - 1);

            int cnt_all = 0;

            int fail_flag = 0;
            string raw_path = "";

            viewModel_distribute.pgb_val_ini(items, 5);

            //src_path가 ftp주소이면 로컬로 먼저 다운로드
            //eqp가 입력이 되었다면 로컬로 먼저 다운로드

            //FTP 재귀함수 개발중

            if (eqp != "eqp")
            {
                int cnt = 0;
                Dispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate
                {
                    //this.ETE_pgb.IsIndeterminate = true;
                    this.ETE_pgb.Foreground = Brushes.Aqua;
                    this.ETE_pgb_txt.Text = "로컬로 다운중";
                }));
                
                string uri = String.Format("ftp://{0}:21/{1}", gfun.source_EQP_dict[eqp], src_path);
                string local_path = @"C:\\Load_Spectrum2\\Temp\\";
                string[] src_temp = new string[1];
                src_temp[0] = local_path;
                result_cnt = 0;                

                var cast_item = (NewModel.Model)items[0];   //더미용 파라미터

                int path_num = 0;
                int path_num_flag = 1;

                (src_items, cnt, fail_flag, raw_path, tar_path, result_cnt, imgchk, ete_flag, eqp, path_num, path_num_flag) = viewModel_distribute.ETL_Download(cast_item, src_path, tar_path, id, pw, cnt, result_cnt, raw_path, fail_flag, imgchk, ete_flag, eqp, path_num, path_num_flag);

                //viewModel_distribute.ETE_LocalDownload(src_path, local_path, id, pw, cnt, result_cnt, raw_path, fail_flag, eqp);

                Dispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate
                {
                    //this.ETE_pgb.IsIndeterminate = false;
                    this.ETE_pgb.Value = 0;
                    this.ETE_pgb_txt.Text = "";
                    this.ETE_pgb.Foreground = Brushes.GreenYellow;
                }));

                cnt_all++;
                double ratio = ((double)cnt_all / (double)sum) * 100.0;
                bw_main.ReportProgress((int)ratio, ete_flag);

                foreach (var item in items)
                {
                    cnt = 0;
                    cast_item = (NewModel.Model)item;
                    string new_path = "";

                    if (src_path.Contains("/"))
                    {
                        new_path = @"C:\Load_Spectrum2\Temp\" + src_path.Substring(src_path.LastIndexOf("/") + 1, src_path.Length - src_path.LastIndexOf("/") - 1);
                    }
                    else
                    {
                        new_path = @"C:\Load_Spectrum2\Temp\" + src_path;
                    }


                    (src_items, cnt, fail_flag, raw_path, tar_path, ete_flag) = viewModel_distribute.LTE_Upload(cast_item, new_path, tar_path, id, pw, cnt, result_cnt, raw_path, fail_flag, ete_flag);

                    cnt_all++;
                    ratio = ((double)cnt_all / (double)sum) * 100.0;
                    bw_main.ReportProgress((int)ratio, ete_flag);
                }
            }
            else
            {
                foreach (var item in items)
                {                    
                    var cast_item = (NewModel.Model)item;                    

                    for (int num = 0; num < Int32.Parse(cast_item.strNum_Set4); num++)
                    {
                        int cnt = 0;
                        result_cnt = 0;
                        src_path = Global_FunSet.ETL_Sim_pathDic[cast_item.strchkEQPID_Set4 + "(" + String.Format("{0}", num + 1) + ")"];
                        int path_num = num + 1;
                        int path_num_flag = Int32.Parse(cast_item.strNum_Set4);
                        (src_items, cnt, fail_flag, raw_path, tar_path, result_cnt, imgchk, ete_flag, eqp, path_num, path_num_flag) = viewModel_distribute.ETL_Download(cast_item, src_path, tar_path, id, pw, cnt, result_cnt, raw_path, fail_flag, imgchk, ete_flag, eqp, path_num, path_num_flag);                                               
                    }
                    

                    cnt_all++;
                    double ratio = ((double)cnt_all / (double)sum) * 100.0;
                    bw_main.ReportProgress((int)ratio, ete_flag);
                }
            }

            List<dynamic> rst = new List<dynamic>();
            rst.Add(src_items);
            rst.Add(ete_flag);
            //e.Result = src_items;
            e.Result = rst;
        }

        private void bw_ETL_Download_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            // Running on a UI thread            
            var arg_flag = (int)e.UserState;
            if (arg_flag == 0)
            {
                this.ETL_pgb.Value = e.ProgressPercentage;
                this.ETL_pgb_txt.Text = e.ProgressPercentage.ToString() + " %";
            }
            else
            {
                this.ETE_pgb.Value = e.ProgressPercentage;
                this.ETE_pgb_txt.Text = e.ProgressPercentage.ToString() + " %";
            }
            
        }

        private void bw_ETL_Download_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            List<dynamic> rst = (List<dynamic>)e.Result;

            var arg = (IList)rst[0];
            int ete_flag = (int)rst[1];

            if (ete_flag == 0)
            {
                this.ETL_Spin.Visibility = Visibility.Collapsed;
                this.ETL_pgb.Value = 100;
                this.ETL_pgb_txt.Text = "100%";

                this.list_ETL.ItemsSource = arg;
                this.list_ETL.Items.Refresh();
                this.ETL_Refresh.IsEnabled = true;
                this.ETL_distribute.IsEnabled = false;
            }
            else
            {
                this.ETE_Spin.Visibility = Visibility.Collapsed;
                this.ETE_pgb.Value = 100;
                this.ETE_pgb_txt.Text = "100%";

                this.list_ETE.ItemsSource = arg;
                this.list_ETE.Items.Refresh();
                this.ETE_Refresh.IsEnabled = true;
                this.ETE_distribute.IsEnabled = false;
            }

            //RNR 정보 전달
            //file.readalltext(file)
            List<string> RNR_Wafer = new List<string>();            
            List<string> RNR_INEG = new List<string>();

            int cnt = 0;
            try
            {
                foreach (var entry in Global_FunSet.ETL_Sim_pathDic)
                {
                    if (cnt == 0)
                    {
                        string select_eqp = entry.Key;
                        string select_path = Global_FunSet.ETL_Sim_LocalPath[select_eqp];

                        string filename = System.IO.Path.GetFileName(select_path);
                        string exten = System.IO.Path.GetExtension(select_path);

                        //로컬확장자가 csv일때만 진행                        
                        RNR_INEG.Add("All");
                        RNR_INEG.Add("INNER");
                        RNR_INEG.Add("EDGE");

                        if (!exten.ToUpper().Contains("csv"))
                        {
                            string txt = File.ReadAllText(select_path);
                            string[] txt_part = txt.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

                            //Wafer 추가
                            foreach (string part in txt_part)       //part는 한 개의 행 의미
                            {
                                if (part.StartsWith("SLOT")) RNR_Wafer.Add(part.Split(',')[1]);     //wafer 추가
                            }
                            //Item, Layer는 ListView 바인딩이라 ViewModel에서 작업                            
                            
                            var src = (List<NewModel.Model>)viewModel_distribute.MkSrc_ItemLayer(txt_part);                            
                            this.List_RNR_Item_Layer.ItemsSource = src;                            
                            this.List_RNR_Item_Layer.Items.Refresh();

                            RNR_Wafer = RNR_Wafer.Distinct().ToList();

                            foreach (string ele in RNR_Wafer)
                            {                                
                                this.List_RNR_Wafer.Items.Add(ele);
                                this.List_RNR_Wafer.Items.Refresh();
                            }
                                
                            foreach (string ele in RNR_INEG)
                            {
                                this.List_RNR_INEG.Items.Add(ele);
                                this.List_RNR_INEG.Items.Refresh();
                            }
                                
                            this.List_RNR_Wafer.SelectedIndex = 0;                            
                            this.List_RNR_INEG.SelectedIndex = 1;

                        }
                        else break;
                    }
                    cnt++;
                }
            }
            catch
            {
                //RNR 데이터 정보 불러오지 못하면 그냥 생략
            }
            
            //Debug.WriteLine(RNR_Wafer);
            //Global_FunSet.ETL_Sim_pathDic.Clear();
            //Global_FunSet.ETL_Sim_LocalPath.Clear();            
        }

        private void select_ETL_eqppath_Click(object sender, RoutedEventArgs e)
        {
            IInputElement focusedControl = FocusManager.GetFocusedElement(this);
            Button btn = (Button)focusedControl;
            int set_flag = 0;

            if (btn.Name == "select_ETL_eqppath") set_flag = 3;
            else if (btn.Name == "ETL_btn") set_flag = 4;
            else if (btn.Name == "ETE_src_btn") set_flag = 5;
            else if (btn.Name == "ETE_tar_btn") set_flag = 6;

            NewView.ViewFTPFilePathDialog viewFTPFilePathDialog = new NewView.ViewFTPFilePathDialog(set_flag);
            if (viewFTPFilePathDialog.ShowDialog() == true)
            {
                var item = viewFTPFilePathDialog.list_ftpdia.SelectedItem;
                if (item == null)
                {
                    var cast_item = (NewModel.Model)item;
                    string str_line = viewFTPFilePathDialog.line_ftpdia.Text;

                    if (str_line == "")
                    {
                        if (set_flag == 3)
                        {
                            this.LTE_eqp_path.Text = "MI";
                            return;
                        }
                        else if (set_flag ==4)
                        {
                            this.ETL_eqp_path.Text = "MI";
                            return;
                        }
                        else if (set_flag == 5)
                        {
                            //skip
                        }
                        else if (set_flag == 6)
                        {
                            this.ETE_tar_path.Text = "MI";
                            return;
                        }
                        
                    }
                    else if (str_line.Substring(str_line.Length - 3, 3) == ":21")
                    {
                        if (set_flag == 3)
                        {
                            this.LTE_eqp_path.Text = "";
                            return;
                        }
                        else if (set_flag == 4)
                        {
                            this.ETL_eqp_path.Text = "";
                            return;
                        }
                        else if (set_flag == 5)
                        {
                            this.ETE_src_path.Text = "";
                            return;
                        }
                        else if (set_flag == 6)
                        {
                            this.ETE_tar_path.Text = "";
                            return;
                        }

                    }

                    //int count = cast_item.ftp_all_filename.Count(f => f == '/');
                    //if (count <= 2) return;

                    string path = viewFTPFilePathDialog.line_ftpdia.Text;
                    string _path = path.Replace("FTP://", "");
                    string real_path = _path.Substring(_path.IndexOf("/") + 1, _path.Length - _path.IndexOf("/") - 1);

                    if (set_flag == 3) this.LTE_eqp_path.Text = real_path;
                    else if (set_flag == 4) this.ETL_eqp_path.Text = real_path;
                    else if (set_flag == 5) this.ETE_src_path.Text = real_path;
                    else if (set_flag == 6) this.ETE_tar_path.Text = real_path;

                    if (set_flag == 3)
                    {
                        if (((ComboBoxItem)viewFTPFilePathDialog.combo_ftpdia.SelectedItem).Content.ToString().Contains("C"))
                        {
                            this.LTE_combo_drive.SelectedIndex = 0;
                        }
                        else if (((ComboBoxItem)viewFTPFilePathDialog.combo_ftpdia.SelectedItem).Content.ToString().Contains("D"))
                        {
                            this.LTE_combo_drive.SelectedIndex = 1;
                        }
                        else
                        {
                            this.LTE_combo_drive.SelectedIndex = 2;
                        }
                    }
                    else if (set_flag == 4)
                    {
                        if (((ComboBoxItem)viewFTPFilePathDialog.combo_ftpdia.SelectedItem).Content.ToString().Contains("C"))
                        {
                            this.ETL_combo_drive.SelectedIndex = 0;
                        }
                        else if (((ComboBoxItem)viewFTPFilePathDialog.combo_ftpdia.SelectedItem).Content.ToString().Contains("D"))
                        {
                            this.ETL_combo_drive.SelectedIndex = 1;
                        }
                        else
                        {
                            this.ETL_combo_drive.SelectedIndex = 2;
                        }
                    }
                    else if (set_flag == 5)
                    {
                        if (((ComboBoxItem)viewFTPFilePathDialog.combo_ftpdia.SelectedItem).Content.ToString().Contains("C"))
                        {
                            this.ETE_src_combo_drive.SelectedIndex = 0;
                        }
                        else if (((ComboBoxItem)viewFTPFilePathDialog.combo_ftpdia.SelectedItem).Content.ToString().Contains("D"))
                        {
                            this.ETE_src_combo_drive.SelectedIndex = 1;
                        }
                        else
                        {
                            this.ETE_src_combo_drive.SelectedIndex = 2;
                        }
                        var cast_item2 = (NewModel.Model)viewFTPFilePathDialog.List_EQPID_ftpdia.SelectedItem;
                        this.ETE_src_eqp.Text = cast_item2.strEQPID_ftpdia;
                    }
                    else if (set_flag == 6)
                    {
                        if (((ComboBoxItem)viewFTPFilePathDialog.combo_ftpdia.SelectedItem).Content.ToString().Contains("C"))
                        {
                            this.ETE_tar_combo_drive.SelectedIndex = 0;
                        }
                        else if (((ComboBoxItem)viewFTPFilePathDialog.combo_ftpdia.SelectedItem).Content.ToString().Contains("D"))
                        {
                            this.ETE_tar_combo_drive.SelectedIndex = 1;
                        }
                        else
                        {
                            this.ETE_tar_combo_drive.SelectedIndex = 2;
                        }
                    }
                }
                else
                {
                    var cast_item = (NewModel.Model)item;
                    if (cast_item.ftp_all_filename == "") return;                    

                    string path = viewFTPFilePathDialog.line_ftpdia.Text + "/" + cast_item.ftp_all_filename;
                    string _path = path.Replace("FTP://", "");

                    string real_path = _path.Substring(_path.IndexOf("/") + 1, _path.Length - _path.IndexOf("/") - 1);                    

                    if (set_flag == 3) this.LTE_eqp_path.Text = real_path;
                    else if (set_flag == 4) this.ETL_eqp_path.Text = real_path;
                    else if (set_flag == 5) this.ETE_src_path.Text = real_path;
                    else if (set_flag == 6) this.ETE_tar_path.Text = real_path;

                    if (set_flag == 3)
                    {
                        if (((ComboBoxItem)viewFTPFilePathDialog.combo_ftpdia.SelectedItem).Content.ToString().Contains("C"))
                        {
                            this.LTE_combo_drive.SelectedIndex = 0;
                        }
                        else if (((ComboBoxItem)viewFTPFilePathDialog.combo_ftpdia.SelectedItem).Content.ToString().Contains("D"))
                        {
                            this.LTE_combo_drive.SelectedIndex = 1;
                        }
                        else
                        {
                            this.LTE_combo_drive.SelectedIndex = 2;
                        }
                    }
                    else if (set_flag == 4)
                    {
                        if (((ComboBoxItem)viewFTPFilePathDialog.combo_ftpdia.SelectedItem).Content.ToString().Contains("C"))
                        {
                            this.ETL_combo_drive.SelectedIndex = 0;
                        }
                        else if (((ComboBoxItem)viewFTPFilePathDialog.combo_ftpdia.SelectedItem).Content.ToString().Contains("D"))
                        {
                            this.ETL_combo_drive.SelectedIndex = 1;
                        }
                        else
                        {
                            this.ETL_combo_drive.SelectedIndex = 2;
                        }
                    }
                    else if (set_flag == 5)
                    {
                        if (((ComboBoxItem)viewFTPFilePathDialog.combo_ftpdia.SelectedItem).Content.ToString().Contains("C"))
                        {
                            this.ETE_src_combo_drive.SelectedIndex = 0;
                        }
                        else if (((ComboBoxItem)viewFTPFilePathDialog.combo_ftpdia.SelectedItem).Content.ToString().Contains("D"))
                        {
                            this.ETE_src_combo_drive.SelectedIndex = 1;
                        }
                        else
                        {
                            this.ETE_src_combo_drive.SelectedIndex = 2;
                        }
                        var cast_item2 = (NewModel.Model)viewFTPFilePathDialog.List_EQPID_ftpdia.SelectedItem;
                        this.ETE_src_eqp.Text = cast_item2.strEQPID_ftpdia;
                    }
                    else if (set_flag == 6)
                    {
                        if (((ComboBoxItem)viewFTPFilePathDialog.combo_ftpdia.SelectedItem).Content.ToString().Contains("C"))
                        {
                            this.ETE_tar_combo_drive.SelectedIndex = 0;
                        }
                        else if (((ComboBoxItem)viewFTPFilePathDialog.combo_ftpdia.SelectedItem).Content.ToString().Contains("D"))
                        {
                            this.ETE_tar_combo_drive.SelectedIndex = 1;
                        }
                        else
                        {
                            this.ETE_tar_combo_drive.SelectedIndex = 2;
                        }
                    }
                }
            }
        }


        //ETL
        private void select_ETL_localfile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            if (openFileDialog.ShowDialog() == true)
            {
                this.ETL_Local_path.Text = openFileDialog.FileName;
            }

        }

        private void select_ETL_localpath_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new Ookii.Dialogs.Wpf.VistaFolderBrowserDialog();
            if (dialog.ShowDialog(this).GetValueOrDefault())
            {
                this.ETL_Local_path.Text = dialog.SelectedPath;
            }

        }

        private void tb_EQPID_eqp4_TextChanged(object sender, TextChangedEventArgs e)
        {
            EPQFilter_View(4);
        }

        private void List_Line_eqpSet4_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            EPQFilter_View(4);
        }

        private void List_ZONE_eqpSet4_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            EPQFilter_View(4);
        }

        private void List_EQPTYPE_eqpSet4_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            EPQFilter_View(4);
        }

        private void List_EQPMODEL_eqpSet4_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            EPQFilter_View(4);
        }

        private void EQPIDResetBtn_EQP4_Click(object sender, RoutedEventArgs e)
        {
            this.List_Line_eqpSet4.SelectedIndex = -1;
            this.List_ZONE_eqpSet4.SelectedIndex = -1;
            this.List_EQPTYPE_eqpSet4.SelectedIndex = -1;            
            this.List_EQPID_eqpSet4.SelectedIndex = -1;
            this.List_EQPMODEL_eqpSet4.SelectedIndex = -1;
            this.tb_EQPID_eqp4.Text = "";
        }

        private void tb_EQPID_eqp5_TextChanged(object sender, TextChangedEventArgs e)
        {
            EPQFilter_View(5);
        }

        private void List_Line_eqpSet5_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            EPQFilter_View(5);
        }

        private void List_ZONE_eqpSet5_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            EPQFilter_View(5);
        }

        private void List_EQPTYPE_eqpSet5_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            EPQFilter_View(5);
        }

        private void List_EQPMODEL_eqpSet5_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            EPQFilter_View(5);
        }

        private void EQPIDResetBtn_EQP5_Click(object sender, RoutedEventArgs e)
        {
            this.List_Line_eqpSet5.SelectedIndex = -1;
            this.List_ZONE_eqpSet5.SelectedIndex = -1;
            this.List_EQPTYPE_eqpSet5.SelectedIndex = -1;
            this.List_EQPMODEL_eqpSet5.SelectedIndex = -1;
            this.List_EQPID_eqpSet5.SelectedIndex = -1;
            this.tb_EQPID_eqp5.Text = "";
        }

        private void ZONEResetBtn_EQP5_Click(object sender, RoutedEventArgs e)
        {
            this.List_ZONE_eqpSet5.SelectedIndex = -1;
        }

        private void EQPTYPEResetBtn_EQP5_Click(object sender, RoutedEventArgs e)
        {
            this.List_EQPTYPE_eqpSet5.SelectedIndex = -1;
        }

        private void EQPMODELRsesetBtn_EQP5_Click(object sender, RoutedEventArgs e)
        {
            this.List_EQPMODEL_eqpSet5.SelectedIndex = -1;
        }

        private void LineResetBtn_EQP5_Click(object sender, RoutedEventArgs e)
        {
            this.List_Line_eqpSet5.SelectedIndex = -1;
        }

        private void LineResetBtn_EQP4_Click(object sender, RoutedEventArgs e)
        {
            this.List_Line_eqpSet4.SelectedIndex = -1;
        }

        private void EQPTYPEResetBtn_EQP4_Click(object sender, RoutedEventArgs e)
        {
            this.List_EQPTYPE_eqpSet4.SelectedIndex = -1;
        }

        private void ZONEResetBtn_EQP4_Click(object sender, RoutedEventArgs e)
        {
            this.List_ZONE_eqpSet4.SelectedIndex = -1;
        }

        private void EQPMODELRsesetBtn_EQP4_Click(object sender, RoutedEventArgs e)
        {
            this.List_EQPMODEL_eqpSet4.SelectedIndex = -1;
        }

        private void ETE_tar_path_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (this.ETE_tar_path.Text.Contains("\\") || this.ETE_tar_path.Text.Contains(":") || this.ETE_tar_path.Text.Contains("*") || this.ETE_tar_path.Text.Contains("?") ||
                this.ETE_tar_path.Text.Contains("\"") || this.ETE_tar_path.Text.Contains("<") || this.ETE_tar_path.Text.Contains(">") || this.ETE_tar_path.Text.Contains("|"))
            {
                this.ETE_tar_path.Text = this.ETE_tar_path.Text.Substring(0, this.ETE_tar_path.Text.Length - 1);
                this.ETE_tar_path.Select(this.ETE_tar_path.Text.Length, 0);
            }

            try
            {
                if (this.ETE_tar_path.Text.Substring(this.ETE_tar_path.Text.Length - 1, 1) == "/" && this.ETE_tar_path.Text.Substring(this.ETE_tar_path.Text.Length - 2, 1) == "/")
                {
                    this.ETE_tar_path.Text = this.ETE_tar_path.Text.Substring(0, this.ETE_tar_path.Text.Length - 1);
                    this.ETE_tar_path.Select(this.ETE_tar_path.Text.Length, 0);
                }
            }
            catch
            {

            }

            if (this.ETE_tar_path.Text.StartsWith("/") || this.ETE_tar_path.Text.StartsWith("\\")) this.ETE_tar_path.Text = "";
        }

        private void ETL_eqp_path_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.ETL_distribute.IsEnabled = false;
        }

        private void btn_RNR_down_Click(object sender, RoutedEventArgs e)
        {
            //Source : Global_FunSet.ETL_Sim_LocalPath
            if (this.List_RNR_Item_Layer.Items.Count == 0)
            {
                MessageBox.Show("Item / Layer가 없습니다.");
                return;
            }

            List<string> s_wafer = new List<string>();            
            List<string> s_ineg = new List<string>();
            List<NewModel.Model> s_ItemLayer = new List<NewModel.Model>();
            Dictionary<string, string> New_RNR_PathDic = new Dictionary<string, string>();
            
            foreach (var item in this.List_RNR_Item_Layer.Items)
            {
                var cast_item = (NewModel.Model)item;
                if ((bool)cast_item.strRNR_Chk == true)
                {
                    s_ItemLayer.Add(cast_item);
                }                
            }
            
            if (s_ItemLayer.Count == 0)
            {
                MessageBox.Show("선택된 Item / Layer가 없습니다.");
                return;
            }
            
            foreach (var item in this.List_RNR_Wafer.SelectedItems)
            {
                s_wafer.Add(item.ToString());
            }            
            foreach (var item in this.List_RNR_INEG.SelectedItems)
            {
                s_ineg.Add(item.ToString());
            }


            //중복안되는 ele 괄호 제거            
            foreach (var entry in Global_FunSet.ETL_Sim_LocalPath)
            {
                string temp = entry.Key.Substring(0, 7);
                int cnt = 0;
                
                if (!System.IO.Path.GetExtension(entry.Value).ToUpper().Contains("CSV"))
                {
                    MessageBox.Show("csv가 아닌 파일이 있습니다.");                    
                    return;
                }

                foreach (var entry2 in Global_FunSet.ETL_Sim_LocalPath)
                {
                    string temp2 = entry2.Key.Substring(0, 7);
                    if (temp == temp2) cnt++;
                }
                if (cnt == 1)
                {
                    //중복 없으면
                    New_RNR_PathDic.Add(temp, entry.Value);
                }
                else
                {
                    //중복 있으면 
                    New_RNR_PathDic.Add(entry.Key, entry.Value);
                }
            }

            //Wafer 오름차순
            s_wafer.Sort();
            List<KeyValuePair<string, string>> myList = New_RNR_PathDic.ToList();
            myList.Sort(new Sort_CSVDate());
            var sort_PathDic = myList.ToDictionary(x => x.Key, x => x.Value);

            Dictionary<string, object> th_dic = new Dictionary<string, object>();

            th_dic.Add("s_ItemLayer", s_ItemLayer);
            th_dic.Add("s_wafer", s_wafer);
            th_dic.Add("s_ineg", s_ineg);
            th_dic.Add("sort_PathDic", sort_PathDic);

            //쓰레드 처리
            Thread thread_ftp_login = new Thread(new ParameterizedThreadStart(MK_Excel));
            this.ETL_pgb.Value = 0;
            this.ETL_Spin.Visibility = Visibility.Visible;
            this.ETL_pgb.IsIndeterminate = true;
            this.ETL_pgb_txt.Text = "데이터취합중";
            this.btn_RNR_down.IsEnabled = false;

            thread_ftp_login.IsBackground = true;
            thread_ftp_login.Start(th_dic);
        }

        private void MK_Excel(object dic)
        {
            //아래는 ftp 접속
            var dic_th = (Dictionary<string, object>)dic;
            List<NewModel.Model> s_ItemLayer = (List<NewModel.Model>)dic_th["s_ItemLayer"];
            List<string> s_wafer = (List<string>)dic_th["s_wafer"];
            List<string> s_ineg = (List<string>)dic_th["s_ineg"];
            Dictionary<string, string> sort_PathDic = (Dictionary<string, string>)dic_th["sort_PathDic"];

            if (Global_FunSet.MkExcelFrom_RNRSrc(s_ItemLayer, s_wafer, s_ineg, sort_PathDic) == -1)
            {
                MessageBox.Show("엑셀 생성 실패" + "\n(같은 이름의 파일을 열고 있으면 엑셀 생성이 안됩니다.)");
                Dispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate
                {
                    this.ETL_pgb.Value = 100;
                    this.ETL_Spin.Visibility = Visibility.Collapsed;
                    this.ETL_pgb.IsIndeterminate = false;
                    this.ETL_pgb_txt.Text = "엑셀 생성 실패";
                    this.btn_RNR_down.IsEnabled = true;
                    return;
                }));
            }

            Dispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate
            {
                this.ETL_pgb.Value = 100;
                this.ETL_Spin.Visibility = Visibility.Collapsed;
                this.ETL_pgb.IsIndeterminate = false;
                this.ETL_pgb_txt.Text = "취합완료";
                this.btn_RNR_down.IsEnabled = true;
            }));
        }

        private void btn_FolderOpen_Click(object sender, RoutedEventArgs e)
        {
            Process.Start(@"C:\Load_Spectrum2\RNR");
        }

        private void btn_LTE_Reset_Click(object sender, RoutedEventArgs e)
        {
            SubListEQPID(lv_all3, lv_eqp3, this.btn_LTE_Reset);
        }

        private void btn_RNR_Reset_Click(object sender, RoutedEventArgs e)
        {
            SubListEQPID(lv_all4, lv_eqp4, this.btn_RNR_Reset);
        }

        private void btn_ETE_Reset_Click(object sender, RoutedEventArgs e)
        {
            SubListEQPID(lv_all5, lv_eqp5, this.btn_ETE_Reset);
        }
    }
}
