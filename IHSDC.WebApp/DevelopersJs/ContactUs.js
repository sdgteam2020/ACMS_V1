window.addEventListener("keydown", function (e) {
    if (e.key === "ArrowUp" || e.key === "ArrowDown") {
        e.preventDefault();
    }
});

window.addEventListener("wheel", function (e) {

    e.preventDefault();
});