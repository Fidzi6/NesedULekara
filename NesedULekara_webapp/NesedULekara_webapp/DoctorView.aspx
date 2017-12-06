<%@ Page Title="Portál doktora" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DoctorView.aspx.cs" Inherits="NesedULekara_webapp.DoctorView" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        lekar
        <br>
        <asp:PlaceHolder ID="PlaceHolder1" runat="server" />
        <br>
        <asp:Button ID="logout2Button" runat="server" Text="Odhlásiť sa" OnClick="logout2Button_Click" />
    </div>

</asp:Content>
