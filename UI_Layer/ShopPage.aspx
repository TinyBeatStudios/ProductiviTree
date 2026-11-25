<%@ Page Title="Theme Shop" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ShopPage.aspx.cs" Inherits="UI_Layer.ShopPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Theme Shop</h2>
    <p> Your Balance: <asp:Label ID="BalanceLabel" runat="server" /> coins</p>
    <asp:Label ID="MessageLabel" runat="server" CssClass="shop-message" />

    
    <asp:Repeater ID="CategoryRepeater" runat="server" OnItemDataBound="CategoryRepeater_ItemDataBound">
        <ItemTemplate>
            <section class="shop-section">
                <h2><%# Eval("Key") %></h2>
            
    
                <div class="shop-grid">
                    <asp:Repeater ID="CategoryItemsRepeater" runat="server" OnItemCommand="CategoryItemsRepeater_ItemCommand">
                        <ItemTemplate>
                            <div class="shop-card">

                                <span class="shop-card-title"><%# Eval("Name") %></span>
                                <span class="shop-card-price"><%# Eval("Price") %> coins</span>

                                <p class="shop-card-description"><%# Eval("Description") %></p>

                                <div class="shop-card-actions">
                                    <asp:Button runat="server" CssClass="btn btn-primary" Text='<%# Eval("Price") + " coins" %>' CommandName="Buy" CommandArgument='<%# Eval("ItemId") %>' Visible='<%# !(bool)Eval("Owned") %>' />

                                    <asp:Button runat="server" CssClass="btn btn-default" Text="Equip" CommandName="Equip" CommandArgument='<%# Eval("ItemId") %>' Visible='<%# (bool)Eval("Owned") && !(bool)Eval("Equipped") %>' />

                                    <asp:Label runat="server" CssClass="shop-equipped-label" Text ="✔ Equipped" Visible='<%# (bool)Eval("Equipped") %>' />
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </section>
        </ItemTemplate>
    </asp:Repeater>
</asp:Content>
