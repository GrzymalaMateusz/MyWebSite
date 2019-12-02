var loadFile = function (event) {
    var output = document.getElementById('thumbs-preview');
    output.src = URL.createObjectURL(event.target.files[0]);
    var trigger = document.getElementById('uploadFile');
    trigger.value = event.target.files[0].name;
};