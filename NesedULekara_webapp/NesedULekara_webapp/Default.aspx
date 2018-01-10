<%@ Page Title="Domáca stránka" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="NesedULekara_webapp._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <h2>Neseď U Lekára</h2> 

        <p class="lead">&nbsp;</p>
        <p class="lead">Prihlásenie pre lekára/administrátora portálu</p>

        <asp:RequiredFieldValidator runat="server" ControlToValidate="loginTextBox" ErrorMessage="Nezadali ste login!" ForeColor="Red"> </asp:RequiredFieldValidator>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:RequiredFieldValidator runat="server" ControlToValidate="passwordTextBox" ErrorMessage="Nezadali ste heslo!" ForeColor="Red"></asp:RequiredFieldValidator> <br>
        <asp:TextBox ID="loginTextBox" runat="server"></asp:TextBox> &nbsp;&nbsp; <asp:TextBox ID="passwordTextBox" runat="server" TextMode="Password"></asp:TextBox> <br>
        
    &nbsp; 
    &nbsp; <asp:Button ID="signInButton" runat="server" class="btn btn-default" Text="Prihlásiť" OnClick="signInButton_Click" /> <br>
        <asp:Label ID="loginError" runat="server" Text="" ForeColor="Red"></asp:Label> <br>
        &nbsp;&nbsp;
    </div>

</asp:Content>
