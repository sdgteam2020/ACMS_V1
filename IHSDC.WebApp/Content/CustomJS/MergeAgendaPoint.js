$("#sortButton").click(function () {
    var selectedPDFs = [];

    var table = document.getElementById("Analysis");
    var rows = table.getElementsByTagName("tr");
    var chkWithoutWaterMark = document.getElementById("ChkWithoutWatermark");
    var withoutWatermark = chkWithoutWaterMark.checked;

    for (var i = 1; i < rows.length; i++) {
        var row = rows[i];
        var checkbox = row.querySelector("#ChkMerge");
        var checkboxHFP = row.querySelector("#ChkHFP");

        var checkvalue = '';
        if (checkbox && checkbox.checked) {
            var pdfInput = row.querySelector("#ItemUpload");


            if (checkboxHFP.checked) {
                checkvalue = true;
            }
            else {
                checkvalue = false;
            }
            var pdfPath = pdfInput.value + ',' + checkvalue;
            selectedPDFs.push(pdfPath);
        }
    }
    if (selectedPDFs.length > 0) {
        var requestData = {
            sortedModelList: selectedPDFs,
            withoutWatermark: withoutWatermark
        };

        $.ajax({
            type: "POST",
            url: "/AgendaPointMerge/AgendaPointMerge",
            data: requestData,
            dataType: "json",
            timeout: 60000,
            success: function (response) {
                if (response.success) {
                    if (response.DownloadPath) {
                        var a = document.createElement('a');
                        a.href = response.DownloadPath;
                        a.download = response.FileName;
                        a.style.display = 'none';
                        document.body.appendChild(a);
                        a.click();
                        document.body.removeChild(a);
                        sweetAlert("Agenda Point pdf Merged Successfully !");
                        location.href = '/AgendaPointMerge/AgendaPointMerge'
                    } else {
                        sweetAlert("An error occurred while generating the PDF.");
                    }
                } else {
                    sweetAlert("An error occurred while generating the PDF.");
                }
            },
            error: function (error) {

                sweetAlert("Some Error Occured while merging PDF !");
            }
        });



    } else {
        sweetAlert("No PDFs selected for Merge.");
    }
});