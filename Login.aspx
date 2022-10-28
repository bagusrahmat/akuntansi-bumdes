<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="bumdesAPP.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login | Sistem Informasi Akuntansi BUMDes Tunas Mandiri</title>
    <link rel="stylesheet" type="text/css" href="App_Themes/Default/StyleSheet1.css" />
</head>
<body style="background-color:#f6f6f6">
        <form id="form1" runat="server">
            <div class="formlogin">
                    <a style="font-size: 24px; font-family:Roboto;font-weight: 800">Selamat Datang!</a><br /><br />
                    <a style="font-size:14px; color: #666;">Sistem Informasi Keuangan<br />
                    BUMDes Tunas Mandiri</a><br /><br /><br />
                    <asp:Image ID="logo" runat="server" ImageUrl="~/img/logo_black.png" Height="60px" />
                    <br /><br /><br />
                    <asp:TextBox ID="username" placeholder="Username" runat="server" style="color: #666666; padding-left: 12px; border-radius: 3px; background-color: #fff; " BorderColor="#1da1f2" BorderStyle="Solid" BorderWidth="1px" ForeColor="#ccc" Width="80%" Height="34px" ></asp:TextBox> <br />
                    <br />
                    <asp:TextBox ID="password" placeholder="Password" TextMode="Password" runat="server" style="color: #666666;
                            padding-left: 12px;
                            border-radius: 3px;
                            background-color: #fff;"                            BorderColor="#1da1f2" BorderStyle="Solid" BorderWidth="1px" ForeColor="#ccc" Width="80%" Height="34px" ></asp:TextBox> <br />
                    <br />
                    <asp:Label ID="LoginStatus" runat="server" Font-Size="14px" ForeColor="#F21D1D"></asp:Label>
                    
                    <asp:Button ID="loginBtn" placeholder="Login" runat="server" Text="Login" style="border-radius: 19px" BorderStyle="None" ForeColor="White" Height="38px" Width="80%" OnClick="loginBtn_Click" />
                    <br />
                    <br />
                    <div>
                        <asp:ScriptManager ID="ScriptManager1" runat="server" />
                        <div>
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <asp:Timer ID="Timer1" runat="server" Interval="1000" OnTick="Timer1_Tick">
                                    </asp:Timer>
                                    <asp:Label ID="Label1" ForeColor="Red" runat="server"></asp:Label>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                    <a href="https://www.instagram.com/tunasmekarnegararatu/" target="_blank"><img src="../img/insta_black.png" style="height:30px; bottom: 16px; left:40%; position:absolute; padding-bottom:10px" /></a>
                </div>
            </div>
        </form>
    
</body>
</html>
