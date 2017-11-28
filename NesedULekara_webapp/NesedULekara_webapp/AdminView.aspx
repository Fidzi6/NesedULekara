<%@ Page Title="Admin" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AdminView.aspx.cs" Inherits="NesedULekara_webapp.AdminView" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <h3>Registrácia nového lekára</h3> 
        <div></div>
        <asp:Label ID="Label1" runat="server" Text="Meno: "></asp:Label><asp:TextBox ID="doctorNameTxb" runat="server"></asp:TextBox><%--<asp:RequiredFieldValidator ID="rq1" runat="server" ControlToValidate="doctorNameTxb" ErrorMessage="Vložte hodnotu!" ForeColor="Red"></asp:RequiredFieldValidator>--%> <br>
        <asp:Label ID="Label3" runat="server" Text="Priezvisko: "></asp:Label><asp:TextBox ID="doctorSurnameTxb" runat="server"></asp:TextBox><%--<asp:RequiredFieldValidator ID="rq2" runat="server" ControlToValidate="doctorSurnameTxb" ErrorMessage="Vložte hodnotu!" ForeColor="Red"></asp:RequiredFieldValidator>--%> <br>
        <asp:Label ID="Label2" runat="server" Text="Email: "></asp:Label><asp:TextBox ID="doctorEmailTxb" runat="server" TextMode="Email"></asp:TextBox><%--<asp:RequiredFieldValidator ID="rq3" runat="server" ControlToValidate="doctorEmailTxb" ErrorMessage="Vložte hodnotu!" ForeColor="Red"></asp:RequiredFieldValidator>--%> <br>
        <asp:Label ID="Label9" runat="server" Text="Telefonický kontakt: "></asp:Label><asp:TextBox ID="doctorTelephoneTxb" runat="server" TextMode="Phone"></asp:TextBox><%--<asp:RequiredFieldValidator ID="rq4" runat="server" ControlToValidate="doctorTelephoneTxb" ErrorMessage="Vložte hodnotu!" ForeColor="Red"></asp:RequiredFieldValidator>--%> <br>
        <asp:Label ID="Label4" runat="server" Text="Zaradenie: "></asp:Label><asp:DropDownList ID="DropDownList1" runat="server" OnInit="DropDownList1_Init" OnTextChanged="DropDownList1_TextChanged" ></asp:DropDownList>&nbsp;&nbsp; <asp:Label ID="Label5" runat="server" Text="Iné: "></asp:Label><asp:TextBox ID="doctorPositionTxb" runat="server"></asp:TextBox> <br>
        <asp:Label ID="Label6" runat="server" Text="Mesto: "></asp:Label><asp:TextBox ID="doctorCityTxb" runat="server"></asp:TextBox><%--<asp:RequiredFieldValidator ID="rq5" runat="server" ControlToValidate="doctorCityTxb" ErrorMessage="Vložte hodnotu!" ForeColor="Red"></asp:RequiredFieldValidator>--%> <br>
        <asp:Label ID="Label7" runat="server" Text="Adresa: "></asp:Label><asp:TextBox ID="doctorAddressTxb" runat="server"></asp:TextBox><%--<asp:RequiredFieldValidator ID="rq6" runat="server" ControlToValidate="doctorAddressTxb" ErrorMessage="Vložte hodnotu!" ForeColor="Red"></asp:RequiredFieldValidator>--%> &nbsp;&nbsp;&nbsp; <asp:Button ID="Button1" runat="server" Text="Vygeneruj GPS" OnClick="Button1_Click" />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <asp:TextBox ID="gpsResultTxb" runat="server" Width="220px"></asp:TextBox>
        <br />
        <asp:TextBox ID="GPSErrorTxb" Text="Chyby pri generovaní GPS ..." runat="server" BorderStyle="Solid" TextMode="MultiLine" Height="76px" Width="432px"></asp:TextBox>
        <br>
        <hr />
        <h4>Úradné hodiny:</h4>
        <asp:Label ID="Label10" runat="server" Text="Začiatok služby: "></asp:Label><asp:TextBox ID="doctorDayStartTxb" runat="server" TextMode="Time" Text="7:00"></asp:TextBox><%--<asp:RequiredFieldValidator ID="rq7" runat="server" ControlToValidate="doctorDayStartTxb" ErrorMessage="Vložte hodnotu!" ForeColor="Red"></asp:RequiredFieldValidator>--%> <br>
        <asp:Label ID="Label11" runat="server" Text="Koniec služby: "></asp:Label><asp:TextBox ID="doctorDayEndTxb" runat="server" TextMode="Time" Text="16:00"></asp:TextBox> <br>
        <asp:Label ID="Label12" runat="server" Text="Čas na jedného pacienta: "></asp:Label><asp:TextBox ID="doctorPacientTimeTxb" runat="server" Text="30"></asp:TextBox>&nbsp;<asp:Label ID="Label17" runat="server" Text=" minút"></asp:Label><%--<asp:RequiredFieldValidator ID="rq9" runat="server" ControlToValidate="doctorPacientTimeTxb" ErrorMessage="Vložte hodnotu!" ForeColor="Red"></asp:RequiredFieldValidator>--%> <br>
        <asp:Label ID="Label13" runat="server" Text="Pohotovosť od "></asp:Label><asp:TextBox ID="doctorEmergencyStartTxb" runat="server" TextMode="Time" Text="7:00"></asp:TextBox>
        &nbsp;
        <asp:Label ID="Label14" runat="server" Text="   do "></asp:Label>&nbsp;<asp:TextBox ID="doctorEmergencyEndTxb" runat="server" TextMode="Time" Text="8:30"></asp:TextBox> <br>
        <asp:Label ID="Label15" runat="server" Text="Obedná prestávka od "></asp:Label><asp:TextBox ID="doctorLunchStartTxb" runat="server" TextMode="Time" Text="12:00"></asp:TextBox>
        &nbsp;
        <asp:Label ID="Label16" runat="server" Text="do"></asp:Label>&nbsp; <asp:TextBox ID="doctorLunchEndTxb" runat="server" TextMode="Time" Text="12:30"></asp:TextBox><%--<asp:RequiredFieldValidator ID="rq10" runat="server" ControlToValidate="doctorLunchStartTxb" ErrorMessage="Vložte hodnotu!" ForeColor="Red"></asp:RequiredFieldValidator><asp:RequiredFieldValidator ID="rq11" runat="server" ControlToValidate="doctorLunchEndTxb" ErrorMessage="Vložte hodnotu!" ForeColor="Red"></asp:RequiredFieldValidator>--%><br>
         <br>
        <hr />
        <asp:Button ID="doctorRegister" runat="server" Text="Registrovať nového lekára" OnClick="doctorRegister_Click" /> <br>
        <br>
        <asp:Label ID="Label8" runat="server" Text="Status registrácie: "></asp:Label> <br>
        <asp:TextBox ID="doctorRegistrationStatusTxb" runat="server" BorderStyle="Solid" TextMode="MultiLine" Height="179px" Width="598px"></asp:TextBox>
        <br>
        <hr />
        <asp:Button ID="logoutButton" runat="server" Text="Odhlásiť sa" OnClick="logoutButton_Click" />
    </div>

</asp:Content>
