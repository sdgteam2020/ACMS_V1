
    let messaget = '@ViewBag.ButtonName';

    $('#spnCorpsId').change(function () {
        $("#CorpsId").val($('#spnCorpsId').val());
    });



$(document).ready(function () {
    $("#unitForm").on("submit", async function (e) {
        e.preventDefault();
        let $form = $("#unitForm");
        if (!$form.valid()) {

            return false;
        }
    
    let id = $("#id").val() || "";

    let buttonName = $(".saveAddUnitConfirm").val() || "";
    
    // Create payload
    const payload = {

        UnitName: $("#UnitName").val() || "",
        ComdId: $("#ComdId").val() || "",
        CorpsId: $("#CorpsId").val() || "",
        Unit_ID: $("#UnitId").val() || "",     
    };

    let jsonData = JSON.stringify(payload);

    let encrypted;

    try {

        encrypted = encryptPayloadData(jsonData);

    }
    catch (error) {

        console.error("Encryption Error:", error);

        swal({
            title: "Encryption Failed!",
            text: error.message || "Unable to encrypt request.",
            type: "error",
            confirmButtonColor: "#DD6B55"
        });

        return false;
    }


    if (!encrypted) {

        console.error("Encrypted request is empty.");

        swal({
            title: "Error",
            text: "Unable to create encrypted request.",
            type: "error",
            confirmButtonColor: "#DD6B55"
        });

        return false;
    }


    swal({

        title: "Are you sure?",

        text: "",

        type: "warning",

        showCancelButton: true,

        confirmButtonColor: "#DD6B55",

        confirmButtonText:
            buttonName === "Add"
                ? "Yes, Add"
                : "Yes, Update",

        cancelButtonText: "Cancel",

        closeOnConfirm: false,

        closeOnCancel: true

    },
        function (isConfirm) {

            if (!isConfirm) {
                return;
            }


            let token =
                $('input[name="__RequestVerificationToken"]').val();


            console.log("Request:", encrypted);
            console.log("ID:", id);
            console.log("btnval:", buttonName);
            console.log("Token:", token);


            $.ajax({

                url: '/Master/AddUnit',

                type: 'POST',

                data: {

                    Request: encrypted,

              

                    btnval: buttonName,

                    __RequestVerificationToken: token
                },


                success: function (response) {

                    console.log("Server Response:", response);
                    // SERVER-SIDE VALIDATION ERROR
                    if (response && response.success === false) {

                        let firstError = Object.values(response.errors || {})
                            .flat()[0] || response.message || "";

                        $("#UnitNameValidation")
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

                    },
                        function () {

                            window.location.href =
                                '/Master/AddUnit';

                        });

                },


                error: function (xhr) {

                    console.error(
                        "Status:",
                        xhr.status
                    );

                    console.error(
                        "Response:",
                        xhr.responseText
                    );


                    swal({

                        title:
                            buttonName === "Add"
                                ? "Add Failed!"
                                : "Update Failed!",

                        text:
                            xhr.responseText ||
                            "Something went wrong.",

                        type: "error",

                        confirmButtonColor: "#DD6B55"

                    });

                }

            });

        });


    return false;
    });
});

