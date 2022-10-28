using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Xml.Linq;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using System.IO;

namespace bumdesAPP.hlm
{
    public partial class transaksi : System.Web.UI.Page
    {
        SqlConnection koneksi = new SqlConnection(WebConfigurationManager.ConnectionStrings["koneksi"].ToString());
        string jenisTransaksi;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }
            else
            {
                koneksi.Open();
                string cekleveluser = "select levelUser from userdata where username='" + Session["user"].ToString() + "'";
                SqlCommand cmd_cek = new SqlCommand(cekleveluser, koneksi);
                SqlDataReader dread = cmd_cek.ExecuteReader();
                dread.Read();
                string leveluser = dread["levelUser"].ToString();
                dread.Close();

                if (leveluser == "User")
                {
                    tambahTransaksi.Visible = false;
                    GridView1.Columns[4].Visible = false;
                }
                koneksi.Close();
            }
            dataTransaksi();
            lblperiode.Visible = true;
            tanggalTransaksiTextBox.Text = DateTime.Today.ToString("yyyy-MM-dd");
            if(!IsPostBack)
            {
                //ButtonPrint.Enabled = false;
                //ButtonPrint.ForeColor = System.Drawing.Color.FromArgb(200, 200, 200);
                //ButtonPrint.BorderColor = System.Drawing.Color.FromArgb(200, 200, 200);
                lblperiode.Text = "TRANSAKSI S/D " + DateTime.Today.ToString("dd MMM yyyy");
                TextBoxPeriode2.Text = DateTime.Today.ToString("yyyy-MM-dd");
            }
            LabelHalaman.Text = "Menampilkan halaman " + (GridView1.PageIndex+1).ToString() + " dari " + GridView1.PageCount.ToString() + "";
            if (Table1.Visible == false)
            {
                spasi1.Visible = false;
                spasi2.Visible = false;
            }
        }
        
        public void CheckBoxJP_Changed(object sender, EventArgs e)
        {
            koneksi.Open();
            string query = "select isnull(max(transaksiID),0) + 1 as transaksiID from transaksi";
            string transaksiID;
            SqlCommand cmd = new SqlCommand(query, koneksi);
            cmd.CommandType = CommandType.Text;
            SqlDataReader dr = cmd.ExecuteReader();
            dr.Read();
            transaksiID = dr["transaksiID"].ToString();
            dr.Close();
            koneksi.Close();
            if (CheckBoxJP.Checked == false)
            {
                if (transaksiID == "1") noTransaksiTextBox.Text = "T-0001";
                else noTransaksiTextBox.Text = "T-" + transaksiID;
            }
            else
            {
                if (transaksiID == "1") noTransaksiTextBox.Text = "JP-0001";
                else noTransaksiTextBox.Text = "JP-" + transaksiID;
            }
        }
        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            dataTransaksi();
            LabelHalaman.Text = "Menampilkan halaman " + (GridView1.PageIndex+1).ToString() + " dari " + GridView1.PageCount.ToString() + "";
        }
        private void dataTransaksi()
        {
            SqlCommand cmd = new SqlCommand("select transaksiID, Convert(varchar(10),tanggal,5) as tanggal, noTransaksi, deskripsi, nominal from transaksi order by Convert(date,tanggal,5) desc , noTransaksi desc", koneksi);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            koneksi.Close();
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }
        protected void lihatTransaksiPeriode_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("select transaksiID, tanggal, noTransaksi, deskripsi, nominal from transaksi where tanggal between '" + TextBoxPeriode1.Text + "' and '" + TextBoxPeriode2.Text + "' order by Convert(date,tanggal,5) desc , noTransaksi desc", koneksi); ;
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            koneksi.Close();
            GridView1.DataSource = dt;
            GridView1.DataBind();
            if (TextBoxPeriode1.Text == "") lblperiode.Text="TRANSAKSI S/D " + Convert.ToDateTime(TextBoxPeriode2.Text).ToString("dd MMM yyyy") + "";
            else lblperiode.Text = "TRANSAKSI PERIODE " + Convert.ToDateTime(TextBoxPeriode1.Text).ToString("dd MMM yyyy") + " S/D " + Convert.ToDateTime(TextBoxPeriode2.Text).ToString("dd MMM yyyy");
            ButtonPrint.ForeColor = System.Drawing.Color.FromArgb(29, 161, 242);
            ButtonPrint.BorderColor = System.Drawing.Color.FromArgb(29, 161, 242);
            if (GridView1.Rows.Count == 0) ButtonPrint.Visible = false;
            else
            {
                ButtonPrint.Visible = true;
                ButtonPrint.Enabled = true;
            }
        }
        protected void tambahTransaksi_Click(object sender, EventArgs e)
        {
            jenisTransaksi = "T-";
            Table2.Visible = false;
            ButtonPrint.Visible = false;
            koneksi.Open();
            string query = "select isnull(max(transaksiID),0) + 1 as transaksiID from transaksi";
            string transaksiID;
            SqlCommand cmd = new SqlCommand(query, koneksi);
            cmd.CommandType = CommandType.Text;
            SqlDataReader dr = cmd.ExecuteReader();
            dr.Read();
            transaksiID = dr["transaksiID"].ToString();
            dr.Close();
            koneksi.Close();
            if (transaksiID == "1") noTransaksiTextBox.Text = "T-0001";
            else noTransaksiTextBox.Text = "T-" + transaksiID;

            cancelTransaksi.Visible = true;
            tambahTransaksi.ForeColor = System.Drawing.Color.FromArgb(29, 161, 242);
            tambahTransaksi.BackColor = System.Drawing.Color.White;
            tambahTransaksi.Enabled = false;
            Table1.Visible = true;
        }
        protected void linkEdit_Click(object sender, EventArgs e)
        {
            cancelTransaksi.Visible = true;
            Table1.Visible = true;
            tambahTransaksi.Text = "Edit";
            tambahTransaksi.ForeColor = System.Drawing.Color.White;
            tambahTransaksi.BackColor = System.Drawing.Color.FromArgb(29, 161, 242);
            koneksi.Open();
            int transaksiID = Convert.ToInt32((sender as LinkButton).CommandArgument);
            string query = "select * from transaksi where transaksiID = '" + transaksiID + "'";
            SqlCommand cmd = new SqlCommand(query, koneksi);
            cmd.CommandType = CommandType.Text;
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                tanggalTransaksiTextBox.Text = Convert.ToDateTime(dr["tanggal"]).ToString("yyyy-MM-dd");
                noTransaksiTextBox.Text = dr["noTransaksi"].ToString();
                deskripsiTextBox.Text = dr["deskripsi"].ToString();
                nominalTextBox.Text = dr["nominal"].ToString();
            }
            dr.Close();
            labeltransaksiID.Text = Convert.ToString(transaksiID);
        }
        protected void cancelrekening_Click(object sender, EventArgs e)
        {
            cancelTransaksi.Visible = false;
            tambahTransaksi.ForeColor = System.Drawing.Color.White;
            tambahTransaksi.BackColor = System.Drawing.Color.FromArgb(29, 161, 242);
            tambahTransaksi.Enabled = true;
            Table1.Visible = false;
            Response.Redirect("transaksi.aspx");
        }
        protected void simpanTransaksi_Click(object sender, EventArgs e)
        {
            if (deskripsiTextBox.Text == "") Response.Write("<script>alert('Field Wajib Diisi')</script>");
            else if (nominalTextBox.Text == "") Response.Write("<script>alert('Field Wajib Diisi')</script>");
            else if (nominalTextBox.Text == "" && deskripsiTextBox.Text == "") Response.Write("<script>alert('Field Wajib Diisi')</script>");
            else
            {
                string tanggal = tanggalTransaksiTextBox.Text.ToString();
                string noTransaksi = noTransaksiTextBox.Text.ToString();
                string deskripsi = deskripsiTextBox.Text.ToString();
                int nominal = Convert.ToInt32(nominalTextBox.Text.ToString());
                if (tambahTransaksi.Text == "Tambah") savedata(tanggal, noTransaksi, deskripsi, nominal);
                else if (tambahTransaksi.Text == "Edit") saveUpdateData(tanggal, noTransaksi, deskripsi, nominal);
                Response.Write("<script>alert('Data Tersimpan')</script>");
                Response.Redirect("transaksi.aspx");
            }
        }
        private void saveUpdateData(string tanggal, string noTransaksi, string deskripsi, int nominal)
        {
            string transaksiID = labeltransaksiID.Text;
            string query = "update transaksi set tanggal = '"+tanggal+"', noTransaksi = '"+noTransaksi+"', deskripsi = '"+deskripsi+"', nominal = '"+nominal+"' where transaksiID = '"+transaksiID+"'";
            String mycon = WebConfigurationManager.ConnectionStrings["koneksi"].ToString();
            SqlConnection con = new SqlConnection(mycon);
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = query;
            cmd.Connection = con;
            cmd.ExecuteNonQuery();
            con.Close();
        }
        private void savedata(string tanggal, string noTransaksi, string deskripsi, int nominal)
        {
            string query = "insert into transaksi(tanggal, noTransaksi, deskripsi, nominal) values('"+tanggal+"', '"+ noTransaksi + "', '"+deskripsi+"', '"+nominal+"')";
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
            int transaksiID = Convert.ToInt32((sender as LinkButton).CommandArgument);
            SqlCommand cmd = new SqlCommand("delete from transaksi where transaksiID = '" + transaksiID + "'", koneksi);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            koneksi.Close();
            Response.Redirect("transaksi.aspx");
        } 
        protected void ButtonPrint_Click(object sender, EventArgs e)
        {
            GridView1.Columns[4].Visible = false;
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=tunasmandiri_transaksi.pdf");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            GridView1.RenderControl(hw);
            StringReader sr = new StringReader(sw.ToString());
            Document pdfdoc = new Document(PageSize.A4, 30f, 30f, 50f, 30f);
            Phrase phrase1 = new Phrase();
            HTMLWorker htmlparse = new HTMLWorker(pdfdoc);
            PdfWriter.GetInstance(pdfdoc, Response.OutputStream);

            pdfdoc.Open();
            pdfdoc.Add(new Paragraph("BUMDES TUNAS MANDIRI DESA NEGARA RATU"));
            pdfdoc.Add(new Paragraph("TRANSAKSI HARIAN"));
            if (!IsPostBack || TextBoxPeriode1.Text=="") pdfdoc.Add(new Paragraph("S.D " + Convert.ToDateTime(TextBoxPeriode2.Text).ToString("dd MMM yyyy") + ""));
            else pdfdoc.Add(new Paragraph("Periode " + Convert.ToDateTime(TextBoxPeriode1.Text).ToString("dd MMM yyyy") + " s/d " + Convert.ToDateTime(TextBoxPeriode2.Text).ToString("dd MMM yyyy") + ""));
            pdfdoc.Add(new Paragraph(" "));
            pdfdoc.Add(new Paragraph(" "));
            htmlparse.Parse(sr);
            pdfdoc.Add(new Paragraph(" "));
            pdfdoc.Add(new Paragraph(" "));
            pdfdoc.Add(new Paragraph("Penyetuju"));
            pdfdoc.Add(new Paragraph(" "));
            pdfdoc.Add(new Paragraph(" "));
            pdfdoc.Add(new Paragraph(" "));
            pdfdoc.Add(new Paragraph("Havez Annamir"));
            pdfdoc.Add(new Paragraph("Ketua"));
            pdfdoc.Close();
            Response.Write(pdfdoc);
            Response.End();
            GridView1.AllowPaging = true;
            GridView1.DataBind();
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
               server control at run time. */
        }
    }
}