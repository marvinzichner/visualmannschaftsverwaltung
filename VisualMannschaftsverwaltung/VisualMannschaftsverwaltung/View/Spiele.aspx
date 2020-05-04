﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Spiele.aspx.cs" Inherits="VisualMannschaftsverwaltung.View.Spiele" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h3>Aktives Spiel</h3>
    <div class="text-center">
        <span class="big">Mannschaft1</span> &emsp; VS &emsp; <span class="big">Mannschaft2</span>
    </div>
    <br />
    <h4>Geplante Spiele</h4>
        <div class="planned">MANNSCHAFT &emsp; VS &emsp; MANNSCHAFT</div>
        <div class="planned">MANNSCHAFT &emsp; VS &emsp; MANNSCHAFT</div>
    <hr />

    <h3>Vergangene Spiele</h3>
    <br />
    <h4>Fussball</h4>
    <table class="tablemaster">
        <tr class="tr-section">
            <td class="td-name">1. FC Köln</td>
            <td class="td-score">1</td>
            <td class="td-score">4</td>
            <td class="td-name td-right">FC Bayern München</td>
        </tr>
        <tr class="tr-section">
            <td class="td-name">Borussia Dortmund</td>
            <td class="td-score">1</td>
            <td class="td-score">1</td>
            <td class="td-name td-right">FC Schalke 04</td>
        </tr>
        <tr class="tr-section">
            <td class="td-name">TSG 1899 Hoffenheim</td>
            <td class="td-score">2</td>
            <td class="td-score">3</td>
            <td class="td-name td-right">Eintracht Frankfurt</td>
        </tr>
    </table>

    <br />
    <h4>Handball</h4>
    <table class="tablemaster">
        <tr class="tr-section">
            <td class="td-name">1. FC Köln</td>
            <td class="td-score">1</td>
            <td class="td-score">4</td>
            <td class="td-name td-right">FC Bayern München</td>
    </table>

    <style>
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
        .tr-section {
            margin-top: 10px;
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
    </style>

</asp:Content>
