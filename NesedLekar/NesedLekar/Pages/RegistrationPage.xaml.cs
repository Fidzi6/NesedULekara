using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Popups;
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
    public sealed partial class RegistrationPage : Page
    {
        public RegistrationPage()
        {
            this.InitializeComponent();
            
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Frame root = Window.Current.Content as Frame;

            if (root.CanGoBack)
                SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
        }

        private void stornoBT_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.GoBack();
        }

        private async void okBT_Click(object sender, RoutedEventArgs e)
        {
            MessageDialog msgDialogError;
            processPR.IsActive = true;
            if (passTB.Password == passCheckTB.Password)
            {
                login l = new login();

                l.nick = loginTB.Text;
                l.pass = passTB.Password;
                l.rights = 3;


                List<login> ll = await App.DatabaseWork.SelectAsync(l.nick);

                if (ll?.Count == 0)
                {
                    App.DatabaseWork.InsertRow(l);

                    this.Frame.GoBack();
                }
                else
                {
                    msgDialogError = new MessageDialog("Zadané meno už existuje!");
                    await msgDialogError.ShowAsync();
                }
            }
            else
            {
                msgDialogError = new MessageDialog("Heslá sa nezhodujú!");
                await msgDialogError.ShowAsync();
            }
            processPR.IsActive = false;
        }
    }
}
