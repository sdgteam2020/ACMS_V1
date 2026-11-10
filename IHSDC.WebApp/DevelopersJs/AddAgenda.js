let UserIntId = $('#txtUserIntId').val();
let UserId = $('#txtUserId').val();
let watermarkText = $('#txtwaterMark').val();
    document.querySelectorAll('.agenda-navigate').forEach(function (element) {
        element.onclick = function () {
            location.href = this.getAttribute('data-agenda-url');
        };
    });

    document.querySelectorAll('.agenda-submit-confirm').forEach(function (element) {
        element.onclick = function () {
            SubmitGEBConfirmation(this, '/ACC/FinalSubmitAgenda', 'Do you want to submit Agenda', 'FinalSubmitAgenda');
        };
    });

    document.querySelectorAll('.agenda-unfreeze-confirm').forEach(function (element) {
        element.onclick = function () {
            SubmitGEBConfirmation(this, '/ACC/AddRequestUnfreezeAgendaPoint', 'Do you place request for Unfreeze Agenda point', 'AddRequestUnfreezeAgendaPoint');
        };
    });

    document.querySelectorAll('.agenda-search-title').forEach(function (element) {
        element.onclick = function () { SearchTitle(); };
    });

    document.querySelectorAll('.agenda-validate-pdf').forEach(function (element) {
        element.onchange = function () { checkPDFHeader(this); };
    });

    document.querySelectorAll('.agenda-validate-word').forEach(function (element) {
        element.onchange = function () { checkWordFile(this); };
    });

    document.querySelectorAll('.agenda-validate-input').forEach(function (element) {
        element.onkeyup = function () { ValInData(this); };
    });

    document.querySelectorAll('.agenda-only-alphanumeric').forEach(function (element) {
        element.onkeypress = function (event) { return onlyAlphaNumeric(event); };
    });

    document.querySelectorAll('.agenda-count-title').forEach(function (element) {
        element.onkeyup = function () { updateCharacterCount(this); };
    });

    document.querySelectorAll('.agenda-count-summary').forEach(function (element) {
        element.onkeyup = function () { updateCharacterCountSum(this); };
    });

    function checkPDFHeader(input) {
    const file = input.files[0];
    if (file) {
        const reader = new FileReader();
    reader.onloadend = function (e) {
            if (e.target.readyState === FileReader.DONE) {
                const arr = new Uint8Array(e.target.result).subarray(0, 4);
                const header = Array.from(arr).map(byte => byte.toString(16)).join("").toUpperCase();

    if (header === "25504446") {
        PreviewPDF(file);
                } else {
        sweetAlert("Please select a valid PDF file.");
    input.value = "";
                }
            }
        };
    reader.readAsArrayBuffer(file);
    }
}

    function PreviewPDF() {
    const input = document.getElementById("files");
    const pdfContainer = document.getElementById("pdf-container");
    const maxSizeInBytes = 5 * 1024 * 1024;

    if (input.files && input.files[0]) {
        const selectedFile = input.files[0];

        if (selectedFile.size > maxSizeInBytes) {
        sweetAlert("File size exceeds the limit of 5MB. Please select a smaller file.");
    input.value = "";
    pdfContainer.innerHTML = "";
    return;
        }
    if (selectedFile.type === "application/pdf" || selectedFile.name.endsWith(".pdf")) {
            const embed = document.createElement("object");
    embed.data = URL.createObjectURL(selectedFile);
    embed.type = "application/pdf";
    embed.width = "100%";
    embed.height = "350px";
    pdfContainer.innerHTML = "";
    pdfContainer.appendChild(embed);
        } else {
        alert("Please select a valid PDF file.");
    input.value = "";
    pdfContainer.innerHTML = "";
        }
    }
}

    function checkWordFile(input) {
    const file = input.files[0];
    const maxSizeInBytes = 5 * 1024 * 1024;

    if (file.size > maxSizeInBytes) {
        sweetAlert("File size exceeds the limit of 5MB. Please select a smaller file.");
    input.value = "";
    return;
    }
    if (file) {
        const reader = new FileReader();

    reader.onloadend = function (e) {
            if (e.target.readyState === FileReader.DONE) {
                const arrayBuffer = e.target.result;

    mammoth.convertToHtml({arrayBuffer: arrayBuffer })
                    .then(result => {

    })
                    .catch(error => {
        sweetAlert("Please select a valid Word document (.docx only).");
    input.value = "";
                    });
            }
        };

    reader.readAsArrayBuffer(file);
    }
}

    function updateCharacterCount(input) {
    let maxLength = 500;
    let regex = /[^a-zA-Z0-9 . / ( ) _ -]/g;
    input.value = input.value.replace(regex, "");
    let currentLength = input.value.length;
    let charactersRemaining = maxLength - currentLength;
    let countDisplay = document.getElementById('characterCount');
    countDisplay.textContent = charactersRemaining;
}

    function updateCharacterCountSum(input) {
    let maxLength = 1000;
    let regex = /[^a-zA-Z0-9 . / ( ) _ -]/g;
    input.value = input.value.replace(regex, "");
    let currentLength = input.value.length;
    let charactersRemaining = maxLength - currentLength;
    let countDisplay = document.getElementById('characterCountsum');
    countDisplay.textContent = charactersRemaining;
}

    let input = document.getElementById("searchTerm");
    input.addEventListener("keypress", function (event) {
    if (event.key === "Enter") {
        event.preventDefault();
    document.getElementById("searchSubmit").click();
    }
});

    const tooltipContainers = document.querySelectorAll('.tooltip-container');

