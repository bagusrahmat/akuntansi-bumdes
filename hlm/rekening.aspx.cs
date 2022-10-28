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
    public partial class rekening : System.Web.UI.Page
    {
        SqlConnection koneksi = new SqlConnection(WebConfigurationManager.ConnectionStrings["koneksi"].ToString());
        string periodeawal, periodeakhir;
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
                    tambahRekening.Visible = false;
                    GridView1.Columns[6].Visible = false;
                }
                koneksi.Close();
            }
            tambahRekening.BackColor = System.Drawing.Color.FromArgb(29, 161, 242);
            tambahRekening.ForeColor = System.Drawing.Color.White;
            if (!IsPostBack)
            {
                periodeawal = DateTime.Today.AddDays(-1).ToString("yyyy-MM-dd");
                periodeakhir = DateTime.Today.ToString("yyyy-MM-dd");
                Label1.Text = "Data Rekening Tanggal " + DateTime.Today.ToString("dd MMM yyyy");
                GridView2.Visible = false;
                BindData();
                
                cancelrekening.Visible = false;
                Table1.Visible = false;
                tambahRekening.Text = "Tambah";
            }
            if (Table1.Visible == true)
            {
                ButtonPrint.Visible = false;
            }
        }
        protected void linkEdit_Click(object sender, EventArgs e)
        {
            cancelrekening.Visible = true;
            Table1.Visible = true;
            tambahRekening.Text = "Edit";
            tambahRekening.ForeColor = System.Drawing.Color.FromArgb(29, 161, 242);
            tambahRekening.BackColor = System.Drawing.Color.White;
            int rekID = Convert.ToInt32((sender as LinkButton).CommandArgument);
            koneksi.Open();
            string query = "select * from rekening where rekUID = " + rekID + "";
            SqlCommand cmd = new SqlCommand(query, koneksi);
            cmd.CommandType = CommandType.Text;
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                rekeningIDTextBox.Text = dr["rekeningID"].ToString();
                namaRekeningTextBox.Text = dr["namaRekening"].ToString();
                DropDownPosisi.Text = dr["posisi"].ToString();
                saldoNormalDropDown.Text = dr["saldoNormal"].ToString();
            }
            dr.Close();
            rekUID.Text = Convert.ToString(rekID);
        }
        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            Response.Redirect("~/hlm/rekening.aspx");
        }
        protected void linkHapus_Click(object sender, EventArgs e)
        {
            int rekUID = Convert.ToInt32((sender as LinkButton).CommandArgument);
            SqlCommand cmd = new SqlCommand("delete from rekening where rekUID = '" + rekUID + "'", koneksi);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            koneksi.Close();
            Response.Redirect("rekening.aspx");
        }
        
        private void BindData()
        {;
            SqlCommand cmd = new SqlCommand("declare @periode1 as date, @periodeawal as date, @periodeakhir as date set @periode1 = '2010-1-1' set @periodeawal = '" + periodeawal + "' set @periodeakhir = '" + periodeakhir + "' select rekUID, rekeningID, namaRekening, posisi, saldoNormal from rekening", koneksi);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            koneksi.Close();
            GridView1.DataSource = dt;
            GridView1.DataBind();

            SqlCommand cmd2 = new SqlCommand("declare @periode1 as date, @periodeawal as date, @periodeakhir as date set @periode1 = '2010-1-1' set @periodeawal = '" + periodeawal + "' set @periodeakhir = '" + periodeakhir + "' select rekUID, rekeningID, namaRekening, posisi, saldoNormal from rekening", koneksi);
            SqlDataAdapter sda2 = new SqlDataAdapter(cmd2);
            DataTable dt2 = new DataTable();
            sda.Fill(dt2);
            koneksi.Close();
            GridView2.DataSource = dt2;
            GridView2.DataBind();
        }
        protected void tambahRekening_Click(object sender, EventArgs e)
        {
            tambahRekening.ForeColor = System.Drawing.Color.FromArgb(29,161,242);
            tambahRekening.BackColor = System.Drawing.Color.White;
            cancelrekening.Visible = true;
            Table1.Visible = true;
            tambahRekening.Enabled = false;
        }
        protected void cancelrekening_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/hlm/rekening.aspx");
            return;
        }
        protected void SimpanRekening_Click(object sender, EventArgs e)
        {
            string rekeningID = rekeningIDTextBox.Text.ToString();
            string namaRekening = namaRekeningTextBox.Text.ToString();
            string posisi = DropDownPosisi.Text.ToString();
            string saldoNormal = saldoNormalDropDown.Text.ToString();

            if (tambahRekening.Text == "Tambah") savedata(rekeningID, namaRekening, posisi, saldoNormal);
            else if(tambahRekening.Text == "Edit") saveEditData(rekeningID, namaRekening, posisi, saldoNormal);
            Response.Write("<script>alert('Data Tersimpan')</script>");
            Response.Redirect("~/hlm/rekening.aspx");
        }
        private void savedata(string rekeningID, string namaRekening, string posisi, string saldoNormal)
        {
            string query = ("insert into rekening values('"+rekeningID+"', '"+namaRekening+"', '"+posisi+"', '"+saldoNormal+"')");
            String mycon = WebConfigurationManager.ConnectionStrings["koneksi"].ToString();
            SqlConnection con = new SqlConnection(mycon);
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = query;
            cmd.Connection = con;
            cmd.ExecuteNonQuery();
            con.Close();
        }
        private void saveEditData(string rekeningID, string namaRekening, string posisi, string saldoNormal)
        {
            string id = rekUID.Text;
            string query = ("update rekening set rekeningID = '" + rekeningID + "', namaRekening = '" + namaRekening + "', posisi = '" + posisi + "', saldoNormal = '" + saldoNormal + "' where rekUID = " + id + "");
            String mycon = WebConfigurationManager.ConnectionStrings["koneksi"].ToString();
            SqlConnection con = new SqlConnection(mycon);
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = query;
            cmd.Connection = con;
            cmd.ExecuteNonQuery();
            con.Close();
        }

        protected void ButtonPrint_Click(object sender, EventArgs e)
        {
            iTextSharp.text.pdf.PdfPTable table = new iTextSharp.text.pdf.PdfPTable(GridView2.Columns.Count);

            int[] widths = new int[GridView2.Columns.Count];
            for(int x=0; x<GridView2.Columns.Count; x++)
            {
                widths[x] = (int)GridView2.Columns[x].ItemStyle.Width.Value;
                string celltext = Server.HtmlDecode(GridView2.HeaderRow.Cells[x].Text);
                //iTextSharp.text.pdf.PdfPCell cell = new iTextSharp.text.pdf.PdfPCell(celltext);
            }

            /*
            GridView2.Visible = true;
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=tunasmandiri_rekening.pdf");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            GridView2.RenderControl(hw);
            StringReader sr = new StringReader(sw.ToString());
            Document pdfdoc = new Document(PageSize.A4, 30f, 30f, 30f, 30f);
            Phrase phrase1 = new Phrase();
            HTMLWorker htmlparse = new HTMLWorker(pdfdoc);
            PdfWriter.GetInstance(pdfdoc, Response.OutputStream);
            BaseFont bfTimes = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false);
            Font tablefont = new Font(bfTimes, 12);
            //GridView2.Style.Add("font-family", "Times New Roman;");

            pdfdoc.Open();
            pdfdoc.Add(new Paragraph("BUMDES TUNAS MANDIRI DESA NEGARA RATU"));
            pdfdoc.Add(new Paragraph("DATA REKENING"));
            pdfdoc.Add(new Paragraph(" "));
            pdfdoc.Add(new Paragraph(Label1.Text));
            htmlparse.Parse(sr);
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
            GridView2.AllowPaging = true;
            GridView2.DataBind();
            */
        }
        public override void VerifyRenderingInServerForm(Control control) { }
    }
}