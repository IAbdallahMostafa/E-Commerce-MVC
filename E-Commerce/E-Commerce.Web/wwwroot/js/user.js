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
                "data": null,
                render: function (data) {

                    let currentDate = new Date();
                    let lockedOutEnd = data.lockoutEnd ? new Date(data.lockoutEnd) : null;
                    let isLocked = lockedOutEnd !== null && lockedOutEnd > currentDate; // المستخدم مقفل

                    let lockIcon = isLocked ? "fa-lock" : "fa-lock-open";
                    let btnClass = isLocked ? "btn-danger" : "btn-success";
                    let title = isLocked ? "Unlock User" : "Lock User";
                    return `
                             <a href="/Admin/Users/LockUnlock/${data.id}" class="btn ${btnClass}" title="${title}" style="width:50px">
                                 <i class="fas ${lockIcon}"></i>
                             </a>
                              <button class="btn btn-danger" onclick="DeleteItem('/Admin/Users/Delete/${data.id}')" title="Delete" style="width:50px">
                                    <i class="fas fa-trash"></i>
                               </button>
                            `;
                }
            }]
    })
};