tooltipContainers.forEach(container => {
        container.addEventListener('mouseenter', () => {
            const tooltip = container.querySelector('.tooltip');
            tooltip.style.visibility = 'visible';
            tooltip.style.opacity = '1';
        });

    container.addEventListener('mouseleave', () => {
        const tooltip = container.querySelector('.tooltip');
    tooltip.style.visibility = 'hidden';
    tooltip.style.opacity = '0';
    });
});


    $('.Closepoup').on('click', function (e) {
    let modal = document.getElementById("PolicyModal");
    modal.style.display = "none";
});

    $('.CloseShowFilepoup').on('click', function (e) {
    let modal = document.getElementById("ShowFileModal");
    modal.style.display = "none";
});


    $('.PolicyModelOpen').on('click', function (e) {
    if ($('#Schedule_ID').val() == "") {
        sweetAlert('Please Select Conf !', '', 'error');
    }
    else {
        let Policy = window.btoa($('#spnDefaultPolicy').html());
    let Conf = window.btoa($('#spnConfTypeId').html());
    let Term = window.btoa($('#Title').val());
    $.ajax({
        url: '/PolicyCorner/SearchTitle',
    type: 'GET',
    data: {Term: Term, PolicyID: Policy, ConType: Conf },
    success: function (data) {
                let modal = document.getElementById("PolicyModal");
    modal.style.display = "block";
    $("#dataTable1").removeClass("d-none");

    $("#tbodySearch").html(data);
    $('#searchTerm').val("");
            },
    error: function () {
        $("#tbodySearch").html("");
    let modal = document.getElementById("PolicyModal");
    modal.style.display = "none";
            }
        });

    }
});


    function SearchTitle() {
    let Policy = window.btoa($('#spnDefaultPolicy').html());
    let Conf = window.btoa($('#spnConfTypeId').html());
    let Term = window.btoa($('#searchTerm').val());
    $.ajax({
        url: '/PolicyCorner/SearchTitle',
    type: 'GET',
    data: {Term: Term, PolicyID: Policy, ConType: Conf },
    success: function (data) {
            let modal = document.getElementById("PolicyModal");
    modal.style.display = "block";
    $("#dataTable1").removeClass("d-none");
    $("#tbodySearch").html(data);
    $('#searchTerm').val("");
        },
    error: function () {
        $("#tbodySearch").html("");
    let modal = document.getElementById("PolicyModal");
    modal.style.display = "none";
        }
    });
}


