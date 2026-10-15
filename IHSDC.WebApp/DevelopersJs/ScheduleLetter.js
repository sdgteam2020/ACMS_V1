
            $("body").addClass("loading");
            $(document).ready(function () {
                let table = $('#Scheduleletter').DataTable({
                    lengthChange: true,
                    pageLength: 50,
                    aLengthMenu: [[25, 50, 100, 150, 200, 250, -1], [25, 50, 100, 150, 200, 250, "All"]],

                });
                let buttonContainer = table.buttons().container().appendTo('#Scheduleletter_wrapper .col-md-6:eq(0)');
                let searchBuilderContainer = table.searchBuilder.container();
                buttonContainer.insertBefore(table.table().container());
                searchBuilderContainer.insertBefore(table.table().container());
            });
        
        
            $(function () {
                $('#ddlOrganizerCat').multiselect({
                    includeSelectAllOption: true,
                    selectAllValue: 'multiselect-all',
                    enableFiltering: true,
                    enableCaseInsensitiveFiltering: true,
                    maxHeight: '300',
                    buttonWidth: '230',
                });

                $('#ddlSponsorCat').multiselect({
                    includeSelectAllOption: true,
                    selectAllValue: 'multiselect-all',
                    enableFiltering: true,
                    enableCaseInsensitiveFiltering: true,
                    maxHeight: '300',
                    buttonWidth: '230',
                });

                $('#ddlNodalCat').multiselect({
                    includeSelectAllOption: true,
                    selectAllValue: 'multiselect-all',
                    enableFiltering: true,
                    enableCaseInsensitiveFiltering: true,
                    maxHeight: '300',
                    buttonWidth: '230',
                });

                $('#ddlBranchIds').multiselect({
                    includeSelectAllOption: true,
                    selectAllValue: 'multiselect-all',
                    enableFiltering: true,
                    enableCaseInsensitiveFiltering: true,
                    maxHeight: '300',
                    buttonWidth: '230',
                });

                $('#ddlParticipateUsers').multiselect({
                    includeSelectAllOption: true,
                    selectAllValue: 'multiselect-all',
                    enableFiltering: true,
                    enableCaseInsensitiveFiltering: true,
                    maxHeight: '300',
                    buttonWidth: '230',
                });

                $('#ddlParticipateNewUsers').multiselect({
                    includeSelectAllOption: true,
                    selectAllValue: 'multiselect-all',
                    enableFiltering: true,
                    enableCaseInsensitiveFiltering: true,
                    maxHeight: '300',
                    buttonWidth: '230',
                });
            });

            $("#ddlOrganizerCat").change(function () {
                let selected = $("#ddlOrganizerCat option:selected");
                let OrganizerCat = "";
                let arrSelected = [];
                selected.each(function () {
                    arrSelected.push($(this).val());
                    OrganizerCat += $(this).val() + ",";
                });
                OrganizerCat = OrganizerCat.slice(0, -1);
                $('#OrganizerCatId').val(OrganizerCat);
            });

            $("#ddlSponsorCat").change(function () {
                let selected = $("#ddlSponsorCat option:selected");
                let SponsorCat = "";
                let arrSelected = [];
                selected.each(function () {
                    arrSelected.push($(this).val());
                    SponsorCat += $(this).val() + ",";
                });
                SponsorCat = SponsorCat.slice(0, -1);
                $('#SponsorCatId').val(SponsorCat);
            });

            $("#ddlNodalCat").change(function () {
                let selected = $("#ddlNodalCat option:selected");
                let NodalCat = "";
                let arrSelected = [];
                selected.each(function () {
                    arrSelected.push($(this).val());
                    NodalCat += $(this).val() + ",";
                });
                NodalCat = NodalCat.slice(0, -1);
                $('#NodalCatId').val(NodalCat);
            });

            $("#ddlBranchIds").change(function () {
                let selected = $("#ddlBranchIds option:selected");
                let BranchId = "";
                let arrSelected = [];
                selected.each(function () {
                    arrSelected.push($(this).val());
                    BranchId += $(this).val() + ",";
                });
                BranchId = BranchId.slice(0, -1);
                $('#BranchIds').val(BranchId);
            });

            $("#ddlParticipateUsers").change(function () {
                let selected = $("#ddlParticipateUsers option:selected");
                let ParticipateUser = "";
                let arrSelected = [];
                selected.each(function () {
                    arrSelected.push($(this).val());
                    ParticipateUser += $(this).val() + ",";
                });
                ParticipateUse = ParticipateUser.slice(0, -1);
                $('#ParticipateUsers').val(ParticipateUser);
            });

        $("#ddlParticipateNewUsers").change(function () {
            let selected = $("#ddlParticipateNewUsers option:selected");
            let ParticipateUser = "";
            let arrSelected = [];
            selected.each(function () {
                arrSelected.push($(this).val());
                ParticipateUser += $(this).val() + ",";
            });
            ParticipateUse = ParticipateUser.slice(0, -1);
            $('#ParticipateNewUsers').val(ParticipateUser);
        });

            $("#chkOrganizer").on("click", function () {
                let isChecked = $(this).prop("checked");
                if (isChecked) {
                    $("#OrgId").show();
                } else {
                    $("#OrgId").hide();
                }
            });

            $("#chkSponsor").on("click", function () {
                let isChecked = $(this).prop("checked");
                if (isChecked) {
                    $("#SponId").show();
                } else {
                    $("#SponId").hide();
                }
            });

            $("#chkNodal").on("click", function () {
                let isChecked = $(this).prop("checked");
                if (isChecked) {
                    $("#NodeId").show();
                } else {
                    $("#NodeId").hide();
                }
            });
            $("#chkBranch").on("click", function () {
                let isChecked = $(this).prop("checked");
                if (isChecked) {
                    $("#BrId").show();
                } else {
                    $("#BrId").hide();
                }
            });

        $("#chkParticipateUsers").on("click", function () {

                let isChecked = $(this).prop("checked");
                if (isChecked) {
                    $("#PUserId").show();
                } else {
                    $("#PUserId").hide();
                }
            });

        $("#chkParticipateNewUsers").on("click", function () {

                   let isChecked = $(this).prop("checked");
                   if (isChecked) {
                       $("#PNewUserId").show();
                   } else {
                       $("#PNewUserId").hide();
                   }
               });


                $(document).ready(function () {
                    let organizerCatId = "@Model.OrganizerCatId";
                    if (organizerCatId !== "") {
                        $("#OrgId").show();
                    } else {
                        $("#OrgId").hide();
                    }
                    let SponsorCatId = "@Model.SponsorCatId";
                    if (SponsorCatId !== "") {
                        $("#SponId").show();
                    } else {
                        $("#SponId").hide();
                    }
                    let NodalCatId = "@Model.NodalCatId";
                    if (NodalCatId !== "") {
                        $("#NodeId").show();
                    } else {
                        $("#NodeId").hide();
                    }
                    let BranchIds = "@Model.BranchIds";
                    if (BranchIds !== "") {
                        $("#BrId").show();
                    } else {
                        $("#BrId").hide();
                    }
                    let PUser = "@Model.ParticipateUsers";
                    if (PUser !== "") {
                        $("#PUserId").show();
                    } else {
                        $("#PUserId").hide();
                    }

                    let PNewUser = "@Model.ParticipateNewUsers";
                    if (PNewUser !== "") {
                        $("#PNewUserId").show();
                    } else {
                        $("#PNewUserId").hide();
                    }

                    let isOrgChecked = $("#chkOrganizer").prop("checked");
                    if (isOrgChecked) {
                        $("#OrgId").show();
                    } else {
                        $("#OrgId").hide();
                    }
                    let isSponChecked = $("#chkSponsor").prop("checked");
                    if (isSponChecked) {
                        $("#SponId").show();
                    } else {
                        $("#SponId").hide();
                    }
                    let isNodeChecked = $("#chkNodal").prop("checked");
                    if (isNodeChecked) {
                        $("#NodeId").show();
                    } else {
                        $("#NodeId").hide();
                    }
                    let isBranchChecked = $("#chkBranch").prop("checked");
                    if (isBranchChecked) {
                        $("#BrId").show();
                    } else {
                        $("#BrId").hide();
                    }
                    let isPUserChecked = $("#chkParticipateUsers").prop("checked");
                    debugger;
                    if (isPUserChecked) {
                        $("#PUserId").show();
                    } else {
                        $("#PUserId").hide();
                    }

                    let isPNewUserChecked = $("#chkParticipateNewUsers").prop("checked");
                    debugger;
                    if (isPNewUserChecked) {
                        $("#PNewUserId").show();
                    } else {
                        $("#PNewUserId").hide();
                    }

                });
               $(document).ready(function () {
                   $('.dropdownsearch').select2();
               });


