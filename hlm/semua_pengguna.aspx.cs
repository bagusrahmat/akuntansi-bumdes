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
    public partial class semua_pengguna : System.Web.UI.Page
    {
        SqlConnection koneksi = new SqlConnection(WebConfigurationManager.ConnectionStrings["koneksi"].ToString());
        protected void Page_Load(object sender, EventArgs e)
        {
            dataUser();
            lblbantuUsername.Visible = false;
        }
        protected void dataUser()
        {
            SqlCommand cmd = new SqlCommand("select row_number() OVER (ORDER BY namaLengkap, username, levelUser) nom, username, namaLengkap, levelUser from userdata order by namaLengkap", koneksi);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            koneksi.Close();
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }
        protected void tambah_Click(object sender, EventArgs e)
        {
            Table1.Visible = true;
            ButtonBatal.Visible = true;
            ButtonTambah.Enabled = false;
            ButtonTambah.ForeColor = System.Drawing.Color.FromArgb(29, 161, 242);
            ButtonTambah.BackColor = System.Drawing.Color.White;
        }
        protected void batal_Click(object sender, EventArgs e)
        {
            Response.Redirect("semua_pengguna.aspx");
        }
        protected void simpanDataUser_Click(object sender, EventArgs e)
        {
            if (namaTextbox.Text == "") Response.Write("<script>alert('Field Wajib Diisi!')</script>");
            else if (usernameTextBox.Text == "") Response.Write("<script>alert('Field Wajib Diisi!')</script>");
            else if (DropDownLevelUser.Text == "-- Pilih Level Pengguna --") Response.Write("<script>alert('Field Wajib Diisi!')</script>");
            else if (passwordTextBox.Text == "") Response.Write("<script>alert('Field Wajib Diisi!')</script>");
            else
            {
                string namaLengkap = namaTextbox.Text.ToString();
                string username = usernameTextBox.Text.ToString();
                string levelUser = DropDownLevelUser.SelectedItem.ToString();
                string password = passwordTextBox.Text.ToString();
                if (ButtonTambah.Text == "Tambah") saveData(namaLengkap, username, levelUser, password);
                else if (ButtonTambah.Text == "Edit") saveUpdateData(namaLengkap, username, levelUser, password);
                Response.Write("<script>alert('Data Tersimpan')</script>");
                Response.Redirect("semua_pengguna.aspx");
            }
        }
        
        protected void linkEdit_Click(object sender, EventArgs e)
        {
            Table1.Visible = true;
            ButtonBatal.Visible = true;
            ButtonTambah.Enabled = false;
            ButtonTambah.ForeColor = System.Drawing.Color.FromArgb(29, 161, 242);
            ButtonTambah.BackColor = System.Drawing.Color.White;
            ButtonTambah.Text = "Edit";
            string username = (sender as LinkButton).CommandArgument;
            koneksi.Open();
            string query = "select * from userdata where username = '" + username + "'";
            SqlCommand cmd = new SqlCommand(query, koneksi);
            cmd.CommandType = CommandType.Text;
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                usernameTextBox.Text = dr["username"].ToString();
                namaTextbox.Text = dr["namaLengkap"].ToString();
                DropDownLevelUser.Text = dr["levelUser"].ToString();
                passwordTextBox.Text = dr["password"].ToString();
            }
            dr.Close();
            lblbantuUsername.Text = username;
        }
        private void saveData(string namaLengkap, string username, string levelUser, string password)
        {
            string query = "insert into userdata(username, namaLengkap, password, levelUser) values('" + username + "', '" + namaLengkap + "', '" + password + "', '" + levelUser + "')";
            String mycon = WebConfigurationManager.ConnectionStrings["koneksi"].ToString();
            SqlConnection con = new SqlConnection(mycon);
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = query;
            cmd.Connection = con;
            cmd.ExecuteNonQuery();
            con.Close();
        }
        private void saveUpdateData(string namaLengkap, string username, string levelUser, string password)
        {
            string query = "update userdata set username = '" + username + "', namaLengkap = '" + namaLengkap + "', levelUser = '" + levelUser + "', password = '" + password + "' where username = '" + lblbantuUsername.Text + "'";
            String mycon = WebConfigurationManager.ConnectionStrings["koneksi"].ToString();
            SqlConnection con = new SqlConnection(mycon);
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = query;
            cmd.Connection = con;
            cmd.ExecuteNonQuery();
            con.Close();
        }
        protected void linkHapus_Click(object sender, EventArgs e)
        {
            string username = (sender as LinkButton).CommandArgument;
            SqlCommand cmd = new SqlCommand("delete from userdata where username = '" + username + "'", koneksi);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            koneksi.Close();
            Response.Redirect("semua_pengguna.aspx");
        }
    }
}