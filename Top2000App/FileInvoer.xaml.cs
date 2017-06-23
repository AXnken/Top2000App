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
    /// <seealso cref="System.Windows.Window" />
    /// <seealso cref="System.Windows.Markup.IComponentConnector" />
    public partial class FileInvoer : Window
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FileInvoer"/> class.
        /// </summary>
        public FileInvoer()
        {
            InitializeComponent();

            for (int i = DateTime.Today.Year; i <= DateTime.Today.Year + 20; i++)
            {
                comboBoxYearInvoer.Items.Add(string.Format("{0}", i));
            }
            comboBoxYearInvoer.SelectedIndex = comboBoxYearInvoer.Items.Count - 1;
        }

        /// <summary>
        /// Handles the Click event of the button control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
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
