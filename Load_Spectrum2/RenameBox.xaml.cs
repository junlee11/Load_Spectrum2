using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// Interaction logic for RenameBox.xaml
    /// </summary>
    public partial class RenameBox : Window
    {
        public static string cur_exe;

        public RenameBox(string cur_filename, string file_exetension)
        {
            InitializeComponent();
            cur_exe = file_exetension;
            this.Text_Rename.Text = cur_filename;

            if (file_exetension == "폴더")
            {
                this.Text_Rename.SelectAll();
            }
            else
            {
                this.Text_Rename.Select(0, cur_filename.Length - file_exetension.Length);
            }
            this.Text_Rename.Focus();
            this.PreviewKeyDown += new KeyEventHandler(Key_Handler);
        }

        private void Key_Handler(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                this.DialogResult = false;
            try
            {
                if (e.Key == Key.Enter)
                {
                    if (cur_exe == "폴더")
                    {
                        this.DialogResult = true;
                    }

                    else if (cur_exe == this.Text_Rename.Text.Substring(this.Text_Rename.Text.LastIndexOf("."), this.Text_Rename.Text.Length - this.Text_Rename.Text.LastIndexOf(".")))
                    {
                        this.DialogResult = true;
                    }

                    //확장자를 바꿀 경우 경고창 띄우기
                    else
                    {
                        RenameWarn renamewarn = new RenameWarn();
                        if (renamewarn.ShowDialog() == true)
                        {
                            this.DialogResult = true;
                        }
                    }
                }
            }
            catch
            {
                RenameWarn renamewarn = new RenameWarn();
                if (renamewarn.ShowDialog() == true)
                {
                    this.DialogResult = true;
                }
            }
            
        }

        private void btn_yes_Click(object sender, RoutedEventArgs e)
        {
            if (cur_exe == "폴더")
            {
                this.DialogResult = true;
            }

            else if (cur_exe == this.Text_Rename.Text.Substring(this.Text_Rename.Text.LastIndexOf("."), this.Text_Rename.Text.Length - this.Text_Rename.Text.LastIndexOf(".")))
            {
                this.DialogResult = true;
            }

            //확장자를 바꿀 경우 경고창 띄우기
            else
            {
                RenameWarn renamewarn = new RenameWarn();
                if (renamewarn.ShowDialog() == true)
                {
                    this.DialogResult = true;
                }
            }
        }

        private void btn_cancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}
