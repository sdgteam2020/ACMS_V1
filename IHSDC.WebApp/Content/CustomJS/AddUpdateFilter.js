$(document).ready(function () {
    GetUserLog(1, $('#spnAviator').html(), $('#LogFromDate').val(), $("#LogToDate").val());
});

$('.BtnUserLog').click(function () {
    if ($('#LogFromDate').val() != '' && $('#LogToDate').val() != '') {
        GetUserLog(2, $(this).closest("tr").find(".Aviator_ID").html(), $('#LogFromDate').val(), $("#LogToDate").val(), $(this).closest("tr").find(".User_Name").html());
    }
});


function GetUserLog(Procid, Aviator_ID, FromDate, ToDate, UserName) {
    var User_Name = UserName;
    $('#SpnAviatorName').html(User_Name);

    $.ajax({
        url: '/Master/GetUserLog',
        type: 'POST',
        data: { "Procid": Procid, "UserID": Aviator_ID, "FromDate": FromDate, "ToDate": ToDate }, //get the search string
        success: function (response) {
            if (response && response.length > 0) {
                var table = $('<table>').addClass('table table-striped align-top').attr('id', 'aviatorTable').css('width', '100%').css('table-layout', 'auto');
                var thead = $('<thead>').addClass('bg-info text-white');
                var headerRow = $('<tr>');
                headerRow.append($('<th>').addClass('bg-info text-white').text('Dt & Time'));
                headerRow.append($('<th>').addClass('bg-info text-white').text('IP'));
                headerRow.append($('<th>').addClass('bg-info text-white').text('UnitName'));
                headerRow.append($('<th>').addClass('bg-info text-white').text('UserName'));
                headerRow.append($('<th>').addClass('bg-info text-white').text('FullName'));
                headerRow.append($('<th>').addClass('bg-info text-white').text('Role'));
                headerRow.append($('<th>').addClass('bg-info text-white').text('Remarks'));

                table.append(headerRow);

                response.forEach(function (row) {
                    var dataRow = $('<tr>');
                    dataRow.append($('<td>').text(row.Logindate));
                    dataRow.append($('<td>').text(row.IpAddress));
                    dataRow.append($('<td>').text(row.UnitName));
                    dataRow.append($('<td>').text(row.UserName));
                    dataRow.append($('<td>').text(row.FullName));
                    dataRow.append($('<td>').text(row.RoleName));
                    dataRow.append($('<td>').text(row.Remarks));
                    table.append(dataRow);
                    
                });

                var responsiveDiv = $('<div>').addClass('table-responsive');
                responsiveDiv.append(table);
                $('#aviatorTable').empty().append(responsiveDiv);
                // Initialize DataTables with pagination options
            } else {
                var noRecordMessage = $('<p>')
                    .text('No record found')
                    .css({
                        'text-align': 'center',
                        'font-weight': 'bold',
                        'font-size': '20px',
                        'color': 'red'
                    });
                    $('#aviatorTable').empty().append(noRecordMessage);
            }
        }
    });
}
//function GetUserLog(Procid, Aviator_ID, FromDate, ToDate, UserName) {
//    var User_Name = UserName;
//    $('#SpnAviatorName').html(User_Name);

//    $.ajax({
//        url: '/Master/GetUserLog',
//        type: 'POST',
//        data: { "Procid": Procid, "UserID": Aviator_ID, "FromDate": FromDate, "ToDate": ToDate }, //get the search string
//        success: function (response) {
//            if (response && response.length > 0) {
//                var table = $('<table>').addClass('table table-striped align-top').attr('id', 'aviatorTable').css('width', '100%').css('table-layout', 'auto');
//                var thead = $('<thead>').addClass('bg-info text-white');
//                var headerRow = $('<tr>');
//                headerRow.append($('<th>').addClass('bg-info text-white').text('Dt & Time'));
//                headerRow.append($('<th>').addClass('bg-info text-white').text('IP'));
//                headerRow.append($('<th>').addClass('bg-info text-white').text('UnitName'));
//                headerRow.append($('<th>').addClass('bg-info text-white').text('UserName'));
//                headerRow.append($('<th>').addClass('bg-info text-white').text('FullName'));
//                headerRow.append($('<th>').addClass('bg-info text-white').text('Role'));

//                table.append(headerRow);

//                response.forEach(function (row) {
//                    var dataRow = $('<tr>');
//                    dataRow.append($('<td>').text(row.Logindate));
//                    dataRow.append($('<td>').text(row.IpAddress));
//                    dataRow.append($('<td>').text(row.UnitName));
//                    dataRow.append($('<td>').text(row.UserName));
//                    dataRow.append($('<td>').text(row.FullName));
//                    dataRow.append($('<td>').text(row.RoleName));
//                    table.append(dataRow);
//                });

//                var responsiveDiv = $('<div>').addClass('table-responsive');
//                responsiveDiv.append(table);
//                $('#aviatorTable').empty().append(responsiveDiv);

//                // Initialize DataTables
//                $('#aviatorTable').DataTable({
//                    "paging": true, // Enable pagination
//                    "ordering": true, // Enable ordering
//                    "searching": true // Enable search box
//                });

//            } else {
//                var noRecordMessage = $('<p>')
//                    .text('No record found')
//                    .css({
//                        'text-align': 'center',
//                        'font-weight': 'bold',
//                        'font-size': '20px',
//                        'color': 'red'
//                    });
//                $('#aviatorTable').empty().append(noRecordMessage);
//            }
//        }
//    });
//}
