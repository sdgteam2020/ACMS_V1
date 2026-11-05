
    $("#chk").click(function () {
            if ($(this).prop("checked")) {
        $("#pass").attr("type", "text");
            }
    else {

        $("#pass").attr("type", "password");
            }
        });
    $(document).ready(function () {
        $("#pass").val("");
        });
    





    $("#chk").click(function () {
            if ($(this).prop("checked")) {
        $("#pass").attr("type", "text");
            }
    else {

        $("#pass").attr("type", "password");
            }
        });
    $(document).ready(function () {
        $("#pass").val("");
        });

    let message = '@message';
    let messageStatus = '@messageStatus';
    let messagemidStatus = '@messagemidStatus';
    if (message) {
            if (message == "s") {
        SubmitToken(this, '/Account/Login', 'Your Token is not connected. Connect Token and Login again', 'SubmitGEB2IC')
    }
    if (message == "b") {
        SubmitToken(this, '/Account/Login', 'Public Key not Found. Ask Admin to install your public key.', 'SubmitGEB2IC')
    }
        }

