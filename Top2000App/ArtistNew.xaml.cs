using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
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
    /// Interaction logic for ArtistNew.xaml
    /// </summary>
    /// <seealso cref="System.Windows.Window" />
    /// <seealso cref="System.Windows.Markup.IComponentConnector" />
    public partial class ArtistNew : Window
    {
        /// <summary>
        /// een byte array die gebruikt wordt om de foto om te zetten naar binary
        /// </summary>
        public byte[] file;
        /// <summary>
        /// Initializes a new instance of the <see cref="ArtistNew"/> class.
        /// </summary>
        public ArtistNew()
        {
            InitializeComponent();
            
        }

        #region Dataconnectie methodes
        /// <summary>
        /// Voegt een nieuwe artiest toe
        /// </summary>
        /// <param name="naam">The naam.</param>
        /// <param name="bio">The bio.</param>
        /// <param name="binfoto">The binfoto.</param>
        /// <param name="url">The URL.</param>
        public void NewArtiestToevoegen(string naam, string bio,object binfoto,string url)
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
                string st = string.Format("exec ToevoegenArtiest @naam = '"+ naam +"', @bio ='"+ bio +"' , @foto = @photofile, @url = '"+url+"'");
                // cmd is een nieuwe sqlcommand die de connectiestring conn en de sql command st pakt
                cmd = new SqlCommand(st, conn);
                //hier is een parameter aangemaakt om de binary van de foto toe te voegen aan de sql query
                cmd.Parameters.AddWithValue("photoFile", binfoto == null ? System.Data.SqlTypes.SqlBinary.Null : binfoto);
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
        /// Button click om een foto te selecteren om hem bij de artiest te voegen
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void button_Click(object sender, RoutedEventArgs e)
        {
            //maakt een nieuwe instantie van een OpenFileDialog
            OpenFileDialog ofd = new OpenFileDialog();
            //opent een OpenFileDialog window
            ofd.ShowDialog();
            //string filename is de naam van het geselecteerde bestand
            string filename = ofd.SafeFileName;
            //inhoud van de label wordt de naam van het bestand
            labelFoto.Content = filename;
            //niewe stream aangemaakt die het bestand is
            Stream stream = ofd.OpenFile();
            //een using wordt gebruikt om een binaryreader te kunnen gebruiken
            using (var Reader = new BinaryReader(stream))
            {
                //de byte array wordt gevuld met de bytes van de stream
                file = Reader.ReadBytes((int)stream.Length);
            }
                
        }

        /// <summary>
        /// Handles the Click event of the button2 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void button2_Click(object sender, RoutedEventArgs e)
        {
            //de window wordt gesloten
            this.Close();
        }

        /// <summary>
        /// Handles the Click event of the button1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            //methode NewArtiestToevoegen wordt uitgevoerd
            NewArtiestToevoegen(textBoxNewNaam.Text,textBoxNewBio.Text, file,textBoxURL.Text);
            //de window wordt gesloten
            this.Close();
        }
    }
}
