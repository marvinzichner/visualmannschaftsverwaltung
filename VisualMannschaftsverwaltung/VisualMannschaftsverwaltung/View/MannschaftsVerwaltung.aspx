<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MannschaftsVerwaltung.aspx.cs" Inherits="VisualMannschaftsverwaltung.View.MannschaftsVerwaltung" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="config" id="configMismatch" runat="server" visible="false">
        Die Datenbank ist derzeit nicht erreichbar. Möglicherweise sind die Verbindungsdaten nicht korrekt.  
    </div>
    <style>
        .config {
            margin: 20px;
            padding: 20px;
            background-color: #ffb3b3;
        }
    </style>

    <asp:DropDownList ID="teamsList" runat="server" CssClass="masterselect" AutoPostBack="True" OnTextChanged="teamSelected">
    </asp:DropDownList>
    <asp:Button ID="teamsListSelect" runat="server" OnClick="teamSelected" Text="Öffnen" />
    <asp:Button ID="teamsDelete" runat="server" OnClick="removeTeam" Text="Löschen" />
    <asp:Button ID="downloadButton" runat="server" OnClick="generateXML" Text="XML Export" />
    
    <div style="float: right; margin-top: 15px;" id="createNewTeam" runat="server">
        Neue Mannschaft anlegen:
        <asp:TextBox ID="newTeamnameBox" runat="server"></asp:TextBox>
        <asp:DropDownList ID="newTeamtype" runat="server">
            <asp:ListItem Value="FUSSBALL" Text="Fußball"></asp:ListItem>
            <asp:ListItem Value="HANDBALL" Text="Handball"></asp:ListItem>
            <asp:ListItem Value="TENNIS" Text="Tennis"></asp:ListItem>
        </asp:DropDownList>
        <asp:Button ID="newTeamBtn" runat="server" OnClick="createTeam" Text="Anlegen" />
    </div>

    <hr />

    <div id="contentContainer" runat="server" visible="false">
        <asp:Button ID="teamsEdit" runat="server" OnClick="changeTeam" Text="Mannschaft bearbeiten" CssClass="floater" />
        <h3 runat="server" ID="teamName">Mannschaftsname</h3>
        
        <hr />
        Hinzufügen: <asp:DropDownList ID="personList" runat="server" CssClass="masterselect"></asp:DropDownList>
        <asp:Button ID="personListButton" runat="server" OnClick="addPersonToMannschaft" Text="Hinzufügen" />
        &emsp;&emsp;&emsp;
        Löschen: <asp:DropDownList ID="personListDelete" runat="server" CssClass="masterselect"></asp:DropDownList>
        <asp:Button ID="personListDeleteButton" runat="server" OnClick="removePersonFromMannschaft" Text="Entfernen" />
        <br />
        Sortierung: 
        <asp:DropDownList ID="dropDownSorting" CssClass="masterselect" runat="server">
            <asp:ListItem Value="UNSORTED">Bitte wählen</asp:ListItem>
            <asp:ListItem Value="NAME_ASC">Vorname (A-Z)</asp:ListItem>
            <asp:ListItem Value="ERFOLG_ASC">Erfolg (gut-schlecht, gruppiert)</asp:ListItem>
            <asp:ListItem Value="BIRTHDATE_ASC">Geburtsdatum (alt-jung)</asp:ListItem>
        </asp:DropDownList>
        <asp:Button ID="confirmSortingButton" runat="server" OnClick="appendFilter" Text="Sortierung anwenden" />
        <hr />

        <br /><br />
        <div class="clear" ID="membersListContainer" runat="server">
            <div class="member clear">
                <div class="rolename">Trainer</div>
                Harald Petersen
                <div class="rolename">33 SPIELE | 4 GEWONNEN</div>
            </div>
        </div>
    </div>

    <style>
        .masterselect {
            margin: 15px;
            padding: 5px;
        }
        .floater {
            float: left;
            margin-right: 20px;
        }
        .member {
            width: calc(100% / 3 - 40px);
            padding: 20px;
            border: 1px solid #e6e6e6;
            margin-left: 20px;
            margin-bottom: 20px;
            float: left;
            font-size: 13pt;
            overflow: hidden;
        }
        .rolename {
            font-size: 9pt;
            color: gray;
            text-transform: uppercase;
        }
        .personname {
            font-size: 13pt;
            color: black;
            text-transform: uppercase;
        }
        .member:hover {
            -webkit-box-shadow: 0px 0px 5px 0px rgba(107,107,107,1);
            -moz-box-shadow: 0px 0px 5px 0px rgba(107,107,107,1);
            box-shadow: 0px 0px 5px 0px rgba(107,107,107,1);
        }
        .clear:after {
            clear: both;
            content: "";
            display: table;
        }
    </style>
</asp:Content>
