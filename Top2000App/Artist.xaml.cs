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
using System.Windows.Shapes;

namespace Top2000App
{
    /// <summary>
    /// Interaction logic for Artist.xaml
    /// </summary>
    public partial class Artist : Window
    {
        public Artist()
        {
            InitializeComponent();
            HaalAlleArtiesten();
        }

        #region Dataconnectie methodes

        public void HaalAlleArtiesten()
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
                string st = string.Format("select * from Artiest {0}","");
                cmd = new SqlCommand(st, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                DataTable table = new DataTable();
                table.Load(reader);
                dataGridArt.ItemsSource = table.DefaultView;
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

        public void ZoekArtiest(string artiest)
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
                string st = string.Format("select * from Artiest where naam like {0}", "'" + "%" + artiest +"%" + "'");
                cmd = new SqlCommand(st, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                DataTable table = new DataTable();
                table.Load(reader);
                dataGridArt.ItemsSource = table.DefaultView;
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

        private void btnZoeken_Click(object sender, RoutedEventArgs e)
        {
            ZoekArtiest(txtName.Text.ToLower());
        }

        private void btnDelArt_Click(object sender, RoutedEventArgs e)
        {
            
            if (dataGridArt.SelectedItems.Count > 1)
            {
                MessageBox.Show("Let op: U heeft meer dan een artiest geselecteerd u mag er maar een selecteren", "Waarschuwing", MessageBoxButton.OK);
                return;
            }
            else if (dataGridArt.SelectedItems.Count == 0)
            {
                MessageBox.Show("Let op: U heeft geen artiest geselecteerd u moet er een selecteren", "Waarschuwing", MessageBoxButton.OK);
            }
            else if (dataGridArt.SelectedItems.Count == 1)
            {
                DataRowView datarow = (DataRowView)dataGridArt.SelectedItem;
                ArtistDelete ad = new ArtistDelete(Convert.ToString(datarow.Row.ItemArray[0]),Convert.ToString(datarow.Row.ItemArray[1]));
                ad.ShowDialog();
                ZoekArtiest(txtName.Text);
            }
            
        }
    }


}
