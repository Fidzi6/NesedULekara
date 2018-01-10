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
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace NesedLekar.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class DetailPage : Page
    {
        private Order info;

        public DetailPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;

            if (e.Parameter as Order != null)
                info = e.Parameter as Order;
            else
                info = null;
        }

        private void StackPanel_Loading(FrameworkElement sender, object args)
        {
            if(info!=null)
            {
                nameTB.Text = info.Doctor.Name;
                addressTB.Text = info.Doctor.Adress;
                departmentTB.Text = info.Doctor.Department;
                dateTB.Text = info.Date;
                timeTB.Text = info.Time;

                if (info.Doctor.Img != null && info.Doctor.Img != string.Empty)
                    try
                    {
                        img.Source = new BitmapImage(new Uri(info.Doctor.Img));
                    }
                    catch (Exception) { img.Source = new BitmapImage(new Uri("ms-appx:///Assets/doctorM.png")); }
            }
        }
    }
}
