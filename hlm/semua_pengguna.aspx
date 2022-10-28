<%@ Page Title="" Language="C#" MasterPageFile="~/hlm/Site1.Master" AutoEventWireup="true" CodeBehind="semua_pengguna.aspx.cs" Inherits="bumdesAPP.hlm.semua_pengguna" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="body">
        <h3 style="font-weight:400; text-align:left">
            BUMDES TUNAS MANDIRI DESA NEGARA RATU<br />
            KELOLA DATA PENGGUNA SISTEM<br /></h3>
        <table>
            <tr>
                <td style="width: 130px;"><asp:Button ID="ButtonTambah" runat="server" Text="Tambah" style="border-radius: 16px" BorderStyle="Solid" Height="34px" Width="100px" ForeColor="White" BorderWidth="1.5px" BorderColor="#1DA1F2" BackColor="#1DA1F2" OnClick="tambah_Click" /></td>
                <td><asp:Button Visible="false" ID="ButtonBatal" runat="server" Text="Batal" style="border-radius: 16px" BorderStyle="None" BackColor="#CC3300" ForeColor="white" Height="34px" Width="100px" OnClick="batal_Click" /></td>
                <td><asp:Label ID="lblbantuUsername" runat="server" Text=""></asp:Label></td>
            </tr>
        </table>
        <asp:Table ID="Table1" style="padding:17px; border: solid 1px #ccc; border-radius:10px" Visible="false" runat="server">
            <asp:TableRow>
                <asp:TableCell style="text-align:right; padding-right:20px">Nama Lengkap<a style="color:red">*</a></asp:TableCell>
                <asp:TableCell Height="40px"><asp:TextBox ID="namaTextbox" MaxLength="50" runat="server" CssClass="textboxstyle" BorderColor="#1da1f2" BorderStyle="Solid" BorderWidth="1px" Width="300px" Height="34px"></asp:TextBox></asp:TableCell>
            </asp:TableRow>
            
            <asp:TableRow>
                <asp:TableCell style="text-align:right; padding-right:20px">Username<a style="color:red">*</a></asp:TableCell>
                <asp:TableCell Height="40px"><asp:TextBox ID="usernameTextBox" MaxLength="20" runat="server" CssClass="textboxstyle" BorderColor="#1da1f2" BorderStyle="Solid" BorderWidth="1px" Width="300px" Height="34px"></asp:TextBox></asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell style="text-align:right; padding-right:20px">Level<a style="color:red">*</a></asp:TableCell>
                <asp:TableCell Height="40px">
                    <asp:DropDownList ID="DropDownLevelUser" runat="server" CssClass="textboxstyle" BorderColor="#1DA1F2" BorderStyle="Solid" 
                    BorderWidth="1px" style="color:#666" ForeColor="#000CCC" Width="314px" Height="38px">
                        <asp:ListItem Text="-- Pilih Level Pengguna --"></asp:ListItem>
                        <asp:ListItem Text="Administrator"></asp:ListItem>
                        <asp:ListItem Text="User"></asp:ListItem>
                    </asp:DropDownList>
                </asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell style="text-align:right; padding-right:20px">Password<a style="color:red">*</a></asp:TableCell>
                <asp:TableCell Height="40px"><asp:TextBox ID="passwordTextBox" MaxLength="10" runat="server" CssClass="textboxstyle" BorderColor="#1da1f2" BorderStyle="Solid" BorderWidth="1px" Width="300px" Height="34px"></asp:TextBox></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell></asp:TableCell>
                <asp:TableCell HorizontalAlign="Right" Height="40px"><asp:Button ID="simpanDataUser" runat="server" OnClientClick="return alert('Data Pengguna Tersimpan.')" Text="Simpan" style="border-radius: 16px" BackColor="#1DA1F2" BorderStyle="none" ForeColor="white" Height="34px" Width="100px" OnClick="simpanDataUser_Click" /></asp:TableCell>
            </asp:TableRow>

        </asp:Table>
        
        <br />
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" CssClass="cssgridview" AlternatingRowStyle-CssClass="alt">
            <Columns>
                <asp:BoundField HeaderText="NO." DataField="nom" HeaderStyle-Width="5%" HeaderStyle-HorizontalAlign="Left" />
                <asp:BoundField HeaderText="NAMA LENGKAP" DataField="namaLengkap" HeaderStyle-Width="40%" HeaderStyle-HorizontalAlign="Left" />
                <asp:BoundField HeaderText="USERNAME" DataField="username" HeaderStyle-Width="20%" HeaderStyle-HorizontalAlign="Left" />
                <asp:BoundField HeaderText="LEVEL" DataField="levelUser" HeaderStyle-Width="20%" HeaderStyle-HorizontalAlign="Left" />
                <asp:TemplateField HeaderText="OPSI" HeaderStyle-Width="15%" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" >
                    <ItemTemplate>
                        <asp:LinkButton ID="linkEdit" Text="Edit" runat="server" CommandArgument='<%# Eval("username") %>' OnClick="linkEdit_Click"></asp:LinkButton>
                        <asp:LinkButton ID="linkHapus" Text="Hapus" runat="server" CommandArgument='<%# Eval("username") %>' OnClientClick="return confirm('Apakah ingin menghapus data?')" OnClick="linkHapus_Click"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
