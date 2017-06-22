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
                string st = string.Format("USE [TOP2000] GO EXEC[dbo].[importManual] @naam = N'{0}', @titel = N'{1}', @jaar = {2}, @tijd = N'{3}',	@top2000jaar = {4}, @positie = {5} GO", artistName, songTitel, dateTime , songYear, position);
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