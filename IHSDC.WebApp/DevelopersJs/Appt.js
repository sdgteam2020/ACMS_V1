
    let messaget = '@ViewBag.ButtonName';
    $(".OrderbyId").off("click").click(function () {
        let orderby = $(this).closest("td").find(".ApptOrderbyId").html();
    let oldorderby = $(this).closest("td").find(".ApptoldOrderbyId").html();
    alert(orderby);
    updateorder(orderby, oldorderby);
    });
    function updateorder(orderby, oldorderby) {
        $.ajax({
            url: '/SignUp/ApptOrderby',
            type: 'POST',
            data: { "OrderbyId": orderby, "NewOrderbyId": oldorderby },
            success: function (result) {
                if (result == 0) {

                    swal("Successfully!", "Update Order Successfully", "success");
                    location.reload();
                }
            }
        });
    }

    function goBack() {
        if (window.history.length > 1) {
        window.history.back();
        } else {
        window.location.href = '@Url.Action("SignUp", "Account")';


        }
    }


    function AddConfirmation(obj, ActionName, Submit) {
        event.preventDefault();

    if (messaget == "Add") {
        swal({
            title: "Please Confirm",
            text: "",
            type: "warning",

            showCancelButton: true,
            cancelButtonText: "Cancel",
            cancelButtonColor: "#FF0000",
            confirmButtonColor: "#DD6B55",
            confirmButtonText: "Yes, Add",

            closeOnCancel: true,
            closeOnConfirm: false
        },
            function (isConfirm) {
                if (isConfirm) {
                    $('form').submit();
                }
            });
        } else {
        swal({
            title: "Please Confirm",
            text: "",
            type: "warning",
            showCancelButton: true,
            confirmButtonColor: "#DD6B55",
            confirmButtonText: "Yes, Update",
            cancelButtonText: "Cancel",

            closeOnConfirm: false,
            closeOnCancel: true
        },
            function (isConfirm) {
                if (isConfirm) {
                    $('form').submit();
                }
            });
        }
    }

    let message = '@message';
    let messageStatus = '@messageStatus';
    let messagemidStatus = '@messagemidStatus';
    if (message)
    sweetAlert(message, messagemidStatus, messageStatus);
