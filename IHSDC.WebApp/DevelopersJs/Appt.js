
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
      
            window.location.href = '/SignUp/Account';

        }
    }

$(document).on("submit", "#apptFormhoutside", function (e) {

    e.preventDefault();
    debugger;
    let $form = $(this);

    // Run client-side validation
    if (!$form.valid()) {
        return false;
    }

    let id = $form.find("#id").val() || "";
    let btnval = $form.find("#btnval").val() || "";

    let buttonName = $form.find(".saveAddApptConfirm").val() || "";

    let Appt = $form.find("#Appt").val();
    let ApptAbbreviation = $form.find("#ApptAbbreviation").val();

    // Hide previous server-side errors
    $("#ApptNameValidation")
        .text("")
        .addClass("d-none");

    swal({
        title: "Please Confirm",
        text: "",
        type: "warning",
        showCancelButton: true,
        cancelButtonText: "Cancel",
        confirmButtonColor: "#DD6B55",
        confirmButtonText: buttonName === "Add"
            ? "Yes, Add"
            : "Yes, Update",
        closeOnCancel: true,
        closeOnConfirm: false

    }, function (isConfirm) {

        if (!isConfirm) {
            return;
        }

        $.ajax({
            url: '/SignUp/AddAppt',
            type: 'POST',

            // Automatically includes AntiForgeryToken and form fields
            data: $form.serialize(),

            success: function (response) {

                console.log(response);

                // Server-side validation errors
                if (response && response.success === false) {

                    let firstError = Object.values(response.errors || {})
                        .flat()[0] || response.message || "Validation failed.";

                    $("#ApptNameValidation")
                        .text(firstError)
                        .removeClass("d-none");

                    swal({
                        title: "Failed!",
                        text: response.message || "Validation failed.",
                        type: "error",
                        confirmButtonColor: "#DD6B55"
                    });

                    return;
                }

                // Success
                swal({
                    title: "Success!",
                    text: btnval === "Add"
                        ? "Added successfully!"
                        : "Updated successfully!",
                    type: "success",
                    confirmButtonColor: "#DD6B55",
                    confirmButtonText: "OK",
                    closeOnConfirm: true

                }, function () {

                    $('#addApptModal').modal('hide');

                    window.location.href = '/SignUp/AddAppt';
                });
            },

            error: function (xhr, status, error) {

                console.error("Status:", xhr.status);
                console.error("Response:", xhr.responseText);
                console.error("Error:", error);

                swal({
                    title: "Request Failed!",
                    text: "HTTP Status: " + xhr.status,
                    type: "error",
                    confirmButtonColor: "#DD6B55"
                });
            }
        });
    });

    return false;
});
  