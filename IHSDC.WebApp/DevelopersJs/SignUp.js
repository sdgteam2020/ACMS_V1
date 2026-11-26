
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
            $('#ComdId').val("");
        $("#chk").click(function () {
            if ($(this).prop("checked")) {
                $("#pass").attr("type", "text");
            } else {
                $("#pass").attr("type", "password");
            }
        });


        $("#pass").val("");

        $('#Username').change(function () {
            $("#Password").val('Admin123!');
            $("#ConfirmPassword").val('Admin123!');
        });

        $('#setComdId, #setUnit').on('click', function () {


            $('#CorpId').val(1);
            $('#BdeId').val(1);

            BindBde($("#ComdId").val(), $("#CorpId").val());
            BindUnit($("#ComdId").val(), $("#CorpId").val(), $("#BdeId").val());
        });

        $('#ComdId').change(function () {
            $('#CorpId').val(1);
            $('#BdeId').val(1);
            let value = $(this).val();
            if (value) {
                BindCorp(value);
            }

            BindBde($(this).val(), $('#CorpId').val());
            BindUnit($(this).val(), $('#CorpId').val(), $('#BdeId').val());
        });


        $('#ComdId').change(function () {
            $('#CorpId').val(1);
            $('#BdeId').val(1);

            BindCorp($(this).val());
            BindBde($(this).val(), $('#CorpId').val());
            BindUnit($(this).val(), $('#CorpId').val(), $('#BdeId').val());
        });




    $('#spnCorpUnit').change(function () {
       
            $("#CorpIdNew").val($('#spnCorpUnit').val());
            BindBde($("#ComdId").val(), $("#CorpId").val());
            BindUnitUnit($("#ComdIdUnit").val(), $("#CorpIdNew").val(), $("#BdeId").val());
        });


        $('#spnCorp').change(function () {
            $("#CorpId").val($(this).val());
            BindBde($("#ComdId").val(), $(this).val());
            BindUnit($("#ComdId").val(), $(this).val(), $("#BdeId").val());
        });

        $('#spnBde').change(function () {
            $("#BdeId").val($(this).val());
            BindUnit($("#ComdId").val(), $("#CorpId").val(), $(this).val());
        });

        $('#spnUnit').change(function () {
            $("#Unit_ID").val($(this).val());
        });

        



    document.getElementById('addbranch').addEventListener('click', function (e) {
           e.preventDefault();
           debugger;

        
        const url ="/SignUp/AddUnitNew";

       fetch(url)
        .then(response => response.json())
        .then(result => {
            if (result.ilUnitCrud.length > 0) {
                let htmlContent = `
                    <div class="signup-style-19">
                        <table class="table table-striped">
                            <thead class="bg-info text-white">
                                <tr>
                                    <th>S/No</th>
                                    <th>Branch Name</th>
                                    <th>Corps</th>
                                    <th>Command</th>
                                </tr>
                            </thead>
                            <tbody>
                `;

                result.ilUnitCrud.forEach((item, index) => {
                    htmlContent += `
                        <tr>
                            <td>${index + 1}</td>
                            <td>${item.UnitName}</td>
                            <td>${item.Corps}</td>
                            <td>${item.Command}</td>
                        </tr>
                    `;
                });

                htmlContent += `
                            </tbody>
                        </table>
                    </div>
                `;

                document.querySelector('#addunitmodel .modal-body-list').innerHTML = htmlContent;



                $("#UnitName").val("");


                 $("#ComdIdUnit").val("");
                 $("#spnCorpUnit").val("");

                $('#addunitmodel').modal('show');

            } else {
                console.log('No data returned.');
            }
        })
        .catch(error => {
            console.error('Error loading AddUnit form:', error);
        });
    });



    document.getElementById('setAppt').addEventListener('click', function (e) {
        debugger;
        e.preventDefault();
       

        let url = this.getAttribute('data-url');

        fetch(url)
            .then(response => {
                if (!response.ok) {
                    throw new Error("Unable to load form.");
                }
                return response.text();
            })
            .then(html => {

                let modalBody = document.querySelector(
                    '#addApptModal .modal-body'
                );

                modalBody.innerHTML = html;

                let $form = $('#apptFormhoutside');

                // Initialize validation for dynamically loaded form
                if ($.validator && $.validator.unobtrusive) {
                    $form.removeData("validator");
                    $form.removeData("unobtrusiveValidation");

                    $.validator.unobtrusive.parse($form);
                }

                $('#addApptModal').modal('show');
            })
            .catch(error => console.error(
                'Error loading AddAppt form:', error
            ));
    });

        document.getElementById('addApptModal').addEventListener('hidden.bs.modal', function () {
            fetch('@Url.Action("ReloadAppointments", "SignUp")')
                .then(response => response.json())
                .then(data => {
                    let dropdown = document.getElementById('Appointment');
                    dropdown.innerHTML = '';
                    dropdown.insertAdjacentHTML('beforeend', '<option value="">--Select--</option>');
                    data.forEach(item => {
                        dropdown.insertAdjacentHTML('beforeend', `<option value="${item.Value}">${item.Text}</option>`);
                    });
                })
                .catch(error => console.error('Error loading appointments:', error));
        });


    document.getElementById('setComdId').addEventListener('click', function (e) {

        e.preventDefault();

        let url = this.getAttribute('data-url');

        fetch(url)
            .then(response => {
                if (!response.ok) {
                    throw new Error("Unable to load form.");
                }
                return response.text();
            })
            .then(html => {

                let modalBody = document.querySelector(
                    '#addDgsModal .modal-body'
                );

                modalBody.innerHTML = html;

                let $form = $('#corpsFormoutside');

                // Initialize validation for dynamically loaded form
                if ($.validator && $.validator.unobtrusive) {
                    $form.removeData("validator");
                    $form.removeData("unobtrusiveValidation");

                    $.validator.unobtrusive.parse($form);
                }

                $('#addDgsModal').modal('show');
            })
            .catch(error => console.error(
                'Error loading AddDgs form:', error
            ));
    });

        document.getElementById('addDgsModal').addEventListener('hidden.bs.modal', function () {

            let ComdIdSignUp = $("#ComdId").val();

            fetch('/SignUp/LoadCorpsNameByCommandId', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify({ "ComdIdSignUp": ComdIdSignUp })
            })
                .then(response => response.json())
                .then(result => {
                    let list = '<option value=1>-No DGs-</option>';

if (result.length > 0) {
    list = '';
    list += '<option value=1>-No DGs-</option>';
    result.forEach(item => {
        list += `<option value=${item.Value}>${item.Text}</option>`;
    });
}

document.getElementById('spnCorp').innerHTML = list;

if ($("#CorpId").val() != 0) {
    $('#spnCorp').val($("#CorpId").val());
} else {
    $("#CorpId").val("1");
}
                })
                .catch (error => {
    console.error('Error loading corps names:', error);
    document.getElementById('spnCorp').innerHTML = '<option value=1>-No DGs-</option>';
    $("#CorpId").val("1");
});
        });





