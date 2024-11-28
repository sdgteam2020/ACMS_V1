
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

function onlyAlphabates(event) {

   // debugger;

    if (((event.charCode > 64 && event.charCode < 91) || (event.charCode > 96 && event.charCode < 123) || event.charCode == 32)) {
        return true;

    }
    else {
        return false;
    }
}

