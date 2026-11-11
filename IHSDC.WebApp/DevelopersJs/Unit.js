
    let messaget = '@ViewBag.ButtonName';

    $('#ComdIdUnit').change(function () {

        alert($('#ComdIdUnit').val());
    $('#CorpId').val(1);
    $('#BdeId').val(1);

    BindCorpUnit($("#ComdIdUnit").val());
    BindBde($("#ComdIdUnit").val(), $("#CorpId").val());
    BindUnitUnit($("#ComdIdUnit").val(), $("#CorpId").val(), $("#BdeId").val());
    });


    $('#spnCorpUnit').change(function () {

        $("#CorpId").val($('#spnCorpUnit').val());
    BindBde($("#ComdIdUnit").val(), $("#CorpId").val());
    BindUnitUnit($("#ComdIdUnit").val(), $("#CorpId").val(), $("#BdeId").val());
    });

    $('#spnBde').change(function () {
        $("#BdeId").val($('#spnBde').val());
    BindUnitUnit($("#ComdIdUnit").val(), $("#CorpId").val(), $("#BdeId").val());
    });

    $('#spnUnit').change(function () {
        $("#Unit_ID").val($('#spnUnit').val());
    });

    function goBack() {
        if (window.history.length > 1) {
        window.history.back();
        } else {
        window.location.href = '@Url.Action("SignUp", "Account")';


        }
    }
    $(document).on("click", ".js-add-appt", function (event) {
        AddConfirmation(this, '/SignUp/AddUnit', 'AddUnit', event);
    });
    function AddConfirmationBranch(obj, ActionName, Submit) {
        event.preventDefault();
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

    let message = '@message';
    let messageStatus = '@messageStatus';
    let messagemidStatus = '@messagemidStatus ';
    if (message)
    sweetAlert(message, messagemidStatus, messageStatus);
