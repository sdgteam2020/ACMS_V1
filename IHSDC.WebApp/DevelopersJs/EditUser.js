
function validateInput(input) {
    let regex = /[^0-9]/g;
    input.value = input.value.replace(regex, "");
}
function ValInData(input) {
    let regex = /[^a-zA-Z0-9 ]/g;
    input.value = input.value.replace(regex, "");
}

function ValInDataReg(input) {
    let regex = /[^a-zA-Z0-9]/g;
    input.value = input.value.replace(regex, "");
}

$(document).ready(function () {
    $(document).on("keyup", ".username-input", function () {
        ValInData(this);
    });
});
$(document).ready(function () {
    $('.username-mob-input').on('input', function () {
        validateInput(this);
    });
});

    $(document).ready(function () {
        debugger;
        $('#spnUnit_ID').change(function () {

            $("#Unit_ID").val($('#spnUnit_ID').val());
        });

        $('#UserTypeId').change(function () {
        });

            var ComdId = $('#hdnComdId').val();
            var CorpId = $('#hdnCorpId').val();

            var unit_id = $('#hdn_unit_id').val();
            var roles_id = $('#hdn_roles_id').val();
            var user_id = $('#hdn_user_id').val();

       
        
        $("#Unit_ID").val(unit_id);

        $(".Roles").val(roles_id);
       
        $("#UserId").val(user_id);

        $('#ComdId').val(ComdId);
        $('#CorpId').val(CorpId);
        BindCorp($("#ComdId").val());
        BindBde($("#ComdId").val(), $("#CorpId").val());
        BindUnit($("#ComdId").val(), $("#CorpId").val(), $("#BdeId").val());



    });
    


    $('#ComdId').change(function () {
        $('#CorpId').val(1);
        $('#BdeId').val(1);

        BindCorp($("#ComdId").val());
        BindBde($("#ComdId").val(), $("#CorpId").val());
        BindUnit($("#ComdId").val(), $("#CorpId").val(), $("#BdeId").val());
    });


    $('#spnCorp').change(function () {
        $("#CorpId").val($('#spnCorp').val());
        BindBde($("#ComdId").val(), $("#CorpId").val());
        BindUnit($("#ComdId").val(), $("#CorpId").val(), $("#BdeId").val());
    });

    $('#spnBde').change(function () {
        $("#BdeId").val($('#spnBde').val());
        BindUnit($("#ComdId").val(), $("#CorpId").val(), $("#BdeId").val());
    });

    $('#spnUnit').change(function () {
        $("#Unit_ID").val($('#spnUnit').val());
    });
    let dropdown = document.getElementById("rolesDropdown");
  
$(document).ready(function () {
    $("#edituserForm").on("submit", async function (e) {   
        debugger;
        e.preventDefault();
       
        let $form = $("#edituserForm");
        if (!$form.valid()) {

            return false;
        }
        let id = $("#userID").val() || "";
        var role = $("#rolesDropdown").val();
     

        let buttonName = $(".saveUpdateConfirm").val() || "";

        const payload = {

            Username: $("#Username").val() || "",
            UserId: $("#userID").html() || "",
            Password: $("#Password").val() || "",
            ConfirmPassword: $("#ConfirmPassword").val() || "",
            Rank: $("#Rank").val() || "",
            Number: $("#Number").val() || "",
            FullName: $("#FullName").val() || "",
            ComdId: $("#ComdId").val() || "",
            CorpId: $("#CorpId").val() || "",
            BdeId: $("#BdeId").val() || "",
            Appointment: $("#Appointment").val() || "",
            Roles: [role],
            ASCON: $("#ASCON").val() || "",
            Active: $("#Active").val() || "",
            MobNo: $("#MobNo").val() || "",
            Unit_ID: $("#Unit_ID").val() || "",

        };
        



        let jsonData = JSON.stringify(payload);

        let encrypted;

        try {

            encrypted = encryptPayloadData(jsonData);

        }
        catch (error) {



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
                    : "",

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

                    url: '/Account/Edit',

                    type: 'POST',

                    data: {

                        Request: encrypted,
                        btnval: buttonName,
                        __RequestVerificationToken: token
                    },


                    success: function (response) {

                        console.log(response);

                        // SERVER-SIDE VALIDATION ERROR
                        if (response && response.success === false) {

                            let firstError = Object.values(response.errors || {})
                                .flat()[0] || response.message || "";

                            $("#editnameValidation")
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
                                buttonName === "Update"
                                    ? "User Updated Successfully!"
                                    : "User Updated Successfully!",

                            type: "success",

                            confirmButtonColor: "#DD6B55",

                            confirmButtonText: "OK",

                            closeOnConfirm: true

                        },
                            function () {

                                window.location.href =
                                    '/Account/Hierarchy';

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
                                buttonName === "Update"
                                    ? ""
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

