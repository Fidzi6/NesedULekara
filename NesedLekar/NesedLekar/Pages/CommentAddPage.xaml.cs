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
    public sealed partial class CommentAddPage : Page
    {
        private DoctorItem doctor;
        public CommentAddPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if(e.Parameter as DoctorItem !=null)
            {
                doctor = e.Parameter as DoctorItem;

                nameTB.Text = doctor.Name;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string s, d, t;
            commentREB.Document.GetText(Windows.UI.Text.TextGetOptions.None, out s);
            if (s.Trim() != string.Empty)
            {
                comments c = new comments();
                c.patient_name = App.Patient;
                c.doctor_name = doctor.Email;

                c.comment = s;
                c.dateTime = DateTime.Now;
                c.rating = "0";

                App.DatabaseWork.InsertRow(c);
                this.Frame.GoBack();
            }
        }
    }
}
