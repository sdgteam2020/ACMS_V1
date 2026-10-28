




    $(document).ready(function () {
        $('.dropdownsearch').select2();
    });

    $('.Closepoup').on('click', function (e) {
        let modal = document.getElementById("DocuUpload");
        modal.style.display = "none";
    });
    $('.CloseLogpoup').on('click', function (e) {
        let modal = document.getElementById("DocuLog");
        modal.style.display = "none";
    });

    $('.DocuUpload').on('click', function (e) {

        $('#spnPolicyName').text($(this).closest("td").find('#spncardid').html());
        let filetext = window.btoa($('#spnPolicyName').html());
        $.ajax({
            url: '/Master/GetFiles',
            type: 'GET',
            data: { FName: filetext },
            success: function (data) {
                $("#tbody").html(data);
                $('#PolicyFolder').val($('#spnPolicyName').html());
                let modal = document.getElementById("DocuUpload");
                modal.style.display = "block";
            },
            error: function () {
                $("#tbody").html("");
                //let modal = document.getElementById("DocuUpload");
                //modal.style.display = "none";
            }
        });
    });

    $('.DocuLog').on('click', function (e) {
        $('#spnLogPolicyName').text($(this).closest("td").find('#spnLogcardid').html());
        let Pid = window.btoa($(this).closest("td").find('#spnLogPolicyid').html());
        $.ajax({
            url: '/Master/GetPolicyLog',
            type: 'GET',
            data: { Id: Pid },
            success: function (data) {
                DataTable.ext.search.push(function (settings, data, dataIndex) {
                    let min = minDate.val();
                    let max = maxDate.val();
                    let date = new Date(data[4]);
                    if (
                        (min === '' && max === '') ||
                        (min === '' && date <= max) ||
                        (min <= date && max === '') ||
                        (min <= date && date < adjustMaxDate(max)) // adjustMaxDate function excludes the maximum date
                    ) {
                        return true;
                    }
                    return false;
                });

                minDate = new DateTime('#min', {
                    format: 'DD-MM-YYYY'
                });
                maxDate = new DateTime('#max', {
                    format: 'DD-MM-YYYY'
                });

                $('#DownloadLog').DataTable().destroy();
                $("#tLogbody").html(data);
                let modal = document.getElementById("DocuLog");
                modal.style.display = "block";

                $('#DownloadLog').DataTable({
                    scrollY: "40vh",
                    scrollCollapse: true,
                    scroller: true,
                    responsive: true,

                });

                $('#min, #max').on('change keyup', function () {
                    $('#DownloadLog').DataTable().draw();
                });
            },
            error: function () {
                $("#tLogbody").html("");
            }
        });
    });


    function adjustMaxDate(max) {

        if (max !== '') {
            let adjustedMaxDate = new Date(max);
            adjustedMaxDate.setDate(adjustedMaxDate.getDate() + 1); // Add one day
            // alert(adjustedMaxDate);
            return adjustedMaxDate;

        }
        return null;
    }




    $(document).ready(function () {
        let currentDate = new Date();
        let day = currentDate.getDate();
        let month = currentDate.getMonth() + 1;
        let year = currentDate.getFullYear();
        let formattedDate = (day < 10 ? '0' : '') + day + '-' + (month < 10 ? '0' : '') + month + '-' + year;
        $('.date-input').val(formattedDate); // Set current date as default
    });



