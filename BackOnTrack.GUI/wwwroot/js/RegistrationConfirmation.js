document.addEventListener("DOMContentLoaded", function () {
    const passwordInput = document.getElementById("password");
    const confirmPasswordInput = document.getElementById("confirmPassword");
    const passwordMatchMessage = document.getElementById("passwordMatchMessage");
    const registerButton = document.getElementById("registerButton");

    function checkPasswordMatch() {
        const password = passwordInput.value;
        const confirmPassword = confirmPasswordInput.value;

        if (password !== confirmPassword) {
            passwordMatchMessage.textContent = "Passwords do not match!";
            registerButton.disabled = true;
        } else {
            passwordMatchMessage.textContent = "";
            registerButton.disabled = false;
        }
    }

    confirmPasswordInput.addEventListener("input", checkPasswordMatch);
});