﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
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
    public sealed partial class DoctorPage : Page
    {
        private DoctorItem doctor;

        public DoctorPage()
        {
            this.InitializeComponent();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            //SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;

            doctor = e.Parameter as DoctorItem;
            if (doctor != null)
            {
                nameTB.Text = doctor.Name;
                addressTB.Text = doctor.Adress;
                departmentTB.Text = doctor.Department;

                if (doctor.Img != null && doctor.Img != string.Empty)
                    try
                    {
                        img.Source = new BitmapImage(new Uri(doctor.Img));
                    }
                    catch (Exception) { img.Source = new BitmapImage(new Uri("ms-appx:///Assets/doctorM.png")); }

                LoadData();                
            }
        }

        private async Task<bool> LoadData()
        {
            processPR.IsActive = true;
            var ld = await App.DatabaseWork.SelectAsynchC(doctor.Email);

            var nld = ld.ToList();

            if (nld?.Count > 0)
                for (int i = 0; i < nld.Count; i++)
                    listLV.Items.Add(new CommentInfo(nld[i]));

            if (listLV.Items.Count > 0)
                noCommentTB.Visibility = Visibility.Collapsed;
            else
                noCommentTB.Visibility = Visibility.Visible;
            processPR.IsActive = false;
            return true;
        }

        private void Grid_Loading(FrameworkElement sender, object args)
        {
            //listLV.Items.Add(new CommentInfo("dobrý.", "Laci", "01.01.2007", "10:00"));
            //listLV.Items.Add(new CommentInfo("Ta to ten doktor sa nezdá.", "Peťo", "01.05.2007", "08:00"));
            //listLV.Items.Add(new CommentInfo("uuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuudddddddddddddddskfjsdlkfjsdklfjsdlfjldksfjklsdvbnsdlneiopnavlsenvlkashnerfnsdlkvnaeivn laejhifsdgklvjhaeiovn aiocfhnvlskhnvl ksdbhofhedvn klwehsfue", "Peťo", "01.05.2007", "08:00"));

            
        }

        private void listLV_ItemClick(object sender, ItemClickEventArgs e)
        {
            (Window.Current.Content as Frame).Navigate(typeof(CommentPage), e.ClickedItem);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            (Window.Current.Content as Frame).Navigate(typeof(AppointmentDatePage), doctor);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            (Window.Current.Content as Frame).Navigate(typeof(CommentAddPage), doctor);
        }
    }
}
