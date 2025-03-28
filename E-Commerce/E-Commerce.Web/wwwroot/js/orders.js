var datatable;
$(document).ready(function () {
    loadData();
});

function loadData() {
    datatable = $('#ordersTable').DataTable({
        "ajax": {
            "url": "/admin/order/GetAll"
        },
        "columns": [
            { "data": "id", "className": "text-center" },
            { "data": "name", "className": "text-center" },
            { "data": "phoneNumber", "className": "text-center" },
            {
                "data": "orderDate",
                "className": "text-center",
                "render": function (data) {
                    return formatDate(data);
                }
            },
            { "data": "orderStatus", "className": "text-center" },
            { "data": "totalPrice", "className": "text-center" },
            {
                "data": "id",
                "className": "text-center",
                "render": function (data) {
                    return ` 
                            <a href="/Admin/Order/Details?orderid=${data}" class="btn btn-success" style="cursor:pointer;">Details</a> 
                            `;
                }
            }
        ]
    });
}

function formatDate(dateString) {
    var options = { 
        day: '2-digit', 
        month: '2-digit', 
        year: 'numeric', 
        hour: '2-digit', 
        minute: '2-digit', 
        second: '2-digit', 
        hour12: true 
    };
    var date = new Date(dateString);
    return date.toLocaleString('en-GB', options).replace(',', ' |');
}

function DeleteItem(url) {
    Swal.fire({
        title: "Are you sure?",
        text: "You won't be able to revert this!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Yes, delete it!"
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                type: "DELETE",
                url: url,
                success: function (data) {
                    if (data.success) {
                        datatable.ajax.reload();
                        toastr.success(data.message);
                    } else {
                        toastr.error(data.message);
                    }
                }
            });
        }
    });
}