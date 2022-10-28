using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;

namespace bumdesAPP.hlm
{
    public partial class Site1 : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }

            SqlConnection koneksi = new SqlConnection(WebConfigurationManager.ConnectionStrings["koneksi"].ToString());
            koneksi.Open();
            string cekunit = "select namaLengkap, levelUser from userdata where username='" + Session["user"].ToString() + "'";
            SqlCommand cmd_cek = new SqlCommand(cekunit, koneksi);
            SqlDataReader dread = cmd_cek.ExecuteReader();
            dread.Read();
            string namaLengkap = dread["namalengkap"].ToString();
            string levelUser = dread["levelUser"].ToString();
            dread.Close();
            koneksi.Close();

            whoami.Text = namaLengkap;
            if (levelUser == "User")
            {
                navlink_pengguna.Visible = false;
                navlink_inputjurnal.Visible = false;
            }
        }

        protected void logoutbtn_Click(object sender, EventArgs e)
        {
            Session.Remove("user");
            Response.Redirect("~/login.aspx");
        }
        protected void Home_Click(object sender, EventArgs e)
        {
            Response.Redirect("home.aspx");
        }
        
    }
}