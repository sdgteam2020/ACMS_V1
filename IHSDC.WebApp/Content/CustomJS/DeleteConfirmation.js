function DeleteConfirmation(obj, ActionName) {
	swal({
		title: "Are you want to sure Deactive this Data?",
		text: "",
		type: "warning",
		showCancelButton: true,
		confirmButtonColor: "#DD6B55",
		confirmButtonText: "Yes, Deactive it!",
		cancelButtonText: "No, cancel ",
		closeOnConfirm: false,
		closeOnCancel: false
	},
		function (isConfirm) {
			if (isConfirm) {
				var encrypted = $(obj).attr('data-id');
				location.href = ActionName + '?id=' + encrypted;
			} else {
				swal("Data will be not Deactived because proccess is Cancelled!", "", "info");
			}
		}
	);
}

function DeleteRecordConfirmation(obj, ActionName) {
	swal({
		title: "Are you want to sure Delete this Data?",
		text: "",
		type: "warning",
		showCancelButton: true,
		confirmButtonColor: "#DD6B55",
		confirmButtonText: "Yes, Delete it!",
		cancelButtonText: "No, cancel ",
		closeOnConfirm: false,
		closeOnCancel: false
	},
		function (isConfirm) {
			if (isConfirm) {
				var encrypted = $(obj).attr('data-id');
				location.href = ActionName + '?id=' + encrypted;
			} else {
				swal("Data will be not Delete because proccess is Cancelled!", "", "info");
			}
		}
	);
}
