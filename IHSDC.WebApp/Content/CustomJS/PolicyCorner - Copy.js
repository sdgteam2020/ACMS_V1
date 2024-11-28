function navigateToSearch() {
    

    document.querySelector('.table-responsive').style.display = 'block';
    var Armyno = $('#SpnArmyNumber1').html();
    
  if ($('#spnDocType').html() == 2)
     
    {
        /*"7f33df8ac6540b5cf7ccfd041d8c837641226444d9f1a4aa30a01924c0610996"*/
        $.ajax({
            url: 'http://localhost/Temporary_Listen_Addresses/FetchUniqueTokenDetails',
            type: 'GET',
            dataType: 'json',
            timeout: 3000,
            success: FetchUniqueTokenDetails,
            error: errorhandle
        });

        function FetchUniqueTokenDetails(response) {
            if (response) {
                if (response[0].Status == '200') {
                    //  if (response[0].TokenValid == true) {
                    if (response[0].TokenValid == false) {
                        validatePersID2FA();
                    }
                    else {
                        sweetAlert('Token Expired 1234', '', 'info');
                    }

                } else if (response[0].Status == '404') {
                    sweetAlert(response[0].Remarks, '', 'info');
                    $("#tbody").html("");
                    var modal = document.getElementById("PolicyModal");
                    modal.style.display = "none";
                }
            }
        }

        function errorhandle(response) {
            sweetAlert('DGIS Application Not Running/Not Installed.', 'To install DGIS App - Download ADN version of DGIS App and run the setup.', 'error');
            $("#tbody").html("");
            var modal = document.getElementById("PolicyModal");
            modal.style.display = "none";
        }

        function validatePersID2FA() {
            $.ajax({
                url: 'http://localhost/Temporary_Listen_Addresses/ValidatePersID2FA',
                type: 'POST',
                contentType: 'application/json',
                data: JSON.stringify({ "inputPersID": Armyno }),
                success: function (result) {
                    if (result) {
                        // if (result.ValidatePersID2FAResult === true) {
                        if (result.ValidatePersID2FAResult === false) {

                            var searchTermValue = window.btoa($('#searchTerm').val());
                            var otherParamValue = window.btoa($('#spnPolicyName').html());
                            var Type = window.btoa($('#spnDocType').html());
                            var Pid = window.btoa($('#spnPId').html());

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
                                    var modal = document.getElementById("PolicyModal");
                                    modal.style.display = "none";
                                }
                            });

                        } else {
                            sweetAlert('You are not authorized for this policy 2', '', 'info');
                            $("#tbody").html("");
                            var modal = document.getElementById("PolicyModal");
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
        var searchTermValue = window.btoa($('#searchTerm').val());
        var otherParamValue = window.btoa($('#spnPolicyName').html());
        var Type = window.btoa($('#spnDocType').html());
        var Pid = window.btoa($('#spnPId').html());

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
                var modal = document.getElementById("PolicyModal");
                modal.style.display = "none";
            }
        });
    }

}


$('.Closepoup').on('click', function (e) {
    var modal = document.getElementById("PolicyModal");
    modal.style.display = "none";
});

$('.CloseShowFilepoup').on('click', function (e) {
    var modal = document.getElementById("ShowFileModal");
    modal.style.display = "none";
});


$('.showpopupFile').on('click', function (e) {
    $('#spnShowFilePolicyName').text($(this).closest("div").find('.spncardid').html());
    $('#spnShowFileDocType').text($(this).closest("div").find('.spnTypeofdocu').html());
    var fileText = window.btoa($('#spnShowFilePolicyName').html());
    var fileType = window.btoa($('#spnShowFileDocType').html());
    $.ajax({
        url: '/PolicyCorner/GetFilesToShow',
        type: 'GET',
        data: { FName: fileText, Type: fileType },
        success: function (data) {
            $("#tShowFilebody").html(data);
            var modal = document.getElementById("ShowFileModal");
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
    var ArmyNo = $(this).closest("div").find('.spnArmyNumber').html();
    var PolicyTmpId = $(this).closest("div").find('.spnPolicyId').html();
   
 if ($(this).closest("div").find('.spnTypeofdocu').html() == 2) 
    {
        /*"7f33df8ac6540b5cf7ccfd041d8c837641226444d9f1a4aa30a01924c0610996"*/
        $.ajax({
            url: 'http://localhost/Temporary_Listen_Addresses/FetchUniqueTokenDetails',
            type: 'GET',
            dataType: 'json',
            timeout: 3000,
            success: FetchUniqueTokenDetails,
            error: errorhandle
        });

        function FetchUniqueTokenDetails(response) {
            if (response) {
                if (response[0].Status == '200') {
                    if (response[0].TokenValid == false) {
                        validatePersID2FA();
                    }
                    else {
                        sweetAlert('Token Expired 123', '', 'info');
                    }
                } else if (response[0].Status == '404') {
                    sweetAlert(response[0].Remarks, '', 'info');
                    $("#tbody").html("");
                    var modal = document.getElementById("PolicyModal");
                    modal.style.display = "none";
                }
            }
        }

        function errorhandle(response) {
            sweetAlert('DGIS Application Not Running/Not Installed.', 'To install DGIS App - Download ADN version of DGIS App and run the setup.', 'error');
            //sweetAlert({
            //    title: 'DGIS Application Not Running/Not Installed.\n To install DGIS App - Download ADN version of DGIS App and run the setup',
            //    icon: 'info',
            //    html: '<span style="font-size: 12px;">DGIS Application Not Running<br>Not Installed</span>'
            //});
            $("#tbody").html("");
            var modal = document.getElementById("PolicyModal");
            modal.style.display = "none";
        }

        function validatePersID2FA() {
            $.ajax({
                url: 'http://localhost/Temporary_Listen_Addresses/ValidatePersID2FA',
                type: 'POST',
                contentType: 'application/json',
                data: JSON.stringify({ "inputPersID": ArmyNo }),
                success: function (result) {
                    if (result) {
                      //  if (result.ValidatePersID2FAResult === true) {
                       if (result.ValidatePersID2FAResult === false) {
                            SaveDataForTokenAccess();
                            $("#tbody").html("");
                            var modal = document.getElementById("PolicyModal");
                            modal.style.display = "block";
                        } else {
                            sweetAlert('You are not authorized for this policy 1', '', 'info');
                            $("#tbody").html("");
                            var modal = document.getElementById("PolicyModal");
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
            var PlId = window.btoa(PolicyTmpId);
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
     var PlId = window.btoa(PolicyTmpId);
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
     var modal = document.getElementById("PolicyModal");
     modal.style.display = "block";
 }
    else {
        $("#tbody").html("");
        var modal = document.getElementById("PolicyModal");
        modal.style.display = "block";
    }
});