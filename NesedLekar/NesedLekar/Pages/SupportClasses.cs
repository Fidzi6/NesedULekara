using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NesedLekar.Pages
{
    public static class Saver
    {
        private static List<AppointmentInfo> infos;

        public static bool Inicilaize { get => infos != null; }

        public static int Count
        {
            get
            {
                if (infos != null)
                    return infos.Count;
                else
                    return 0;
            }
        }

        public static AppointmentInfo GetInfo(int index)
        {
            return infos[index];
        }

        public static void Inicialize()
        {
            infos = new List<AppointmentInfo>();
        }

        public static void Clear()
        {
            infos.Clear();
        }
        
        public static void AddInfo(AppointmentInfo info)
        {
            if (!infos.Contains(info))
            {
                infos.Add(info);

                infos.Sort((x, y) =>
                {
                    bool b = false;
                    DateTime d1, d2;

                    try
                    {
                        d1 = DateTime.Parse(x.Date);
                        b = true;
                        d2 = DateTime.Parse(y.Date);

                        return DateTime.Compare(d1, d2);
                    }
                    catch (Exception)
                    {
                        if (b)
                            return -1;
                        else
                            return 1;
                    }
                });
            }
        }

        public static bool RemoveInfo(AppointmentInfo info)
        {
            return infos.Remove(info);
        }

        public static void RemoveInfo(int index)
        {
            infos.RemoveAt(index);
        }
    }

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
        //private DateTime dateDT;

        public DoctorItem Doctor { get => doctor; }
        public string Date { get => date; }
        public string Time { get => time; }
        //public DateTime DateDT { get => dateDT; }

        public AppointmentInfo(DoctorItem doctor, string date, string time)
        {
            //string[] sd, st;

            this.doctor = doctor;
            this.date = date;
            this.time = time;

            //sd = date.Split('.');
            //st = time.Split(':');

            //if (sd.Length > 2 && st.Length > 1)
                

        }
    }
}
