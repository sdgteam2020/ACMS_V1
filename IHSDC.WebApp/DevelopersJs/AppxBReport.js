
    $(document).ready(function () {
        let table = $('#AppxB').DataTable({
        lengthChange: true,

    aLengthMenu: [[25, 50, 100, 150, 200, 250, -1], [25, 50, 100, 150, 200, 250, "All"]],

    dom: 'lBfrtip',
    colReorder: true,
    responsive: true,
    paging: false,
    ordering: false,
    info: false,
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
    let buttonContainer = table.buttons().container().appendTo('#AppxB_wrapper .col-md-6:eq(0)');
    let searchBuilderContainer = table.searchBuilder.container();
    buttonContainer.insertBefore(table.table().container());
    searchBuilderContainer.insertBefore(table.table().container());

    });

    function PrintDiv() {
        let divToPrint = document.getElementById('PrintAppxB');
    let popupWin = window.open('', 'AppxB', 'top=100,width=1000,height=500,location=no');
    popupWin.document.open();
    let table_style = document.getElementById("table_style").innerHTML;
    popupWin.document.write('<style type="text/css">');
        popupWin.document.write(table_style);
        popupWin.document.write('</style>')
    popupWin.document.title = 'AppxB';
    popupWin.document.write('<html><body onload="window.print()">' + divToPrint.innerHTML + '<div class="custom-watermark">' + '@(ViewBag.ipadd)' + "" + '</html>');
        popupWin.document.close();
    }

