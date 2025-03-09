var dataTable;
$(document).ready(function ()
{
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#usersTable').DataTable({
        "ajax": {
            "url": "/admin/users/GetAll"
        },
        "columns": [
            { "data": "name" },
            { "data": "email" },
            { "data": "phoneNumber" },
            { "data": "address" },
            { "data": "age" },
            {
                "data": "id",
                render: function (data) {
                    return `
                             <a href="/Admin/Product/LockUnlock/${data}" class="btn btn-success" style="cursor:pointer;"><i class="fas fa-lock-open"></i></a> 
                             <a onClick=DeleteItem('/Admin/Product/Delete/${data}') class="btn btn-danger" style="cursor:pointer;">Delete</a>
                            `;
                }
            }]
    })
};
