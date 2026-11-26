
$(document).ready(function () {

    // Initialize main DataTable
    if ($.fn.DataTable && !$.fn.DataTable.isDataTable('#dataTable')) {

        $('#dataTable').DataTable({
            paging: true,
            info: true,
            searching: true,
            lengthMenu: [5, 10, 25, 50],
            pageLength: 5,
            autoWidth: false
        });

    }

    // Open user log popup and load selected command
    $(document).on('click', '.BtnUserLog', function () {

        var command = $(this)
            .closest('tr')
            .find('.Aviator_ID')
            .text()
            .trim();

        OpenCounterLog();

        GetUserLog(18, command);

    });


    // Close popup
    $(document).on('click', '.CloseLogpoup', function () {

        CloseCounterLog();

    });


    // Close popup when clicking outside
    $('#CounterLog').on('click', function (e) {

        if (e.target === this) {
            CloseCounterLog();
        }

    });


    // Close popup with Escape key
    $(document).on('keydown', function (e) {

        if (e.key === 'Escape') {
            CloseCounterLog();
        }

    });


    // Print button
    $('#btnPrintLog').on('click', function () {

        PrintDiv();

    });

});


// Open custom modal
function OpenCounterLog() {

    $('#CounterLog')
        .addClass('counter-modal-open')
        .attr('aria-hidden', 'false');

}


// Close custom modal
function CloseCounterLog() {

    $('#CounterLog')
        .removeClass('counter-modal-open')
        .attr('aria-hidden', 'true');

}


// AJAX function
function GetUserLog(Procid, Aviator_ID) {

    $('#aviatorTableContainer').empty();

    var loadingMessage = $('<p>')
        .addClass('loading-message')
        .text('Loading records...');

    $('#aviatorTableContainer').append(loadingMessage);

    $.ajax({

        url: '/Master/GetCounterLog',

        type: 'POST',

        data: {
            Procid: Procid,
            Command: Aviator_ID
        },

        success: function (response) {

            $('#aviatorTableContainer').empty();

            if (response && response.length > 0) {

                CreateCounterLogTable(response);

            }
            else {

                var noRecordMessage = $('<p>')
                    .addClass('no-record-message')
                    .text('No record found');

                $('#aviatorTableContainer').append(noRecordMessage);

            }

        },

        error: function (xhr, status, error) {

            $('#aviatorTableContainer').empty();

            var errorMessage = $('<p>')
                .addClass('error-message')
                .text('Unable to load counter logs. Please try again.');

            $('#aviatorTableContainer').append(errorMessage);

            console.error('Counter Log Error:', error);

        }

    });

}


// Generate Counter Log Table
function CreateCounterLogTable(response) {

    var table = $('<table>')
        .addClass('table table-striped align-top custom-aviator-table')
        .attr('id', 'aviatorTable');

    // Table Header
    var thead = $('<thead>')
        .addClass('bg-info text-white');

    var headerRow = $('<tr>');

    headerRow.append($('<th>').text('Ser No'));
    headerRow.append($('<th>').text('Comd Name'));
    headerRow.append($('<th>').text('UserName'));
    headerRow.append($('<th>').text('Today'));
    headerRow.append($('<th>').text('Current Month'));
    headerRow.append($('<th>').text('Total'));

    thead.append(headerRow);

    table.append(thead);


    // Table Body
    var tbody = $('<tbody>');

    var grandTotal = 0;
    var TodayTotal = 0;
    var MonthTotal = 0;

    response.forEach(function (row, index) {

        var today = Number(row.Today) || 0;
        var month = Number(row.CurrentMonth) || 0;
        var total = Number(row.Total) || 0;

        var dataRow = $('<tr>');

        dataRow.append($('<td>').text(index + 1));
        dataRow.append($('<td>').text(row.ComdName || ''));
        dataRow.append($('<td>').text(row.UserName || ''));
        dataRow.append($('<td>').text(today));
        dataRow.append($('<td>').text(month));
        dataRow.append($('<td>').text(total));

        grandTotal += total;
        TodayTotal += today;
        MonthTotal += month;

        tbody.append(dataRow);

    });


    // Footer Total
    var footerRow = $('<tr>');

    footerRow.append(
        $('<td>')
            .attr('colspan', 3)
            .addClass('bold-text top-border-black')
            .text('Total')
    );

    footerRow.append(
        $('<td>')
            .addClass('bold-text top-border-black')
            .text(FormatTotal(TodayTotal))
    );

    footerRow.append(
        $('<td>')
            .addClass('bold-text top-border-black')
            .text(FormatTotal(MonthTotal))
    );

    footerRow.append(
        $('<td>')
            .addClass('bold-text top-border-black')
            .text(FormatTotal(grandTotal))
    );

    tbody.append(footerRow);

    table.append(tbody);


    // Append table
    $('#aviatorTableContainer').empty().append(table);

}


// Format totals
function FormatTotal(value) {

    return Number.isInteger(value)
        ? value.toFixed(0)
        : value.toFixed(2);

}


// Print Counter Log
function PrintDiv() {

    var printContent = document.getElementById('UserLogPrint');

    if (!printContent) {
        return;
    }

    var printWindow = window.open(
        '',
        '_blank',
        'width=900,height=600'
    );

    if (!printWindow) {
        alert('Please allow popups to print the report.');
        return;
    }

    var printHtml = `
        <!DOCTYPE html>
        <html>
        <head>
            <title>Counter Log</title>

            <link rel="stylesheet"
                  href="/DeveloperCSS/CounterLogs.css" />

        </head>

        <body class="counter-print-page">

            ${printContent.innerHTML}

            <script>
                window.onload = function () {
                    window.print();
                };
            <\/script>

        </body>
        </html>
    `;

    printWindow.document.open();
    printWindow.document.write(printHtml);
    printWindow.document.close();

}
