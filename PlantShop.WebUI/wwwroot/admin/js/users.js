$(document).ready(function () {
    $('#user-datatable').DataTable({
        ajax: {
            url: '/AdminPanel/User/UserListGet',
            dataSrc: 'data'
        },
        columns: [
            { data: 'id', },
           
            { data: 'userName', },
            { data: 'name', },
            { data: 'id', render: function (data, type, row, meta) {
                    return 
                    `<div class="btn-list">
                        <button onclick=Edit(${JSON.stringify(data)}) type="button" class="btn btn-sm btn-primary">
                            <i class="fe fe-eye"></i>
                        </button>
                    </div>`;
                }
            },
        ]
    });
   
    $('#portfolio-datatable').on('click', '.remove', function () {
        var table = $('#portfolio-datatable').DataTable();
        var id = $(this).attr("id");
        var thh = $(this);
        debugger;
        Swal.fire({
            title: 'Are you sure?',
            text: "You won't be able to revert this!",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Yes, delete it!'
        }).then((result) => {
            if (result.isConfirmed) {
                $.ajax({
                    type: "PUT",
                    url: `/AdminPanel/Portfolio/Delete?id=${id}`,
                    success: function (result) {
                        if (result.success == true) {
                            Swal.fire(
                                'Deleted!',
                                'Your file has been deleted.',
                                'success'
                            ).then((result) => {
                                if (result.isConfirmed) {
                                    table
                                        .row($(thh).parents('tr'))
                                        .remove()
                                        .draw(false);
                                    debugger;
                                }
                            });

                        }
                        else {
                            alert(result.message)
                        }
                    }
                });

            }
        })

        debugger;
    });

});
function Edit(id) {
    location.href = `/AdminPanel/User/Update?Id=${id}`;
}