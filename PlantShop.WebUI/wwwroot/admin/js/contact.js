$(document).ready(function () {
    $('#message-datatable').DataTable({
        destroy: true,
        ajax: {
            url: '/AdminPanel/Contact/ListGet',
            dataSrc: 'data'
        },
        columns: [
            { data: 'name', },
            { data: 'email' },
            { data: 'subject' },
            {
                data: 'id', render: function (data, type, row, meta) {
                    return `<div class="btn-list">
                           <button id="${JSON.stringify(data)}" type="button" class="btn remove btn-sm btn-danger"><span class="fa fa-delete"><i class="si si-trash"></i></span></button>
                           <button id="${JSON.stringify(data)}" type="button" class="btn detail btn-sm btn-info"><span class="fa fa-detail"><i class="fa fa-info-circle"></i></span></button>
                            </div>
                        `;
                }
            },
         ]
    });
    $('#message-datatable').on('click', '.remove', function () {
        var table = $('#message-datatable').DataTable();
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
                    url: `/AdminPanel/Contact/Delete?messageId=${id}`,
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

    });
    $('#message-datatable').on('click', '.detail', function () {
        var id = $(this).attr("id");
        location.href = `/AdminPanel/Contact/Detail?messageId=${id}`

    });
}); 