
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

$(document).ready(function () {
    $("#apptForm").on("submit", async function (e) {

        e.preventDefault();
        let $form = $("#apptForm");
        if (!$form.valid()) {

            return false;
        }
        let id = $("#id").val() || "";
        let btnval = $("#btnval").val();
        let buttonName = $(".saveAddApptConfirm").val() || "";
        let Appt = $("#Appt").val();
        let ApptAbbreviation = $("#ApptAbbreviation").val();

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
                    url: '/Master/AddAppt',
                    type: 'POST',

                    data: {
                        __RequestVerificationToken:
                            $('#apptForm input[name="__RequestVerificationToken"]').val(),

                        id: id,
                        Appt: Appt,
                        ApptAbbreviation: ApptAbbreviation,
                        btnval: btnval
                    },

                    success: function (response) {

                        console.log(response);

                        // SERVER-SIDE VALIDATION ERROR
                        if (response && response.success === false) {

                            let firstError = Object.values(response.errors || {})
                                .flat()[0] || response.message || "";

                            $("#ApptNameValidation")
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

                            window.location.href = '/master/AddAppt';



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


