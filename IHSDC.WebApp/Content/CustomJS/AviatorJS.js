function GetUsername(UnitName) {
    var listItem = "";
    listItem = "<option value='" + UnitName +"'>-- Select User --</option>";
    $("#EstablishmentFull").html(listItem);

    $.ajax({
        url: '/AccountController/GetUsername',
        type: 'POST',
        data: { "UnitName": UnitName }, 
        success: function (response) {

            if (response != "null") {
                {
                    for (var i = 0; i < response.length; i++) {
                        listItem += '<option value="' + response[i].Value + '">' + response[i].Text + '</option>';
                    }
                    $("#DEFwdAuth").html(listItem);
                }
            }

        }


    });
}


function SubmitConfirmation(obj, ActionName, titleMessage, Submit) {

    swal({
        title: titleMessage,
        text: "",
        type: "info",
        showCancelButton: true,
        confirmButtonColor: "#DD6B55",
        confirmButtonText: "Yes, Submit it!",
        cancelButtonText: "No, cancel ",
        closeOnConfirm: false,
        closeOnCancel: false
    },
        function (isConfirm) {
            if (isConfirm) {
                var encrypted = $(obj).attr('data-id');
                location.href = ActionName + '?id=' + encrypted + '&submit=' + Submit;
            } else {
                swal("Not Submitted!", "", "info");
            }
        }
    );
}
function SubmitToken(obj, ActionName, titleMessage, Submit) {

    swal({
        title: titleMessage,
        text: "",
        type: "info",
        showCancelButton: true,
        confirmButtonColor: "#DD6B55",
        confirmButtonText: "Ok, Connected!",
        cancelButtonText: "No, cancel ",
        closeOnConfirm: false,
        closeOnCancel: false
    },
        function (isConfirm) {
            if (isConfirm) {

                var encrypted = $(obj).attr('data-id');
                location.href = ActionName;
            } else {
                var encrypted = $(obj).attr('data-id');
                location.href = ActionName;
            }
        }
    );
}

function Case(obj) {
        var strings = $(obj).val();
    var str = strings.toUpperCase();
    $(obj).val(str);
}


function CaseName(obj) {

    if ((e.which < 48 || e.which > 57) && e.which != 46) {
        if (e.which == 8 || e.which == 0) {
            return true;
        }
        else {
            return false;
        }
    }
    var string = $(obj).val();
    var str = string.toLowerCase().replace(/\b[a-z]/g, function (letter) {
        return letter.toUpperCase();
    });
    $(obj).val(str);
}
function SetSuffixLetter(obj) {
    //debugger;
    var arr1 = [], arr2 = [];
    var ArmyNumber = $(obj).val();
    var fixed = "98765432";
    arr1 = ArmyNumber.split('');
    arr2 = fixed.split('');
    var len = arr2.length - arr1.length;
    if (len == 1) {
        ArmyNumber = "0" + ArmyNumber;
    }
    if (len == 2) {
        ArmyNumber = "00" + ArmyNumber;
    }
    if (len == 3) {
        ArmyNumber = "000" + ArmyNumber;
    }
    if (len == 4) {
        ArmyNumber = "0000" + ArmyNumber;
    }
    if (len == 5) {
        ArmyNumber = "00000" + ArmyNumber;
    }
    if (len == 6) {
        ArmyNumber = "000000" + ArmyNumber;
    }
    if (len == 7) {
        ArmyNumber = "0000000" + ArmyNumber;
    }

    var total = 0;
    arr1 = ArmyNumber.split('');
    for (var i = 0; i < arr1.length; i++) {
        var val1 = arr1[i];
        var val2 = arr2[i];
        total += parseInt(val1) * parseInt(val2);
    }
    var rem = total % 11;
    var Sletter = '';
    switch (rem.toString()) {
        case '0':
            Sletter = 'A'
            break;
        case '1':
            Sletter = 'F'
            break;
        case '2':
            Sletter = 'H'
            break;
        case '3':
            Sletter = 'K'
            break;
        case '4':
            Sletter = 'L'
            break;
        case '5':
            Sletter = 'M'
            break;
        case '6':
            Sletter = 'N'
            break;
        case '7':
            Sletter = 'P'
            break;
        case '8':
            Sletter = 'W'
            break;
        case '9':
            Sletter = 'X'
            break;
        case '10':
            Sletter = 'Y'
            break;

    }
    $('#SuffixLetter').val(Sletter.toString());
}



