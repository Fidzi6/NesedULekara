using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace NesedULekara_webapp
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //var cnnString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            //using (SqlConnection conn = new SqlConnection(cnnString))
            //{
            //    using (SqlCommand cmd = new SqlCommand(@"INSERT INTO dbo.[TestLogin] (login, password, rights) VALUES (@c1, @c2, @c3)", conn))
            //    {
            //        cmd.Parameters.AddWithValue("@c1", "feri"); //name
            //        cmd.Parameters.AddWithValue("@c2", "feri10"); //surname
            //        cmd.Parameters.AddWithValue("@c3", 2); //email
            //        conn.Open();
            //        cmd.ExecuteNonQuery();
            //        conn.Close();
            //    }
            //}




            //Test
            //write new doctor data to table dbo.doctors

        //    try
        //    {
        //        string message = string.Empty;
        //        var cnnString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        //        using (SqlConnection conn = new SqlConnection(cnnString))
        //        {
        //            using (SqlCommand cmd = new SqlCommand(@"INSERT INTO dbo.[doctors] (name, surname, email, specialization, tel, adress, latitude, longtude, emergency, lunch, start, interval, rating, superEnd) VALUES (@c1, @c2, @c3, @c4, @c5, @c6, @c7, @c8, @c9, @c10, @c11, @c12, @c13, @c14)", conn))
        //            {
        //                cmd.Parameters.AddWithValue("@c1", "DoktorMeno"); //name
        //                cmd.Parameters.AddWithValue("@c2", "DoktorMeno2"); //surname
        //                cmd.Parameters.AddWithValue("@c3", "emilX"); //email
        //                cmd.Parameters.AddWithValue("@c4", "special"); //function
        //                cmd.Parameters.AddWithValue("@c5", "095059"); //tel
        //                cmd.Parameters.AddWithValue("@c6", "ke" + ", " + "ff"); //adress
        //                cmd.Parameters.AddWithValue("@c7", "54654"); //latitude
        //                cmd.Parameters.AddWithValue("@c8", "456456"); //longitude
        //                cmd.Parameters.AddWithValue("@c9", "9"); //emergency
        //                cmd.Parameters.AddWithValue("@c10", "12"); //lunch
        //                cmd.Parameters.AddWithValue("@c11", "7"); //lunch
        //                cmd.Parameters.AddWithValue("@c12", "3"); //lunch
        //                cmd.Parameters.AddWithValue("@c13", "30"); //lunch
        //                cmd.Parameters.AddWithValue("@c14", "10");
        //                conn.Open();
        //                cmd.ExecuteNonQuery();
        //                conn.Close();
        //            }
        //        }

        //        //write login and password to table dbo.login
        //        using (SqlConnection conn = new SqlConnection(cnnString))
        //        {
        //            int userId = 0;
        //            using (SqlCommand cmd = new SqlCommand("login_user"))
        //            {
        //                using (SqlDataAdapter sda = new SqlDataAdapter())
        //                {
        //                    cmd.CommandType = CommandType.StoredProcedure;
        //                    cmd.Parameters.AddWithValue("@login", "emil15415@g.sk");
        //                    cmd.Parameters.AddWithValue("@password", "heslo");
        //                    cmd.Parameters.AddWithValue("@rights", 2);
        //                    cmd.Connection = conn;
        //                    conn.Open();
        //                    userId = Convert.ToInt32(cmd.ExecuteScalar());
        //                    conn.Close();
        //                }
        //            }

        //            switch (userId)
        //            {
        //                case -1:
        //                    message = "Username already exists.\\nPlease choose a different username.";
        //                    break;
        //                default:
        //                    message = "Registration successful.\\nUser Id: " + userId.ToString();
        //                    break;
        //            }
        //            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('" + message + "');", true);

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        string s = ex.Message;
        //    }
        }

        protected void signInButton_Click(object sender, EventArgs e)
        {
            //Response.Redirect("~/DoctorView.aspx");
            if (loginTextBox.Text != null && passwordTextBox.Text != null)
            {
                //get login and password from textboxs
                string login = loginTextBox.Text;
                string pass = passwordTextBox.Text;
                Application["login"] = login;

                //try to perform login - only if entered values are correct (login exists and password is good)
                try
                {
                    double right = 0;
                    //precita heslo z tab. kde meno = MENO
                    var cnnString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                    using (SqlConnection conn = new SqlConnection(cnnString))
                    {
                        var cmd = conn.CreateCommand();
                        cmd.CommandText = @"SELECT rights FROM dbo.[login] WHERE login = @c1 AND password = @c2";
                        cmd.Parameters.AddWithValue("@c1", login);
                        cmd.Parameters.AddWithValue("@c2", pass);
                        conn.Open();
                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows == false)
                            {
                                loginError.Text = "Nepodarilo sa prihlásiť. Skontrolujte si svoje prihlasovacie údaje.";
                            }
                            while (reader.Read())
                            {
                                right = reader.GetDouble(0);
                            }
                        }
                        conn.Close();
                    }
                    //Application["meno"] = meno;
                    if (right <= 1.1) Response.Redirect("~/AdminView.aspx");
                    if (right <= 2.1) Response.Redirect("~/DoctorView.aspx");
                }
                catch(Exception ex)
                {
                    loginError.Text = "Nepodarilo sa prihlásiť. Skontrolujte si svoje prihlasovacie údaje.";
                }
            }
            

        }

        ////test CREATE TABLE
        //protected void testButton_Click(object sender, EventArgs e)
        //{
        //    var cnnString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        //    using (SqlConnection con = new SqlConnection(cnnString))
        //    {

        //        try
        //        {
        //            //
        //            // Open the SqlConnection.
        //            //
        //            con.Open();
        //            //
        //            // The following code uses an SqlCommand based on the SqlConnection.
        //            //
        //            using (SqlCommand command = new SqlCommand("CREATE TABLE doktor1(stlpec1 char(50), stlpec2 int, stlpec3 datetime);", con))
        //                command.ExecuteNonQuery();
        //            con.Close();
        //        }
        //        catch (Exception ex)
        //        {
                    
        //        }
        //    }
        //}

        ////test INSERT
        //protected void insertToDB_Click(object sender, EventArgs e)
        //{
        //    var cnnString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        //    using (SqlConnection conn = new SqlConnection(cnnString))
        //    {
        //        using (SqlCommand cmd = new SqlCommand(@"INSERT INTO dbo.[testt] (stlpec1, stlpec2) VALUES (@c1, @c2)", conn))
        //        {
        //            cmd.Parameters.AddWithValue("@c1", "serus");
        //            cmd.Parameters.AddWithValue("@c2", 102);
        //            conn.Open();
        //            cmd.ExecuteNonQuery();
        //            conn.Close();
        //        }
        //    }
        //}

        ////test select
        //protected void readFromDB_Click(object sender, EventArgs e)
        //{
        //    string v1 = null;
        //    int v2 = 0;
        //    var cnnString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        //    using (SqlConnection conn = new SqlConnection(cnnString))
        //    {
        //        using (SqlCommand cmd = new SqlCommand(@"SELECT s.stlpec1, s.stlpec2 FROM dbo.[testt] AS s WHERE s.id=5", conn))
        //        {
        //            conn.Open();
        //            using (var reader = cmd.ExecuteReader())
        //            {
        //                while (reader.Read())
        //                {
        //                    v1 = reader.GetString(0);
        //                    v2 = reader.GetInt32(1);
        //                }
        //            }
        //            conn.Close();
        //        }
        //    }
        //    stringLabel.Text = v1;
        //    intLabel.Text = v2.ToString();
        //}
    }
}