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
                             <a href="/Admin/Product/Delete/${data}" class="btn btn-danger" style="cursor:pointer;">Delete</a>
                            `;

                }
            }, 
            
        ]
    });
}