function blockSpecialChar(e) {
    if ((e.which < 48 || e.which > 57) && e.which != 46) {
        if (e.which == 8 || e.which == 0) {
            return true;
        }
        else {
            return false;
        }
    }
}



function onlyAlphaNumeric(event) {

   // debugger;

    if ((event.which < 48 || event.which > 57) && event.which != 46) {
        if (event.which == 8 || event.which == 0) {
            return true;

        }
        else if (((event.charCode > 64 && event.charCode < 91) || (event.charCode > 96 && event.charCode < 123) || event.charCode == 32)) {
            return true;

        }
        else {
            return false;
        }

    }

}

function onlyAlph(event) {

    //debugger;  

    if (((event.charCode > 64 && event.charCode < 91) || (event.charCode > 96 && event.charCode < 123) || event.charCode==32)) {        
            return true;       
        
    }
    else {
        return false;
    }
}




function CheckMinute(obj) {
    //debugger;
    var str = $(obj).val();
    var array = $(obj).val().split(".");
    var val = array[0];
    var pval = array[1];
    if (val.length == 1) {
        val = '0' + val;
        $(obj).val(val + "." + (pval));

    }
    if (pval != undefined && pval != null && pval != "") {
        if (pval.length == 1) {
            pval = '0' + pval;
            $(obj).val(val + "." + (pval));
        }
        pval = pval.substring(0, 2);
        if (parseInt(pval) > 59) {
            var v = pval % 60;
            val = parseInt(val) + 1;
            var mod = v % 5;
            var total = v - mod;
            $(obj).val(val + "." + total);
            var valstr = val.toString();
            if (valstr.length == 1) {
                valstr = '0' + val;
                $(obj).val(valstr + "." + total);
            }
            else {
                $(obj).val(val + "." + total);
            }
        }
        else {
            var mod = (parseInt(pval) % 5);
            var total = (parseInt(pval) - mod);
            $(obj).val(val + "." + total);
            var setstr = total.toString();
            if (setstr.length == 1) {
                setstr = '0' + total;
                $(obj).val(val + "." + (setstr));
            }
            else {
                $(obj).val(val + "." + total);
            }
        }
    }
    else {
        sweetAlert("Please enter correct flying Hrs !", "something like that 00.00", "warning");
        $(obj).val("");
    }
    $(obj).focus();
}

function validateEmail(emailField) {
    var reg = /^([A-Za-z0-9_\-\.])+\@([A-Za-z0-9_\-\.])+\.([A-Za-z]{2,4})$/;

    if (reg.test(emailField.value) == false) {
        //alert('Invalid Email Address');
        swal("Pl fill correct email address!", "Error", "info");
        emailField.value = '';
    }

    return true;

}


function isDate(obj) {

    var datestr = $(obj).val();
    if (datestr != null) {
        //var datePat = /^(\d{2,2})(\/)(\d{2,2})\2(\d{4}|\d{4})$/;
        var datePat = new RegExp("^([0]?[1-9]|[1-2]\\d|3[0-1])/(Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec)/[1-2]\\d{3}$", 'i');

        //var datePat = /^\d{2}\/\d{3}\/\d{4}$/ //Basic check for format validity
        var matchArray = datestr.match(datePat); // is the format ok?
        if (matchArray == null) {
            swal("Date must be in DD/MM/YYYY format", "Error", "info");
            //  alert("Date must be in DD/MMM/YYYY format")
            $(obj).val(null);

        }
    }
}




function back() {
    window.history.back();
}















