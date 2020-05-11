<%@ Page Title="Contact" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Contact.aspx.cs" Inherits="VisualMannschaftsverwaltung.Contact" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h3>Anwendungsverantwortlicher</h3>
    <address>
        Marvin Zichner<br />
        53783 Eitorf<br />
    </address>

    <h3>Technische Informationen</h3>
    <b>Datenbankverbindung</b><br />
    <b>Server:</b> <span runat="server" id="infoServer"></span><br />
    <b>Datenbank:</b> <span runat="server" id="infoDatabase"></span><br />
    <b>Benutzername:</b> <span runat="server" id="infoUsername"></span><br />
    <b>Kennwort:</b> <span runat="server" id="infoPassword">*********</span><br />
    <b>Verbindungsstatus:</b> <span runat="server" id="infoStatus"></span><br />
    <b>Aktuelles Datenbankschema:</b> <span runat="server" id="infoScheme"></span><br />

</asp:Content>
