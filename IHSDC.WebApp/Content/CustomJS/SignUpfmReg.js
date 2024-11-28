$(document).ready(function () {
    
    $('#ComdIdUnit').change(function () { 
      
        if ($('#ComdIdUnit').val() == "") {        
            list = '';         
            $('#spnCorpUnit').html(list);
            $("#CorpId").val("1");
        }
        else {    

           
            $('#CorpId').val(1);
            $('#BdeId').val(1);
            BindCorpUnit($("#ComdIdUnit").val());
            BindBde($("#ComdIdUnit").val(), $("#CorpId").val());
            BindUnitUnit($("#ComdIdUnit").val(), $("#CorpId").val(), $("#BdeId").val());
        }        
    });
});
function BindCorp(ComdId) {
   
    var list = '';
    $.ajax({
        url: '/SignUp/LoadCorpsNameByCommandId',
        type: 'POST',
        data: { "ComdId": ComdId },

        success: function (result) {
          
            if (result.length > 0) {
            
                list = '';
                list = '<option value=1>-No DGs-</option>';
                for (var j = 0; j < result.length; j++) {
                    list += '<option value=' + result[j].Value + '>' + result[j].Text + '</option>';
                }
                $('#spnCorp').html(list);
            }
            else {
                list = '';
                list = '<option value=1>-No DGs-</option>';
                $('#spnCorp').html(list);
                $("#CorpId").val("1");

            }
            if ($("#CorpId").val() != 0) {
                $('#spnCorp').val($("#CorpId").val());
            }
        }
    });
}



function BindCorpUnit(ComdId) {


    var list = '';
    $.ajax({
        url: '/SignUp/LoadCorpsNameByCommandId',
        type: 'POST',
        data: { "ComdId": ComdId },

        success: function (result) {
          
            if (result.length > 0) {

                list = '';
                list = '<option value=1>-No DGs-</option>';
                for (var j = 0; j < result.length; j++) {
                    list += '<option value=' + result[j].Value + '>' + result[j].Text + '</option>';
                }
                $('#spnCorpUnit').html(list);
            }
            else {
                list = '';
                list = '<option value=1>-No DGs-</option>';
                $('#spnCorpUnit').html(list);
                $("#CorpId").val("1");

            }
            if ($("#CorpId").val() != 0) {
                $('#spnCorpUnit').val($("#CorpId").val());
            }
        }
    });
}

function BindBde(ComdId, CorpId) {
    var data = ComdId + ',' + CorpId;
    var list = '';
    $.ajax({
        url: '/SignUp/LoadBdeCATbyId',
        type: 'POST',
        data: { "Alldata": data },
     
        success: function (result) {
          
            if (result.length > 0) {

                list = '';
                list = '<option value=1>-No Bde-</option>';
                for (var j = 0; j < result.length; j++) {
                    list += '<option value=' + result[j].Value + '>' + result[j].Text + '</option>';
                }
                $('#spnBde').html(list);
            }
            else {
                list = '';
                list = '<option value=1>-No Bde-</option>';
                $('#spnBde').html(list);
                $("#BdeId").val("1");
            }
            if ($("#BdeId").val() != 0) {
                $('#spnBde').val($("#BdeId").val());
            }
        }
    });
}

function BindUnit(ComdId, CorpId, BdeId) {
    var data = ComdId + ',' + CorpId + ',' + BdeId;
  

    var list = '';
    $.ajax({
        url: '/SignUp/LoadUnitbyId',
        type: 'POST',
        data: { "Alldata": data },

        success: function (result) {
          
            if (result.length > 0) {

                list = '';
                list = '<option value="">Select Br</option>';
                for (var j = 0; j < result.length; j++) {
                    list += '<option value=' + result[j].Value + '>' + result[j].Text + '</option>';
                }
                $('#spnUnit').html(list);
               
            }
            else {
                list = '';
                list = '<option value="">Select Br</option>';
                $('#spnUnit').html(list);
               
            }
            if ($("#Unit_ID").val() != 0) {
                $('#spnUnit').val($("#Unit_ID").val());
               
            }
        }
    });
}


function BindUnitUnit(ComdId, CorpId, BdeId) {
    var data = ComdId + ',' + CorpId + ',' + BdeId;
   
    var list = '';
    $.ajax({
        url: '/SignUp/LoadUnitbyId',
        type: 'POST',
        data: { "Alldata": data },

        success: function (result) {
         
            if (result.length > 0) {
              
                list = '';
                list = '<option value="">Select Br</option>';
                for (var j = 0; j < result.length; j++) {
                    list += '<option value=' + result[j].Value + '>' + result[j].Text + '</option>';
                }
                $('#spnUnit').html(list);
                
            }
            else {
           
                list = '';
                list = '<option value="">Select Br</option>';
                $('#spnUnit').html(list);
              
            }
            if ($("#Unit_ID").val() != 0) {
               
                $('#spnUnit').val($("#Unit_ID").val());
                
            }
        }
    });
}
