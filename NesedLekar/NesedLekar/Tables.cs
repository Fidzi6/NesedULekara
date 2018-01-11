using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace NesedLekar
{
    public class login
    {
        [JsonProperty(PropertyName = "id")]
        public string id { get; set; }
        [JsonProperty(PropertyName = "login")]
        public string nick { get; set; }
        [JsonProperty(PropertyName = "password")]
        public string pass { get; set; }
        [JsonProperty(PropertyName = "rights")]
        public float rights { get; set; }
    }

    public class comments
    {
        public string id { get; set; }
        public string doctor_name { get; set; }
        public string patient_name { get; set; }
        public string comment { get; set; }
        public DateTime dateTime { get; set; }
        public string rating { get; set; }
    }

    public class doctors
    {
        public string id { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string email { get; set; }
        public string specialization { get; set; }
        public string tel { get; set; }
        public string adress { get; set; }
        public string latitude { get; set; }
        public string longtude { get; set; }
        public string start { get; set; }
        public string lunch { get; set; }
        public string emergency { get; set; }
        public string interval { get; set; }
        public string rating { get; set; }
        public string superEnd { get; set; }
    }

    public class patients
    {
        public string id { get; set; }
        public string patient { get; set; }
        public string doctor { get; set; }
        public DateTime dateTime { get; set; }
    }

    public class DoktoriIntervals
    {
        public string id { get; set; }
        public DateTime date { get; set; }
        public string doktor { get; set; }
        public string i1 { get; set; }
        public string i2 { get; set; }
        public string i3 { get; set; }
        public string i4 { get; set; }
        public string i5 { get; set; }
        public string i6 { get; set; }
        public string i7 { get; set; }
        public string i8 { get; set; }
        public string i9 { get; set; }
        public string i10 { get; set; }
        public string i11 { get; set; }
        public string i12 { get; set; }
        public string i13 { get; set; }
        public string i14 { get; set; }
        public string i15 { get; set; }
        public string i16 { get; set; }
        public string i17 { get; set; }
        public string i18 { get; set; }
        public string i19 { get; set; }
        public string i20 { get; set; }
        public string i21 { get; set; }
        public string i22 { get; set; }
        public string i23 { get; set; }
        public string i24 { get; set; }
        public string i25 { get; set; }

        public string this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0: return i1;
                    case 1: return i2;
                    case 2: return i3;
                    case 3: return i4;
                    case 4: return i5;
                    case 5: return i6;
                    case 6: return i7;
                    case 7: return i8;
                    case 8: return i9;
                    case 9: return i10;
                    case 10: return i11;
                    case 11: return i12;
                    case 12: return i13;
                    case 13: return i14;
                    case 14: return i15;
                    case 15: return i16;
                    case 16: return i17;
                    case 17: return i18;
                    case 18: return i19;
                    case 19: return i20;
                    case 20: return i21;
                    case 21: return i22;
                    case 22: return i23;
                    case 23: return i24;
                    case 24: return i25;
                    default: return string.Empty;
                }
            }
            set
            {
                switch (index)
                {
                    case 0: i1 = value; break;
                    case 1: i2 = value; break;
                    case 2: i3 = value; break;
                    case 3: i4 = value; break;
                    case 4: i5 = value; break;
                    case 5: i6 = value; break;
                    case 6: i7 = value; break;
                    case 7: i8 = value; break;
                    case 8: i9 = value; break;
                    case 9: i10 = value; break;
                    case 10: i11 = value; break;
                    case 11: i12 = value; break;
                    case 12: i13 = value; break;
                    case 13: i14 = value; break;
                    case 14: i15 = value; break;
                    case 15: i16 = value; break;
                    case 16: i17 = value; break;
                    case 17: i18 = value; break;
                    case 18: i19 = value; break;
                    case 19: i20 = value; break;
                    case 20: i21 = value; break;
                    case 21: i22 = value; break;
                    case 22: i23 = value; break;
                    case 23: i24 = value; break;
                    case 24: i25 = value; break;
                }
            }
        }

        public DoktoriIntervals()
        {
            i1 = "-";
            i2 = "-";
            i3 = "-";
            i4 = "-";
            i5 = "-";
            i6 = "-";
            i7 = "-";
            i8 = "-";
            i9 = "-";
            i10 = "-";
            i11 = "-";
            i12 = "-";
            i13 = "-";
            i14 = "-";
            i15 = "-";
            i16 = "-";
            i17 = "-";
            i18 = "-";
            i19 = "-";
            i20 = "-";
            i21 = "-";
            i22 = "-";
            i23 = "-";
            i24 = "-";
            i25 = "-";
        }
    }
}
