using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Controls;

namespace Datalayer
{
    /// <summary>
    ///  een static class met alle methodes die nodig zijn voor de interactie met het database.
    /// </summary>
    public static class databaseMethodes
    {
        #region fields
        /// <summary>
        /// The filename
        /// </summary>
        private static string filename = "";
        /// <summary>
        /// The ofd
        /// </summary>
        private static OpenFileDialog ofd = new OpenFileDialog();
        #endregion

        #region methods
        /// <summary>
        /// Fulls the data grid.
        /// </summary>
        /// <param name="year">The year.</param>
        /// <returns></returns>
        public static DataTable fullDataGrid(string year)
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

            //connectie wordt geopend
            conn.Open();
            //sql command als string
            string st = string.Format("select Lijst.Positie , Song.Titel from Lijst inner join Song on Song.songid = Lijst.songid where top2000jaar = {0}", year);
            // cmd is een nieuwe sqlcommand die de connectiestring conn en de sql command st pakt
            cmd = new SqlCommand(st, conn);
            //hier is een sql data reader die de cmd die we net hadden aangemaakt uitvoert
            SqlDataReader reader = cmd.ExecuteReader();
            DataTable table = new DataTable();
            //de datatable wordt gelinked aan de reader 
            table.Load(reader);
            //als de connectie nog niet gesloten is sluit hij hem
            if (conn.State != System.Data.ConnectionState.Closed)
            {
                conn.Close();
            }
            return table;


        }

        /// <summary>
        /// Haals the alle jaar.
        /// </summary>
        /// <param name="M">The m.</param>
        public static void  HaalAlleJaar(ComboBox M)
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

