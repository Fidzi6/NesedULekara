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
        public string date { get; set; }
        public string time { get; set; }
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
        public string date { get; set; }
        public string time { get; set; }
        public string patient { get; set; }
        public string doctor { get; set; }
    }

    public class DoktoriIntervals
    {
        public string id { get; set; }
        public string date { get; set; }
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
    }
}
