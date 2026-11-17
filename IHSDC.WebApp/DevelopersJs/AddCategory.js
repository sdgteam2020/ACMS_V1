
    let messaget = '@ViewBag.ButtonName';

    $(".OrderbyId").off("click").click(function () {
        let orderby = $(this).closest("td").find(".CatOrderbyId").html();
    updateorder(orderby);
    });
    function updateorder(orderby) {
        $.ajax({
            url: '/Master/CATOrderby',
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

$(document).ready(function () {
    $("#catForm").on("submit", async function (e) {

        e.preventDefault();
        let $form = $("#catForm");
        if (!$form.valid()) {

            return false;
        }
        let id = $("#id").val() || "";
        let btnval = $("#btnval").val();
        let buttonName = $(".saveAddCatgConfirm").val() || "";
        let CategoryName = $("#CategoryName").val();
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

            $.ajax({
                url: '/Master/AddCategory',
                type: 'POST',

                data: {
                    __RequestVerificationToken:
                        $('#catForm input[name="__RequestVerificationToken"]').val(),

                    id: id,
                    CategoryName: CategoryName,
                    btnval: btnval
                },

                success: function (response) {

                    console.log(response);

                    // SERVER-SIDE VALIDATION ERROR
                    if (response && response.success === false) {

                        let firstError = Object.values(response.errors || {})
                            .flat()[0] || response.message || "";

                        $("#CatNameValidation")
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

                    // SUCCESS


                    swal({
                        title: "",
                        text: btnval === "Add"
                            ? "Added successfully!"
                            : "Updated successfully!",
                        type: "success",
                        confirmButtonColor: "#DD6B55",
                        confirmButtonText: "OK",
                        closeOnConfirm: true
                    }, function () {

                        window.location.href = '/master/AddCategory';



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



        }

    });
    });

});


  