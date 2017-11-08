<%@ Page Title="Admin" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AdminView.aspx.cs" Inherits="NesedULekara_webapp.AdminView" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <h3>Registrácia nového lekára</h3> 
        <div></div>
        <asp:Label ID="Label2" runat="server" Text="Titul: "></asp:Label><asp:TextBox ID="doctorTitleTxb" runat="server"></asp:TextBox> <br>
        <asp:Label ID="Label1" runat="server" Text="Meno: "></asp:Label><asp:TextBox ID="doctorNameTxb" runat="server"></asp:TextBox> <br>
        <asp:Label ID="Label3" runat="server" Text="Priezvisko: "></asp:Label><asp:TextBox ID="doctorSurnameTxb" runat="server"></asp:TextBox> <br>
        <asp:Label ID="Label4" runat="server" Text="Zaradenie: "></asp:Label><asp:DropDownList ID="DropDownList1" runat="server"></asp:DropDownList>&nbsp;&nbsp; <asp:Label ID="Label5" runat="server" Text="Iné: "></asp:Label><asp:TextBox ID="doctorPositionTxb" runat="server"></asp:TextBox> <br>
        <asp:Label ID="Label6" runat="server" Text="Mesto: "></asp:Label><asp:TextBox ID="doctorCityTxb" runat="server"></asp:TextBox> <br>
        <asp:Label ID="Label7" runat="server" Text="Adresa: "></asp:Label><asp:TextBox ID="doctorAddressTxb" runat="server"></asp:TextBox> 
        <asp:Label ID="addressError" runat="server" Text=""></asp:Label>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <asp:TextBox ID="lattitude" runat="server"></asp:TextBox><asp:TextBox ID="longitude" runat="server"></asp:TextBox>
        <br />
        <br>

        <asp:Button ID="Button1" runat="server" Text="Registrovať" OnClick="Button1_Click" /> <br>
        <asp:Label ID="Label8" runat="server" Text="Stav registrácie: "></asp:Label>&nbsp;&nbsp; <asp:Label ID="doctorRegistrationSuccessLbl" runat="server" Text="---"></asp:Label>
    </div>

</asp:Content>
