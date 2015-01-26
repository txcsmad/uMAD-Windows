﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace uMAD
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            //StatusBar.GetForCurrentView().HideAsync();
            this.NavigationCacheMode = NavigationCacheMode.Required;
            Loaded += MainPage_Loaded;
        }

        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            //StatusBar.GetForCurrentView().ForegroundColor = Colors.White;
            var bar = StatusBar.GetForCurrentView();
            bar.BackgroundColor = (Color)App.Current.Resources["MADColor"];
            bar.BackgroundOpacity = 1;
        }


        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // TODO: Prepare page for display here.

            // TODO: If your application contains multiple pages, ensure that you are
            // handling the hardware Back button by registering for the
            // Windows.Phone.UI.Input.HardwareButtons.BackPressed event.
            // If you are using the NavigationHelper provided by some templates,
            // this event is handled for you.
        }

        private void navigationPivot_PivotItemLoaded(Pivot sender, PivotItemEventArgs args)
        {
            switch (navigationPivot.SelectedIndex)
            {
                case 0:
                    VisualStateManager.GoToState(this, "ScheduleState", true);
                    break;
                case 1:
                    VisualStateManager.GoToState(this, "TwitterState", true);
                    break;
                case 2:
                    VisualStateManager.GoToState(this, "SponsorState", true);
                    break;
            }
        }

        private void scheduleHeaderBlock_Tapped(object sender, TappedRoutedEventArgs e)
        {
            navigationPivot.SelectedIndex = 0;
        }

        private void Social_Tapped(object sender, TappedRoutedEventArgs e)
        {
            navigationPivot.SelectedIndex = 1;
        }

        private void sponsorsHeaderBlock_Tapped(object sender, TappedRoutedEventArgs e)
        {
            navigationPivot.SelectedIndex = 2;
        }
    }
}
