

    let watermarkText = Html.Raw(Json.Encode(watermarkText));

    let message = Html.Raw(Json.Encode(message));
    let messageStatus = Html.Raw(Json.Encode(messageStatus));
    let messagemidStatus = Html.Raw(Json.Encode(messagemidStatus));

    $(document).ready(function () {

            let table = $('#Analysis').DataTable({
        lengthChange: true,
    pageLength: 50,
    aLengthMenu: [
    [25, 50, 100, 150, 200, 250, -1],
    [25, 50, 100, 150, 200, 250, "All"]
    ],
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
                    }
    ],

    searchBuilder: {
        conditions: {
        num: {
        'MultipleOf': {
        conditionName: 'Multiple Of',

    init: function (that, fn, preDefined = null) {
                                    let el = $('<input />').on('input', function () {
        fn(that, this);
                                    });

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

    let dropdownContainer = $('<div class="dropdown ml-3"></div>')
    .insertAfter($(".buttons-Search"));

    let select = $('<select class="form-control custom-dropdown" multiple="multiple"></select>')
    .appendTo(dropdownContainer);

    table.columns(5).every(function () {
                let column = this;

    column.data().unique().sort().each(function (d, j) {
        select.append(
            '<option value="' + d + '">' + d + '</option>'
        );
                });
            });

    select.prepend(
    '<option value="__reset__">-- Reset --</option>'
    );

    select.select2({
        placeholder: "-- Select Conf Type --",
    width: '100%'
            });

    select.on('change', function () {

                let selectedValues = $(this).val() || [];

    if (selectedValues.includes('__reset__')) {

        selectedValues = select.find('option')
            .not(':selected[value="__reset__"]')
            .map(function () {
                return this.value;
            })
            .get();

    let index = selectedValues.indexOf('__reset__');

    if (index !== -1) {
        selectedValues.splice(index, 1);
                    }

    select.val(null).trigger('change');
                }

    table.column(5).search(
    selectedValues.length
    ? '^(' + selectedValues.join('|') + ')$'
    : '',
    true,
    false
    ).draw();
            });

    let buttonContainer = table.buttons().container()
    .appendTo('#Analysis_wrapper .col-md-6:eq(0)');

    let searchBuilderContainer = table.searchBuilder.container();

    buttonContainer.insertBefore(table.table().container());

    searchBuilderContainer.insertBefore(table.table().container());

    $('.dropdownsearch').select2();

    $('.clickable-div').on('click', function () {
                let href = $(this).data('href');
    window.location.href = href;
            });

    $('#Schedule_ID').on('change', function () {
                let id = $('#Schedule_ID').val();
    let encrypt = window.btoa(id);

    let url = @Html.Raw(Json.Encode(Url.Action("SearchInboxNoting", "AgendaPointMerge")))
    + "?Conf=" + encrypt;

    window.location.href = url;
            });

    $('#ChkAll').on('click', function () {

                let rows = table.rows({
        'search': 'applied'
                }).nodes();

    $('input[type="checkbox"]', rows)
    .prop('checked', this.checked);

    if (!this.checked) {
        $('#ChkAll').prop('checked', false);
                }
            });

    $('#Search tbody').on(
    'change',
    'input[type="checkbox"]',
    function () {

                    if (!this.checked) {
                        let el = $('#ChkAll').get(0);

    if (el && el.checked && ('indeterminate' in el)) {
        el.indeterminate = true;
                        }
                    }
                }
    );

    $('#Analysis tbody').on(
    'change',
    '.ChkMergeClass',
    function () {

                    const ChkHFPCL = this.parentNode.parentNode
    .querySelector('.ChkHFPClass');

    if (ChkHFPCL) {
        ChkHFPCL.checked = this.checked;
                    }
                }
    );

    $(document).on(
    'click',
    'a[data-navigation-url]',
    function () {

                    let navigationUrl = $(this).attr('data-navigation-url');

    if (navigationUrl) {
        window.location.href = navigationUrl;
                    }
                }
    );

    if (message) {
        sweetAlert(message, messagemidStatus, messageStatus);
            }

        });

    function PrintDiv() {

            let popupWin = window.open(
    '',
    '_blank',
    'top=100,width=900,height=500,location=no'
    );

    if (!popupWin) {
                return;
            }

    let table = $('#Analysis').DataTable();

    let visibleColumns = table.columns(':visible')
    .header()
    .toArray();

    let filteredData = table.rows({
        search: 'applied'
            }).data().toArray();

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

let pageStyles = document.getElementById('pageStyles').textContent;

popupWin.addEventListener('load', function () {
    popupWin.focus();
    popupWin.print();
});

popupWin.document.open();

popupWin.document.write(
    '<html>' +
    '<head>' +
    '<style>' + pageStyles + '</style>' +
    '</head>' +
    '<body class="print-page">' +
    '<h1 class="print-heading">Search</h1>' +
    tableHTML +
    '<div id="printWatermark" class="print-watermark"></div>' +
    '</body>' +
    '</html>'
);

        let printWatermark = popupWin.document
    .getElementById('printWatermark');

if (printWatermark) {
    printWatermark.textContent = watermarkText == null
        ? ''
        : String(watermarkText);
}

popupWin.document.close();
        }

let row = null;

$(document).on(
    'dragstart',
    '#Analysis tbody tr[draggable="true"]',
    function (e) {

        row = this;
    }
);

$(document).on(
    'dragover',
    '#Analysis tbody tr[draggable="true"]',
    function (e) {

        e.preventDefault();

        if (!row || row === this) {
            return;
        }

        if (this.parentNode !== row.parentNode) {
            return;
        }

        let children = Array.from(this.parentNode.children);

        if (children.indexOf(this) > children.indexOf(row)) {
            this.after(row);
        } else {
            this.before(row);
        }
    }
);

   