<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PersonenVerwaltung.aspx.cs" Inherits="VisualMannschaftsverwaltung.View.PersonenVerwaltung" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div style="
        width: 100%; 
        padding: 30px; 
        background-color: #e6e6e6; 
        color: gray; 
        height: 100px; 
        line-height: 100px; 
        font-size: 15pt;
        margin-bottom: 30px;
    ">Personen Verwaltung</div>

    <h4>1. Wählen Sie den zu erstellenden Personentyp aus:</h4>
    <asp:RadioButtonList ID="PersonSelectionType" runat="server">
        <asp:ListItem Value="FussballSpieler">Fussballspieler</asp:ListItem>
        <asp:ListItem Value="HandballSpieler">Handballspieler</asp:ListItem>
        <asp:ListItem Value="TennisSpieler">Tennisspieler</asp:ListItem>
        <asp:ListItem>Physiotherapeuth</asp:ListItem>
        <asp:ListItem>Trainer</asp:ListItem>
    </asp:RadioButtonList>

    <br />
    <asp:Button ID="buttonConfirmSelection" runat="server" Text="Auswahl bestätigen" OnClick="confirmPersonSelection" />

    <hr />
    <h4>2. Geben Sie Detailinformationen zu dieser Person an:</h4>
    
    <div class="listSpacer">
        <div class="listLabel">Vorname</div>
        <asp:TextBox ID="fieldVorname" CssClass="listField" runat="server"></asp:TextBox>
    </div>
    <div class="listSpacer">
        <div class="listLabel">Nachname</div>
        <asp:TextBox ID="fieldNachname" CssClass="listField" runat="server"></asp:TextBox>
    </div>
    <div class="listSpacer">
        <div class="listLabel">Geburtsdatum</div>
        <asp:TextBox ID="fieldBirthdate" CssClass="listField" runat="server"></asp:TextBox>
    </div>

    <style>
        .listSpacer {
            height: 30px;
            line-height: 30px;
            width: 100%;
            margin-bottom: 5px;
        }
        .listLabel {
            float: left;
            width: 200px;
        }
        .listField {
            float: left;
            height: 30px;
            width: 400px;
        }
    </style>

</asp:Content>