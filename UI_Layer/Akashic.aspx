<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="Akashic.aspx.cs" Inherits="UI_Layer.WebForm1" %>

<asp:Content ID = "Akashic" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .book{
            position: absolute;
            width: 80vw;
            max-width: 6000px;
            top: 10vw;
            left: 10vw;
        }
        .leftpage {
            position: relative;
            margin-left: 10vw;
            margin-top: 5vw;
        }
        .rightpage {
            position: relative;
            margin-right: 10vw;
        }
        #questList {
            list-style-type: none;
            padding: 0;
        }
    </style>

    <main>
        <asp:Image ID="QuestBookImage" runat="server" ImageUrl="~/Images/Questbook.png" CssClass="book"></asp:Image>

        <!-- Contents for the quest book will go below. This includes having a set "book" background, quest information formatted properly, and maybe interactive elements like flipping pages for each quest. the left side of the book will have the quest log/list, and the right side will have the selected quest on display. -->
        
        <div class="book">
            <div class="leftpage">
                <h1>Akashic Records</h1>
                <asp:Label ID="WelcomeLabel" runat="server" Text="Welcome to the Akashic Records. Here you can view and manage your quests."></asp:Label>

                <ol id="questList">
                    <li>
                        <asp:LinkButton runat="server"> - Task #1 name </asp:LinkButton>
                    </li>

                    <li>
                        <asp:LinkButton runat="server"> - Task #2 name </asp:LinkButton>
                    </li>
                </ol>
            </div>

            <div class="rightpage">
                
            </div>
        </div>
    </main>
</asp:Content>