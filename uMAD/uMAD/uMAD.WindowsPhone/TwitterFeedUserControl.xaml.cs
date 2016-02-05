using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using uMAD.Data;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using LinqToTwitter;
using Windows.System;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace uMAD
{
    public sealed partial class TwitterFeedUserControl : UserControl
    {
        public string Handle = "@utcsmad";
        private ObservableCollection<Status> tweetData;
        private bool isLoaded;
        public event EventHandler LoadingTweets;
        public event EventHandler LoadedTweets;
        public TwitterFeedUserControl()
        {
            this.InitializeComponent();
            tweetData = new ObservableCollection<Status>();

            Loaded += TwitterFeedUserControl_Loaded;
        }

        private async void TwitterFeedUserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (!isLoaded)
            {
                if (LoadingTweets != null)
                    LoadingTweets(this, new EventArgs());
                //add Twitter feeds
                TwitterFeed.ItemsSource = tweetData;
                await RefreshData();
                isLoaded = true;
                if (LoadedTweets != null)
                    LoadedTweets(this, new EventArgs());
            }
            if (tweetData.Count == 0)
                emptyControl.Visibility = Visibility.Visible;

        }

        private async Task RefreshData()
        {
            tweetData.Clear();
            var data = await LoadData();
            foreach (var item in data)
                tweetData.Add(item);
        }

        private async Task<List<Status>> LoadData()
        {
            var auth = new ApplicationOnlyAuthorizer();
            auth.AccessType = AuthAccessType.Read;
            auth.CredentialStore = new InMemoryCredentialStore() { ConsumerKey = Private.Keys.TWITTER_API_KEY, ConsumerSecret = Private.Keys.TWITTER_API_SECRET };
            await auth.AuthorizeAsync();
            var context = new TwitterContext(auth);
            var tweets = new List<Status>();
            if (Handle.StartsWith("#"))
            {
                var result = await (from tweet in context.Search where tweet.Type == SearchType.Search && tweet.Query == Handle && tweet.ResultType == ResultType.Recent && tweet.Count == 200 select tweet).SingleOrDefaultAsync();
                tweets = result.Statuses;
            }
            else
                tweets = await (from tweet in context.Status where tweet.Type == StatusType.User && tweet.ScreenName == Handle && tweet.Count == 200 select tweet).ToListAsync();

            for (int i = 0; i < tweets.Count; i++)
            {
                if (tweets[i].RetweetedStatus != null && tweets[i].RetweetedStatus.StatusID != 0)
                    tweets[i] = tweets[i].RetweetedStatus;
            }
            return tweets;
        }

        private async void TwitterFeed_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Status status = TwitterFeed.SelectedItem as Status;
            if (status == null)
                return;
            await Launcher.LaunchUriAsync(new Uri(string.Format("https://twitter.com/{0}/status/{1}", status.User.ScreenNameResponse, status.StatusID)));
        }

        //private List<Tweet> LoadFakeData()
        //{
        //    List<Tweet> list = new List<Tweet>();
        //    list.Add(new Tweet() { displayName = "MAD", username = "utcsmad", twitterImage = new BitmapImage(new Uri("ms-appx-web:///Assets/SmallLogo.scale-240.png")), tweetText = "wassup guise" });
        //    list.Add(new Tweet() { displayName = "MAD", username = "utcsmad", twitterImage = new BitmapImage(new Uri("ms-appx-web:///Assets/SmallLogo.scale-240.png")), tweetText = "do you like me? #new" });
        //    list.Add(new Tweet() { displayName = "MAD", username = "utcsmad", twitterImage = new BitmapImage(new Uri("ms-appx-web:///Assets/SmallLogo.scale-240.png")), tweetText = "aaayyyeeee lmao" });
        //    return list;
        //}


    }
}
