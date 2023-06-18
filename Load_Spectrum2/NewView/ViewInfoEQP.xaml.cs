using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
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
    /// Interaction logic for ViewInfoEQP.xaml
    /// </summary>
    public partial class ViewInfoEQP : Window
    {
        private NewViewModel.ViewModel_InfoEQP viewModel_InfoEQP;
        public static Global_FunSet gfun;

        public ViewInfoEQP()
        {
            InitializeComponent();

            gfun = new Global_FunSet();

            viewModel_InfoEQP = new NewViewModel.ViewModel_InfoEQP();
            this.DataContext = viewModel_InfoEQP;
            this.datagrid_eqp.DataContext = gfun.dt_eqp_info;
        }

        private void btn_info_AddRow_Click(object sender, RoutedEventArgs e)
        {
            AddrowDataGrid(this.datagrid_eqp);            
        }

        private void AddrowDataGrid(DataGrid dg)
        {
            dg.CanUserAddRows = true;
            dg.Focus();
            dg.SelectedIndex = this.datagrid_eqp.Items.Count - 1;
            dg.ScrollIntoView(this.datagrid_eqp.Items[this.datagrid_eqp.Items.Count - 1]);
            dg.CurrentCell = new DataGridCellInfo(datagrid_eqp.Items[this.datagrid_eqp.Items.Count - 1], datagrid_eqp.Columns[0]);
            dg.BeginEdit();            
        }

        private void btn_info_Save_Click(object sender, RoutedEventArgs e)
        {
            NewView.ViewPassword viewPassword = new NewView.ViewPassword();
            DataGrid dg = this.datagrid_eqp;
            try
            {
                if (viewPassword.ShowDialog() == true)
                {
                    if (viewPassword.Text_pw.Password == Global_FunSet.radmin_pw)
                    {
                        //인터락 걸기                    
                        //1. EQPID 중복 여부
                        //2. IP 중복 여부

                        if (Global_FunSet.CheckDataGridOverlap(dg, 0))
                        {
                            MessageBox.Show("중복된 EQPID는 등록할 수 없습니다.");
                            return;
                        }

                        if (Global_FunSet.CheckDataGridOverlap(dg, 1))
                        {
                            MessageBox.Show("중복된 IP는 등록할 수 없습니다.");
                            return;
                        }


                        this.pgb_infoEQP.Value = 0;
                        this.pgb_infoEQP.IsIndeterminate = true;
                        this.datagrid_eqp.ScrollIntoView(this.datagrid_eqp.Items[0]);

                        //데이터그리드 가져와서 서버에 업로드
                        Dictionary<string, dynamic> source_datagrid = new Dictionary<string, dynamic>();
                        DataTable dt_new = Global_FunSet.DataGridtoDataTable(this.datagrid_eqp);

                        if (dt_new == null)
                        {
                            this.pgb_infoEQP.Value = 100;
                            this.pgb_infoEQP.IsIndeterminate = false;
                            this.datagrid_eqp.ScrollIntoView(this.datagrid_eqp.Items[0]);
                            return;
                        }
                        //gfun.print_DataTable(dt_new);

                        source_datagrid.Add("dt_new", dt_new);

                        Thread thread_datagridUpload = new Thread(new ParameterizedThreadStart(th_DagaGridUpload));
                        thread_datagridUpload.IsBackground = true;
                        thread_datagridUpload.Start(source_datagrid);
                    }
                    else
                    {
                        MessageBox.Show("비밀번호가 틀립니다.");
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
            
        }

        private void th_DagaGridUpload(object dic)
        {            

            var dic_src = (Dictionary<string, dynamic>)dic;            
            DataTable dt_new = (DataTable)dic_src["dt_new"];
            //gfun.print_DataTable(dt_new);
            
            //1. dt_new를 csv로 저장 
            if (!Global_FunSet.DatatableToCSV(dt_new, Global_FunSet.info_t_path))
            {
                Dispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate
                {
                    this.pgb_infoEQP.Value = 100;
                    this.pgb_infoEQP.IsIndeterminate = false;
                    this.datagrid_eqp.ScrollIntoView(this.datagrid_eqp.Items[0]);
                }));
                return;
            }            

            //2. 저장한 csv를 서버에 업로드
            int passfail = Global_FunSet.FTPOneFileUpload(Global_FunSet.info_uri, Global_FunSet.info_t_path, Global_FunSet.info_id, Global_FunSet.info_pw);
            if (passfail == -1)
            {
                Dispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate
                {
                    this.pgb_infoEQP.Value = 100;
                    this.pgb_infoEQP.IsIndeterminate = false;
                    this.datagrid_eqp.ScrollIntoView(this.datagrid_eqp.Items[0]);
                }));
                MessageBox.Show("FTP 업로드 실패");
                return;
            }

            Dispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate
            {
                this.pgb_infoEQP.Value = 100;
                this.pgb_infoEQP.IsIndeterminate = false;
                this.datagrid_eqp.ScrollIntoView(this.datagrid_eqp.Items[0]);
                MessageBox.Show("설비등록 완료");
            }));
        }

        private void datagrid_eqp_KeyUp(object sender, KeyEventArgs e)
        {
            //key event
            DataGrid dg = sender as DataGrid;
            DataGridCellInfo cellinfo = dg.CurrentCell;
            DataGridCell cell = Global_FunSet.TryToFindGridCell(dg, cellinfo, -1, -1);
            var columnIndex = dg.Columns.IndexOf(dg.CurrentColumn);
            var rowIndex = dg.Items.IndexOf(dg.CurrentItem);

            if (cell.IsEditing == true && e.Key == Key.Right)
            {                     
                //cancel edit, save current text, move cell
                cell.Content = "dummy";         //when press arrow, "dummy" deleted and current text is saved
                this.datagrid_eqp.CancelEdit();
                try
                {
                    this.datagrid_eqp.CurrentCell = new DataGridCellInfo(datagrid_eqp.Items[rowIndex], datagrid_eqp.Columns[columnIndex + 1]);
                }
                catch (System.ArgumentOutOfRangeException)
                {
                    //last column exception
                    this.datagrid_eqp.CurrentCell = new DataGridCellInfo(datagrid_eqp.Items[rowIndex], datagrid_eqp.Columns[columnIndex]);
                }
            }
            else if (cell.IsEditing == true && e.Key == Key.Left)
            {
                //cancel edit, save current text, move cell
                cell.Content = "dummy";
                this.datagrid_eqp.CancelEdit();
                try
                {
                    this.datagrid_eqp.CurrentCell = new DataGridCellInfo(datagrid_eqp.Items[rowIndex], datagrid_eqp.Columns[columnIndex - 1]);
                }
                catch (System.ArgumentOutOfRangeException)
                {
                    //first column exception
                    this.datagrid_eqp.CurrentCell = new DataGridCellInfo(datagrid_eqp.Items[rowIndex], datagrid_eqp.Columns[columnIndex]);
                }
            }
            else if (cell.IsEditing == true && e.Key == Key.Up)
            {
                //cancel edit, save current text, move cell
                cell.Content = "dummy";
                this.datagrid_eqp.CancelEdit();
                try
                {
                    this.datagrid_eqp.CurrentCell = new DataGridCellInfo(datagrid_eqp.Items[rowIndex - 1], datagrid_eqp.Columns[columnIndex]);
                }
                catch (System.ArgumentOutOfRangeException)
                {
                    //first row exception
                    this.datagrid_eqp.CurrentCell = new DataGridCellInfo(datagrid_eqp.Items[rowIndex], datagrid_eqp.Columns[columnIndex]);
                }
            }
            else if (cell.IsEditing == true && e.Key == Key.Down)
            {
                //cancel edit, save current text, move cell
                cell.Content = "dummy";
                this.datagrid_eqp.CancelEdit();
                try
                {
                    this.datagrid_eqp.CurrentCell = new DataGridCellInfo(datagrid_eqp.Items[rowIndex + 1], datagrid_eqp.Columns[columnIndex]);
                }
                catch (System.ArgumentOutOfRangeException)
                {
                    //last row exception
                    this.datagrid_eqp.CurrentCell = new DataGridCellInfo(datagrid_eqp.Items[rowIndex], datagrid_eqp.Columns[columnIndex]);
                }
            }

        }

        

        private void datagrid_eqp_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.V && (Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
            {
                DataGrid dg = sender as DataGrid;
                DataGridCellInfo cellinfo = dg.CurrentCell;
                DataGridCell cell = Global_FunSet.TryToFindGridCell(dg, cellinfo, -1, -1);
                var y = dg.Columns.IndexOf(dg.CurrentColumn);
                var x = dg.Items.IndexOf(dg.CurrentItem);
                //row x
                //column y

                //붙여넣기 위치 가장 첫번째열, 인터락
                if (y != 0)
                {
                    MessageBox.Show("붙여넣기는 첫번째 열을 선택한 상태에서 해야 합니다.");
                    return;
                }

                DataTable dt_copy = Global_FunSet.MkClipboardToDataTable();                

                if (dt_copy.Columns.Count > dg.Columns.Count)
                {
                    MessageBox.Show("복사한 테이블 열 개수가 너무 많습니다.");
                    return;
                }                

                //붙여넣기
                foreach (DataRow row in dt_copy.Rows)
                {
                    for (int j = 0; j <= dt_copy.Columns.Count - 1; j++)
                    {
                        if (y > 5) y = 0;
                        dynamic temp_t = null;
                        //Debug.WriteLine("x:" + x + " / y:" + y);                        
                        cellinfo = new DataGridCellInfo(dg.Items[x], dg.Columns[y]);                                                
                        cell = Global_FunSet.TryToFindGridCell(dg, cellinfo, x, y);
                        try
                        {
                            temp_t = (TextBlock)cell.Content;
                        }
                        catch
                        {
                            temp_t = (TextBox)cell.Content;
                        }

                        //Debug.WriteLine(row[j].ToString());
                        temp_t.Text = row[j].ToString();

                        y = y + 1;
                    }
                    x = x + 1;
                }
            }            
        }

        private void btn_info_AddRows_Click(object sender, RoutedEventArgs e)
        {
            //멀티행 추가
            NewView.View_MultiRow view_MultiRow = new NewView.View_MultiRow();
            int multi_row = 0;

            if (view_MultiRow.ShowDialog() == true)
            {
                try
                {
                    multi_row = Int32.Parse(view_MultiRow.Text_MultiRow.Text);
                }
                catch
                {
                    MessageBox.Show("잘못된 입력 형식입니다.");
                    return;
                }

                for (int i = 0; i < multi_row; i++)
                {
                    AddrowDataGrid(this.datagrid_eqp);
                }
            }
        }

        private void datagrid_eqp_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            if (e.Row.GetIndex() + 1 == 1) e.Row.Header = "x";
            if (e.Row.GetIndex() + 1 == 2) e.Row.Header = "y";

            //e.Row.Header = (e.Row.GetIndex() + 1).ToString();
        }
    }
}
