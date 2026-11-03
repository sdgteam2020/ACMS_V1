
    
     let message = '@message';
     let messageStatus = '@messageStatus';
     let messagemidStatus = '@messagemidStatus ';
    if (message)
            sweetAlert(message, messagemidStatus, messageStatus);

  

        $(document).ready(function () {
            $('#spnScheduleId').change(function () {
                $("#ScheduleId").val($('#spnScheduleId').val());

                let typeofConf = $("#TypeOfConfId").val();
                let scheduleid = $("#ScheduleId").val();
                $.ajax({
                    url: '/Master/GetConf',
                    type: 'POST',
                    data: { "Proc": "12", "TypeOfConf": typeofConf, "ScheduleId": scheduleid },
                    success: function (result) {
                        if (result.length > 0) {
                            $('#ConfRefNo').val(result[0].ScheduleRefNoDt);
                        }
                        else {
                            $('#ConfRefNo').val("");
                        }
                    }
                });
            });
        });

        $('#TypeOfConfId').change(function () {
            BindConf($("#TypeOfConfId").val());
        });




        function BindConf(TypeOfConfId) {
            let list = '';
            let list1 = '';
            let list2 = '';
            let list3 = '';
            $.ajax({
                url: '/Master/GetConf',
                type: 'POST',
                data: { "Proc": "12", "TypeOfConf": TypeOfConfId, "ScheduleId": "0" },
                success: function (result) {
                    if (result.length > 0) {
                        list = '';
                        list = '<option value="">Select Conf</option>';
                        for (var j = 0; j < result.length; j++) {
                            list += '<option value=' + result[j].ScheduleId + '>' + result[j].Subject + '</option>';
                        }
                        $('#spnScheduleId').html(list);
                    }
                    else {
                        list = '';
                        list = '<option value="">Select Conf</option>';
                        $('#spnScheduleId').html(list);
                    }

                    if ($("#ScheduleId").val() != 0) {
                        $('#spnScheduleId').val($("#ScheduleId").val());
                    }
                }
            });
        }

        $(document).on("click", ".generateReport", function (e) {

            e.preventDefault();

            debugger;

            const payload = {

                TypeOfConfId: $("#TypeOfConfId").val() || "",
                ScheduleId: $("#ScheduleId").val() || "",
                Type: $("#Type").val() || "",
                ConfRefNo: $("#ConfRefNo").val() || "",           
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

                $.ajax({
                    url: '/Report/Reports',
                    type: 'POST',
                    data: {
                        Request: encrypted,
                    },
                    success: function (response) {
                      
                        if (payload.Type == "1") {
                            window.location.href =
                                '/Report/AppxAReport';
                        }
                        else if (payload.Type == "2") {
                            window.location.href =
                                '/Report/AppxBReport';
                        }
                        else {

                        }
                    },
                    error: function () {

                    }
                });          

        });
 