<%@ Page Title="" Language="C#" MasterPageFile="~/hlm/Site1.Master" AutoEventWireup="true" CodeBehind="rekening.aspx.cs" EnableEventValidation="false" Inherits="bumdesAPP.hlm.rekening" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="body">
        <h3 style="font-weight:400; text-align:left">
            BUMDES TUNAS MANDIRI DESA NEGARA RATU<br />
            DATA REKENING<br /></h3>
        <table>
            <tr>
                <td style="width: 12%;"><asp:Button ID="tambahRekening" runat="server" Text="Tambah" style="border-radius: 16px" BorderStyle="Solid" Height="34px" Width="100px" BorderWidth="1.5px" BorderColor="#1DA1F2" OnClick="tambahRekening_Click" /></td>
                <td style="width: 12%"><asp:Button ID="cancelrekening" runat="server" Text="Batal" style="border-radius: 16px" BorderStyle="None" BackColor="#CC3300" ForeColor="white" Height="34px" Width="100px" OnClick="cancelrekening_Click" /></td>
            </tr>
        </table>
        <asp:Table ID="Table1"  style="padding:17px; border: solid 1px #ccc; border-radius:10px" runat="server">
            <asp:TableRow>
                <asp:TableCell style="text-align:right; padding-right:20px" Width="140px">Kode Rekening<a style="color:red">*</a></asp:TableCell>
                <asp:TableCell Height="40px"><asp:TextBox ID="rekeningIDTextBox" placeholder="Kode Rekening" runat="server" MaxLength="10" CssClass="textboxstyle" BorderColor="#1da1f2" BorderStyle="Solid" BorderWidth="1px" ForeColor="#777777" Width="300px" Height="34px"></asp:TextBox></asp:TableCell>
                <asp:TableCell>
                    <asp:Label ForeColor="white" ID="rekUID" runat="server" Text=""></asp:Label></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell style="text-align:right; padding-right:20px">Nama Rekening<a style="color:red">*</a></asp:TableCell>
                <asp:TableCell Height="40px"><asp:TextBox ID="namaRekeningTextBox" placeholder="Nama Rekening" runat="server" MaxLength="50" CssClass="textboxstyle" BorderColor="#1da1f2" BorderStyle="Solid" BorderWidth="1px" ForeColor="#777777" Width="300px" Height="34px"></asp:TextBox></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell style="text-align:right; padding-right:20px">Posisi<a style="color:red">*</a></asp:TableCell>
                <asp:TableCell Height="40px"><asp:DropDownList ID="DropDownPosisi"  placeholder="Posisi" runat="server" style="color:#666666" CssClass="textboxstyle" BorderColor="#1da1f2" BorderStyle="Solid" BorderWidth="1px" ForeColor="#777777" Width="314px" Height="38px">
                        <asp:ListItem>-- Posisi --</asp:ListItem>
                        <asp:ListItem>Neraca</asp:ListItem>
                        <asp:ListItem>Laba Rugi</asp:ListItem>
                    </asp:DropDownList></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell style="text-align:right; padding-right:20px">Saldo Normal<a style="color:red">*</a></asp:TableCell>
                 <asp:TableCell><asp:DropDownList ID="saldoNormalDropDown"  placeholder="Saldo Normal" runat="server" style="color: #666666;
                                padding-left: 12px;
                                border-radius: 3px;
                                background-color: #fff;"
                    BorderColor="#1da1f2" BorderStyle="Solid" BorderWidth="1px" ForeColor="#777777" Width="314px" Height="38px">
                        <asp:ListItem>-- Saldo Normal --</asp:ListItem>
                        <asp:ListItem>Debet</asp:ListItem>
                        <asp:ListItem>Kredit</asp:ListItem>
                    </asp:DropDownList></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell></asp:TableCell>
                <asp:TableCell HorizontalAlign="Right" Height="40px"><asp:Button ID="SimpanRekening" OnClientClick="alert('Data Tersimpan.')" runat="server" Text="Simpan" style="border-radius: 16px" BackColor="#1DA1F2" BorderStyle="none" ForeColor="white" Height="34px" Width="100px" OnClick="SimpanRekening_Click" /></asp:TableCell>
            </asp:TableRow>
        </asp:Table>
        <div style="float:right">
            <asp:Button ID="ButtonPrint" runat="server" Text="Print PDF" style="border-radius: 6px" BorderStyle="Solid" Height="38px" Width="100px" BorderWidth="1.5px" ForeColor="#1DA1F2" BackColor="White" BorderColor="#1DA1F2" OnClick="ButtonPrint_Click" />
        </div>
        <br />
        <br />
        <div style="text-align:center"><asp:Label ID="Label1" runat="server" Text="Label"></asp:Label></div>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" BorderStyle="None" CssClass="cssgridview" AlternatingRowStyle-CssClass="alt" >
            <Columns>
                <asp:BoundField HeaderText="KODE" DataField="rekeningID" HeaderStyle-Width="8%" HeaderStyle-HorizontalAlign="Left" />
                <asp:BoundField HeaderText="NAMA REKENING" DataField="namaRekening" HeaderStyle-Width="30%" HeaderStyle-HorizontalAlign="Left" />
                <asp:BoundField HeaderText="POSISI" DataField="posisi" HeaderStyle-Width="13%" HeaderStyle-HorizontalAlign="Left" />
                <asp:BoundField HeaderText="SALDO NORMAL" DataField="saldoNormal" HeaderStyle-Width="10%" HeaderStyle-HorizontalAlign="Left" />
                <asp:TemplateField HeaderText="OPSI" HeaderStyle-Width="13%" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" >
                    <ItemTemplate>
                        <asp:LinkButton ID="linkEdit" Text="Edit" runat="server" CommandArgument='<%# Eval("rekUID") %>' OnClick="linkEdit_Click"></asp:LinkButton>
                        <asp:LinkButton ID="linkHapus" Text="Hapus" runat="server" CommandArgument='<%# Eval("rekUID") %>' OnClientClick="return confirm('Apakah ingin menghapus data?')" OnClick="linkHapus_Click"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="false">
            <Columns>
                <asp:BoundField HeaderText="KODE" ItemStyle-Width="100px" DataField="rekeningID" />
                <asp:BoundField HeaderText="NAMA REKENING" ItemStyle-Width="200px" DataField="namaRekening" />
                <asp:BoundField HeaderText="POSISI" ItemStyle-Width="100px" DataField="posisi" />
                <asp:BoundField HeaderText="SALDO NORMAL" ItemStyle-Width="100px" DataField="saldoNormal" />
            </Columns>
        </asp:GridView>
    </div>
    <script>
            function hanyaAngka(evt) {
                var charcode = (evt.which) ? evt.which : event.keyCode
                if (charcode > 31 && (charcode < 48 || charcode > 57))
                    return false;
                return true;
            }
        </script>
</asp:Content>
