<%@ Page Title="Portál doktora" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DoctorView.aspx.cs" Inherits="NesedULekara_webapp.DoctorView" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        lekar
        <asp:Table ID="Table1" runat="server"></asp:Table>

        <asp:GridView ID="GridView1" runat="server"></asp:GridView> <br>
        <br>
        <asp:Button ID="logout2Button" runat="server" Text="Odhlásiť sa" OnClick="logout2Button_Click" />
    </div>

</asp:Content>
