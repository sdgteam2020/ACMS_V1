
    window.onscroll = function () {myFunction()};

    let header = document.getElementById("myHeader");
    let sticky = header.offsetTop;

    function myFunction() {
            if (window.pageYOffset > sticky) {
        header.classList.add("sticky");
            } else {
        header.classList.remove("sticky");
            }
        }


    const currentPage = window.location.pathname;

    const links = document.querySelectorAll(".sidebar a");
    for (let i = 0; i < links.length; i++) {
            if (links[i].href === currentPage) {
        links[i].classList.add("active");
            }
        }



    let date = new Date();
    let year = date.getFullYear();
    let month = date.getMonth();
    $('.datepicker').datepicker({

        minViewMode: 1,
    weekStart: 1,
    daysOfWeekHighlighted: "6,0",
    autoclose: true,
    todayHighlight: true,
    startDate: new Date(year - 15, month, '01'),
    endDate: new Date(year, month, '31')
        });
    $('.datepicker3').datepicker({
        format: "dd/M/yyyy",
    autoclose: true,
        });
$('.datepicker4').datepicker({
    format: "dd/M/yyyy",
    startDate: new Date(),
    autoclose: true,
    todayHighlight: true,
    orientation: "bottom auto"
});



    $(document).ready(function () {

        $('.sidebar a').click(function () {

            $('.sidebar a').removeClass('active');

            $(this).addClass('active');
        });
        });


function ValInData(input) {
    let regex = /[^a-zA-Z0-9 . / ( ) _ -]/g;
    input.value = input.value.replace(regex, "");
}
        $(document).ready(function () {
            $.ajax({
                url: '/Master/GetCounter',
                type: 'POST',
                data: { "Proc": "16" },
                success: function (result) {
                    if (result.length > 0) {
                        $('#spnComdName').html(result[0].ComdName);
                        $('#spnTodayCnt').html(result[0].Today);
                        $('#spnMonthlyCnt').html(result[0].CurrentMonth);
                        $('#spnTotalCnt').html(result[0].Total);
                    }
                    else {
                        $('#spnComdName').html(result[0].ComdName);
                        $('#spnTodayCnt').html("0");
                        $('#spnMonthlyCnt').html("0");
                        $('#spnTotalCnt').html("0");
                    }
                }
            });
        });
    