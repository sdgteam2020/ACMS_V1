
let messaget = '@ViewBag.ButtonName';
    $(".OrderbyId").off("click").click(function () {

       
        let orderby = $(this).closest("td").find(".ApptOrderbyId").html();
    let oldorderby = $(this).closest("td").find(".ApptoldOrderbyId").html();
    alert(orderby);
    updateorder(orderby, oldorderby);
    });
    function updateorder(orderby, oldorderby) {
        $.ajax({
            url: '/Master/ApptOrderby',
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

$(".saveAddApptConfirm").on("click", function () {

    event.preventDefault();
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



