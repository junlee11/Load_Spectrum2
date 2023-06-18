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
using System.Windows.Shapes;

namespace Load_Spectrum2
{
    /// <summary>
    /// Interaction logic for NewOverRideChk.xaml
    /// </summary>
    public partial class NewOverRideChk : Window
    {
        public NewOverRideChk(string filename)
        {
            InitializeComponent();
            this.PreviewKeyDown += new KeyEventHandler(Key_Handler);
            this.main_txt.Text = this.main_txt.Text + " : " + filename;
            this.temp_txt.Text = "";
        }
        private void Key_Handler(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                this.DialogResult = false;

            if (e.Key == Key.Enter)
                this.DialogResult = true;
        }

        private void btn_yes_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void btn_cancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void btn_allyes_Click(object sender, RoutedEventArgs e)
        {
            this.temp_txt.Text = "all";
            this.DialogResult = true;
        }

        private void btn_cancel_Click_1(object sender, RoutedEventArgs e)
        {
            this.temp_txt.Text = "all";
            this.DialogResult = false;
        }
    }
}
