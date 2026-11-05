
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

        let watermarkText = Html.Raw(Json.Encode(watermarkText));


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

        let tableStyles = `
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
                                background-color: #f2f2f2;
                                color: black;
                            }
                            @@media print {

                                @@page {
                                    margin-top: 10px;
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
                                    .custom-closed-agenda-heading {
    text-align: center;
    margin-top: 20px;
}
.custom-page-heading {
    text-align: center;
    margin-top: 20px;
}
.print-watermark {
    transform: rotate(-45deg);
    z-index: 10000;
    opacity: 0.3;
    position: fixed;
    left: 6%;
    top: 39%;
    color: #8e9191;
    font-size: 80px;
    font-weight: 500;
    display: grid;
    justify-content: center;
    align-content: center;
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
        let filteredData = table.rows({ search: 'applied' }).data().toArray();

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
        let pageHeading = '<h1 class="custom-page-heading">Closed Agenda Point List</h1>';
popupWin.document.write('<html><head>' + pageHeading + tableStyles + '</head><body onload="window.print()">' + tableHTML + '<div class="print-watermark">' + watermarkText + '</div></body></html>');

popupWin.document.close();
    }

$(document).on("click", ".btnOpenAgendaPtNoting", function (e) {

    e.preventDefault();

    let url = $(this).attr("data-url");

    if (!url) {
        toastr.error("Invalid Agenda URL.");
        return;
    }

    window.location.href = url;

});


let message = '@message';
let messageStatus = '@messageStatus';
let messagemidStatus = '@messagemidStatus ';
    if (message)
			sweetAlert(message, messagemidStatus, messageStatus);







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
                            const reader = new FileReader();

                            reader.onload = function (e) {
                                const embed = document.createElement("embed");
                                embed.src = e.target.result;
                                embed.type = "application/pdf";
                                embed.width = "100%";
                                embed.height = "370px";
                                pdfContainer.innerHTML = "";
                                pdfContainer.appendChild(embed);
                                pdfContainer.style.display = "block";
                            };

                            reader.readAsDataURL(selectedFile);
                        } else {
                            alert("Please select a valid PDF file.");
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

    $(document).on('click', 'a[data-navigation-url]', function () {
        window.location.href = $(this).attr('data-navigation-url');
    });
