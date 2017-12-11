<%@ Page Title="Portál doktora" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DoctorView.aspx.cs" Inherits="NesedULekara_webapp.DoctorView" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        Údaje o lekárovi:
        <br>
        <asp:GridView ID="GridView1" runat="server" DataSourceID="SqlDataSource1" />
        <asp:SqlDataSource ID="SqlDataSource1" runat="server"
            ConnectionString="<%$ ConnectionStrings:ConnectionString %>" /> <%--SelectCommand="SELECT * FROM dbo.[APIS08_evidencia_bicyklov]"--%>
        <br>
        Harmonogram:
        <br>
        <asp:PlaceHolder ID="PlaceHolder1" runat="server" />
        <br>
        <asp:Button ID="logout2Button" runat="server" Text="Odhlásiť sa" OnClick="logout2Button_Click" />
    </div>

</asp:Content>
