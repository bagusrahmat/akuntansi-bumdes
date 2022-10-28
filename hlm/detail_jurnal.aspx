<%@ Page Title="" Language="C#" MasterPageFile="~/hlm/Site1.Master" AutoEventWireup="true" CodeBehind="detail_jurnal.aspx.cs" Inherits="bumdesAPP.hlm.detail_jurnal" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="body">
        <h3 style="font-weight:400; text-align:left">
            BUMDES TUNAS MANDIRI DESA NEGARA RATU<br />
            JURNAL UMUM<br /></h3>
        <asp:Table ID="Table2" runat="server">
            <asp:TableRow>
                <asp:TableCell style="width:120px">Periode<div style="font-size:11px">(mm/dd/yyyy)</div></asp:TableCell>
                <asp:TableCell>:</asp:TableCell>
                <asp:TableCell><asp:TextBox ID="TextBoxPeriode1" TextMode="Date" runat="server" CssClass="textboxstyle" BorderColor="#1da1f2" BorderStyle="Solid" BorderWidth="1px" style="color:#666" ForeColor="#ccc" Width="150px" Height="34px"></asp:TextBox></asp:TableCell>
                <asp:TableCell>s.d</asp:TableCell>
                <asp:TableCell><asp:TextBox ID="TextBoxPeriode2" TextMode="Date" runat="server" CssClass="textboxstyle" BorderColor="#1da1f2" BorderStyle="Solid" BorderWidth="1px" style="color:#666" ForeColor="#ccc" Width="150px" Height="34px"></asp:TextBox></asp:TableCell>
                <asp:TableCell><asp:Button ID="lihatDetailJurnalPeriode" runat="server" Text="Lihat" style="background-color:#1DA1F2; border-radius: 16px" ForeColor="white" BorderStyle="none" Height="34px" Width="100px" OnClick="lihatDetailJurnalPeriode_Click" /></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>
                    <asp:LinkButton ID="LinkButton1" Font-Size="12px" OnClick="LinkButton1_Click" runat="server">LinkButton</asp:LinkButton></asp:TableCell>
            </asp:TableRow>
        </asp:Table>
        <%--<div style="padding-left: 90%"><asp:Button ID="ButtonPrint" runat="server" Text="Print PDF" style="border-radius: 6px" BorderStyle="Solid" Height="38px" Width="100px" BorderWidth="1.5px" ForeColor="#1DA1F2" BackColor="White" BorderColor="#1DA1F2" OnClick="ButtonPrint_Click" /></div>--%>
        <br />
        <div style="text-align:center"><asp:Label ID="lblperiode" runat="server"></asp:Label></div>
        <asp:GridView ID="GridView1" runat="server" CssClass="cssgridview" AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="false" OnPageIndexChanging="GridView1_PageIndexChanging" AllowPaging="True" PagerSettings-Visible="True" PageSize="15" >
            <Columns>
                <asp:BoundField HeaderText="NO.JURNAL" DataField="noJurnal" HeaderStyle-Width="15%" HeaderStyle-HorizontalAlign="Left" />
                <asp:BoundField HeaderText="TANGGAL" DataField="tanggal" HeaderStyle-Width="13%" HeaderStyle-HorizontalAlign="Left" DataFormatString="{0:dd-MM-yy}" />
                <asp:BoundField HeaderText="REKENING" DataField="namaRekening" HeaderStyle-Width="15%" HeaderStyle-HorizontalAlign="Left" />
                <asp:BoundField HeaderText="DEBET" DataField="debet" HeaderStyle-Width="15%" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n0}" FooterStyle-HorizontalAlign="Right" />
                <asp:BoundField HeaderText="KREDIT" DataField="kredit" HeaderStyle-Width="15%" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n0}" FooterStyle-HorizontalAlign="Right" />
                <asp:BoundField HeaderText="DESKRIPSI" DataField="deskripsi" HeaderStyle-Width="27%" HeaderStyle-HorizontalAlign="Left" />
            </Columns>
        </asp:GridView>
        <br />
        <asp:Label ID="LabelHalaman" runat="server" Text=""></asp:Label>
    </div>
</asp:Content>
