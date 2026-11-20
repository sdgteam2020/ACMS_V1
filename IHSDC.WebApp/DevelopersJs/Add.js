
    function validateInput(input) {
        let regex = /[^0-9]/g;
        input.value = input.value.replace(regex, "");
    }
    function ValInData(input) {
        let regex = /[^a-zA-Z0-9\s\-()._ ]/g;
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

                        $('#spnUnit_ID').change(function () {

                            $("#Unit_ID").val($('#spnUnit_ID').val());
                        });

                        $('#UserTypeId').change(function () {
                            Bindunit($("#UserTypeId").val());

                        });

                        $('#Username').change(function () {
                            $("#Password").val('Admin123!')
                            $("#ConfirmPassword").val('Admin123!')
                        });

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

    $("#adduserForm").on("submit", function (e) {
        debugger;
        e.preventDefault();

        let $form = $(this);

        if (!$form.valid()) {
            return false;
        }

        let buttonName = $(".saveAddConfirm").val() || "Add";

        let role = $("#rolesDropdown").val();

        // RegisterViewModel properties
        let payload = {

            Username: $("#Username").val(),
            Password: $("#Password").val(),
            ConfirmPassword: $("#ConfirmPassword").val(),

            Rank: $("#Rank").val(),
            Number: $("#Number").val(),
            FullName: $("#FullName").val(),

            ComdId: $("#ComdId").val(),
            CorpId: $("#CorpId").val(),
            BdeId: $("#BdeId").val(),

            Appointment: $("#Appointment").val(),

            Roles: role ? [role] : [],

            ASCON: $("#ASCON").val(),
            Active: $("#Active").val(),

            MobNo: $("#MobNo").val(),
            Unit_ID: $("#Unit_ID").val()

        };

        // AntiForgeryToken
        let token = $('input[name="__RequestVerificationToken"]').val();

        swal({
            title: "Are you sure?",
            text: "",
            type: "warning",
            showCancelButton: true,
            confirmButtonColor: "#DD6B55",
            confirmButtonText: buttonName === "Add"
                ? "Yes, Add"
                : "Yes, Update",
            cancelButtonText: "Cancel",
            closeOnConfirm: false,
            closeOnCancel: true

        }, function (isConfirm) {

            if (!isConfirm) {
                return;
            }

            $.ajax({

                url: '/Account/Add',
                type: 'POST',

                data: $.extend({}, payload, {
                    __RequestVerificationToken: token
                }),

                success: function (response) {

                    console.log("Server Response:", response);

                    if (response.success === false) {

                        let firstError =
                            Object.values(response.errors || {}).flat()[0]
                            || response.message
                            || "Validation failed.";

                        $("#addnameValidation")
                            .text(firstError)
                            .removeClass("d-none");

                        swal({
                            title: "Failed!",
                            text: firstError,
                            type: "error"
                        });

                        return;
                    }

                    swal({
                        title: "",
                        text: buttonName === "Add"
                            ? "User Created Successfully!"
                            : "",
                        type: "success",
                        confirmButtonColor: "#DD6B55",
                        confirmButtonText: "OK"
                    }, function () {

                        window.location.href = '/Account/Hierarchy';

                    });

                },

                error: function (xhr, status, error) {

                    console.log("HTTP Status:", xhr.status);
                    console.log("Error:", error);
                    console.log("Response:", xhr.responseText);

                    swal({
                        title: "Request Failed!",
                        text: xhr.responseText || error,
                        type: "error"
                    });

                }

            });

        });

        return false;

    });

});



                   

