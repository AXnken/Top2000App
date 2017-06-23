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
    /// <seealso cref="System.Windows.Window" />
    /// <seealso cref="System.Windows.Markup.IComponentConnector" />
    public partial class updateArtist : Window
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="updateArtist"/> class.
        /// </summary>
        /// <param name="artiestid">The artiestid.</param>
        /// <param name="artiestnaam">The artiestnaam.</param>
        /// <param name="biographie">The biographie.</param>
        /// <param name="foto">The foto.</param>
        /// <param name="url">The URL.</param>
        public updateArtist(string artiestid, string artiestnaam,string biographie,byte[] foto,string url)
        {
            InitializeComponent();
            //als de foto null is zorgt hij dat hij deze code skipt
            if (foto != null)
            { 
            BitmapImage biImg = new BitmapImage();
            MemoryStream ms = new MemoryStream(foto);
            biImg.BeginInit();
            biImg.StreamSource = ms;
            biImg.EndInit();
            ImageSource imgSrc = biImg as ImageSource;

            image.Source = imgSrc;
            }
            textBoxid.Text = artiestid;
            textBoxUpdateNaam.Text = artiestnaam;
            textBoxUpdateBio.Text = biographie;
            textBoxUpdateURL.Text = url;
        }

        #region dataconnectie methods
        /// <summary>
        /// Alters the artiest.
        /// </summary>
        /// <param name="artiestid">The artiestid.</param>
        /// <param name="artiestnaam">The artiestnaam.</param>
        /// <param name="biographie">The biographie.</param>
        /// <param name="url">The URL.</param>
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
        /// <summary>
        /// Handles the Click event of the buttonCancel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Handles the Click event of the buttonOpslaan control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void buttonOpslaan_Click(object sender, RoutedEventArgs e)
        {
            alterArtiest(textBoxid.Text,textBoxUpdateNaam.Text,textBoxUpdateBio.Text,textBoxUpdateURL.Text);
            this.Close();
        }
    }
}
