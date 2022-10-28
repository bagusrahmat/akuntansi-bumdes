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
    public partial class neraca : System.Web.UI.Page
    {
        SqlConnection koneksi = new SqlConnection(WebConfigurationManager.ConnectionStrings["koneksi"].ToString());
        int totalsaldokiri = 0, totalsaldokanan = 0;
        int intkiri = 0, intkanan = 0, totalsaldodebet = 0, totalsaldokredit = 0, labarugi = 0, labarugitotal=0;
        protected void Page_Load(object sender, EventArgs e)
        {
            koneksi.Open();
            string query = "select (select count(*) from rekening where saldoNormal='debet' and posisi='neraca') as intkiri, (select count(*) from rekening where saldoNormal='kredit' and posisi='neraca') as intkanan";
            SqlCommand cmd = new SqlCommand(query, koneksi);
            cmd.CommandType = CommandType.Text;
            SqlDataReader dr = cmd.ExecuteReader();
            dr.Read();
            intkiri = Convert.ToInt16(dr["intkiri"]);
            intkanan = Convert.ToInt16(dr["intkanan"]) + 1;
            dr.Close();
            if (!IsPostBack)
            {
                DDTahun();
                DropDownBulan.SelectedValue = Convert.ToString(DateTime.Today.Month);
                lihatLabaRugi();
                lihatNeracaKiri();
                lihatNeracaKanan();
                
            }
            lblperiode.Text = "NERACA PERIODE S/D : " + DateTime.Today.ToString("dd-MMM-yyyy") + " (Hari ini)<br/>";
        }
        private void DDTahun()
        {
            int year = DateTime.Now.Year;
            for(int i=year; i>=year-5; i--)
            {
                System.Web.UI.WebControls.ListItem li = new System.Web.UI.WebControls.ListItem(i.ToString());
                DropDownTahun.Items.Add(li);
            }
        }
        public void lihatLabaRugi()
        {
            int hari = DateTime.DaysInMonth(Convert.ToInt16(DropDownTahun.SelectedValue), Convert.ToInt16(DropDownBulan.SelectedValue));
            SqlCommand cmd = new SqlCommand("declare @periode1 as date, @periode2 as date set @periode1 = '2010-1-1' set @periode2 = '" + DropDownTahun.SelectedValue + "-" + DropDownBulan.SelectedValue + "-" + hari + "' select rekeningID, namaRekening, isnull((select sum (debet) from detail_jurnal left join jurnal on jurnal.noJurnal = detail_jurnal.noJurnal left join transaksi on transaksi.noTransaksi = jurnal.noTransaksi where rekening.rekeningID = detail_jurnal.rekeningID and tanggal between @periode1 and @periode2) - (select sum (kredit) from detail_jurnal left join jurnal on jurnal.noJurnal = detail_jurnal.noJurnal left join transaksi on transaksi.noTransaksi = jurnal.noTransaksi where rekening.rekeningID = detail_jurnal.rekeningID and tanggal between @periode1 and @periode2),0) as debet, 0 as kredit from rekening where saldoNormal='debet' and posisi='laba rugi' union select rekeningID, namaRekening, 0 as debet, isnull((select sum (kredit)  from detail_jurnal left join jurnal on jurnal.noJurnal = detail_jurnal.noJurnal left join transaksi on transaksi.noTransaksi = jurnal.noTransaksi where rekening.rekeningID = detail_jurnal.rekeningID and tanggal between @periode1 and @periode2) -  (select sum (debet) from detail_jurnal left join jurnal on jurnal.noJurnal = detail_jurnal.noJurnal left join transaksi on transaksi.noTransaksi = jurnal.noTransaksi where rekening.rekeningID = detail_jurnal.rekeningID and tanggal between @periode1 and @periode2),0) as kredit from rekening where saldoNormal='kredit' and posisi='laba rugi'", koneksi);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            GridView3.DataSource = dt;
            GridView3.DataBind();
        }
        public void GridView3_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                totalsaldodebet += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "debet"));
                totalsaldokredit += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "kredit"));
                labarugi = totalsaldokredit - totalsaldodebet;
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.BackColor = System.Drawing.Color.FromArgb(240, 240, 240);
                //e.Row.Cells[0].ColumnSpan = 2;
                e.Row.Cells[0].Text = "LABA/RUGI";
                e.Row.Cells[1].Text = string.Format("{0:n0}", labarugi);
                //e.Row.Cells.RemoveAt(1);
            }
        }
        public void lihatNeracaKiri()
        {
            int hari = DateTime.DaysInMonth(Convert.ToInt16(DropDownTahun.SelectedValue), Convert.ToInt16(DropDownBulan.SelectedValue));
            SqlCommand cmd = new SqlCommand("declare @periode1 as date, @periode2 as date set @periode1 = '2010-1-1' set @periode2 = '" + DropDownTahun.SelectedValue + "-" + DropDownBulan.SelectedValue + "-" + hari + "' select rekeningID, namaRekening, isnull((select sum (debet) from detail_jurnal left join jurnal on jurnal.noJurnal = detail_jurnal.noJurnal left join transaksi on transaksi.noTransaksi = jurnal.noTransaksi where rekening.rekeningID = detail_jurnal.rekeningID and tanggal between @periode1 and @periode2) - (select sum (kredit) from detail_jurnal left join jurnal on jurnal.noJurnal = detail_jurnal.noJurnal left join transaksi on transaksi.noTransaksi = jurnal.noTransaksi where rekening.rekeningID = detail_jurnal.rekeningID and tanggal between @periode1 and @periode2),0) as saldo from rekening where saldoNormal='debet' and posisi='neraca'", koneksi); ; ;
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            koneksi.Close();
            if (intkiri < intkanan)
            {
                for (int i = 0; i < intkanan - intkiri; i++) dt.Rows.Add("", "", 0);
            }
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }
        public void lihatNeracaKanan()
        {
            int hari = DateTime.DaysInMonth(Convert.ToInt16(DropDownTahun.SelectedValue), Convert.ToInt16(DropDownBulan.SelectedValue));
            SqlCommand cmd = new SqlCommand("declare @periode1 as date, @periode2 as date set @periode1 = '2010-1-1' set @periode2 = '" + DropDownTahun.SelectedValue + "-" + DropDownBulan.SelectedValue + "-" + hari + "' select rekeningID, namaRekening, isnull((select sum (kredit) from detail_jurnal left join jurnal on jurnal.noJurnal = detail_jurnal.noJurnal left join transaksi on transaksi.noTransaksi = jurnal.noTransaksi where rekening.rekeningID = detail_jurnal.rekeningID and tanggal between @periode1 and @periode2) - (select sum (debet) from detail_jurnal left join jurnal on jurnal.noJurnal = detail_jurnal.noJurnal left join transaksi on transaksi.noTransaksi = jurnal.noTransaksi where rekening.rekeningID = detail_jurnal.rekeningID and tanggal between @periode1 and @periode2),0) as saldo from rekening where saldoNormal='kredit' and posisi='neraca'", koneksi); ;
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            koneksi.Close();
            dt.Rows.Add("", "Laba/Rugi", labarugi);
            if (intkanan < intkiri)
                for (int i = 0; i < intkiri - intkanan; i++) dt.Rows.Add("", "", 0);
            GridView2.DataSource = dt;
            GridView2.DataBind();
        }
        protected void lihatNeraca_Click(object sender, EventArgs e)
        {
            lihatNeracaKiri();
            lihatNeracaKanan();
            lihatLabaRugi();
            
            if (DateTime.Today.Month <= Convert.ToInt16(DropDownBulan.SelectedValue) && DateTime.Today.Year <= Convert.ToInt16(DropDownTahun.SelectedValue))
                lblperiode.Text = "NERACA PERIODE S/D : " + DateTime.Today.ToString("dd-MMM-yyyy") + " (Hari ini)<br/>";
            else lblperiode.Text = "NERACA PERIODE S/D " + DropDownBulan.SelectedItem + " " + DropDownTahun.SelectedValue;
        }
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
                totalsaldokiri += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "saldo"));
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.BackColor = System.Drawing.Color.FromArgb(240, 240, 240);
                e.Row.Cells[0].ColumnSpan = 2;
                e.Row.Cells[0].Text = "TOTAL AKTIVA";
                e.Row.Cells[2].Text = string.Format("{0:n0}", totalsaldokiri);
                e.Row.Cells.RemoveAt(1);
            }
        }
        protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
                totalsaldokanan += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "saldo"));
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.BackColor = System.Drawing.Color.FromArgb(240, 240, 240);
                e.Row.Cells[0].ColumnSpan = 2;
                e.Row.Cells[0].Text = "TOTAL PASIVA";
                e.Row.Cells[2].Text = string.Format("{0:n0}", totalsaldokiri);
                e.Row.Cells.RemoveAt(1);
            }
        }
        
        protected void ButtonPrint_Click(object sender, EventArgs e)
        {
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=tunasmandiri_neraca.pdf");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            StringWriter sw1 = new StringWriter();
            HtmlTextWriter hw1 = new HtmlTextWriter(sw1);
            GridView1.RenderControl(hw1);
            StringWriter sw2 = new StringWriter();
            HtmlTextWriter hw2 = new HtmlTextWriter(sw2);
            GridView2.RenderControl(hw2);
            StringReader sr1 = new StringReader(sw1.ToString());
            StringReader sr2 = new StringReader(sw2.ToString());
            Document pdfdoc = new Document(PageSize.A4, 50f, 50f, 50f, 30f);
            Phrase phrase1 = new Phrase();
            HTMLWorker htmlparse = new HTMLWorker(pdfdoc);
            PdfWriter.GetInstance(pdfdoc, Response.OutputStream);

            pdfdoc.Open();
            pdfdoc.Add(new Paragraph("BUMDES TUNAS MANDIRI DESA NEGARA RATU"));
            pdfdoc.Add(new Paragraph("LAPORAN NERACA"));

            if (DateTime.Today.Month <= Convert.ToInt16(DropDownBulan.SelectedValue) && DateTime.Today.Year <= Convert.ToInt16(DropDownTahun.SelectedValue))
                pdfdoc.Add(new Paragraph("PERIODE S/D : " + DateTime.Today.ToString("dd-MMM-yyyy") + " (Hari Ini)"));
            else pdfdoc.Add(new Paragraph("PERIODE S/D " + DropDownBulan.SelectedItem + " " + DropDownTahun.SelectedValue));

            pdfdoc.Add(new Paragraph(" "));
            pdfdoc.Add(new Paragraph(" "));
            htmlparse.Parse(sr1);
            pdfdoc.Add(new Paragraph(" "));
            pdfdoc.Add(new Paragraph(" "));
            htmlparse.Parse(sr2);
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
            GridView2.AllowPaging = true;
            GridView2.DataBind();
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
               server control at run time. */
        }
    }
}