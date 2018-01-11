using System;
using System.Collections.Generic;
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

using NesedLekar.Pages;
using Windows.UI.Core;
using Windows.UI.Popups;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace NesedLekar
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            this.NavigationCacheMode = NavigationCacheMode.Required;
            Saver.Inicialize();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            loginTB.Text = "";
            passwordTB.Password = "";
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Collapsed;            
        }

        private async void loginBT_Click(object sender, RoutedEventArgs e)
        {
            MessageDialog msgDialogError;
            processPR.IsActive = true;
            if (loginTB.Text != string.Empty || passwordTB.Password != string.Empty)
            {
                List<login> ll = await App.DatabaseWork.SelectAsync(loginTB.Text, passwordTB.Password);

                if (ll != null && ll.Count > 0)
                {
                    App.Patient = loginTB.Text;
                    (Window.Current.Content as Frame).Navigate(typeof(CalendarPage), "login");
                }
                else
                {
                    msgDialogError = new MessageDialog("Nesprávne prihlasovacie údaje!");
                    await msgDialogError.ShowAsync();
                }
            }
            processPR.IsActive = false;
        }

        private void regBT_Click(object sender, RoutedEventArgs e)
        {
            (Window.Current.Content as Frame).Navigate(typeof(RegistrationPage));
        }
    }
}
