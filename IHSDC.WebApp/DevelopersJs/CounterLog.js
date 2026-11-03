
    $('.CloseLogpoup').on('click', function (e) {
            let modal = document.getElementById("CounterLog");
    modal.style.display = "none";
        });

    $(document).ready(function () {
        $('#dataTable').DataTable({
            paging: true, // Enable pagination
            info: true, // Show table information
            "lengthMenu": [5, 10, 25, 50], // Customize the number of rows per page
            "pageLength": 5 // Set the initial number of rows per page
        });
        });

    function PrintDiv() {
        let divToPrint = document.getElementById('CounterLog');
    let popupWin = window.open('', '_blank', 'top=100,width=900,height=500,location=no');
    popupWin.document.open();
    let table_style = document.getElementById("table_style").innerHTML;
    popupWin.document.write('<style type="text/css">');
        popupWin.document.write(table_style);
        popupWin.document.write('</style>')
    popupWin.document.title = 'MonthWiseUsersLog';
    popupWin.document.write('<html><body onload="window.print()">' + divToPrint.innerHTML + '<div class="custom-watermark">' + '@(ViewBag.ipadd)' + "" + '</html>');
        popupWin.document.close();
    }

  