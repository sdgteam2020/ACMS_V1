
   
    let messaget = '@ViewBag.ButtonName';


    $(".OrderbyId").off("click").click(function () {
        let orderby = $(this).closest("td").find(".spnOrderbyId").html();
    updateorder(orderby);
       // CorpsOrderby
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

$(document).ready(function () {
    $("#corpsForm").on("submit", async function (e) {
    e.preventDefault();
        let $form = $("#corpsForm");
        if (!$form.valid()) {

            return false;
        }
    let id = $("#id").val() || "";
  
   
        let buttonName = $(".saveAddCorpsConfirm").val() || "";
  
    const payload = {
        
        CorpsName: $("#CorpsName").val() || "",

        ComdId: $("#ComdId").val() || "",
        CorpsId: $("#Corpsd").val() || ""
      
    };


    console.log("Payload:", payload);
    console.log("Encrypted ID:", id);
    console.log("Button:", buttonName);


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

        confirmButtonText:
            buttonName === "Add"
                ? "Yes, Add"
                : "Yes, Update",

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
            console.log("ID:", id);
            console.log("btnval:", buttonName);
            console.log("Token:", token);


            $.ajax({

                url: '/Master/AddCorps',

                type: 'POST',

                data: {

                    Request: encrypted,

                

                    btnval: buttonName,

                    __RequestVerificationToken: token
                },


                success: function (response) {

                    console.log(response);

                    // SERVER-SIDE VALIDATION ERROR
                    if (response && response.success === false) {

                        let firstError = Object.values(response.errors || {})
                            .flat()[0] || response.message || "";

                        $("#CorpsNameValidation")
                            .text(firstError)
                            .removeClass("d-none");

                        swal({
                            title: "Failed!",
                            text: response.message || "Validation failed.",
                            type: "error",
                            confirmButtonColor: "#DD6B55"
                        });

                        return;
                    }

               
                    swal({

                        title: "",

                        text:
                            buttonName === "Add"
                                ? "Added successfully!"
                                : "Updated successfully!",

                        type: "success",

                        confirmButtonColor: "#DD6B55",

                        confirmButtonText: "OK",

                        closeOnConfirm: true

                    },
                        function () {

                            window.location.href =
                                '/Master/AddCorps';

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
                            buttonName === "Add"
                                ? "Add Failed!"
                                : "Update Failed!",

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
});

$(document).ready(function () {
    $('.dropdownsearch').select2();
});
