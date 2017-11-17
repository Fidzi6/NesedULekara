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

        //registration of a new doctor
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
                if(adrs.ex1 != null)
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
            Response.Redirect("~/Default.aspx");
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