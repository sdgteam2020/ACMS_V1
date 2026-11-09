
        function checkPDFHeader(input) {
            const files = input.files;
            for (let i = 0; i < files.length; i++) {
                const file = files[i];
                const reader = new FileReader();
                reader.onloadend = function (e) {
                    if (e.target.readyState === FileReader.DONE) {
                        const arr = new Uint8Array(e.target.result).subarray(0, 4);
                        const header = Array.from(arr).map(byte => byte.toString(16)).join("").toUpperCase();

                        if (header === "25504446") {

                        } else {
                            sweetAlert("Please select a valid PDF file.");
                            input.value = "";
                        }
                    }
                };
                reader.readAsArrayBuffer(file.slice(0, 4));
            }
        }

        const ChkMergeCL = document.querySelectorAll('.ChkMergeClass');
        ChkMergeCL.forEach(ChkMergeCL => {
            ChkMergeCL.onclick = function () {
                const ChkHFPCL = this.parentNode.parentNode.querySelector('.ChkHFPClass');
                ChkHFPCL.checked = this.checked;
            };
        });
        $(document).ready(function () {
            $('.dropdownsearch').select2();
        });
    


$('#Schedule_ID').change(function () {
    let id = $('#Schedule_ID').val();
    let encrypt = window.btoa(id);
    let url = "@Url.Action("AgendaPointMerge", "AgendaPointMerge")" + "?Conf=" + encrypt;
    window.location.href = url;
});

  
      
    
        $(document).ready(function () {
            let table = $('#Analysis').DataTable({
                lengthChange: true,
                pageLength: 50,
                aLengthMenu: [[25, 50, 100, 150, 200, 250, -1], [25, 50, 100, 150, 200, 250, "All"]],
                dom: 'lBfrtip',
                buttons: [
                    'copy',
                    'excel',
                    'csv',
                    {
                        text: 'PDF',
                        extend: 'pdfHtml5',
                        action: function () {
                            PrintDiv();
                        }
                    },
                    'colvis',
                    {
                        text: 'Advanced Search',
                        className: "buttons-Search",
                        action: function () {
                            $(".dtsb-searchBuilder").removeClass("d-none");
                        }
                    },
                ],
                searchBuilder: {
                    conditions: {
                        num: {
                            'MultipleOf': {
                                conditionName: 'Multiple Of',
                                init: function (that, fn, preDefined = null) {
                                    let el = $('<input/>').on('input', function () { fn(that, this) });

                                    if (preDefined !== null) {
                                        $(el).val(preDefined[0]);
                                    }

                                    return el;
                                },
                                inputValue: function (el) {
                                    return $(el[0]).val();
                                },
                                isInputValid: function (el, that) {
                                    return $(el[0]).val().length !== 0;
                                },
                                search: function (value, comparison) {
                                    return value % comparison === 0;
                                }
                            }
                        }
                    }
                }
            });

            let dropdownContainer = $('<div class="dropdown ml-3"></div>').insertAfter($(".buttons-Search"));

            let select = $('<select class="form-control custom-dropdown" multiple="multiple"></select>').appendTo(dropdownContainer);

            table.columns(5).every(function () {
                let column = this;
                column.data().unique().sort().each(function (d, j) {
                    select.append('<option value="' + d + '">' + d + '</option>')
                });
            });

            select.prepend('<option value="__reset__">-- Reset --</option>');

            select.select2({
                placeholder: "-- Select Conf Type --",
                width: '100%',
            });

            select.on('change', function () {
                let selectedValues = $(this).val() || [];
                if (selectedValues.includes('__reset__')) {
                    selectedValues = select.find('option').not(':selected[value="__reset__"]').map(function () { return this.value; }).get();

                    let index = selectedValues.indexOf('__reset__');
                    if (index !== -1) {
                        selectedValues.splice(index, 1);
                    }
                    select.val(null).trigger('change');
                }
                table.column(5).search(selectedValues ? '^(' + selectedValues.join('|') + ')$' : '', true, false).draw();
            });

            let buttonContainer = table.buttons().container().appendTo('#Analysis_wrapper .col-md-6:eq(0)');
            let searchBuilderContainer = table.searchBuilder.container();
            buttonContainer.insertBefore(table.table().container());
            searchBuilderContainer.insertBefore(table.table().container());
        });



        function PrintDiv() {
            let popupWin = window.open('', '_blank', 'top=100,width=900,height=500,location=no');
            if (!popupWin) return;
            let table = $('#Analysis').DataTable();
            let visibleColumns = table.columns(':visible').header().toArray();
            let filteredData = table.rows({ search: 'applied' }).data().toArray();
            let tableHTML = '<table><thead><tr>';
            visibleColumns.forEach(function (header) {
                tableHTML += '<th>' + header.innerHTML + '</th>';
            });
            tableHTML += '</tr></thead><tbody>';
            for (var i = 0; i < filteredData.length; i++) {
                tableHTML += '<tr>';
                for (var j = 0; j < filteredData[i].length; j++) {
                    if (!table.column(j).visible()) continue;
                    tableHTML += '<td>' + filteredData[i][j] + '</td>';
                }
                tableHTML += '</tr>';
            }
            tableHTML += '</tbody></table>';
let pageStyles = document.getElementById('agendaPageStyles').textContent;
popupWin.addEventListener('load', function () {
    popupWin.focus();
    popupWin.print();
});
popupWin.document.open();
popupWin.document.write('<html><head><style>' + pageStyles + '</style></head>' +
    '<body class="print-page"><h1 class="print-heading">Agenda Point</h1>' +
    tableHTML + '<div class="print-watermark" id="printWatermark"></div></body></html>');
            let printWatermark = popupWin.document.getElementById('printWatermark');
if (printWatermark) printWatermark.textContent = watermarkText == null ? '' : String(watermarkText);
popupWin.document.close();
        }
  
        $(document).ready(function () {
            $('.clickable-div').click(function () {
                let href = $(this).data('href');
                window.location.href = href;
            });
        });
  
        $(document).ready(function () {

            let table = $('#Analysis').DataTable();
            $('#ChkAll').on('click', function () {
                let rows = table.rows({ 'search': 'applied' }).nodes();
                $('input[type="checkbox"]', rows).prop('checked', this.checked);
                if (!this.checked) {
                    $('#ChkAll').prop('checked', false);
                }
            });
            $('#Analysis tbody').on('change', 'input[type="checkbox"]', function () {
                if (!this.checked) {
                    let el = $('#ChkAll').get(0);
                    if (el && el.checked && ('indeterminate' in el)) {
                        el.indeterminate = true;
                    }
                }
            });
        });

let draggedRow = null;
        $(document).on('dragstart', '#Analysis tbody tr[draggable="true"]', function (e) {
            draggedRow = this;
        });
        $(document).on('dragover', '#Analysis tbody tr[draggable="true"]', function (e) {
            e.preventDefault();
            if (!draggedRow || draggedRow === this || draggedRow.parentNode !== this.parentNode) return;
            let children = Array.from(this.parentNode.children);
            if (children.indexOf(this) > children.indexOf(draggedRow)) this.after(draggedRow);
            else this.before(draggedRow);
        });
  

let today = new Date();
let message = @Html.Raw(Json.Encode(message));
    let messageStatus = @Html.Raw(Json.Encode(messageStatus));
    let messagemidStatus = @Html.Raw(Json.Encode(messagemidStatus));
    if (message)
            sweetAlert(message, messagemidStatus, messageStatus);
  
        $(document).on('click', 'a[data-navigation-url]', function () {
            let url = $(this).attr('data-navigation-url');
            if (url) window.location.href = url;
        });
        $(document).on('click', 'a[data-delete-url]', function () {
            DeleteRecordConfirmation(this, $(this).attr('data-delete-url'));
        });
        $(document).on('change', '#files', function () {
            checkPDFHeader(this);
        });
   