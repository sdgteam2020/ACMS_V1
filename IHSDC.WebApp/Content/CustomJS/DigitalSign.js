var baseUrl = "";
$(document).ready(function () {
    CheckDGISLatestVersion();

});
function CheckDGISLatestVersion() {
    $.ajax({
        url: 'https://dgisapp.army.mil:55102/Temporary_Listen_Addresses/FetchUniqueTokenDetails',
        type: 'GET',
        dataType: 'json',
        timeout: 3000,
        success: function () {
            baseUrl = "https://dgisapp.army.mil:55102";
            localStorage.setItem("dgisBaseUrl", baseUrl);
        },
        error: function () {
            CheckDGISPreviousVersion();
        }
    });
}

function CheckDGISPreviousVersion() {
    $.ajax({
        url: 'http://localhost/Temporary_Listen_Addresses/FetchUniqueTokenDetails',
        type: 'GET',
        dataType: 'json',
        timeout: 3000,
        success: function () {
            baseUrl = "http://localhost";
            localStorage.setItem("dgisBaseUrl", baseUrl);
        },
        error: function () {
            
        }
    });
}
$('#btnSignAgendapoint').click(function () {
       $.ajax({
           url: baseUrl+'/Temporary_Listen_Addresses/FetchUniqueTokenDetails',
        type: 'GET',
        dataType: 'json',
        timeout: 3000,
        success: FetchUniqueTokenDetails,
        error: errorhandle
    });
});

function FetchUniqueTokenDetails(response) {
    if (response) {
        if (response[0].Status == '200') {
            $('#spnThumbprint').html(response[0].Thumbprint);

            if (response[0].TokenValid == true) {
                SignPdf($('#spnThumbprint').html(), $('#SpnUploadFile').html(), $('#spnInboxId').html());
            }
            else {
                sweetAlert('Token Expired', '', 'info');
            }

        } else if (response[0].Status == '404') {
            sweetAlert(response[0].Remarks, '', 'info');
        }
    }
}

function errorhandle(response) {
    sweetAlert('Hastakshar SEWA Not Running/Not Installed.', 'To install Hastakshar SEWA - Download ADN version of Hastakshar SEWA and run the setup.', 'error');
}

function SignPdf(thumbprint, pathofpdf, InboxId) {
    var EncPath = window.btoa(pathofpdf)
    $.ajax({
        url: baseUrl+'/Temporary_Listen_Addresses/ByteDigitalSignAsync',
        type: 'POST',
        contentType: 'application/json',
        data: JSON.stringify([{ "Thumbprint": thumbprint, "XCoordinate": 40, "YCoordinate": 65, "pdfpath": pathofpdf }]),
        success: function (result) {
            if (result) {
                if (result.Valid) {
                    $.ajax({
                        type: "POST",
                        url: "/ACC/SignAgendaPoint",
                        data: { "file": result.Message, "path": EncPath },
                        dataType: "json",
                        timeout: 60000,
                        success: function (response) {
                           
                            if (response.success) {
                                sweetAlert("Agenda Point Signed Successfully !");
                                location.reload(true);
                            } else {
                                sweetAlert("An error occurred while Signing the PDF.");
                            }
                        },
                        error: function (error) {
                            sweetAlert("Some Error Occured while merging PDF !");
                        }
                    });
                }
                else {
                    sweetAlert("PDF not valid");
                }
            }
        },
        error: function (xhr, status, error) {
            console.error(xhr.responseText);
        }
    });

}