$(document).ready(function () {
   
    
        $('.dropdownsearch').select2();
});

    $(document).ready(function () {
    let table = $('#Agenda').DataTable({
        lengthChange: true,
    pageLength: 50,
    aLengthMenu: [[25, 50, 100, 150, 200, 250, -1], [25, 50, 100, 150, 200, 250, "All"]],

    dom: 'lBfrtip',
    buttons: [
    'copy',
    'excel',
    'csv',

    {
        text: 'PDF',
    extend: 'pdfHtml5',
    action: function (e, dt, node, config) {
        PrintDiv();
                }
            },
    'colvis',
    ],

    });


    let buttonContainer = table.buttons().container().appendTo('#Agenda_wrapper .col-md-6:eq(0)');
    let searchBuilderContainer = table.searchBuilder.container();
    buttonContainer.insertBefore(table.table().container());
    searchBuilderContainer.insertBefore(table.table().container());

});


    function PrintDiv() {
    let popupWin = window.open('', '_blank', 'top=100,width=900,height=500,location=no');
    popupWin.document.open();
    popupWin.onload = function () {popupWin.print(); };

    let tableStyles = `
    <style type="text/css">
        .agenda-print-heading {
            text - align: center;
        margin-top: 20px;
                                }
        .agenda-print-watermark {
            transform: rotate(-45deg);
        z-index: 10000;
        opacity: 0.3;
        color: #8e9191;
        position: fixed;
        top: 39%;
        left: 6%;
        font-size: 80px;
        font-weight: 500px;
        display: grid;
        justify-content: center;
        align-content: center;
                                }
    </style>
    <style type="text/css">
        table {
            width: 100%;
        border-collapse: collapse;
        margin-bottom: 20px;
                            }

        th, td {
            padding: 8px;
        border: 1px solid #ddd;
        text-align: center;
                            }

        th {
            background - color: #f2f2f2;
        color: black;
                            }

        @@media print {

            @@page {
            margin - top: 10px;
        margin-bottom: 10px;
                                }

        body::after {
            content: none !important;
                                }

        .card-page {
                                    break-after: page;
                                }

        .card-page:last-of-type {
                                        break-after: auto;
                                    }

        footer {
            position: fixed;
        bottom: 0;
        left: 0;
        right: 0;
        height: 10px;
        background-color: gray;
        color: white;
        text-align: center;
        padding: 10px;
                                }

        header {
            position: fixed;
        top: 0;
        left: 0;
        right: 0;
        height: 10px;
        background-color: gray;
        color: white;
        text-align: center;
        padding: 10px;
                                }
                            }
    </style>
    `;

    let table = $('#Agenda').DataTable();
    let visibleColumns = table.columns(':visible').header().toArray();
    let filteredData = table.rows({search: 'applied' }).data().toArray();

    let tableHTML = '<table>';

        tableHTML += '<thead>';
            tableHTML += '<tr>';
                visibleColumns.forEach(function (header) {
                    tableHTML += '<th>' + header.innerHTML + '</th>';
    });
                tableHTML += '</tr>';
            tableHTML += '</thead>';

        tableHTML += '<tbody>';
            for (var i = 0; i < filteredData.length; i++) {
                tableHTML += '<tr>';
            for (var j = 0; j < filteredData[i].length; j++) {
            if (!table.column(j).visible()) {
                continue;
            }
            tableHTML += '<td>' + filteredData[i][j] + '</td>';
        }
            tableHTML += '</tr>';
    }
        tableHTML += '</tbody>';

    tableHTML += '</table>';


let pageHeading = '<h1 class="agenda-print-heading">Agenda Point List</h1>';
popupWin.document.write('<html><head>' + pageHeading + tableStyles + '</head><body>' + tableHTML + '<div class="agenda-print-watermark">' + watermarkText + '</div></body></html>');

popupWin.document.close();
}

//let message = '@message';
//let messageStatus = '@messageStatus';
//let messagemidStatus = '@messagemidStatus ';
//if (message)
//    sweetAlert(message, messagemidStatus, messageStatus);


