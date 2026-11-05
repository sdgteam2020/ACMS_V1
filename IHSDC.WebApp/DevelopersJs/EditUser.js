
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
        $('.username-input').on('input', function () {
            validateInput(this);
        });
    });

    $(document).ready(function () {



       
        $('#spnUnit_ID').change(function () {

            $("#Unit_ID").val($('#spnUnit_ID').val());
        });

        $('#UserTypeId').change(function () {

           
        });
        var unit_id = '@ViewBag.sel_unit_id';
        var roles_id = '@ViewBag.sel_roles_id';
        var user_id = '@ViewBag.UserId';
        var pass = '@ViewBag.pass';
        $("#Unit_ID").val(unit_id);

        $(".Roles").val(roles_id);
        $("#Password").val(pass);
        $("#UserId").val(user_id);

        $('#ComdId').val(@ViewBag.ComdId);
        $('#CorpId').val(@ViewBag.CorpId);
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
    var dropdown = document.getElementById("rolesDropdown");
    dropdown.addEventListener("keydown", function (event) {
        event.preventDefault();
    });

             var message = '@message';
    var messageStatus = '@messageStatus';
    var messagemidStatus = '@messagemidStatus';
    if (message)
            sweetAlert(message, messagemidStatus, messageStatus);

