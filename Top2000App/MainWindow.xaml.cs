using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Top2000App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// <seealso cref="System.Windows.Window" />
    /// <seealso cref="System.Windows.Markup.IComponentConnector" />
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            //voert de methode haalallejaren op
            HaalAlleJaar();

            #region Get latest year
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
                string st = string.Format("select max(top2000jaar) from Lijst");
                cmd = new SqlCommand(st, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                DataTable table = new DataTable();
                table.Load(reader);
                dataGridTop2000.ItemsSource = table.DefaultView;
                string item = table.Rows[0].ItemArray[0].ToString();
                for (int i = 1999; i <= Convert.ToInt32(item); i++)
                {
                    ComboYear.Items.Add(string.Format("{0}", i));
                }
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
            #endregion
            //geselecteerde item van combobox in de grote
            ComboYear.SelectedIndex =  ComboYear.Items.Count -1;
            //de methode dataconnection wordt uitgevoerd
            DataConnection(ComboYear.SelectedItem.ToString());
        }


        /// <summary>
        /// Handles the Click event of the btnEditArt control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void btnEditArt_Click(object sender, RoutedEventArgs e)
        {
            // nieuwe instantie van artist wordt gemaakt
            Artist art = new Artist();
            //de window artist wordt geopend
            art.Show();
        }        



        /// <summary>
        /// Handles the 1 event of the btnManualImport_Click control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void btnManualImport_Click_1(object sender, RoutedEventArgs e)
        {
            // nieuwe instantie van ManualImport wordt gemaakt
            ManualImport MaIm = new ManualImport();
            //de window maualimport wordt geopend
            MaIm.Show();
        }

        /// <summary>
        /// Handles the SelectionChanged event of the ComboYear control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="SelectionChangedEventArgs"/> instance containing the event data.</param>
        private void ComboYear_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //de methode dataconnection wordt uitgevoerd
            DataConnection(ComboYear.SelectedItem.ToString());

        }

        /// <summary>
        /// Handles the Click event of the btnAutoImport control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void btnAutoImport_Click(object sender, RoutedEventArgs e)
        {
            // nieuwe instantie van Fileinvoer wordt gemaakt
            FileInvoer fl = new FileInvoer();
            //de window fileinvoer wordt geopend
            fl.ShowDialog();
        }

        #region Dataconnectie methode

        //methode maken voor automatische connectie
        /// <summary>
        /// Datas the connection.
        /// </summary>
        /// <param name="year">The year.</param>
        public void DataConnection(string year)
        {
            //stringbuilder wordt gebruikt om de connectionstring op te bouwen
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
                //connectie wordt geopend
                conn.Open();
                //sql command als string
                string st = string.Format("select l.Positie,s.Titel from Lijst l inner join Song s on s.songid = l.songid where top2000jaar = {0}", year);
                // cmd is een nieuwe sqlcommand die de connectiestring conn en de sql command st pakt
                cmd = new SqlCommand(st, conn);
                //hier is een sql data reader die de cmd die we net hadden aangemaakt uitvoert
                SqlDataReader reader = cmd.ExecuteReader();
                DataTable table = new DataTable();
                //de datatable wordt gelinked aan de reader 
                table.Load(reader);
                //de inhoud van de van de datagrid wordt gelinked met de datatable
                dataGridTop2000.ItemsSource = table.DefaultView;
                string item = (table.Rows.Count - 1).ToString();


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

        public void HaalAlleJaar()
        {
            //stringbuilder wordt gebruikt om de connectionstring op te bouwen
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
                //connectie wordt geopend
                conn.Open();
                //sql command als string
                string st = string.Format("select distinct top2000jaar from Lijst");
                // cmd is een nieuwe sqlcommand die de connectiestring conn en de sql command st pakt
                cmd = new SqlCommand(st, conn);
                //hier is een sql data reader die de cmd die we net hadden aangemaakt uitvoert
                SqlDataReader reader = cmd.ExecuteReader();
                //voegt de inhoud van de reader aan de combobox toe
                ComboYear.Items.Add(string.Format("{1}", reader));

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
