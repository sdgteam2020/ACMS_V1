
    $(document).ready(function () {
        $('.dropdownsearch').select2();
    });

    let messaget = '@ViewBag.ButtonName';


    $(".OrderbyId").off("click").click(function () {
        let orderby = $(this).closest("td").find(".spnOrderbyId").html();
    updateorder(orderby);
      
    });
    function updateorder(orderby) {

        $.ajax({
            url: '/Master/CorpsOrderby',
            type: 'POST',
            data: { "OrderbyId": orderby }, //get the search string
            success: function (result) {


                if (result == 0) {

                    swal("Successfully!", "Update Order Successfully", "success");
                    location.reload();

                }

            }
        });



    }


    function AddConfirmationDgs(obj, ActionName, Submit) {
        event.preventDefault();


    $('.error-label').remove();


    let emptyFields = [];
    $('form').find(':input[required]').each(function () {
            if ($.trim($(this).val()) === '') {
        emptyFields.push(this);
            }
        });

    
        if (emptyFields.length > 0) {
            let firstEmptyField = emptyFields[0];
    $(firstEmptyField).after('<label class="error-label">This field is required</label>');
    return;
        }


    if (messaget == "Add") {
        swal({
            title: "Please Confirm",
            text: "",
            type: "warning",

            showCancelButton: true,
            cancelButtonText: "Cancel",
            cancelButtonColor: "#FF0000",
            confirmButtonColor: "#DD6B55",
            confirmButtonText: "Yes, Add",

            closeOnCancel: true,
            closeOnConfirm: false
        },
            function (isConfirm) {
                if (isConfirm) {
                    $('form').submit();
                }
            });
        } else {
        swal({
            title: "Please Confirm",
            text: "",
            type: "warning",
            showCancelButton: true,
            confirmButtonColor: "#DD6B55",
            confirmButtonText: "Yes, Update",
            cancelButtonText: "Cancel",

            closeOnConfirm: false,
            closeOnCancel: true
        },
            function (isConfirm) {
                if (isConfirm) {
                    $('form').submit();
                }
            });
        }
    }

    let message = '@message';
    let messageStatus = '@messageStatus';
    let messagemidStatus = '@messagemidStatus ';
    if (message)
    sweetAlert(message, messagemidStatus, messageStatus);
