
    function ValInData1(input) {
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
            $('#ComdIdSignUp').val("");
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

            BindBde($("#ComdIdSignUp").val(), $("#CorpId").val());
            BindUnit($("#ComdIdSignUp").val(), $("#CorpId").val(), $("#BdeId").val());
        });

        $('#ComdIdSignUp').change(function () {
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
            BindBde($("#ComdIdUnit").val(), $("#CorpIdNew").val());
            BindUnitUnit($("#ComdIdUnit").val(), $("#CorpIdNew").val(), $("#BdeId").val());
        });


        $('#spnCorp').change(function () {
            $("#CorpId").val($(this).val());
            BindBde($("#ComdIdSignUp").val(), $(this).val());
            BindUnit($("#ComdIdSignUp").val(), $(this).val(), $("#BdeId").val());
        });

        $('#spnBde').change(function () {
            $("#BdeId").val($(this).val());
            BindUnit($("#ComdIdSignUp").val(), $("#CorpId").val(), $(this).val());
        });

        $('#spnUnit').change(function () {
            $("#Unit_ID").val($(this).val());
        });




       document.getElementById('addbranch').addEventListener('click', function (e) {
    e.preventDefault();

    const url = '@Url.Action("AddUnitNew", "SignUp")';

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
            e.preventDefault();
            let url = this.getAttribute('data-url');

            fetch(url)
                .then(response => response.text())
                .then(html => {
                    document.querySelector('#addApptModal .modal-body').innerHTML = html;
                    $('#addApptModal').modal('show');
                })
                .catch(error => console.error('Error loading AddAppt form:', error));
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
                .then(response => response.text())
                .then(html => {
                    document.querySelector('#addDgsModal .modal-body').innerHTML = html;
                    $('#addDgsModal').modal('show');
                })
                .catch(error => console.error('Error loading AddAppt form:', error));
        });

        document.getElementById('addDgsModal').addEventListener('hidden.bs.modal', function () {

            let ComdIdSignUp = $("#ComdIdSignUp").val();

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


let message = '@message';
let messageStatus = '@messageStatus';
let messagemidStatus = '@messagemidStatus';

if (message) {
    if (message == "s") {
        SubmitToken(this, '/Account/Login', 'Your Token is not connected. Connect Token and Login again', 'SubmitGEB2IC');
    } else if (message == "b") {
        SubmitToken(this, '/Account/Login', 'Public Key not Found. Ask Admin to install your public key.', 'SubmitGEB2IC');
    } else {
        sweetAlert(message, messagemidStatus, messageStatus);
    }
}

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
    let appt = $("#Appt").val().trim();
    let apptAbbreviation = $("#ApptAbbreviation").val().trim();

    if (!appt) {
        sweetAlert("Appt Abbreviation is required.");
        return;
    }
    if (!apptAbbreviation) {
        sweetAlert("Appt Name is required.");
        return;
    }
    AddConfirmation(this, '/SignUp/AddAppt', 'AddAppt', event);
});
function AddConfirmation(obj, ActionName, Submit, event) {
    event.preventDefault();
    let appt = $("#Appt").val().trim();
    let apptAbbreviation = $("#ApptAbbreviation").val().trim();

    if (!appt) {
        sweetAlert("Appt Abbreviation is required.");
        return;
    }
    if (!apptAbbreviation) {
        sweetAlert("Appt Name is required.");
        return;
    }

    let formData = new FormData($('form')[0]);
    $.ajax({
        url: '@Url.Action("AddAppt", "SignUp")',
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
                url: '@Url.Action("ReloadAppointments", "SignUp")',
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
            $('#ComdIdSignUp').val('').trigger('change');
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
        url: '@Url.Action("AddUnitNew", "SignUp")',
        type: 'POST',
        data: formData,
        contentType: false,
        processData: false,
        success: function (response) {


            sweetAlert(response.message);

            $('#ComdIdSignUp').val(null).trigger('change');
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
        let message = '@message';
        let messageStatus = '@messageStatus';
        let messagemidStatus = '@messagemidStatus';
        if (message)
            sweetAlert(message, messagemidStatus, messageStatus);

    $(document).ready(function () {
        $('.dropdownsearch').select2();

    });

    $(document).ready(function () {
        $('#Number').on('keyup', function () { ValInDataReg(this); });
        $('#FullName, #UnitName').on('keyup', function () { ValInData(this); });
        $('#ASCON, #MobNo').on('input', function () { ValInData1(this); });
        $('#addunitmodel button[name="btnval"]').on('click', function (event) {
            AddConfirmationBranch(this, '/SignUp/AddUnit', 'SignUp', event);
        });
        $('#btnNew').on('click', function () {
            location.href = '@Url.Action("Login", "Account")';
        });
    });