            //connectie wordt geopend
            conn.Open();
            //sql command als string
            string st = string.Format("select distinct top2000jaar from Lijst order by top2000jaar");
            // cmd is een nieuwe sqlcommand die de connectiestring conn en de sql command st pakt
            cmd = new SqlCommand(st, conn);
            //hier is een sql data reader die de cmd die we net hadden aangemaakt uitvoert
            SqlDataReader reader = cmd.ExecuteReader();
            DataTable table = new DataTable();
            table.Load(reader);
            int length = table.Rows.Count;
            ComboBox ComboYear = M;
            for (int i = 0; i <= length -1; i++)
            {
                ComboYear.Items.Add(string.Format("{0}", table.Rows[i].ItemArray[0]));
            }
            if (conn.State != System.Data.ConnectionState.Closed)
            {
                conn.Close();
            }
        }

        /// <summary>
        /// Alters the artiest.
        /// </summary>
        /// <param name="artiestid">The artiestid.</param>
        /// <param name="artiestnaam">The artiestnaam.</param>
        /// <param name="biographie">The biographie.</param>
        /// <param name="url">The URL.</param>
        /// <param name="fotoArtist">The foto artist.</param>
        public static void alterArtiest(string artiestid, string artiestnaam, string biographie, string url,byte[] fotoArtist)
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

            //connectie wordt geopend
            conn.Open();
            //sql command als string
            string st = string.Format("exec AlterArtist @id= @idArtiest ,@naam= @naamArtiest ,@biografie=@biographieArtiest ,@foto = @NewFoto ,@url= @URLArtiest");
            // cmd is een nieuwe sqlcommand die de connectiestring conn en de sql command st pakt
            cmd = new SqlCommand(st, conn);
            cmd.Parameters.AddWithValue("idArtiest", artiestid);
            cmd.Parameters.AddWithValue("naamArtiest", artiestnaam);
            cmd.Parameters.AddWithValue("biographieArtiest", biographie);
            cmd.Parameters.AddWithValue("URLArtiest", url);
            cmd.Parameters.AddWithValue("NewFoto",fotoArtist);
            //hier is een sql data reader die de cmd die we net hadden aangemaakt uitvoert
            SqlDataReader reader = cmd.ExecuteReader();

            if (conn.State != System.Data.ConnectionState.Closed)
            {
                conn.Close();
            }

        }

        /// <summary>
        /// Bytes to foto.
        /// </summary>
        /// <param name="foto">The foto.</param>
        /// <returns></returns>
        public static ImageSource byteToFoto(byte[] foto)
        {

            BitmapImage biImg = new BitmapImage();
            MemoryStream ms = new MemoryStream(foto);
            biImg.BeginInit();
            biImg.StreamSource = ms;
            biImg.EndInit();
            ImageSource imgSrc = biImg as ImageSource;
            return imgSrc;
        }

        /// <summary>
        /// Haals the alle artiesten.
        /// </summary>
        /// <returns></returns>
        public static DataTable HaalAlleArtiesten()
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
                //connectie wordt geopend
                conn.Open();
                //sql command als string    
                string st = string.Format("select * from Artiest {0}", "");
                // cmd is een nieuwe sqlcommand die de connectiestring conn en de sql command st pakt
                cmd = new SqlCommand(st, conn);
                //hier is een sql data reader die de cmd die we net hadden aangemaakt uitvoert
                SqlDataReader reader = cmd.ExecuteReader();
                //hier wordt een datatable gemaakt die we nodig hebben om het resultaat van de sql command in een datagrid te stoppen
                DataTable table = new DataTable();
                //hier laad je de reader van net in het zojuist aangemaakte datatable
                table.Load(reader);
           
                if (conn.State != System.Data.ConnectionState.Closed)
                {
                    conn.Close();
                }
            return table;
        }

        /// <summary>
        /// Zoeks the artiest.
        /// </summary>
        /// <param name="artiest">The artiest.</param>
        /// <returns></returns>
        public static DataTable ZoekArtiest(string artiest)
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


                //connectie wordt geopend
                conn.Open();
                //sql command als string
                string st = string.Format("select * from Artiest where naam like {0}", "'%" + artiest + "%'");
                // cmd is een nieuwe sqlcommand die de connectiestring conn en de sql command st pakt
                cmd = new SqlCommand(st, conn);
                //hier is een sql data reader die de cmd die we net hadden aangemaakt uitvoert
                SqlDataReader reader = cmd.ExecuteReader();
                //hier wordt een datatable gemaakt die we nodig hebben om het resultaat van de sql command in een datagrid te stoppen
                DataTable table = new DataTable();
                //hier laad je de reader van net in het zojuist aangemaakte datatable
                table.Load(reader);
                if (conn.State != System.Data.ConnectionState.Closed)
                {
                    conn.Close();
                }
                return table;         
        }

        /// <summary>
        /// Verwijders the artiest.
        /// </summary>
        /// <param name="artiestNaam">The artiest naam.</param>
        public static void VerwijderArtiest(string artiestNaam)
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
                //connectie wordt geopend
                conn.Open();
                //sql command als string
                string st = string.Format("exec dbo.VerwijderArtiest @artiestNaam =" + "'" + artiestNaam + "'");
                // cmd is een nieuwe sqlcommand die de connectiestring conn en de sql command st pakt
                cmd = new SqlCommand(st, conn);
                //hier is een sql data reader die de cmd die we net hadden aangemaakt uitvoert
                SqlDataReader reader = cmd.ExecuteReader();
           
                if (conn.State != System.Data.ConnectionState.Closed)
                {
                    conn.Close();
                }
            
        }

        /// <summary>
        /// Checks the artiest.
        /// </summary>
        /// <param name="artiestNaam">The artiest naam.</param>
        /// <returns></returns>
        public static bool CheckArtiest(string artiestNaam)
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


                conn.Open();
                string st = string.Format("Select naam from Artiest where naam = '" + artiestNaam + "'");
                cmd = new SqlCommand(st, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                bool result = reader.HasRows;
                if (conn.State != System.Data.ConnectionState.Closed)
                {
                    conn.Close();
                }
                return result;
        }

        /// <summary>
        /// News the artiest toevoegen.
        /// </summary>
        /// <param name="naam">The naam.</param>
        /// <param name="bio">The bio.</param>
        /// <param name="binfoto">The binfoto.</param>
        /// <param name="url">The URL.</param>
        public static void NewArtiestToevoegen(string naam, string bio, object binfoto, string url)
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
                //connectie wordt geopend
                conn.Open();
                //sql command als string
                string st = string.Format("exec ToevoegenArtiest @naam = '" + naam + "', @bio ='" + bio + "' , @foto = @photofile, @url = '" + url + "'");
                // cmd is een nieuwe sqlcommand die de connectiestring conn en de sql command st pakt
                cmd = new SqlCommand(st, conn);
                //hier is een parameter aangemaakt om de binary van de foto toe te voegen aan de sql query
                cmd.Parameters.AddWithValue("photoFile", binfoto == null ? System.Data.SqlTypes.SqlBinary.Null : binfoto);
                //hier is een sql data reader die de cmd die we net hadden aangemaakt uitvoert
                SqlDataReader reader = cmd.ExecuteReader();

                if (conn.State != System.Data.ConnectionState.Closed)
                {
                    conn.Close();
                }

        }

        /// <summary>
        /// Fotoes to byte.
        /// </summary>
        /// <returns></returns>
        public static byte[] fotoToByte()
        {
            byte[] file;
            //opent een OpenFileDialog window
            ofd.ShowDialog();
            //string filename is de naam van het geselecteerde bestand
            Filename = ofd.SafeFileName;
            //niewe stream aangemaakt die het bestand is
            Stream stream = ofd.OpenFile();
            //een using wordt gebruikt om een binaryreader te kunnen gebruiken
            using (var Reader = new BinaryReader(stream))
            {
                //de byte array wordt gevuld met de bytes van de stream
                file = Reader.ReadBytes((int)stream.Length);
            }
            return file;
        }

        #endregion

        #region properties

        /// <summary>
        /// Gets or sets the filename.
        /// </summary>
        /// <value>
        /// The filename.
        /// </value>
        public static string Filename
        {
            get
            {
                return filename;
            }
            set
            {
                filename = value ;
            }
        }

        #endregion
    }

}


