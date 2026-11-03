
    $(document).ready(function () {
        $('.BtnUserLog').click(function () {
            
            let row = $(this).closest('tr');
            let aviatorId = row.find('.Aviator_ID').text();
            let userName = row.find('.User_Name').text();


            $('#ReturnUrl').val('APLOG^' + userName);

            
          $.ajax({
            url: '@Url.Action("clientIp", "Master")',
            type: 'GET',
            dataType: 'json',
            success: function(response) {
                if (response === 1) {
                    $.ajax({
                        url: '@Url.Action("Login", "Account")',
                        type: 'POST',
                        data: $('#myForm').serialize(),
                        success: function(response) {
                            if (response == 1) {
                                window.location.href = '@Url.Action("AddInboxNotingFwd", "ACC")';
                            }
                        },
                        error: function(xhr, status, error) {
                            alert('Error: ' + error);
                        }
                    });
                    return true;
                } else {
                    alert("Error. Please Contact System Admin");
                    return false;
                }
            },
            error: function(xhr, status, error) {
                alert('Error:', error);
            }
        });
        });
    });




        $('.CloseLogpoup').on('click', function (e) {
            let modal = document.getElementById("APUserLog");
            modal.style.display = "none";
        });




        $(document).ready(function () {
            $('#dataTable').DataTable({
                paging: true, 
                info: true, 
                "lengthMenu": [5, 10, 25, 50], 
                "pageLength": 5 
            });
        });

    function PrintDiv() {
        let divToPrint = document.getElementById('APUserLog');
        let popupWin = window.open('', '_blank', 'top=100,width=900,height=500,location=no');
        popupWin.document.open();
        let table_style = document.getElementById("table_style").innerHTML;
        popupWin.document.write('<style type = "text/css">');
        popupWin.document.write(table_style);
        popupWin.document.write('</style>')
        popupWin.document.title = 'MonthWiseUsersLog';
        popupWin.document.write('<html><body onload="window.print()">' + divToPrint.innerHTML + '<div class="custom-watermark">' + '@(ViewBag.ipadd)' + "" + '</html>');
        popupWin.document.close();
    }