$(document).ready(function () {

    BindOrganizerCategory($("#spnScheduleId").html());
    GetOrganizerId($("#spnScheduleId").html());
   
    $('#spnCategoryId').change(function () {
        $("#CategoryId").val($('#spnCategoryId').val());
    });

    $('#spnSponsorId').change(function () {
        $("#SponsorCategoryId").val($('#spnSponsorId').val());
    });

    $('#spnNodalId').change(function () {
        $("#NodalCategoryId").val($('#spnNodalId').val());
    });

    $('#spnBranch').change(function () {
        $("#Branch").val($('#spnBranch').val());
    });

    $('#Schedule_ID').change(function () {
        BindOrganizerCategory($("#Schedule_ID").val());
        GetOrganizerId($("#Schedule_ID").val())
    });

    if ($('#spnGSOId').val() == UserId) {
        if ($('#spnsponsor').val() != UserIntId) {
            $('#btnsubmit').addClass('agenda-script-hidden');
        }
    }

});

function GetOrganizerId(Schedule_ID) {
    $.ajax({
        url: '/Master/GetOrganizerId',
        type: 'POST',
        data: { "Schedule_ID": Schedule_ID },
        success: function (result) {
            if (result.length > 0) {

                if (result[0].OrganizerCatId != "") {
                    if (result[0].LetterCreatedBy == UserIntId) {
                        $('#spnCategoryId').prop('disabled', false);
                    }
                    else {
                        if ($("#CategoryId").val() == 0) {
                            $('#spnCategoryId').val('');
                        }
                        $('#spnCategoryId').prop('disabled', true);
                    }
                }
                else {
                    $('#spnCategoryId').val('');
                    $('#spnCategoryId').prop('disabled', true);
                }

                if (result[0].BranchIds != "") {
                    if (result[0].LetterCreatedBy == UserIntId) {
                        $('#spnBranch').prop('disabled', false);
                    }
                    else {
                        if ($("#Branch").val() == 0) {
                            $('#spnBranch').val('');
                        }
                        $('#spnBranch').prop('disabled', true);
                    }
                }
                else {
                    $('#spnBranch').val('');
                    $('#spnBranch').prop('disabled', true);
                }

                if (result[0].SponsorCatId != "") {
                    if ($('#Inbox_ID').val() != 0) {
                        if ($('#spnsponsor').html() == UserIntId || result[0].LetterCreatedBy == UserIntId) {

                            $('#spnSponsorId').prop('disabled', false);
                        }
                        else {
                            if ($("#SponsorCategoryId").val() == 0) {
                                $('#spnSponsorId').val('');
                            }
                            $('#spnSponsorId').prop('disabled', true);
                        }
                    }
                    else {
                        $('#spnSponsorId').prop('disabled', false);
                    }
                }
                else {
                    $('#spnSponsorId').val('');
                    $('#spnSponsorId').prop('disabled', true);
                }

                if (result[0].NodalCatId != "") {
                    if ($('#spnNode').html() == UserIntId || result[0].LetterCreatedBy == UserIntId) {
                        $('#spnNodalId').prop('disabled', false);
                    }
                    else {
                        if ($("#NodalCategoryId").val() == 0) {
                            $('#spnNodalId').val('');
                        }
                        $('#spnNodalId').prop('disabled', true);
                    }
                }
                else {
                    $('#spnNodalId').val('');
                    $('#spnNodalId').prop('disabled', true);
                }
            }
            else {
                $('#spnletterCreatedById').html("");
            }
        }
    });

}

