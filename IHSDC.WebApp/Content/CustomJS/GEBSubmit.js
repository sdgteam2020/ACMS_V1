
function SubmitGEBLetter(obj, ActionName, titleMessage, Submit) {

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
				var CommandId = $(obj).attr('data-cid');
                location.href = ActionName + '?id=' + encrypted + '&submit=' + Submit + '&Comd=' + CommandId;

			} else {
				swal("Not Submitted!", "", "info");
			}
		}
	);
}



function SubmitGEBConfirmation(obj, ActionName, titleMessage, Submit) {

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
                var id = $(obj).attr('data-id');
                var fid = $(obj).attr('data-fid');
                location.href = ActionName + '?id=' + id + '&fid=' + fid;

            } else {
                swal("Not Submitted!", "", "info");
            }
        }
    );
}


function MailRevertConfirmation(obj, ActionName, titleMessage, Submit) {
    swal({
        title: titleMessage,
        text: "",
        type: "info",
        showCancelButton: true,
        confirmButtonColor: "#DD6B55",
        confirmButtonText: "Yes, Delete it!",
        cancelButtonText: "No, cancel",
        closeOnConfirm: false,
        closeOnCancel: false
    },
        function (isConfirm) {
            if (isConfirm) {
                var id = $(obj).attr('data-id');
                var fid = $(obj).attr('data-fid');

                var data = {
                    id: id,
                    fid: fid
                };
                $.ajax({
                    url: ActionName,
                    type: "POST",
                    data: data,
                    success: function (response) {
                        if (response.success) {
                            swal(response.Msg, "", "success");
                            location.href = ActionName;
                        }
                        else
                        {
                            swal(response.Msg, "", "warning");
                        }
                    },
                    error: function () {
                        swal("Error Deleting!", "", "error");
                    }
                });

            } else {
                swal("Not Deleted!", "", "info");
            }
        });
}


function SubmitGEBLetterFor(obj, ActionName, titleMessage, Submit) {
    //debugger;
    var parameters = [];
    var encrypted, submit, CommandId;
    var isOk = false;
    submit = "SubmitGSO1AA7";
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
                ///debugger;
                encrypted = $(obj).attr('data-id');
                CommandId = $(obj).attr('data-cid');
                $("#childTable tr").each(function () {

                    if ($(this).find("#chkcheckbox").is(":checked")) {
                        isOk = true;
                    }

                });

                if (isOk) {
                    $("#childTable tr").each(function () {
                        //  if ($(this).find("#chkcheckbox").is(":checked")) {
                        var IsNotification = true;
                        var value = $(this).find("#chkcheckbox").is(":checked");
                        if (value == true) {
                            IsNotification = false;
                        }
                            parameters.push({
                                ChildId: $(this).find("#ChildId").val(),
                                RoleName: $(this).find("#RoleName").val(),
                                UserName: $(this).find("#UserName").val(),
                                TypeOfUnit: $(this).find("#TypeOfUnit").val(),
                                IsSubmit: value,
                                IsNotification: IsNotification
                            });
                      // }
                    });
                    $.ajax({
                        url: "/GEB/SubmitGEBLetter",
                        type: 'Post',
                        data: { "id": encrypted, "submit": submit, "Comd": CommandId, "Detailsstr": JSON.stringify(parameters) },
                        
                        success: function (result) {
                            if (result) {
                              
                                swal('Letter Forwarded to Command  Successfully !', '', 'success')
                               
                            } else {
                                swal('Something  !', '', 'error')
                            }
                            
                        },
                        error: function (xhr, status) {
                            swal('Some problem occur !', '', 'error')
                            // alert(status);
                        }
                    });
                    location.reload();


                }
                else {
                    swal("Please Select Any Command For GEB Apprearing!", "", "warning");
                    isOk = false;
                    return false;

                    if (!isOk) {
                        e.preventDefault(e);
                        return;
                    }
                }


            }


            else {
                swal("Not Submitted!", "", "info");
            }
        }
    );
}

function GetChilds(obj) {    
    $("#childTable tr ").remove();
    var cid = $(obj).attr('data-cid');
    var gid = $(obj).attr('data-gid');
    $.ajax({
        url: "/GEB/Childs",
        type: 'GET',
        data: { "ComdId": cid, "Gid": gid },
        //async: false,
        success: function (result) {
            if (result) {
                $.each(result, function (i, r) {
                    
                    var html = '<tr>' +                       
                        '<td  style="font-weight:bold;width: 10%;">' + '<label>' + r.UserName + ' </label>'  + '</td>' +                        
                        '<td  style="font-weight:bold;width: 10%;">' + '<input id="chkcheckbox"  type="checkbox" />' + '</td>' +
                        '<td style="display:none">' + '<input id="ChildId"  value=' + r.ChildId + ' type="hidden" />' + '</td>' +
                        '<td style="display:none">' + '<input id="RoleName"  value=' + r.RoleName + ' type="hidden" />' + '</td>' +
                        '<td style="display:none">' + '<input id="UserName"  value=' + r.UserName + ' type="hidden" />' + '</td>' +
                        '<td style="display:none">' + '<input id="TypeOfUnit"  value=' + r.TypeOfUnit + ' type="TypeOfUnit" />' + '</td>' +                        
                        '</tr>'
                    
                    $(html).appendTo($("#childTable"));
                    ///$('#childTable ').append('<td>' + html + '</td>');
                    //$("#childTable").append($('<td>' + html + '</td>'));
                })
               
                // swal('success', '', 'success')
            } else {
                swal('Something  !', '', 'error');
            }
        },
        error: function (xhr, status) {
            swal('Some problem occur !', '', 'error');
            // alert(status);
        }
    });
   
}





function GetChildsList(obj) {
    $("#childTable tr ").remove();
    var cid = $(obj).attr('data-cid');
    var gid = $(obj).attr('data-gid');
    $.ajax({
        url: "/GEB/GetChildsList",
        type: 'GET',
        data: { "ComdId": cid, "Gid": gid },
        //async: false,
        success: function (result) {
            if (result) {
                $.each(result, function (i, r) {

                    var html = '<tr>' +
                        '<td  style="font-weight:bold;width: 10%;">' + '<label>' + r.UserName + ' </label>' + '</td>' +
                        '<td  style="font-weight:bold;width: 10%;">' + '<label>' + r.TypeOfUnit + ' </label>' + '</td>' +
                        '</tr>';

                    $(html).appendTo($("#childTable"));

                });

                // swal('success', '', 'success')
            } else {
                swal('Something  !', '', 'error');
            }
        },
        error: function (xhr, status) {
            swal('Some problem occur !', '', 'error');
            // alert(status);
        }
    });

}


function SubmitAppx(obj, ActionName, titleMessage, Submit) {
   // debugger;
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
                location.href = ActionName + '?fid=' + encrypted + '&submit=' + Submit;
            } else {
                swal("Not Submitted!", "", "info");
            }
        }
    );
}