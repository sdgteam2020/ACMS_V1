$(document).ready(function () {
   
    if ($("#usertypeid").html() == "4") {

       
        GetSQN(0, 0, 0, 0);
    }
    if ($("#usertypeid").html() == "3") {


        GetBde(0, 0, 0, 0);
    }
    if ($("#usertypeid").html() == "5") {
        GetUnit(0, 0, 0, 0);
    }
    if ($("#usertypeid").html() == "1") {

        GetCorps(0, 0, 0, 0);
    }
    if ($("#usertypeid").html() == "8") {

        GetComand(0,0,0,0);
    }
    $("#SqnId").change(function () {
        GetUnit($("#ComdId").val(), $("#CorpsId").val(), $("#BdeCatId").val() ,$("#SqnId").val());
    });
    $("#BdeCatId").change(function () {

       
        GetSQN($("#ComdId").val(), $("#CorpsId").val() ,$("#BdeCatId").val(),0);
    });


    $("#ComdId").change(function () {
        GetCorps($("#ComdId").val(),0,0,0);
    });
    $("#CorpsId").change(function () {

        GetBde($("#ComdId").val() ,$("#CorpsId").val(),0,0);


    });
});
function GetUnit(ComndId, CorpsId, BdeId, SqnId) {
    var list = '';
    list = '<option value=0>-All Unit-</option>';
    $.ajax({
        url: '/Master/LoadUnitAll',
        type: 'POST',
        data: { "ComndId": ComndId, "CorpsId": CorpsId, "BdeId": BdeId, "SqnId": SqnId }, //get the search string
        success: function (result) {


            if (result.length > 0) {




                for (var j = 0; j < result.length; j++) {


                    list += '<option value=' + result[j].Id + '>' + result[j].Text + '</option>';
                }



            }
            $('#UnitId').html(list);
            
        }
    });



}
function GetCorps(ComndId, CorpsId, BdeId, SqnId) {
    var list = '';
    list += '<option value=0>All Corps</option>';
    list += '<option value=1>No Corps</option>';
    $.ajax({
        url: '/Master/LoadCrpsAll',
        type: 'POST',
        data: { "ComndId": ComndId, "CorpsId": CorpsId, "BdeId": BdeId, "SqnId": SqnId }, //get the search string
        success: function (result) {


            if (result.length > 0) {


                
               
                for (var j = 0; j < result.length; j++) {

                    if (result[j].Id != 1) {
                        list += '<option value=' + result[j].Id + '>' + result[j].Text + '</option>'
                    }

                    
                }


               
            }
            $('#CorpsId').html(list);
           
        }
    });



}
function GetBde(ComndId, CorpsId, BdeId, SqnId) {
   
    var list = '';
    list = '<option value=0>-Select All-</option>';
    $.ajax({
        url: '/Master/LoadBdeAll',
        type: 'POST',
        data: { "ComndId": ComndId, "CorpsId": CorpsId, "BdeId": BdeId, "SqnId": SqnId }, //get the search string
        success: function (result) {


            if (result.length > 0) {


                for (var j = 0; j < result.length; j++) {


                    list += '<option value=' + result[j].Id + '>' + result[j].Text + '</option>'
                }



            }
            $('#BdeCatId').html(list);
           
           
        }
    });



}
function GetSQN(ComndId, CorpsId, BdeId, SqnId) {
   
    var list = '';
    list = '<option value=0>-All SQN-</option>';
    $.ajax({
        url: '/Master/LoadSqnAll',
        type: 'POST',
        data: { "ComndId": ComndId, "CorpsId": CorpsId, "BdeId": BdeId, "SqnId": SqnId }, //get the search string
        success: function (result) {


            if (result.length > 0) {




                for (var j = 0; j < result.length; j++) {


                    list += '<option value=' + result[j].Id + '>' + result[j].Text + '</option>';
                }



            }
            $('#SqnId').html(list);
            
        }
    });



}
function GetComand(ComndId, CorpsId, BdeId, SqnId) {
    var list = '';

    list = '<option value=0>-Select Comd-</option>';
    $.ajax({
        url: '/Master/LoadComdAll',
        type: 'POST',
        data: { "ComndId": ComndId, "CorpsId": CorpsId, "BdeId": BdeId, "SqnId": SqnId}, //get the search string
        success: function (result) {


            if (result.length > 0) {




                for (var j = 0; j < result.length; j++) {


                    list += '<option value=' + result[j].Id + '>' + result[j].Text + '</option>';
                }



            }
            $('#ComdId').html(list);

        }
    });



}