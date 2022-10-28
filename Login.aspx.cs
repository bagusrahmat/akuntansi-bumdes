using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;

namespace bumdesAPP
{
    public partial class Login : System.Web.UI.Page
    {
        static int hl = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            loginBtn.BackColor = System.Drawing.Color.FromArgb(29, 161, 242);
            if (!IsPostBack)
            {
                hl = 0;
                LoginStatus.Visible = false;
                //Label1.Visible = false;
            }
            loginBtn.Enabled = true;
        }
        protected void Timer1_Tick(object sender, EventArgs e)
        {
            TimeSpan time1 = new TimeSpan();
            time1 = (DateTime)Session["time"] - DateTime.Now;
            if (time1.Seconds == 0)
            {
                Label1.Text = "TimeOut!";
                Label1.Visible = false;
                hl = 0;
                Response.Redirect("Login.aspx");
                Session["time"] = null;
            }
            else if (time1.Seconds >= 0)
            {
                Label1.Text = "Anda sudah mencoba login lebih dari 3 kali. Silahkan coba login kembali dalam " + time1.Seconds.ToString();
            }

        }

        protected void loginBtn_Click(object sender, EventArgs e)
        {
            
            SqlConnection koneksi = new SqlConnection(WebConfigurationManager.ConnectionStrings["koneksi"].ToString());
            koneksi.Open();
            string query = "select count(*) from userdata where username='" + username.Text + "' and password='" + password.Text + "'";

            SqlCommand command = new SqlCommand(query, koneksi);
            string output = command.ExecuteScalar().ToString();

            if (output == "1" && hl <=3)
            {
                Session["user"] = username.Text;
                Response.Redirect("~/hlm/home.aspx");
            }

            else if (hl >= 3 && username.Text!="" && password.Text!="")
            {
                //Response.Write(@"<script>alert('Username dan Password 3x Salah.')</script>");
                Label1.Text = "Anda sudah mencoba login lebih dari 3 kali.";
                Session["time"] = DateTime.Now.AddSeconds(59);
                Label1.Visible = true;
                loginBtn.BackColor = System.Drawing.Color.FromArgb(180, 180, 180);
                loginBtn.Enabled = false;
            }
            else if (username.Text=="" && password.Text=="")
                Label1.Text = "Username dan/atau password harus diisi.";

            else if (username.Text == "" || password.Text == "")
                Label1.Text = "Username dan/atau password harus diisi.";

            else if (output!= "1")
            {
                hl++;
                Label1.Visible = true;
                Label1.Text = "Username & Password Tidak Sesuai. (" + hl + ")";
            }
        }
    }
}