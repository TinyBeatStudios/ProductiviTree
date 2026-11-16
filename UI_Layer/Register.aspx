<%@ Page Title="Register" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="UI_Layer.Register" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h1>Register for ProductiviTree</h1>

    <div class="auth-container">
        <asp:Label runat="server" AssociatedControlID="username" Text="Username:"></asp:Label>
        <asp:TextBox ID="username" runat="server"></asp:TextBox>
        <br />
        <asp:Label runat="server" AssociatedControlID="email" Text="Email:"></asp:Label>
        <asp:TextBox ID="email" runat="server" TextMode="Email"></asp:TextBox>
        <br />
        <asp:Label runat="server" AssociatedControlID="password" Text="Password:"></asp:Label>
        <asp:TextBox ID="password" runat="server" TextMode="Password"></asp:TextBox>
        <br />
        <asp:Label runat="server" AssociatedControlID="confirm_password" Text="Confirm Password:"></asp:Label>
        <asp:TextBox ID="confirm_password" runat="server" TextMode="Password"></asp:TextBox>
        <br />
        <asp:Button ID="registerButton" runat="server" Text="Register"/>
    </div>

    <p>Already have an account? <a runat="server" href="~/Login.aspx">Login here!</a>.</p>
</asp:Content>