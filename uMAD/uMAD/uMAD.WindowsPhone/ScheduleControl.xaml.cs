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
                dataList.ItemsSource = scheduleData;
                await RefreshData();
                isLoaded = true;
            }
        }

        private async Task RefreshData()
        {
            scheduleData.Clear();
            var data = await LoadFakeData();
            foreach (var item in data)
                scheduleData.Add(item);
        }

        private Task<IList<ScheduleSession>> LoadFakeData()
        {
            List<ScheduleSession> list = new List<ScheduleSession>();
            list.Add(new ScheduleSession() { SpeakerName = "Mustafa Taleb", SubjectName = "WP is Awesome", Time = new DateTime(2015, 2, 21, 15, 30, 0) });
            list.Add(new ScheduleSession() { SpeakerName = "Mustafa Taleb", SubjectName = "WP is Awesome", Time = new DateTime(2015, 2, 21, 16, 30, 0) });
            list.Add(new ScheduleSession() { SpeakerName = "Mustafa Taleb", SubjectName = "WP is Great", Time = new DateTime(2015, 2, 21, 9, 30, 0) });
        }
    }
}
