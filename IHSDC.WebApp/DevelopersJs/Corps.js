
    $(document).ready(function () {
        $('.dropdownsearch').select2();
    });

    


    $(".OrderbyId").off("click").click(function () {
        let orderby = $(this).closest("td").find(".spnOrderbyId").html();
    updateorder(orderby);
      
    });
    function updateorder(orderby) {

        $.ajax({
            url: '/Master/CorpsOrderby',
            type: 'POST',
            data: { "OrderbyId": orderby }, //get the search string
            success: function (result) {


                if (result == 0) {

                    swal("Successfully!", "Update Order Successfully", "success");
                    location.reload();

                }

            }
        });



    }


$(document).on("submit", "#corpsFormoutside", function (e) {

    e.preventDefault();
    debugger;
    let $form = $(this);

    // Run client-side validation
    if (!$form.valid()) {
        return false;
    }

    let id = $form.find("#id").val() || "";
    let btnval = $form.find("#btnval").val() || "";

    let buttonName = $form.find(".saveAddCorpsConfirm").val() || "";

    let CorpsName = $form.find("#CorpsName").val();
    let ComdId = $form.find("#ComdId").val();

    // Hide previous server-side errors
    $("#CorpsNameValidation")
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
            url: '/SignUp/AddCorps',
            type: 'POST',

            // Automatically includes AntiForgeryToken and form fields
            data: $form.serialize(),

            success: function (response) {

                console.log(response);

                // Server-side validation errors
                if (response && response.success === false) {

                    let firstError = Object.values(response.errors || {})
                        .flat()[0] || response.message || "Validation failed.";

                    $("#CorpsNameValidation")
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

                    $('#addDgsModal').modal('hide');

                   
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
