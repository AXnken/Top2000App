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
    /// Interaction logic for Artist.xaml
    /// </summary>
    /// <seealso cref="System.Windows.Window" />
    /// <seealso cref="System.Windows.Markup.IComponentConnector" />
    public partial class Artist : Window
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Artist"/> class.
        /// </summary>
        public Artist()
        {
            InitializeComponent();
            try
            {
                /// <summary>
                /// Haalt alle artiesten op uit het database en stopt ze in de datagrid
                /// </summary>
                dataGridArt.ItemsSource = databaseMethodes.HaalAlleArtiesten().DefaultView;
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
        /// Handles the Click event of the btnDelArt control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void btnDelArt_Click(object sender, RoutedEventArgs e)
        {
            //checkt of de gebruiker meer dan 1 geselecteerd heeft en laat een foutmelding zien
            if (dataGridArt.SelectedItems.Count > 1)
            {
                MessageBox.Show("Let op: U heeft meer dan een artiest geselecteerd u mag er maar een selecteren", "Waarschuwing", MessageBoxButton.OK);
                return;
            }
            //checkt of de gebruiker wel wat geselecteerd heeft en laat een foutmelding zien
            else if (dataGridArt.SelectedItems.Count == 0)
            {
                MessageBox.Show("Let op: U heeft geen artiest geselecteerd u moet er een selecteren", "Waarschuwing", MessageBoxButton.OK);
            }
            //wanneer de gebruiker precies 1 heeft geselecteerd
            else if (dataGridArt.SelectedItems.Count == 1)
            {
                DataRowView datarow = (DataRowView)dataGridArt.SelectedItem;
                ArtistDelete ad = new ArtistDelete(Convert.ToString(datarow.Row.ItemArray[0]),Convert.ToString(datarow.Row.ItemArray[1]));
                ad.ShowDialog();
                try
                {
                    //voert de methode zoekArtiest uit met de inhoud van txtName omgezet naar lowercase
                    if (txtName.Text.Contains("'") || txtName.Text.ToLower().Contains("drop") || txtName.Text.ToLower().Contains("select") || txtName.Text.ToLower().Contains("delete") || txtName.Text.ToLower().Contains("insert") || txtName.Text.ToLower().Contains("alter"))
                    {
                        return;
                    }

                    try
                    {
                    dataGridArt.ItemsSource = databaseMethodes.ZoekArtiest(txtName.Text.TrimEnd().TrimStart().ToLower()).DefaultView;
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
                catch (Exception ex)
                {
                    if (ex is SqlException)
                    {
                        MessageBox.Show("Let op: er is iets miss gegaan bij het database", "Waarschuwing", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                    {
                        MessageBox.Show(ex.Message, ex.GetType().ToString(), MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }

        /// <summary>
        /// Handles the Click event of the btnNewArt control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void btnNewArt_Click(object sender, RoutedEventArgs e)
        {
            //nieuw instantie aangemaakt van ArtistNew
            ArtistNew an = new ArtistNew();
            //de window Artistnew wordt geopend
            an.ShowDialog();
        }

        /// <summary>
        /// Handles the SelectionChanged event of the txtName control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void txtName_SelectionChanged(object sender, RoutedEventArgs e)
        {
            try
            {
                //voert de methode zoekArtiest uit met de inhoud van txtName omgezet naar lowercase
                if (txtName.Text.Contains("'") || txtName.Text.ToLower().Contains("drop") || txtName.Text.ToLower().Contains("select") || txtName.Text.ToLower().Contains("delete") || txtName.Text.ToLower().Contains("insert") || txtName.Text.ToLower().Contains("alter"))
                {
                    return;
                }
                try
                { 
                dataGridArt.ItemsSource =  databaseMethodes.ZoekArtiest(txtName.Text.TrimEnd().TrimStart().ToLower()).DefaultView;
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
            catch(Exception ex)
            {
                if (ex is SqlException)
                {
                    MessageBox.Show("Let op: er is iets miss gegaan bij het database","Waarschuwing",MessageBoxButton.OK,MessageBoxImage.Error);
                }
                else
                {
                    MessageBox.Show(ex.Message, ex.GetType().ToString(), MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        /// <summary>
        /// Handles the Click event of the btnUpdateArt control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void btnUpdateArt_Click(object sender, RoutedEventArgs e)
        {
            //checkt of de gebruiker meer dan 1 geselecteerd heeft en laat een foutmelding zien
            if (dataGridArt.SelectedItems.Count > 1)
            {
                MessageBox.Show("Let op: U heeft meer dan een artiest geselecteerd u mag er maar een selecteren", "Waarschuwing", MessageBoxButton.OK);
                return;
            }
            //checkt of de gebruiker wel wat geselecteerd heeft en laat een foutmelding zien
            else if (dataGridArt.SelectedItems.Count == 0)
            {
                MessageBox.Show("Let op: U heeft geen artiest geselecteerd u moet er een selecteren", "Waarschuwing", MessageBoxButton.OK);
            }
            //wanneer de gebruiker precies 1 heeft geselecteerd
            else if (dataGridArt.SelectedItems.Count == 1)
            {
                DataRowView datarow = (DataRowView)dataGridArt.SelectedItem;
                //nieuw instantie aangemaakt van updateArtist
                updateArtist ua = new updateArtist(Convert.ToString(datarow.Row.ItemArray[0]), Convert.ToString(datarow.Row.ItemArray[1]), Convert.ToString(datarow.Row.ItemArray[2]),datarow.Row.ItemArray[3] == DBNull.Value ? null : (byte[])datarow.Row.ItemArray[3],  Convert.ToString(datarow.Row.ItemArray[4]));
                //de window updateArtist wordt geopend
                ua.ShowDialog();
                try
                {
                    //voert de methode zoekArtiest uit met de inhoud van txtName omgezet naar lowercase
                    if (txtName.Text.Contains("'") || txtName.Text.ToLower().Contains("drop") || txtName.Text.ToLower().Contains("select") || txtName.Text.ToLower().Contains("delete") || txtName.Text.ToLower().Contains("insert") || txtName.Text.ToLower().Contains("alter"))
                    {
                        return;
                    }
                    try
                    { 
                    dataGridArt.ItemsSource = databaseMethodes.ZoekArtiest(txtName.Text.TrimEnd().TrimStart().ToLower()).DefaultView;
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
                catch (Exception ex)
                {
                    //als het een een fout is aan database kant geeft hij een custom waarschuwing terug
                    if (ex is SqlException)
                    {
                        MessageBox.Show("Let op: er is iets miss gegaan bij het database", "Waarschuwing", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    //als er een fout komt dan geeft hij een foutmelding terug met het type fout
                    else
                    {
                        MessageBox.Show(ex.Message, ex.GetType().ToString(), MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }
    }


}
