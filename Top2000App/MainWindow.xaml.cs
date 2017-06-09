using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Top2000App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            for (int i = 1999; i <= 2015; i++)
            {
                ComboYear.Items.Add(string.Format("{0}", i));
            }
            ComboYear.SelectedIndex =  ComboYear.Items.Count -1;

            DataConnection(ComboYear.SelectedItem.ToString());
        }
        

        private void btnEditArt_Click(object sender, RoutedEventArgs e)
        {
            Artist art = new Artist();
            art.Show();
        }

        

        private void btnManualImport_Click_1(object sender, RoutedEventArgs e)
        {
            ManualImport MaIm = new ManualImport();
            MaIm.Show();
        }

        private void ComboYear_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataConnection(ComboYear.SelectedItem.ToString());
        }

        #region Dataconnectie methode
        
        //methode maken voor automatische connectie
        public void DataConnection(string year)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(@"Server = (localdb)\mssqllocaldb;");
            sb.Append("Database = TOP2000;");
            sb.Append("User Id = i5ao1;");
            sb.Append("Password = test;");
            string sr = "";
            string cs = sb.ToString();
            SqlConnection conn = new SqlConnection(sr);
            SqlCommand cmd;
            conn.ConnectionString = cs;

            try
            {
                conn.Open();
                string st = string.Format("select * from Lijst where top2000jaar = {0}", year);
                cmd = new SqlCommand(st, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                DataTable table = new DataTable();
                table.Load(reader);
                dataGridTop2000.ItemsSource = table.DefaultView;
            }
            catch (SqlException sqlEx)
            {
                MessageBox.Show(sqlEx.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (conn.State != System.Data.ConnectionState.Closed)
                {
                    conn.Close();
                }
            }
        }
        #endregion
    }
}
