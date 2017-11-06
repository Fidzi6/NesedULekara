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

        private void Grid_Loading(FrameworkElement sender, object args)
        {
            listLV.Items.Add(new CommentInfo("dobrý.", "Laci", "01.01.2007", "10:00"));
            listLV.Items.Add(new CommentInfo("Ta to ten doktor sa nezdá.", "Peťo", "01.05.2007", "08:00"));
            listLV.Items.Add(new CommentInfo("uuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuudddddddddddddddskfjsdlkfjsdklfjsdlfjldksfjklsdvbnsdlneiopnavlsenvlkashnerfnsdlkvnaeivn laejhifsdgklvjhaeiovn aiocfhnvlskhnvl ksdbhofhedvn klwehsfue", "Peťo", "01.05.2007", "08:00"));

            if (listLV.Items.Count > 0)
                noCommentTB.Visibility = Visibility.Collapsed;
            else
                noCommentTB.Visibility = Visibility.Visible;
        }

        private void listLV_ItemClick(object sender, ItemClickEventArgs e)
        {
            (Window.Current.Content as Frame).Navigate(typeof(CommentPage), e.ClickedItem);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            (Window.Current.Content as Frame).Navigate(typeof(AppointmentDatePage), doctor);
        }
    }

    public class CommentInfo
    {
        private string fullText;
        private string name;
        private string date;
        private string time;
                
        public string FullText { get => fullText; set => fullText = value; }
        public string Name { get => name; set => name = value; }
        public string Date { get { return "(" + date + ")"; } set { date = value; } }
        public string Time { get => time; set => time = value; }

        public CommentInfo(string fullText, string name, string date, string time)
        {
            this.fullText = fullText;
            this.name = name;
            this.date = date;
            this.time = time;
        }

    }
}
