
    let messaget = '@ViewBag.ButtonName';


$(".saveAddRankConfirm").on("click", function () {

    event.preventDefault();

    var $form = $(this).closest("form");


    $form.find(".error-label").remove();


    let emptyFields = [];

    $form.find(":input[required]").each(function () {

        if ($.trim($(this).val() || "") === "") {
            emptyFields.push(this);
        }

    });

    if (emptyFields.length > 0) {

        let firstEmptyField = emptyFields[0];

        $(firstEmptyField).after(
            '<label class="error-label">This field is required</label>'
        );

        $(firstEmptyField).focus();
        return false;
    }

    let buttonName = $(this).val() || "";



    swal({
        title: "Please Confirm",
        text: "",
        type: "warning",
        showCancelButton: true,
        cancelButtonText: "Cancel",
        confirmButtonColor: "#DD6B55",
        confirmButtonText:
            buttonName === "Add"
                ? "Yes, Add"
                : "Yes, Update",
        closeOnCancel: true,
        closeOnConfirm: false
    }, function (isConfirm) {

        if (isConfirm) {

            swal({
                title: "",
                text:
                    buttonName === "Add"
                        ? "Added successfully!"
                        : "Updated successfully!",
                type: "success",
                confirmButtonColor: "#DD6B55",
                confirmButtonText: "OK",
                closeOnConfirm: true
            }, function () {

                // Submit form after clicking OK
                $form[0].submit();

            });

        }

    });

});



