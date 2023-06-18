using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Shapes;

namespace Load_Spectrum2
{
    /// <summary>
    /// Interaction logic for Lot_Search.xaml
    /// </summary>
    public partial class Lot_Search : Window
    {

        public static List<Control_Vis> source_LotSearch_Line = new List<Control_Vis>();
        public static List<Control_Vis> source_LotSearch_ListView = new List<Control_Vis>();

        public Lot_Search()
        {
            InitializeComponent();

            this.List_LotSearch_Line.DataContext = source_LotSearch_Line;
            this.List_LotSearch_Line.Items.SortDescriptions.Add(new SortDescription("strLine_Set1", ListSortDirection.Ascending));
        }
    }
}
