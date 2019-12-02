function delete_portfolios(id) {
    $('#modal-portfolios-delete-confirmed').attr('onClick', 'delete_portfolios_confirm(' + id + ')');
}
function delete_portfolios_confirm(id) {
    jQuery.ajax({
        type: "POST",
        url: "Portfolios/Delete",
        data: {
            'id': id
        },
        async: true,
        dataType: "json",
        success: function (data) {
            toastr.options = {
                "closeButton": true,
                "debug": false,
                "newestOnTop": true,
                "positionClass": "toast-bottom-right",
                "preventDuplicates": true,
                "showDuration": "300",
                "hideDuration": "1000",
                "timeOut": "5000",
                "extendedTimeOut": "1000",
                "showEasing": "swing",
                "hideEasing": "linear",
                "showMethod": "fadeIn",
                "hideMethod": "fadeOut"
            }
            if (data == "Deleted") {
                $('#portfolios-' + id).remove();
                toastr.success("", "Usunięto");
            }
            if (data == "Error") {
                toastr.error("", "Błąd");
            }
        }
    });
    $('#modal-portfolios-delete-confirmed').attr('onClick', '');
}