using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
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
    public sealed partial class DoctorsPage : Page
    {
        public DoctorsPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
        }

        private void Page_Loading(FrameworkElement sender, object args)
        {
            listLV.ItemsSource = new List<DoctorItem>() { new DoctorItem("Jana", "Prešov", "Vseobecny", false), new DoctorItem("Fero", "Poprad", "Chirurg", true) };

            RefreshDoctors();
        }

        private void listLV_ItemClick(object sender, ItemClickEventArgs e)
        {
            (Window.Current.Content as Frame).Navigate(typeof(DoctorPage), e.ClickedItem);
        }

        private void RefreshDoctors()
        {

            if (listLV.ItemsSource != null)
                noDoctorTB.Visibility = Visibility.Collapsed;
            else
                noDoctorTB.Visibility = Visibility.Visible;
        }
    }
}
