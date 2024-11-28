$(document).ready(function () {
    GetCorps();
    $("#ComdId").change(function () {
        $("#Command").val($("#ComdId").val());
        GetCorps();
        $('#corpsidbind').html(1);
        $('#BdeCatIdbind').html(1);
        $('#SqnIdbind').html(1);
    });
    $("#CorpsId").change(function () {
        GetBde();
    });
    $("#ComdId").change(function () {
        GetCorps();
    });
    $("#spnCorpsId").change(function () {
        GetBde();
    });
    $("#BdeCatId").change(function () {
        GetSQN();
    });
});

function GetCorps() {
    var list = '';
    list = '<option value=1>-No Corps-</option>';
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
            $('#spnCorpsId').html(list);
            $('#spnCorpsId').val($('#corpsidbind').html());
          
            GetBde();
        }
    });



}
function GetBde() {
    var data = $('#ComdId').val() + ',' + $('#CorpsId').val();
    var list = '';
    list = '<option value=1>-No Bde-</option>';
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
            GetSQN();
        }
    });



}
function GetSQN() {
    var data = $('#ComdId').val() + ',' + $('#CorpsId').val() + ',' + $('#BdeCatId').val();
    var list = '';
    list = '<option value=1>-No SQN-</option>';
    $.ajax({
        url: '/Master/LoadGetSQNId',
        type: 'POST',
        data: { "Alldata": data }, //get the search string
        success: function (result) {

          
            if (result.length > 0) {




                for (var j = 0; j < result.length; j++) {


                    list += '<option value=' + result[j].Value + '>' + result[j].Text + '</option>';
                }



            }
            $('#SqnId').html(list);
            $('#SqnId').val($('#SqnIdbind').html());
        }
    });



}
