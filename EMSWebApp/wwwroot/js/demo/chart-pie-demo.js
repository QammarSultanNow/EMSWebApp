
    $(document).ready(function () {
        GetDepartmentsData();
    })

    function GetDepartmentsData(){
        $.ajax({
            type: "POST",
            url: "/Home/GetDepartmentData",
            data: "",
            // contentType: "application/json; charset=utf-8",
            // dataType: "json",
            success: function (result) {

                var labels = [];
                var values = [];
                $.each(result, function (index, item) {
                    //console.log(item.departmentName);
                    labels.push(item.departmentName);
                    values.push(item.employeeCount);
                })


                // Create the chart
                var ctx = document.getElementById('myPieChart');
                var myPieChart = new Chart(ctx, {
                    type: 'doughnut',
                    data: {
                        labels: labels,
                        datasets: [{
                            data: values,
                            backgroundColor: ['#4e73df', '#1cc88a', '#36b9cc', '#060c1e', '#46e046', '#de692f', '#d99a2e', '#25e8ae'],
                            hoverBackgroundColor: ['#2e59d9', '#17a673', '#2c9faf', '#09122d', '#1ac91a', '#ed5509', '#966003','#145441'],

                        }],
                    },
                    options: {
                        maintainAspectRatio: false,
                        tooltips: {
                            backgroundColor: "rgb(255,255,255)",
                            bodyFontColor: "#858796",
                            borderColor: '#dddfeb',
                            borderWidth: 1,
                            xPadding: 15,
                            yPadding: 15,
                            displayColors: false,
                            caretPadding: 10,
                        },
                        legend: {
                            display: false
                        },
                        cutoutPercentage: 80,
                    }
                });
            },
            error: function (err) {
                alert("error");
            }
        });
    }

