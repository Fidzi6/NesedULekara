using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NesedLekar.Pages
{
    public class DoctorItem
    {
        private string name;
        private string address;
        private string dep;
        private string img;

        public string Name { get => name; set => name = value; }
        public string Address { get => address; set => address = value; }
        public string Department { get => dep; set => dep = value; }
        public string Img { get => img; }

        public DoctorItem(string name, string address, string department, bool male)
        {
            this.name = name;
            this.address = address;
            this.dep = department;

            if (male)
                this.img = "ms-appx:///Assets/doctorM.png";
            else
                this.img = "ms-appx:///Assets/doctorF.png";
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

    public class AppointmentInfo
    {
        private DoctorItem doctor;
        private string date;
        private string time;

        public DoctorItem Doctor { get => doctor; set => doctor = value; }
        public string Date { get => date; set => date = value; }
        public string Time { get => time; set => time = value; }

        public AppointmentInfo(DoctorItem doctor, string date, string time)
        {
            this.Doctor = doctor;
            this.Date = date;
            this.Time = time;
        }
    }
}
