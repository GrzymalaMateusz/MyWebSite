$(function () {
    $('#datetimepicker1').datetimepicker({ locale: 'pl', format: 'L' });
    $('#datetimepicker2').datetimepicker({ locale: 'pl', format: 'L' });
    $('#datetimepicker3').datetimepicker({ locale: 'pl', format: 'L' });
    $('#datetimepicker4').datetimepicker({ locale: 'pl', format: 'L' });
    $('#datetimepicker5').datetimepicker({ locale: 'pl', format: 'L' });
    $('#datetimepicker6').datetimepicker({ locale: 'pl', format: 'L' });

});
$(function () {
    $('#AddCvSchool').click(function () {
        // title is optional
        jQuery.ajax({
            type: "POST",
            url: "AddSchool", 
            data: {
                'Name': $('#modal-School-name').val(),
                'Profile': $('#modal-Profile-name').val(),
                'TitleOfThesis': $('#modal-TitleOfThesis').val(),
                'DateStart': $('#modal-School-DataStart').val(),
                'DateEnd': $('#modal-School-DataEnd').val()
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
                if (data.text == "Sended") {

                    toastr.success("Wysłano", "Wiadomość");

                    var school = document.getElementById("CV-Schools");
                    var d1 = document.createElement("div");
                    d1.setAttribute("id", "CV-School-single-" + data.id);
                    d1.setAttribute("class", "row CV-School-single")

                    var d2 = document.createElement("div");
                    d2.setAttribute("class", "col-12 col-md-3");
                    d2.innerHTML = "<p style='margin-bottom:0px'>" + $('#modal-School-DataStart').val().substring(3, 10) + " - " + $('#modal-School-DataEnd').val().substring(3, 10) + "</p>";

                    var d3 = document.createElement("div");
                    d3.setAttribute("class", "col-12 col-md-9");
                    d3.innerHTML = "<p style='margin-bottom:0px'>" + $('#modal-School-name').val() + "</p>";

                    var d4 = document.createElement("div");
                    d4.setAttribute("class", "col-12 col-md-9 offset-md-3 mb-25");
                    d4.innerHTML = "<p style='margin-bottom:0px'>Profil / Kierunek: " + $('#modal-Profile-name').val() + "</p>";

                    var p = document.createElement("p");
                    p.setAttribute("class", "float-left");
                    p.setAttribute("style", "margin-bottom:0px");
                    if ($('#modal-TitleOfThesis').val() != "") {
                        p.innerText = "Temat pracy dyplomowej: " + $('#modal-TitleOfThesis').val();
                    }

                    var tr = document.createElement("a");
                    tr.setAttribute("data-toggle", "modal");
                    tr.setAttribute("data-target", "#myModal1");
                    tr.setAttribute("class", "float-right ");
                    tr.setAttribute("onclick", "delete_school(" + data.id + ")");

                    var i = document.createElement("i");
                    i.setAttribute("class", "fa fa-trash list-delete-btn");
                    i.setAttribute("style", "margin:0px");



                    d1.appendChild(d2);
                    d1.appendChild(d3);
                    d4.appendChild(p);
                    d1.appendChild(d4);
                    tr.appendChild(i);
                    d4.appendChild(tr);
                    school.appendChild(d1);
                    $('#modal-School-name').val("");
                    $('#modal-Profile-name').val("");
                    $('#modal-TitleOfThesis').val("");
                    $('#modal-School-DataStart').val("");
                    $('#modal-School-DataEnd').val("");
                }
                if (data == "Error") {
                    $('#Newsletter-email').val("");
                    toastr.error("Adres email i treść wiadomości są wymagane", "Błąd");

                }
            }
        });
    });
    $(function () {
        $('#AddCvJob').click(function () {
            // title is optional
            jQuery.ajax({
                type: "POST",
                url: "AddJob",
                data: {
                    'CompanyName': $('#modal-Company-name').val(),
                    'Stand': $('#modal-Stand-name').val(),
                    'DataStart': $('#modal-Job-DataStart').val(),
                    'DateEnd': $('#modal-Job-DataEnd').val()
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
                    if (data.text == "Sended") {
                        var shool = document.getElementById("CV-Jobs");
                        var d1 = document.createElement("div");
                        d1.setAttribute("id", "CV-Job-single-" + data.id);
                        d1.setAttribute("class", "row CV-Job-single")

                        var d2 = document.createElement("div");
                        d2.setAttribute("class", "col-12 col-md-3");
                        d2.innerHTML = "<p style='margin-bottom:0px'>" + $('#modal-Job-DataStart').val().substring(3, 10) + " - " + $('#modal-Job-DataEnd').val().substring(3, 10) + "</p>";

                        var d3 = document.createElement("div");
                        d3.setAttribute("class", "col-12 col-md-9");
                        d3.innerHTML = "<p style='margin-bottom:0px'>" + $('#modal-Company-name').val() + "</p>";

                        var d4 = document.createElement("div");
                        d4.setAttribute("class", "col-12 col-md-9 offset-md-3 mb-25");
                        d4.innerHTML = "<p style='margin-bottom:0px'>Stanowisko: " + $('#modal-Stand-name').val() + "</p>";

                        var tr = document.createElement("a");
                        tr.setAttribute("data-toggle", "modal");
                        tr.setAttribute("data-target", "#myModal1");
                        tr.setAttribute("class", "float-right ");
                        tr.setAttribute("onclick", "delete_job(" + data.id + ")");

                        var i = document.createElement("i");
                        i.setAttribute("class", "fa fa-trash list-delete-btn");
                        i.setAttribute("style", "margin:0px");

                        d1.appendChild(d2);
                        d1.appendChild(d3);
                        d1.appendChild(d4);
                        tr.appendChild(i);
                        d4.appendChild(tr);
                        shool.appendChild(d1);

                        $('#modal-Company-name').val("");
                        $('#modal-Stand-name').val("");
                        $('#modal-Job-DataStart').val("");
                        $('#modal-Job-DataEnd').val("");
                        toastr.success("Wysłano", "Wiadomość");
                    }
                    if (data == "Error") {
                        $('#Newsletter-email').val("");
                        toastr.error("Adres email i treść wiadomości są wymagane", "Błąd");

                    }
                }
            });
        });
    });
});
$(function () {
    $('#AddCvSkill').click(function () {
        // title is optional
        jQuery.ajax({
            type: "POST",
            url: "AddSkill",
            data: {
                'Name': $('#modal-Skill-name').val(),

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
                if (data.text == "Sended") {


                    var skill = document.getElementById("CV-Skills");
                    var d1 = document.createElement("div");
                    d1.setAttribute("id", "CV-Skill-single-" + data.id);
                    d1.setAttribute("class", "row CV-Skill-single")

                    var d2 = document.createElement("div");
                    d2.setAttribute("class", "col-12 col-md-9 offset-md-3 mb-25");
                    d2.innerHTML = "<p class='float-left' style='margin-bottom:0px'>" + $('#modal-Skill-name').val() + "</p>";


                    var tr = document.createElement("a");
                    tr.setAttribute("data-toggle", "modal");
                    tr.setAttribute("data-target", "#myModal1");
                    tr.setAttribute("class", "float-right ");
                    tr.setAttribute("onclick", "delete_skill(" + data.id + ")");

                    var i = document.createElement("i");
                    i.setAttribute("class", "fa fa-trash list-delete-btn");
                    i.setAttribute("style", "margin:0px");


                    d1.appendChild(d2);
                    tr.appendChild(i);
                    d2.appendChild(tr);
                    skill.appendChild(d1);

                    $('#modal-Skill-name').val("");
                    toastr.success("Dodano", "Umiejętność");
                }
                if (data == "Error") {
                    $('#Newsletter-email').val("");
                    toastr.error("Błędne dane", "Spróbuj ponownie");

                }
            }
        });
    });
});
function delete_school(id) {
    $('#modal-CV-delete-confirmed').attr('onClick', 'delete_school_confirm(' + id + ')');
}
function delete_school_confirm(id) {
    jQuery.ajax({
        type: "POST",
        url: "DeleteSchool",
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
            if (data == "Removed") {
                $('#CV-School-single-' + id).remove();
                toastr.success("", "Usunięto");
            }
            if (data == "Error") {
                toastr.error("", "Błąd");
            }
        }
    });
    $('#modal-CV-delete-confirmed').attr('onClick', '');
}
function delete_job(id) {
    $('#modal-CV-delete-confirmed').attr('onClick', 'delete_job_confirm(' + id + ')');
}
function delete_job_confirm(id) {
    jQuery.ajax({
        type: "POST",
        url: "DeleteJob",
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
            if (data == "Removed") {
                $('#CV-Job-single-' + id).remove();
                toastr.success("", "Usunięto");
            }
            if (data == "Error") {
                toastr.error("", "Błąd");
            }
        }
    });
    $('#modal-CV-delete-confirmed').attr('onClick', '');
}
function delete_skill(id) {
    $('#modal-CV-delete-confirmed').attr('onClick', 'delete_skill_confirm(' + id + ')');
}
function delete_skill_confirm(id) {
    jQuery.ajax({
        type: "POST",
        url: "DeleteSkill",
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
            if (data == "Removed") {
                $('#CV-Skill-single-' + id).remove();
                toastr.success("", "Usunięto");
            }
            if (data == "Error") {
                toastr.error("", "Błąd");
            }
        }
    });
    $('#modal-CV-delete-confirmed').attr('onClick', '');
}