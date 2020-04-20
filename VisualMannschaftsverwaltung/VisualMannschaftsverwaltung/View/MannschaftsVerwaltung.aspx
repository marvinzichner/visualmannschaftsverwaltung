<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MannschaftsVerwaltung.aspx.cs" Inherits="VisualMannschaftsverwaltung.View.MannschaftsVerwaltung" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <asp:DropDownList ID="teamsList" runat="server" CssClass="masterselect">
    </asp:DropDownList>
    <asp:Button ID="teamsListSelect" runat="server" OnClick="teamSelected" Text="Mannschaft öffnen" />
    <asp:Button ID="teamsDelete" runat="server" OnClick="removeTeam" Text="Mannschaft löschen" />
    
    <div style="float: right; margin-top: 15px;">
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
        <h3 runat="server" ID="teamName">Mannschaftsname</h3>
     
        Hinzufügen: <asp:DropDownList ID="personList" runat="server" CssClass="masterselect"></asp:DropDownList>
        <asp:Button ID="personListButton" runat="server" OnClick="addPersonToMannschaft" Text="Hinzufügen" />
        <br />
        Löschen: <asp:DropDownList ID="personListDelete" runat="server" CssClass="masterselect"></asp:DropDownList>
        <asp:Button ID="personListDeleteButton" runat="server" OnClick="removePersonFromMannschaft" Text="Entfernen" />

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
        .member {
            width: calc(100% / 3  - 40px);
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
