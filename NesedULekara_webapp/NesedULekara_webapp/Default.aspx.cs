using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Configuration;

namespace NesedULekara_webapp
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void signInButton_Click(object sender, EventArgs e)
        {
            //Response.Redirect("~/DoctorView.aspx");
            if (loginTextBox.Text != null && passwordTextBox.Text != null)
            {
                //get login and password from textboxs
                string login = loginTextBox.Text;
                string pass = passwordTextBox.Text;

                //try to perform login - only if entered values are correct (login exists and password is good)
                try
                {
                    int right = 0;
                    //precita heslo z tab. kde meno = MENO
                    var cnnString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                    using (SqlConnection conn = new SqlConnection(cnnString))
                    {
                        var cmd = conn.CreateCommand();
                        cmd.CommandText = @"SELECT s.rights FROM dbo.[login] AS s WHERE s.login = @c1 AND s.password = @c2";
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
                                right = reader.GetInt32(0);
                            }
                        }
                        conn.Close();
                    }
                    //Application["meno"] = meno;
                    if (right == 1) Response.Redirect("~/AdminView.aspx");
                    if (right == 2) Response.Redirect("~/DoctorView.aspx");
                }
                catch
                {
                    loginError.Text = "Nepodarilo sa prihlásiť. Skontrolujte si svoje prihlasovacie údaje.";
                }
            }
            

        }

        //test CREATE TABLE
        protected void testButton_Click(object sender, EventArgs e)
        {
            var cnnString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(cnnString))
            {

                try
                {
                    //
                    // Open the SqlConnection.
                    //
                    con.Open();
                    //
                    // The following code uses an SqlCommand based on the SqlConnection.
                    //
                    using (SqlCommand command = new SqlCommand("CREATE TABLE doktor1(stlpec1 char(50), stlpec2 int, stlpec3 datetime);", con))
                        command.ExecuteNonQuery();
                    con.Close();
                }
                catch (Exception ex)
                {
                    
                }
            }
        }

        //test INSERT
        protected void insertToDB_Click(object sender, EventArgs e)
        {
            var cnnString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(cnnString))
            {
                using (SqlCommand cmd = new SqlCommand(@"INSERT INTO dbo.[testt] (stlpec1, stlpec2) VALUES (@c1, @c2)", conn))
                {
                    cmd.Parameters.AddWithValue("@c1", "serus");
                    cmd.Parameters.AddWithValue("@c2", 102);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

        //test select
        protected void readFromDB_Click(object sender, EventArgs e)
        {
            string v1 = null;
            int v2 = 0;
            var cnnString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(cnnString))
            {
                using (SqlCommand cmd = new SqlCommand(@"SELECT s.stlpec1, s.stlpec2 FROM dbo.[testt] AS s WHERE s.id=5", conn))
                {
                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            v1 = reader.GetString(0);
                            v2 = reader.GetInt32(1);
                        }
                    }
                    conn.Close();
                }
            }
            stringLabel.Text = v1;
            intLabel.Text = v2.ToString();
        }
    }
}