using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
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
    public sealed partial class AppointmentDoctorPage : Page
    {
        public AppointmentDoctorPage()
        {
            this.InitializeComponent();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            processPR.IsActive = true;
            var ld = await App.DatabaseWork.AllDoctors();

            var nld = ld.ToList();

            if (nld?.Count > 0)
                for (int i = 0; i < nld.Count; i++)
                    listLV.Items.Add(new DoctorItem(nld[i], true));

            if (listLV.Items.Count > 0)
                noDoctorTB.Visibility = Visibility.Collapsed;
            else
                noDoctorTB.Visibility = Visibility.Visible;
            processPR.IsActive = false;
        }

        private void listLV_ItemClick(object sender, ItemClickEventArgs e)
        {
            (Window.Current.Content as Frame).Navigate(typeof(AppointmentDatePage), e.ClickedItem);
        }

        private void Page_Loading(FrameworkElement sender, object args)
        {
            //listLV.Items.Add(new DoctorItem("Fero", "Košice", "Chirurg", true));
            //listLV.Items.Add(new DoctorItem("Jana", "Prešov", "Všeobecny", false));

            
        }

        private void departmentCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
