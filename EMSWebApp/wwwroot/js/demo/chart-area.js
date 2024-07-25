
$(document).ready(function () {
    getEmployeeNumberWithDepartment();
})

function getEmployeeNumberWithDepartment() {
    $.ajax({
        type: "POST",
        url: "/Home/GetDepartmentData",
        data: "",
        // contentType: "application/json; charset=utf-8",
        // dataType: "json",
        success: function (result) {
            var label = [];
            var value = [];
            $.each(result, function (index, item) {
                label.push(item.departmentName);
                value.push(item.employeeCount);
            })



            //Chart 
            const ctx = document.getElementById('myAreaChart');

            new Chart(ctx, {
                type: 'bar',
                data: {
                    labels: label,
                    datasets: [{
                        label: 'Employees',
                        data: value,
                        backgroundColor: ['#4e73df', '#1cc88a', '#36b9cc', '#060c1e', '#46e046', '#de692f', '#d99a2e', '#38d6bc'],
                        hoverBackgroundColor: ['#2e59d9', '#17a673', '#2c9faf', '#09122d', '#1ac91a', '#ed5509', '#966003', '#287569'],
                        borderWidth: 1
                    }]
                },
                options: {
                    scales: {
                        y: {
                            min: result.minValue,
                            max: result.maxValue
                        }
                    }
                }
            });

        },
        error: function (err) {
            alert("errrrrrrror");
        }
    })
}

















