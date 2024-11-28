
function ValInDataEmail(input) {
    var regex = /[^a-zA-Z0-9@._-]/g;
    input.value = input.value.replace(regex, "");
}