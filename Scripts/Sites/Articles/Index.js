function show_input_search() {
    if ($('#list-input-search').css("width") == "35px") {
        $('#list-input-search').animate({ width: "175px" }, {
            queue: false,
            duration: 3000
        });
    } else {
        $('#list-search').submit();
    }
}
function delete_article(id) {
    $('#modal-pagecontact-delete-confirmed').attr('onClick', 'delete_article_confirm(' + id + ')');
}
function delete_article_confirm(id) {
    jQuery.ajax({
        type: "POST",
        url: "Articles/Delete",
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
                $('#article-' + id).remove();
                toastr.success("", "Usunięto");
            }
            if (data == "Error") {
                toastr.error("", "Błąd");
            }
        }
    });
    $('#modal-pagecontact-delete-confirmed').attr('onClick', '');
}