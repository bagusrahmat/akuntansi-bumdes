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

namespace bumdesAPP.hlm
{
    public partial class jurnal : System.Web.UI.Page
    {
        int saldoKas = 0;
        int saldoBank = 0;
        string noTransaksi, noJurnal, deskripsi, nominal;
        SqlConnection koneksi = new SqlConnection(WebConfigurationManager.ConnectionStrings["koneksi"].ToString());
        protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack)
            {
                lblBoolEdit.Text = "";
                lblKetJurnal.Text = "JURNAL S/D TANGGAL " + DateTime.Today.ToString("dd MMM yyyy");
                TextBoxPeriode2.Text = DateTime.Today.ToString("yyyy-MM-dd");
            }

            koneksi.Open();
            string query = "declare @periode1 as date, @periode2 as date set @periode1 = '2010-1-1' set @periode2 = '" + DateTime.Today.ToString("yyyy-MM-dd") + "' select isnull((select sum (debet)  from detail_jurnal left join jurnal on jurnal.noJurnal = detail_jurnal.noJurnal left join transaksi on transaksi.noTransaksi = jurnal.noTransaksi where rekening.rekeningID = detail_jurnal.rekeningID and tanggal between @periode1 and @periode2) -  (select sum (kredit) from detail_jurnal left join jurnal on jurnal.noJurnal = detail_jurnal.noJurnal left join transaksi on transaksi.noTransaksi = jurnal.noTransaksi where rekening.rekeningID = detail_jurnal.rekeningID and tanggal between @periode1 and @periode2),0) as saldoKas from rekening where saldoNormal='debet' and rekeningID = '1.110'";
            SqlCommand cmd = new SqlCommand(query, koneksi);
            cmd.CommandType = CommandType.Text;
            SqlDataReader dr = cmd.ExecuteReader();
            dr.Read();
            saldoKas = Convert.ToInt32(dr["saldoKas"].ToString());
            dr.Close();

