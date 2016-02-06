using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using uMAD.Common;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Networking.Connectivity;
using Windows.UI;
using Windows.UI.Popups;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace uMAD
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();
        public MainPage()
        {
            this.InitializeComponent();
            checkInternet();
            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += this.NavigationHelper_LoadState;
            this.navigationHelper.SaveState += this.NavigationHelper_SaveState;
            Loaded += MainPage_Loaded;
        }

        private async void checkInternet()
        {
            if (!IsInternet())
            {
                MessageDialog dlg = new MessageDialog("Please make sure you are connected to the internet.");
                await dlg.ShowAsync();
                App.Current.Exit();
            }
        }

        public static bool IsInternet()
        {
            ConnectionProfile connections = NetworkInformation.GetInternetConnectionProfile();
            bool internet = connections != null && connections.GetNetworkConnectivityLevel() == NetworkConnectivityLevel.InternetAccess;
            return internet;
        }

        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            //StatusBar.GetForCurrentView().ForegroundColor = Colors.White;
            var bar = StatusBar.GetForCurrentView();
            bar.BackgroundColor = (Color)App.Current.Resources["MADColor"];
            bar.BackgroundOpacity = 1;
        }

        private void LoadedEvent(object sender, EventArgs e)
        {
            loadingRing.IsActive = false;
        }

        private void LoadingEvent(object sender, EventArgs e)
        {
            loadingRing.IsActive = true;
        }



        /// <summary>
        /// Gets the <see cref="NavigationHelper"/> associated with this <see cref="Page"/>.
        /// </summary>
        public NavigationHelper NavigationHelper
        {
            get { return this.navigationHelper; }
        }

        /// <summary>
        /// Gets the view model for this <see cref="Page"/>.
        /// This can be changed to a strongly typed view model.
        /// </summary>
        public ObservableDictionary DefaultViewModel
        {
            get { return this.defaultViewModel; }
        }

        /// <summary>
        /// Populates the page with content passed during navigation.  Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="sender">
        /// The source of the event; typically <see cref="NavigationHelper"/>
        /// </param>
        /// <param name="e">Event data that provides both the navigation parameter passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested and
        /// a dictionary of state preserved by this page during an earlier
        /// session.  The state will be null the first time a page is visited.</param>
        private void NavigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
        }

        /// <summary>
        /// Preserves state associated with this page in case the application is suspended or the
        /// page is discarded from the navigation cache.  Values must conform to the serialization
        /// requirements of <see cref="SuspensionManager.SessionState"/>.
        /// </summary>
        /// <param name="sender">The source of the event; typically <see cref="NavigationHelper"/></param>
        /// <param name="e">Event data that provides an empty dictionary to be populated with
        /// serializable state.</param>
        private void NavigationHelper_SaveState(object sender, SaveStateEventArgs e)
        {
        }

        #region NavigationHelper registration

        /// <summary>
        /// The methods provided in this section are simply used to allow
        /// NavigationHelper to respond to the page's navigation methods.
        /// <para>
        /// Page specific logic should be placed in event handlers for the  
        /// <see cref="NavigationHelper.LoadState"/>
        /// and <see cref="NavigationHelper.SaveState"/>.
        /// The navigation parameter is available in the LoadState method 
        /// in addition to page state preserved during an earlier session.
        /// </para>
        /// </summary>
        /// <param name="e">Provides data for navigation methods and event
        /// handlers that cannot cancel the navigation request.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedFrom(e);
        }

        #endregion

        private void navigationPivot_PivotItemLoaded(Pivot sender, PivotItemEventArgs args)
        {
            switch (navigationPivot.SelectedIndex)
            {
                case 0:
                    VisualStateManager.GoToState(this, "ScheduleState", true);
                    break;
                case 1:
                    VisualStateManager.GoToState(this, "TwitterState", true);
                    break;
                case 2:
                    VisualStateManager.GoToState(this, "SponsorState", true);
                    break;
            }
        }

        private void scheduleHeaderBlock_Tapped(object sender, TappedRoutedEventArgs e)
        {
            navigationPivot.SelectedIndex = 0;
        }

        private void Social_Tapped(object sender, TappedRoutedEventArgs e)
        {
            navigationPivot.SelectedIndex = 1;
        }

        private void sponsorsHeaderBlock_Tapped(object sender, TappedRoutedEventArgs e)
        {
            navigationPivot.SelectedIndex = 2;
        }

        private void AboutBtn_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(AboutPage));
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(LoginPage));
        }
    }
}
