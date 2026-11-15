<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="Akashic.aspx.cs" Inherits="UI_Layer.WebForm1" %>

<asp:Content ID = "Akashic" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .book {
            position: absolute;
            min-width: 100vw;
            min-height: 50vh;
            bottom: 0px;
            justify-content: center;
        }
    </style>
    
    <h1>Akashic Record</h1>
    <p>Welcome to the Akashic Records. Here you can view and manage your quests and tasks.</p>

    <div class="book">
        this holds the questbook and the quest log, as well as the quest tracker
    </div>

</asp:Content>




