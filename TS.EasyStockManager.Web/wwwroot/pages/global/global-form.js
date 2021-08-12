$(document).ready(function () {
    $('#frmCreate').ajaxForm({
        beforeSubmit: beforeForm,
        success: successForm,
        dataType: 'json'
    });
});

function beforeForm(formData, jqForm, options) {
    $('#btnSave').attr("disabled", true);
    $('#btnSave').html('<span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span> Loading...');
}

function successForm(result, statusText) {
    if (result.IsRedirect == true) {
        window.location = result.RedirectUrl;
    }
    else {
        $('#btnSave').html('Save');
        $('#btnSave').attr("disabled", false);
        if (result.IsSucceeded == true) {
            $('#frmCreate').clearForm();
            if ($('.table-form-crate').length > 0) {
                $('.table-form-crate tbody tr:not(:first)').remove();
                $('.prd-first-select2').val('').trigger('change');
            }

            toastr.success(result.UserMessage);
        }
        else {
            toastr.error(result.UserMessage);
        }
    }
}
