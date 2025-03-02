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
                             <a href="/Admin/Category/Delete/${data}" class="btn btn-danger" style="cursor:pointer;">Delete</a>
                            `;

                }
            },

        ]
    });
}