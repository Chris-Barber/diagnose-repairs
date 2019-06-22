$(document).ready(function () {
    AddHandlers("CategoryStepForm");
});

function AddHandlers(formId) {
    $('form#' + formId + ' input[type=radio]').on('change', function () {
        $(this).closest("form").submit();
    });
}

function highlightSelectedFormElement(formId, data, nextForm) {
    $("#" + formId + " :input").each(function () {
        var input = $(this);

        if (input.is(':radio')) {
            if (input.is(':checked')) {
                input.prev().addClass("is-black");
            } else {
                input.closest('div').fadeOut();
            }
        }

    });

    AddHandlers(nextForm);
}
