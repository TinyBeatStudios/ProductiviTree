<%@ Page Title="Theme Shop" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ShopPage.aspx.cs" Inherits="UI_Layer.ShopPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Theme Shop</h2>
    <p> Your Balance: <asp:Label ID="BalanceLabel" runat="server" /> coins</p>
    <asp:Label ID="MessageLabel" runat="server" CssClass="shop-message" />

    <asp:Repeater ID="ShopRepeater" runat="server" OnItemCommand="ShopRepeater_ItemCommand">
        <ItemTemplate>
            <div class="shop-card">
                <h3><%# Eval("Name") %></h3>
                <p><%# Eval("Description") %></p>

                <asp:Button runat="server" CssClass="btn btn-primary" Text='<%# "Buy for " + Eval("Price") + " coins" %>' CommandName="Buy" CommandArgument='<%# Eval("ItemId") %>' Visible='<%# !(bool)Eval("Owned") %>' />

                <asp:Button runat="server" CssClass="btn btn-default" Text="Equip" CommandName="Equip" CommandArgument='<%# Eval("ItemId") %>' Visible='<%# (bool)Eval("Owned") && !(bool)Eval("Equipped") %>' />

                <asp:Label runat="server" Text ="✔ Equipped" Visible='<%# (bool)Eval("Equipped") %>' />
            </div>
        </ItemTemplate>
    </asp:Repeater>
</asp:Content>
