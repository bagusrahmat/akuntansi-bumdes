<%@ Page Title="" Language="C#" MasterPageFile="~/hlm/Site1.Master" AutoEventWireup="true" CodeBehind="transaksi.aspx.cs" Inherits="bumdesAPP.hlm.transaksi" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script src="../Script/JavaScript.js"></script>
    <div class="body">
        <h3 style="font-weight:400; text-align:left">
            BUMDES TUNAS MANDIRI DESA NEGARA RATU<br />
            TRANSAKSI HARIAN<br /></h3>
        <table>
            <tr>
                <td style="width: 150px;"><asp:Button ID="tambahTransaksi" runat="server" Text="Tambah" style="border-radius: 16px" BorderStyle="Solid" Height="34px" Width="100px" ForeColor="White" BorderWidth="1.5px" BorderColor="#1DA1F2" BackColor="#1DA1F2" OnClick="tambahTransaksi_Click" /></td>
                <td><asp:Button Visible="false" ID="cancelTransaksi" runat="server" Text="Batal" style="border-radius: 16px" BorderStyle="None" BackColor="#CC3300" ForeColor="white" Height="34px" Width="100px" OnClick="cancelrekening_Click" /></td>
            </tr>
        </table>
        <br runat="server" id="spasi1" />
        <asp:Table ID="Table1" style="padding:17px; border: solid 1px #ccc; border-radius:10px" Visible="false" runat="server">
            <asp:TableRow>
                <asp:TableCell style="text-align:right; padding-right:20px">No. Transaksi</asp:TableCell>
                <asp:TableCell Height="40px"><asp:TextBox ID="noTransaksiTextBox" Enabled="false" runat="server" CssClass="textboxstyle" BorderColor="#cccccc" BorderStyle="Solid" BorderWidth="1px" Width="300px" Height="34px"></asp:TextBox></asp:TableCell>
            </asp:TableRow>
            
            <asp:TableRow>
                <asp:TableCell style="text-align:right; padding-right:20px">Tanggal<a style="color:red">*</a><div style="font-weight:400;font-size:10px">(MM-DD-YYYY)</div></asp:TableCell>
                <asp:TableCell Height="40px"><asp:TextBox ID="tanggalTransaksiTextBox" TextMode="Date" placeholder="Tanggal" runat="server" CssClass="textboxstyle" BorderColor="#1da1f2" BorderStyle="Solid" BorderWidth="1px" Width="300px" Height="34px"></asp:TextBox></asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell style="text-align:right; padding-right:20px">Deskripsi<a style="color:red">*</a></asp:TableCell>
                <asp:TableCell Height="40px"><asp:TextBox ID="deskripsiTextBox" MaxLength="150" TextMode="MultiLine" runat="server" CssClass="textboxstyle" BorderColor="#1da1f2" BorderStyle="Solid" BorderWidth="1px" Width="298px" Height="60px"></asp:TextBox></asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell style="text-align:right; padding-right:20px">Nominal<a style="color:red">*</a></asp:TableCell>
                <asp:TableCell Height="40px"><asp:TextBox ID="nominalTextBox" MaxLength="13" runat="server" CssClass="textboxstyle" OnKeyPress="return hanyaAngka(event)" BorderColor="#1da1f2" BorderStyle="Solid" BorderWidth="1px" Width="300px" Height="34px" AutoPostBack="true"></asp:TextBox></asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell></asp:TableCell>
                <asp:TableCell Height="40px"><asp:CheckBox ID="CheckBoxJP" AutoPostBack="true" OnCheckedChanged="CheckBoxJP_Changed" Text="Jurnal Penyesuaian" runat="server" /></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell></asp:TableCell>
                <asp:TableCell HorizontalAlign="Right" Height="40px"><asp:Button ID="simpanTransaksi" OnClientClick="validasiTransaksi()" runat="server" Text="Simpan" style="border-radius: 16px" BackColor="#1DA1F2" BorderStyle="none" ForeColor="white" Height="34px" Width="100px" OnClick="simpanTransaksi_Click" /></asp:TableCell>
            </asp:TableRow>

        </asp:Table>
        <br runat="server" id="spasi2" />
        <asp:Table ID="Table2" runat="server">
            <asp:TableRow>
                <asp:TableCell style="width:120px">Periode<div style="font-size:11px">(mm/dd/yyyy)</div></asp:TableCell>
                <asp:TableCell>:</asp:TableCell>
                <asp:TableCell><asp:TextBox ID="TextBoxPeriode1" TextMode="Date" runat="server" CssClass="textboxstyle" BorderColor="#1da1f2" BorderStyle="Solid" BorderWidth="1px" style="color:#666" ForeColor="#ccc" Width="150px" Height="34px"></asp:TextBox></asp:TableCell>
                <asp:TableCell>s.d</asp:TableCell>
                <asp:TableCell><asp:TextBox ID="TextBoxPeriode2" TextMode="Date" runat="server" CssClass="textboxstyle" BorderColor="#1da1f2" BorderStyle="Solid" BorderWidth="1px" style="color:#666" ForeColor="#ccc" Width="150px" Height="34px"></asp:TextBox></asp:TableCell>
                <asp:TableCell><asp:Button ID="lihatTransaksiPeriode" runat="server" Text="Lihat" style="background-color:#1DA1F2; border-radius: 16px" ForeColor="white" BorderStyle="none" Height="34px" Width="100px" OnClick="lihatTransaksiPeriode_Click" /></asp:TableCell>
                <asp:TableCell Width="470px"><div style="float:right"><asp:Button ID="ButtonPrint" runat="server" Text="Print PDF" style="border-radius: 6px" BorderStyle="Solid" Height="38px" Width="100px" BorderWidth="1.5px" ForeColor="#1DA1F2" BackColor="White" BorderColor="#1DA1F2" OnClick="ButtonPrint_Click" /></div></asp:TableCell>
            </asp:TableRow>
        </asp:Table>
        
        <br />
        <div style="text-align:center;"><asp:Label ID="lblperiode" runat="server" Visible="false" Text=""></asp:Label></div>
        <asp:GridView ID="GridView1" runat="server" CssClass="cssgridview" AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="false" OnPageIndexChanging="GridView1_PageIndexChanging" AllowPaging="true" PagerSettings-Visible="true" PageSize="15" >
            <Columns>
                <asp:BoundField HeaderText="TANGGAL" DataField="tanggal" HeaderStyle-Width="15%" HeaderStyle-HorizontalAlign="Left" DataFormatString="{0:dd-MM-yy}" />
                <asp:BoundField HeaderText="NO.TRANSAKSI" DataField="noTransaksi" HeaderStyle-Width="15%" HeaderStyle-HorizontalAlign="Left" />
                <asp:BoundField HeaderText="DESKRIPSI" DataField="deskripsi" HeaderStyle-Width="40%" HeaderStyle-HorizontalAlign="Left" />
                <asp:BoundField HeaderText="NOMINAL" ItemStyle-CssClass="itemgridviewnominal" DataFormatString="{0:n0}" DataField="nominal" HeaderStyle-Width="17%" ItemStyle-HorizontalAlign="right" />
                <asp:TemplateField HeaderText="OPSI" HeaderStyle-Width="13%" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" >
                    <ItemTemplate>
                        <asp:LinkButton ID="linkEdit" Text="Edit" runat="server" CommandArgument='<%# Eval("transaksiID") %>' OnClick="linkEdit_Click"></asp:LinkButton>
                        <asp:LinkButton ID="linkHapus" Text="Hapus" runat="server" CommandArgument='<%# Eval("transaksiID") %>' OnClientClick="return confirm('Apakah ingin menghapus data?')" OnClick="linkHapus_Click"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <br />
        <asp:Label ID="LabelHalaman" runat="server"></asp:Label>

        <asp:Label ID="labeltransaksiID" runat="server"></asp:Label>
    </div>
    <script>
        function hanyaAngka(evt) {
            var charcode = (evt.which) ? evt.which : event.keyCode
            if (charcode > 31 && (charcode < 48 || charcode > 57))
                return false;
            return true;
        }
        function validasiTransaksi() {
            var deskripsi = document.getElementById('deskripsiTextBox.Text').value;
            var nominal = document.getElementById('nominalTextBox.Text').value;

            if (deskripsi == ""){
                alert("Deskripsi Wajib Diisi.");
                return false;
            }
            else if (nominal == ""){
                alert("Nominal Wajib Diisi.");
                return false;
            }
            else if (nominal == "" && deskripsi == "") {
                alert("Field F Wajib Diisi.");
                return false;
            }
            else return true;
        }
        function onlyNumbers(evt) {
            var e = event || evt; // for trans-browser compatibility
            var charCode = e.which || e.keyCode;

            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;
            return true;

        };
        function todesimal() {
            $("#nominal").maskMoney({ thousands: '.', precision: 0 });
            var sharga = $("#nominal").val();
            var xharga = sharga.replace(/[^0-9]/g, '');
            $("#angka").val(xharga);
        };
    </script>
</asp:Content>
