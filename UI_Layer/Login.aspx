<%@ Page Title="Login" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="UI_Layer.Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h1>Welcome to ProductiviTree!</h1>

    <p>Please login or create a new account if you are a new member.</p>

    <div class="auth-container">
        <asp:Label runat="server" AssociatedControlID="username" Text="Username:"></asp:Label>
        <asp:TextBox ID="username" runat="server"></asp:TextBox>
        <br />
        <asp:Label runat="server" AssociatedControlID="password" Text="Password:"></asp:Label>
        <asp:TextBox ID="password" runat="server" TextMode="Password"></asp:TextBox>
        <br />
        <asp:Button ID="loginButton" runat="server" Text="Login" OnClick="loginButton_Click"/>
    </div>

    <p>Dont have an account? <a runat="server" href="~/Register.aspx">Register Here!</a>.</p>

</asp:Content>
