using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
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
    /// Interaction logic for ViewFTPFilePathDialog.xaml
    /// </summary>
    public partial class ViewFTPFilePathDialog : Window
    {
        private static Global_FunSet gfun;
        private NewViewModel.ViewModel_FTPFilePathDialog viewModel_FTPFilePathDialog;
        public static Dictionary<string, string> source_EQP_dict = new Dictionary<string, string>();
        ListSortDirection _lastDirection = ListSortDirection.Ascending;
        GridViewColumnHeader _lastHeaderClicked = null;
        public static List<String> Back_Path_FTPDia;
        public static List<String> Foward_Path_FTPDia;
        private int Set_flag;
        private int header_flag = 0;

        public ViewFTPFilePathDialog(int set_flag)
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(MainPage_Loaded);
            this.PreviewKeyDown += new KeyEventHandler(Key_Handler);
            Set_flag = set_flag;

            viewModel_FTPFilePathDialog = new NewViewModel.ViewModel_FTPFilePathDialog();
            this.DataContext = viewModel_FTPFilePathDialog;

            this.combo_ftpdia.SelectedIndex = 0;
            

            gfun = new Global_FunSet();

            Back_Path_FTPDia = new List<String>();
            Foward_Path_FTPDia = new List<String>();

            this.List_EQPID_ftpdia.Items.SortDescriptions.Add(new SortDescription("strEQPID_ftpdia", ListSortDirection.Ascending));
        }

        void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            //왜 Xaml에서 안먹는지 몰라서 그냥 코드로 구현          
            ListView l = this.list_ftpdia;
            GridView g = l.View as GridView;
            double total = 0;
            for (int i = 0; i < g.Columns.Count - 1; i++)
            {
                total += g.Columns[i].Width;
            }

            g.Columns[g.Columns.Count - 1].Width = l.ActualWidth - total;

        }

        private void Key_Handler(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                this.DialogResult = false;

            if (e.Key == Key.Enter || e.Key == Key.Space)
            {
                IInputElement focusedControl = FocusManager.GetFocusedElement(this);
                var f_listview = focusedControl as ListViewItem;
                if (f_listview == null) return;
                var str_chk = f_listview.Content.ToString();
                
                if (str_chk.Contains("NewModel.Model"))
                {
                    if (this.list_ftpdia.SelectedItems.Count > 1) return;
                    var items = (NewModel.Model)this.list_ftpdia.SelectedItem;

                    if (items.ftp_all_extension == "폴더")
                    {
                        MkFTPDiaList(this.line_ftpdia.Text + "/" + items.ftp_all_filename, "1");
                    }
                    else
                    {
                        if (Set_flag == 3) MessageBox.Show("업로드할 폴더를 선택하세요");
                        else
                        {
                            this.DialogResult = true;
                        }
                    }
                }

            }
            else if (e.Key == Key.Back || (Keyboard.IsKeyDown(Key.LeftAlt) && (Keyboard.IsKeyDown(Key.Left))))
            {
                IInputElement focusedControl = FocusManager.GetFocusedElement(this);
                var f_listviewitem = focusedControl as ListViewItem;
                var f = focusedControl as ListView;

                if (f_listviewitem == null & f == null) return;

                if (f_listviewitem != null)
                {
                    try
                    {
                        if (Back_Path_FTPDia[Back_Path_FTPDia.Count() - 1] == this.line_ftpdia.Text)
                            Back_Path_FTPDia.Remove(Back_Path_FTPDia[Back_Path_FTPDia.Count() - 1]);
                    }
                    catch { }

                    if (Back_Path_FTPDia == null || Back_Path_FTPDia.Count() == 0 || Back_Path_FTPDia[Back_Path_FTPDia.Count() - 1] == "")
                    {
                        return;
                    }
                    else
                    {
                        Foward_Path_FTPDia.Add(this.line_ftpdia.Text);

                        string uri = Back_Path_FTPDia[Back_Path_FTPDia.Count() - 1];
                        string foback_flag = "2";

                        MkFTPDiaList(uri, foback_flag);

                        Back_Path_FTPDia.Remove(Back_Path_FTPDia[Back_Path_FTPDia.Count() - 1]);
                    }


                }
                else if (f_listviewitem == null)
                {
                    try
                    {
                        if (Back_Path_FTPDia[Back_Path_FTPDia.Count() - 1] == this.line_ftpdia.Text)
                            Back_Path_FTPDia.Remove(Back_Path_FTPDia[Back_Path_FTPDia.Count() - 1]);
                    }
                    catch { }

                    if (Back_Path_FTPDia == null || Back_Path_FTPDia.Count() == 0 || Back_Path_FTPDia[Back_Path_FTPDia.Count() - 1] == "")
                    {
                        return;
                    }
                    else
                    {
                        Foward_Path_FTPDia.Add(this.line_ftpdia.Text);

                        string uri = Back_Path_FTPDia[Back_Path_FTPDia.Count() - 1];
                        string foback_flag = "2";

                        MkFTPDiaList(uri, foback_flag);

                        Back_Path_FTPDia.Remove(Back_Path_FTPDia[Back_Path_FTPDia.Count() - 1]);
                    }
                }
            }
        }
        private void select_ftpdia_Click(object sender, RoutedEventArgs e)
        {
            if (this.list_ftpdia.SelectedItems.Count > 1)
            {
                MessageBox.Show("1개의 폴더(파일)만 선택하세요");
                return;
            }
            else
            {
                this.DialogResult = true;
            }            
        }

        private void tb_EQPID_ftpdia_TextChanged(object sender, TextChangedEventArgs e)
        {
            var src = viewModel_FTPFilePathDialog.FTPDialogEQPFilter(this.tb_EQPID_ftpdia.Text);
            this.List_EQPID_ftpdia.ItemsSource = null;
            this.List_EQPID_ftpdia.ItemsSource = src;
            this.List_EQPID_ftpdia.Items.Refresh();
        }

        private void List_EQPID_ftpdia_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {                
                var item = this.List_EQPID_ftpdia.SelectedItem;
                var cast_item = (NewModel.Model)item;
                string uri = string.Format(@"FTP://{0}:{1}", gfun.source_EQP_dict[cast_item.strEQPID_ftpdia], 21);
                MkFTPDiaList(uri, "1");                
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void MkFTPDiaList(string uri, string foback_flag)
        {
            Dictionary<string, string> source_ftpdia_dict = new Dictionary<string, string>();

            string path = "";
            string id = "";
            string pw = "";            
            var item = this.List_EQPID_ftpdia.SelectedItem;
            var cast_item = (NewModel.Model)item;
            string eqp = cast_item.strEQPID_ftpdia;
            string drive = ((ComboBoxItem)this.combo_ftpdia.SelectedItem).Content.ToString();

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
            else return;

            this.List_EQPID_ftpdia.IsHitTestVisible = false;
            this.list_ftpdia.IsHitTestVisible = false;
            try
            {
                source_ftpdia_dict.Add("uri", uri);
                source_ftpdia_dict.Add("id", id);
                source_ftpdia_dict.Add("pw", pw);
                source_ftpdia_dict.Add("eqp", eqp);
                source_ftpdia_dict.Add("drive", drive);
                source_ftpdia_dict.Add("foback_flag", foback_flag);

                Thread thread_ftpdia_login = new Thread(new ParameterizedThreadStart(FTPdia_Login));
                thread_ftpdia_login.IsBackground = true;
                thread_ftpdia_login.Start(source_ftpdia_dict);
            }
            catch
            {

            }
            
        }

        private void FTPdia_Login(object dic)
        {
            //아래는 ftp 접속
            var dic_ftp = (Dictionary<string, string>)dic;
            string uri = dic_ftp["uri"];
            string id = dic_ftp["id"];
            string pw = dic_ftp["pw"];
            string eqp = dic_ftp["eqp"];
            string drive = dic_ftp["drive"];
            string path = "";
            string str_err = "";
            List<NewModel.Model> src = new List<NewModel.Model>();

            Dispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate
            {
                this.pgb_ftpdia.IsIndeterminate = true;
                this.pgbTxt_ftpdia.Text = "접속중";
            }));
            
            (src, str_err) = viewModel_FTPFilePathDialog.ReturnPathSrc_ftpdia(uri, id, pw);            
            //FTP 접속 실패
            if (src == null)
            {
                Dispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate
                {
                    this.pgb_ftpdia.IsIndeterminate = false;
                    this.pgb_ftpdia.Value = 0;
                    this.pgbTxt_ftpdia.Text = "접속실패";
                    this.line_ftpdia.Text = "";
                    this.list_ftpdia.ItemsSource = null;
                    MessageBox.Show(str_err);
                    this.list_ftpdia.IsHitTestVisible = true;
                    this.List_EQPID_ftpdia.IsHitTestVisible = true;
                    return;
                }));                
            }

            Dispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate
            {
                if (dic_ftp["foback_flag"] == "1")
                {                    
                    Back_Path_FTPDia.Add(this.line_ftpdia.Text);

                }

                this.pgb_ftpdia.IsIndeterminate = false;
                this.pgb_ftpdia.Value = 100;
                this.pgbTxt_ftpdia.Text = "접속완료";
                this.line_ftpdia.Text = uri;
                this.list_ftpdia.ItemsSource = null;
                this.list_ftpdia.ItemsSource = src;
                this.list_ftpdia.Items.SortDescriptions.Clear();
                this.list_ftpdia.Items.SortDescriptions.Add(new SortDescription("ftp_all_extension", ListSortDirection.Descending));
                this.list_ftpdia.Items.SortDescriptions.Add(new SortDescription("ftp_all_filename", ListSortDirection.Ascending));
                //var view = (ListCollectionView)CollectionViewSource.GetDefaultView(list_ftpdia.ItemsSource); //Items is an ObservableCollection<T> 
                //view.CustomSort = new FTPDiaCustomSort(); //MyComparable implements IComparer
                this.list_ftpdia.Items.Refresh();
                try
                {
                    this.list_ftpdia.ScrollIntoView(this.list_ftpdia.Items[0]);
                    this.list_ftpdia.SelectedIndex = 0;
                    this.list_ftpdia.Focus();

                    ListBoxItem lbi = this.list_ftpdia.ItemContainerGenerator.ContainerFromIndex(this.list_ftpdia.SelectedIndex) as ListBoxItem;

                    if (lbi != null)
                    {

                        lbi.Focus();
                    }

                }
                catch { }
                this.line_cureqp.Text = string.Format("{0} 연결: {1}", eqp, drive);
                this.list_ftpdia.IsHitTestVisible = true;
                this.List_EQPID_ftpdia.IsHitTestVisible = true;
            }));           

        }

        private void list_ftpdia_Click(object sender, RoutedEventArgs e)
        {
            ListviewColumnSort(sender, e);
            header_flag = 1;
        }

        private void ListviewColumnSort(object sender, RoutedEventArgs e)
        {
            var headerClicked = e.OriginalSource as GridViewColumnHeader;
            ListSortDirection direction;

            if (headerClicked != null)
            {
                if (headerClicked.Role != GridViewColumnHeaderRole.Padding)
                {
                    if (headerClicked != _lastHeaderClicked)
                    {
                        direction = ListSortDirection.Ascending;
                    }
                    else
                    {
                        if (_lastDirection == ListSortDirection.Ascending)
                        {
                            direction = ListSortDirection.Descending;
                        }
                        else
                        {
                            direction = ListSortDirection.Ascending;
                        }
                    }

                    var columnBinding = headerClicked.Column.DisplayMemberBinding as Binding;
                    var sortBy = columnBinding?.Path.Path ?? headerClicked.Column.Header as string;

                    //object인 sender를 casting하여 Listview로 변환하여 함수에 매개인자로 전달
                    Sort(sortBy, direction, (ListView)sender);
                    var t = (ListView)sender;


                    if (direction == ListSortDirection.Ascending)
                    {
                        headerClicked.Column.HeaderTemplate =
                          Resources["HeaderTemplateArrowUp"] as DataTemplate;
                    }
                    else
                    {
                        headerClicked.Column.HeaderTemplate =
                          Resources["HeaderTemplateArrowDown"] as DataTemplate;
                    }

                    // Remove arrow from previously sorted header
                    if (_lastHeaderClicked != null && _lastHeaderClicked != headerClicked)
                    {
                        _lastHeaderClicked.Column.HeaderTemplate = null;
                    }

                    _lastHeaderClicked = headerClicked;
                    _lastDirection = direction;
                }
            }
        }

        private void Sort(string sortBy, ListSortDirection direction, ListView lv)
        {
            ICollectionView dataView =
              CollectionViewSource.GetDefaultView(lv.ItemsSource);
            try
            {
                dataView.SortDescriptions.Clear();
                SortDescription sd = new SortDescription(sortBy, direction);
                dataView.SortDescriptions.Add(sd);
                dataView.Refresh();
            }
            catch
            {
                return;
            }

        }

        private void EQPIDResetBtn_ftpdia_Click(object sender, RoutedEventArgs e)
        {
            this.List_EQPID_ftpdia.SelectedIndex = -1;
        }

        private void BackBtn_ftpdia_Click(object sender, RoutedEventArgs e)
        {
            try
            {                
                if (Back_Path_FTPDia[Back_Path_FTPDia.Count() - 1] == this.line_ftpdia.Text)
                    Back_Path_FTPDia.Remove(Back_Path_FTPDia[Back_Path_FTPDia.Count() - 1]);                
            }
            catch { }

            if (Back_Path_FTPDia == null || Back_Path_FTPDia.Count() == 0 || Back_Path_FTPDia[Back_Path_FTPDia.Count() - 1] == "")
            {
                return;
            }
            else
            {
                Foward_Path_FTPDia.Add(this.line_ftpdia.Text);

                string uri = Back_Path_FTPDia[Back_Path_FTPDia.Count() - 1];
                string foback_flag = "2";

                MkFTPDiaList(uri, foback_flag);

                Back_Path_FTPDia.Remove(Back_Path_FTPDia[Back_Path_FTPDia.Count() - 1]);
            }
        }

        private void FowardBtn_ftpdia_Click(object sender, RoutedEventArgs e)
        {
            if (Foward_Path_FTPDia == null || Foward_Path_FTPDia.Count() == 0)
            {
                return;
            }
            else
            {
                Back_Path_FTPDia.Add(this.line_ftpdia.Text);

                string uri = Foward_Path_FTPDia[Foward_Path_FTPDia.Count() - 1];
                string foback_flag = "2";

                MkFTPDiaList(uri, foback_flag);

                Foward_Path_FTPDia.Remove(Foward_Path_FTPDia[Foward_Path_FTPDia.Count() - 1]);
            }
        }

        private void UpperBtn_ftpdia_Click(object sender, RoutedEventArgs e)
        {
            var str_path = this.line_ftpdia.Text;
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
                string uri = str_path;                
                string foback_flag = "2";

                MkFTPDiaList(uri, foback_flag);
            }
        }

        private void list_ftpdia_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (header_flag == 1)
            {
                header_flag = 0;
                return;
            }
            if (this.list_ftpdia.SelectedItems.Count != 1) return;
            var items = (NewModel.Model)this.list_ftpdia.SelectedItem;            

            if (items.ftp_all_extension == "폴더")
            {
                MkFTPDiaList(this.line_ftpdia.Text + "/" + items.ftp_all_filename, "1");
            }
            else
            {
                if (Set_flag == 3) MessageBox.Show("업로드할 폴더를 선택하세요");
                else
                {
                    this.DialogResult = true;
                }

            }
            
        }
    }
}




