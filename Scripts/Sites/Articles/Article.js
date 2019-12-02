$(window).on('load', function () {
    var h = $('#height-find').outerHeight() + 78;
    $('#article-gallery').height(h);
})
$(window).resize(function () {
    if ($('#article-gallery-show').is(":visible")) {
        var h = $('#height-find').outerHeight() + 78;
        $('#article-gallery').height(h);
    }

})
function report_comment(id) {
    $('#modal-comment-report-confirmed').attr('onClick', 'report_comment_confirm(' + id + ')');
}
function report_comment_confirm(id) {
    jQuery.ajax({
        type: "Post",
        url: "../ReportComment",
        data: {
            'id_c': id,
            'text_reason': document.getElementById("modal-report-comment-text").value
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
            if (data != "Error") {
                toastr.success("Zgłoszono", "Komentarz");
                $('#modal-comment-report-confirmed').attr('onClick', '');
            } else {
                toastr.error("", "Błąd");
                $('#modal-comment-report-confirmed').attr('onClick', '');
            }
        }
    });
};
$(document).ready(function () {
    $(".fancybox").fancybox();
});
$('#article-gallery-show').click(function () {
    document.getElementById("article-gallery").style.height = "auto";
    $('#article-gallery-show').hide();
});
$('#article-comments-comment').click(function () {
    jQuery.ajax({
        type: "POST",
        url: "../Comment",
        data: {
            'ida': $('#input_id').val(),
            'CommentName': $('#input_name').val(),
            'CommentText': $('#input_text').val(),
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
            if (data != "Error") {
                $('#input_name').val("");
                $('#input_text').val("");
                var ac = document.createElement("div");
                ac.className = "article-comment";

                var av = document.createElement("div");
                av.className = "article-comment-avatar";
                if (data.User != null && data.User.Photo != null) {
                    av.style.backgroundImage = "url('/Images/User/" + data.User.Photo + "')";
                } else {
                    av.style.backgroundImage = "url('/Images/Admin/User.jpg')";
                }
                var ah = document.createElement("p");

                var au = document.createElement("b");
                var Name = "";
                if (data.Name == null) {
                    Name = data.User.ForName + " " + data.User.SurName.substring(0, 1) + ".";
                } else {
                    Name = data.Name;
                }
                au.innerText = Name;
                ah.appendChild(au);

                var ad = document.createElement("span");
                var date = new Date(data.CreateDate.replace("/Date(", "").replace(")/", "") * 1);
                var mounth = '';
                var day = '';
                var hour = "";
                var min = "";
                if (date.getMonth() < 10) {
                    mounth = "0" + (1 + date.getMonth());
                } else {
                    mounth = date.getDate();
                }
                if (date.getDate() < 10) {
                    day = "0" + date.getDate();
                } else {
                    day = date.getDate();
                }
                if (date.getHours() < 10) {
                    hour = "0" + date.getHours();
                } else {
                    hour = date.getHours();
                }
                if (date.getMinutes() < 10) {
                    min = "0" + date.getMinutes();
                } else {
                    min = date.getMinutes();
                }
                ad.innerText = day + "." + mounth + "." + date.getFullYear() + " " + hour + ":" + min;
                ah.appendChild(ad);
                ac.appendChild(av);
                ac.appendChild(ah);

                var at = document.createElement("p");
                at.innerText = data.Text;
                ac.appendChild(at);

                $('#article-comments').append(ac);
                toastr.success("Dodano", "Komentarz");
            }
            else {
                toastr.error("", "Błąd");

            }
        }
    });
});