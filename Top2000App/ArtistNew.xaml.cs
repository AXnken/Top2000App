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
    public partial class ArtistNew : Window
    {
        public byte[] file;
        public ArtistNew()
        {
            InitializeComponent();
            
        }

        #region Dataconnectie methodes
        public void NewArtiestToevoegen(string naam, string bio,object binfoto,string url)
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
                string st = string.Format("exec ToevoegenArtiest @naam = '"+ naam +"', @bio ='"+ bio +"' , @foto = @photofile, @url = '"+url+"'");
                cmd = new SqlCommand(st, conn);
                cmd.Parameters.AddWithValue("photoFile", binfoto == null ? System.Data.SqlTypes.SqlBinary.Null : binfoto);
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

        private void button_Click(object sender, RoutedEventArgs e)
        {
            
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.ShowDialog();
            string filename = ofd.SafeFileName;
            labelFoto.Content = filename;
            Stream stream = ofd.OpenFile();
            using (var Reader = new BinaryReader(stream))
            {
                file = Reader.ReadBytes((int)stream.Length);
            }
                
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            NewArtiestToevoegen(textBoxNewNaam.Text,textBoxNewBio.Text, file,textBoxURL.Text);
            this.Close();
        }
    }
}
