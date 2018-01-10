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
        private doctors doctor;
        
        //private string name;
        //private string address;
        //private string dep;
        private string img;

        //public string Name { get => name; set => name = value; }
        //public string Address { get => address; set => address = value; }
        //public string Department { get => dep; set => dep = value; }
        public string Img { get => img; }

        public string Name { get => doctor.name + " " + doctor.surname; }
        public string Email { get => doctor.email; }
        public string Department { get => doctor.specialization; }
        public string Phone { get => doctor.tel; }
        public string Adress { get => doctor.adress; }

        public string StartOrdinary { get => doctor.start; }
        public string EndOrdinary { get => doctor.superEnd; }
        public string Interval { get => doctor.interval; }

        public DoctorItem(doctors doctor, bool male)
        {
            this.doctor = doctor;
            if (male)
                this.img = "ms-appx:///Assets/doctorM.png";
            else
                this.img = "ms-appx:///Assets/doctorF.png";
        }
    }

    public class CommentInfo
    {
        private comments comment;

        //private string fullText;
        //private string name;
        //private string date;
        //private string time;

        //public string FullText { get => fullText; set => fullText = value; }
        //public string Name { get => name; set => name = value; }
        //public string Date { get { return "(" + date + ")"; } set { date = value; } }
        //public string Time { get => time; set => time = value; }

        public string FullText { get => comment.comment; }
        public string Comment
        {
            get
            {
                string[] ss;
                if (comment.comment != null && comment.comment != string.Empty && comment.comment.Length > 50)
                {
                    ss = comment.comment.Split('\r');
                    if (ss.Length > 3)
                        return ss[0] + "\r" + ss[1] + "\r...";
                    else
                        return comment.comment.Substring(0, 50) + "...";
                }
                else
                {
                    ss = comment.comment.Split('\r');
                    if (ss.Length > 3)
                        return ss[0] + "\r" + ss[1] + "\r...";
                    else
                        return comment.comment;
                }
            }
        }
        public string PatientName { get => comment.patient_name; }
        public string DateTime { get => comment.date+" ("+comment.time+")"; }

        public CommentInfo(comments comment)
        {
            this.comment = comment;

            //this.fullText = fullText;
            //this.name = name;
            //this.date = date;
            //this.time = time;
        }
    }

    public class Order
    {
        private patients order;
        private DoctorItem doctor;
        private bool ok;
        
        public DoctorItem Doctor { get { if (ok) return doctor; else return null; } }
        public string Date { get => order.date; }
        public string Time { get => order.time; }


        public Order(patients patient)
        {
            ok = false;
            order = patient;
            Task t = new Task(() => Work(patient.doctor));
            t.Start();
            t.Wait();

        }

        private void Work(string doctor)
        {
            var x = App.DatabaseWork.SelectAsynchDoc(doctor);
            x.Wait();
            if (x.Result?.Count > 0)
                this.doctor = new DoctorItem(x.Result[0], true);
            else
                this.doctor = null;
            ok = true;
        }
    }

    public class AppointmentInfo
    {        
        private DoktoriIntervals interval;
        private DoctorItem doctor;
        private string time;
        private int index;

        public string Date { get => interval.date; }
        public string Time { get => time; }
        public string Doctor { get => doctor.Name; }
        public string Adress { get => doctor.Adress; }
        public DoktoriIntervals Row { get => interval; }
        public int Index { get => index; }

        //public DateTime DateDT { get => dateDT; }

        //public AppointmentInfo(patients patient)
        //{
        //    interval = null;
        //    email = patient.doctor;
        //    time = patient.time;

        //}

        public AppointmentInfo(DoctorItem doctor, DoktoriIntervals row, string time, int index)
        {
            interval = row;
            this.doctor = doctor;
            this.time = time;
            this.index = index;
        }

        //public patients GetInstance()
        //{
        //    patients p = new patients();

        //    //patient name - from app - get actual patient login 
        //    p.doctor = doctor.Email;
        //    p.date = interval.date;
        //    p.time = time;

        //    return p;

        //}

        //private async Task<DoctorItem> GetDoctor()
        //{
        //    List<doctors> ld = null;

        //    ld = await App.DatabaseWork.SelectAsynchDoc(email);

        //    if (ld?.Count > 0)
        //        return new DoctorItem(ld[0], true);
        //    else
        //        return null;
        //}
    }
}
