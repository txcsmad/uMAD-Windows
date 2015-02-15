using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Parse;
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
        public ScheduleViewModel viewModel;
        //private ObservableCollection<ScheduleSession> scheduleData;
        private bool isLoaded;
        public event EventHandler LoadingTweets;
        public event EventHandler LoadedTweets;
        public ScheduleControl()
        {
            this.InitializeComponent();
            viewModel = new ScheduleViewModel();
            //scheduleData = new ObservableCollection<ScheduleSession>();
            Loaded += ScheduleControl_Loaded;
        }

        private async void ScheduleControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (!isLoaded)
            {
                semanticControl.ViewChangeStarted += SemanticControl_ViewChangeStarted;
                (semanticControl.ZoomedOutView as ListViewBase).ItemsSource = viewModel.Items;

                if (LoadingTweets != null)
                    LoadingTweets(this, new EventArgs());
                this.DataContext = viewModel;
                scheduleCollection.Source = viewModel.Items;
                await RefreshData();
                isLoaded = true;
                if (LoadedTweets != null)
                    LoadedTweets(this, new EventArgs());
            }

            if (viewModel.Count == 0)
                emptyControl.Visibility = Visibility.Visible;
        }

        private void SemanticControl_ViewChangeStarted(object sender, SemanticZoomViewChangedEventArgs e)
        {
            if (e.SourceItem == null || e.SourceItem.Item == null)
                return;

            if (e.SourceItem.Item.GetType() == typeof(GroupList<ScheduleSession>))
            {
                var group = (GroupList<ScheduleSession>)e.SourceItem.Item;

                //var group = viewModel.Items.FirstOrDefault(d => ((char)d.Key) == hi.Name);
                if (group != null)
                    e.DestinationItem = new SemanticZoomLocation() { Item = group };
            }
        }

        private async Task RefreshData()
        {
            viewModel.Clear();
            var data = await LoadData();
            viewModel.Populate(data);
        }

        private async Task<IEnumerable<ScheduleSession>> LoadData()
        {
            var query = from item in new ParseQuery<ScheduleSession>()
                        orderby item.Time ascending
                        select item;
            //query = query.WhereGreaterThanOrEqualTo("startTime", DateTime.Now);
            //query = query.OrderBy("startTime");
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

    public class ScheduleViewModel
    {
        public ObservableCollection<GroupList<ScheduleSession>> Items { get; set; }
        private string[] titles = { "past sessions", "upcoming sessions" };
        public int Count { get; private set; }

        public ScheduleViewModel()
        {
            Items = new ObservableCollection<GroupList<ScheduleSession>>();
        }

        public void Populate(IEnumerable<ScheduleSession> list)
        {
            Count = Count + list.Count();
            var pastGroup = new GroupList<ScheduleSession>() { Title = titles[0] };
            var upcomingGroup = new GroupList<ScheduleSession>() { Title = titles[1] };

            var pastList = from item in list
                           where item.Time < DateTime.Now
                           select item;
            var upcomingList = from item in list
                               where item.Time >= DateTime.Now
                               select item;
            pastGroup.AddRange(pastList);
            upcomingGroup.AddRange(upcomingList);
            if (upcomingGroup.Count > 0)
                Items.Add(upcomingGroup);
            if (pastGroup.Count > 0)
                Items.Add(pastGroup);
        }

        public void Clear()
        {
            Items.Clear();
        }
    }

    public class GroupList<T> : List<T>
    {
        public string Title { get; set; }

    }
}
