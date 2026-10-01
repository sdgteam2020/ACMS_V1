
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

    // Remove any existing error labels
    $('.error-label').remove();

    // Check if all required form fields are filled in
    var emptyFields = [];
    $('form').find(':input[required]').each(function () {
        if ($.trim($(this).val()) === '') {
            emptyFields.push(this);
        }
    });

    // If there are empty fields, display an error message for the first empty field
    if (emptyFields.length > 0) {
        var firstEmptyField = emptyFields[0];
        $(firstEmptyField).after('<label class="error-label" style="color: red;">This field is required</label>');
        return;
    }

    // If all fields are filled, display the confirmation dialog
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
});
    $(document).ready(function () {
        $('.dropdownsearch').select2();
    });
