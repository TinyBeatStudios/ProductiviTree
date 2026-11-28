
using System;
using System.Web.UI;

namespace UI_Layer
{
    public partial class Pomodoro : Page
    {
        private int FocusMinutes
        {
            get { return (int)(Session["PomoFocus"] ?? 25); }
            set { Session["PomoFocus"] = value; }
        }

        private int ShortBreakMinutes
        {
            get { return (int)(Session["PomoShortBreak"] ?? 5); }
            set { Session["PomoShortBreak"] = value; }
        }

        private int LongBreakMinutes
        {
            get { return (int)(Session["PomoLongBreak"] ?? 15); }
            set { Session["PomoLongBreak"] = value; }
        }

        private int RemainingSeconds
        {
            get { return (int)(Session["PomoRemaining"] ?? 1500); }
            set { Session["PomoRemaining"] = value; }
        }

        private int TotalSeconds
        {
            get { return (int)(Session["PomoTotal"] ?? 1500); }
            set { Session["PomoTotal"] = value; }
        }

        private int CompletedSessions
        {
            get { return (int)(Session["PomoSessions"] ?? 0); }
            set { Session["PomoSessions"] = value; }
        }

        private int TotalStudyMinutes
        {
            get { return (int)(Session["PomoStudyTime"] ?? 0); }
            set { Session["PomoStudyTime"] = value; }
        }

        private string CurrentMode
        {
            get { return (string)(Session["PomoMode"] ?? "Focus"); }
            set { Session["PomoMode"] = value; }
        }

        private bool IsRunning
        {
            get { return (bool)(Session["PomoRunning"] ?? false); }
            set { Session["PomoRunning"] = value; }
        }

        private DateTime EndTime
        {
            get
            {
                return (DateTime)(Session["PomoEndTime"]
                    ?? DateTime.UtcNow);
            }
            set { Session["PomoEndTime"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtFocus.Text = FocusMinutes.ToString();
                txtShortBreak.Text = ShortBreakMinutes.ToString();
                txtLongBreak.Text = LongBreakMinutes.ToString();

                if (IsRunning)
                {
                    UpdateRemainingTime();
                }

                UpdateDisplay();
            }
        }

        // Apply custom timer settings
        protected void btnSetTimer_Click(object sender, EventArgs e)
        {
            int focus;
            int shortBreak;
            int longBreak;

            if (!int.TryParse(txtFocus.Text, out focus) ||
                !int.TryParse(txtShortBreak.Text, out shortBreak) ||
                !int.TryParse(txtLongBreak.Text, out longBreak))
            {
                lblMessage.Text = "Please enter valid whole numbers.";
                return;
            }

            if (focus < 1 || focus > 180 ||
                shortBreak < 1 || shortBreak > 60 ||
                longBreak < 1 || longBreak > 120)
            {
                lblMessage.Text =
                    "Focus: 1-180 minutes. Short break: 1-60. Long break: 1-120.";

                return;
            }

            FocusMinutes = focus;
            ShortBreakMinutes = shortBreak;
            LongBreakMinutes = longBreak;

            IsRunning = false;
            CurrentMode = "Focus";

            TotalSeconds = FocusMinutes * 60;
            RemainingSeconds = TotalSeconds;

            lblMessage.Text = "Timer settings updated successfully.";

            UpdateDisplay();
        }

        // Start or resume the countdown
        protected void btnStart_Click(object sender, EventArgs e)
        {
            if (IsRunning)
                return;

            EndTime = DateTime.UtcNow.AddSeconds(RemainingSeconds);

            IsRunning = true;

            lblMessage.Text = CurrentMode == "Focus"
                ? "Stay focused. You can do this!"
                : "Enjoy your break!";

            UpdateDisplay();
        }

        // Pause the countdown
        protected void btnPause_Click(object sender, EventArgs e)
        {
            if (!IsRunning)
                return;

            UpdateRemainingTime();

            IsRunning = false;

            lblMessage.Text = "Timer paused.";

            UpdateDisplay();
        }

        // Reset the current timer
        protected void btnReset_Click(object sender, EventArgs e)
        {
            IsRunning = false;

            RemainingSeconds = TotalSeconds;

            lblMessage.Text = "Timer reset.";

            UpdateDisplay();
        }

        // Called every second by the ASP.NET Timer
        protected void timerCountdown_Tick(object sender, EventArgs e)
        {
            if (IsRunning)
            {
                UpdateRemainingTime();
            }

            UpdateDisplay();
        }

        // Calculate remaining time using the actual end time
        private void UpdateRemainingTime()
        {
            RemainingSeconds = Math.Max(
                0,
                (int)Math.Ceiling(
                    (EndTime - DateTime.UtcNow).TotalSeconds
                )
            );

            if (RemainingSeconds == 0)
            {
                CompleteSession();
            }
        }

        // Complete a focus session or break
        private void CompleteSession()
        {
            if (CurrentMode == "Focus")
            {
                CompletedSessions++;

                TotalStudyMinutes += TotalSeconds / 60;

                if (CompletedSessions % 4 == 0)
                {
                    CurrentMode = "Long Break";
                    TotalSeconds = LongBreakMinutes * 60;

                    lblMessage.Text =
                        "Four focus sessions completed! Enjoy a long break.";
                }
                else
                {
                    CurrentMode = "Short Break";
                    TotalSeconds = ShortBreakMinutes * 60;

                    lblMessage.Text =
                        "Focus session completed! Take a short break.";
                }
            }
            else
            {
                CurrentMode = "Focus";
                TotalSeconds = FocusMinutes * 60;

                lblMessage.Text =
                    "Break completed! Time to focus again.";
            }

            RemainingSeconds = TotalSeconds;

            EndTime = DateTime.UtcNow.AddSeconds(RemainingSeconds);

            IsRunning = true;
        }

        // Refresh the timer and statistics
        private void UpdateDisplay()
        {
            TimeSpan time = TimeSpan.FromSeconds(RemainingSeconds);

            lblTimer.Text = string.Format(
                "{0:00}:{1:00}",
                (int)time.TotalMinutes,
                time.Seconds
            );

            lblMode.Text = CurrentMode == "Focus"
                ? "Focus Session"
                : CurrentMode;

            lblSessions.Text = CompletedSessions.ToString();

            lblStudyTime.Text = TotalStudyMinutes.ToString();

            int cycle = CurrentMode == "Focus"
                ? CompletedSessions % 4 + 1
                : (CompletedSessions - 1) % 4 + 1;

            lblCycle.Text = cycle + " / 4";

            int progress = TotalSeconds > 0
                ? (int)((TotalSeconds - RemainingSeconds)
                    * 100.0 / TotalSeconds)
                : 0;

            progressBar.Style["width"] = progress + "%";

            timerCountdown.Enabled = IsRunning;

            btnStart.Enabled = !IsRunning;
            btnPause.Enabled = IsRunning;

            btnStart.Text = RemainingSeconds < TotalSeconds
                ? "Resume"
                : "Start";

            txtFocus.Enabled = !IsRunning;
            txtShortBreak.Enabled = !IsRunning;
            txtLongBreak.Enabled = !IsRunning;
            btnSetTimer.Enabled = !IsRunning;
        }
    }
}
