<%@ Page Title="" Language="C#" MasterPageFile="~/hlm/Site1.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="buku_besar.aspx.cs" Inherits="bumdesAPP.hlm.buku_besar" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="body">
        <h3 style="text-align:left; font-weight:400">
            BUMDES TUNAS MANDIRI DESA NEGARA RATU<br />
            LAPORAN BUKU BESAR <asp:Label ID="LabelRekeningBB" runat="server" Text=""></asp:Label><br /></h3>
        <table>
            <tr>
                <td style="width: 120px">Rekening</td>
                <td style="width: 10px">:</td>
                <td><asp:DropDownList ID="DropDownListRekening" runat="server" CssClass="textboxstyle" BorderColor="#1DA1F2" BorderStyle="Solid" BorderWidth="1px" style="color:#666" ForeColor="#000CCC" Width="316px" Height="38px"></asp:DropDownList></td>
            </tr>
        </table>
        <table>
            <tr>
                <td style="width: 120px">Periode<div style="font-size:11px">(mm/dd/yyyy)</div></td>
                <td style="width: 10px">:</td>
                <td><asp:TextBox ID="TextBoxPeriode1" TextMode="Date" runat="server" CssClass="textboxstyle" BorderColor="#1da1f2" BorderStyle="Solid" BorderWidth="1px" style="color:#666" ForeColor="#ccc" Width="130px" Height="34px"></asp:TextBox></td>
                <td>s.d</td>
                <td><asp:TextBox ID="TextBoxPeriode2" TextMode="Date" runat="server" CssClass="textboxstyle" BorderColor="#1da1f2" BorderStyle="Solid" BorderWidth="1px" style="color:#666" ForeColor="#ccc" Width="130px" Height="34px"></asp:TextBox></td>
            </tr>
        </table>
        <table>
            <tr>
                <td style="width:12%"><asp:Button ID="lihatBukuBesar" runat="server" Text="Lihat" style="background-color:#1DA1F2; border-radius: 16px" ForeColor="white" BorderStyle="none" Height="34px" Width="100px" OnClick="lihatBukuBesar_Click" /></td>
                <td style="width:88%; padding-left: 95%"><asp:Button ID="ButtonPrint" runat="server" Text="Print PDF" style="border-radius: 6px" BorderStyle="Solid" Height="38px" Width="100px" BorderWidth="1.5px" ForeColor="#1DA1F2" BackColor="White" BorderColor="#1DA1F2" OnClick="ButtonPrint_Click" /></td>
            </tr>
        </table>
        <br />
        <asp:GridView ID="GridView1" runat="server" CssClass="cssgridview" AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="false" OnPageIndexChanging="GridView1_PageIndexChanging" AllowPaging="true" PagerSettings-Visible="true" PageSize="15" >
            <Columns>
                <asp:BoundField HeaderText="TANGGAL" DataField="tanggal" HeaderStyle-Width="10%" HeaderStyle-HorizontalAlign="Left" DataFormatString="{0:dd-MM-yy}" />
                <asp:BoundField HeaderText="NO. TRANSAKSI" DataField="noTransaksi" HeaderStyle-Width="15%" HeaderStyle-HorizontalAlign="Left" />
                <asp:BoundField HeaderText="DESKRIPSI" DataField="deskripsi" HeaderStyle-Width="50%" HeaderStyle-HorizontalAlign="Left" />
                <asp:BoundField HeaderText="DEBET" DataField="debet" HeaderStyle-Width="15%" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n0}" />
                <asp:BoundField HeaderText="KREDIT" DataField="kredit" HeaderStyle-Width="15%" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n0}" />
            </Columns>
        </asp:GridView>
        <br />
        <asp:Label ID="labelHalaman" runat="server" Text=""></asp:Label>

    </div>
</asp:Content>
