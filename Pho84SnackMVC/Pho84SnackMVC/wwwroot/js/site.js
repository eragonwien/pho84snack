// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// General
$('.modal-trigger').click(function (e) {
   e.preventDefault();
   var target = $(this).data('target');
   $('#' + target).addClass('is-active');
   var firstInput = $('#' + target).find("input[type='text'], textarea").first();
   firstInput.focus();
   firstInput.select();
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
      if (!data || !data.responseJSON) {
         return;
      }
      Object.keys(data.responseJSON).forEach(function (key) {
         if (key && key !== '') {
            var input = $('#' + key);
            input.closest('.field').find('p.help').text(data.responseJSON[key]);
            input.focus();
            input.select();
         }
      });
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
