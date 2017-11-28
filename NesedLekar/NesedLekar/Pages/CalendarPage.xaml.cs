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

            if (e.Parameter as AppointmentInfo != null)
                Saver.AddInfo(e.Parameter as AppointmentInfo);
            else if (e.Parameter as string != null && (e.Parameter as string) == "login")
                Saver.Clear();
            
            //Saver.AddInfo(new AppointmentInfo(new DoctorItem("Fero", "Košice", "Chirurg", true), "20. 11. 2017", "9:10"));
            //Saver.AddInfo(new AppointmentInfo(new DoctorItem("Jana", "Prešov", "Detská", true), "10. 12. 2017", "19:10"));
        }

        private void doctorAppBarButton_Click(object sender, RoutedEventArgs e)
        {
            (Window.Current.Content as Frame).Navigate(typeof(DoctorsPage));
        }

        private void addAppBarButton_Click(object sender, RoutedEventArgs e)
        {
            (Window.Current.Content as Frame).Navigate(typeof(AppointmentPage));
        }

        private void Page_Loading(FrameworkElement sender, object args)
        {
            //Nacitat z databazy / suboru ake objednania ma pacient
            //Odobrat zastarale

            List<AppointmentInfo> linfo = new List<AppointmentInfo>();

            for (int i = 0; i < Saver.Count; i++)
                linfo.Add(Saver.GetInfo(i));

            appointmentLV.ItemsSource = linfo;

            if (appointmentLV.Items.Count > 0)
                noAppointmentTB.Visibility = Visibility.Collapsed;
            else
                noAppointmentTB.Visibility = Visibility.Visible;
        }

        private void appointmentLV_ItemClick(object sender, ItemClickEventArgs e)
        {
            (Window.Current.Content as Frame).Navigate(typeof(DetailPage), e.ClickedItem);
        }
    }
}
