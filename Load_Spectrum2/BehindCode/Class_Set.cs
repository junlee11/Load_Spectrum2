using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Load_Spectrum2
{
    #region class 모음
    //Xaml Binding Converter
    #region Listview WidthConverter

    


    public class WidthConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            //int columnsCount = System.Convert.ToInt32(parameter);
            double columnsCount = System.Convert.ToDouble(parameter);
            double width = (double)value;
            return width / columnsCount;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }

    //ListEI_EQPNameConverter

    public class ItemToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            //int columnsCount = System.Convert.ToInt32(parameter);
            if (value.ToString().Contains("EQP_path_all1"))
            {
                var cast_item = (EQP_path_all1)value;
                return cast_item.eqp_all_filename;
            }
            else if (value.ToString().Contains("EQP_path_all2"))
            {
                var cast_item = (EQP_path_all2)value;
                return cast_item.eqp_all_filename;
            }
            else if (value.ToString().Contains("Local_path_all1"))
            {
                var cast_item = (Local_path_all1)value;
                return cast_item.local_all_filename;
            }
            else
            {
                var cast_item = (Local_path_all2)value;
                return cast_item.local_all_filename;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
    #endregion

    //Local Class
    public class Local_path1
    {
        public string local_comment { get; set; }
        public string local_path { get; set; }
    }

    public class Local_path2
    {
        public string local_comment { get; set; }
        public string local_path { get; set; }
    }

    public class Local_path_all1 : INotifyPropertyChanged
    {
        public string _local_all_filename;
        public DateTime _local_all_date;
        public string _local_all_extension;
        public string _local_all_size;
        public string _local_all_src;
        public bool _IsNew;

        public bool IsNew
        {
            get
            {
                return _IsNew;
            }
            set
            {
                _IsNew = value;
                RaisePropertyChangedEvent("IsNew");
            }
        }

        public string local_all_filename
        {
            get
            {
                return _local_all_filename;
            }
            set
            {
                _local_all_filename = value;
                RaisePropertyChangedEvent("local_all_filename");
            }
        }

        public DateTime local_all_date
        {
            get
            {
                return _local_all_date;
            }
            set
            {
                _local_all_date = value;
                RaisePropertyChangedEvent("local_all_date");
            }
        }

        public string local_all_extension
        {
            get
            {
                return _local_all_extension;
            }
            set
            {
                _local_all_extension = value;
                RaisePropertyChangedEvent("local_all_extension");
            }
        }

        public string local_all_size
        {
            get
            {
                return _local_all_size;
            }
            set
            {
                _local_all_size = value;
                RaisePropertyChangedEvent("local_all_size");
            }
        }

        public string local_all_src
        {
            get
            {
                return _local_all_src;
            }
            set
            {
                _local_all_src = value;
                RaisePropertyChangedEvent("local_all_src");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        void RaisePropertyChangedEvent(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }

    public class Local_path_all2 : INotifyPropertyChanged
    {
        public string _local_all_filename;
        public DateTime _local_all_date;
        public string _local_all_extension;
        public string _local_all_size;
        public string _local_all_src;
        public bool _IsNew;

        public bool IsNew
        {
            get
            {
                return _IsNew;
            }
            set
            {
                _IsNew = value;
                RaisePropertyChangedEvent("IsNew");
            }
        }

        public string local_all_filename
        {
            get
            {
                return _local_all_filename;
            }
            set
            {
                _local_all_filename = value;
                RaisePropertyChangedEvent("local_all_filename");
            }
        }

        public DateTime local_all_date
        {
            get
            {
                return _local_all_date;
            }
            set
            {
                _local_all_date = value;
                RaisePropertyChangedEvent("local_all_date");
            }
        }

        public string local_all_extension
        {
            get
            {
                return _local_all_extension;
            }
            set
            {
                _local_all_extension = value;
                RaisePropertyChangedEvent("local_all_extension");
            }
        }

        public string local_all_size
        {
            get
            {
                return _local_all_size;
            }
            set
            {
                _local_all_size = value;
                RaisePropertyChangedEvent("local_all_size");
            }
        }

        public string local_all_src
        {
            get
            {
                return _local_all_src;
            }
            set
            {
                _local_all_src = value;
                RaisePropertyChangedEvent("local_all_src");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        void RaisePropertyChangedEvent(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }

    public class Line_text1 : INotifyPropertyChanged
    {
        public string _text_set1_local;

        public string text_set1_local
        {
            get
            {
                return _text_set1_local;
            }
            set
            {
                _text_set1_local = value;
                RaisePropertyChangedEvent("text_set1_local");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        void RaisePropertyChangedEvent(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }

    public class Line_text2 : INotifyPropertyChanged
    {
        public string _text_set2_local;

        public string text_set2_local
        {
            get
            {
                return _text_set2_local;
            }
            set
            {
                _text_set2_local = value;
                RaisePropertyChangedEvent("text_set2_local");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        void RaisePropertyChangedEvent(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }

    public class Line_text_eqp : INotifyPropertyChanged
    {
        public string _text_set1_eqp;
        public string _text_set2_eqp;

        public string text_set1_eqp
        {
            get
            {
                return _text_set1_eqp;
            }
            set
            {
                _text_set1_eqp = value;
                RaisePropertyChangedEvent("text_set1_eqp");
            }
        }

        public string text_set2_eqp
        {
            get
            {
                return _text_set2_eqp;
            }
            set
            {
                _text_set2_eqp = value;
                RaisePropertyChangedEvent("text_set2_eqp");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        void RaisePropertyChangedEvent(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }

    #region Listview LastWidthConverter
    public class LastWidthConverter : IValueConverter
    {
        public object Convert(object o, Type type, object parameter, CultureInfo culture)
        {
            ListView l = o as ListView;
            GridView g = l.View as GridView;
            double total = 0;
            for (int i = 0; i < g.Columns.Count - 1; i++)
            {
                total += g.Columns[i].Width;
            }
            
            return (l.ActualWidth - total);
        }

        public object ConvertBack(object o, Type type, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
    #endregion

    #region ListItemCntConverter : Count Seleted Items 
    public class ListItemCntConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((int)value == 0)
            {
                return null;
            }
            else
            {
                return value + "개 항목 선택함";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    #endregion

    #region ListAllItemCntConverter : Count All Items 
    public class ListAllItemCntConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value + "개 항목";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    #endregion

    public class CustomGrid : Grid
    {
        /// <summary>
        /// GetCellPadding
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public Thickness CellPadding
        {
            get
            {
                return (Thickness)this.GetValue(CellPaddingProperty);
            }
            set
            {
                this.SetValue(CellPaddingProperty, value);
            }
        }

        /// <summary>
        /// CellPadding 의존 속성
        /// </summary>
        public static readonly DependencyProperty CellPaddingProperty =
            DependencyProperty.Register("CellPadding", typeof(Thickness), typeof(CustomGrid),
            new FrameworkPropertyMetadata(new Thickness(0.0, 0.0, 0.0, 0.0), FrameworkPropertyMetadataOptions.AffectsArrange,
                OnCellPaddingChanged));

        static void OnCellPaddingChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs eventArgs)
        {
            CustomGrid grid = (dependencyObject as CustomGrid);
            foreach (UIElement uiElement in grid.Children)
            {
                ApplyMargin(grid, uiElement);
            }
        }

        // UIElementCollection 을 거칠 것 없이 곧바로 여기서 처리해도 됨.
        protected override void OnVisualChildrenChanged(DependencyObject visualAdded, DependencyObject visualRemoved)
        {
            FrameworkElement childElement = visualAdded as FrameworkElement;
            ApplyMargin(this, childElement);

            base.OnVisualChildrenChanged(visualAdded, visualRemoved);
        }

        public static void ApplyMargin(CustomGrid PaddingGrid, UIElement element)
        {
            FrameworkElement childElement = element as FrameworkElement;
            Thickness cellPadding = PaddingGrid.CellPadding;

            CustomGrid childPaddingGrid = element as CustomGrid;
            if (childPaddingGrid != null)
            {
                // 자식 노드가 PaddingGrid인 경우에는,
                // Margin 이 아닌 CellPadding을 설정한다.
                childPaddingGrid.CellPadding = cellPadding;
            }
            else
            {
                if (childElement != null)
                {
                    // 일반 자식 노드는 Margin을 설정
                    childElement.Margin = cellPadding;
                }
            }
        }



        #region OnRender
        //System.Windows.Media.Pen line = new System.Windows.Media.Pen(System.Windows.Media.Brushes.Black, 2);
        System.Windows.Media.Pen line = new System.Windows.Media.Pen(System.Windows.Media.Brushes.Black, 1);
        /// <summary>
        /// Border를 그리기 위한 OnRender 재정의
        /// </summary>
        /// <param name="dc"></param>
        protected override void OnRender(System.Windows.Media.DrawingContext dc)
        {

            base.OnRender(dc);

            CustomGrid customGrid = this.Parent as CustomGrid;
            if (customGrid == null)
            {

                dc.DrawRectangle(null, line, new Rect(0, 0, this.ActualWidth, this.ActualHeight));
            }

            double linePoint = 0;
            double posFrom = 0.0;
            double posTo = 0.0;

            int rowCount = Math.Max(this.RowDefinitions.Count, 1);
            int columnCount = Math.Max(this.ColumnDefinitions.Count, 1);

            bool[,] rowCellStatus;
            bool[,] columnCellStatus;

            GetRowLineCellStatus(rowCount, columnCount, out rowCellStatus, out columnCellStatus);

            if (this.ColumnDefinitions.Count != 0)
            {
                for (int row = 0; row < rowCount - 1; row++)
                {
                    var r = this.RowDefinitions[row];

                    linePoint += r.ActualHeight;
                    for (int column = 0; column < columnCount; column++)
                    {
                        bool drawLine = rowCellStatus[row + 1, column];
                        posTo += this.ColumnDefinitions[column].ActualWidth;

                        if (drawLine == true)
                        {
                            dc.DrawLine(line, new System.Windows.Point(posFrom, linePoint), new System.Windows.Point(posTo, linePoint));
                        }

                        posFrom = posTo;
                    }

                    posFrom = 0.0;
                    posTo = 0.0;
                }
            }

            linePoint = 0;
            posFrom = 0.0;
            posTo = 0.0;

            if (this.RowDefinitions.Count != 0)
            {
                for (int column = 0; column < columnCount - 1; column++)
                {
                    var r = this.ColumnDefinitions[column];

                    linePoint += r.ActualWidth;
                    for (int row = 0; row < rowCount; row++)
                    {
                        bool drawLine = columnCellStatus[row, column + 1];
                        posTo += this.RowDefinitions[row].ActualHeight;

                        if (drawLine == true)
                        {
                            dc.DrawLine(line, new System.Windows.Point(linePoint, posFrom), new System.Windows.Point(linePoint, posTo));
                        }

                        posFrom = posTo;
                    }

                    posTo = 0.0;
                    posFrom = 0.0;
                }
            }
        }

        private void GetRowLineCellStatus(int rowCount, int columnCount, out bool[,] rowCellStatus, out bool[,] columnCellStatus)
        {
            rowCellStatus = new bool[rowCount, columnCount];
            columnCellStatus = new bool[rowCount, columnCount];

            foreach (UIElement element in this.Children)
            {
                int row = Grid.GetRow(element);
                int column = Grid.GetColumn(element);

                int spanCount = Grid.GetColumnSpan(element);

                for (int span = 0; span < spanCount; span++)
                {
                    try
                    {
                        rowCellStatus[row, column + span] = true;
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(ex.Message);
                    }
                }

                spanCount = Grid.GetRowSpan(element);
                for (int span = 0; span < spanCount; span++)
                {
                    columnCellStatus[row + span, column] = true;
                }
            }
        }
        #endregion OnRender


    }

    //EQP Class
    public class EQP_Info_Line1 : INotifyPropertyChanged
    {

        public string _strLine_Set1;

        public string strLine_Set1
        {
            get
            {
                return _strLine_Set1;
            }
            set
            {
                _strLine_Set1 = value;
                RaisePropertyChangedEvent("strLine_Set1");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        void RaisePropertyChangedEvent(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
    public class EQP_Info_Line2 : INotifyPropertyChanged
    {
        public string _strLine_Set2;

        public string strLine_Set2
        {
            get
            {
                return _strLine_Set2;
            }
            set
            {
                _strLine_Set2 = value;
                RaisePropertyChangedEvent("strLine_Set2");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        void RaisePropertyChangedEvent(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }

    public class EQP_Info_Line3 : INotifyPropertyChanged
    {
        public string _strLine_Set3;

        public string strLine_Set3
        {
            get
            {
                return _strLine_Set3;
            }
            set
            {
                _strLine_Set3 = value;
                RaisePropertyChangedEvent("strLine_Set3");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        void RaisePropertyChangedEvent(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }

    public class EQP_Info_EQPTYPE1
    {
        public string strEQPTYPE_Set1 { get; set; }
    }
    public class EQP_Info_EQPTYPE2
    {
        public string strEQPTYPE_Set2 { get; set; }
    }
    public class EQP_Info_EQPTYPE3
    {
        public string strEQPTYPE_Set3 { get; set; }
    }

    public class EQP_Info_EQPMODEL1
    {
        public string strEQPMODEL_Set1 { get; set; }
    }
    public class EQP_Info_EQPMODEL2
    {
        public string strEQPMODEL_Set2 { get; set; }
    }
    public class EQP_Info_EQPMODEL3
    {
        public string strEQPMODEL_Set3 { get; set; }
    }

    public class EQP_Info_ZONE1
    {
        public string strZONE_Set1 { get; set; }
    }
    public class EQP_Info_ZONE2
    {
        public string strZONE_Set2 { get; set; }
    }
    public class EQP_Info_ZONE3
    {
        public string strZONE_Set3 { get; set; }
    }

    public class EQP_Info_EQPID1 : INotifyPropertyChanged
    {

        public string _strEQPID_Set1;

        public string strEQPID_Set1
        {
            get
            {
                return _strEQPID_Set1;
            }
            set
            {
                _strEQPID_Set1 = value;
                RaisePropertyChangedEvent("strEQPID_Set1");
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        void RaisePropertyChangedEvent(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
    public class EQP_Info_EQPID2 : INotifyPropertyChanged
    {
        public string _strEQPID_Set2;

        public string strEQPID_Set2
        {
            get
            {
                return _strEQPID_Set2;
            }
            set
            {
                _strEQPID_Set2 = value;
                RaisePropertyChangedEvent("strEQPID_Set2");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        void RaisePropertyChangedEvent(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
    public class EQP_Info_EQPID3 : INotifyPropertyChanged
    {
        public string _strEQPID_Set3;

        public string strEQPID_Set3
        {
            get
            {
                return _strEQPID_Set3;
            }
            set
            {
                _strEQPID_Set3 = value;
                RaisePropertyChangedEvent("strEQPID_Set3");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        void RaisePropertyChangedEvent(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }

    public class EQP_Info_EISelect : INotifyPropertyChanged
    {
        public string _strEQPID_EIselect;

        public string strEQPID_EIselect
        {
            get
            {
                return _strEQPID_EIselect;
            }
            set
            {
                _strEQPID_EIselect = value;
                RaisePropertyChangedEvent("strEQPID_EIselect");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        void RaisePropertyChangedEvent(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }

    public class EQP_path_all1 : INotifyPropertyChanged
    {
        public string _eqp_all_filename;
        public DateTime _eqp_all_date;
        public string _eqp_all_extension;
        public string _eqp_all_size;
        public string _eqp_all_src;
        public bool _IsNew;

        public bool IsNew
        {
            get
            {
                return _IsNew;
            }
            set
            {
                _IsNew = value;
                RaisePropertyChangedEvent("IsNew");
            }
        }

        public string eqp_all_filename
        {
            get
            {
                return _eqp_all_filename;
            }
            set
            {
                _eqp_all_filename = value;
                RaisePropertyChangedEvent("eqp_all_filename");
            }
        }

        public DateTime eqp_all_date
        {
            get
            {
                return _eqp_all_date;
            }
            set
            {
                _eqp_all_date = value;
                RaisePropertyChangedEvent("eqp_all_date");
            }
        }

        public string eqp_all_extension
        {
            get
            {
                return _eqp_all_extension;
            }
            set
            {
                _eqp_all_extension = value;
                RaisePropertyChangedEvent("eqp_all_extension");
            }
        }

        public string eqp_all_size
        {
            get
            {
                return _eqp_all_size;
            }
            set
            {
                _eqp_all_size = value;
                RaisePropertyChangedEvent("eqp_all_size");
            }
        }

        public string eqp_all_src
        {
            get
            {
                return _eqp_all_src;
            }
            set
            {
                _eqp_all_src = value;
                RaisePropertyChangedEvent("eqp_all_src");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        void RaisePropertyChangedEvent(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }

    public class EQP_path_all2 : INotifyPropertyChanged
    {
        public string _eqp_all_filename;
        public DateTime _eqp_all_date;
        public string _eqp_all_extension;
        public string _eqp_all_size;
        public string _eqp_all_src;
        public bool _IsNew;

        public bool IsNew
        {
            get
            {
                return _IsNew;
            }
            set
            {
                _IsNew = value;
                RaisePropertyChangedEvent("IsNew");
            }
        }

        public string eqp_all_filename
        {
            get
            {
                return _eqp_all_filename;
            }
            set
            {
                _eqp_all_filename = value;
                RaisePropertyChangedEvent("eqp_all_filename");
            }
        }

        public DateTime eqp_all_date
        {
            get
            {
                return _eqp_all_date;
            }
            set
            {
                _eqp_all_date = value;
                RaisePropertyChangedEvent("eqp_all_date");
            }
        }

        public string eqp_all_extension
        {
            get
            {
                return _eqp_all_extension;
            }
            set
            {
                _eqp_all_extension = value;
                RaisePropertyChangedEvent("eqp_all_extension");
            }
        }

        public string eqp_all_size
        {
            get
            {
                return _eqp_all_size;
            }
            set
            {
                _eqp_all_size = value;
                RaisePropertyChangedEvent("eqp_all_size");
            }
        }

        public string eqp_all_src
        {
            get
            {
                return _eqp_all_src;
            }
            set
            {
                _eqp_all_src = value;
                RaisePropertyChangedEvent("eqp_all_src");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        void RaisePropertyChangedEvent(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }

    public static class VisualTree
    {
        public static object GetParent(object child, Type parentType)
        {
            DependencyObject Item = child as DependencyObject;
            while (Item != null)
            {
                if (Item.GetType() == parentType)
                    return Item;

                Item = VisualTreeHelper.GetParent(Item);
            }
            return null;
        }
    }

    public class Control_Vis : INotifyPropertyChanged
    {
        //Main Stack
        private Visibility _Vstack1_local;
        public Visibility Vstack1_local
        {
            get { return _Vstack1_local; }
            set { _Vstack1_local = value; RaisePropertyChangedEvent("Vstack1_local"); }
        }
        private Visibility _Vstack2_local;
        public Visibility Vstack2_local
        {
            get { return _Vstack2_local; }
            set { _Vstack2_local = value; RaisePropertyChangedEvent("Vstack2_local"); }
        }
        private Visibility _Vstack1_eqp;
        public Visibility Vstack1_eqp
        {
            get { return _Vstack1_eqp; }
            set { _Vstack1_eqp = value; RaisePropertyChangedEvent("Vstack1_eqp"); }
        }
        private Visibility _Vstack2_eqp;
        public Visibility Vstack2_eqp
        {
            get { return _Vstack2_eqp; }
            set { _Vstack2_eqp = value; RaisePropertyChangedEvent("Vstack2_eqp"); }
        }

        //이미지뷰어 
        private Visibility _Vimg1_local;
        public Visibility Vimg1_local
        {
            get { return _Vimg1_local; }
            set { _Vimg1_local = value; RaisePropertyChangedEvent("Vimg1_local"); }
        }
        private Visibility _Vimg2_local;
        public Visibility Vimg2_local
        {
            get { return _Vimg2_local; }
            set { _Vimg2_local = value; RaisePropertyChangedEvent("Vimg2_local"); }
        }
        private Visibility _Vimg1_eqp;
        public Visibility Vimg1_eqp
        {
            get { return _Vimg1_eqp; }
            set { _Vimg1_eqp = value; RaisePropertyChangedEvent("Vimg1_eqp"); }
        }
        private Visibility _Vimg2_eqp;
        public Visibility Vimg2_eqp
        {
            get { return _Vimg2_eqp; }
            set { _Vimg2_eqp = value; RaisePropertyChangedEvent("Vimg2_eqp"); }
        }

        private Visibility _Vlocal2_path;
        public Visibility Vlocal2_path
        {
            get { return _Vlocal2_path; }
            set { _Vlocal2_path = value; RaisePropertyChangedEvent("Vlocal2_path"); }
        }

        private Visibility _VEQP2_path;
        public Visibility VEQP2_path
        {
            get { return _VEQP2_path; }
            set { _VEQP2_path = value; RaisePropertyChangedEvent("VEQP2_path"); }
        }

        private Visibility _canvas_vis;
        public Visibility canvas_vis
        {
            get { return _canvas_vis; }
            set { _canvas_vis = value; RaisePropertyChangedEvent("canvas_vis"); }
        }


        //이미지 소스 str
        private string _Vimg1_local_src;
        public string Vimg1_local_src
        {
            get { return _Vimg1_local_src; }
            set { _Vimg1_local_src = value; RaisePropertyChangedEvent("Vimg1_local_src"); }
        }
        private string _Vimg2_local_src;
        public string Vimg2_local_src
        {
            get { return _Vimg2_local_src; }
            set { _Vimg2_local_src = value; RaisePropertyChangedEvent("Vimg2_local_src"); }
        }
        private string _Vimg1_eqp_src;
        public string Vimg1_eqp_src
        {
            get { return _Vimg1_eqp_src; }
            set { _Vimg1_eqp_src = value; RaisePropertyChangedEvent("Vimg1_eqp_src"); }
        }
        private string _Vimg2_eqp_src;
        public string Vimg2_eqp_src
        {
            get { return _Vimg2_eqp_src; }
            set { _Vimg2_eqp_src = value; RaisePropertyChangedEvent("Vimg2_eqp_src"); }
        }

        //ListView rowspan
        private int _Vrowspan_local;
        public int Vrowspan_local
        {
            get { return _Vrowspan_local; }
            set { _Vrowspan_local = value; RaisePropertyChangedEvent("Vrowspan_local"); }
        }
        private int _Vrowspan_eqp;
        public int Vrowspan_eqp
        {
            get { return _Vrowspan_eqp; }
            set { _Vrowspan_eqp = value; RaisePropertyChangedEvent("Vrowspan_eqp"); }
        }
                
        //New Model
        //지침서 
        private string _mguide_ver;
        public string mguide_ver
        {
            get { return _mguide_ver; }
            set { _mguide_ver = value; RaisePropertyChangedEvent("mguide_ver"); }
        }
        private DateTime _mguide_date;
        public DateTime mguide_date
        {
            get { return _mguide_date; }
            set { _mguide_date = value; RaisePropertyChangedEvent("mguide_date"); }
        }

        //EQP Image (EI)
        private string _eqp_all_filename;
        public string eqp_all_filename
        {
            get { return _eqp_all_filename; }
            set { _eqp_all_filename = value; RaisePropertyChangedEvent("eqp_all_filename"); }
        }
        private DateTime _eqp_all_date;
        public DateTime eqp_all_date
        {
            get { return _eqp_all_date; }
            set { _eqp_all_date = value; RaisePropertyChangedEvent("eqp_all_date"); }
        }
        private string _eqp_all_size;
        public string eqp_all_size
        {
            get { return _eqp_all_size; }
            set { _eqp_all_size = value; RaisePropertyChangedEvent("eqp_all_size"); }
        }
        private string _eqp_all_extension;
        public string eqp_all_extension
        {
            get { return _eqp_all_extension; }
            set { _eqp_all_extension = value; RaisePropertyChangedEvent("eqp_all_extension"); }
        }
        private string _eqp_all_src;
        public string eqp_all_src
        {
            get { return _eqp_all_src; }
            set { _eqp_all_src = value; RaisePropertyChangedEvent("eqp_all_src"); }
        }


        //설비별 이미지
        private string _strEI_src1;
        public string strEI_src1
        {
            get { return _strEI_src1; }
            set { _strEI_src1 = value; RaisePropertyChangedEvent("strEI_src1"); }
        }
        private string _strEI_src2;
        public string strEI_src2
        {
            get { return _strEI_src2; }
            set { _strEI_src2 = value; RaisePropertyChangedEvent("strEI_src2"); }
        }
        private string _strEI_src3;
        public string strEI_src3
        {
            get { return _strEI_src3; }
            set { _strEI_src3 = value; RaisePropertyChangedEvent("strEI_src3"); }
        }
        private string _strEI_src4;
        public string strEI_src4
        {
            get { return _strEI_src4; }
            set { _strEI_src4 = value; RaisePropertyChangedEvent("strEI_src4"); }
        }
        private string _strEI_src5;
        public string strEI_src5
        {
            get { return _strEI_src5; }
            set { _strEI_src5 = value; RaisePropertyChangedEvent("strEI_src5"); }
        }
        private string _strEI_src6;
        public string strEI_src6
        {
            get { return _strEI_src6; }
            set { _strEI_src6 = value; RaisePropertyChangedEvent("strEI_src6"); }
        }
        private string _strEI_src7;
        public string strEI_src7
        {
            get { return _strEI_src7; }
            set { _strEI_src7 = value; RaisePropertyChangedEvent("strEI_src7"); }
        }
        private string _strEI_src8;
        public string strEI_src8
        {
            get { return _strEI_src8; }
            set { _strEI_src8 = value; RaisePropertyChangedEvent("strEI_src8"); }
        }
        private string _strEI_src9;
        public string strEI_src9
        {
            get { return _strEI_src9; }
            set { _strEI_src9 = value; RaisePropertyChangedEvent("strEI_src9"); }
        }
        private string _strEI_src10;
        public string strEI_src10
        {
            get { return _strEI_src10; }
            set { _strEI_src10 = value; RaisePropertyChangedEvent("strEI_src10"); }
        }
        private string _strEI_src11;
        public string strEI_src11
        {
            get { return _strEI_src11; }
            set { _strEI_src11 = value; RaisePropertyChangedEvent("strEI_src11"); }
        }
        private string _strEI_src12;
        public string strEI_src12
        {
            get { return _strEI_src12; }
            set { _strEI_src12 = value; RaisePropertyChangedEvent("strEI_src12"); }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        void RaisePropertyChangedEvent(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
    
    
    #endregion