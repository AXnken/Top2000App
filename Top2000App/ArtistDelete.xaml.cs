using Datalayer;
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
    /// Interaction logic for ArtistDelete.xaml
    /// </summary>
    /// <seealso cref="System.Windows.Window" />
    /// <seealso cref="System.Windows.Markup.IComponentConnector" />
    public partial class ArtistDelete : Window
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ArtistDelete"/> class.
        /// </summary>
        /// <param name="artiestid">The artiestid.</param>
        /// <param name="artiestnaam">The artiestnaam.</param>
        public ArtistDelete(string artiestid, string artiestnaam)
        {
            InitializeComponent();
            textBoxArtiestID.Text = artiestid;
            textBoxArtiestnaam.Text = artiestnaam;
        }
        /// <summary>
        /// Handles the Click event of the button control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void button_Click(object sender, RoutedEventArgs e)
        {

            if(MessageBox.Show("Weet u zeker dat u deze artiest wilt verwijderen", "Waarschuwing",MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {
                try
                {
                    databaseMethodes.VerwijderArtiest(textBoxArtiestnaam.Text);
                    if (databaseMethodes.CheckArtiest(textBoxArtiestnaam.Text))
                    {
                        MessageBox.Show("Let op: er is iets miss gegaan en de artiest is niet verwijdert. Vergeet niet dat je alleen artiesten kan verwijderen zonder liedjes", "Error", MessageBoxButton.OK);
                        this.Close();
                    }
                    else
                    {

                        MessageBox.Show("De artiest is succesvol verwijdert", "Verandering", MessageBoxButton.OK);
                        this.Close();
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
                    this.Close();
                }

            }
            else
            {
                this.Close();
            }
        }
    }
}
