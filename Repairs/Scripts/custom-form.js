$(document).ready(function () {
    //$("form").submit(function (event) {
    //    highlightSelectedFormElement(event.target.id);
    //}); 
});

$('input[type=radio]').on('change', function () {
    $(this).closest("form").submit();
});

function highlightSelectedFormElement(formId, data) {
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
}

//var onAjaxRequestSuccess = function (result) {
//    if (result.EnableError) {
//        // Setting.  
//        alert(result.ErrorMsg);
//    } else if (result.EnableSuccess) {
//        this.highlightSelectedFormElement(result.FormId, result.SelectedName, result.SelectedValue);



//        //this.loadStep(result.NextStepName, result.SelectedValue);



//        // Resetting form.
//        $('#' + result.FormId).get(0).reset();
//    }
//};



//function highlightSelectedFormElement(formId, name, selectedValue) {
//    $("#" + formId + " :input").each(function () {
//        var input = $(this);

//        if (input[0].name === name) {
//            if (input.val() === selectedValue) {
//                input.prev().addClass("is-black");
//            } else {
//                input.closest('div').fadeOut();
//            }
//        }
//    });
//}

//function loadStep(action, selectedValue) {
//    //$("#" + action).load("/Diagnose/" + action, { selectedValue: selectedValue });

//    var url = "/Diagnose/" + action;
//    $.get(url, { selectedValue: selectedValue }, function (data, status) {
//        $("#" + action).html(data);
//    });
//}