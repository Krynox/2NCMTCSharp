using ModelPort;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
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
    public sealed partial class LineupView : ProjectApp.Common.LayoutAwarePage
    {
        public ObservableCollection<LineUp> AllLineUp { get; set; }
        public LineupView()
        {
            this.InitializeComponent();
            AllLineUp = new ObservableCollection<LineUp>();
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
            // TODO: Assign a bindable collection of items to this.DefaultViewModel["Items"]
        }

        private void cboStage_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Stage stage = new Stage();
            DateTime date = new DateTime();
            if (cboStage.SelectedItem == null)
            {
                stage = (Stage)cboStage.Items[0];
                cboStage.SelectedItem = stage;
            }
            else {
                stage = (Stage)cboStage.SelectedItem;
            }

            if (cboDate.SelectedItem == null)
            {
                date = (DateTime)cboDate.Items[0];
                cboDate.SelectedItem = date;
            }
            else {
                date = (DateTime)cboDate.SelectedItem;
            }

            ObservableCollection<LineUp> FilteredBands = new ObservableCollection<LineUp>();
            if (AllLineUp.Count == null || AllLineUp.Count == 0)
            {
                foreach (object item in itemGridView.Items)
                {
                    AllLineUp.Add((LineUp)item);
                }
            }
            foreach (LineUp b in AllLineUp)
            {
                if (b.Stage.ID == stage.ID && b.Date == date)
                {
                    FilteredBands.Add(b);
                }
            }
            itemGridView.ItemsSource = FilteredBands;
        }

        private void HyperlinkButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(ItemsPage));
        }
    }
}
