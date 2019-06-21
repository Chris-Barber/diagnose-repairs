$(document).ready(function () {
    $("#AjaxformId").submit(function (event) {
        var dataString;
        event.preventDefault();
        event.stopImmediatePropagation();
        var action = $("#AjaxformId").attr("action");
        // Setting.  
        dataString = new FormData($("#AjaxformId").get(0));
        contentType = false;
        processData = false;
        $.ajax({
            type: "POST",
            url: action,
            data: dataString,
            dataType: "json",
            contentType: contentType,
            processData: processData,
            success: function (result) {
                // Result.  
                onAjaxRequestSuccess(result);
            },
            error: function (jqXHR, textStatus, errorThrown) {
                //do your own thing  
                alert("fail");
            }
        });
    }); //end .submit()  
});

var onAjaxRequestSuccess = function (result) {
    if (result.EnableError) {
        // Setting.  
        alert(result.ErrorMsg);
    } else if (result.EnableSuccess) {
        var formId = 'AjaxformId';
        this.highlightSelectedFormElement(formId, result.SelectedName, result.SelectedValue);
        this.loadStep(result.NextStepName, result.SelectedValue);
        // Resetting form.  
        $('#' + formId).get(0).reset();
    }
};

$('input[type=radio]').on('change', function () {
    $(this).closest("form").submit();
});

function highlightSelectedFormElement(formId, name, selectedValue) {
    $("#" + formId + " :input").each(function () {
        var input = $(this);

        if (input[0].name === name) {
            if (input.val() === selectedValue) {
                input.next().addClass("is-black");
            } else {
                input.closest('div').fadeOut();
            }
        }
    });
}

function loadStep(action, selectedValue) {
    $("#" + action).load("/Diagnose/" + action, { selectedValue: selectedValue });
}