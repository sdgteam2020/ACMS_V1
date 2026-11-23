


    $('#ComdIdUnit').change(function () {

        alert($('#ComdIdUnit').val());
    $('#CorpId').val(1);
    $('#BdeId').val(1);

    BindCorpUnit($("#ComdIdUnit").val());
    BindBde($("#ComdIdUnit").val(), $("#CorpId").val());
    BindUnitUnit($("#ComdIdUnit").val(), $("#CorpId").val(), $("#BdeId").val());
    });


    $('#spnCorpUnit').change(function () {

        $("#CorpId").val($('#spnCorpUnit').val());
    BindBde($("#ComdIdUnit").val(), $("#CorpId").val());
    BindUnitUnit($("#ComdIdUnit").val(), $("#CorpId").val(), $("#BdeId").val());
    });

    $('#spnBde').change(function () {
        $("#BdeId").val($('#spnBde').val());
    BindUnitUnit($("#ComdIdUnit").val(), $("#CorpId").val(), $("#BdeId").val());
    });

    $('#spnUnit').change(function () {
        $("#Unit_ID").val($('#spnUnit').val());
    });

    function goBack() {
        if (window.history.length > 1) {
        window.history.back();
        } else {
        window.location.href = '@Url.Action("SignUp", "Account")';


        }
    }
    $(document).on("click", ".js-add-appt", function (event) {
        AddConfirmation(this, '/SignUp/AddUnit', 'AddUnit', event);
    });
    function AddConfirmationBranch(obj, ActionName, Submit) {
        event.preventDefault();
        let $form = $(this).closest("form");
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

$(document).on("submit", "#unitFormoutside", function (e) {

    e.preventDefault();
    debugger;
    let $form = $(this);

    // Run client-side validation
    if (!$form.valid()) {
        return false;
    }

    let id = $form.find("#id").val() || "";
    let btnval = $form.find("#btnval").val() || "";

    let buttonName = $form.find(".saveAddunitConfirm").val() || "";

    let UnitName = $form.find("#UnitName").val();
    let ComdIdUnit = $form.find("#ComdIdUnit").val();
    let CorpId = $form.find("#CorpId").val();

    // Hide previous server-side errors
    $("#unitNameValidation")
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
            url: '/SignUp/AddUnitNew',
            type: 'POST',

            // Automatically includes AntiForgeryToken and form fields
            data: $form.serialize(),

            success: function (response) {

                console.log(response);

                // Server-side validation errors
                if (response && response.success === false) {

                    let firstError = Object.values(response.errors || {})
                        .flat()[0] || response.message || "Validation failed.";

                    $("#unitNameValidation")
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

                    $('#addunitmodel').modal('hide');


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
