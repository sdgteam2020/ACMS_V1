
    var messaget = '@ViewBag.ButtonName';

    $(".OrderbyId").off("click").click(function () {
        var orderby = $(this).closest("td").find(".BranchOrderbyId").html();
    updateorder(orderby);
    });
    function updateorder(orderby) {
        $.ajax({
            url: '/Master/BranchOrderby',
            type: 'POST',
            data: { "OrderbyId": orderby },
            success: function (result) {
                if (result == 0) {

                    swal("Successfully!", "Update Order Successfully", "success");
                    location.reload();
                }
            }
        });
    }

$(".saveAddBranchConfirm").on("click", function () {
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
    }
    );

 