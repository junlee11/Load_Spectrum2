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

namespace Load_Spectrum2.NewView
{
    /// <summary>
    /// Interaction logic for ViewPassword.xaml
    /// </summary>
    public partial class ViewPassword : Window
    {
        public ViewPassword()
        {
            InitializeComponent();
            this.Text_pw.Focus();
            this.PreviewKeyDown += new KeyEventHandler(Key_Handler);
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
    }
}
