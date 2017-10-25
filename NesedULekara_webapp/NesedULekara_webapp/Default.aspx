<%@ Page Title="Domáca stránka" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="NesedULekara_webapp._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <h2>Neseď U Lekára</h2> 

        <p class="lead">&nbsp;</p>
        <p class="lead">Prihlásenie pre lekára/administrátora portálu</p>
        &nbsp;
        <asp:TextBox ID="loginTextBox" runat="server"></asp:TextBox>
    &nbsp; <asp:TextBox ID="passwordTextBox" runat="server" TextMode="Password"></asp:TextBox>
    &nbsp; <asp:Button ID="signInButton" runat="server" class="btn btn-default" Text="Button" />
    </div>

</asp:Content>
