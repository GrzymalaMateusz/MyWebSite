$(document).ready(function () {
    $(".fancybox").fancybox();
});
function show(i) {
    jQuery.ajax({
        type: "POST",
        url: "../ShowComment",
        data: {
            'id': i,
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
            if (data == "Showed") {
                toastr.success("Komentarz", "Pokazano");
                $('#article-comment-' + i).removeClass("article-comment-reported-removed");
                $('.article-comment-show-' + i).val('Ukryj');
                $('.article-comment-show-' + i).addClass('article-comment-hide-' + i);
                $('.article-comment-hide-' + i).removeClass('.article-comment-show-' + i);
                $('.article-comment-hide-' + i).removeAttr('onclick');
                $('.article-comment-hide-' + i).attr('onclick', 'hide(' + i + ')');
            }
            if (data == "Error") {
                toastr.error("", "Błąd");
            }
        }
    });
}
function hide(i) {

    jQuery.ajax({
        type: "POST",
        url: "../HideComment",
        data: {
            'id': i,
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
            if (data == "Hided") {
                toastr.success("Komentarz", "Ukryto");
                if ($('#article-comment-' + i).hasClass("article-comment-reported")) {
                    $('#article-comment-' + i).removeClass("article-comment-reported");
                }
                $('#article-comment-' + i).addClass("article-comment-reported-removed");
                $('.article-comment-hide-' + i).val('Pokaż');
                $('.article-comment-hide-' + i).addClass('article-comment-show-' + i);
                $('.article-comment-show-' + i).removeClass('.article-comment-hide-' + i);
                $('.article-comment-show-' + i).removeAttr('onclick');
                $('.article-comment-show-' + i).attr('onclick', 'show(' + i + ')');
            }
            if (data == "Error") {
                toastr.error("", "Błąd");
            }
        }
    });
}
function unreport(i) {
    jQuery.ajax({
        type: "POST",
        url: "../UnreportComment",
        data: {
            'id': i,
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
            if (data == "Unreport") {
                toastr.success("Zgłoszenie", "Odrzucono");
                $('#article-comment-' + i).removeClass("article-comment-reported");
                $('article-comment-unreport-' + i).remove();
            }
            if (data == "Error") {
                toastr.error("", "Błąd");
            }
        }
    });
}