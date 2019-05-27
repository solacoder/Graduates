var setUpNameId = $('#load-form').attr('data-setupname-id');
var parentId = $('#load-form').attr('data-parent-id');

var appObject = null;

function setUpValue(setUpValue) {
    this.id = setUpValue !== null ? setUpValue.id : 0;
    this.name = setUpValue !== null ? setUpValue.name : '';
    this.parentId = setUpValue !== null ? setUpValue.parentId : 0;
    this.setUpNameId = setUpValue !== null ? setUpValue.setUpNameId : '';
}

var infoViewModel = {
    setUpValue: new setUpValue(
        {
            'id': 0,
            'name': null,
            'parentId': 0,
            'setUpNameId': 0
        }),
    setUpNames: [],
    parents: [],
    toggleParentValidation: false
};

var gridListVM = {
    dt: null,
    init: function () {
        dt = $('#data-table').DataTable({
            "serverSide": true,
            "processing": true,
            "ajax": {
                "url": "/setupvalue/data/",
                "data": function (data) {
                    data.setUpNameId = setUpNameId;
                }
            },
            "columns": [
                { "title": "Id", "data": "id", "visible": false },
                { "title:": "Setup Name", "data": "setUpName", "searchable": true },
                { "title": "Name", "data": "name", "searchable": true },
                { "title": "Parent", "data": "parent", "searchable": true },
                {
                    render: function (o, type, data) {
                        var links = "<a class='md-btn' id='edit' href='#' data-url-form='/SetUpValue/Create' data-url-data='/SetUpValue/Edit?Id=" + data.id;
                        links += "' data-id='" + data.id + "' data-parent-id='" + data.parentSetUpNameId + "' onClick='return editRow(this)'><span class='fa fa-edit'></span></a>" + "&nbsp;";
                        links += "<a href='javascript:;' data-delete-url='/SetUpValue/Delete?id=" + data.id + "' onclick=' return deleteItem(this)' onmouseover='deleteItem(this)'><span class='fa fa-trash'></span></a>";
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
            valid: 'glyphicon glyphicon-ok',
            invalid: 'glyphicon glyphicon-remove',
            validating: 'glyphicon glyphicon-refresh'
        },
        fields: {
            setUpNameId: {
                validators: {
                    notEmpty: {
                        message: '<strong>Setup Name</strong> Required'
                    }
                }
            },
            name: {
                validators: {
                    notEmpty: {
                        message: '<strong>Name</strong> Required'
                    }
                }
            },
        },
        onSuccess: function (e) {
            e.preventDefault();
            formHelper.saveData({
                data: appObject.vm.setUpValue,
                url: $('#form').attr('action'),
                token: $("input[name = '__RequestVerificationToken']").val()
            });

            $(document).off('graduate.submitEvent').on('graduate.submitEvent', function () {
                if (appObject.vm.setUpValue.id === 0) {
                    appObject.vm.setUpValue.name = '';
                    $('#form').bootstrapValidator('resetForm', true);
                    $('#data-table').DataTable().ajax.reload();
                }
            });
        },
        onError: function (e) {
            e.preventDefault();
        }
    });
}

//Hooking the back btn on the panel to work 
$('#back-btn').on('click', function (e) {
    e.preventDefault();
    var url = $(this).attr('href');
    var ajaxCall = new MyAjaxCall();
    ajaxCall.load(url, 'main', null, true);
});

//for loading modal for new data capture
$("#load-form").on('click', function (event) {
    event.preventDefault();
    event.stopPropagation();

    var url = $(this).attr('href');
    var setUpNameId = $(this).attr('data-setupname-id');
    var parentId = $(this).attr('data-parent-id');
    
    showModal(0, '/setupvalue/create', null, { parentId: parentId, setUpNameId: setUpNameId });
    appObject.vm.setUpValue.name = '';
});

function editRow(sender) {
    $('[data-toggle="confirmation"]').confirmation();

    var formURL = $(sender).attr('data-url-form');
    var id = $(sender).attr('data-id');
    var dataURL = $(sender).attr('data-url-data');
    var parentId = $(sender).attr('data-parent-id');
    showModal(id, formURL, dataURL, { parentId: parentId, setUpNameId: setUpNameId });

    return false;
}

function showModal(id, formURL, dataURL, data) {

    $('.modal-body').load(formURL, function (result) {
        
        infoViewModel.setUpNames = new GetDataFromServer().loadData('SetUpValue/Get', null, 'setUpNames', false);
                
        infoViewModel.parents = new GetDataFromServer().loadData('SetUpValue/GetParentSetUpNameId', { 'parentSetUpNameId': data.parentId }, 'parentData', false);

        //for updating existing record
        if (id > 0) {
            infoViewModel.setUpValue = new GetDataFromServer().loadData(dataURL, null, 'setUpValue', false);
            
        }

        //for creating new record
        else {
            infoViewModel.setUpValue = new setUpValue({
                'id': 0,
                'name': null,
                'parentId': 0,
                'setUpNameId': data.setUpNameId
            });
        }

        //vue js root object
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

//sets up the contact person table display for delete operations using javascript's hooks
function deleteItem(obj) {
    var id = $(obj).attr('data-delete-url');
    $(obj).confirmation({
        onConfirm: function () {
            var id = $(obj).attr('data-delete-url');
            alert(id);
        }
    });
}


function formSubmit() {
    $('#sub-btn').submit();
}

