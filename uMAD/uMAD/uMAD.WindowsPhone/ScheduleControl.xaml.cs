using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
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
    public sealed partial class ScheduleControl : UserControl
    {
        private ObservableCollection<ScheduleSession> scheduleData;
        private bool isLoaded;
        public event EventHandler LoadingTweets;
        public event EventHandler LoadedTweets;
        public ScheduleControl()
        {
            this.InitializeComponent();
            scheduleData = new ObservableCollection<ScheduleSession>();
            Loaded += ScheduleControl_Loaded;
        }

        private async void ScheduleControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (!isLoaded)
            {
                if (LoadingTweets != null)
                    LoadingTweets(this, new EventArgs());
                dataList.ItemsSource = scheduleData;
                await RefreshData();
                isLoaded = true;
                if (LoadedTweets != null)
                    LoadedTweets(this, new EventArgs());
            }
        }

        private async Task RefreshData()
        {
            scheduleData.Clear();
            var data = await LoadData();
            foreach (var item in data)
                scheduleData.Add(item);
        }

        private async Task<IEnumerable<ScheduleSession>> LoadData()
        {
            var query = new Parse.ParseQuery<ScheduleSession>();
            query = query.WhereGreaterThanOrEqualTo("startTime", DateTime.Now);
            query = query.OrderBy("startTime");
            return await query.FindAsync();
        }
        private async Task<List<ScheduleSession>> LoadFakeData()
        {
            return await Task.Factory.StartNew<List<ScheduleSession>>(() =>
            {
                List<ScheduleSession> list = new List<ScheduleSession>();
                list.Add(new ScheduleSession() { SpeakerName = "Mustafa Taleb", SubjectName = "WP is Awesome", Time = new DateTime(2015, 2, 21, 15, 30, 0) });
                list.Add(new ScheduleSession() { SpeakerName = "Mustafa Taleb", SubjectName = "WP is Amazing", Time = new DateTime(2015, 2, 21, 16, 30, 0) });
                list.Add(new ScheduleSession() { SpeakerName = "Mustafa Taleb", SubjectName = "WP is Great", Time = new DateTime(2015, 2, 21, 9, 30, 0) });
                return list;
            });
        }

        private void Grid_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var element = sender as FrameworkElement;
            var session = element.DataContext as ScheduleSession;
            if (session != null)
            {
                ScheduleSession.CurrentSession = session;
                ((Frame)Window.Current.Content).Navigate(typeof(SessionView));
            }
        }
    }
}