document.querySelector('.info-container').addEventListener('mouseover', function () {
    const message = document.querySelector('.info-message');
    message.style.display = 'block';

});

document.querySelector('.info-container').addEventListener('mouseout', function () {
    const message = document.querySelector('.info-message');
    message.style.display = 'none';

});

document.querySelector('.info-container-role').addEventListener('mouseover', function () {
    const message = document.querySelector('.info-message-role');
    message.style.display = 'block';

});

document.querySelector('.info-container-role').addEventListener('mouseout', function () {
    const message = document.querySelector('.info-message-role');
    message.style.display = 'none';

});

});

$(document).on("click", ".js-add-appt", function (event) {
    debugger;
    let appt = $("#Appt").val().trim();
    let apptAbbreviation = $("#ApptAbbreviation").val().trim();

 
    AddConfirmation(this, '/SignUp/AddAppt', 'AddAppt', event);
});
function AddConfirmation(obj, ActionName, Submit, event) {
    event.preventDefault();
    let appt = $("#Appt").val().trim();
    let apptAbbreviation = $("#ApptAbbreviation").val().trim();

   

    let formData = new FormData($('form')[0]);
    $.ajax({
        url: '/SignUp/AddAppt',
      
        type: 'POST',
        data: formData,
        contentType: false,
        processData: false,
        success: function (response) {
            $("#Appt").val("");
            $("#ApptAbbreviation").val("");
            sweetAlert(response.message);
            $('#addApptModal').modal('hide');

            $.ajax({
                url: '/SignUp/ReloadAppointments',
             
                type: 'GET',
                success: function (data) {
                    let dropdown = $('#Appointment');
                    dropdown.empty();
                    dropdown.append($('<option></option>').val("").text("--Select--"));
                    $.each(data, function (index, item) {
                        dropdown.append($('<option></option>').val(item.Value).text(item.Text));
                    });
                    dropdown.val("");
                },
                error: function (xhr, textStatus, errorThrown) {
                    console.error('Error loading appointments:', textStatus, errorThrown);
                }
            });
        },
        error: function (xhr, status, error) {
            sweetAlert("Error adding appointment: " + response.message);
        }
    });
}
$(document).on("click", ".js-add-corps", function (event) {
    debugger;
    let ComdIdd = $("#ComdId").val().trim();
    let CorpsName = $("#CorpsName").val().trim();

    if (!ComdIdd) {
        sweetAlert("Comd/PSOs Name is required.");
        return;
    }
    if (!CorpsName) {
        sweetAlert("DGs Name is required.");
        return;
    }
    AddConfirmationDgs(this, '/SignUp/AddCorps', 'AddCorps', event);
});
function AddConfirmationDgs(obj, ActionName, Submit, event) {
    event.preventDefault();
    let ComdIdd = $("#ComdId").val().trim();
    let CorpsName = $("#CorpsName").val().trim();
    if (!ComdIdd) {
        sweetAlert("Comd/PSOs Name is required.");
        return;
    }
    if (!CorpsName) {
        sweetAlert("DGs Name is required.");
        return;
    }
    let formData = new FormData($('form')[0]);

    $.ajax({
        url: '@Url.Action("AddCorps", "SignUp")',
        type: 'POST',
        data: formData,
        contentType: false,
        processData: false,
        success: function (response) {
            sweetAlert(response.message);
            $('#ComdId').val('').trigger('change');
            $('#addDgsModal').modal('hide');
        },
        error: function (xhr, status, error) {
            sweetAlert("Error adding DGs: " + response.message);
        }
    });
}
function AddConfirmationBranch(obj, ActionName, Submit, event) {
    event.preventDefault();
    let UnitName = $("#UnitName").val();
    let ComdIdUnit = $("#ComdIdUnit").val();
    let spnCorpUnit = $("#spnCorpUnit").val();
    if (!ComdIdUnit) {
        sweetAlert("Comd/PSOs Name cannot be empty.");
        return;
    }
    if (!spnCorpUnit) {
        sweetAlert("DGs Name cannot be empty.");
        return;
    }
    if (!UnitName.trim()) {
        sweetAlert("Branch Name cannot be empty.");
        return;
    }

    let formData = new FormData($('form')[0]);

    formData.append("UnitName", UnitName);
    formData.append("ComdIdUnit", ComdIdUnit);
    formData.append("spnCorpUnit", spnCorpUnit);

    $.ajax({
       
        url: '/SignUp/AddUnitNew',
        type: 'POST',
        data: formData,
        contentType: false,
        processData: false,
        success: function (response) {


            sweetAlert(response.message);

            $('#ComdId').val(null).trigger('change');
            $('#UnitName').val('');
            $('#ComdIdUnit').val('');
            list = '';
            $('#spnCorpUnit').html(list);
            $("#spnCorpUnit").val("1");


            $('#addunitmodel').modal('hide');
        },
        error: function (xhr, status, error) {
            sweetAlert("Error adding Branch: " + xhr.responseText || "Unknown error");
        }
    });
}

