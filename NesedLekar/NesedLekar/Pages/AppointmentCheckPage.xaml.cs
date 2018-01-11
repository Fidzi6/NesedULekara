﻿using System;
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
    public sealed partial class AppointmentCheckPage : Page
    {
        private AppointmentInfo info;

        public AppointmentCheckPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter as AppointmentInfo != null)
            {
                info = e.Parameter as AppointmentInfo;

                doctorTB.Text = info.Doctor;
                addressTB.Text = info.Adress;
                dateTB.Text = info.Date;
                timeTB.Text = info.Time;


                info.Row[info.Index] = App.Patient;
            }
            else
                info = null;
        }

        private void okAppBarButton_Click(object sender, RoutedEventArgs e)
        {
            DateTime dt = info.Row.date;
            dt = dt.Date + new TimeSpan(int.Parse(info.Time.Split(':')[0]), int.Parse(info.Time.Split(':')[1]), 0);
            patients p = new patients();
            p.dateTime = dt;
            p.patient = App.Patient;
            p.doctor = info.Row.doktor;

            App.DatabaseWork.InsertRow(info.Row);
            App.DatabaseWork.InsertRow(p);
            
            (Window.Current.Content as Frame).Navigate(typeof(CalendarPage), info);
        }

        private void cancelAppBarButton_Click(object sender, RoutedEventArgs e)
        {
            (Window.Current.Content as Frame).Navigate(typeof(CalendarPage));
        }
    }
}
