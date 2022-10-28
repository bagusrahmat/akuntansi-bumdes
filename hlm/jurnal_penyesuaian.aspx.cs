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
    public partial class jurnal_penyesuaian : System.Web.UI.Page
    {
        SqlConnection koneksi = new SqlConnection(WebConfigurationManager.ConnectionStrings["koneksi"].ToString());
        int totalsaldodebet = 0, totalsaldokredit = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                TextBoxPeriode2.Text = DateTime.Today.ToString("yyyy-MM-dd");
                lblperiode.Text = "JURNAL PENYESUAIAN S/D " + DateTime.Today.ToString("dd MMM yyyy") + "";
            }
            lihatData();
        }
        
        protected void lihatData_Click(object sender, EventArgs e)
        {
            lihatData();
            if (TextBoxPeriode1.Text == "") lblperiode.Text = "JURNAL PENYESUAIAN S/D " + DateTime.Today.ToString("dd MMM yyyy") + "";
            else lblperiode.Text = "JURNAL PENYESUAIAN PERIODE " + Convert.ToDateTime(TextBoxPeriode1.Text).ToString("dd MMM yyyy") + " S/D " + Convert.ToDateTime(TextBoxPeriode2.Text).ToString("dd MMM yyyy") + "";
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
                e.Row.Cells[0].ColumnSpan = 2;
                e.Row.Cells[0].Text = "JUMLAH";
                e.Row.Cells[2].Text = string.Format("{0:n0}", totalsaldodebet);
                e.Row.Cells[3].Text = string.Format("{0:n0}", totalsaldokredit);
                e.Row.Cells.RemoveAt(1);
            }
        }
        protected void lihatData()
        {
            SqlCommand cmd = new SqlCommand("declare @periode1 as date, @periode2 as date set @periode1 = '" + TextBoxPeriode1.Text + "' set @periode2 = '" + TextBoxPeriode2.Text + "' select rekeningID, namaRekening, isnull((select sum (debet) from detail_jurnal left join jurnal on jurnal.noJurnal = detail_jurnal.noJurnal left join transaksi on transaksi.noTransaksi = jurnal.noTransaksi where rekening.rekeningID = detail_jurnal.rekeningID and jurnal.jenis='JP' and tanggal between @periode1 and @periode2),0) as debet, isnull((select sum (kredit) from detail_jurnal left join jurnal on jurnal.noJurnal = detail_jurnal.noJurnal left join transaksi on transaksi.noTransaksi = jurnal.noTransaksi where rekening.rekeningID = detail_jurnal.rekeningID and jurnal.jenis='JP' and tanggal between @periode1 and @periode2),0) as kredit from rekening", koneksi);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            koneksi.Close();
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }
    }
}