
    let messaget = '@ViewBag.ButtonName';

    $(".OrderbyId").off("click").click(function () {
        let orderby = $(this).closest("td").find(".spnOrderbyId").html();
    updateorder(orderby);
        // CorpsOrderby
    });
    function updateorder(orderby) {

        $.ajax({
            url: '/Master/sqnOrderby',
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
    $(document).ready(function () {
        $(document).on("click", ".squadron-add-button", function () {
            AddConfirmation(
                this,
                $(this).data("url"),
                $(this).data("action")
            );
        });
    });
    function AddConfirmation(obj, ActionName, Submit) {
        event.preventDefault();
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

  
