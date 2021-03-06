﻿using Datalayer;
using Microsoft.Win32;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Top2000App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// <seealso cref="System.Windows.Window" />
    /// <seealso cref="System.Windows.Markup.IComponentConnector" />
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            //voert de methode haalallejaren op
            try
            {
                databaseMethodes.HaalAlleJaar(ComboYear);
            }
            catch (SqlException sqlEx)
            {
                MessageBox.Show(sqlEx.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            //geselecteerde item van combobox in de grote
            ComboYear.SelectedIndex = ComboYear.Items.Count - 1;

            //de methode fullDataGrid wordt uitgevoerd uit de class databseMethodes      
            try
            {
                dataGridTop2000.ItemsSource = databaseMethodes.fullDataGrid(ComboYear.SelectedItem.ToString()).DefaultView;
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
        /// Handles the Click event of the btnEditArt control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void btnEditArt_Click(object sender, RoutedEventArgs e)
        {
            // nieuwe instantie van artist wordt gemaakt
            Artist art = new Artist();
            //de window artist wordt geopend
            art.Show();
        }        



        /// <summary>
        /// Handles the 1 event of the btnManualImport_Click control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void btnManualImport_Click_1(object sender, RoutedEventArgs e)
        {
            // nieuwe instantie van ManualImport wordt gemaakt
            ManualImport MaIm = new ManualImport();
            //de window maualimport wordt geopend
            MaIm.Show();
        }

        /// <summary>
        /// Handles the SelectionChanged event of the ComboYear control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="SelectionChangedEventArgs"/> instance containing the event data.</param>
        private void ComboYear_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //de methode fullDataGrid wordt uitgevoerd uit de class databseMethodes 
            try
            {
                dataGridTop2000.ItemsSource = databaseMethodes.fullDataGrid(ComboYear.SelectedItem.ToString()).DefaultView;
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
        /// Handles the Click event of the btnAutoImport control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void btnAutoImport_Click(object sender, RoutedEventArgs e)
        {
            // nieuwe instantie van Fileinvoer wordt gemaakt
            FileInvoer fl = new FileInvoer();
            //de window fileinvoer wordt geopend
            fl.ShowDialog();
        }
    }
}
