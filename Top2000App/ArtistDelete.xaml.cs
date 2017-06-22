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
    /// Interaction logic for ArtistDelete.xaml
    /// </summary>
    public partial class ArtistDelete : Window
    {
        public ArtistDelete(string artiestid, string artiestnaam)
        {
            InitializeComponent();
            textBoxArtiestID.Text = artiestid;
            textBoxArtiestnaam.Text = artiestnaam;
        }

        #region dataconectie methods
        public void VerwijderArtiest(string artiestNaam)
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
                string st = string.Format("exec dbo.VerwijderArtiest @artiestNaam =" +"'"+  artiestNaam + "'");
                cmd = new SqlCommand(st, conn);
                
                SqlDataReader reader = cmd.ExecuteReader();
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

        public bool CheckArtiest(string artiestNaam)
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
                string st = string.Format("Select naam from Artiest where naam = '" + artiestNaam +"'");
                cmd = new SqlCommand(st, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                return reader.HasRows;
            }
            catch (SqlException sqlEx)
            {
                MessageBox.Show(sqlEx.Message);
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
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

        private void button_Click(object sender, RoutedEventArgs e)
        {

            if(MessageBox.Show("Weet u zeker dat u deze artiest wilt verwijderen", "Waarschuwing",MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {
                VerwijderArtiest(textBoxArtiestnaam.Text);
                if (CheckArtiest(textBoxArtiestnaam.Text))
                {
                    MessageBox.Show("Let op: er is iets miss gegaan en de artiest is niet verwijdert. Vergeet niet dat je alleen artiesten kan verwijderen zonder liedjes", "Error", MessageBoxButton.OK);
                    this.Close();
                }
                else
                {
                    
                    MessageBox.Show("De artiest is succesvol verwijdert", "Verandering", MessageBoxButton.OK);
                    this.Close();
                }
            }
            else
            {
                this.Close();
            }
        }
    }
}
