using Datalayer;
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

        /// <summary>
        /// Button click om een foto te selecteren om hem bij de artiest te voegen
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                file = databaseMethodes.fotoToByte();
                labelFoto.Content = databaseMethodes.Filename;  
            }
            catch (SqlException sqlEx)
            {
                MessageBox.Show(sqlEx.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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
            try
            {
                //methode NewArtiestToevoegen wordt uitgevoerd
                databaseMethodes.NewArtiestToevoegen(textBoxNewNaam.Text, textBoxNewBio.Text, file, textBoxURL.Text);
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
                //de window wordt gesloten
                this.Close();
            }

        }
    }
}
