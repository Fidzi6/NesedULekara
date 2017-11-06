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
    public sealed partial class AppointmentDatePage : Page
    {
        public AppointmentDatePage()
        {
            this.InitializeComponent();
        }

        private void DatePicker_DateChanged(object sender, DatePickerValueChangedEventArgs e)
        {
            listLV.ItemsSource = null;

            listLV.Items.Add("9 : 00");
            listLV.Items.Add("9 : 30");
            listLV.Items.Add("10 : 00");

            if (listLV.Items.Count > 0)
                noDateTB.Visibility = Visibility.Collapsed;
            else
                noDateTB.Visibility = Visibility.Visible;
        }

        private void ListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            (Window.Current.Content as Frame).Navigate(typeof(AppointmentCheckPage), e.ClickedItem);
        }
    }
}