function BindOrganizerCategory(Schedule_ID) {
    let list = '';
    let list1 = '';
    let list2 = '';
    let list3 = '';
    $.ajax({
        url: '/Master/GetCategory',
        type: 'POST',
        data: { "Proc": "7", "Schedule_ID": Schedule_ID },
        success: function (result) {
            if (result.length > 0) {
                list = '';
                list = '<option value="">Select Category</option>';
                for (var j = 0; j < result.length; j++) {
                    list += '<option value=' + result[j].CategoryId + '>' + result[j].CategoryName + '</option>';
                }
                $('#spnCategoryId').html(list);
            }
            else {
                list = '';
                list = '<option value="">Select Category</option>';
                $('#spnCategoryId').html(list);
            }

            if ($("#CategoryId").val() != 0) {
                $('#spnCategoryId').val($("#CategoryId").val());
            }

        }
    });

    $.ajax({
        url: '/Master/GetCategory',
        type: 'POST',
        data: { "Proc": "8", "Schedule_ID": Schedule_ID },
        success: function (result) {
            if (result.length > 0) {
                list1 = '';
                list1 = '<option value="">Select Category</option>';
                for (var j = 0; j < result.length; j++) {
                    list1 += '<option value=' + result[j].CategoryId + '>' + result[j].CategoryName + '</option>';
                }
                $('#spnSponsorId').html(list1);
            }
            else {
                list1 = '';
                list1 = '<option value="">Select Category</option>';
                $('#spnSponsorId').html(list1);
            }
            if ($("#SponsorCategoryId").val() != 0) {
                $('#spnSponsorId').val($("#SponsorCategoryId").val());
            }
        }
    });

    $.ajax({
        url: '/Master/GetCategory',
        type: 'POST',
        data: { "Proc": "9", "Schedule_ID": Schedule_ID },
        success: function (result) {
            if (result.length > 0) {
                list2 = '';
                list2 = '<option value="">Select Category</option>';
                for (var j = 0; j < result.length; j++) {
                    list2 += '<option value=' + result[j].CategoryId + '>' + result[j].CategoryName + '</option>';
                }
                $('#spnNodalId').html(list2);
            }
            else {
                list2 = '';
                list2 = '<option value="">Select Category</option>';
                $('#spnNodalId').html(list2);
            }
            if ($("#NodalCategoryId").val() != 0) {
                $('#spnNodalId').val($("#NodalCategoryId").val());
            }
        }
    });

    $.ajax({
        url: '/Master/GetBranch',
        type: 'POST',
        data: { "Proc": "5", "Schedule_ID": Schedule_ID },
        success: function (result) {
            if (result.length > 0) {
                list3 = '';
                list3 = '<option value="">Select Nodal Branch</option>';
                for (var j = 0; j < result.length; j++) {
                    list3 += '<option value=' + result[j].BranchId + '>' + result[j].BranchName + '</option>';
                }
                $('#spnBranch').html(list3);
            }
            else {
                list3 = '';
                list3 = '<option value="">Select Nodal Branch</option>';
                $('#spnBranch').html(list3);
            }
            if ($("#Branch").val() != 0) {
                $('#spnBranch').val($("#Branch").val());
            }
        }
    });


    $.ajax({
        url: '/Master/GetPolicyByConfId',
        type: 'POST',
        data: { "Proc": "4", "Id": Schedule_ID },
        success: function (result) {
            if (result.length > 0) {
                $('#spnDefaultPolicy').html(result[0].PolicyName);
                $('#spnConfTypeId').html(result[0].TypeOfConfId);
            }
            else {
                $('#spnDefaultPolicy').html("");
                $('#spnConfTypeId').html("");
            }
        }
    });

}


$(document).ready(function () {

    var pdfUrl = $("#pdfPath").attr('value');
    if (pdfUrl !== undefined) {
        if (pdfUrl) {
            pdfUrl += "?t=" + new Date().getTime();
            console.log("Converted File Object:", pdfUrl);
            fetch(pdfUrl)
                .then(response => {
                    if (!response.ok) {
                        throw new Error("Failed to load PDF document");
                    }
                    return response.blob();
                })
                .then(blob => {
                    const pdfFile = new File([blob], "aa.pdf", { type: 'application/pdf' });

                    const pdfContainer = document.getElementById("pdf-container");

                    if (pdfFile) {
                        const selectedFile = pdfFile;

                        if (selectedFile.type === "application/pdf" || selectedFile.name.endsWith(".pdf")) {
                            const embed = document.createElement("object");
                            embed.data = URL.createObjectURL(selectedFile);
                            embed.type = "application/pdf";
                            embed.width = "100%";
                            embed.height = "350px";
                            pdfContainer.innerHTML = "";
                            pdfContainer.appendChild(embed);
                        } else {
                            alert("Please select a valid PDF file.");
                            input.value = "";
                            pdfContainer.innerHTML = "";
                        }
                    }

                    console.log("Converted File Object:", pdfFile);

                    const formData = new FormData();
                })
                .catch(error => {
                    console.error("Failed to fetch or convert the PDF:", error);
                    const pdfContainer = document.getElementById("pdf-container");
                    pdfContainer.innerHTML = "Failed to load PDF document.";
                });
        }
    }


});

