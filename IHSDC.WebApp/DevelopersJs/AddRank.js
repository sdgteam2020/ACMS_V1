$(document).ready(function () {
    $("#rankForm").on("submit", async function (e) {
        debugger;
        e.preventDefault();
        let $form = $("#rankForm");
        if (!$form.valid()) {

            return false;
        }
        let id = $("#id").val() || "";
     
      
        let btnval = $("#btnval").val();
        let buttonName = $(".saveAddRankConfirm").val() || "";
        let RankName = $("#RankName").val();
        let RankAbbreviation = $("#RankAbbreviation").val();
       
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
                url: '/Master/AddRank',
                type: 'POST',

                data: {
                    __RequestVerificationToken:
                        $('#rankForm input[name="__RequestVerificationToken"]').val(),

                    id: id,
                    RankName: RankName,
                    RankAbbreviation: RankAbbreviation,
                    btnval: btnval
                },

                success: function (response) {

                    console.log(response);

                    // SERVER-SIDE VALIDATION ERROR
                    if (response && response.success === false) {

                        let firstError = Object.values(response.errors || {})
                            .flat()[0] || response.message || "";

                        $("#RankNameValidation")
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

                        window.location.href = '/master/AddRank';



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



