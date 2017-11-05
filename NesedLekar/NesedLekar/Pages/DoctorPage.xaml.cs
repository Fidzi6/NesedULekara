﻿using System;
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
    public sealed partial class DoctorPage : Page
    {
        private DoctorItem doctor;

        public DoctorPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            //SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;

            doctor = e.Parameter as DoctorItem;
            if (doctor != null)
            {
                nameTB.Text = doctor.Name;
                departmentTB.Text = doctor.Department;

                if (doctor.ImageSource != null && doctor.ImageSource != string.Empty)
                    try
                    {
                        img.Source = new BitmapImage(new Uri(doctor.ImageSource));
                    }
                    catch (Exception) { img.Source = new BitmapImage(new Uri("ms-appx:///Assets/doctorM.png")); }
            }
        }
    }
}
