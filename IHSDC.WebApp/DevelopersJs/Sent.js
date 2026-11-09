let watermarkText = $('#txtwaterMark').val();
    $('#changenodalPopup').on('shown.bs.modal', function (e) {

    });
    $('#changenodalPopup').on('hidden.bs.modal', function (e) {

        $('#ddlCivEdnId').multiselect('deselectAll', false);
    $('#ddlCivEdnId').multiselect('updateButtonText');
    $("#txtForward").val("");
    $('#NodalId').val("0");
    $('#NodalComment').val("");
    $('#FwdComment').val("");
        });
    $("#closefwd").click(function () {

        $('#ddlCivEdnId').multiselect('deselectAll', false);
    $('#ddlCivEdnId').multiselect('updateButtonText');
    $("#txtForward").val("");
    $('#NodalId').val("0");
    $('#NodalComment').val("");
    $('#FwdComment').val("");
        });

    $("#clearfwd").click(function () {

        $('#ddlCivEdnId').multiselect('deselectAll', false);
    $('#ddlCivEdnId').multiselect('updateButtonText');
    $("#txtForward").val("");
    $('#NodalId').val("0");
    $('#NodalComment').val("");
    $('#FwdComment').val("");
        });


    $('#changenodalPopup').on('click', function (e) {
            if (e.target === this) {

        $('#ddlCivEdnId').multiselect('deselectAll', false);
    $('#ddlCivEdnId').multiselect('updateButtonText');
    $("#txtForward").val("");
    $('#changenodalPopup').modal('hide');
    $('#NodalId').val("0");
    $('#NodalComment').val("");
    $('#FwdComment').val("");
            }
        });

    $("#clearOpenReason").click(function () {
        $('#OpenReason').val("");
        });
    $("#clearCloseReason").click(function () {
        $('#CloseReason').val("");
        });


    $(document).ready(function () {
        let table = $('#Inbox').DataTable({
        lengthChange: true,
    pageLength: 50,
    aLengthMenu: [[25, 50, 100, 150, 200, 250, -1], [25, 50, 100, 150, 200, 250, "All"]],
    /* dom: 'Bfrtip',*/
    dom: 'lBfrtip',
    colReorder: true,
    responsive: true,
    buttons: [
    'copy',
    'excel',
    'csv',
    /* 'print',*/
    {
        text: 'PDF',
    extend: 'pdfHtml5',
    action: function (e, dt, node, config) {
        PrintDiv();
                            }
                        },
    'colvis',
    ],
                });

    let buttonContainer = table.buttons().container().appendTo('#Inbox_wrapper .col-md-6:eq(0)');
    let searchBuilderContainer = table.searchBuilder.container();
    buttonContainer.insertBefore(table.table().container());
    searchBuilderContainer.insertBefore(table.table().container());

            });

    function PrintDiv() {
        let popupWin = window.open('', '_blank', 'top=100,width=900,height=500,location=no');
    popupWin.document.open();

    let tableStyles = `
    <style type="text/css">
        table {
            width: 100%;
        border-collapse: collapse;
        margin-bottom: 20px;
                                                                }
        .custom-watermark {
            transform: rotate(-45deg);
        z-index: 10000;
        opacity: 0.3;
        color: #8e9191;
        position: fixed;
        left: 6%;
        top: 39%;
        font-size: 80px;
        font-weight: 500;
        display: grid;
        justify-content: center;
        align-content: center;
}
        th, td {
            padding: 8px;
        border: 1px solid #ddd;
        text-align: center;
                                                                }

        th {
            background - color: #f2f2f2;
        color: black;
                                                                }
        @@media print {

            @@page {
            margin - top: 10px;
        margin-bottom: 10px;
                                                                    }

        body::after {
            content: none !important;
                                                                    }

        .card-page {
                                                                        break-after: page;
                                                                    }

        .card-page:last-of-type {
                                                                            break-after: auto;
                                                                        }

        footer {
            position: fixed;
        bottom: 0;
        left: 0;
        right: 0;
        height: 10px;
        background-color: gray;
        color: white;
        text-align: center;
        padding: 10px;
                                                                    }

        header {
            position: fixed;
        top: 0;
        left: 0;
        right: 0;
        height: 10px;
        background-color: gray;
        color: white;
        text-align: center;
        padding: 10px;
                                                                    }
                                                                }
    </style>
    `;

    let table = $('#Inbox').DataTable();
    let visibleColumns = table.columns(':visible').header().toArray();
    let filteredData = table.rows({search: 'applied' }).data().toArray();

    let tableHTML = '<table>';

        tableHTML += '<thead>';
            tableHTML += '<tr>';
                visibleColumns.forEach(function (header) {
                    tableHTML += '<th>' + header.innerHTML + '</th>';
                });
                tableHTML += '</tr>';
            tableHTML += '</thead>';

        tableHTML += '<tbody>';
            for (var i = 0; i < filteredData.length; i++) {
                tableHTML += '<tr>';
            for (var j = 0; j < filteredData[i].length; j++) {
                        if (!table.column(j).visible()) {
                            continue;
                        }
            tableHTML += '<td>' + filteredData[i][j] + '</td>';
                    }
            tableHTML += '</tr>';
                }
        tableHTML += '</tbody>';

    tableHTML += '</table>';
let pageHeading = '<h1 class="custom-sent-heading">Sent</h1>';
popupWin.document.write('<html><head>' + pageHeading + tableStyles + '</head><body onload="window.print()">' + tableHTML + '<div class="custom-watermark">' + watermarkText + '</div></body></html>');

popupWin.document.close();
            }

      


      
            $(document).ready(function () {
                $('.clickable-div').click(function () {
                    let href = $(this).data('href');
                    window.location.href = href;
                });

                $('.dropdownselect').select2({
                    dropdownParent: $('#changenodalPopup'),
                    closeOnSelect: false
                });
            });

            $(document).on("click", ".saveChangenodal", function (event) {
                event.preventDefault();
                debugger;        
                // Create payload
                const payload = {
                    ForwardID: $("#ForwardID").val() || "",
                    NodalId: $("#NodalId1").val() || "",              
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


                if (!encrypted) {

                    console.error("Encrypted request is empty.");

                    swal({
                        title: "Error",
                        text: "Unable to create encrypted request.",
                        type: "error",
                        confirmButtonColor: "#DD6B55"
                    });

                    return false;
                }


                swal({

                    title: "Are you sure?",

                    text: "",

                    type: "warning",

                    showCancelButton: true,
                    confirmButtonColor: "#DD6B55",
                    confirmButtonText:"Yes, Add!",                          
                    cancelButtonText: "Cancel",
                    closeOnConfirm: false,
                    closeOnCancel: true

                },
                    function (isConfirm) {

                        if (!isConfirm) {
                            return;
                        }


                        let token =
                            $('input[name="__RequestVerificationToken"]').val();


                        console.log("Request:", encrypted);
                       
                        console.log("Token:", token);


                        $.ajax({

                            url: '/Sent/ChangeNodal',

                            type: 'POST',

                            data: {
                                Request: encrypted,                            
                               __RequestVerificationToken: token
                            },


                            success: function (response) {

                                console.log("Server Response:", response);


                                // If controller returns success false
                                if (
                                    response &&
                                    response.success === false
                                ) {

                                    swal({

                                        title: "Failed!",

                                        text:
                                            response.message ||
                                            "Unable to save.",

                                        type: "error",

                                        confirmButtonColor: "#DD6B55"
                                    });

                                    return;
                                }


                                swal({

                                    title: "",

                                    text:"Nodal Changed Successfully",
                                           
                                    type: "success",

                                    confirmButtonColor: "#DD6B55",

                                    confirmButtonText: "OK",

                                    closeOnConfirm: true

                                },
                                    function () {

                                        window.location.href =
                                            '/Sent/AddSentBox';
                                    });
                            },


                            error: function (xhr) {

                                console.error(
                                    "Status:",
                                    xhr.status
                                );

                                console.error(
                                    "Response:",
                                    xhr.responseText
                                );


                                swal({

                                    title:                                    
                                             "Add Failed!",                                          
                                    text:
                                        xhr.responseText ||
                                        "Something went wrong.",

                                    type: "error",

                                    confirmButtonColor: "#DD6B55"

                                });

                            }

                        });

                    });


                return false;

            });
       
     
            $('.btnChangeNodal').click(function () {
                GetUserdropdown(12, $(this).closest("tr").find(".Forword_Id").html(), $(this).closest("tr").find(".Schedule_ID").html());
            });



            function GetUserdropdown(Procid, Forwordid, ScheduleID) {
                $('#ForwardID').val(Forwordid);
                $.ajax({
                    url: '/Master/GetUserList',
                    type: 'POST',
                    data: { "Procid": Procid, "ForwardID": Forwordid, "scheduleId": ScheduleID },
                    success: function (response) {
                        if (response && response.length > 0) {

                            var dropdown = $('#NodalId1');
                            dropdown.empty();
                            dropdown.append('<option value="">--Select Nodal--</option>');
                            for (var j = 0; j < response.length; j++) {
                                dropdown.append('<option value="' + response[j].SentId + '">' + response[j].SentName + '</option>');
                            }
                            dropdown.selectpicker('refresh');
                        } else {

                        }
                    }


                });
            }



            $('#NodalId1').change(function () {
                $("#NodalId").val($('#NodalId1').val());
            });


   


    

            let today = new Date();
            let message = '@message';
            let messageStatus = '@messageStatus';
            let messagemidStatus = '@messagemidStatus';
    if (message)
            sweetAlert(message, messagemidStatus, messageStatus);



