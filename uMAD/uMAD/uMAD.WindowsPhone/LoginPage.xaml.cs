using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using Parse;
using uMAD.Common;
using uMAD.Data;
using ZXing;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace uMAD
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LoginPage : Page
    {
        private const string createAccUrl = "http://umad.me";
        private bool _isOnQRState;
        private NavigationHelper navigationHelper;
        public User User { get; set; }
        private DispatcherTimer _timer;
        public LoginPage()
        {
            this.InitializeComponent();
            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += NavigationHelper_LoadState;
            this.navigationHelper.SaveState += NavigationHelper_SaveState;
            //_timer = new DispatcherTimer();
            //_timer.Tick += _timer_Tick;
            //_timer.Interval = TimeSpan.FromSeconds(3);
        }

        /// <summary>
        /// Gets the <see cref="NavigationHelper"/> associated with this <see cref="Page"/>.
        /// </summary>
        public NavigationHelper NavigationHelper => this.navigationHelper;

        private void NavigationHelper_SaveState(object sender, SaveStateEventArgs e)
        {

        }

        private void NavigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
            if (ParseUser.CurrentUser == null)
            {
                VisualStateManager.GoToState(this, "LoginState", true);
            }
            else
            {
                //_timer.Start();
                VisualStateManager.GoToState(this, "LoadingState", true);
                GenerateQrCode();
                VisualStateManager.GoToState(this, "QRState", true);
            }
        }

        private async void _timer_Tick(object sender, object e)
        {
            await FillUserInfo();
        }

        private async Task FillUserInfo()
        {
            var query = from user in new Parse.ParseQuery<User>()
                        where user.ObjectId.Equals(ParseUser.CurrentUser.ObjectId)
                        select user;
            User = await query.FirstAsync();

            this.DataContext = User;
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

        private async void GenerateQrCode()
        {
            if (User == null)
            {
                VisualStateManager.GoToState(this, "LoadingState", true);
                await FillUserInfo();
                VisualStateManager.GoToState(this, "QRState", true);
            }
            IBarcodeWriter writer = new BarcodeWriter
            {
                Format = BarcodeFormat.QR_CODE,//Mentioning type of bar code generation   
                Options = new ZXing.Common.EncodingOptions
                {
                    Height = 300,
                    Width = 300
                },
                Renderer = new ZXing.Rendering.PixelDataRenderer() { Foreground = Colors.Black }//Adding color QR code   
            };
            var result = writer.Write(User.ObjectId);
            var wb = result.ToBitmap() as WriteableBitmap;
            //Displaying QRCode Image   
            QRImage.Source = wb;

        }

        private async void SignUpBtn_OnClick(object sender, RoutedEventArgs e)
        {
            await Launcher.LaunchUriAsync(new Uri(createAccUrl, UriKind.Absolute));
        }

        private async void LoginBtn_OnClick(object sender, RoutedEventArgs e)
        {
            string username = usernameTextBox.Text.ToLower();
            string password = passwordTextBox.Password;
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                await App.Notify("Please input a username and a password.");
                return;
            }
            try
            {
                VisualStateManager.GoToState(this, "LoadingState", true);
                await ParseUser.LogInAsync(username, password);
                await FillUserInfo();
                //_timer.Start();
                GenerateQrCode();
                VisualStateManager.GoToState(this, "QRState", true);

                //this.Frame.Navigate(typeof(MainPage));
                //this.Frame.BackStack.Remove(this.Frame.BackStack.First(x => x.SourcePageType == typeof(LoginPage)));
                //this.Frame.BackStack.Remove(this.Frame.BackStack.First(x => x.SourcePageType == typeof(LandingPage)));
            }
            catch (ParseException ex)
            {
                await App.Notify(ex.Message);
                VisualStateManager.GoToState(this, "LoginState", true);
            }
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            ParseUser.LogOut();
            VisualStateManager.GoToState(this, "LoginState", true);
        }
    }
}
