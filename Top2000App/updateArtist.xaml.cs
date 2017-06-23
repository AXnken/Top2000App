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
using System.Windows.Shapes;

namespace Top2000App
{
    /// <summary>
    /// Interaction logic for updateArtist.xaml
    /// </summary>
    public partial class updateArtist : Window
    {
        public updateArtist(string artiestid, string artiestnaam,string biographie,byte[] foto,string url)
        {
            InitializeComponent();

            BitmapImage biImg = new BitmapImage();
            MemoryStream ms = new MemoryStream(foto);
            biImg.BeginInit();
            biImg.StreamSource = ms;
            biImg.EndInit();
            ImageSource imgSrc = biImg as ImageSource;

            image.Source = imgSrc;

            textBoxid.Text = artiestid;
            textBoxUpdateNaam.Text = artiestnaam;
            textBoxUpdateBio.Text = biographie;
            textBoxUpdateURL.Text = url;
        }

        #region dataconnectie methods
        public void alterArtiest(string artiestid, string artiestnaam, string biographie, string url)
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
                string st = string.Format("exec AlterArtist @id= @idArtiest ,@naam= @naamArtiest ,@biografie=@biographieArtiest ,@foto = null ,@url= @URLArtiest");               
                // cmd is een nieuwe sqlcommand die de connectiestring conn en de sql command st pakt
                cmd = new SqlCommand(st, conn);
                cmd.Parameters.AddWithValue("idArtiest",artiestid);
                cmd.Parameters.AddWithValue("naamArtiest", artiestnaam);
                cmd.Parameters.AddWithValue("biographieArtiest",biographie);
                cmd.Parameters.AddWithValue("URLArtiest",url);
                //hier is een sql data reader die de cmd die we net hadden aangemaakt uitvoert
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
        #endregion
        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void buttonOpslaan_Click(object sender, RoutedEventArgs e)
        {
            alterArtiest(textBoxid.Text,textBoxUpdateNaam.Text,textBoxUpdateBio.Text,textBoxUpdateURL.Text);
            this.Close();
        }
    }
}
