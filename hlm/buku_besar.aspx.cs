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
    public partial class buku_besar : System.Web.UI.Page
    {
        SqlConnection koneksi = new SqlConnection(WebConfigurationManager.ConnectionStrings["koneksi"].ToString());
        protected void Page_Load(object sender, EventArgs e)
        {
            dataRekening();
            if(!IsPostBack) TextBoxPeriode2.Text = DateTime.Now.ToString("yyyy-MM-dd");
            if (!IsPostBack) GridView1.Visible = false;
            if (GridView1.Visible == false) ButtonPrint.Visible = false;
            ButtonPrint.Enabled = false;
            ButtonPrint.ForeColor = System.Drawing.Color.FromArgb(200, 200, 200);
            ButtonPrint.BorderColor = System.Drawing.Color.FromArgb(200, 200, 200);
        }
        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            lihatData();
            labelHalaman.Text = "Menampilkan halaman " + (GridView1.PageIndex + 1).ToString() + " dari " + GridView1.PageCount.ToString() + "";
        }
        public void dataRekening()
        {
            if (!IsPostBack)
            {
                koneksi.Open();
                SqlCommand com = new SqlCommand("select rekeningID, rekeningID + ' - ' + namaRekening as namaRekening from rekening order by rekeningID", koneksi);
                SqlDataAdapter da = new SqlDataAdapter(com);
                DataTable ds = new DataTable();
                da.Fill(ds);
                DropDownListRekening.DataSource = ds;
                DropDownListRekening.DataTextField = "namaRekening";
                DropDownListRekening.DataValueField = "rekeningID";
                DropDownListRekening.DataBind();
            }
        }
        protected void lihatBukuBesar_Click(object sender, EventArgs e)
        {
            koneksi.Open();
            string query = "select rekeningID + ' (' + namaRekening + ')' as namaRekening from rekening where rekeningID = '" + DropDownListRekening.SelectedValue+"' ";
            SqlCommand cmd = new SqlCommand(query, koneksi);
            cmd.CommandType = CommandType.Text;
            SqlDataReader dr = cmd.ExecuteReader();
            dr.Read();
                LabelRekeningBB.Text = dr["namaRekening"].ToString();
            dr.Close();
            lihatData();
            ButtonPrint.ForeColor = System.Drawing.Color.FromArgb(29, 161, 242);
            ButtonPrint.BorderColor = System.Drawing.Color.FromArgb(29, 161, 242);
            ButtonPrint.Enabled = true;
        }
        private void lihatData()
        {
            SqlCommand cmd = new SqlCommand("select tanggal, transaksi.noTransaksi, deskripsi, debet, kredit from jurnal left join bumdesapp.dbo.transaksi on transaksi.noTransaksi = jurnal.noTransaksi left join detail_jurnal on detail_jurnal.noJurnal = jurnal.noJurnal where transaksi.tanggal between '" + TextBoxPeriode1.Text + "' and '" + TextBoxPeriode2.Text + "' and rekeningID = '" + DropDownListRekening.SelectedValue + "' union select null as tanggal, null as noTransaksi, 'TOTAL' as deskripsi, sum(debet), sum(kredit) from jurnal left join bumdesapp.dbo.transaksi on transaksi.noTransaksi = jurnal.noTransaksi left join detail_jurnal on detail_jurnal.noJurnal = jurnal.noJurnal where transaksi.tanggal between '" + TextBoxPeriode1.Text + "' and '" + TextBoxPeriode2.Text + "' and rekeningID = '" + DropDownListRekening.SelectedValue + "' order by tanggal desc", koneksi);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            koneksi.Close();
            GridView1.DataSource = dt;
            GridView1.DataBind();
            GridView1.Visible = true;
            ButtonPrint.Visible = true;
            labelHalaman.Text = "Menampilkan halaman " + (GridView1.PageIndex + 1).ToString() + " dari " + GridView1.PageCount.ToString() + "";
        }

        protected void ButtonPrint_Click(object sender, EventArgs e)
        {
            GridView1.Visible = true;
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=tunasmandiri_bukubesar("+ LabelRekeningBB.Text +").pdf");
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
            pdfdoc.Add(new Paragraph("BUKU BESAR " + LabelRekeningBB.Text + ""));
            if(TextBoxPeriode1.Text=="") pdfdoc.Add(new Paragraph("S/D " + Convert.ToDateTime(TextBoxPeriode2.Text).ToString("dd MMM yyyy") + ""));
            else pdfdoc.Add(new Paragraph("PERIODE " + Convert.ToDateTime(TextBoxPeriode1.Text).ToString("dd MMM yyyy") + " S/D " + Convert.ToDateTime(TextBoxPeriode2.Text).ToString("dd MMM yyyy") + ""));
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