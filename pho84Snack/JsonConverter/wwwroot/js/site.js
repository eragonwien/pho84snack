// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your Javascript code.
$('#control-file').change(displaySelectedFile);

function displaySelectedFile() {
    var selected = $('#control-file').val();
    selected = selected.substring(selected.lastIndexOf('\\') + 1);
    $('#custom-file-label').text(selected);
}