$(document).ready(function () {
    $("#files").on("change", function () {
        checkPDFHeader(this);
    });
});

    function checkPDFHeader(input) {
        const files = input.files;
        for (let i = 0; i < files.length; i++) {
            const file = files[i];
            const reader = new FileReader();
            reader.onloadend = function (e) {
                if (e.target.readyState === FileReader.DONE) {
                    const arr = new Uint8Array(e.target.result).subarray(0, 4);
                    const header = Array.from(arr).map(byte => byte.toString(16)).join("").toUpperCase();

                    if (header === "25504446") {

                    } else {
                        sweetAlert("Please select a valid PDF file.");
                        input.value = "";
                    }
                }
            };
        }
    }

        let messaget = '@ViewBag.ButtonName';

    $(".OrderbyId").off("click").click(function () {
        let orderby = $(this).closest("td").find(".spnOrderbyId").html();
        updateorder(orderby);

    });
    function updateorder(orderby) {

        $.ajax({
            url: '/Master/PolicyOrderby',
            type: 'POST',
            data: { "OrderbyId": orderby },
            success: function (result) {


                if (result.length > 0) {

                    swal("Successfully!", "Update Order Successfully", "success");
                    location.reload();

                }

            }
        });



    }

    function AddConfirmation(obj, ActionName, Submit) {
        event.preventDefault();

        $('.error-label').remove();

        let emptyFields = [];
        $('form').find(':input[required]').each(function () {
            if ($.trim($(this).val()) === '') {
                emptyFields.push(this);
            }
        });

        if (emptyFields.length > 0) {
            let firstEmptyField = emptyFields[0];
            $(firstEmptyField).after('<label class="error-label">This field is required</label>');
            return;
        }

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

$(".saveAddPolicyConfirm").on("click", function (event) {

    event.preventDefault();

    // Remove any existing error labels
    $('.error-label').remove();

    // Check if all required form fields are filled in
    let emptyFields = [];
    $('form').find(':input[required]').each(function () {
        if ($.trim($(this).val()) === '') {
            emptyFields.push(this);
        }
    });

    // If there are empty fields, display an error message for the first empty field
    if (emptyFields.length > 0) {
        let firstEmptyField = emptyFields[0];
        $(firstEmptyField).after('<label class="error-label" style="color: red;">This field is required</label>');
        return;
    }

   

    const payload = {
      
        PolicyName: $("#PolicyName").val(),
        TypeOfDocu: $("#TypeOfDocu").val(),
        PolicyRemarks: $("#PolicyRemarks").val(),
        PolicyId: $("#PolicyId").val() || ""
    };

    let jsonData = JSON.stringify(payload);

    let encrypted = encryptPayloadData(jsonData);

    let id = $("#id").val();
    let oldid = $("#oldid").val();

    // Get Add / Update directly from clicked button
    let buttonName = $(this).val();

    console.log("Payload:", payload);
    console.log("JSON:", jsonData);
    console.log("Encrypted:", encrypted);
    console.log("ID:", id);
    console.log("Button:", buttonName);

    swal({
        title: "Are you sure?",
        text: "",
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: "#DD6B55",
        confirmButtonText: buttonName === "Add" ? "Yes, Add" : "Yes, Update",
        cancelButtonText: "Cancel",
        closeOnConfirm: false,
        closeOnCancel: true
    },
        function (isConfirm) {

            if (isConfirm) {


                $.ajax({
                    url: '/Master/AddPolicy',
                    type: 'POST',

                    data: {
                        Request: encrypted,
                        //id: id,
                        btnval: buttonName,
                        oldid: oldid,

                        // Required because controller has
                        // [ValidateAntiForgeryToken]
                        __RequestVerificationToken:
                            $('input[name="__RequestVerificationToken"]').val()
                    },

                    success: function (response) {

                        console.log(response);

                        swal({
                            title: "",
                            text: buttonName === "Add" ? "Add successfully" : "Update successfully",
                            type: "success",
                            confirmButtonColor: "#DD6B55",
                            confirmButtonText: "OK",
                            closeOnConfirm: true
                        },
                            function () {

                                // Reload page after success
                                window.location.href = '/Master/AddPolicy';

                            });
                    },

                    error: function (xhr) {

                        console.log("Status:", xhr.status);
                        console.log("Response:", xhr.responseText);

                        swal({
                            title: "Update Failed!",
                            text: xhr.responseText || "",
                            type: "error",
                            confirmButtonColor: "#DD6B55"
                        });
                    }
                });
            }
        });
});