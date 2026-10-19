
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
				let encrypted = $(obj).attr('data-id');
				let CommandId = $(obj).attr('data-cid');
                location.href = ActionName + '?id=' + encrypted + '&submit=' + Submit + '&Comd=' + CommandId;

			} else {
				swal("Not Submitted!", "", "info");
			}
		}
	);
}



$(document).on("click", ".btnGEBConfirmation", function (e) {

    e.preventDefault();

    debugger;

    let obj = this;

    let ActionName = $(this).attr("data-action");
    let titleMessage = $(this).attr("data-title");
    let Submit = $(this).attr("data-submit");

    console.log("Action URL:", ActionName);
    console.log("Title:", titleMessage);
    console.log("Submit:", Submit);
    console.log("Schedule ID:", $(this).attr("data-id"));
    console.log("Status:", $(this).attr("data-fid"));

    SubmitGEBConfirmation(obj, ActionName, titleMessage, Submit);

});

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
                let id = $(obj).attr('data-id');
                let fid = $(obj).attr('data-fid');
                location.href = ActionName + '?id=' + id + '&fid=' + fid;

            } else {
                swal("Not Submitted!", "", "info");
            }
        }
    );
}
$(document).on("click", ".btnMailRevert", function (e) {

    e.preventDefault();

    debugger;
    let id = $(this).attr("data-id");
    let fid = $(this).attr("data-fid");
    const payload = {

        Ord: id,
        FwdId: fid,      
    };
    let jsonData = JSON.stringify(payload);
    let encrypted;
    try {
        encrypted = encryptPayloadData(jsonData);
    }
    catch (error) {

        console.error("Encryption Error:", error);

        swal({
            title: "Encryption Failed!",
            text: error.message || "Unable to encrypt request.",
            type: "error",
            confirmButtonColor: "#DD6B55"
        });

        return false;
    }
 
    MailRevertConfirmation(encrypted);

});

function MailRevertConfirmation(encrypted) {
    swal({
        title: "Do you want to delete!",
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
             
                $.ajax({
                    url: '/Sent/AddSentBox',
                    type: 'POST',
                    data: {
                        Request: encrypted,
                    },
                    success: function (response) {
                        if (response.success) {
                            swal(response.Msg, "", "success");
                            window.location.href =
                                '/Sent/AddSentBox';
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

            }
            else {
                swal("Not Deleted!", "", "info");
            }
        }
    );
}


function SubmitGEBLetterFor(obj, ActionName, titleMessage, Submit) {
    //debugger;
    let parameters = [];
    let encrypted, submit, CommandId;
    let isOk = false;
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
                      
                        let IsNotification = true;
                        let value = $(this).find("#chkcheckbox").is(":checked");
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
    let cid = $(obj).attr('data-cid');
    let gid = $(obj).attr('data-gid');
    $.ajax({
        url: "/GEB/Childs",
        type: 'GET',
        data: { "ComdId": cid, "Gid": gid },
        //async: false,
        success: function (result) {
            if (result) {
                $.each(result, function (i, r) {
                    
                    let html = '<tr>' +                       
                        '<td  style="font-weight:bold;width: 10%;">' + '<label>' + r.UserName + ' </label>'  + '</td>' +                        
                        '<td  style="font-weight:bold;width: 10%;">' + '<input id="chkcheckbox"  type="checkbox" />' + '</td>' +
                        '<td style="display:none">' + '<input id="ChildId"  value=' + r.ChildId + ' type="hidden" />' + '</td>' +
                        '<td style="display:none">' + '<input id="RoleName"  value=' + r.RoleName + ' type="hidden" />' + '</td>' +
                        '<td style="display:none">' + '<input id="UserName"  value=' + r.UserName + ' type="hidden" />' + '</td>' +
                        '<td style="display:none">' + '<input id="TypeOfUnit"  value=' + r.TypeOfUnit + ' type="TypeOfUnit" />' + '</td>' +                        
                        '</tr>'
                    
                    $(html).appendTo($("#childTable"));
                   
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
    let cid = $(obj).attr('data-cid');
    let gid = $(obj).attr('data-gid');
    $.ajax({
        url: "/GEB/GetChildsList",
        type: 'GET',
        data: { "ComdId": cid, "Gid": gid },
        //async: false,
        success: function (result) {
            if (result) {
                $.each(result, function (i, r) {

                    let html = '<tr>' +
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
                let encrypted = $(obj).attr('data-id');
                location.href = ActionName + '?fid=' + encrypted + '&submit=' + Submit;
            } else {
                swal("Not Submitted!", "", "info");
            }
        }
    );
}