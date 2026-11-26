$(document).ready(function () {
    GetUserLog(17, $('#spnAviator').html());
});
$('.BtnUserLog').click(function () {
    if ($('#LogFromDate').val() != '' && $('#LogToDate').val() != '') {
        GetUserLog(18, $(this).closest("tr").find(".Aviator_ID").html());
    }
});
function GetUserLog(Procid, Aviator_ID) {
    debugger;
    $.ajax({
        url: '/Master/GetCounterLog',
        type: 'POST',
        data: { "Procid": Procid, "Command": Aviator_ID},
        success: function (response) {
          
            if (response && response.length > 0) {
                var table = $('<table>')
                    .addClass('table table-striped align-top custom-aviator-table')
                    .attr('id', 'aviatorTable');
                var thead = $('<thead>').addClass('bg-info text-white');
                var headerRow = $('<tr>');  
                headerRow.append($('<th>').addClass('bg-info text-white').text('Ser No'));
                headerRow.append($('<th>').addClass('bg-info text-white').text('Comd Name'));
                headerRow.append($('<th>').addClass('bg-info text-white').text('UserName'));
                headerRow.append($('<th>').addClass('bg-info text-white').text('Today'));
                headerRow.append($('<th>').addClass('bg-info text-white').text('Current Month'));
                headerRow.append($('<th>').addClass('bg-info text-white').text('Total'));   
                table.append(headerRow);
                var grandTotal = 0;
                var TodayTotal = 0;
                var MonthTotal = 0;
                response.forEach(function (row, index) {
                    var dataRow = $('<tr>'); 
                    dataRow.append($('<td>').text(index + 1));
                    dataRow.append($('<td>').text(row.ComdName));
                    dataRow.append($('<td>').text(row.UserName));
                    dataRow.append($('<td>').text(row.Today));
                    dataRow.append($('<td>').text(row.CurrentMonth));
                    dataRow.append($('<td>').text(row.Total));
                    grandTotal += row.Total;
                    TodayTotal += row.Today;
                    MonthTotal += row.CurrentMonth;
                    table.append(dataRow);
                });
                var formattedTotal = grandTotal % 1 === 0 ? grandTotal.toFixed(0) : grandTotal.toFixed(2);
                var formattedTodayTotal = TodayTotal % 1 === 0 ? TodayTotal.toFixed(0) : TodayTotal.toFixed(2);
                var formattedMonthTotal = MonthTotal % 1 === 0 ? MonthTotal.toFixed(0) : MonthTotal.toFixed(2);
                 
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
                        .text(formattedTodayTotal)
                );

                footerRow.append(
                    $('<td>')
                        .addClass('bold-text top-border-black')
                        .text(formattedMonthTotal)
                );

                footerRow.append(
                    $('<td>')
                        .addClass('bold-text top-border-black')
                        .text(formattedTotal)
                );            
                table.append(footerRow);



                var responsiveDiv = $('<div>').addClass('table-responsive');
                responsiveDiv.append(table);
                $('#aviatorTable').empty().append(responsiveDiv);               
            } else {
                var noRecordMessage = $('<p>')
                    .text('No record found')
                    .addClass('no-record-message');
                    $('#aviatorTable').empty().append(noRecordMessage);
            }
        }
    });
}
