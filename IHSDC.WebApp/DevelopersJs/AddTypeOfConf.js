
    let messaget = '@ViewBag.ButtonName';


$(".updateTypeConf").on("click", function () {
    debugger;
    const payload = {
        TypeOfConf: $("#TypeOfConf").val(),
        PolicyId: $("#PolicyId").val(),
        TypeOfConfId: parseInt(atob($("#id").val()), 10)
    };

    let jsonData = JSON.stringify(payload);
    let encrypted = encryptPayloadData(jsonData);
    swal({
        title: "Are you sure?",
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
                $.ajax({
                    url: '/Master/UpdateTypeofConf',
                    type: 'POST',
                    data: { Request: encrypted },

                    success: function (response) {
                        swal({
                            title: "",
                            text: "Updated successfully!",
                            type: "success",

                            confirmButtonColor: "#DD6B55",
                            confirmButtonText: "OK",

                            closeOnConfirm: true,
                            closeOnCancel: true
                        });
                    },

                    error: function (xhr) {

                        swal({
                            title: "Update Failed!",
                            text: "",
                            type: "error",
                            confirmButtonColor: "#DD6B55",
                            closeOnConfirm: false,
                            closeOnCancel: true
                        });

                        console.log(xhr.status);
                        console.log(xhr.responseText);
                    }
                });
            }
        });
    
});






$(".saveAddConfirmation").on("click", function () {

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
    $(document).ready(function () {
        $('.dropdownsearch').select2();
    });


