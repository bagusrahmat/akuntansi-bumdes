<%@ Page Title="" Language="C#" MasterPageFile="~/hlm/Site1.Master" AutoEventWireup="true" CodeBehind="home.aspx.cs" Inherits="bumdesAPP.hlm.home" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="position:relative; z-index:10">
        <img src="../img/homeimg.JPG" style="width:100%; padding-top:56px" />
        <div style="position: absolute; top: 90px; left: 110px; padding-left: 20px; padding-top: 20px; line-height:1.4">
            <a style="color:#333; font-size: 42px; font-weight: 500">BUMDes (Badan Usaha Milik<br />Desa) Tunas Mandiri</a>
            <a style="color:#666; font-size: 21px;"><br /><br />Desa Negara Ratu, Kecamatan Natar, Kabupaten<br />Lampung Selatan, Lampung.</a>
        </div>
    </div>
    <div class="body" style="font-family:Roboto; font-size:18px; line-height:1.6;padding-top:20px;padding-bottom: 450px">
        <p style="color:#666; font-size:28px; font-weight:500">Tentang</p>
        <p style="font-size:21px; color: #666">BUMDes (Badan Usaha Milik Desa) Tunas Mandiri merupakan badan usaha yang didirikan oleh pemerintah Desa Negara Ratu pada tahun 2016.
            BUMDes Tunas Mandiri terletak di Desa Negara Ratu, Kecamatan Natar, Kabupaten Lampung Selatan.
            BUMDes Tunas Mandiri bergerak di bidang penjualan, yang saat ini sudah memiliki dua jenis usaha yaitu penjualan ikan lele dan es kopyor.</p><br /><br />
        <p style="width:40%; float:left">
            <asp:Image ID="Image1" runat="server" Width="100%" Height="100%" ImageUrl="~/img/IMG_0510.JPG" /></p>
        <p style="font-size:21px; color: #666; float:right; width:54%; height:280px; padding-top:40px">BUMDes (Badan Usaha Milik Desa) Tunas Mandiri merupakan badan usaha yang didirikan oleh pemerintah Desa Negara Ratu pada tahun 2016.
            BUMDes Tunas Mandiri terletak di Desa Negara Ratu, Kecamatan Natar, Kabupaten Lampung Selatan.</p>
        <br /><br />
        <div style="font-size:21px; color: #666; float:left; width:47%; text-align:center">
            <p style="color:#666; font-size:28px;">Kenapa Perlu BUMDes?</p>
            Sebagai penyediaan pelayanan publik, mendorong pembangunan ekonomi desa,
            peningkatan kapasitas pemerintah desa menuju kemandirian.
        </div>
        <div style="font-size:21px; color: #666; float:left; width:47%; padding-left:6%; text-align:center">
            <p style="color:#666; font-size:28px; ">Peranan BUMDes?</p>
            Sebagai instrumen penguatan otonomi desa, sebagai instrumen kesejahteraan masyarakat.
        </div>
    </div>
</asp:Content>
