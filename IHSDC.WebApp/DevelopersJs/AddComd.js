

    var messaget = '@ViewBag.ButtonName';

    $(".OrderbyId").off("click").click(function () {
        var orderby = $(this).closest("td").find(".spnOrderbyId").html();
    updateorder(orderby);

    });
    function updateorder(orderby) {

        $.ajax({
            url: '/Master/CommadOrderby',
            type: 'POST',
            data: { "OrderbyId": orderby },
            success: function (result) {


                if (result.length > 0) {

                    swal("Successfully!", "Update Order Successfully", "success");
                    location.reload();

                }

            }
        });



}


$(".saveAddComdConfirm").on("click", function () {
        event.preventDefault();

    $('.error-label').remove();

    var emptyFields = [];
    $('form').find(':input[required]').each(function () {
            if ($.trim($(this).val()) === '') {
        emptyFields.push(this);
            }
        });

        if (emptyFields.length > 0) {
            var firstEmptyField = emptyFields[0];
    $(firstEmptyField).after('<label class="error-label" style="color: red;">This field is required</label>');
    return;
        }

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
