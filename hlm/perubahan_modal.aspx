 <%@ Page Title="" Language="C#" MasterPageFile="~/hlm/Site1.Master" AutoEventWireup="true" CodeBehind="perubahan_modal.aspx.cs" Inherits="bumdesAPP.hlm.perubahan_modal" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="body">
        <h3 style="font-weight:400; text-align:left">
            BUMDES TUNAS MANDIRI DESA NEGARA RATU<br />
            LAPORAN LABA RUGI<br /></h3>
        <table>
            <tr>
                <td style="width: 120px">Periode</td>
                <td style="width: 10px">:</td>
                <td><asp:DropDownList ID="DropDownTahun" runat="server" CssClass="textboxstyle" BorderColor="#1DA1F2" BorderStyle="Solid" 
                    BorderWidth="1px" style="color:#666" ForeColor="#000CCC" Width="80px" Height="38px"></asp:DropDownList></td>
                <td style="padding-left:10px"><asp:Button ID="Btnlihat" runat="server" Text="Lihat" style="background-color:#1DA1F2; border-radius: 16px" ForeColor="white" BorderStyle="none" Height="34px" Width="100px" OnClick="Btnlihat_Click" /></td>
            </tr>
        </table>
        <div style="padding-left: 90%"><asp:Button ID="ButtonPrint" runat="server" Text="Print PDF" style="border-radius: 6px" BorderStyle="Solid" Height="38px" Width="100px" BorderWidth="1.5px" ForeColor="#1DA1F2" BackColor="White" BorderColor="#1DA1F2" OnClick="ButtonPrint_Click" /></div>
        <br />
        <div style="text-align:center"><asp:Label ID="lblperiode" runat="server"></asp:Label></div>
        <div>
        <asp:Label ID="Labelperiode" runat="server"></asp:Label>
        <asp:GridView ID="GridView1" CssClass="cssgridview" AlternatingRowStyle-CssClass="alt" runat="server" AutoGenerateColumns="false" OnRowDataBound="GridView1_RowDataBound" ShowFooter="true">
            <Columns>
                <asp:BoundField HeaderText="NAMA REKENING" DataField="namaRekening" HeaderStyle-Width="32%" HeaderStyle-HorizontalAlign="Left" />
                <asp:BoundField HeaderText="DEBET" DataFormatString="{0:n0}" ItemStyle-HorizontalAlign="Right" DataField="debet" FooterStyle-HorizontalAlign="Right" HeaderStyle-Width="13%" />
                <asp:BoundField HeaderText="KREDIT" DataFormatString="{0:n0}" ItemStyle-HorizontalAlign="Right" DataField="kredit" FooterStyle-HorizontalAlign="Right" HeaderStyle-Width="13%" />
            </Columns>
        </asp:GridView>
        </div>
    </div>
</asp:Content>
