let watermarkText = $('#txtwaterMark').val();
 
        $(document).ready(function () {
            let table = $('#Analysis').DataTable({
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
                        action: function () {
                            PrintDiv();
                        }
                    },
                    'colvis',
                    {
                        text: 'Search',
                        className: "buttons-Search",
                        action: function () {
                            $(".dtsb-searchBuilder").removeClass("d-none");
                        }
                    },
                ],
                searchBuilder: {
                    conditions: {
                        num: {
                            'MultipleOf': {
                                conditionName: 'Multiple Of',
                                init: function (that, fn, preDefined = null) {
                                    let el = $('<input/>').on('input', function () { fn(that, this) });

                                    if (preDefined !== null) {
                                        $(el).val(preDefined[0]);
                                    }

                                    return el;
                                },
                                inputValue: function (el) {
                                    return $(el[0]).val();
                                },
                                isInputValid: function (el, that) {
                                    return $(el[0]).val().length !== 0;
                                },
                                search: function (value, comparison) {
                                    return value % comparison === 0;
                                }
                            }
                        }
                    }
                }
            });

            let buttonContainer = table.buttons().container().appendTo('#Analysis_wrapper .col-md-6:eq(0)');
            let searchBuilderContainer = table.searchBuilder.container();
            buttonContainer.insertBefore(table.table().container());
            searchBuilderContainer.insertBefore(table.table().container());

        });

        $(document).on('click', 'a[data-navigation-url]', function () {
            let navigationUrl = $(this).attr('data-navigation-url');
            if (navigationUrl) {
                window.location.href = navigationUrl;
            }
        });

        function PrintDiv() {
            let popupWin = window.open('', '_blank', 'top=100,width=900,height=500,location=no');
            if (!popupWin) return;
            popupWin.document.open();



            let table = $('#Analysis').DataTable();
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
let pageStyles = document.getElementById('analysis-page-styles').textContent;
popupWin.addEventListener('load', function () {
    popupWin.focus();
    popupWin.print();
});
popupWin.document.write('<html><head><style>' + pageStyles + '</style></head>' +
    '<body class="print-page"><h1 class="print-heading">Analysis</h1>' +
    tableHTML + '<div class="print-watermark" id="printWatermark"></div></body></html>');
            let printWatermark = popupWin.document.getElementById('printWatermark');
if (printWatermark) {
    printWatermark.textContent = watermarkText == null ? '' : String(watermarkText);
}

popupWin.document.close();
        }


   
        $(document).ready(function () {
            $('.clickable-div').click(function () {
                let href = $(this).data('href');
                window.location.href = href;
            });
        });
  




let today = new Date();
let message = Html.Raw(Json.Encode(message));
let messageStatus = Html.Raw(Json.Encode(messageStatus));
let messagemidStatus = Html.Raw(Json.Encode(messagemidStatus));
    if (message)
            sweetAlert(message, messagemidStatus, messageStatus);



  