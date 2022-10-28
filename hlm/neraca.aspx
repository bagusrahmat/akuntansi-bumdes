<%@ Page Title="" Language="C#" MasterPageFile="~/hlm/Site1.Master" AutoEventWireup="true" CodeBehind="neraca.aspx.cs" Inherits="bumdesAPP.hlm.neraca" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="body">
        <h3 style="font-weight:400; text-align:left">
            BUMDES TUNAS MANDIRI DESA NEGARA RATU<br />
            LAPORAN NERACA<br /></h3>
        <table>
            <tr>
                <td style="width: 120px">Periode</td>
                <td style="width: 10px">:</td>
                <td><asp:DropDownList ID="DropDownBulan" runat="server" CssClass="textboxstyle" BorderColor="#1DA1F2" BorderStyle="Solid" 
                    BorderWidth="1px" style="color:#666" ForeColor="#000CCC" Width="120px" Height="38px">
                    <asp:ListItem Text="Januari" Value="1"></asp:ListItem>
                    <asp:ListItem Text="Februari" Value="2"></asp:ListItem>
                    <asp:ListItem Text="Maret" Value="3"></asp:ListItem>
                    <asp:ListItem Text="April" Value="4"></asp:ListItem>
                    <asp:ListItem Text="Mei" Value="5"></asp:ListItem>
                    <asp:ListItem Text="Juni" Value="6"></asp:ListItem>
                    <asp:ListItem Text="Juli" Value="7"></asp:ListItem>
                    <asp:ListItem Text="Agustus" Value="8"></asp:ListItem>
                    <asp:ListItem Text="September" Value="9"></asp:ListItem>
                    <asp:ListItem Text="Oktober" Value="10"></asp:ListItem>
                    <asp:ListItem Text="November" Value="11"></asp:ListItem>
                    <asp:ListItem Text="Desember" Value="12"></asp:ListItem>
                    </asp:DropDownList></td>
                <td> </td>
                <td><asp:DropDownList ID="DropDownTahun" runat="server" CssClass="textboxstyle" BorderColor="#1DA1F2" BorderStyle="Solid" 
                    BorderWidth="1px" style="color:#666" ForeColor="#000CCC" Width="80px" Height="38px"></asp:DropDownList></td>
                <td style="padding-left:10px"><asp:Button ID="lihatNeraca" runat="server" Text="Lihat" style="background-color:#1DA1F2; border-radius: 16px" ForeColor="white" BorderStyle="none" Height="34px" Width="100px" OnClick="lihatNeraca_Click" /></td>
            </tr>
        </table>
        <div style="padding-left:90%"><asp:Button ID="ButtonPrint" runat="server" Text="Print PDF" style="border-radius: 6px" BorderStyle="Solid" Height="38px" Width="100px" BorderWidth="1.5px" ForeColor="#1DA1F2" BackColor="White" BorderColor="#1DA1F2" OnClick="ButtonPrint_Click" /></div>
        <br />
        <div style="text-align:center"><asp:Label ID="lblperiode" runat="server"></asp:Label></div>
        <div style="width:50%; float:left">
        <asp:GridView ID="GridView1" CssClass="cssgridview" AlternatingRowStyle-CssClass="alt" runat="server" ShowFooter="true" OnRowDataBound="GridView1_RowDataBound"
            AutoGenerateColumns="false">
            <Columns>
                <asp:BoundField HeaderText="KODE" DataField="rekeningID" HeaderStyle-Width="5%" HeaderStyle-HorizontalAlign="Left" />
                <asp:BoundField HeaderText="NAMA REKENING" DataField="namaRekening" HeaderStyle-Width="32%" HeaderStyle-HorizontalAlign="Left" />
                <asp:BoundField HeaderText="SALDO" DataFormatString="{0:n0}" ItemStyle-HorizontalAlign="Right" DataField="saldo" FooterStyle-HorizontalAlign="Right" HeaderStyle-Width="13%" />
            </Columns>
        </asp:GridView>
        </div>
        
        <div style="width:50%; float:right">
        <asp:GridView ID="GridView2"  CssClass="cssgridview" AlternatingRowStyle-CssClass="alt" runat="server" ShowFooter="true" OnRowDataBound="GridView2_RowDataBound" AutoGenerateColumns="false">
            <Columns>
                <asp:BoundField HeaderText="KODE" DataField="rekeningID" HeaderStyle-Width="5%"  HeaderStyle-HorizontalAlign="Left" />
                <asp:BoundField HeaderText="NAMA REKENING" DataField="namaRekening" HeaderStyle-Width="32%"  HeaderStyle-HorizontalAlign="Left" />
                <asp:BoundField HeaderText="SALDO" DataFormatString="{0:n0}" ItemStyle-HorizontalAlign="right" DataField="saldo" FooterStyle-HorizontalAlign="Right" HeaderStyle-Width="13%" />
            </Columns>
        </asp:GridView>
        </div>
        <br />
        
        <br />
        <asp:GridView ID="GridView3" Visible="false" runat="server" ShowFooter="true" OnRowDataBound="GridView3_RowDataBound"
            AutoGenerateColumns="false">
            <Columns>
                <asp:BoundField HeaderText="DEBET" DataFormatString="{0:n0}" ItemStyle-HorizontalAlign="Right" DataField="debet" FooterStyle-HorizontalAlign="Right" />
                <asp:BoundField HeaderText="KREDIT" DataFormatString="{0:n0}" ItemStyle-HorizontalAlign="Right" DataField="kredit" FooterStyle-HorizontalAlign="Right" />
            </Columns>
        </asp:GridView>

    </div>
    <br />
</asp:Content>
