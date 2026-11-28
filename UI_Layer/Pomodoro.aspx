
<%@ Page Title="Study Dashboard" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Pomodoro.aspx.cs" Inherits="UI_Layer.Pomodoro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <style>
        .pomodoro-dashboard {
            max-width: 1000px;
            margin: 30px auto;
        }

        .pomodoro-panel {
            border: 1px solid rgba(128,128,128,0.4);
            border-radius: 16px;
            padding: 25px;
            margin-bottom: 20px;
        }

        .timer-display {
            font-size: clamp(55px, 10vw, 100px);
            font-weight: bold;
            font-variant-numeric: tabular-nums;
        }

        .timer-mode {
            font-size: 20px;
            font-weight: 600;
        }

        .stat-value {
            font-size: 32px;
            font-weight: bold;
        }

        .timer-controls {
            display: flex;
            justify-content: center;
            flex-wrap: wrap;
            gap: 10px;
        }

        .timer-controls .btn {
            min-width: 100px;
        }

        .progress {
            height: 12px;
            margin: 20px 0;
        }
    </style>

    <div class="pomodoro-dashboard">

        <div class="text-center mb-4">
            <h1>ProductiviTree Study Dashboard</h1>
            <p>Stay focused. Build healthy habits. Grow your productivity.</p>
        </div>

        <asp:UpdatePanel ID="upPomodoro" runat="server"
            UpdateMode="Conditional">

            <ContentTemplate>

                <asp:Timer ID="timerCountdown"
                    runat="server"
                    Interval="1000"
                    Enabled="false"
                    OnTick="timerCountdown_Tick" />

                <!-- Timer Settings -->

                <div class="pomodoro-panel">

                    <h3>Timer Settings</h3>

                    <div class="row">

                        <div class="col-md-4 mb-3">

                            <asp:Label runat="server"
                                AssociatedControlID="txtFocus"
                                Text="Focus Duration (minutes)" />

                            <asp:TextBox ID="txtFocus"
                                runat="server"
                                TextMode="Number"
                                Text="25"
                                CssClass="form-control" />

                        </div>

                        <div class="col-md-4 mb-3">

                            <asp:Label runat="server"
                                AssociatedControlID="txtShortBreak"
                                Text="Short Break (minutes)" />

                            <asp:TextBox ID="txtShortBreak"
                                runat="server"
                                TextMode="Number"
                                Text="5"
                                CssClass="form-control" />

                        </div>

                        <div class="col-md-4 mb-3">

                            <asp:Label runat="server"
                                AssociatedControlID="txtLongBreak"
                                Text="Long Break (minutes)" />

                            <asp:TextBox ID="txtLongBreak"
                                runat="server"
                                TextMode="Number"
                                Text="15"
                                CssClass="form-control" />

                        </div>

                    </div>

                    <asp:Button ID="btnSetTimer"
                        runat="server"
                        Text="Apply Settings"
                        CssClass="btn btn-primary"
                        OnClick="btnSetTimer_Click" />

                    <asp:Label ID="lblMessage"
                        runat="server"
                        CssClass="d-block mt-3" />

                </div>

                <!-- Main Timer -->

                <div class="pomodoro-panel text-center">

                    <asp:Label ID="lblMode"
                        runat="server"
                        Text="Focus Session"
                        CssClass="timer-mode" />

                    <div class="timer-display">

                        <asp:Label ID="lblTimer"
                            runat="server"
                            Text="25:00" />

                    </div>

                    <div class="progress">

                        <div id="progressBar"
                            runat="server"
                            class="progress-bar bg-success"
                            style="width: 0%;">
                        </div>

                    </div>

                    <div class="timer-controls">

                        <asp:Button ID="btnStart"
                            runat="server"
                            Text="Start"
                            CssClass="btn btn-success"
                            OnClick="btnStart_Click" />

                        <asp:Button ID="btnPause"
                            runat="server"
                            Text="Pause"
                            CssClass="btn btn-warning"
                            OnClick="btnPause_Click" />

                        <asp:Button ID="btnReset"
                            runat="server"
                            Text="Reset"
                            CssClass="btn btn-danger"
                            OnClick="btnReset_Click" />

                    </div>

                </div>

                <!-- Study Statistics -->

                <div class="row text-center">

                    <div class="col-md-4">

                        <div class="pomodoro-panel">

                            <h5>Completed Sessions</h5>

                            <asp:Label ID="lblSessions"
                                runat="server"
                                Text="0"
                                CssClass="stat-value" />

                        </div>

                    </div>

                    <div class="col-md-4">

                        <div class="pomodoro-panel">

                            <h5>Study Minutes</h5>

                            <asp:Label ID="lblStudyTime"
                                runat="server"
                                Text="0"
                                CssClass="stat-value" />

                        </div>

                    </div>

                    <div class="col-md-4">

                        <div class="pomodoro-panel">

                            <h5>Current Cycle</h5>

                            <asp:Label ID="lblCycle"
                                runat="server"
                                Text="1 / 4"
                                CssClass="stat-value" />

                        </div>

                    </div>

                </div>

            </ContentTemplate>

        </asp:UpdatePanel>

    </div>

</asp:Content>