document.addEventListener("DOMContentLoaded", function () {
    const form = document.getElementById("myForm");
    const btnSave = document.getElementById("btnSave");
    const wordFileInput = document.getElementById("Wordfiles");
    const wordFileError = document.getElementById("wordFileError");
    const PdfFileInput = document.getElementById("files");
    const PdfFileError = document.getElementById("PdfFileError");

    function clearFileError(inputElement, errorElement) {
        inputElement.addEventListener("change", function () {
            if (inputElement.files.length) {
                errorElement.style.display = "none";
            }
        });
    }


    clearFileError(wordFileInput, wordFileError);
    clearFileError(PdfFileInput, PdfFileError);

    btnSave.addEventListener("click", function (e) {
        let isValid = true;


        if (!wordFileInput.files.length) {
            wordFileError.style.display = "block";
            isValid = false;
        } else {
            wordFileError.style.display = "none";
        }


        if (!PdfFileInput.files.length) {
            PdfFileError.style.display = "block";
            isValid = false;
        } else {
            PdfFileError.style.display = "none";
        }

        const buttonName = "@ViewBag.ButtonName";
        const uploadPath = "@ViewBag.UploadPath";
        if (buttonName === "Update" && uploadPath !== "") {

            wordFileError.style.display = "none";
            PdfFileError.style.display = "none";
            return true;
        }

        if (!isValid) {
            e.preventDefault();
            return false;
        }
    });
});

$('#Title').keyup(function () {
    if (event.keyCode === 32 || event.keyCode === 8) {
        let searchTermValue = window.btoa($(this).val());
        let Policy = window.btoa($('#spnDefaultPolicy').html());
        let ConTypeId = window.btoa($('#spnConfTypeId').html());
        $.ajax({
            url: '/PolicyCorner/SearchAgendaPointTitle',
            type: 'POST',
            data: { "Term": searchTermValue, "PolicyID": Policy, "ConType": ConTypeId },
            dataType: 'json',
            timeout: 3000,
            success: function (response) {
                displaySuggestions(response);
            },
            error: function (error) {

            }
        });
    }
});
function displaySuggestions(suggestions) {
    let suggestionBox = $("#suggestionBox");
    suggestionBox.empty();
    for (var i = 0; i < suggestions.length; i++) {
        let suggestionId = window.btoa(suggestions[i].FileName);
        let suggestionItem = $("<div class='suggestionItem' id=" + suggestionId + "></div>");

        let suggestionContent = suggestions[i].paragraph;
        if (suggestions[i].FileName.toLowerCase().endsWith('.pdf')) {
            suggestionContent = "<div class='pdfup'></div>" + suggestionContent;
        }

        suggestionItem.html(suggestionContent);
        suggestionBox.append(suggestionItem);
    }
    suggestionBox.show();
}

$("#suggestionBox").on("click", ".suggestionItem", function () {
    let Id = window.atob($(this).attr("id"));
    $('#ShowpdfModal').show();

    if (Id) {
        fetch(Id)
            .then(response => {
                if (!response.ok) {
                    throw new Error("Failed to load PDF document");
                }
                return response.blob();
            })
            .then(blob => {
                const pdfFile = new File([blob], "aa.pdf", { type: 'application/pdf' });

                const pdfContainer = document.getElementById("Searchpdf-container");

                if (pdfFile) {
                    const selectedFile = pdfFile;

                    if (selectedFile.type === "application/pdf" || selectedFile.name.endsWith(".pdf")) {
                        const embed = document.createElement("object");
                        embed.data = URL.createObjectURL(selectedFile);
                        embed.type = "application/pdf";
                        embed.width = "100%";
                        embed.height = "600px";
                        pdfContainer.innerHTML = "";
                        pdfContainer.appendChild(embed);
                    } else {
                        alert("Please select a valid PDF file.");
                        input.value = "";
                        pdfContainer.innerHTML = "";
                    }
                }

                console.log("Converted File Object:", pdfFile);

                const formData = new FormData();
            })
            .catch(error => {
                console.error("Failed to fetch or convert the PDF:", error);
                const pdfContainer = document.getElementById("Searchpdf-container");
                pdfContainer.innerHTML = "Failed to load PDF document.";
            });
    }

});
$('.CloseSearchpoup').on('click', function (e) {
    let modal = document.getElementById("ShowpdfModal");
    modal.style.display = "none";
});




