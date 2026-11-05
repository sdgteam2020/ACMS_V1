
let baseUrl = "";
$(document).ready(function () {
    $("#pageloader").hide();
});
function CheckDGISLatestVersion() {

    return new Promise((resolve, reject) => {

        $("#pageloader").show();
        $.ajax({
            url: 'https://dgisapp.army.mil:55102/Temporary_Listen_Addresses/FetchUniqueTokenDetails',
            type: 'GET',
            dataType: 'json',
            timeout: 3000,
            success: function () {
                baseUrl = "https://dgisapp.army.mil:55102";
                $('#pageloader').hide();
                resolve();
            },
            error: function () {
                // fallback to previous version
                $.ajax({
                    url: 'http://localhost/Temporary_Listen_Addresses/FetchUniqueTokenDetails',
                    type: 'GET',
                    dataType: 'json',
                    timeout: 3000,
                    success: function () {
                        baseUrl = "http://localhost";
                        $('#pageloader').hide();
                        resolve();
                    },
                    error: function () {
                        $('#pageloader').hide();
                        reject("DGIS not available");
                    }
                });
            }
        });
    });
}
$(document).ready(function () {

    $("#searchSubmit").on("click", function () {
        navigateToSearch();
    });

});
$(document).ready(function () {

    $("#searchSubmit").on("click", function () {
        navigateToSearch();
    });

});
function navigateToSearch() {
    debugger;
    document.querySelector('.table-responsive').style.display = 'block';
    let Armyno = $('#SpnArmyNumber1').html();
    if ($('#spnDocType').html() == 2)

   
    {


       
        CheckDGISLatestVersion()
            .then(() => {
               
                $.ajax({
                    url: baseUrl + '/Temporary_Listen_Addresses/FetchUniqueTokenDetails',
                    type: 'GET',
                    dataType: 'json',
                    timeout: 3000,
                    success: FetchUniqueTokenDetails,
                    error: errorhandle
                });
            })
            .catch((err) => {
                errorhandle(); // or display a custom alert
            });


        function FetchUniqueTokenDetails(response) {
            if (response) {
                if (response[0].Status == '200') {
                    if (response[0].TokenValid == true) {
                        validatePersID2FA();
                    }
                    else {
                        sweetAlert('Token Expired', '', 'info');
                    }

                } else if (response[0].Status == '404') {
                    sweetAlert(response[0].Remarks, '', 'info');
                    $("#tbody").html("");
                    let modal = document.getElementById("PolicyModal");
                    modal.style.display = "none";
                }
            }
        }

        function errorhandle(response) {
            sweetAlert('Hastakshar SEWA Not Running/Not Installed.', 'To install Hastakshar SEWA - Download ADN version of Hastakshar SEWA and run the setup.', 'error');
            $("#tbody").html("");
            let modal = document.getElementById("PolicyModal");
            modal.style.display = "none";
        }

        function validatePersID2FA() {
            $.ajax({
                url: baseUrl + '/Temporary_Listen_Addresses/ValidatePersID2FA',
                type: 'POST',
                contentType: 'application/json',
                data: JSON.stringify({ "inputPersID": Armyno }),
                success: function (result) {
                    if (result) {
                        if (result.ValidatePersID2FAResult === true) {

                            let searchTermValue = window.btoa($('#searchTerm').val());
                            let otherParamValue = window.btoa($('#spnPolicyName').html());
                            let Type = window.btoa($('#spnDocType').html());
                            let Pid = window.btoa($('#spnPId').html());

                            $.ajax({
                                url: '/PolicyCorner/Search',
                                type: 'GET',
                                data: { Term: searchTermValue, id: otherParamValue, type: Type, PLid: Pid },
                                success: function (data) {
                                    $("#dataTable1").removeClass("d-none");
                                    $("#tbody").html(data);
                                    $('#searchTerm').val("");
                                },
                                error: function () {
                                    $("#tbody").html("");
                                    let modal = document.getElementById("PolicyModal");
                                    modal.style.display = "none";
                                }
                            });

                        } else {
                            sweetAlert('You are not authorized for this policy', '', 'info');
                            $("#tbody").html("");
                            let modal = document.getElementById("PolicyModal");
                            modal.style.display = "none";
                        }
                    }
                },
                error: function (xhr, status, error) {
                    console.error(xhr.responseText);
                }
            });
        }
    }
    else {
        let searchTermValue = window.btoa($('#searchTerm').val());
        let otherParamValue = window.btoa($('#spnPolicyName').html());
        let Type = window.btoa($('#spnDocType').html());
        let Pid = window.btoa($('#spnPId').html());

        $.ajax({
            url: '/PolicyCorner/Search',
            type: 'GET',
            data: { Term: searchTermValue, id: otherParamValue, type: Type, PLid: Pid },
            success: function (data) {
                $("#dataTable1").removeClass("d-none");
                $("#tbody").html(data);
                $('#searchTerm').val("");

            },
            error: function () {
                $("#tbody").html("");
                let modal = document.getElementById("PolicyModal");
                modal.style.display = "none";
            }
        });
    }

}


$('.Closepoup').on('click', function (e) {
    let modal = document.getElementById("PolicyModal");
    modal.style.display = "none";
});

$('.CloseShowFilepoup').on('click', function (e) {
    let modal = document.getElementById("ShowFileModal");
    modal.style.display = "none";
});


