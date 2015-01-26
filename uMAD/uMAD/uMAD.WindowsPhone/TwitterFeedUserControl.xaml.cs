using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace uMAD
{
    public sealed partial class TwitterFeedUserControl : UserControl
    {
        private ObservableCollection<Tweet> tweetData;
        private bool isLoaded;

        public TwitterFeedUserControl()
        {
            this.InitializeComponent();
            tweetData = new ObservableCollection<Tweet>();

            Loaded += TwitterFeedUserControl_Loaded;
        }

        private void TwitterFeedUserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if(!isLoaded)
            {
                //TODO add Twitter feeds
                isLoaded = true;
            }
            
        }


    }
}
