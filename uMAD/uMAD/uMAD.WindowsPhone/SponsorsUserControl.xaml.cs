using Parse;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using uMAD.Data;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace uMAD
{
    public sealed partial class SponsorsUserControl : UserControl
    {
        private List<Sponsor> sponsors;
        private ParseQuery<Sponsor> parseSponsors;
        private bool isLoaded;
        public event EventHandler LoadingTweets;
        public event EventHandler LoadedTweets;
        public SponsorsUserControl()
        {
            this.InitializeComponent();

            Loaded += SponsorsUserControl_Loaded;
            //setSponsors();
        }

        private void SponsorsUserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (!isLoaded)
            {
                if (LoadingTweets != null)
                    LoadingTweets(this, new EventArgs());
                retrieveParseSponsors();
                isLoaded = true;
                if (LoadedTweets != null)
                    LoadedTweets(this, new EventArgs());
            }

        }

        private async void retrieveParseSponsors()
        {
            parseSponsors = from sponsor in new ParseQuery<Sponsor>() orderby sponsor.Level descending select sponsor;
            //parseSponsors = parseSponsors.Include("companyImage");
            //parseSponsors.g
            SponsorList.ItemsSource = await parseSponsors.FindAsync();

        }

        private async void CompanyLogo_Tapped(object sender, TappedRoutedEventArgs e)
        {
            FrameworkElement image = sender as FrameworkElement;

            if (image == null)
                return;
            Sponsor sponsor = image.DataContext as Sponsor;
            if (sponsor == null)
                return;
            await Launcher.LaunchUriAsync(new Uri(sponsor.Company.WebsiteUrl));

        }

        //private void setSponsors()
        //{
        //    sponsors = new List<Sponsor>();

        //    sponsors.Add(new Sponsor() { CompanyName = "MSFT", CompanyURL = "http://www.microsoft.com/en-us/default.aspx", CompanyLogo = new BitmapImage( new Uri("/Assets/SmallLogo.scale-240.png", UriKind.Relative)) } );
        //    sponsors.Add(new Sponsor() { CompanyName = "NewSponsor", CompanyURL = "http://www.google.com/", CompanyLogo = new BitmapImage(new Uri("/Assets/WideLogo.scale-240.png", UriKind.Relative)) });
        //}
    }
}
