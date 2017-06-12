using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace Top2000App
{
    /// <summary>
    /// Interaction logic for FileInvoer.xaml
    /// </summary>
    public partial class FileInvoer : Window
    {
        public FileInvoer()
        {
            InitializeComponent();

            for (int i = DateTime.Today.Year; i <= DateTime.Today.Year + 20; i++)
            {
                comboBoxYearInvoer.Items.Add(string.Format("{0}", i));
            }
            comboBoxYearInvoer.SelectedIndex = comboBoxYearInvoer.Items.Count - 1;
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.ShowDialog();
            StreamReader sr = new StreamReader(ofd.OpenFile());
            labelFileName.Content = ofd.SafeFileName;
            while (sr.Peek() > 0)
            {
                string[] invoerItems = sr.ReadLine().Split(';');
            }
            sr.Close();
        }
    }
}
