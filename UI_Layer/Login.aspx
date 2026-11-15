<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="UI_Layer.Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

 <!DOCTYPE html>
<html xmlns ="http://www.w3.org/2000/svg">
    <meta charset="utf-8" />
    <title>ProductiviTree</title>
<body>
    <h1>Welcome to ProductiviTree!</h1>
    <p>Please login or create a new account if you are a new member.</p>
        <div class="auth-container">
        <label for="username">Username:</label>
        <input type="text" id="username" name="username" />
        <br />
        <label for="password">Password:</label>
        <input type="password" id="password" name="password" />
        <br />
        <input type="submit" value="Login" />
    
    <p>Dont have an account? <a href="Register.aspx">Register here</a>.</p>
</body>
</html>

</asp:Content>
