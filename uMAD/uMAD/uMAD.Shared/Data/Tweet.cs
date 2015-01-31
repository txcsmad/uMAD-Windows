using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI.Xaml.Media.Imaging;

namespace uMAD.Data
{
    public class Tweet
    {
        public Uri twitterImage { get; set; }
        public string username { get; set; }
        public string displayName { get; set; }
        public string tweetText { get; set; }

    }
}
