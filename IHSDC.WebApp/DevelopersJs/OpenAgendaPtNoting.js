$(document).ready(function () {
    $("#myForm3").on("submit", async function (e) {
       
        e.preventDefault(); // stop normal submit

        var $form = $(this);

        // Validate actual form
        if (!$form.valid()) {
            $("#CommentErrorOpen").removeClass('d-none');
            return false;
        }
        $("#CommentErrorOpen").addClass('d-none');
        this.submit();
    });
});
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
    document.getElementById("btnExport").addEventListener("click", function (event) {
        event.preventDefault();
    PrintDiv();
    });

function PrintDiv() {

    PrintContent(
        "APLogData",
        "table_style",
        "AgendaPointLog"
    );
}

function PrintChatDiv() {

    PrintContent(
        "ChatBox",
        "table_style1",
        "Agenda Point Noting Detail",
        "Agenda Point Noting Detail"
    );
}

async function PrintContent(
    elementId,
    printStyleId,
    title,
    pageHeading = ""
) {

    let IpAddress = $("#hdnIpAdd").val();

    let divToPrint = document.getElementById(elementId);
    let commonCSS = document.getElementById("openAgendaPtCSS");
    let printCSS = document.getElementById(printStyleId);

    if (!divToPrint || !commonCSS || !printCSS) {
        console.error("Required print elements not found.");
        return;
    }

    let popupWin = window.open(
        '',
        '_blank',
        'top=100,width=900,height=500,location=no'
    );

    if (!popupWin) {
        console.error("Popup window blocked.");
        return;
    }

    let doc = popupWin.document;

    doc.title = title;

    // Add heading if required
    if (pageHeading) {

        let heading = doc.createElement("h1");

        heading.className = "noting-print-heading";
        heading.textContent = pageHeading;

        doc.body.appendChild(heading);
    }

    // Add printing content
    doc.body.appendChild(
        doc.importNode(divToPrint, true)
    );

    // Add IP address watermark
    let watermark = doc.createElement("div");

    watermark.className = "noting-print-watermark";
    watermark.textContent = IpAddress || "";

    doc.body.appendChild(watermark);

    // Load external CSS
    function loadCSS(url) {

        return new Promise(function (resolve, reject) {

            let link = doc.createElement("link");

            link.rel = "stylesheet";
            link.href = url;

            link.onload = resolve;
            link.onerror = reject;

            doc.head.appendChild(link);
        });
    }

    try {

        await Promise.all([
            loadCSS(commonCSS.href),
            loadCSS(printCSS.href)
        ]);

        await doc.fonts.ready;

        popupWin.focus();
        popupWin.print();

    } catch (error) {

        console.error("Print CSS loading failed.", error);
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
            embed.height = "370px";
            pdfContainer.innerHTML = "";
            pdfContainer.appendChild(embed);
        } else {
            alert("Please select a valid PDF file.");
            input.value = "";
            pdfContainer.innerHTML = "";
        }
    }
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

  

var message = $("#hdnMessage").val();
var messageStatus = $("#hdnMessageStatus").val();
var messagemidStatus = $("#hdnMessagemidStatus").val();
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
            var pdfUrl = $("#pdfPath").attr('value');
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
                                embed.data = URL.createObjectURL(selectedFile) +
                                    "#toolbar=0&navpanes=0";
                                embed.type = "application/pdf";
                                embed.width = "100%";
                                embed.height = "370px";
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

        $(function () {
            $('#files').on('change', PreviewPDF);
            $('#wordPath').on('change', function () { if (typeof PreviewWordDoc === 'function') PreviewWordDoc(); });
            $('#Attachmentfiles').on('change', function () { checkPDFHeader(this); });
            $('#UploadPath, #WordFilePath, #txtComment, #OpenReason, #CloseReason, #txtForward, #FwdComment').on('keyup', function () { if (typeof ValInData === 'function') ValInData(this); });
            $('#UploadPath, #WordFilePath').on('keypress', function (e) { if (typeof onlyAlphaNumeric === 'function' && onlyAlphaNumeric(e) === false) e.preventDefault(); });
            $('.notingprint').on('click', PrintChatDiv);
            $('#btnExport').on('click', function (e) { e.preventDefault(); PrintDiv(); });
        });
 
