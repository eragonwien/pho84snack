
document.addEventListener('DOMContentLoaded', () => {
   const navbarBurger = document.querySelector('.navbar-burger');
   navbarBurger.addEventListener('click', function () {
      navbarBurger.classList.toggle('is-active');
      document.querySelector('.navbar-menu').classList.toggle('is-active');
   });
});

const inputAutocomplete = document.querySelector('.input-autocomplete');
if (inputAutocomplete) {
   document.querySelector('.input-autocomplete').addEventListener('keyup', function (e) {
      const inputText = this.value;
      const outputs = document.querySelectorAll('.output-autocomplete');
      forEach(outputs, function (index, value) {
         if (outputs[index].text.toLowerCase().includes(inputText)) {
            outputs[index].classList.remove('is-hidden');
         }
         else {
            outputs[index].classList.add('is-hidden');
         }
      });
   });
}

const filterSelect = document.querySelector('.filter-select');
if (filterSelect) {
   filterSelect.addEventListener('change', function () {
      let selectedValue = this.value.toLowerCase();
      const outputs = document.querySelectorAll('.output-autocomplete');
      forEach(outputs, function (index, value) {
         let activated = outputs[index].dataset.activated.toLowerCase() === 'true';
         if (selectedValue === 'all' || (selectedValue === 'activated' && activated) || (selectedValue === 'not activated' && !activated)) {
            outputs[index].classList.remove('is-hidden-2');
         }
         else {
            outputs[index].classList.add('is-hidden-2');
         }
      });
   });
}


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
   var checkboxLabel = $(this).closest('label');
   var field = $(this).closest('.field');
   checkboxLabel.find('input[type=checkbox]').not('.checkbox-all').prop('checked', checked);
   checkboxLabel.toggleClass('is-success', checked);
   checkboxLabel.toggleClass('is-danger', !checked);
   field.find('.price-input').prop('disabled', !checked);
});

function enableEdit(button) {
   button.classList.add('is-hidden');
   button.previousElementSibling.classList.remove('is-hidden');
   const form = button.closest('form');
   form.classList.remove('is-static');
   let inputs = form.querySelectorAll('.input, .textarea, .checkbox, .checkbox-label');
   forEach(inputs, function (index, value) {
      inputs[index].removeAttribute('readonly');
      inputs[index].removeAttribute('disabled');
      if (inputs[index].hasAttribute('rows')) {
         inputs[index].setAttribute('rows', 5);
      }
   });
   inputs[0].focus();
   inputs[0].select();
}

var forEach = function (array, callback, scope) {
   for (var i = 0; i < array.length; i++) {
      callback.call(scope, i, array[i]); // passes back stuff we need
   }
};

function onCheckboxChanged(checkbox) {
   checkbox.parentElement.classList.toggle('checked', checkbox.checked);
}
