
$(document).ready(function () {

   // Instantiate
   const screenWidth = window.screen.availWidth;
   const list = new mdc.list.MDCList.attachTo(document.querySelector('.mdc-list'));
   const drawer = new mdc.drawer.MDCDrawer.attachTo(document.querySelector('.mdc-drawer'));
   drawer.open = screenWidth >= 768;
   const topAppBar = new mdc.topAppBar.MDCTopAppBar.attachTo(document.querySelector('.mdc-top-app-bar'));
   topAppBar.setScrollTarget(document.getElementById('main'));
   topAppBar.listen('MDCTopAppBar:nav', () => {
      drawer.open = !drawer.open;
   });
});

window.onresize = function (e) {
   const screenWidth = window.screen.availWidth;
   const drawer = new mdc.drawer.MDCDrawer.attachTo(document.querySelector('.mdc-drawer'));
   if (screenWidth >= 768) {
      drawer.open = true;
   }
   else {
      drawer.open = false;
   }
};

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

$('input[type=checkbox].checkbox-all').change(function (e) {
   var checked = $(this).is(':checked');
   console.log(checked);
   $(this).closest('label').find('input[type=checkbox]').not('.checkbox-all').prop('checked', checked);
});
