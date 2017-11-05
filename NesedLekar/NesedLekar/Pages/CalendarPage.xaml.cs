using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace NesedLekar.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CalendarPage : Page
    {
        public CalendarPage()
        {
            this.InitializeComponent();
            
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {            
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Collapsed;
        }

        private void calendarView_DayItemChange(CalendarView sender, CalendarViewDayItemChangingEventArgs args)
        {
            //if (args.Item.Date.Date.Equals(DateTime.Now.Date))
            //    args.Item.Background = new SolidColorBrush(Colors.Red);
            if (args.Item.Date.Date.Equals(DateTime.Now.Date))
                args.Item.Background = new SolidColorBrush(Colors.Blue);
            else if (Comparer<DateTime>.Default.Compare(args.Item.Date.Date, new DateTime(2017, 12, 12)) == 0)
                args.Item.Background = new SolidColorBrush(Colors.Green);
        }

        private void doctorAppBarButton_Click(object sender, RoutedEventArgs e)
        {
            (Window.Current.Content as Frame).Navigate(typeof(DoctorsPage));
        }

        private void addAppBarButton_Click(object sender, RoutedEventArgs e)
        {
            (Window.Current.Content as Frame).Navigate(typeof(AppointmentPage));
        }
    }
}
