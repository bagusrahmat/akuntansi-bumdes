using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using System.IO;

namespace bumdesAPP.hlm
{
    public partial class detail_jurnal : System.Web.UI.Page
    {
        SqlConnection koneksi = new SqlConnection(WebConfigurationManager.ConnectionStrings["koneksi"].ToString());
        protected void Page_Load(object sender, EventArgs e)
        {
            LinkButton1.Text = "Tampilkan Semua";
            if(!IsPostBack)
            {
                TextBoxPeriode1.Text = "";
                TextBoxPeriode2.Text = DateTime.Today.ToString("yyyy-MM-dd");
                lblperiode.Text = "JURNAL UMUM S/D " + DateTime.Today.ToString("dd MMM yyyy");
                dataDetailJurnal();
                LabelHalaman.Text = "Menampilkan halaman " + (GridView1.PageIndex + 1).ToString() + " dari " + GridView1.PageCount.ToString();
            }
        }
        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            dataDetailJurnal();
            LabelHalaman.Text = "Menampilkan halaman " + (GridView1.PageIndex + 1).ToString() + " dari " + GridView1.PageCount.ToString();
        }
        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            dataDetailJurnal();
            lblperiode.Text = "JURNAL UMUM S/D " + DateTime.Now.ToString("dd MMM yyyy");
            TextBoxPeriode1.Text = "";
        }
        
        protected void dataDetailJurnal()
        {
            SqlCommand cmd = new SqlCommand("select tanggal, detail_jurnal.noJurnal, deskripsi, namaRekening, debet, kredit from detail_jurnal left join jurnal on jurnal.noJurnal = detail_jurnal.nojurnal left join transaksi on transaksi.noTransaksi = jurnal.noTransaksi left join rekening on rekening.rekeningID = detail_jurnal.rekeningID where jurnal.noTransaksi is not null and tanggal between '" + TextBoxPeriode1.Text + "' and '" + TextBoxPeriode2.Text + "' union (select null as tanggal, null as noJurnal, null as deskripsi, 'TOTAL' as namaRekening, sum(debet) as debet, sum(kredit) as kredit from detail_jurnal left join jurnal on jurnal.noJurnal = detail_jurnal.noJurnal left join transaksi on transaksi.noTransaksi = jurnal.noTransaksi where jurnal.noTransaksi is not null and tanggal between '" + TextBoxPeriode1.Text + "' and '" + TextBoxPeriode2.Text + "') order by tanggal desc", koneksi); ; ;
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            koneksi.Close();
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }
        protected void lihatDetailJurnalPeriode_Click(object sender, EventArgs e)
        {
            dataDetailJurnal();
            if (TextBoxPeriode1.Text == "") lblperiode.Text = "JURNAL UMUM S/D " + Convert.ToDateTime(TextBoxPeriode2.Text).ToString("dd MMM yyyy");
            else lblperiode.Text = "JURNAL UMUM PERIODE " + Convert.ToDateTime(TextBoxPeriode1.Text).ToString("dd MMM yyyy") + " S/D " + Convert.ToDateTime(TextBoxPeriode2.Text).ToString("dd MMM yyyy");
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
               server control at run time. */
        }
    }
}