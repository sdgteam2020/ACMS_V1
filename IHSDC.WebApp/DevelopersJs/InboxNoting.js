
let watermarkText = Html.Raw(Json.Encode(watermarkText));
   
        $("body").addClass("loading");
        $(document).ready(function () {
            let table = $('#Inbox').DataTable({
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
            let buttonContainer = table.buttons().container().appendTo('#Inbox_wrapper .col-md-6:eq(0)');

            buttonContainer.insertBefore(table.table().container());

        });
        function PrintDiv() {
            let popupWin = window.open('', '_blank', 'top=100,width=900,height=500,location=no');
            popupWin.addEventListener('load', function () { popupWin.print(); }, { once: true });
            popupWin.document.open();

            let tableStyles = `<style type="text/css">
                                                                .inbox-print-title { text-align: center; margin-top: 20px; }
                                                                .inbox-print-watermark { transform: rotate(-45deg); z-index: 10000; opacity: 0.3; color: #8e9191; position: fixed; top: 39%; left: 6%; font-size: 80px; font-weight: 500; display: grid; justify-content: center; align-content: center; }
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
                                                            </style>`;
            let table = $('#Inbox').DataTable();
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
            let pageHeading = '<h1 class="inbox-print-title">Inbox</h1>';
popupWin.document.write('<html><head>' + pageHeading + tableStyles + '</head><body>' + tableHTML + '<div class="inbox-print-watermark">' + watermarkText + '</div></body></html>');
popupWin.document.close();
        }


        $(document).ready(function () {
            $(document).on('click', 'a[data-navigation-url]', function () {
                window.location.href = $(this).attr('data-navigation-url');
            });
            $('.clickable-div').click(function () {
                let href = $(this).data('href');
                window.location.href = href;
            });
        });
 
        $(document).ready(function () {

            let table = $('#Inbox').DataTable();
            $('#ChkAll').on('click', function () {
                let rows = table.rows({ 'search': 'applied' }).nodes();
                $('input[type="checkbox"]', rows).prop('checked', this.checked);
                if (!this.checked) {
                    $('#ChkAll').prop('checked', false);
                }
            });
            $('#Inbox tbody').on('change', 'input[type="checkbox"]', function () {
                if (!this.checked) {
                    let el = $('#ChkAll').get(0);
                    if (el && el.checked && ('indeterminate' in el)) {
                        el.indeterminate = true;
                    }
                }
            });
        });

let row;
        function start() {
            row = event.target;
        }
        function dragover() {
            let e = event;
            e.preventDefault();

            let children = Array.from(e.target.parentNode.parentNode.children);
            if (children.indexOf(e.target.parentNode) > children.indexOf(row))
                e.target.parentNode.after(row);
            else
                e.target.parentNode.before(row);
        }

let today = new Date();
let message = '@message';
let messageStatus = '@messageStatus';
let messagemidStatus = '@messagemidStatus';
    if (message)
            sweetAlert(message, messagemidStatus, messageStatus);
   