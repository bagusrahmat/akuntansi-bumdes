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
    public partial class perubahan_modal : System.Web.UI.Page
    {
        SqlConnection koneksi = new SqlConnection(WebConfigurationManager.ConnectionStrings["koneksi"].ToString());
        int totalsaldodebet = 0, totalsaldokredit = 0, modalAwal=0;
        string periode;
        protected void Page_Load(object sender, EventArgs e)
        {
            periode = DateTime.Today.ToString("yyyy-MM-dd");
            koneksi.Open();
            string query = "select distinct 'Modal Awal' as namaRekening, (select top(1) kredit from detail_jurnal left join jurnal on jurnal.noJurnal = detail_jurnal.noJurnal left join transaksi on transaksi.noTransaksi=jurnal.noTransaksi where rekeningID='3.100' order by tanggal asc) as kredit from rekening";
            SqlCommand cmd = new SqlCommand(query, koneksi);
            cmd.CommandType = CommandType.Text;
            SqlDataReader dr = cmd.ExecuteReader();
            dr.Read();
            modalAwal = Convert.ToInt32(dr["kredit"].ToString());
            dr.Close();
            if (!IsPostBack)
            {
                DDTahun();
                ButtonPrint.Visible = false;
            }
            lihatData();
        }
        private void DDTahun()
        {
            int year = DateTime.Now.Year;
            for (int i = year; i >= year - 5; i--)
            {
                System.Web.UI.WebControls.ListItem li = new System.Web.UI.WebControls.ListItem(i.ToString());
                DropDownTahun.Items.Add(li);
            }
        }
        protected void Btnlihat_Click(object sender, EventArgs e)
        {
            periode = "" + DropDownTahun.SelectedValue + "-MM-dd";
            if (DropDownTahun.SelectedValue == DateTime.Today.ToString("yyyy"))
                Labelperiode.Text = "LAPORAN PERUBAHAN MODAL TAHUN " + DropDownTahun.SelectedValue + " (BERJALAN)";
            else Labelperiode.Text = "LAPORAN PERUBAHAN MODAL TAHUN " + DropDownTahun.SelectedValue + "";
            lihatData();
        }
        public void lihatData()
        {

            //int hari = DateTime.DaysInMonth(Convert.ToInt16(DropDownTahun.SelectedValue), Convert.ToInt16(DropDownBulan.SelectedValue));
            SqlCommand cmd = new SqlCommand("declare @periode1 as date, @periode2 as date set @periode1 = '2010-1-1' set @periode2 = '" + Convert.ToDateTime(periode) + "' select null as rekeningID, 'Modal Awal' as namaRekening, 0 as debet, (select top(1) kredit from detail_jurnal left join jurnal on jurnal.noJurnal = detail_jurnal.noJurnal left join transaksi on transaksi.noTransaksi=jurnal.noTransaksi where rekeningID='3.100' order by tanggal asc) as kredit from rekening union select rekeningId, namaRekening, isnull((select sum (debet) from detail_jurnal left join jurnal on jurnal.noJurnal = detail_jurnal.noJurnal left join transaksi on transaksi.noTransaksi = jurnal.noTransaksi where rekening.rekeningID = detail_jurnal.rekeningID),0) as debet, isnull((select sum (kredit) from detail_jurnal left join jurnal on jurnal.noJurnal = detail_jurnal.noJurnal left join transaksi on transaksi.noTransaksi = jurnal.noTransaksi where rekening.rekeningID = detail_jurnal.rekeningID),0) as kredit from rekening where rekeningID in ('3.100', '3.200', '3.300')", koneksi);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            koneksi.Close();
            GridView1.DataSource = dt;
            GridView1.DataBind();
            ButtonPrint.Visible = true;
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                totalsaldodebet += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "debet"));
                totalsaldokredit += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "kredit"));
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.BackColor = System.Drawing.Color.FromArgb(240, 240, 240);
                //e.Row.Cells[0].ColumnSpan = 2;
                e.Row.Cells[0].Text = "Modal Akhir";
                //e.Row.Cells[2].Text = string.Format("{0:n0}", totalsaldodebet);
                e.Row.Cells[2].Text = string.Format("{0:n0}", totalsaldokredit- totalsaldodebet - modalAwal);
                //e.Row.Cells.RemoveAt(1);
            }
        }
        protected void ButtonPrint_Click(object sender, EventArgs e)
        {
            GridView1.Visible = true;
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=tunasmandiri_perubahan_modal.pdf");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            GridView1.RenderControl(hw);
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
            pdfdoc.Add(new Paragraph("LAPORAN PERUBAHAN MODAL"));
            pdfdoc.Add(new Paragraph("PERIODE :"));
            pdfdoc.Add(new Paragraph(" "));
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
            GridView1.AllowPaging = true;
            GridView1.DataBind();
        }
        public override void VerifyRenderingInServerForm(Control control) { }

        
    }
}