$(function () {
    $('#datetimepicker1').datetimepicker({ locale: 'pl', format: 'L' });
});

function Upload(event) {
    var formData = new FormData();
    formData.append('file', event.target.files[0]);
    jQuery.ajax({
        type: "Post",
        url: "../UploadImage",
        data: formData,
        async: true,
        contentType: false,
        mimeTypes: "multipart/form-data",
        processData: false,
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


                toastr.success("Przesłano", "Zdjęcie");
                var count = $('#photo-gallery-count').attr("data-counts");
                var int = parseInt(count) + 1;


                //Liczba/nazwa zdjęcia
                var trigger = document.getElementById('uploadGallery');
                if (5 > int && int > 1) {
                    trigger.value = "Wybrano " + int + " zdjęcia";
                } else if ((int) > 4) {
                    trigger.value = "Wybrano " + int + " zdjęć"
                } else {
                    trigger.value = event.target.files[0].name;
                }


                var galleryprewiew = document.getElementById("gallery");
                var newdiv = document.createElement("div");
                newdiv.setAttribute("class", "article-gallery-preview col-12 col-md-3 col-lg-4");
                var newimg = document.createElement("img");
                newdiv.appendChild(newimg);
                $('#stringofphoto').val($('#stringofphoto').val() + data + "|");
                newimg.src = '/Images/Article/Gallery/' + data;
                newimg.setAttribute("class", "img-fluid");
                newimg.style.borderRadius = "15px";
                newimg.style.border = "9px solid white";
                newdiv.setAttribute("id", "article-gallery-preview-" + data.replace(".", ""));
                newdiv.setAttribute("onclick", "Removes('" + data + "')");
                galleryprewiew.appendChild(newdiv);
                $('#photo-gallery-count').attr('data-counts', int)
                $('upImage').val();
            } else {
                toastr.error("", "Błąd");
            }
        }
    });
};
function Removes(name) {
    jQuery.ajax({
        type: "Post",
        url: "../RemoveImage",
        data: {
            'name': name,
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
                $("#article-gallery-preview-" + name.replace(".", "")).remove();
                var count = $('#photo-gallery-count').attr("data-counts");
                var int = parseInt(count) - 1;
                $('#photo-gallery-count').attr('data-counts', int)

                var trigger = document.getElementById('uploadGallery');
                if (5 > int && int > 1) {
                    trigger.value = "Wybrano " + int + " zdjęcia";
                } else if ((int) > 4) {
                    trigger.value = "Wybrano " + int + " zdjęć"
                } else {
                    trigger.value = "Dodaj zdjęcie";
                }
                $('#stringofphoto').val($('#stringofphoto').val().replace(name + "|", ""));
                toastr.success("Usunięto", "Zdjęcie");

            } else {
                toastr.error("", "Błąd");
            }
        }
    });
};
function removeUP(id, ph) {
    jQuery.ajax({
        type: "POST",
        url: "../DeletePhoto",
        data: {
            'id': id,
            'ph': ph,
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
            if (data == "PDeleted") {
                $('#article-gallery-previewU-' + ph).remove();
                toastr.success("Zdęcie", "Usunięto");
            }
            if (data == "Error") {
                toastr.error("", "Błąd");
            }
        }
    });
}
var loadFile = function (event) {
    var output = document.getElementById('thumbs-preview');
    output.src = URL.createObjectURL(event.target.files[0]);
    var trigger = document.getElementById('uploadFile');
    trigger.value = event.target.files[0].name;
};
