using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using uMAD.Data;
using Windows.Foundation;
using Windows.Foundation.Collections;
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

        public SponsorsUserControl()
        {
            this.InitializeComponent();
            setSponsors();
        }

        private void setSponsors()
        {
            sponsors = new List<Sponsor>();

            sponsors.Add(new Sponsor() { CompanyName = "MSFT", CompanyURL = "http://www.microsoft.com/en-us/default.aspx", CompanyLogo = new BitmapImage( new Uri("/Assets/SmallLogo.scale-240.png", UriKind.Relative)) } );
            sponsors.Add(new Sponsor() { CompanyName = "NewSponsor", CompanyURL = "http://www.google.com/", CompanyLogo = new BitmapImage(new Uri("/Assets/WideLogo.scale-240.png", UriKind.Relative)) });
        }
    }
}
