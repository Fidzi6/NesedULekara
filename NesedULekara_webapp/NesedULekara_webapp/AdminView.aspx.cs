using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
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
        private Adress adrs;
        private static string lat;
        private static string longit;
        //private static int c = 0;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            //načíta zoznam lekárskych zameraní z databázy - prečíta originálne zameranie už registrovaných lekárov
            //ak je to prvý registrovaný lekár, tak musí manuálne napísať svoju špecifikáciu - dropDownList ostane skrytý!!!
            //DropDownList1.Items.Clear();
            var cnnString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(cnnString))
            {
                using (SqlCommand cmd = new SqlCommand(@"SELECT DISTINCT s.specialization FROM dbo.[doctors] AS s", conn))
                {
                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                DropDownList1.Items.Add(reader.GetString(i));
                            }
                        }
                    }
                    conn.Close();
                }
            }
        }

        //GPS generation
        protected void Button1_Click(object sender, EventArgs e)
        {
            gpsResultTxb.Text = null;
            GPSErrorTxb.Text = null;

            adrs = new Adress();
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
            lat = adrs.Latitude.ToString();
            longit = adrs.Longitude.ToString();
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
            string message = null;
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
                List<string> startIntervals = new List<string>();
                List<string> eList = new List<string>();
                List<string> lList = new List<string>();

                //determine whether is possible to divide total interval, emergency interval and lunch interval into intervals
                bool status = true;
                bool status2 = true;
                
                if (range.TotalMinutes % interval != 0)
                {
                    message += "Nie je možné vytvoriť časové intervaly pre pacientov. Skontrolujte si začiatok a koniec služby, prípadne interval pre pacienta." + Environment.NewLine + "----------" + Environment.NewLine;
                    status = false;
                    status2 = false;
                }
                if (emerg.TotalMinutes % interval != 0)
                {
                    message += "Interval pohotovosti sa nedá rozdeliť na definované časové úseky. Opravte si to!!!" + Environment.NewLine + "----------" + Environment.NewLine;
                    status = false;
                    status2 = false;
                }
                if (lunc.TotalMinutes % interval != 0)
                {
                    message += "Interval na obed sa nedá rozdeliť na definované časové úseky. Opravte si to!!!" + Environment.NewLine + "----------" + Environment.NewLine;
                    status = false;
                    status2 = false;
                }
                if (start > emergencyStart)
                {
                    message += "Pohotovosť začína ešte pred začatím služby. Opravte si to!!!" + Environment.NewLine + "----------" + Environment.NewLine;
                    status = false;
                    status2 = false;
                }
                if (start > lunchStart)
                {
                    message += "Obed začína ešte pred začatím služby. Opravte si to!!!" + Environment.NewLine + "----------" + Environment.NewLine;
                    status = false;
                    status2 = false;
                }
                if (end < emergencyEnd)
                {
                    message += "Pohotovosť končí až po ukončení služby. Opravte si to!!!" + Environment.NewLine + "----------" + Environment.NewLine;
                    status = false;
                    status2 = false;
                }
                if (end < lunchEnd)
                {
                    message += "Obed končí až po ukončení služby. Opravte si to!!!" + Environment.NewLine + "----------" + Environment.NewLine;
                    status = false;
                    status2 = false;
                }

                if (status) //it is possile to create intervals, check whether is possible to delete emergency and lunch intervals from complete intervals list
                {
                    //calculate number of all intervals for pacients
                    double numOfIntervals = range.TotalMinutes / interval;

                    if (numOfIntervals > 25)
                    {
                        message += "Príliš veľa intervalov. Zmeň si dĺžku intervalu.";
                        status2 = false;
                    } 

                    ////create list of start time intervals
                    //for (int i = 0; i < numOfIntervals; i++)
                    //{
                    //    string s = start.AddMinutes(i * interval).ToString("HHmm");
                    //    //s += "_";
                    //    //s += start.AddMinutes((i + 1) * interval).ToString("HHmm");
                    //    startIntervals.Add(s);
                    //}

                        ////calculate number of intervals for emergency
                        //double numOfELIntervals = emerg.TotalMinutes / interval;

                        ////add emergency intervals to list
                        //for (int i = 0; i < numOfELIntervals; i++)
                        //{
                        //    eList.Add(emergencyStart.AddMinutes(i * interval).ToString("HHmm"));
                        //}

                        ////calculate number of intervals for lunch
                        //numOfELIntervals = lunc.TotalMinutes / interval;

                        ////add lunch intervals to list
                        //for (int i = 0; i < numOfELIntervals; i++)
                        //{
                        //    lList.Add(lunchStart.AddMinutes(i * interval).ToString("HHmm"));
                        //}

                        ////try to delete emergency intervals from complete list
                        //for (int i = 0; i < eList.Count; i++)
                        //{
                        //    try
                        //    {
                        //        startIntervals.Remove(eList[i]);
                        //    }
                        //    catch
                        //    {
                        //        message += "Boli vložené nesprávne intervaly pre pohotovosť. Opravte si to!!!" + Environment.NewLine + "----------" + Environment.NewLine;
                        //        status2 = false;
                        //        break;
                        //    }
                        //}

                        ////try to delete lunch intervals from complete list
                        //for (int i = 0; i < lList.Count; i++)
                        //{
                        //    try
                        //    {
                        //        startIntervals.Remove(lList[i]);
                        //    }
                        //    catch
                        //    {
                        //        message += "Boli vložené nesprávne intervaly pre obed. Opravte si to!!!" + Environment.NewLine + "----------" + Environment.NewLine;
                        //        status2 = false;
                        //        break;
                        //    }
                        //}
                }
                else //cannot create good intervals - must check time range or pacient interval
                {
                    doctorRegistrationStatusTxb.Text = message;
                }

                if (status2) //all previous steps are correct, continue to table creation + write doctor data to table
                {
                    bool hyperend = false;
                    //create string for whole emergency and lunch interval
                    string doctorEmergency = emergencyStart.ToString("HHmm") + "_" + emergencyEnd.ToString("HHmm");
                    string doctorLunch = lunchStart.ToString("HHmm") + "_" + lunchEnd.ToString("HHmm");
                    var cnnString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

                    //write login and password to table dbo.login
                    using (SqlConnection conn = new SqlConnection(cnnString))
                    {
                        string userId = "-1";
                        using (SqlCommand cmd = new SqlCommand("login_user"))
                        {
                            using (SqlDataAdapter sda = new SqlDataAdapter())
                            {
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("@login", doctorEmailTxb.Text);
                                cmd.Parameters.AddWithValue("@password", doctorPasswordTxb.Text);
                                cmd.Parameters.AddWithValue("@rights", 2);
                                cmd.Connection = conn;
                                conn.Open();
                                userId = cmd.ExecuteScalar().ToString();
                                conn.Close();
                            }
                        }

                        switch (userId)
                        {
                            case "-1":
                                message = "Username already exists.\\nPlease choose a different username.";
                                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('" + message + "');", true);
                                hyperend = true;
                                break;
                            default:
                                message = "Registration successful.";
                                break;
                        }


                        if (hyperend)
                            return;

                        //using (SqlCommand cmd = new SqlCommand(@"INSERT INTO dbo.[login] (login, password, rights) VALUES (@c1, @c2, 2)", conn))
                        //{
                        //    cmd.Parameters.AddWithValue("@c1", doctorEmailTxb.Text);
                        //    cmd.Parameters.AddWithValue("@c2", doctorPasswordTxb.Text);
                        //    conn.Open();
                        //    cmd.ExecuteNonQuery();
                        //    conn.Close();
                        //}
                    }



                    //write new doctor data to table dbo.doctors
                    using (SqlConnection conn = new SqlConnection(cnnString))
                    {
                        using (SqlCommand cmd = new SqlCommand(@"INSERT INTO dbo.[doctors] (name, surname, email, specialization, tel, adress, latitude, longtude, emergency, lunch, start, interval, superEnd, rating) VALUES (@c1, @c2, @c3, @c4, @c5, @c6, @c7, @c8, @c9, @c10, @c11, @c12, @c13, @c14)", conn))
                        {
                            cmd.Parameters.AddWithValue("@c1", doctorNameTxb.Text); //name
                            cmd.Parameters.AddWithValue("@c2", doctorSurnameTxb.Text); //surname
                            cmd.Parameters.AddWithValue("@c3", doctorEmailTxb.Text); //email
                            cmd.Parameters.AddWithValue("@c4", doctorPositionTxb.Text); //function
                            cmd.Parameters.AddWithValue("@c5", doctorTelephoneTxb.Text); //tel
                            cmd.Parameters.AddWithValue("@c6", doctorCityTxb.Text + ", " + doctorAddressTxb.Text); //adress
                            cmd.Parameters.AddWithValue("@c7", lat); //latitude
                            cmd.Parameters.AddWithValue("@c8", longit); //longitude
                            cmd.Parameters.AddWithValue("@c9", doctorEmergency); //emergency
                            cmd.Parameters.AddWithValue("@c10", doctorLunch); //lunch
                            cmd.Parameters.AddWithValue("@c11", doctorDayStartTxb.Text); //lunch
                            cmd.Parameters.AddWithValue("@c12", doctorPacientTimeTxb.Text); //lunch
                            cmd.Parameters.AddWithValue("@c13", doctorDayEndTxb.Text); //lunch
                            cmd.Parameters.AddWithValue("@c14", "0");
                            conn.Open();
                            cmd.ExecuteNonQuery();
                            conn.Close();
                        }
                    }

                    

                    ////create new specific table
                    //using (SqlConnection con = new SqlConnection(cnnString))
                    //{
                    //    try
                    //    {
                    //        string command = @"CREATE TABLE ";
                    //        command += doctorEmailTxb.Text.Replace(".", "_"); //table name = dbo.doctor_email
                    //        command += "(date DATETIME";
                    //        for (int i = 0; i < startIntervals.Count; i++) //cerate specific number of rows - depends on numberOfIntervals
                    //        {
                    //            command += ", t";
                    //            command += startIntervals[i];// start.AddMinutes(i * interval).ToString("HHmm");
                    //            command += "_";
                    //            string sub1 = startIntervals[i].Substring(0, 2);
                    //            string sub2 = startIntervals[i].Substring(2, 2);
                    //            string sub = sub1 + ":" + sub2;
                    //            DateTime dt = DateTime.Parse(sub);
                    //            //int ii = Int32.Parse(startIntervals[i]);
                    //            command += dt.AddMinutes(interval).ToString("HHmm"); //(ii + interval).ToString(); //start.AddMinutes((i + 1) * interval).ToString("HHmm");
                    //            command += " VARCHAR(50)";
                    //        }
                    //        command += ");";

                    //        using (SqlCommand cmd = new SqlCommand(command, con))
                    //        {
                    //            con.Open();
                    //            cmd.ExecuteNonQuery();
                    //            con.Close();
                    //        }

                    //        doctorRegistrationStatusTxb.Text = "Registrácia úspešná.";
                    //    }
                    //    catch (Exception ex)
                    //    {
                    //        doctorRegistrationStatusTxb.Text = ex.ToString();
                    //    }
                    //}
                }
                else //wrong emergency or lunch intervals was inserted
                {
                    doctorRegistrationStatusTxb.Text = message;
                }
            }
            catch (Exception ex)
            {
                doctorRegistrationStatusTxb.Text = ex.ToString();
            }
        }

        //
        protected void DropDownList1_Init(object sender, EventArgs e)
        {
            //doctorPositionTxb.Text = DropDownList1.Text;
        }

        protected void DropDownList1_TextChanged(object sender, EventArgs e)
        {
            //doctorPositionTxb.Text = DropDownList1.Text;
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            doctorPositionTxb.Text = DropDownList1.Text;
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