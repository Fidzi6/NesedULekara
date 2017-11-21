using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

namespace NesedULekara_webapp
{
    public partial class AdminView : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //načíta zoznam lekárskych zameraní z databázy - prečíta originálne zameranie už registrovaných lekárov
            //ak je to prvý registrovaný lekár, tak musí manuálne napísať svoju špecifikáciu - dropDownList ostane skrytý!!!
        }

        //GPS generation
        protected void Button1_Click(object sender, EventArgs e)
        {
            gpsResultTxb.Text = null;
            GPSErrorTxb.Text = null;

            Adress adrs = new Adress();
            string adr = doctorCityTxb.Text + ", " + doctorAddressTxb.Text;
            adrs.Address = adr;
            while (adrs.Latitude == null || adrs.Longitude == null)
            {
                adrs.GeoCode();
                if (adrs.ex1 != null)
                {
                    GPSErrorTxb.Text = "Nefunguje pripojenie na Google geocode API, adresa sa nedá preložiť do GPS súradníc.";
                }
                if (adrs.ex2 != null)
                {
                    GPSErrorTxb.Text = "Neplatná adresa, skúste ju zadať znova.";
                }
            }
            gpsResultTxb.Text = adrs.Latitude.ToString() + ", " + adrs.Longitude.ToString();

            //var cnnString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            //using (SqlConnection conn = new SqlConnection(cnnString))
            //{
            //    using (SqlCommand cmd = new SqlCommand(@"INSERT INTO dbo.[???] VALUES (@c1, @c2)", conn))
            //    {
            //        cmd.Parameters.AddWithValue("@c1", doctorTitleTxb.Text); //titul
            //        cmd.Parameters.AddWithValue("@c2", doctorNameTxb.Text); //meno
            //        cmd.Parameters.AddWithValue("@c3", doctorSurnameTxb.Text); //priezvisko
            //        cmd.Parameters.AddWithValue("@c4", doctorPositionTxb.Text); //zaradenie
            //        cmd.Parameters.AddWithValue("@c5", doctorCityTxb.Text); //mesto
            //        cmd.Parameters.AddWithValue("@c6", doctorAddressTxb.Text); //adresa
            //        cmd.Parameters.AddWithValue("@c7", adrs.Latitude); //gps lattitude string
            //        cmd.Parameters.AddWithValue("@c8", adrs.Longitude); //gps longitude string
            //        cmd.Parameters.AddWithValue("@c9", 0); //hodnotenie - asi prázdna hodnota
            //        conn.Open();
            //        cmd.ExecuteNonQuery();
            //        conn.Close();
            //    }
            //}

            //doctorCityTxb.Text = null;
            //doctorAddressTxb.Text = null;
        }

        protected void logoutButton_Click(object sender, EventArgs e)
        {
            //rq1.Enabled = false;
            //rq2.Enabled = false;
            //rq3.Enabled = false;
            //rq4.Enabled = false;
            //rq5.Enabled = false;
            //rq6.Enabled = false;
            //rq7.Enabled = false;
            //rq8.Enabled = false;
            //rq9.Enabled = false;
            //rq10.Enabled = false;
            //rq11.Enabled = false;
            Response.Redirect("~/Default.aspx");
        }

        //new doctor registration
        protected void doctorRegister_Click(object sender, EventArgs e)
        {
            doctorRegistrationStatusTxb.Text = null;
            try
            {
                //get time range from doctorDayStart to doctorDayEnd
                DateTime start = DateTime.Parse(doctorDayStartTxb.Text);
                DateTime end = DateTime.Parse(doctorDayEndTxb.Text);
                TimeSpan range = end - start;

                //get datetimes of emergency and lunch
                DateTime emergencyStart = DateTime.Parse(doctorEmergencyStartTxb.Text);
                DateTime emergencyEnd = DateTime.Parse(doctorEmergencyEndTxb.Text);
                DateTime lunchStart = DateTime.Parse(doctorLunchStartTxb.Text);
                DateTime lunchEnd = DateTime.Parse(doctorLunchEndTxb.Text);
                TimeSpan emerg = emergencyEnd - emergencyStart;
                TimeSpan lunc = lunchEnd - lunchStart;

                int interval = Int32.Parse(doctorPacientTimeTxb.Text);

                //determine whether is possible to divide emergency and lunch into intervals
                List<string> emlun = new List<string>();
                if (emerg.TotalMinutes % interval == 0)
                {
                    if (lunc.TotalMinutes % interval == 0)
                    {

                    }
                    else
                    {
                        doctorRegistrationStatusTxb.Text += "Interval na obed sa nedá rozdeliť na definované časové úseky. Opravte si to!!!" + Environment.NewLine;
                    }
                }
                else
                {
                    doctorRegistrationStatusTxb.Text += "Interval pohotovosti sa nedá rozdeliť na definované časové úseky. Opravte si to!!!" + Environment.NewLine;
                }

                //determine whether it is possible to divide range into doctor-defined intervals for pacients
                if (range.TotalMinutes % interval == 0) //everything is ok
                {
                    double numOfIntervals = range.TotalMinutes / interval; //calculate number of intervals for pacients

                    //create list of start time intervals
                    List<string> startIntervals = new List<string>();
                    for (int i = 0; i < numOfIntervals; i++)
                    {
                        string s = start.AddMinutes(i * interval).ToString("HHmm");
                        //s += "_";
                        //s += start.AddMinutes((i + 1) * interval).ToString("HHmm");
                        startIntervals.Add(s);
                    }

                    //create new specific table
                    var cnnString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                    using (SqlConnection con = new SqlConnection(cnnString))
                    {
                        try
                        {
                            string command = @"CREATE TABLE ";
                            command += doctorEmailTxb.Text.Replace(".", "_"); //table name = dbo.doctor_email
                            command += "(date DATETIME";
                            for (int i = 0; i < numOfIntervals; i++) //cerate specific number of rows - depends on numberOfIntervals
                            {
                                command += ", t";
                                command += start.AddMinutes(i * interval).ToString("HHmm");
                                command += "_";
                                command += start.AddMinutes((i + 1) * interval).ToString("HHmm");
                                command += " VARCHAR(50)";
                            }
                            command += ");";

                            using (SqlCommand cmd = new SqlCommand(command, con))
                            {
                                con.Open();
                                cmd.ExecuteNonQuery();
                                con.Close();
                            }
                        }
                        catch (Exception ex)
                        {
                            doctorRegistrationStatusTxb.Text = ex.ToString();
                        }
                    }
                }
                else //cannot create good intervals - must check time range or pacient interval
                {
                    doctorRegistrationStatusTxb.Text += "Nie je možné vytvoriť časové intervaly pre pacientov. Skontrolujte si začiatok a koniec služby, prípadne interval pre pacienta." + Environment.NewLine;
                }
            }
            catch (Exception ex)
            {
                doctorRegistrationStatusTxb.Text = ex.ToString();
            }
        }
    }

    public class Adress
    {
        public Adress()
        {
        }
        //Properties
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string Address { get; set; }
        public Exception ex1;
        public Exception ex2;

        //The Geocoding here i.e getting the latt/long of address
        public void GeoCode()
        {
            //to Read the Stream
            StreamReader sr = null;

            //The Google Maps API Either return JSON or XML. We are using XML Here
            //Saving the url of the Google API 
            string url = String.Format("http://maps.googleapis.com/maps/api/geocode/xml?address=" +
            this.Address + "&sensor=false");

            //to Send the request to Web Client 
            WebClient wc = new WebClient();
            try
            {
                sr = new StreamReader(wc.OpenRead(url));
            }
            catch (Exception ex)
            {
                //throw new Exception("The Error Occured" + ex.Message);
                ex1 = ex;
            }

            try
            {
                XmlTextReader xmlReader = new XmlTextReader(sr);
                bool latread = false;
                bool longread = false;

                while (xmlReader.Read())
                {
                    xmlReader.MoveToElement();
                    switch (xmlReader.Name)
                    {
                        case "lat":

                            if (!latread)
                            {
                                xmlReader.Read();
                                this.Latitude = xmlReader.Value.ToString();
                                latread = true;

                            }
                            break;
                        case "lng":
                            if (!longread)
                            {
                                xmlReader.Read();
                                this.Longitude = xmlReader.Value.ToString();
                                longread = true;
                            }

                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                //throw new Exception("An Error Occured" + ex.Message);
                ex2 = ex;
            }
        }
    }

}