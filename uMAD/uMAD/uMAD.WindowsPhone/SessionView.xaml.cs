using uMAD.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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
using uMAD.Data;
using Windows.System;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace uMAD
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SessionView : Page
    {
        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();
        public SessionView()
        {
            this.InitializeComponent();

            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += this.NavigationHelper_LoadState;
            this.navigationHelper.SaveState += this.NavigationHelper_SaveState;
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
            navigationHelper.OnNavigatedTo(e);
            if (ScheduleSession.CurrentSession == null)
                Frame.GoBack();
            //this.DataContext = ScheduleSession.CurrentSession;
            LoadTwitter();
        }

        private void LoadTwitter()
        {
            if (string.IsNullOrEmpty(ScheduleSession.CurrentSession?.Company?.TwitterHandle))
                return;
            var twitterControl = new TwitterFeedUserControl() { Handle = ScheduleSession.CurrentSession.Company.TwitterHandle };
            twitterControl.LoadingTweets += TwitterControl_LoadingTweets;
            twitterControl.LoadedTweets += TwitterControl_LoadedTweets;
            sessionPivot.Items.Add(new PivotItem() { Margin = new Thickness(0), Header = ScheduleSession.CurrentSession.Company.TwitterHandle, Content = twitterControl });
        }

        private void TwitterControl_LoadedTweets(object sender, EventArgs e)
        {
            loadingRing.IsActive = false;
        }

        private void TwitterControl_LoadingTweets(object sender, EventArgs e)
        {
            loadingRing.IsActive = true;
        }

        private async void urlBlock_Tapped(object sender, TappedRoutedEventArgs e)
        {
            await Launcher.LaunchUriAsync(new Uri(ScheduleSession.CurrentSession.Company.WebsiteUrl));
        }

        private async void emailBlock_Tapped(object sender, TappedRoutedEventArgs e)
        {
            await Launcher.LaunchUriAsync(new Uri("mailto:" + ScheduleSession.CurrentSession.Email));
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedFrom(e);
        }

        #endregion


       
        // Using a DependencyProperty as the backing store for IsFavorite.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsFavoriteProperty =
            DependencyProperty.Register("IsFavorite", typeof(bool), typeof(SessionView), new PropertyMetadata(default(bool)));

    }
}
