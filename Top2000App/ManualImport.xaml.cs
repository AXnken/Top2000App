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
    /// <seealso cref="System.Windows.Window" />
    /// <seealso cref="System.Windows.Markup.IComponentConnector" />
    public partial class ManualImport : Window
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ManualImport"/> class.
        /// </summary>
        public ManualImport()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Handles the Click event of the btnSubmit control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {

            CheckInput();
        }

        /// <summary>
        /// Checks the input and displays messagebox if the input is not right
        /// </summary>
        public void CheckInput()
        {
            int n;
            if(int.TryParse(txtPos.Text, out n))
            {
                if (int.TryParse(txtYear.Text, out n))
                {
                    if (int.TryParse(txtposYear.Text, out n))
                    {
                        DataConnection();
                    }
                    else
                    {
                        MessageBox.Show("Het ingevoerde waarde moet alleen cijfers bevatten", "Invoer error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    }
                }
                else
                {
                    MessageBox.Show("Het ingevoerde waarde moet alleen cijfers bevatten", "Invoer error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
            }
            else
            {
                MessageBox.Show("Het ingevoerde waarde moet alleen cijfers bevatten", "Invoer error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        #region Import Manual methode

        /// <summary>
        /// Imports a new row into the database.
        /// </summary>
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
            int posYear = Convert.ToInt32(txtposYear.Text);

            try
            {
                conn.Open();
               string st = string.Format("DECLARE @return_value int EXEC @return_value = [dbo].[checkArtist] @inputName = N'{0}' SELECT  'Return Value' = @return_value ", artistName);
                cmd = new SqlCommand(st, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                DataTable table = new DataTable();
                table.Load(reader);
                string item = table.Rows[0].ItemArray[0].ToString();
                // If the artist exists
                if (item == "1")
                {
                    conn.Open();
                    string stone = string.Format("EXEC[dbo].[importManual] @naam = N'{0}', @titel = N'{1}', @jaar = {2}, @tijd = N'{3}', @top2000jaar = {4}, @positie = {5} ", artistName, songTitel, songYear , dateTime, posYear, position);
                    cmd = new SqlCommand(st, conn);
                    SqlDataReader readerone = cmd.ExecuteReader();
                    DataTable tableone = new DataTable();
                    table.Load(reader);
                }
                // If the artist does not exist
                else {
                    conn.Open();
                    string sttwo = string.Format("EXEC[dbo].[importManual] @naam = N'{0}', @titel = N'{1}', @jaar = {2}, @tijd = N'{3}', @top2000jaar = {4}, @positie = {5}", artistName, songTitel, songYear , dateTime, posYear, position);
                    cmd = new SqlCommand(st, conn);
                    SqlDataReader readertwo = cmd.ExecuteReader();
                    DataTable tablet = new DataTable();
                    table.Load(reader);
                }
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

        #region txtbox clear and fill tips

        /// <summary>
        /// Handles the GotKeyboardFocus event of the txtPos control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="KeyboardFocusChangedEventArgs"/> instance containing the event data.</param>
        private void txtPos_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            txtPos.Clear();
        }

        /// <summary>
        /// Handles the GotKeyboardFocus event of the txtTitel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="KeyboardFocusChangedEventArgs"/> instance containing the event data.</param>
        private void txtTitel_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            txtTitel.Clear();
        }

        /// <summary>
        /// Handles the GotKeyboardFocus event of the txtArtistN control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="KeyboardFocusChangedEventArgs"/> instance containing the event data.</param>
        private void txtArtistN_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            txtArtistN.Clear();
        }

        /// <summary>
        /// Handles the GotKeyboardFocus event of the txtYear control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="KeyboardFocusChangedEventArgs"/> instance containing the event data.</param>
        private void txtYear_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            txtYear.Clear();
        }

        /// <summary>
        /// Handles the GotKeyboardFocus event of the txtTime control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="KeyboardFocusChangedEventArgs"/> instance containing the event data.</param>
        private void txtTime_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            txtTime.Clear();
        }

        /// <summary>
        /// Handles the GotKeyboardFocus event of the txtposYear control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="KeyboardFocusChangedEventArgs"/> instance containing the event data.</param>
        private void txtposYear_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            txtposYear.Clear();
        }

        /// <summary>
        /// Handles the IsKeyboardFocusedChanged event of the txtPos control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
        private void txtPos_IsKeyboardFocusedChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if(txtPos.Text == "")
            {
                txtPos.Text = "Position";
            }
        }

        /// <summary>
        /// Handles the IsKeyboardFocusedChanged event of the txtTitel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
        private void txtTitel_IsKeyboardFocusedChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if(txtTitel.Text == "")
            {
                txtTitel.Text = "Titel";
            }
        }

        /// <summary>
        /// Handles the IsKeyboardFocusedChanged event of the txtArtistN control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
        private void txtArtistN_IsKeyboardFocusedChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (txtArtistN.Text == "") {
                txtArtistN.Text = "Artist name";
            }
        }

        /// <summary>
        /// Handles the IsKeyboardFocusedChanged event of the txtYear control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
        private void txtYear_IsKeyboardFocusedChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if(txtYear.Text == "")
            {
                txtYear.Text = "Year of song";
            }
        }

        /// <summary>
        /// Handles the IsKeyboardFocusedChanged event of the txtposYear control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
        private void txtposYear_IsKeyboardFocusedChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if(txtposYear.Text == "")
            {
                txtposYear.Text = "Year of input ";
            }
        }

        /// <summary>
        /// Handles the IsKeyboardFocusedChanged event of the txtTime control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
        private void txtTime_IsKeyboardFocusedChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if(txtTime.Text == "")
            {
                txtTime.Text = "Time";
            }
        }

        #endregion
    }

}
