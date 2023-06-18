using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Load_Spectrum2
{

    public partial class MainWindow : Window
    {
        public void MainListSortAscending(ListView lv, string col_name, int set_flag)
        {
            if (col_name == "이름" && set_flag == 1) source_Local_path_all1.Sort(new SortAscending_Name());
            else if (col_name == "이름" && set_flag == 2) source_Local_path_all2.Sort(new SortAscending_Name());
            else if (col_name == "이름" && set_flag == 3) source_EQP_path_all1.Sort(new SortAscending_Name());
            else if (col_name == "이름" && set_flag == 4) source_EQP_path_all2.Sort(new SortAscending_Name());
            else if (col_name == "이름" && set_flag == 5) source_EQP_EI_all.Sort(new SortAscending_Name());

            if (col_name == "수정날짜" && set_flag == 1) source_Local_path_all1.Sort(new SortAscending_Date());
            else if (col_name == "수정날짜" && set_flag == 2) source_Local_path_all2.Sort(new SortAscending_Date());
            else if (col_name == "수정날짜" && set_flag == 3) source_EQP_path_all1.Sort(new SortAscending_Date());
            else if (col_name == "수정날짜" && set_flag == 4) source_EQP_path_all2.Sort(new SortAscending_Date());
            else if (col_name == "수정날짜" && set_flag == 5) source_EQP_EI_all.Sort(new SortAscending_Date());

            if (col_name == "유형" && set_flag == 1) source_Local_path_all1.Sort(new SortAscending_Type());
            else if (col_name == "유형" && set_flag == 2) source_Local_path_all2.Sort(new SortAscending_Type());
            else if (col_name == "유형" && set_flag == 3) source_EQP_path_all1.Sort(new SortAscending_Type());
            else if (col_name == "유형" && set_flag == 4) source_EQP_path_all2.Sort(new SortAscending_Type());
            else if (col_name == "유형" && set_flag == 5) source_EQP_EI_all.Sort(new SortAscending_Type());

            if (col_name == "크기" && set_flag == 1) source_Local_path_all1.Sort(new SortAscending_Size());
            else if (col_name == "크기" && set_flag == 2) source_Local_path_all2.Sort(new SortAscending_Size());
            else if (col_name == "크기" && set_flag == 3) source_EQP_path_all1.Sort(new SortAscending_Size());
            else if (col_name == "크기" && set_flag == 4) source_EQP_path_all2.Sort(new SortAscending_Size());
            else if (col_name == "크기" && set_flag == 5) source_EQP_EI_all.Sort(new SortAscending_Size());

            lv.Items.Refresh();
        }

        public void MainListSortDscending(ListView lv, string col_name, int set_flag)
        {
            if (col_name == "이름" && set_flag == 1) source_Local_path_all1.Sort(new SortDscending_Name());
            else if (col_name == "이름" && set_flag == 2) source_Local_path_all2.Sort(new SortDscending_Name());
            else if (col_name == "이름" && set_flag == 3) source_EQP_path_all1.Sort(new SortDscending_Name());
            else if (col_name == "이름" && set_flag == 4) source_EQP_path_all2.Sort(new SortDscending_Name());
            else if (col_name == "이름" && set_flag == 5) source_EQP_EI_all.Sort(new SortDscending_Name());

            if (col_name == "수정날짜" && set_flag == 1) source_Local_path_all1.Sort(new SortDscending_Date());
            else if (col_name == "수정날짜" && set_flag == 2) source_Local_path_all2.Sort(new SortDscending_Date());
            else if (col_name == "수정날짜" && set_flag == 3) source_EQP_path_all1.Sort(new SortDscending_Date());
            else if (col_name == "수정날짜" && set_flag == 4) source_EQP_path_all2.Sort(new SortDscending_Date());
            else if (col_name == "수정날짜" && set_flag == 5) source_EQP_EI_all.Sort(new SortDscending_Date());

            if (col_name == "유형" && set_flag == 1) source_Local_path_all1.Sort(new SortDscending_Type());
            else if (col_name == "유형" && set_flag == 2) source_Local_path_all2.Sort(new SortDscending_Type());
            else if (col_name == "유형" && set_flag == 3) source_EQP_path_all1.Sort(new SortDscending_Type());
            else if (col_name == "유형" && set_flag == 4) source_EQP_path_all2.Sort(new SortDscending_Type());
            else if (col_name == "유형" && set_flag == 5) source_EQP_EI_all.Sort(new SortDscending_Type());

            if (col_name == "크기" && set_flag == 1) source_Local_path_all1.Sort(new SortDscending_Size());
            else if (col_name == "크기" && set_flag == 2) source_Local_path_all2.Sort(new SortDscending_Size());
            else if (col_name == "크기" && set_flag == 3) source_EQP_path_all1.Sort(new SortDscending_Size());
            else if (col_name == "크기" && set_flag == 4) source_EQP_path_all2.Sort(new SortDscending_Size());
            else if (col_name == "크기" && set_flag == 5) source_EQP_EI_all.Sort(new SortDscending_Size());

            lv.Items.Refresh();
        }


        //예전 함수
        //칼럼 클릭하면 Sort
        #region function ListviewColumnSort All
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

    }

    //다운, 업로드, 새폴더 등으로 리스트뷰 아이템 추가될때 
    //추가된 아이템이 맨위
    public class SortDefault : IComparer<object>
    {
        public int Compare(object x, object y)
        {
            //폴더 - 파일이름 정렬
            dynamic str1_class = null;
            dynamic str2_class = null;
            int set_flag = 0;

            if (x.ToString().Contains("Local_path_all1"))
            {
                str1_class = (Local_path_all1)x;
                str2_class = (Local_path_all1)y;
                set_flag = 1;
            }
            else if (x.ToString().Contains("Local_path_all2"))
            {
                str1_class = (Local_path_all2)x;
                str2_class = (Local_path_all2)y;
                set_flag = 2;
            }
            else if (x.ToString().Contains("EQP_path_all1"))
            {
                str1_class = (EQP_path_all1)x;
                str2_class = (EQP_path_all1)y;
                set_flag = 3;
            }
            else if (x.ToString().Contains("EQP_path_all2"))
            {
                str1_class = (EQP_path_all2)x;
                str2_class = (EQP_path_all2)y;
                set_flag = 4;
            }
            else if (x.ToString().Contains("Control_Vis"))
            {
                str1_class = (Control_Vis)x;
                str2_class = (Control_Vis)y;
                set_flag = 5;
            }

            if (set_flag < 3)
            {
                //하나만 폴더인 경우
                if (str1_class.local_all_extension == "폴더" ^ str2_class.local_all_extension == "폴더")
                {
                    //폴더를 앞으로
                    return str1_class.local_all_extension == "폴더" ? -1 : 1;
                }
                else
                {
                    //둘다 폴더이거나 둘다 파일이면 이름순 정렬
                    return ((new CaseInsensitiveComparer()).Compare(str1_class.local_all_filename, str2_class.local_all_filename));
                }
            }
            else
            {
                //하나만 폴더인 경우
                if (str1_class.eqp_all_extension == "폴더" ^ str2_class.eqp_all_extension == "폴더")
                {
                    //폴더를 앞으로
                    return str1_class.eqp_all_extension == "폴더" ? -1 : 1;
                }
                else
                {
                    //둘다 폴더이거나 둘다 파일이면 이름순 정렬
                    return ((new CaseInsensitiveComparer()).Compare(str1_class.eqp_all_filename, str2_class.eqp_all_filename));
                }
            }
        }
    }

    public class SortAscending_Name : IComparer<object>
    {
        public int Compare(object x, object y)
        {
            //폴더 - 파일이름 정렬
            dynamic str1_class = null;
            dynamic str2_class = null;
            int set_flag = 0;

            if (x.ToString().Contains("Local_path_all1"))
            {
                str1_class = (Local_path_all1)x;
                str2_class = (Local_path_all1)y;
                set_flag = 1;
            }
            else if (x.ToString().Contains("Local_path_all2"))
            {
                str1_class = (Local_path_all2)x;
                str2_class = (Local_path_all2)y;
                set_flag = 2;
            }
            else if (x.ToString().Contains("EQP_path_all1"))
            {
                str1_class = (EQP_path_all1)x as EQP_path_all1;
                str2_class = (EQP_path_all1)y as EQP_path_all1;
                set_flag = 3;
            }
            else if (x.ToString().Contains("EQP_path_all2"))
            {
                str1_class = (EQP_path_all2)x;
                str2_class = (EQP_path_all2)y;
                set_flag = 4;
            }
            else if (x.ToString().Contains("Control_Vis"))
            {
                str1_class = (Control_Vis)x;
                str2_class = (Control_Vis)y;
                set_flag = 5;
            }

            if (set_flag < 3)
            {
                return ((new CaseInsensitiveComparer()).Compare(str1_class.local_all_filename, str2_class.local_all_filename));
            }
            else
            {
                return ((new CaseInsensitiveComparer()).Compare(str1_class.eqp_all_filename, str2_class.eqp_all_filename));
            }
        }
    }

    public class SortDscending_Name : IComparer<object>
    {
        public int Compare(object x, object y)
        {
            //폴더 - 파일이름 정렬
            dynamic str1_class = null;
            dynamic str2_class = null;
            int set_flag = 0;

            if (x.ToString().Contains("Local_path_all1"))
            {
                str1_class = (Local_path_all1)x;
                str2_class = (Local_path_all1)y;
                set_flag = 1;
            }
            else if (x.ToString().Contains("Local_path_all2"))
            {
                str1_class = (Local_path_all2)x;
                str2_class = (Local_path_all2)y;
                set_flag = 2;
            }
            else if (x.ToString().Contains("EQP_path_all1"))
            {
                str1_class = (EQP_path_all1)x as EQP_path_all1;
                str2_class = (EQP_path_all1)y as EQP_path_all1;
                set_flag = 3;
            }
            else if (x.ToString().Contains("EQP_path_all2"))
            {
                str1_class = (EQP_path_all2)x;
                str2_class = (EQP_path_all2)y;
                set_flag = 4;
            }
            else if (x.ToString().Contains("Control_Vis"))
            {
                str1_class = (Control_Vis)x;
                str2_class = (Control_Vis)y;
                set_flag = 5;
            }

            if (set_flag < 3)
            {
                return ((new CaseInsensitiveComparer()).Compare(str2_class.local_all_filename, str1_class.local_all_filename));
            }
            else
            {
                return ((new CaseInsensitiveComparer()).Compare(str2_class.eqp_all_filename, str1_class.eqp_all_filename));
            }
        }
    }

    public class SortAscending_Date : IComparer<object>
    {
        public int Compare(object x, object y)
        {
            //폴더 - 파일이름 정렬
            dynamic str1_class = null;
            dynamic str2_class = null;
            int set_flag = 0;

            if (x.ToString().Contains("Local_path_all1"))
            {
                str1_class = (Local_path_all1)x;
                str2_class = (Local_path_all1)y;
                set_flag = 1;
            }
            else if (x.ToString().Contains("Local_path_all2"))
            {
                str1_class = (Local_path_all2)x;
                str2_class = (Local_path_all2)y;
                set_flag = 2;
            }
            else if (x.ToString().Contains("EQP_path_all1"))
            {
                str1_class = (EQP_path_all1)x as EQP_path_all1;
                str2_class = (EQP_path_all1)y as EQP_path_all1;
                set_flag = 3;
            }
            else if (x.ToString().Contains("EQP_path_all2"))
            {
                str1_class = (EQP_path_all2)x;
                str2_class = (EQP_path_all2)y;
                set_flag = 4;
            }
            else if (x.ToString().Contains("Control_Vis"))
            {
                str1_class = (Control_Vis)x;
                str2_class = (Control_Vis)y;
                set_flag = 5;
            }

            if (set_flag < 3)
            {
                return ((new CaseInsensitiveComparer()).Compare(str1_class.local_all_date, str2_class.local_all_date));
            }
            else
            {
                return ((new CaseInsensitiveComparer()).Compare(str1_class.eqp_all_date, str2_class.eqp_all_date));
            }
        }
    }

    public class SortDscending_Date : IComparer<object>
    {
        public int Compare(object x, object y)
        {
            //폴더 - 파일이름 정렬
            dynamic str1_class = null;
            dynamic str2_class = null;
            int set_flag = 0;

            if (x.ToString().Contains("Local_path_all1"))
            {
                str1_class = (Local_path_all1)x;
                str2_class = (Local_path_all1)y;
                set_flag = 1;
            }
            else if (x.ToString().Contains("Local_path_all2"))
            {
                str1_class = (Local_path_all2)x;
                str2_class = (Local_path_all2)y;
                set_flag = 2;
            }
            else if (x.ToString().Contains("EQP_path_all1"))
            {
                str1_class = (EQP_path_all1)x as EQP_path_all1;
                str2_class = (EQP_path_all1)y as EQP_path_all1;
                set_flag = 3;
            }
            else if (x.ToString().Contains("EQP_path_all2"))
            {
                str1_class = (EQP_path_all2)x;
                str2_class = (EQP_path_all2)y;
                set_flag = 4;
            }
            else if (x.ToString().Contains("Control_Vis"))
            {
                str1_class = (Control_Vis)x;
                str2_class = (Control_Vis)y;
                set_flag = 5;
            }

            if (set_flag < 3)
            {
                return ((new CaseInsensitiveComparer()).Compare(str2_class.local_all_date, str1_class.local_all_date));
            }
            else
            {
                return ((new CaseInsensitiveComparer()).Compare(str2_class.eqp_all_date, str1_class.eqp_all_date));
            }
        }
    }

    public class SortAscending_Type : IComparer<object>
    {
        public int Compare(object x, object y)
        {
            //폴더 - 파일이름 정렬
            dynamic str1_class = null;
            dynamic str2_class = null;
            int set_flag = 0;

            if (x.ToString().Contains("Local_path_all1"))
            {
                str1_class = (Local_path_all1)x;
                str2_class = (Local_path_all1)y;
                set_flag = 1;
            }
            else if (x.ToString().Contains("Local_path_all2"))
            {
                str1_class = (Local_path_all2)x;
                str2_class = (Local_path_all2)y;
                set_flag = 2;
            }
            else if (x.ToString().Contains("EQP_path_all1"))
            {
                str1_class = (EQP_path_all1)x as EQP_path_all1;
                str2_class = (EQP_path_all1)y as EQP_path_all1;
                set_flag = 3;
            }
            else if (x.ToString().Contains("EQP_path_all2"))
            {
                str1_class = (EQP_path_all2)x;
                str2_class = (EQP_path_all2)y;
                set_flag = 4;
            }
            else if (x.ToString().Contains("Control_Vis"))
            {
                str1_class = (Control_Vis)x;
                str2_class = (Control_Vis)y;
                set_flag = 5;
            }

            if (set_flag < 3)
            {
                return ((new CaseInsensitiveComparer()).Compare(str1_class.local_all_extension, str2_class.local_all_extension));
            }
            else
            {
                return ((new CaseInsensitiveComparer()).Compare(str1_class.eqp_all_extension, str2_class.eqp_all_extension));
            }
        }
    }

    public class SortDscending_Type : IComparer<object>
    {
        public int Compare(object x, object y)
        {
            //폴더 - 파일이름 정렬
            dynamic str1_class = null;
            dynamic str2_class = null;
            int set_flag = 0;

            if (x.ToString().Contains("Local_path_all1"))
            {
                str1_class = (Local_path_all1)x;
                str2_class = (Local_path_all1)y;
                set_flag = 1;
            }
            else if (x.ToString().Contains("Local_path_all2"))
            {
                str1_class = (Local_path_all2)x;
                str2_class = (Local_path_all2)y;
                set_flag = 2;
            }
            else if (x.ToString().Contains("EQP_path_all1"))
            {
                str1_class = (EQP_path_all1)x as EQP_path_all1;
                str2_class = (EQP_path_all1)y as EQP_path_all1;
                set_flag = 3;
            }
            else if (x.ToString().Contains("EQP_path_all2"))
            {
                str1_class = (EQP_path_all2)x;
                str2_class = (EQP_path_all2)y;
                set_flag = 4;
            }
            else if (x.ToString().Contains("Control_Vis"))
            {
                str1_class = (Control_Vis)x;
                str2_class = (Control_Vis)y;
                set_flag = 5;
            }

            if (set_flag < 3)
            {
                return ((new CaseInsensitiveComparer()).Compare(str2_class.local_all_extension, str1_class.local_all_extension));
            }
            else
            {
                return ((new CaseInsensitiveComparer()).Compare(str2_class.eqp_all_extension, str1_class.eqp_all_extension));
            }
        }
    }

    public class SortAscending_Size : IComparer<object>
    {
        public int Compare(object x, object y)
        {
            //폴더 - 파일이름 정렬
            dynamic str1_class = null;
            dynamic str2_class = null;
            int set_flag = 0;

            if (x.ToString().Contains("Local_path_all1"))
            {
                str1_class = (Local_path_all1)x;
                str2_class = (Local_path_all1)y;
                set_flag = 1;
            }
            else if (x.ToString().Contains("Local_path_all2"))
            {
                str1_class = (Local_path_all2)x;
                str2_class = (Local_path_all2)y;
                set_flag = 2;
            }
            else if (x.ToString().Contains("EQP_path_all1"))
            {
                str1_class = (EQP_path_all1)x as EQP_path_all1;
                str2_class = (EQP_path_all1)y as EQP_path_all1;
                set_flag = 3;
            }
            else if (x.ToString().Contains("EQP_path_all2"))
            {
                str1_class = (EQP_path_all2)x;
                str2_class = (EQP_path_all2)y;
                set_flag = 4;
            }
            else if (x.ToString().Contains("Control_Vis"))
            {
                str1_class = (Control_Vis)x;
                str2_class = (Control_Vis)y;
                set_flag = 5;
            }

            if (set_flag < 3)
            {
                return ((new CaseInsensitiveComparer()).Compare(str1_class.local_all_size, str2_class.local_all_size));
            }
            else
            {
                return ((new CaseInsensitiveComparer()).Compare(str1_class.eqp_all_size, str2_class.eqp_all_size));
            }
        }
    }

    public class SortDscending_Size : IComparer<object>
    {
        public int Compare(object x, object y)
        {
            //폴더 - 파일이름 정렬
            dynamic str1_class = null;
            dynamic str2_class = null;
            int set_flag = 0;

            if (x.ToString().Contains("Local_path_all1"))
            {
                str1_class = (Local_path_all1)x;
                str2_class = (Local_path_all1)y;
                set_flag = 1;
            }
            else if (x.ToString().Contains("Local_path_all2"))
            {
                str1_class = (Local_path_all2)x;
                str2_class = (Local_path_all2)y;
                set_flag = 2;
            }
            else if (x.ToString().Contains("EQP_path_all1"))
            {
                str1_class = (EQP_path_all1)x as EQP_path_all1;
                str2_class = (EQP_path_all1)y as EQP_path_all1;
                set_flag = 3;
            }
            else if (x.ToString().Contains("EQP_path_all2"))
            {
                str1_class = (EQP_path_all2)x;
                str2_class = (EQP_path_all2)y;
                set_flag = 4;
            }
            else if (x.ToString().Contains("Control_Vis"))
            {
                str1_class = (Control_Vis)x;
                str2_class = (Control_Vis)y;
                set_flag = 5;
            }

            if (set_flag < 3)
            {
                return ((new CaseInsensitiveComparer()).Compare(str2_class.local_all_size, str1_class.local_all_size));
            }
            else
            {
                return ((new CaseInsensitiveComparer()).Compare(str2_class.eqp_all_size, str1_class.eqp_all_size));
            }
        }
    }

    public class SortStringNumber : IComparer<object>
    {
        public int Compare(object x, object y)
        {
            //폴더 - 파일이름 정렬
            var str_x = (Control_Vis)x;
            var str_y = (Control_Vis)y;
            int x1 = Int32.Parse(str_x.mguide_ver);
            int y1 = Int32.Parse(str_y.mguide_ver);

            if (x1 < y1) return 1;
            else return -1;
            
        }
    }


    public class NewCustomSort : IComparer
    {
        private string _text;
        public NewCustomSort(string text)
        {
            _text = text;
        }

        public int Compare(object x, object y)
        {
            dynamic str1_class = null;
            dynamic str2_class = null;
            int set_flag = 0;

            if (x.ToString().Contains("Local_path_all1"))
            {
                str1_class = (Local_path_all1)x;
                str2_class = (Local_path_all1)y;
                set_flag = 1;
            }
            else if (x.ToString().Contains("Local_path_all2"))
            {
                str1_class = (Local_path_all2)x;
                str2_class = (Local_path_all2)y;
                set_flag = 2;
            }
            else if (x.ToString().Contains("EQP_path_all1"))
            {
                str1_class = (EQP_path_all1)x as EQP_path_all1;
                str2_class = (EQP_path_all1)y as EQP_path_all1;
                set_flag = 3;
            }
            else if (x.ToString().Contains("EQP_path_all2"))
            {
                str1_class = (EQP_path_all2)x;
                str2_class = (EQP_path_all2)y;
                set_flag = 4;
            }

            if (set_flag < 3)
            {
                //하나만 새로운거면 앞으로
                if (str1_class.IsNew ^ str2_class.IsNew)
                {
                    return str1_class.IsNew ? -1 : 1;
                }
                //둘다 새로우면 폴더 - 파일 - 이름 순 정렬
                else if (str1_class.IsNew && str2_class.IsNew)
                {
                    if (str1_class.local_all_extension == "폴더" ^ str2_class.local_all_extension == "폴더")
                    {
                        return str1_class.local_all_extension == "폴더" ? 1 : -1;
                    }
                    else
                    {
                        return ((new CaseInsensitiveComparer()).Compare(str1_class.local_all_filename, str2_class.local_all_filename));
                    }
                }
                //둘다 예전꺼면 정렬 안함
                return 0;
            }
            else
            {
                //하나만 새로운거면 앞으로
                if (str1_class.IsNew ^ str2_class.IsNew)
                {
                    return str1_class.IsNew ? -1 : 1;
                }
                //둘다 새로우면 폴더 - 파일 - 이름 순 정렬
                else if (str1_class.IsNew && str2_class.IsNew)
                {
                    if (str1_class.eqp_all_extension == "폴더" ^ str2_class.eqp_all_extension == "폴더")
                    {
                        return str1_class.eqp_all_extension == "폴더" ? 1 : -1;
                    }
                    else
                    {
                        return ((new CaseInsensitiveComparer()).Compare(str1_class.eqp_all_filename, str2_class.eqp_all_filename));
                    }
                }
                //둘다 예전꺼면 정렬 안함
                return 0;
            }
        }
    }

    public class NormalCustomSort : IComparer
    {
        //private string _text;
        //public NormalCustomSort(string text)
        //{
        //    _text = text;
        //}

        public int Compare(object x, object y)
        {
            return 0;
        }
    }

    public class FTPDiaCustomSort : IComparer
    {
        //private string _text;
        //public NewCustomSort(string text)
        //{
        //    _text = text;
        //}

        public int Compare(object x, object y)
        {
            var cast_x = (NewModel.Model)x;
            var cast_y = (NewModel.Model)x;

            if (cast_x.ftp_all_extension == "폴더" ^ cast_y.ftp_all_extension == "폴더")
            {
                return cast_x.ftp_all_extension == "폴더" ? 1 : -1;
            }
            else
            {
                return ((new CaseInsensitiveComparer()).Compare(cast_x.ftp_all_filename, cast_y.ftp_all_filename));
            }
            return 0;
        }
    }

    public class Sort_CSVDate : IComparer<KeyValuePair<string, string>>
    {   
        public int Compare(KeyValuePair<string, string> x, KeyValuePair<string, string> y)
        {
            //KeyValuePair<string, string>
            var x1 = x.Value;
            var y1 = y.Value;

            var cast_x = x1.Substring(x1.IndexOf("-") + 1, x1.Length - x1.IndexOf("-") - 1).Replace(".csv", ""); 
            var cast_y = y1.Substring(y1.IndexOf("-") + 1, y1.Length - y1.IndexOf("-") - 1).Replace(".csv", "");

            DateTime date_x = DateTime.ParseExact(cast_x, "dd-MMM-yyyy HH.mm.ss", CultureInfo.InvariantCulture);
            DateTime date_y = DateTime.ParseExact(cast_y, "dd-MMM-yyyy HH.mm.ss", CultureInfo.InvariantCulture);

            return date_x.CompareTo(date_y);
        }
    }

}
#endregion