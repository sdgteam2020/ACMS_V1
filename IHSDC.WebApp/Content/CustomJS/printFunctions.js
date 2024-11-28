
function convertPDF(fileName) {
    var divToPrint = document.getElementById('printAppxE');

    var newWin = window.open('', 'Print-Window');

    newWin.document.open();

    newWin.document.write('<html><body onload="window.print()">' + divToPrint.innerHTML + '</body></html>');

    newWin.document.close();

    setTimeout(function () { newWin.close(); }, 10);
}

function convertIntoWord(fileName) {
    var htmltable = document.getElementById('dataTable');
    var html = htmltable.outerHTML;
    window.open('data:application/msword,' + '\uFEFF' + encodeURIComponent(html));
    
}
