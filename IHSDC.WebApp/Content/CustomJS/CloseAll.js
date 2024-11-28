$("#closeAP").click(function () {
    var CloseApIds = "";
   
    var table = document.getElementById("Inbox");
    var rows = table.getElementsByTagName("tr");

    for (var i = 1; i < rows.length; i++) {
        var row = rows[i];
        var checkbox = row.querySelector("#ChkAPClose");
        if (checkbox && checkbox.checked) {
            
            var closeapidList = row.querySelector("#CloseAPId");
            CloseApIds = CloseApIds + ',' + closeapidList.value;
        }
    }
    var CloseId = CloseApIds.substring(1);

    var requestData = {
        CloseIdList: CloseId
    };


    $.ajax({
        type: "POST",
        url: "/ACC/CloseAllAgendaPoints",
        data: requestData,
        dataType: "json",
        timeout: 60000,
        success: function (response) {
            if (response.success) {
                sweetAlert("Agenda Points Closed Successfully !");
                location.href = '/ACC/AddInboxNotingFwd'
            } else {
                sweetAlert("An error occurred while Closing AP.");
            }
        },
        error: function (error) {

            sweetAlert("An error occurred while Closing AP.");
        }
    });

});