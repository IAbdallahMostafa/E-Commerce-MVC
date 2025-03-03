var datatable;
$(document).ready(function () {
    loadData();
});
function loadData() {
    datatable = $('#categoriesTable').DataTable({
        "ajax": {
            "url": "/admin/category/GetAll"
        },
        "columns": [
            { "data": "name" },
            { "data": "description" },
            {
                "data": "createAt",
                "render": function (data) {
                    if (!data) return ""; 
                    return new Date(data).toLocaleString('en-GB', {
                        day: '2-digit', month: '2-digit', year: 'numeric',
                        hour: '2-digit', minute: '2-digit', second: '2-digit',
                        hour12: true
                    }).replace(',', ' |');
                }
            },
            {
                "data": "id",
                "render": function (data) {
                    return ` 
                             <a href="/Admin/Category/Edit/${data}" class="btn btn-success" style="cursor:pointer;">Edit</a> 
                             <a onClick=DeleteItem('/Admin/Category/Delete/${data}') class="btn btn-danger" style="cursor:pointer;">Delete</a>
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