            string query1 = "declare @periode1 as date, @periode2 as date set @periode1 = '2010-1-1' set @periode2 = '" + DateTime.Today.ToString("yyyy-MM-dd") + "' select isnull((select sum (debet)  from detail_jurnal left join jurnal on jurnal.noJurnal = detail_jurnal.noJurnal left join transaksi on transaksi.noTransaksi = jurnal.noTransaksi where rekening.rekeningID = detail_jurnal.rekeningID and tanggal between @periode1 and @periode2) -  (select sum (kredit) from detail_jurnal left join jurnal on jurnal.noJurnal = detail_jurnal.noJurnal left join transaksi on transaksi.noTransaksi = jurnal.noTransaksi where rekening.rekeningID = detail_jurnal.rekeningID and tanggal between @periode1 and @periode2),0) as saldoBank from rekening where saldoNormal='debet' and rekeningID = '1.120'";
            SqlCommand cmd1 = new SqlCommand(query1, koneksi);
            cmd1.CommandType = CommandType.Text;
            SqlDataReader dr1 = cmd1.ExecuteReader();
            dr1.Read();
            saldoBank = Convert.ToInt32(dr1["saldoBank"].ToString());
            dr1.Close();
            koneksi.Close();
            Table1.Visible = false;
            Table2.Visible = false;
            dataSemuaJurnal();
            dataRekening();
            LabelHalaman.Text = "Menampilkan halaman " + (GridView3.PageIndex + 1).ToString() + " dari " + GridView3.PageCount.ToString() + "";
            tambahJurnalButton.ForeColor = System.Drawing.Color.White;
            tambahJurnalButton.BackColor = System.Drawing.Color.FromArgb(29, 161, 242);
        }
        protected void GridView3_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView3.PageIndex = e.NewPageIndex;
            dataSemuaJurnal();
            LabelHalaman.Text = "Menampilkan halaman " + (GridView3.PageIndex + 1).ToString() + " dari " + GridView3.PageCount.ToString() + "";
        }
        protected void lihatJurnalPertanggal_Click(object sender, EventArgs e)
        {
            if (TextBoxPeriode1.Text == "") lblKetJurnal.Text = "JURNAL S/D TANGGAL " + Convert.ToDateTime(TextBoxPeriode2.Text).ToString("dd MMM yyyy");
            else lblKetJurnal.Text = "JURNAL PERIODE " + Convert.ToDateTime(TextBoxPeriode1.Text).ToString("dd MMM yyyy") + " S/D " + Convert.ToDateTime(TextBoxPeriode2.Text).ToString("dd MMM yyyy") + "";
            SqlCommand cmd = new SqlCommand("select distinct transaksiID, jurnal.noJurnal, tanggal, deskripsi, transaksi.noTransaksi from transaksi left join jurnal on transaksi.noTransaksi = jurnal.noTransaksi left join detail_jurnal on jurnal.noJurnal = detail_jurnal.noJurnal where detail_jurnal.rekeningID is not null and tanggal between '" + TextBoxPeriode1.Text + "' and '" + TextBoxPeriode2.Text + "' order by tanggal desc", koneksi);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            koneksi.Close();
            GridView3.DataSource = dt;
            GridView3.DataBind();
        }
        protected void tambahJurnalBtn_Click(object sender, EventArgs e){
            dataBlmDijurnal();
            TablePertanggalSort.Visible = false;
            lblKetJurnal.Visible = false;
            GridView3.Visible = false;
            tambahJurnalButton.Visible = false;
            if (GridView1.Rows.Count < 1){
                lbltidakadadatatransaksi.Visible = true;
                Response.AddHeader("REFRESH", "1.5;URL=jurnal.aspx");
                TablePertanggalSort.Visible = true;
                lblgridview1.Visible = false;
                GridView3.Visible = true;
                lblKetJurnal.Visible = true;
            }
            
        }
        private void dataBlmDijurnal(){
            lblgridview1.Visible = true;
            SqlCommand cmd = new SqlCommand("select distinct transaksiID, tanggal, deskripsi, transaksi.noTransaksi, nominal from transaksi left join jurnal on transaksi.noTransaksi = jurnal.noTransaksi left join detail_jurnal on jurnal.noJurnal = detail_jurnal.noJurnal where detail_jurnal.rekeningID is null order by tanggal desc", koneksi);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            koneksi.Close();
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }
        private void dataSemuaJurnal(){
            SqlCommand cmd = new SqlCommand("select distinct transaksiID, jurnal.noJurnal, tanggal, deskripsi, transaksi.noTransaksi from transaksi left join jurnal on transaksi.noTransaksi = jurnal.noTransaksi left join detail_jurnal on jurnal.noJurnal = detail_jurnal.noJurnal where detail_jurnal.rekeningID is not null order by tanggal desc", koneksi);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            koneksi.Close();
            GridView3.DataSource = dt;
            GridView3.DataBind();
        }
        protected void linkPilih_Click(object sender, EventArgs e){
            GridView1.Visible = false;
            lblgridview1.Visible = false;
            Table1.Visible = true;
            Table2.Visible = true;
            int transaksiID = Convert.ToInt32((sender as LinkButton).CommandArgument);
            string deskripsi, noTransaksi, tanggal, nominal;
            koneksi.Open();
            string query = "select *, format (getdate(), 'yyyy/MM/dd') as tanggalformat from transaksi where transaksiID = '" + transaksiID + "'";
            SqlCommand cmd = new SqlCommand(query, koneksi);
            cmd.CommandType = CommandType.Text;
            SqlDataReader dr = cmd.ExecuteReader();
            dr.Read();
                tanggal = dr["tanggalformat"].ToString();
                deskripsi = dr["deskripsi"].ToString();
                noTransaksi = dr["noTransaksi"].ToString();
                nominal = dr["nominal"].ToString();
            dr.Close();
            lblnoJurnal.Text = transaksiID + "/" + tanggal;
            lblnoTransaksi.Text = noTransaksi;
            lbldeskripsi.Text = deskripsi;
            lblNominal.Text = String.Format("{0:n0}", Convert.ToInt32(nominal));
            string jenisjurnal = noTransaksi;
            lbltrjp.Text = noTransaksi.Substring(0,1);
        }
        protected void linkEdit_Click(object sender, EventArgs e)
        {
            lblBoolEdit.Text = "Edit";
            tambahJurnalButton.Visible = false;
            GridView3.Visible = false;
            TablePertanggalSort.Visible = false;
            lblKetJurnal.Visible = false;
            LabelHalaman.Visible = false;
            Table1.Visible = true;
            Table2.Visible = true;
            string rekeningID_debet, rekeningID_kredit;
            int transaksiID = Convert.ToInt32((sender as LinkButton).CommandArgument);

            koneksi.Open();
            string query = "select distinct transaksiID, transaksi.noTransaksi, jurnal.noJurnal, deskripsi, nominal from transaksi left join jurnal on transaksi.noTransaksi = jurnal.noTransaksi left join detail_jurnal on jurnal.noJurnal = detail_jurnal.noJurnal where transaksiID = '" + transaksiID + "'";
            SqlCommand cmd = new SqlCommand(query, koneksi);
            cmd.CommandType = CommandType.Text;
            SqlDataReader dr = cmd.ExecuteReader();
            dr.Read();
                noTransaksi = dr["noTransaksi"].ToString();
                noJurnal = dr["noJurnal"].ToString();
                deskripsi = dr["deskripsi"].ToString();
                nominal = dr["nominal"].ToString();
            dr.Close();

            lblnoTransaksi.Text = noTransaksi;
            lblnoJurnal.Text = noJurnal;
            lbldeskripsi.Text = deskripsi;
            lblNominal.Text = String.Format("{0:n0}", Convert.ToInt32(nominal));
            DataTable dataTable1 = new DataTable();

            string querydebet = ("select distinct transaksiID, rekeningID, debet, kredit from bumdesApp.dbo.transaksi left join bumdesApp.dbo.jurnal on transaksi.noTransaksi = jurnal.noTransaksi left join bumdesApp.dbo.detail_jurnal on jurnal.noJurnal = detail_jurnal.noJurnal where transaksi.transaksiID = '" + transaksiID + "' and kredit = 0");
            SqlCommand cmd1 = new SqlCommand(querydebet, koneksi);
            cmd1.CommandType = CommandType.Text;
            SqlDataReader dr1 = cmd1.ExecuteReader();
            dr1.Read();
            rekeningID_debet = dr1["rekeningID"].ToString();
            dr1.Close();
            DropDownListRekening.SelectedValue = rekeningID_debet;

            string querykredit = ("select distinct transaksiID, rekeningID, debet, kredit from bumdesApp.dbo.transaksi left join bumdesApp.dbo.jurnal on transaksi.noTransaksi = jurnal.noTransaksi left join bumdesApp.dbo.detail_jurnal on jurnal.noJurnal = detail_jurnal.noJurnal where transaksi.transaksiID = '" + transaksiID + "' and debet = 0");
            SqlCommand cmd2 = new SqlCommand(querykredit, koneksi);
            cmd2.CommandType = CommandType.Text;
            SqlDataReader dr2 = cmd2.ExecuteReader();
            dr2.Read();
            rekeningID_kredit = dr2["rekeningID"].ToString();
            dr2.Close();
            DropDownListRekening2.SelectedValue = rekeningID_kredit;
            koneksi.Close();
        }
        protected void linkHapus_Click(object sender, EventArgs e){
            string noJurnal = (sender as LinkButton).CommandArgument;
            SqlCommand cmd = new SqlCommand("delete from detail_jurnal where noJurnal = '" + noJurnal + "' delete from jurnal where noJurnal = '" + noJurnal + "'", koneksi);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            koneksi.Close();
            Response.Redirect("jurnal.aspx");
        }
        
        public void dataRekening(){
            if (!IsPostBack){
                koneksi.Open();
                SqlCommand com = new SqlCommand("select rekeningID, rekeningID + ' - ' + namaRekening as namaRekening from rekening order by rekeningID", koneksi);
                SqlDataAdapter da = new SqlDataAdapter(com);
                DataTable ds = new DataTable();
                da.Fill(ds);
                DropDownListRekening.DataSource = ds;
                DropDownListRekening.DataTextField = "namaRekening";
                DropDownListRekening.DataValueField = "rekeningID";
                DropDownListRekening.DataBind();
                DropDownListRekening2.DataSource = ds;
                DropDownListRekening2.DataTextField = "namaRekening";
                DropDownListRekening2.DataValueField = "rekeningID";
                DropDownListRekening2.DataBind();
            }
        }
        protected void simpanData_Click(object sender, EventArgs e){
            string noTransaksi = lblnoTransaksi.Text.ToString();
            string noJurnal = lblnoJurnal.Text.ToString();
            koneksi.Open();
            string query = ("select nominal from transaksi where noTransaksi='" + noTransaksi + "'");
            SqlCommand cmd = new SqlCommand(query, koneksi);
            cmd.CommandType = CommandType.Text;
            SqlDataReader dr = cmd.ExecuteReader();
            dr.Read();
            int nominal = Convert.ToInt32(dr["nominal"].ToString());
            dr.Close();
            koneksi.Close();

            if (DropDownListRekening2.SelectedValue == "1.110" && saldoKas < nominal)
            {
                lblCekSaldo.Text = "Maaf, Saldo Kas Tidak Cukup.";
                simpanData.Enabled = false;
            }
            else if (DropDownListRekening2.SelectedValue == "1.120" && saldoBank < nominal)
            {
                lblCekSaldo.Text = "Maaf, Saldo Bank Tidak Cukup.";
                simpanData.Enabled = false;
            }
            else if (saldoKas < nominal && saldoBank < nominal)
            {
                lblCekSaldo.Text = "Maaf, Saldo Kas dan Bank Tidak Cukup.";
                simpanData.Enabled = false;
            }
            else if (DropDownListRekening.SelectedValue == DropDownListRekening2.SelectedValue)
            {
                Response.Write(@"<script>alert('Data yang dimasukkan tidak valid.')</script>");
                Table1.Visible = true;
                Table2.Visible = true;
                GridView1.Visible = false;
                lblgridview1.Visible = false;
            }
            else
            {
                if (lblBoolEdit.Text == "Edit")
                {
                    editdata();
                    savedata(noTransaksi, noJurnal);
                }
                else savedata(noTransaksi, noJurnal);
                Response.Write(@"<script>alert('Data Tersimpan.')</script>");
            }
            Response.Redirect("jurnal.aspx");
        }
        public void savedata(string noTransaksi, string noJurnal)
        {
            string jenisJurnal;
            if (lbltrjp.Text == "T") jenisJurnal = "Tr"; else jenisJurnal = "JP";
            koneksi.Open();
            string query = ("select nominal from transaksi where noTransaksi='" + noTransaksi + "'");
            SqlCommand cmd = new SqlCommand(query, koneksi);
            cmd.CommandType = CommandType.Text;
            SqlDataReader dr = cmd.ExecuteReader();
            dr.Read();
            int nominal = Convert.ToInt32(dr["nominal"].ToString());
            dr.Close();
            string query1;
            if (lblBoolEdit.Text == "Edit")
            {
                query1 = "insert into bumdesapp.dbo.detail_jurnal(detail_jurnal.noJurnal, rekeningID, debet, kredit) values('" + noJurnal + "', '" + DropDownListRekening.SelectedValue + "', '" + nominal + "', '0')" +
                            "insert into bumdesapp.dbo.detail_jurnal(detail_jurnal.noJurnal, rekeningID, debet, kredit) values('" + noJurnal + "', '" + DropDownListRekening2.SelectedValue + "', '0', '" + nominal + "')";
            }
            else
            {

                query1 = "insert into bumdesapp.dbo.jurnal(noTransaksi, jurnal.noJurnal, jenis, username) values('" + noTransaksi + "', '" + noJurnal + "', '" + jenisJurnal + "', 'admin')" +
                                "insert into bumdesapp.dbo.detail_jurnal(detail_jurnal.noJurnal, rekeningID, debet, kredit) values('" + noJurnal + "', '" + DropDownListRekening.SelectedValue + "', '" + nominal + "', '0')" +
                                "insert into bumdesapp.dbo.detail_jurnal(detail_jurnal.noJurnal, rekeningID, debet, kredit) values('" + noJurnal + "', '" + DropDownListRekening2.SelectedValue + "', '0', '" + nominal + "')";
            }
            SqlCommand cmd1 = new SqlCommand();
            cmd1.CommandText = query1;
            cmd1.Connection = koneksi;
            cmd1.ExecuteNonQuery();
            koneksi.Close();

        }
        protected void editdata()
        {
            string query = "delete from bumdesapp.dbo.detail_jurnal where noJurnal = '" + lblnoJurnal.Text.ToString() + "'";
            koneksi.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = query;
            cmd.Connection = koneksi;
            cmd.ExecuteNonQuery();
            koneksi.Close();
            //for (int i = 0; i < GridView2.Rows.Count; i++)
            //{
            //    string query = "insert into bumdesapp.dbo.detail_jurnal (detail_jurnal.noJurnal, rekeningID, debet, kredit) values('" + lblnoJurnal.Text.ToString() + "', '" + GridView2.Rows[i].Cells[1].Text + "', '" + Convert.ToInt32(GridView2.Rows[i].Cells[2].Text) + "', '" + Convert.ToInt32(GridView2.Rows[i].Cells[3].Text) + "')";
            //    String mycon = WebConfigurationManager.ConnectionStrings["koneksi"].ToString();
            //    SqlConnection con = new SqlConnection(mycon);
            //    con.Open();
            //    SqlCommand cmd = new SqlCommand();
            //    cmd.CommandText = query;
            //    cmd.Connection = con;
            //    cmd.ExecuteNonQuery();
            //    con.Close();
            //}
        }
        //protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e){
        //    if (e.Row.RowType == DataControlRowType.DataRow){
        //        totalDebet += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Debet"));
        //        totalKredit += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Kredit"));
        //    }
        //    else if(e.Row.RowType==DataControlRowType.Footer){
        //        e.Row.BackColor = System.Drawing.Color.FromArgb(240, 240, 240);
        //        e.Row.Cells[0].ColumnSpan = 2;
        //        e.Row.Cells[0].Text = "TOTAL";
        //        e.Row.Cells[2].Text = string.Format("{0:n0}", totalDebet);
        //        e.Row.Cells[3].Text = string.Format("{0:n0}", totalKredit);
        //        e.Row.Cells.RemoveAt(1);
        //    }
        //}
        /*
        public void ifelsetotaldksama(){
            //koneksi.Open();
            //string query = "select nominal from transaksi left join jurnal on transaksi.noTransaksi = jurnal.noTransaksi left join detail_jurnal on detail_jurnal.noJurnal = jurnal.noJurnal where rekeningID is null";
            //SqlCommand cmd = new SqlCommand(query, koneksi);
            //cmd.CommandType = CommandType.Text;
            //SqlDataReader dr = cmd.ExecuteReader();
            if (totalDebet == totalKredit && totalDebet != 0){
                simpanDataBtn.ForeColor = System.Drawing.Color.White;
                simpanDataBtn.BackColor = System.Drawing.Color.FromArgb(29, 161, 242);
                simpanDataBtn.Enabled = true;
                lbltotaldkhrssama.Visible = false;
            }
            else{
                simpanDataBtn.ForeColor = System.Drawing.Color.White;
                simpanDataBtn.BackColor = System.Drawing.Color.FromArgb(180, 180, 180);
                simpanDataBtn.Enabled = false;
                lbltotaldkhrssama.Visible = true;
                lbltotaldkhrssama.Text = "   Total Debet dan Kredit Harus Sama.";
                lbltotaldkhrssama.ForeColor = System.Drawing.Color.Red;
            }
        }
        */
        /*
        public void tambahRowGV2(){
            if (DropDownListRekening.SelectedValue == DropDownListRekening2.SelectedValue) Response.Write("<script>alert('Data yang dimasukkan tidak valid!')</script>");
            else
            {
                try
                {
                    DataTable dataTable1 = new DataTable();
                    if (ViewState["Row"] != null)
                    {
                        dataTable1 = (DataTable)ViewState["Row"];
                        DataRow dataRow1 = dataTable1.NewRow();
                        dataRow1 = dataTable1.NewRow();
                        if (dataTable1.Rows.Count > 0)
                        {
                            dataRow1["rekeningID"] = DropDownListRekening.Text;
                            //dataRow1["debet"] = debetTextBox.Text;
                            //dataRow1["kredit"] = kreditTextBox.Text;
                            dataRow1["debet"] = DropDownListRekening.SelectedValue;
                            dataRow1["kredit"] = DropDownListRekening2.SelectedValue;
                            dataTable1.Rows.Add(dataRow1);
                            ViewState["Row"] = dataTable1;
                            GridView2.DataSource = ViewState["Row"];
                            GridView2.DataBind();
                        }
                    }
                    else
                    {
                        dataTable1.Columns.Add("rekeningID", typeof(string));
                        dataTable1.Columns.Add("debet", typeof(int));
                        dataTable1.Columns.Add("kredit", typeof(int));
                        DataRow dataRow2 = dataTable1.NewRow();
                        dataRow2 = dataTable1.NewRow();
                        dataRow2["rekeningID"] = DropDownListRekening.Text;
                        //dataRow2["debet"] = debetTextBox.Text;
                        //dataRow2["kredit"] = kreditTextBox.Text;
                        dataTable1.Rows.Add(dataRow2);
                        ViewState["Row"] = dataTable1;
                        GridView2.DataSource = ViewState["Row"];
                        GridView2.DataBind();
                    }
                }
                catch (Exception ex) { }
            }
        }
        */

        //public void GridView2_RowDeleting(object sender, GridViewDeleteEventArgs e){
        //    DataTable dt1 = (DataTable)ViewState["Row"];
        //    dt1.Rows[e.RowIndex].Delete();
        //    GridView2.DataSource = dt1;
        //    GridView2.DataBind();
        //    tambahData.Visible = true;
        //    simpanDataBtn.Visible = true;
        //    ifelsetotaldksama();
        //    Table2.Visible = true;
        //}
        //protected void simpanDataBtn_Click(object sender, EventArgs e){
        //    if (lblBoolEdit.Text == "Edit") hapusDatayangAda();
        //    else saveDataBaru();
        //    Response.Redirect("jurnal.aspx");
        //}
        //protected void saveDataBaru() {
        //    string query2 = "insert into bumdesapp.dbo.jurnal (noTransaksi, jurnal.noJurnal, username) values('" + lblnoTransaksi.Text.ToString() + "', '" + lblnoJurnal.Text.ToString() + "', 'admin') ";
        //    String mycon2 = WebConfigurationManager.ConnectionStrings["koneksi"].ToString();
        //    SqlConnection con2 = new SqlConnection(mycon2);
        //    con2.Open();
        //    SqlCommand cmd2 = new SqlCommand();
        //    cmd2.CommandText = query2;
        //    cmd2.Connection = con2;
        //    cmd2.ExecuteNonQuery();
        //    con2.Close();
        //    for (int i = 0; i < GridView2.Rows.Count; i++) {
        //        string query = "insert into bumdesapp.dbo.detail_jurnal (detail_jurnal.noJurnal, rekeningID, debet, kredit) values('" + lblnoJurnal.Text.ToString() + "', '" + GridView2.Rows[i].Cells[1].Text + "', '" + Convert.ToInt32(GridView2.Rows[i].Cells[2].Text) + "', '" + Convert.ToInt32(GridView2.Rows[i].Cells[3].Text) + "')";
        //        String mycon = WebConfigurationManager.ConnectionStrings["koneksi"].ToString();
        //        SqlConnection con = new SqlConnection(mycon);
        //        con.Open();
        //        SqlCommand cmd = new SqlCommand();
        //        cmd.CommandText = query;
        //        cmd.Connection = con;
        //        cmd.ExecuteNonQuery();
        //        con.Close();
        //    }
        //}

    }
}