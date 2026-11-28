
let timerInterval = null;
let selectedTime = 25 * 60;
let remainingTime = selectedTime;
let completedSessions = 0;
let endTime = null;

// Update the timer display
function updateDisplay() {
    const minutes = Math.floor(remainingTime / 60);
    const seconds = remainingTime % 60;

    document.getElementById("timerDisplay").textContent =
        String(minutes).padStart(2, "0") + ":" +
        String(seconds).padStart(2, "0");
}

// Allow students to enter their own study time
function setTimer() {
    const input = document.getElementById("studyMinutes");
    const minutes = Number(input.value);

    if (!Number.isInteger(minutes) ||
        minutes < 1 || minutes > 180) {
        alert("Please enter a whole number between 1 and 180.");
        return;
    }

    pauseTimer();

    selectedTime = minutes * 60;
    remainingTime = selectedTime;

    updateDisplay();
}

// Start or resume the countdown
function startTimer() {
    if (timerInterval !== null || remainingTime <= 0) {
        return;
    }

    endTime = Date.now() + remainingTime * 1000;

    timerInterval = setInterval(function () {
        remainingTime = Math.max(
            0,
            Math.ceil((endTime - Date.now()) / 1000)
        );

        updateDisplay();

        if (remainingTime === 0) {
            clearInterval(timerInterval);
            timerInterval = null;
            endTime = null;

            completedSessions++;

            document.getElementById("sessionCount").textContent =
                completedSessions;

            alert("Great job! You completed your study session!");
        }
    }, 250);
}

// Pause the countdown
function pauseTimer() {
    if (timerInterval !== null) {
        remainingTime = Math.max(
            0,
            Math.ceil((endTime - Date.now()) / 1000)
        );

        clearInterval(timerInterval);
        timerInterval = null;
        endTime = null;

        updateDisplay();
    }
}

// Reset to the student's selected duration
function resetTimer() {
    pauseTimer();

    remainingTime = selectedTime;
    updateDisplay();
}
