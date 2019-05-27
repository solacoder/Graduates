function user(user) {
    this.id = user !== null ? user.id : 0;
    this.userName = user !== null ? user.userName : null;
    this.firstName = user !== null ? user.firstName : null;
    this.lastName = user !== null ? user.lastName : null;
    this.role = user !== null ? user.role : '';
    this.email = user !== null ? user.email : null;
    this.phoneNumber = user !== null ? user.phoneNumber : null;
}

var infoViewModel = {
    user: new user(null),
    roles: []
};

var appObject = null; 

var gridListVM, gridListUnapprovedVM;

gridListVM = {
    dt: null,
    init: function () {
        dt = $('#data-table').DataTable({
            "serverSide": true,
            "processing": true,
            "ajax": {
                "url": "/user/approvedusers/"
            },
            "columns": [
                { "title": "Id", "data": "id", "visible": false },
                { "title": "UserName", "data": "userName", "searchable": true },
                { "title": "First Name", "data": "firstName", "searchable": true },
                { "title": "Last Name", "data": "lastName", "searchable": true },
                { "title": "Email", "data": "email", "searchable": true },
                { "title": "Phone", "data": "phoneNumber", "searchable": true },
                {
                    render: function (o, type, data) {
                        var links = "<a class='md-btn' id='edit' href='#' data-url-form='institution/Create' data-url-data='institution/Edit/" + data.id;
                        links += "' data-id='" + data.id + "' onClick='return editRow(this)'><span class='fa fa-edit'></span></a>" + "&nbsp;";
                        links += "<a href='institution/Delete/" + data.id + "'><span class='fa fa-trash'></span></a>";
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

//unapproved user grid
gridListUnApprovedVM = {
    dt: null,
    init: function () {
        dt = $('#data-table-unapproved').DataTable({
            "serverSide": true,
            "processing": true,
            "ajax": {
                "url": "/user/unapprovedusers/"
            },
            "columns": [
                { "title": "Id", "data": "id", "visible": false },
                { "title": "UserName", "data": "userName", "searchable": true },
                { "title": "First Name", "data": "firstName", "searchable": true },
                { "title": "Last Name", "data": "lastName", "searchable": true },
                { "title": "Email", "data": "email", "searchable": true },
                { "title": "Phone", "data": "phoneNumber", "searchable": true },
                {
                    render: function (o, type, data) {
                        var links = "<a class='md-btn' id='edit' href='#' data-url-form='institution/Create' data-url-data='institution/Edit/" + data.id;
                        links += "' data-id='" + data.id + "' onClick='return editRow(this)'><span class='fa fa-edit'></span></a>" + "&nbsp;";
                        links += "<a href='institution/Delete/" + data.id + "'><span class='fa fa-trash'></span></a>";
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
gridListUnApprovedVM.init();


function validate() {
    $('#form').bootstrapValidator({
        container: '#messages',
        feedbackIcons: {
            valid: 'glyphicon glyphicon-ok',
            invalid: 'glyphicon glyphicon-remove',
            validating: 'glyphicon glyphicon-refresh'
        },
        fields: {
            name: {
                validators: {
                    notEmpty: {
                        message: '<strong>Name</strong> Required'
                    }
                }
            },
            email: {
                validators: {
                    notEmpty: {
                        message: '<strong>Email</strong> Required'
                    },
                    emailAddress: {
                        message: '<strong>Email Invalid</strong> Required'
                    }
                }
            },
            phone: {
                validators: {
                    notEmpty: {
                        message: '<strong>Phone</strong> Required'
                    }
                }
            }
        },
        onSuccess: function (e) {
            formHelper.saveData({
                data: appObject.vm.user,
                url: $('#form').attr('action'),
                token: $("input[name = '__RequestVerificationToken']").val()
            });
            $(document).off('graduate.submitEvent').on('graduate.submitEvent', function () {
                if (appObject.vm.user.id === 0) {
                    appObject.vm.user = new user(null);
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
    showModal(0, '/user/create', null);
    appObject.vm.user = new user(null);
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

        infoViewModel.roles = new GetDataFromServer().loadData('role/index', null, 'roles', false);
        console.log(JSON.stringify(infoViewModel.roles));
        
        if (id > 0) {
            
            infoViewModel.user = new GetDataFromServer().loadData(dataURL, null, 'user', false);
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

function formSubmit() {
    $('#sub-btn').submit();
}