$("#chk").click(function () {
            if ($(this).prop("checked")) {
                $("#pass").attr("type", "text");
            }
            else {

                $("#pass").attr("type", "password");
            }
        });
        $(document).ready(function () {
            $("#pass").val("");
        });
      

    $(document).ready(function () {
        $('.dropdownsearch').select2();

    });

    $(document).ready(function () {
        $('#Number').on('keyup', function () { ValInDataReg(this); });
        $('#FullName, #UnitName').on('keyup', function () { ValInData(this); });
       
        $('#addunitmodel button[name="btnval"]').on('click', function (event) {
          //  AddConfirmationBranch(this, '/SignUp/AddUnit', 'SignUp', event);
        });
        $('#btnNew').on('click', function () {
            location.href = '/Account/Login';       
          
        });
    });

document.getElementById('addbranch').addEventListener('click', function (e) {

    e.preventDefault();

    const url = '/SignUp/AddUnitNew';

    fetch(url)
        .then(response => {
            if (!response.ok) {
                throw new Error("Unable to load branch data.");
            }
            return response.json();
        })
        .then(result => {

            // Bind branch list
            if (result.ilUnitCrud && result.ilUnitCrud.length > 0) {

                let htmlContent = `
                    <div class="signup-style-19">
                        <table class="table table-striped">
                            <thead class="bg-info text-white">
                                <tr>
                                    <th>S/No</th>
                                    <th>Branch Name</th>
                                    <th>Corps</th>
                                    <th>Command</th>
                                </tr>
                            </thead>
                            <tbody>
                `;

                result.ilUnitCrud.forEach((item, index) => {

                    htmlContent += `
                        <tr>
                            <td>${index + 1}</td>
                            <td>${item.UnitName}</td>
                            <td>${item.Corps}</td>
                            <td>${item.Command}</td>
                        </tr>
                    `;
                });

                htmlContent += `
                            </tbody>
                        </table>
                    </div>
                `;

                document.querySelector(
                    '#addunitmodel .modal-body-list'
                ).innerHTML = htmlContent;

            } else {

                document.querySelector(
                    '#addunitmodel .modal-body-list'
                ).innerHTML = "<p>No branch records found.</p>";
            }

            // Reset Add Branch form
            $("#unitFormoutside")[0].reset();

            // Clear validation messages
            $("#unitFormoutside .text-danger").text("");

            // Open modal AFTER binding data
            $('#addunitmodel').modal('show');

        })
        .catch(error => {
            console.error('Error loading AddUnit form:', error);
        });

});

