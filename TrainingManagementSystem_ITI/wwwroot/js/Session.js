// Make it global:
window.Delete = function (url) {
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
                url: url,
                type: 'DELETE',
                success: function (data) {
                    if (data && data.success) {
                        // If you use DataTables:
                        if (window.dataTable && dataTable.ajax) {
                            dataTable.ajax.reload();
                        } else {
                            // fallback: refresh page
                            location.reload();
                        }
                        toastr.success(data.message || "Deleted successfully");
                    } else {
                        toastr.error((data && data.message) || "Delete failed");
                    }
                },
                error: function () {
                    toastr.error("Delete failed");
                }
            });
        }
    });
};

