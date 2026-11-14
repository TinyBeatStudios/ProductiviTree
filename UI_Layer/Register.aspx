<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="UI_Layer.Register" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

 <!DOCTYPE html>
<html xmlns ="http://www.w3.org/2000/svg">
    <meta charset="utf-8" />
    <title>ProductiviTree</title>
<body>
    <h1>Register for ProductiviTree</h1>
    <form>
        <label for="username">Username:</label>
        <input type="text" id="username" name="username" />
        <br />
        <label for="email">Email:       </label>
        <input type="email" id="email" name="email" />
        <br />
        <label for="password">Password:</label>
        <input type="password" id="password" name="password" />
        <br />
        <label for="confirm_password">Confirm Password:</label>
        <input type="password" id="confirm_password" name="confirm_password" />
        <br />
        <input type="submit" value="Register" />
    </form>
    <p>Already have an account? <a href="Login.aspx">Login here</a>.</p>
</body>
</html>
</asp:Content>