$(document).ready(function () {

    $("#adduserForm").on("submit", function (e) {

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
                        text: buttonName === "Register"
                            ? "User Created Successfully!"
                            : "",
                        type: "success",
                        confirmButtonColor: "#DD6B55",
                        confirmButtonText: "OK"
                    }, function () {

                        window.location.href = '/Home/ContactUs';

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


$(document).on("submit", "#unitFormoutside", function (e) {

    e.preventDefault();

    let $form = $(this);

    // Client-side validation
    if (!$form.valid()) {
        return false;
    }

    // Get values using actual HTML IDs
    let UnitName = $form.find("[name='unitCRUD.UnitName']").val();
    let ComdIdUnit = $form.find("[name='unitCRUD.ComdIdUnit']").val();
    let CorpId = $form.find("#spnCorpUnit").val();

    // Get submit button value
    let btnval = $form.find("button[type='submit']:focus").val() || "Add";

    console.log("UnitName:", UnitName);
    console.log("ComdIdUnit:", ComdIdUnit);
    console.log("CorpId:", CorpId);
    console.log("btnval:", btnval);

    $("#UnitNameValidation").text("").addClass("d-none");

    swal({
        title: "Please Confirm",
        text: "",
        type: "warning",
        showCancelButton: true,
        cancelButtonText: "Cancel",
        confirmButtonColor: "#DD6B55",
        confirmButtonText: btnval === "Add" ? "Yes, Add" : "Yes, Update",
        closeOnCancel: true,
        closeOnConfirm: false

    }, function (isConfirm) {

        if (!isConfirm) return;

        $.ajax({
            url: '/SignUp/AddUnitNew',
            type: 'POST',
            data: $form.serialize(),

            success: function (response) {

                console.log(response);

                if (response && response.success === false) {

                    let firstError = Object.values(response.errors || {})
                        .flat()[0] || response.message || "Validation failed.";

                    $("#UnitNameValidation")
                        .text(firstError)
                        .removeClass("d-none");

                    swal("Failed!", response.message || "Validation failed.", "error");
                    return;
                }

                swal({
                    title: "Success!",
                    text: "Added successfully!",
                    type: "success"
                }, function () {
                    $('#addunitmodel').modal('hide');
                    $form[0].reset();
                });
            },

            error: function (xhr) {
                console.error(xhr.responseText);

                swal(
                    "Request Failed!",
                    "HTTP Status: " + xhr.status,
                    "error"
                );
            }
        });
    });

    return false;
});