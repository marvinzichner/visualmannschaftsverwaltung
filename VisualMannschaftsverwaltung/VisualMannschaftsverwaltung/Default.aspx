<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="VisualMannschaftsverwaltung._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <br />
    <div class="clear" ID="membersListContainer" runat="server">
        <a runat="server" href="~/View/PersonenVerwaltung">
            <div class="member clear">
                <div class="rolename">QUICK LINK</div>
                Personen
            </div>
        </a>

        <a runat="server" href="~/View/MannschaftsVerwaltung">
            <div class="member clear">
                <div class="rolename">QUICK LINK</div>
                Mannschaften
            </div>
        </a>

         <a runat="server" href="~/View/TurnierVerwaltung">
            <div class="member clear">
                <div class="rolename">QUICK LINK</div>
                Turniere
            </div>
        </a>

        <a runat="server" href="~/View/Spiele">
            <div class="member clear">
                <div class="rolename">QUICK LINK</div>
                Spiele
            </div>
        </a>

        <a runat="server" href="~/Contact">
            <div class="member clear">
                <div class="rolename">QUICK LINK</div>
                Anwendungsinformationen
            </div>
        </a>
    </div>

    <style>
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