$('.showpopupFile').on('click', function (e) {
    $('#spnShowFilePolicyName').text($(this).closest("div").find('.spncardid').html());
    $('#spnShowFileDocType').text($(this).closest("div").find('.spnTypeofdocu').html());
    let fileText = window.btoa($('#spnShowFilePolicyName').html());
    let fileType = window.btoa($('#spnShowFileDocType').html());
    $.ajax({
        url: '/PolicyCorner/GetFilesToShow',
        type: 'GET',
        data: { FName: fileText, Type: fileType },
        success: function (data) {
            $("#tShowFilebody").html(data);
            let modal = document.getElementById("ShowFileModal");
            modal.style.display = "block";
        },
        error: function () {
            $("#tShowFilebody").html("");
        }
    });
});


$('.PolicyModelOpen').on('click', function (e) {

    $('#spnPolicyName').text($(this).closest("div").find('.spncardid').html());
    $('#spnDocType').text($(this).closest("div").find('.spnTypeofdocu').html());
    $('#spnPId').text($(this).closest("div").find('.spnPolicyId').html());
    $('#SpnArmyNumber1').text($(this).closest("div").find('.spnArmyNumber').html());

    $("#dataTable1").addClass("d-none");
    let ArmyNo = $(this).closest("div").find('.spnArmyNumber').html();
    let PolicyTmpId = $(this).closest("div").find('.spnPolicyId').html();

    if ($(this).closest("div").find('.spnTypeofdocu').html() == 2)
    
    {

        CheckDGISLatestVersion()
            .then(() => {
             
                $.ajax({
                    url: baseUrl + '/Temporary_Listen_Addresses/FetchUniqueTokenDetails',
                    type: 'GET',
                    dataType: 'json',
                    timeout: 3000,
                    success: FetchUniqueTokenDetails,
                    error: errorhandle
                });
            })
            .catch((err) => {
                errorhandle(); // or display a custom alert
            });

        function FetchUniqueTokenDetails(response) {
            if (response) {
                //    alert(response[0].Status);
                if (response[0].Status == '200') {
                    if (response[0].TokenValid == true) {
                        validatePersID2FA();
                    }
                    else {
                        sweetAlert('Token Expired', '', 'info');
                    }
                } else if (response[0].Status == '404') {
                    sweetAlert(response[0].Remarks, '', 'info');
                    $("#tbody").html("");
                    let modal = document.getElementById("PolicyModal");
                    modal.style.display = "none";
                }
            }
        }

        function errorhandle(response) {

            sweetAlert('Hastakshar SEWA Not Running/Not Installed.', 'To install Hastakshar SEWA - Download ADN version of Hastakshar SEWA and run the setup.', 'error');
           
            $("#tbody").html("");
            let modal = document.getElementById("PolicyModal");
            modal.style.display = "none";
        }

        function validatePersID2FA() {

            $.ajax({
                url: baseUrl + '/Temporary_Listen_Addresses/ValidatePersID2FA',
                type: 'POST',
                contentType: 'application/json',
                data: JSON.stringify({ "inputPersID": ArmyNo }),
                success: function (result) {
                    if (result) {
                        if (result.ValidatePersID2FAResult === true) {
                            SaveDataForTokenAccess();
                            $("#tbody").html("");
                            let modal = document.getElementById("PolicyModal");
                            modal.style.display = "block";
                        } else {
                            sweetAlert('You are not authorized for this policy', '', 'info');
                            $("#tbody").html("");
                            let modal = document.getElementById("PolicyModal");
                            modal.style.display = "none";
                        }
                    }
                },
                error: function (xhr, status, error) {
                    console.error(xhr.responseText);
                }
            });
        }

        function SaveDataForTokenAccess() {

            //   alert('Test123');
            let PlId = window.btoa(PolicyTmpId);
            $.ajax({
                url: '/PolicyCorner/AddAccessLog',
                type: 'POST',
                contentType: 'application/json',
                data: JSON.stringify({ "Pid": PlId }),
                success: function (result) {
                    if (result) {
                        if (result.success === true) {
                        } else {
                            sweetAlert('Log Data Not Saved', '', 'error');
                        }
                    }
                },
                error: function (xhr, status, error) {
                    console.error(xhr.responseText);
                }
            });
        }

    }
    else if ($(this).closest("div").find('.spnTypeofdocu').html() == 0 || $(this).closest("div").find('.spnTypeofdocu').html() == 1) {
        let PlId = window.btoa(PolicyTmpId);
        $.ajax({
            url: '/PolicyCorner/AddAccessLog',
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify({ "Pid": PlId }),
            success: function (result) {
                if (result) {
                    if (result.success === true) {
                    } else {
                        sweetAlert('Log Data Not Saved', '', 'error');
                    }
                }
            },
            error: function (xhr, status, error) {
                console.error(xhr.responseText);
            }
        });

        $("#tbody").html("");
        let modal = document.getElementById("PolicyModal");
        modal.style.display = "block";
    }

    else {

        $("#tbody").html("");
        let modal = document.getElementById("PolicyModal");
        modal.style.display = "block";

    }
});

window.addEventListener("keydown", function (e) {
    if (e.key === "ArrowUp" || e.key === "ArrowDown") {
        e.preventDefault();
    }
});

window.addEventListener("wheel", function (e) {

    e.preventDefault();
});

       
    let input = document.getElementById("searchTerm");
    input.addEventListener("keypress", function (event) {
        if (event.key === "Enter") {
            event.preventDefault();
            document.getElementById("searchSubmit").click();
        }
    });

let marquee = document.getElementById('marquee');

    marquee.addEventListener('mouseover', function () {
        this.stop();
    });

    marquee.addEventListener('mouseout', function () {
        this.start();
    });

let message = '@message';
let messageStatus = '@messageStatus';
let messagemidStatus = '@messagemidStatus ';
    if (message)
        sweetAlert(message, messagemidStatus, messageStatus);


