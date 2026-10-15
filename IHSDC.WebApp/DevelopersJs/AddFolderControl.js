let messaget = '@ViewBag.ButtonName';



$(".saveAddFolderControlConfirm").on("click", function (event) {

    event.preventDefault();

    // Remove any existing error labels
    $('.error-label').remove();

    // Check if all required form fields are filled in
    let emptyFields = [];
    $('form').find(':input[required]').each(function () {
        if ($.trim($(this).val()) === '') {
            emptyFields.push(this);
        }
    });

    // If there are empty fields, display an error message for the first empty field
    if (emptyFields.length > 0) {
        let firstEmptyField = emptyFields[0];
        $(firstEmptyField).after('<label class="error-label">This field is required</label>');
        return;
    }

    // Get selected users from multiselect
    let selectedUsers = $("#ddlUsers").val();

    // If your model expects comma separated user IDs
    let users = selectedUsers ? selectedUsers.join(",") : "";

    const payload = {
        Users: users,
        PolicyId: $("#PolicyId").val(),
        FolderControlId: $("#FolderControlId").val() || ""
    };

    let jsonData = JSON.stringify(payload);

    let encrypted = encryptPayloadData(jsonData);

    let id = $("#id").val();

    // Get Add / Update directly from clicked button
    let buttonName = $(this).val();

    console.log("Payload:", payload);
    console.log("JSON:", jsonData);
    console.log("Encrypted:", encrypted);
    console.log("ID:", id);
    console.log("Button:", buttonName);

    swal({
        title: "Are you sure?",
        text: "",
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: "#DD6B55",
        confirmButtonText: buttonName === "Add" ? "Yes, Add" : "Yes, Update",
        cancelButtonText: "Cancel",
        closeOnConfirm: false,
        closeOnCancel: true
    },
        function (isConfirm) {

            if (isConfirm) {

              
                $.ajax({
                    url: '/Master/AddFolderControl',
                    type: 'POST',

                    data: {
                        Request: encrypted,
                       // id: id,
                        btnval: buttonName,

                        // Required because controller has
                        // [ValidateAntiForgeryToken]
                        __RequestVerificationToken:
                            $('input[name="__RequestVerificationToken"]').val()
                    },

                    success: function (response) {

                        console.log(response);

                        swal({
                            title: "",
                            text: buttonName === "Add" ? "Add successfully" : "Update successfully",                      
                            type: "success",
                            confirmButtonColor: "#DD6B55",
                            confirmButtonText: "OK",
                            closeOnConfirm: true
                        },
                            function () {

                                // Reload page after success
                                window.location.href = '/Master/AddFolderControl';

                            });
                    },

                    error: function (xhr) {

                        console.log("Status:", xhr.status);
                        console.log("Response:", xhr.responseText);

                        swal({
                            title: "Update Failed!",
                            text: xhr.responseText || "",
                            type: "error",
                            confirmButtonColor: "#DD6B55"
                        });
                    }
                });
            }
        });
});

