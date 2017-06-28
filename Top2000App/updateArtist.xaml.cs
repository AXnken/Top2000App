using Datalayer;
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
        public updateArtist(string artiestid, string artiestnaam,string biographie,byte[] _foto,string url)
        {
            InitializeComponent();
            //als de foto null is zorgt hij dat hij deze code skipt
            if (_foto != null)
            {
            image.Source = databaseMethodes.byteToFoto(_foto);
            }
            textBoxid.Text = artiestid;
            textBoxUpdateNaam.Text = artiestnaam;
            textBoxUpdateBio.Text = biographie;
            textBoxUpdateURL.Text = url;
        }
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
            try
            {
                databaseMethodes.alterArtiest(textBoxid.Text, textBoxUpdateNaam.Text, textBoxUpdateBio.Text, textBoxUpdateURL.Text,databaseMethodes.fotoToByte());
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
                this.Close();
            }
        }

        private void button_MouseEnter(object sender, MouseEventArgs e)
        {
            buttonWijzigFoto.Opacity = 0.5;
        }

        private void buttonWijzigFoto_MouseLeave(object sender, MouseEventArgs e)
        {
            buttonWijzigFoto.Opacity = 0.0;
        }

        private void buttonWijzigFoto_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                image.Source = databaseMethodes.byteToFoto(databaseMethodes.fotoToByte());            
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString(), MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
    }
}
