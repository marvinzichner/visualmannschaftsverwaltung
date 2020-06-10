<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TurnierVerwaltung.aspx.cs" Inherits="VisualMannschaftsverwaltung.View.TurnierVerwaltung" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h3>Turnierverwaltung</h3>
    <table runat="server" id="storedTurniere" class="table"></table>

    <style>
        .table {
            width: 100%;
            padding: 10px;
        }
        .tablecell {
           
        }
        .cellHead {
            font-weight: bold;
            background-color: #f2f2f2;
        }
        .cellBody {
            
        }
        .cellBodyHover:hover {
            background-color: #f2f2f2;
        }
        .cellReadOnly {
            font-size: 9pt;
            color: #808080;
        }
        .cellEnd {
            border-bottom: 2px solid #cccccc;
        }
    </style>

</asp:Content>