$(document).ready(function () {

    const $form = $("#btnSave").closest("form");

    async function EncryptAgendaPayload(form) {

        const payload = {};

        // Collect all form fields
        $(form).serializeArray().forEach(function (field) {

            if (field.name === "__RequestVerificationToken") {
                return;
            }

            // Keeps first value for MVC checkbox true/false pairs
            if (!Object.prototype.hasOwnProperty.call(
                payload, field.name
            )) {
                payload[field.name] = field.value;
            }
        });

        payload.btnval = $("#btnSave").val();

        // Convert complete form data into JSON
        let jsonData = JSON.stringify(payload);

        // Encrypt using your existing function
        let encrypted = await encryptPayloadData(jsonData);

        return encrypted;
    }


    $form.on("submit", async function (e) {

        const form = this;

        const Inbox_ID =
            parseInt($("#Inbox_ID").val(), 10) || 0;

        // SAVE: Normal form submission
        if (Inbox_ID === 0) {
            return;
        }

        // UPDATE: AJAX encrypted submission
        e.preventDefault();

        if (!form.checkValidity()) {
            form.reportValidity();
            return;
        }

        const $btn = $("#btnSave");

        if ($btn.prop("disabled")) {
            return;
        }

        $btn.prop("disabled", true);

        try {

            // Encrypt form fields
            let encrypted = await EncryptAgendaPayload(form);

            if (!encrypted) {
                throw new Error("Encryption failed.");
            }

            const requestData = new FormData();

            // Encrypted payload
            requestData.append("Payload", encrypted);

            // PDF file - send directly if selected
            const pdfInput = document.getElementById("files");

            requestData.append(
                "Upload",
                pdfInput && pdfInput.files.length > 0
                    ? pdfInput.files[0]
                    : new Blob([]),
                pdfInput && pdfInput.files.length > 0
                    ? pdfInput.files[0].name
                    : ""
            );

            // Second file: Word → Request.Files[1]
            const wordInput = document.getElementById("Wordfiles");

            requestData.append(
                "WordFile",
                wordInput && wordInput.files.length > 0
                    ? wordInput.files[0]
                    : new Blob([]),
                wordInput && wordInput.files.length > 0
                    ? wordInput.files[0].name
                    : ""
            );

            // Anti-forgery token
            const token = $form.find(
                'input[name="__RequestVerificationToken"]'
            ).val();

            if (token) {
                requestData.append(
                    "__RequestVerificationToken",
                    token
                );
            }

            // POST encrypted fields + optional files
            $.ajax({
                url: "/ACC/UpdateAgendaEncrypted",
                type: "POST",
                data: requestData,
                processData: false,
                contentType: false,
                dataType: "json",

                success: function (response) {
                    if (response.success) {
                        swal({
                            title: "Update Successfully",
                            text: "",
                            type: "success",
                            confirmButtonText: "OK",
                            closeOnConfirm: true
                        }, function (isConfirm) {
                            if (isConfirm) {
                                window.location.href = "/ACC/AddAgenda";
                            }
                        });
                    }
                    else {
                        sweetAlert("Error!", response.message, "error");
                    }
                },

                complete: function () {
                    $btn.prop("disabled", false);
                }
            });

        } catch (error) {

            $btn.prop("disabled", false);

            console.error(error);

            sweetAlert(
                "Encryption Failed!",
                error.message,
                "error"
            );
        }

    });

});

