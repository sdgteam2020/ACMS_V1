
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
    embed.height = "470px";
    pdfContainer.innerHTML = "";
    pdfContainer.appendChild(embed);
            } else {
        alert("Please select a valid PDF file.");
    input.value = "";
    pdfContainer.innerHTML = "";
            }
        }
    }



    $(document).ready(function () {
        $("#ddlCivEdnId").change(function () {
            if ($("#NodalId").val() == "") {
                alert("Select first Nodal Branch(To)");
                $("#ddlCivEdnId").val("");
                $(".multiselect").html("<span class='multiselect-selected-text's>None selected</span>");
                $(".form-check-input").prop("checked", false);
            }
        });
    $("#NodalId").change(function () {
        $(".multiselect-option").removeClass("d-none");
    $(".multiselect-option").each(function () {
                if ($("#NodalId option:selected").text().trim() == $(this).text().trim()) {
        $(this).addClass("d-none");
                }
            });
        });


    $('.dropdownselect').select2({
        dropdownParent: $('#FwdPopup'),
    closeOnSelect: false
        });

    });

    $('#FwdPopup').on('shown.bs.modal', function (e) {

    });
    $('#FwdPopup').on('hidden.bs.modal', function (e) {

        $('#ddlCivEdnId').multiselect('deselectAll', false);
    $('#ddlCivEdnId').multiselect('updateButtonText');
    $("#txtForward").val("");
    $('#NodalId').val("0");
    $('#NodalComment').val("");
    $('#FwdComment').val("");
    });
    $("#closefwd").click(function () {

        $('#ddlCivEdnId').multiselect('deselectAll', false);
    $('#ddlCivEdnId').multiselect('updateButtonText');
    $("#txtForward").val("");
    $('#NodalId').val("0");
    $('#NodalComment').val("");
    $('#FwdComment').val("");
    });

    $("#clearfwd").click(function () {

        $('#ddlCivEdnId').multiselect('deselectAll', false);
    $('#ddlCivEdnId').multiselect('updateButtonText');
    $("#txtForward").val("");
    $('#NodalId').val("0");
    $('#NodalComment').val("");
    $('#FwdComment').val("");
    });


    $('#FwdPopup').on('click', function (e) {
        if (e.target === this) {

        $('#ddlCivEdnId').multiselect('deselectAll', false);
    $('#ddlCivEdnId').multiselect('updateButtonText');
    $("#txtForward").val("");
    $('#FwdPopup').modal('hide');
    $('#NodalId').val("0");
    $('#NodalComment').val("");
    $('#FwdComment').val("");
        }
    });

    $("#clearOpenReason").click(function () {
        $('#OpenReason').val("");
    });
    $("#clearCloseReason").click(function () {
        $('#CloseReason').val("");
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

    } else {
        sweetAlert("Please select a valid PDF file.");
    input.value = "";
                    }
                }
            };
    reader.readAsArrayBuffer(file);
        }
    }


    function PrintDiv() {
           let divToPrint = document.getElementById('APLogData');
    let popupWin = window.open('', '_blank', 'top=100,width=900,height=500,location=no');
    popupWin.document.open();
    let table_style = document.getElementById("table_style").innerHTML;
    popupWin.document.write('<style type="text/css">');
        popupWin.document.write(table_style);
        popupWin.document.write('</style>')
    popupWin.document.title = 'AgendaPointLog';
    popupWin.document.write('<html><body>' + divToPrint.innerHTML + '<div class="noting-print-watermark">' + '@(ViewBag.ipadd)' + "" + '</div></body></html>');
    popupWin.document.close();
    popupWin.focus();
    popupWin.print();
    }

    function PrintChatDiv() {
         let divToPrint = document.getElementById('ChatBox');
    let popupWin = window.open('', '_blank', 'top=100,width=900,height=500,location=no');
    popupWin.document.open();
    let table_style = document.getElementById("table_style1").innerHTML;
    popupWin.document.write('<style type="text/css">');
        popupWin.document.write(table_style);
        popupWin.document.write('</style>')
    let pageHeading = '<h1 class="noting-print-heading">Agenda Point Noting Detail</h1>';
    popupWin.document.write('<html><body>' + pageHeading + divToPrint.innerHTML + '<div class="noting-print-watermark">' + '@(ViewBag.ipadd)' + "" + '</div></body></html>');
    popupWin.document.close();
    popupWin.focus();
    popupWin.print();
    }


    $(function () {
        $('#ddlCivEdnId').multiselect({
            includeSelectAllOption: true,
            selectAllValue: 'multiselect-all',
            enableFiltering: true,
            enableCaseInsensitiveFiltering: true,
            maxHeight: '300',
            buttonWidth: '770',
        });
            });

    $("#ddlCivEdnId").change(function () {
                let selected = $("#ddlCivEdnId option:selected");
    let CivEdn = "";
    let arrSelected = [];
    selected.each(function () {
        arrSelected.push($(this).val());
    CivEdn += $(this).val() + ",";
                });
    CivEdn = CivEdn.slice(0, -1);
    $('#SentID').val(CivEdn);
            });


        $(document).ready(function () {
            let recipientContainer = $('#recipientContainer');
            let recipientInput = $('#recipientInput');
            recipientInput.keypress(function (e) {
                if (e.which === 13) {
                    e.preventDefault();
                    let recipientEmail = recipientInput.val().trim();
                    if (recipientEmail !== "") {
                        let recipientBox = $('<div class="recipient-box">' + recipientEmail + '<span class="remove-recipient">x</span></div>');
                        recipientContainer.append(recipientBox);
                        recipientInput.val('');
                    }
                }
            });

            recipientContainer.on('click', '.remove-recipient', function () {
                $(this).parent().remove();
            });
        });
 
        $('#clearComment').click(function () {
            $("#txtComment").val("");
        });

  

               let message = '@message';
    let messageStatus = '@messageStatus';
    let messagemidStatus = '@messagemidStatus ';
    if (message)
			sweetAlert(message, messagemidStatus, messageStatus);


   
        $(document).ready(function () {
            $("#search-input").on("keyup", function () {
                let searchValue = $(this).val().toLowerCase();
                $(".chat-messages .chat-message-right, .chat-messages .chat-message-left").each(function () {
                    let text = $(this).text().toLowerCase();
                    if (text.indexOf(searchValue) === -1) {
                        $(this).hide();
                    } else {
                        $(this).show();
                    }
                });
            });
        });
   

        $(document).ready(function () {
            let pdfUrl = $("#files").attr('value');
            if (pdfUrl) {
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
                                embed.height = "450px";
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
        });

   
        document.querySelectorAll('.noting-event-1').forEach(function (element) { element.addEventListener('change', function (event) { PreviewPDF(); }); });
        document.querySelectorAll('.noting-event-2').forEach(function (element) { element.addEventListener('keyup', function (event) { ValInData(this); }); });
        document.querySelectorAll('.noting-event-2').forEach(function (element) { element.addEventListener('keypress', function (event) { if (onlyAlphaNumeric(event) === false) event.preventDefault(); }); });
        document.querySelectorAll('.noting-event-3').forEach(function (element) { element.addEventListener('change', function (event) { PreviewWordDoc(); }); });
        document.querySelectorAll('.noting-event-4').forEach(function (element) { element.addEventListener('keyup', function (event) { ValInData(this); }); });
        document.querySelectorAll('.noting-event-4').forEach(function (element) { element.addEventListener('keypress', function (event) { if (onlyAlphaNumeric(event) === false) event.preventDefault(); }); });
        document.querySelectorAll('.noting-event-5').forEach(function (element) { element.addEventListener('keyup', function (event) { ValInData(this); }); });
        document.querySelectorAll('.noting-event-6').forEach(function (element) { element.addEventListener('change', function (event) { checkPDFHeader(this); }); });
        document.querySelectorAll('.noting-event-7').forEach(function (element) { element.addEventListener('keyup', function (event) { ValInData(this); }); });
        document.querySelectorAll('.noting-event-8').forEach(function (element) { element.addEventListener('keyup', function (event) { ValInData(this); }); });
        document.querySelectorAll('.noting-event-9').forEach(function (element) { element.addEventListener('keyup', function (event) { ValInData(this); }); });
        document.querySelectorAll('.noting-event-10').forEach(function (element) { element.addEventListener('keyup', function (event) { ValInData(this); }); });
        document.querySelectorAll('.noting-event-11').forEach(function (element) { element.addEventListener('click', function (event) { window.location.href = this.dataset.href; }); });
        document.querySelectorAll('.noting-event-12').forEach(function (element) { element.addEventListener('click', function (event) { PrintChatDiv(); }); });
        document.querySelectorAll('.noting-event-13').forEach(function (element) { element.addEventListener('click', function (event) { PrintDiv(); }); });
    