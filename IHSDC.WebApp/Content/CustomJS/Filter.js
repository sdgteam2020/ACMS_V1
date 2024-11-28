$(document).ready(function () {

    GetCorps();

    $("#ComdId").change(function () {

        GetCorps();

    });
    $("#CorpsId").change(function () {

        GetBde();


    });
});

function GetCorps() {
    var list = '';
    list = '<option value=1>No Corps</option>';
    $.ajax({
        url: '/Master/LoadCorpsNameByCommandId',
        type: 'POST',
        data: { "ComdId": $('#ComdId').val() }, //get the search string
        success: function (result) {


            if (result.length > 0) {




                for (var j = 0; j < result.length; j++) {


                    list += '<option value=' + result[j].Value + '>' + result[j].Text + '</option>';
                }



            }
            $('#CorpsId').html(list);
            $('#CorpsId').val($('#corpsidbind').html());

            GetBde();
        }
    });



}
function GetBde() {
    var data = $('#ComdId').val() + ',' + $('#CorpsId').val();
    var list = '';
    list = '<option value=1>No Bde</option>';
    $.ajax({
        url: '/Master/LoadBdeCATbyId',
        type: 'POST',
        data: { "Alldata": data }, //get the search string
        success: function (result) {


            if (result.length > 0) {




                for (var j = 0; j < result.length; j++) {


                    list += '<option value=' + result[j].Value + '>' + result[j].Text + '</option>';
                }



            }
            $('#BdeCatId').html(list);
            $('#BdeCatId').val($('#BdeCatIdbind').html());
        }
    });



}
