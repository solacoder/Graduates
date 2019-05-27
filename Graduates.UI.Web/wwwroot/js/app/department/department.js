function department(department) {
    this.id = department !== null ? department.id : 0;
    this.name = department !== null ? department.name : null;
    this.email = department !== null ? department.email : null;
    this.phone = department !== null ? department.phone : null;
    this.institutionId = department !== null ? department.institutionId : 0;
    this.facultyId = department !== null ? department.facultyId : 0;
}

var infoViewModel = {
    department: new department(null),
    institutions: [],
    faculties: []
};

var appObject = null;

var gridListVM;

gridListVM = {
    dt: null,
    init: function () {
        dt = $('#data-table').DataTable({
            "serverSide": true,
            "processing": true,
            "ajax": {
                "url": "/department/data/"
            },
            "columns": [
                { "title": "Id", "data": "id", "visible": false },
                { "title": "Name", "data": "name", "searchable": true },
                { "title": "Email", "data": "email", "searchable": true },
                { "title": "Phone", "data": "phone", "searchable": true },
                { "title": "Institution", "data": "institution", "searchable": true },
                { "title": "Faculty", "data": "faculty", "searchable": true },
                {
                    render: function (o, type, data) {
                        var links = "<a class='md-btn' id='edit' href='#' data-url-form='department/create' data-url-data='department/Edit/" + data.id;
                        links += "' data-id='" + data.id + "' onClick='return editRow(this)'><span class='fa fa-edit'></span></a>" + "&nbsp;";
                        links += "<a href='department/Delete/" + data.id + "'><span class='fa fa-trash'></span></a>";
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
                        message: '<strong>Invalid Email</strong> Required'
                    }
                }
            },
            phone: {
                validators: {
                    notEmpty: {
                        message: '<strong>Phone</strong> Required'
                    }
                }
            },
            institutionId: {
                validators: {
                    notEmpty: {
                        message: '<strong>Institution</strong> Required'
                    }
                }
            }
        },
        onSuccess: function (e) {
            formHelper.saveData({
                data: appObject.vm.department,
                url: $('#form').attr('action'),
                token: $("input[name = '__RequestVerificationToken']").val()
            });
            $(document).off('graduate.submitEvent').on('graduate.submitEvent', function () {
                if (appObject.vm.department.id === 0) {
                    appObject.vm.department = new department(null);
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
    showModal(0, '/department/create', null);
    appObject.vm.department = new department(null);
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

        infoViewModel.institutions = new GetDataFromServer().loadData('/institution/getAll', null, 'institutions', false);

        if (id > 0) {
            infoViewModel.department = new GetDataFromServer().loadData(dataURL, null, 'department', false);
            infoViewModel.faculties = new GetDataFromServer().loadData('/faculty/GetFacultiesByInstitution', { 'institutionId': infoViewModel.department.institutionId }, 'faculties', false);
        }

        appObject = new Vue({
            el: "#modal-body",
            data: {
                vm: infoViewModel
            },
            methods: {
                loadFaculties: function () {
                    infoViewModel.faculties = new GetDataFromServer().loadData('/faculty/GetFacultiesByInstitutionId', { 'institutionId': appObject.vm.department.institutionId }, 'faculties', false);
                }
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