<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TurnierVerwaltung.aspx.cs" Inherits="VisualMannschaftsverwaltung.View.TurnierVerwaltung" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div id="sectionTurnierlist" runat="server">
        <h3>Turnierverwaltung</h3>
        <asp:Button ID="buttonConfirmSelection" runat="server" Text="Neues Turnier erstellen" OnClick="openCreationMode" />
        <asp:Button ID="buttonAddMapping" runat="server" Text="Mannschaft zu Turnier hinzufügen" OnClick="openMappingMode" />
        <br />
        <br />
        <table runat="server" id="storedTurniere" class="table"></table>
    </div>

    <div id="sectionCreateTurnier" runat="server">
        <h3>Neues Turnier erstellen</h3>
        <br />
        <div class="clear">
            <div class="listSpacer">
                <div class="listLabelFlow">Turniername</div>
                <asp:TextBox AutoCompleteType="Disabled" class="listField" ID="turnierNameField" CssClass="listField" runat="server"></asp:TextBox>
            </div>
            <div class="listSpacer">
                <div class="listLabelFlow">Tuniertyp</div>
                <asp:DropDownList ID="turnierTypeField" runat="server">
                    <asp:ListItem Value="FUSSBALL" Text="Fußball"></asp:ListItem>
                    <asp:ListItem Value="HANDBALL" Text="Handball"></asp:ListItem>
                    <asp:ListItem Value="TENNIS" Text="Tennis"></asp:ListItem>
                </asp:DropDownList>
            </div>
            <br />
            <br />
            <asp:Button ID="createNewTurnierButton" runat="server" Text="Erstellen" OnClick="createNewTurnier" />
            <asp:Button ID="cancelActionButton" runat="server" Text="Abbrechen" OnClick="cancelAction" />
        </div>
    </div>

    <div id="sectionMapping" runat="server">
        <h3>Mannschaft zu Turnier hinzufügen</h3>

        <div id="debug" runat="server"></div>

        Füge Mannschaft 
        <asp:DropDownList ID="dropdownMannschaften" runat="server"></asp:DropDownList>
        zum Turnier
        <asp:DropDownList ID="dropdownTurniere" runat="server"></asp:DropDownList>
        hinzu.

        <br />
        <br />
        <asp:Button ID="createNewMapping" runat="server" Text="Mannschaft hinzufügen" OnClick="doMapping" />
        <asp:Button ID="Button2" runat="server" Text="Abbrechen" OnClick="cancelAction" />
    </div>

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
        .cellInteraction {
            font-size: 9pt;
            color: #0094ff;
            text-decoration: underline;
        }
        .cellEnd {
            border-bottom: 2px solid #cccccc;
        }
         .listSpacer {
            height: 30px;
            line-height: 30px;
            width: 100%;
            margin-bottom: 5px;
        }
        .listSpacerDynamic div {
            height: 30px;
            line-height: 30px;
            width: 100%;
            margin-bottom: 5px;
        }
        .notMatching {
            color: red;
            font-weight: bold;
            text-decoration: underline;
        }
        .listLabel {
            float: left;
            width: 200px;
        }
        .listLabelFlow {
            float: left;
            width: 200px;
        }
        .clear:after {
            clear: both;
            content: "";
            display: table;
        }
        .listField {
            float: left;
            height: 30px;
            width: 400px;
            margin-right: 20px;
        }
        .tableHeader {
            font-weight: bold;
        }
        .errorText {
            color: red;
            padding: 10px;
        }
    </style>

</asp:Content>
