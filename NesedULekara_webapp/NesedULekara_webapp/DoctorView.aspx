<%@ Page Title="Portál doktora" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DoctorView.aspx.cs" Inherits="NesedULekara_webapp.DoctorView" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        lekar
        <asp:Table ID="Table1" runat="server"></asp:Table>

        <asp:GridView ID="GridView1" runat="server"></asp:GridView>
    </div>

</asp:Content>
