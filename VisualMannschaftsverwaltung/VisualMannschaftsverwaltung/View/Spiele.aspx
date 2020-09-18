<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Spiele.aspx.cs" Inherits="VisualMannschaftsverwaltung.View.Spiele" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div id="RuntimeExceptionWrapper" runat="server"></div>
    <h3 id="spielTitle" runat="server">Bitte wählen Sie ein Turnier aus</h3>
    Lade alle Spiele aus dem Turnier:
    <asp:DropDownList ID="turniereDropdown" runat="server"></asp:DropDownList>
    <asp:Button ID="turnierLoadButton" runat="server" OnClick="selectTurnier" Text="Anwenden"/><br /><br />

    <div id="createNewGame" runat="server" visible="false">
        <h3>Neues Spiel erstellen</h3>
        
        Mannschaft 
        <asp:DropDownList ID="dropdownTeamA" runat="server"></asp:DropDownList> 
        gegen 
        <asp:DropDownList ID="dropdownTeamb" runat="server"></asp:DropDownList><br />
        <br />
        Spieltag 
        <asp:TextBox ID="spieltag" runat="server"></asp:TextBox>
        
        <asp:Button ID="creationButton" runat="server" OnClick="createNewTurnier" Text="Erstellen"/>
    </div>

    <div id="previewHider" runat="server">
        <asp:Button ID="addNewTurnier" runat="server" OnClick="showCreationMode" AutoPostBack="true" Text="Neues Spiel erstellen"/>
        <asp:Button ID="generateTurniere" runat="server" OnClick="generateKnockoutSpiele" AutoPostBack="true" Text="Knockout: Alle erforderlichen Spiele generieren"/>
        <asp:Button ID="randomResults" runat="server" OnClick="generateRandomResults" AutoPostBack="true" Text="Zufällige Ergebnisse generieren"/>
        <br />
        <br />
        <h3>Tabelle</h3>
        <table class="table" runat="server" id="presenterRank">
        </table>
        <br /><br />
        <h3>Ergebnisse der Spiele</h3>
        <asp:Button ID="editButton" runat="server" AutoPostBack="True" OnClick="editList" Text="Einträge bearbeiten"/>
        &emsp; <span style="color: cornflowerblue"><b>Information zum Löschen</b></span>
        Schreiben Sie während der Bearbeitung der Spiele ein "x" auf eine beliebeige Teilnehmerseite.<br /><br />
        <table class="table" runat="server" id="presenterTable">
       
        </table>
    </div>

    <style>
        .rotate {
            -webkit-transform: rotate(-90deg);
            -moz-transform: rotate(-90deg);
            -ms-transform: rotate(-90deg);
            -o-transform: rotate(-90deg);
            float: left;
        }
        .table {
            width: 100%;
            padding: 10px;
        }
        .section-turnier {
            padding: 10px;
            background-color: #e6e6e6;
            color: rgba(0, 0, 0, .8);
        }
        .cellHead {
            font-weight: bold;
            background-color: #f2f2f2;
        }
        .cellBodyHover:hover {
            background-color: #f2f2f2;
        }
        .cellReadOnly {
            font-size: 9pt;
            color: #808080;
        }
        .cellInteraction {
            font-size: 9pt;
            color: #0094ff;
            text-decoration: underline;
        }
        .cellEnd {
            border-bottom: 2px solid #cccccc;
        }


        .planned {
            padding: 5px;
            width: 100%;
            border-bottom:2px solid #e6e6e6;
            margin-bottom: 10px;
            text-align: center;
        }
        .planned:hover {
            background-color: #f2f2f2;
        }
        .text-center {
            text-align: center;
            font-size: 15pt;
            padding: 20px;
            background-color: #e6e6e6;
        }
        .tablemaster {
            width: 100%;
            border: none;
            text-transform: uppercase;
        }
        .text-centerized {
            text-align: center;
        }
        .tr-section {
            margin-top: 10px;
            cursor: pointer;
        }
        .tr-section:hover td {
            background-color: #f2f2f2;
        }
        .big {
            font-size: 30pt;
        }
        .td-name {
            border-bottom: 2px dashed #e6e6e6;
            padding: 5px;
        }
        .td-score {
            border: 2px solid #e6e6e6;
            padding: 5px;
            width: 40px;
            text-align: center;
        }
        .td-right {
            text-align: right;
        }
        .pointsizer {
            width: 60px;
            text-align: center;
        }
    </style>

</asp:Content>
