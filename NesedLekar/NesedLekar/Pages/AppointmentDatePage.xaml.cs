using System;
using System.Collections.Generic;
using System.Globalization;
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
//<DatePicker x:Name="dateDTP" Grid.Row="2" Margin="0,10,0,20" HorizontalAlignment="Center" DateChanged="DatePicker_DateChanged"/>
namespace NesedLekar.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AppointmentDatePage : Page
    {
        private DoctorItem doctor;
        private DoktoriIntervals data;
        private List<int> lIndex;

        public AppointmentDatePage()
        {
            this.InitializeComponent();
            dateDTP.MinDate = DateTime.Now.AddDays(1);
            dateDTP.MaxDate = DateTime.Now.AddYears(1);
            lIndex = new List<int>();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            data = null;
            if (e.Parameter as DoctorItem != null)
                doctor= e.Parameter as DoctorItem;
            else
                doctor = null;
        }

        //private void DatePicker_DateChanged(object sender, DatePickerValueChangedEventArgs e)
        //{
        //    listLV.ItemsSource = null;

        //    //listLV.Items.Add("9 : 00");
        //    //listLV.Items.Add("9 : 30");
        //    //listLV.Items.Add("10 : 00");

        //    if (listLV.Items.Count > 0)
        //        noDateTB.Visibility = Visibility.Collapsed;
        //    else
        //        noDateTB.Visibility = Visibility.Visible;
        //}

        private void ListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            int i = 0;

            for (int a = 0; a < listLV.Items.Count; a++)
                if (listLV.Items[a].ToString() == e.ClickedItem.ToString())
                {
                    i = lIndex[a];
                    break;
                }
            (Window.Current.Content as Frame).Navigate(typeof(AppointmentCheckPage), new AppointmentInfo(doctor, data, e.ClickedItem.ToString(), i));
        }

        private async void DatePicker_DateChanged(CalendarDatePicker sender, CalendarDatePickerDateChangedEventArgs args)
        {
            processPR.IsActive = true;
            listLV.ItemsSource = null;


            string str;
            DateTime a, s, e;
            double interval = double.Parse(doctor.Interval);
            int ix = 0;
            List<string> ls = new List<string>();
            try
            {
                s = DateTime.ParseExact(doctor.StartOrdinary, "HH:mm", CultureInfo.CurrentCulture);
                e = DateTime.ParseExact(doctor.EndOrdinary, "HH:mm", CultureInfo.CurrentCulture);
                a = s;
                str = dateDTP.Date.Value.ToString("dd.MM.yyyy");

                List<DoktoriIntervals> ldi = await App.DatabaseWork.SelectAsynch(doctor.Email, str);

                if (ldi == null || ldi.Count < 1)
                {
                    data = new DoktoriIntervals();
                    while (e > a)
                    {
                        lIndex.Add(ix);
                        data[ix++] = "0";
                        ls.Add(a.ToString("HH:mm"));
                        a = a.AddMinutes(interval);
                    }
                    data.doktor = doctor.Email;
                    data.date = str;
                }
                else
                {
                    data = ldi[0];

                    for (int i = 0; i < 25; i++)
                        if (data[i] == "0")
                        {
                            lIndex.Add(i);
                            ls.Add(s.AddMinutes(i * interval).ToString("HH:mm"));
                        }
                }

            }
            catch (Exception)
            {
                ls = new List<string>();
            }

            listLV.ItemsSource = ls;

            if (listLV.Items.Count > 0)
                noDateTB.Visibility = Visibility.Collapsed;
            else
                noDateTB.Visibility = Visibility.Visible;
            processPR.IsActive = false;
        }
    }
}
