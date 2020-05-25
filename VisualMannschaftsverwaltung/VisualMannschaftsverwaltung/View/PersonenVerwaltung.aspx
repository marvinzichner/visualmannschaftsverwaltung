<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PersonenVerwaltung.aspx.cs" Inherits="VisualMannschaftsverwaltung.View.PersonenVerwaltung" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div runat="server" ID="errorMessages" class="errorText"></div>

    <h4>1. Wählen Sie den zu erstellenden Personentyp aus:</h4>
    <asp:DropDownList ID="PersonSelectionTypeDD" runat="server">
        <asp:ListItem Value="FussballSpieler">Fussballspieler</asp:ListItem>
        <asp:ListItem Value="HandballSpieler">Handballspieler</asp:ListItem>
        <asp:ListItem Value="TennisSpieler">Tennisspieler</asp:ListItem>
        <asp:ListItem Value="Physiotherapeut">Physiotherapeut</asp:ListItem>
        <asp:ListItem Value="Trainer">Trainer</asp:ListItem>
    </asp:DropDownList>

    <!--
    <asp:RadioButtonList ID="PersonSelectionType2" runat="server">
        <asp:ListItem Value="FussballSpieler">Fussballspieler</asp:ListItem>
        <asp:ListItem Value="HandballSpieler">Handballspieler</asp:ListItem>
        <asp:ListItem Value="TennisSpieler">Tennisspieler</asp:ListItem>
        <asp:ListItem Value="Physiotherapeut">Physiotherapeut</asp:ListItem>
        <asp:ListItem Value="Trainer">Trainer</asp:ListItem>
    </asp:RadioButtonList>
    -->

    &emsp;
    <asp:Button ID="buttonConfirmSelection" runat="server" Text="Auswahl bestätigen" OnClick="confirmPersonSelection" />
    <br />

    <h4>2. Geben Sie Detailinformationen zu dieser Person an:</h4>
    <i>HILFE: </i><b>Birthdate</b> dd.mm.yyyy &emsp; <b>IsLeftFeet</b> True/False &emsp; <b>IsLeftHand</b> True/False  &emsp; <b>HasLicense</b> True/False<br />
    <br />

    <div class="clear">
        <div class="listSpacer">
            <div class="listLabelFlow">Vorname</div>
            <asp:TextBox AutoCompleteType="Disabled" class="listField" ID="fieldVorname" CssClass="listField" runat="server"></asp:TextBox>
        </div>
        <div class="listSpacer">
            <div class="listLabelFlow">Nachname</div>
            <asp:TextBox AutoCompleteType="Disabled" class="listField" ID="fieldNachname" CssClass="listField" runat="server"></asp:TextBox>
        </div>
        <div class="listSpacer">
            <div class="listLabelFlow">Geburtsdatum</div>
            <asp:TextBox AutoCompleteType="Disabled" class="listField" TextMode="DateTime" ID="fieldBirthdate" CssClass="listField" runat="server"></asp:TextBox>
        </div>

        <div class="listSpacer listSpacerDynamic" runat="server" id="dynamicFlow"></div>
    </div>

    <asp:Button ID="button2" runat="server" OnClick="createNewPerson" Text="Person erstellen" />
    <br />

    <hr />

    <h4>Anzeige der vorhandenen Personen</h4>
    Sortieren nach:
    <asp:DropDownList ID="dropDownSorting" runat="server">
        <asp:ListItem Value="UNSORTED">Bitte wählen</asp:ListItem>
        <asp:ListItem Value="NAME_ASC">Vorname (A-Z)</asp:ListItem>
        <asp:ListItem Value="ERFOLG_ASC">Erfolg (gut-schlecht, gruppiert)</asp:ListItem>
        <asp:ListItem Value="BIRTHDATE_ASC">Geburtsdatum (alt-jung)</asp:ListItem>
    </asp:DropDownList>
    <asp:Button ID="confirmSortingButton" runat="server" OnClick="dropDownSortingChanged" Text="Sortierung anwenden" />
    <asp:Button ID="downloadButton" runat="server" OnClick="generateXML" Text="XML Export" />
    &emsp;&emsp;&emsp;
    <asp:Button ID="editButton" runat="server" OnClick="editSelectedPerson" Text="Bearbeiten" Visible="false" />
    <asp:Button ID="deleteButton" runat="server" OnClick="removeSelectedPerson" Text="Löschen" Visible="false" />
    
    <br /><br />
    <div runat="server" id="staticPersonListHeader" class="tableHeader"></div>
    <div runat="server" id="dynamicPersonList" class="clear"></div>

    <br /><br />

    <style>
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