<%@ Page Title="" Language="C#" MasterPageFile="~/hlm/Site1.Master" AutoEventWireup="true" CodeBehind="jurnal_penyesuaian.aspx.cs" Inherits="bumdesAPP.hlm.jurnal_penyesuaian" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="body">
        <h3 style="font-weight:400; text-align:left">
            BUMDES TUNAS MANDIRI DESA NEGARA RATU<br />
            JURNAL PENYESUAIAN<br /></h3>
        <asp:Table ID="Table1" runat="server">
            <asp:TableRow>
                <asp:TableCell style="width:120px">Periode<div style="font-size:11px">(mm/dd/yyyy)</div></asp:TableCell>
                <asp:TableCell>:</asp:TableCell>
                <asp:TableCell><asp:TextBox ID="TextBoxPeriode1" TextMode="Date" runat="server" CssClass="textboxstyle" BorderColor="#1da1f2" BorderStyle="Solid" BorderWidth="1px" style="color:#666" ForeColor="#ccc" Width="150px" Height="34px"></asp:TextBox></asp:TableCell>
                <asp:TableCell>s.d</asp:TableCell>
                <asp:TableCell><asp:TextBox ID="TextBoxPeriode2" TextMode="Date" runat="server" CssClass="textboxstyle" BorderColor="#1da1f2" BorderStyle="Solid" BorderWidth="1px" style="color:#666" ForeColor="#ccc" Width="150px" Height="34px"></asp:TextBox></asp:TableCell>
                <asp:TableCell><asp:Button ID="lihadData" runat="server" Text="Lihat" style="background-color:#1DA1F2; border-radius: 16px" ForeColor="white" BorderStyle="none" Height="34px" Width="100px" OnClick="lihatData_Click" /></asp:TableCell>
            </asp:TableRow>
        </asp:Table>
        <br />
        <div style="text-align:center"><asp:Label ID="lblperiode" runat="server"></asp:Label></div>
        <asp:GridView ID="GridView1" runat="server" CssClass="cssgridview" AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="false" ShowFooter="true" OnRowDataBound="GridView1_RowDataBound" >
            <Columns>
                <asp:BoundField HeaderText="KODE" DataField="rekeningID" HeaderStyle-Width="15%" HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="15%" />
                <asp:BoundField HeaderText="REKENING" DataField="namaRekening" HeaderStyle-Width="15%" HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="55%" />
                <asp:BoundField HeaderText="DEBET" DataField="debet" HeaderStyle-Width="15%" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n0}" FooterStyle-HorizontalAlign="Right" ItemStyle-Width="15%" />
                <asp:BoundField HeaderText="KREDIT" DataField="kredit" HeaderStyle-Width="15%" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n0}" FooterStyle-HorizontalAlign="Right" ItemStyle-Width="15%" />
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
