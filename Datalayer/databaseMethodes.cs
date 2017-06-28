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

namespace Datalayer
{
    public static class databaseMethodes
    {
        #region fields
        private static string filename = "";
        #endregion

        #region methods
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
            string st = string.Format("select l.Positie,s.Titel from Lijst l inner join Song s on s.songid = l.songid where top2000jaar = {0}", year);
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

        public static DataTable HaalAlleJaar()
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
            string st = string.Format("select distinct top2000jaar from Lijst");
            // cmd is een nieuwe sqlcommand die de connectiestring conn en de sql command st pakt
            cmd = new SqlCommand(st, conn);
            //hier is een sql data reader die de cmd die we net hadden aangemaakt uitvoert
            SqlDataReader reader = cmd.ExecuteReader();
            DataTable table = new DataTable();
            table.Load(reader);

            if (conn.State != System.Data.ConnectionState.Closed)
            {
                conn.Close();
            }

            return table;
        }

        public static void alterArtiest(string artiestid, string artiestnaam, string biographie, string url)
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
            string st = string.Format("exec AlterArtist @id= @idArtiest ,@naam= @naamArtiest ,@biografie=@biographieArtiest ,@foto = null ,@url= @URLArtiest");
            // cmd is een nieuwe sqlcommand die de connectiestring conn en de sql command st pakt
            cmd = new SqlCommand(st, conn);
            cmd.Parameters.AddWithValue("idArtiest", artiestid);
            cmd.Parameters.AddWithValue("naamArtiest", artiestnaam);
            cmd.Parameters.AddWithValue("biographieArtiest", biographie);
            cmd.Parameters.AddWithValue("URLArtiest", url);
            //hier is een sql data reader die de cmd die we net hadden aangemaakt uitvoert
            SqlDataReader reader = cmd.ExecuteReader();

            if (conn.State != System.Data.ConnectionState.Closed)
            {
                conn.Close();
            }

        }

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

        public static byte[] fotoToByte()
        {
            byte[] file;
            //maakt een nieuwe instantie van een OpenFileDialog
            OpenFileDialog ofd = new OpenFileDialog();
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