$(document).on("click", ".saveAddScheduleLetter", function (event) {

    event.preventDefault();
    debugger;
 
    // Get encrypted ID exactly as it exists
    let id = $("#id").val() || "";
    let oldids = $("#oldid").val() || "";
    // Get Add / Update
    let buttonName = $(this).val() || "";

    // Create payload
    const payload = {

        TypeOfConfId: $("#TypeOfConfId").val() || "",
        FromDate: $("#FromDate").val() || "",
        ToDate: $("#ToDate").val() || "",
        Subject: $("#Subject").val() || "",
        ScheduleRefNoDt: $("#ScheduleRefNoDt").val() || "",
        Remark: $("#Remark").val() || "",
        ScheduleId: $("#ScheduleId").val() || "",
      
        OrganizerCatId:
            ($("#ddlOrganizerCat").val() || []).join(","),

        SponsorCatId:
            ($("#ddlSponsorCat").val() || []).join(","),

        NodalCatId:
            ($("#ddlNodalCat").val() || []).join(","),

        BranchIds:
            ($("#ddlBranchIds").val() || []).join(","),

        ParticipateUsers:
            ($("#ddlParticipateUsers").val() || []).join(","),

        ParticipateNewUsers:
            ($("#ddlParticipateNewUsers").val() || []).join(",")

    };


    console.log("Payload:", payload);
    console.log("Encrypted ID:", id);
    console.log("Button:", buttonName);


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

                url: '/Schedule/ScheduleLetter',

                type: 'POST',

                data: {

                    Request: encrypted,

                    btnval: buttonName,
                    oldid: oldids,
                  
                    __RequestVerificationToken: token
                },


                success: function (response) {

                    console.log("Server Response:", response);


                    // If controller returns success false
                    if (
                        response &&
                        response.success === false
                    ) {

                        swal({

                            title: "Failed!",

                            text:
                                response.message ||
                                "Unable to save Corps.",

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
                                '/Schedule/ScheduleLetter';

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
       