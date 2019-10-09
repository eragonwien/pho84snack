// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(document).ready(function () {
   $('.chosen-select').chosen();
});

// General
$('.modal-trigger').click(function (e) {
   e.preventDefault();
   var target = $(this).data('target');
   $('#' + target).addClass('is-active');
});

// close modal
$(document).on('click', '.modal-close, .cancel-button', function (e) {
   e.preventDefault();
   $(this).closest('.modal').removeClass('is-active');
});

// submits ajax form
$('.ajax-form').submit(function (e) {
   e.preventDefault();
   var form = $(this);
   $.ajax({
      type: 'POST',
      url: form.attr('action'),
      data: form.serialize(),
      success: handleSuccess,
      error: handleAjaxFormError
   });

   function handleSuccess() {
      location.reload();
   }

   function handleAjaxFormError(data) {
      console.log(data);
      return;
   }

   function closeModal() {
      form.closest('.modal').removeClass('is-active');
   }
});

// remove notification
$('.notification .delete').click(function (e) {
   e.preventDefault();
   var notification = $(this).closest('.notification');
   notification.remove();
});

// Submit form on enter press
$('.input').keyup(function (e) {
   if (e.keyCode === 13 && $(this).hasClass('is-static') === false) {
      $(this).closest('form').submit();
   }
});