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
    /// Interaction logic for ManualImport.xaml
    /// </summary>
    public partial class ManualImport : Window
    {
        public ManualImport()
        {
            InitializeComponent();
        }        


        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            DataConnection();
        }

        #region Dataconnectie methode

        //methode maken voor automatische connectie
        public void DataConnection()
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

            int position = Convert.ToInt32(txtPos.Text);
            string songTitel = txtTitel.Text;
            string artistName = txtArtistN.Text;
            int songYear = Convert.ToInt32(txtYear.Text);
            string dateTime = txtTime.Text;

            try
            {
                conn.Open();
                string st = string.Format("BEGIN TRANSACTION DECLARE @POS int; DECLARE @SONGtitel nvarchar(100); DECLARE @ARTISTname nvarchar(100); DECLARE @SONGyear int; DECLARE @SONGtime datetime; SET @POS = { 0 }; SET @SONGtitel = { 1 }; SET @ARTISTname = { 2 }; SET @SONGyear = { 3 }; SET @SONGtime = { 4 }; INSERT INTO Lijst(positie) VALUES(@POS); INSERT INTO Song(titel, jaar, tijd) VALUES(@SONGtitel, @SONGyear, @SONGtime); INSERT INTO Artiest(naam) VALUES(@ARTISTname); COMMIT", position, songTitel, artistName, songYear, dateTime);
                cmd = new SqlCommand(st, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                DataTable table = new DataTable();
                table.Load(reader);
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