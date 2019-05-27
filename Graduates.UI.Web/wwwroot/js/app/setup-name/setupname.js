function setupName(setupName) {
    this.id = setupName !== null ? setupName.id : 0;
    this.name = setupName !== null ? setupName.name : null;
    this.parentId = setupName !== null ? setupName.parentId : 0;
}

var infoViewModel = {
    setupName: new setupName(null),
    setupNames: []
};

var appObject = null; 

var gridListVM = {
    dt: null,
    init: function () {
        dt = $('#data-table').DataTable({
            "serverSide": true,
            "processing": true,
            "ajax": {
                "url": "/setupname/data/"
            },
            "columns": [
                { "title": "Id", "data": "id", "visible": false },
                { "title": "Name", "data": "name", "searchable": true },
                { "title": "Parent", "data": "parent", "searchable": true },
                {
                    render: function (o, type, data) {
                        var links = "<a id='details' href='SetUpValue/Index' data-setupname-id='" + data.id + "' data-parent-id='" + data.parentId + "' onClick='return loadSetUpValue(this)'><span class='fa fa-plus'></span></a>" + "&nbsp;";
                        links += "<a class='md-btn' id='edit' href='#' data-url-form='SetUpName/create' data-url-data='SetUpName/Edit?id=" + data.id;
                        links += "' data-id='" + data.id + "' onClick='return editRow(this)'><span class='fa fa-edit'></span></a>" + "&nbsp;";
                        links += "<a href='SetUpName/Delete/" + data.id + "'><span class='fa fa-trash'></span></a>";
                        return links;
                    }
                }
            ],
            "lengthMenu": [[10, 25, 50, 100], [10, 25, 50, 100]],
            "columnDefs": [
                { "width": "8%", "targets": -1, "sortable": false }
            ]
        });
    }
};
// initialize the datatables
gridListVM.init();


function validate() {
    $('#form').bootstrapValidator({
        container: '#messages',
        feedbackIcons: {
            valid: 'fa fa-ok',
            invalid: 'fa fa-remove',
            validating: 'fa fa-refresh'
        },
        fields: {
            name: {
                validators: {
                    notEmpty: {
                        message: '<strong>Name</strong> Required'
                    }
                }
            }
        },
        onSuccess: function (e) {
            formHelper.saveData({
                data: appObject.vm.setupName,
                url: $('#form').attr('action'),
                token: $("input[name = '__RequestVerificationToken']").val()
            });
            $(document).off('graduate.submitEvent').on('graduate.submitEvent', function () {
                if (appObject.vm.setupName.id === 0) {
                    appObject.vm.setupName = new setupName(null);
                    $('#form').bootstrapValidator('resetForm', true);
                }
                $('#data-table').DataTable().ajax.reload();
            });
        },
        onError: function (e) {
            e.preventDefault();
            formHelper.errorAlert;
        }
    });
}

//for loading modal for new data capture
$("#load-form").on('click', function (event) {
    event.preventDefault();
    event.stopPropagation();
    showModal(0, '/setupname/create', null);
    appObject.vm.setupName = new setupName(null);
});

function editRow(sender) {
    var formURL = $(sender).attr('data-url-form');
    var id = $(sender).attr('data-id');
    var dataURL = $(sender).attr('data-url-data');
    showModal(id, formURL, dataURL);
    return false;
}

function showModal(id, formURL, dataURL) {

    $('.modal-body').load(formURL, function (result) {

        infoViewModel.setupNames = new GetDataFromServer().loadData('SetUpName/GetAll', null, 'setupNames', false);

        if (id > 0) {
            infoViewModel.setupName = new GetDataFromServer().loadData(dataURL, null, 'setupName', false);

            console.log(JSON.stringify(infoViewModel.setupName));
        }

        appObject = new Vue({
            el: "#modal-body",
            data: {
                vm: infoViewModel
            }
        });

        $('#modal-dialog').modal({
            show: true
        });

        validate();
    });
}

function loadSetUpValue(sender) {
    
    var url = $(sender).attr('href');
    var setUpNameId = $(sender).attr('data-setupname-id');
    var parentId = $(sender).attr('data-parent-id');

    var data = {
        parentId: parseInt(parentId),
        setUpNameId: parseInt(setUpNameId)
    };

    var call = new MyAjaxCall();
    call.load(url, 'main', data, true);

    return false;
}

function formSubmit() {
    $('#sub-btn').submit();
}

