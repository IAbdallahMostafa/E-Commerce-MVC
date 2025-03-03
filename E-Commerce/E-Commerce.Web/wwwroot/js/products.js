var datatable;
$(document).ready(function () {
    loadData();
});
function loadData() {
    datatable = $('#productsTable').DataTable({
        "ajax": {
            "url": "/admin/product/GetAll"
        },
        "columns": [
            { "data": "name" },
            { "data": "description" },
            { "data": "price" },
            { "data": "category.name" },
            {
                "data": "image",
                "render": function (data) {
                    return `<img src="/Images/Product/${data}" alt="Product Image" width="50" height="50"/>`;
                }
            },
            {
                "data": "id",
                "render": function (data) {
                    return ` 
                            <a href="/Admin/Product/Edit/${data}" class="btn btn-success" style="cursor:pointer;">Edit</a> 
                             <a onClick=DeleteItem('/Admin/Product/Delete/${data}') class="btn btn-danger" style="cursor:pointer;">Delete</a>
                            `;

                }
            }, 
            
        ]
    });
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