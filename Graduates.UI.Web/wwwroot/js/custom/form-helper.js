var formHelper = (function() {
    function successAlert() {
        $.gritter.add({
            title: "<i class='fa fa-thumbs-up'></i>",
            text: 'Success',
            sticky: false,
            time: '3000',
            class_name: "gritter-success"
        });
        return false;
    }

    function errorAlert(msg) {
        $.gritter.add({
            title: "<i class='fa fa-thumbs-down'></i>",
            text: 'Failed - ' + msg,
            sticky: false,
            time: '3000',
            class_name: "gritter-failed"
        });
        return false;
    }

    function clearFields(formId) {
        $form = $("#" + formId);   
    }

    function saveData(param) {
        console.log(JSON.stringify(param.data));

        $.ajax({
            url: param.url,
            async: true,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            headers: { "RequestVerificationToken": param.token },
            data: JSON.stringify(Object.assign({}, param.data)),
            type: "POST",
            beforeSend: function (xhr) {
                $('#btnSubmit').addClass('disabled');
                $('#btnSubmit').html('Processing...');
            },
            success: function (data) {
                if (data.success) {
                    successAlert();
                    $(document).trigger('graduate.submitEvent');
                }
                else if (!data.success) {
                    $('#btnSubmit').prop('disabled', false);
                    errorAlert("Error", data.reason);   
                }
            },
            complete: function () {
                $('#btnSubmit').removeClass('disabled');
                $('#btnSubmit').html('Submit');
                $('#form').find('button.btn.btn-success').removeAttr('disabled');
            },
            error: function (jqXHR, textStatus, errorThrown) {
                errorAlert("Error", "jqXHR: " + jqXHR + "textStatus :" + textStatus + "errorThrown: " + errorThrown);
            }
        });
    }

    function callModal(modalName) {
        $('#' + modalName).modal()
    }

    function beforeSend() {
        $('div#create-form').empty();
        $('div#create-form').html('<img id="loader-img" alt="" src="images/spinner.gif" width="30" height="30" style="position: absolute; right: 46%; top: 0px;" />');
    }
    return {
        saveData: saveData,
        callModal: callModal,
        clearFields: clearFields,
        successAlert: successAlert,
        errorAlert: errorAlert,
        beforeSend: beforeSend
    }
})();