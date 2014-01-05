using ModelPort;
using ProjectApp.Data;
using ProjectApp.DataModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Display;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Items Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234233

namespace ProjectApp
{
    /// <summary>
    /// A page that displays a collection of item previews.  In the Split Application this page
    /// is used to display and select one of the available groups.
    /// </summary>
    public sealed partial class ItemsPage : ProjectApp.Common.LayoutAwarePage
    {
        public ItemsPage()
        {
            this.InitializeComponent();
            AllBand = new ObservableCollection<Band>();
        }
        private ObservableCollection<Band> _allBand;

        public ObservableCollection<Band> AllBand
        {
            get { return _allBand; }
            set { _allBand = value; }
        }
        

        /// <summary>
        /// Populates the page with content passed during navigation.  Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="navigationParameter">The parameter value passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested.
        /// </param>
        /// <param name="pageState">A dictionary of state preserved by this page during an earlier
        /// session.  This will be null the first time a page is visited.</param>
        protected override void LoadState(Object navigationParameter, Dictionary<String, Object> pageState)
        {

        }

        /// <summary>
        /// Invoked when an item is clicked.
        /// </summary>
        /// <param name="sender">The GridView (or ListView when the application is snapped)
        /// displaying the item clicked.</param>
        /// <param name="e">Event data that describes the item clicked.</param>
        void ItemView_ItemClick(object sender, ItemClickEventArgs e)
        {
            // Navigate to the appropriate destination page, configuring the new page
            // by passing required information as a navigation parameter
            Band clicked =(Band) e.ClickedItem;
            this.Frame.Navigate(typeof(BandDetail), clicked);
        }

        private void cboGenres_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Genre genre=(Genre)cboGenres.SelectedItem;
            ObservableCollection<Band> FilteredBands = new ObservableCollection<Band>();
            if (_allBand.Count == null || _allBand.Count == 0)
            {
                foreach (object item in itemGridView.Items)
                {
                    _allBand.Add((Band)item);
                }
            }
            foreach(Band b in _allBand)
            {
                foreach(Genre g in b.Genres)
                {
                    if (g.ID == genre.ID)
                    {
                        FilteredBands.Add(b);
                    }
                }
            }
            itemGridView.ItemsSource = FilteredBands;
        }

        private void HyperlinkButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(LineupView));
        }
    }
}
