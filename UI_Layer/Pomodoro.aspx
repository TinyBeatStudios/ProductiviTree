<%@ Page Title="Dashboard" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Pomodoro.aspx.cs" Inherits="UI_Layer.Pomodoro" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">


    <div class="container text-center mt-5">

        <h1>ProductiviTree Pomodoro Timer</h1>

        <p>Stay focused and build healthy study habits!</p>

        <div class="mt-4">

            <h3>Set Your Study Time</h3>

            <label for="studyMinutes">
                How many minutes would you like to study?
            </label>

            <br /><br />

            <input type="number"
                   id="studyMinutes"
                   min="1"
                   max="180"
                   value="25"
                   class="form-control mx-auto"
                   style="max-width: 200px;" />

            <br />

            <button type="button"
                    class="btn btn-success"
                    onclick="setTimer()">
                Set Timer
            </button>

        </div>

        <div class="mt-5">

            <h1 id="timerDisplay"
                style="font-size: 70px;">
                25:00
            </h1>

            <button type="button"
                    class="btn btn-success"
                    onclick="startTimer()">
                Start
            </button>

            <button type="button"
                    class="btn btn-warning"
                    onclick="pauseTimer()">
                Pause
            </button>

            <button type="button"
                    class="btn btn-danger"
                    onclick="resetTimer()">
                Reset
            </button>

        </div>

        <div class="mt-4">

            <h4>Completed Study Sessions</h4>

            <p id="sessionCount">0</p>

        </div>

    </div>

    <script src="<%= ResolveUrl("~/Scripts/pomodoro.js") %>"></script>

</asp:Content>

