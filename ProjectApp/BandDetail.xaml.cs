using ModelPort;
using ProjectApp.DataModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage.Streams;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Item Detail Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234232

namespace ProjectApp
{
    /// <summary>
    /// A page that displays details for a single item within a group while allowing gestures to
    /// flip through other items belonging to the same group.
    /// </summary>
    public sealed partial class BandDetail : ProjectApp.Common.LayoutAwarePage
    {
        public BandDetail()
        {
            this.InitializeComponent();
        }
        private Band _bandform;

        public Band Bandform
        {
            get { return _bandform; }
            set { _bandform = value; }
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
            // Allow saved page state to override the initial item to display
            if (pageState != null && pageState.ContainsKey("SelectedItem"))
            {
                navigationParameter = pageState["SelectedItem"];
            }
            //this.DefaultViewModel["Group"] = navigationParameter.Group;
            //this.DefaultViewModel["Items"] = blist;
            //this.flipView.SelectedItem = (Band)navigationParameter;
            //var item = SampleDataSource.GetItem((String)navigationParameter);
            //this.DefaultViewModel["Group"] = item.Group;
            //this.DefaultViewModel["Items"] = item.Group.Items;
            //this.flipView.SelectedItem = item;
            // TODO: Assign a bindable group to this.DefaultViewModel["Group"]
            // TODO: Assign a collection of bindable items to this.DefaultViewModel["Items"]
            // TODO: Assign the selected item to this.flipView.SelectedItem
            Bandform =(Band)navigationParameter;
            this.DefaultViewModel["BandImage"] = Bandform.Picture;
            txtNaam.Text = Bandform.Name;
            txtDesc.Text = Bandform.Description;

        }
        private void Facebook(object sender, RoutedEventArgs e)
        {
            Launcher.LaunchUriAsync(new Uri("http://" + Bandform.Facebook));
        }

        private void Twitter(object sender, RoutedEventArgs e)
        {
            Launcher.LaunchUriAsync(new Uri("http://" + Bandform.Twitter));
        }
        /// <summary>
        /// Preserves state associated with this page in case the application is suspended or the
        /// page is discarded from the navigation cache.  Values must conform to the serialization
        /// requirements of <see cref="SuspensionManager.SessionState"/>.
        /// </summary>
        /// <param name="pageState">An empty dictionary to be populated with serializable state.</param>
        protected override void SaveState(Dictionary<String, Object> pageState)
        {
            //var selectedItem = this.flipView.SelectedItem;
            // TODO: Derive a serializable navigation parameter and assign it to pageState["SelectedItem"]
        }
    }
}
