document.addEventListener("DOMContentLoaded", function () {

    document.querySelectorAll(".restrict-copy-paste")
        .forEach(function (field) {

            ["copy", "paste", "cut", "drop", "contextmenu"]
                .forEach(function (event) {

                    field.addEventListener(event, function (e) {
                        e.preventDefault();
                    });

                });
        });
});

document.addEventListener("DOMContentLoaded", function () {

    const password = document.getElementById("pass");
    const toggle = document.getElementById("togglePassword");
    const icon = document.getElementById("passwordIcon");

    if (!password || !toggle || !icon) return;

    toggle.addEventListener("click", function () {

        const isHidden = password.type === "password";

        password.type = isHidden ? "text" : "password";

        icon.classList.toggle("fa-eye", !isHidden);
        icon.classList.toggle("fa-eye-slash", isHidden);

        toggle.setAttribute("aria-pressed", isHidden);

        toggle.setAttribute(
            "aria-label",
            isHidden ? "Hide password" : "Show password"
        );
    });

});