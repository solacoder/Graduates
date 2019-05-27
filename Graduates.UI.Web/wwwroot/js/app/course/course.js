function course(course) {
    this.id = course !== null ? course.id : 0;
    this.name = course !== null ? course.name : null;
    this.email = course !== null ? course.email : null;
    this.phone = course !== null ? course.phone : null;
    this.institutionId = course !== null ? course.institutionId : 0;
    this.facultyId = course !== null ? course.facultyId : 0;
    this.departmentId = course !== null ? course.departmentId : 0;
    this.programs = [];
    this.program = new program(null);
}

function program(program) {
    this.id = program !== null ? program.id : 0;
    this.qualificationTypeId = program !== null ? program.qualificationTypeId : '';
    this.qualificationTypeName = program !== null ? program.qualificationTypeName : '';
    this.programDurationUnitId = program !== null ? program.programDurationUnitId : '';
    this.programDurationUnitName = program !== null ? program.programDurationUnitName : '';
    this.duration = program !== null ? program.duration : 0; 
}

var infoViewModel = {
    course: new course(null),
    institutions: [],
    faculties: [],
    departments: [],
    qualificationTypes: [],
    programDurationUnits: []
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
                "url": "/course/data/"
            },
            "columns": [
                { "title": "Id", "data": "id", "visible": false },
                { "title": "Name", "data": "name", "searchable":true, "width": "20%" },
                { "title": "Email", "data": "email", "searchable": true },
                { "title": "Phone", "data": "phone", "searchable": true },
                { "title": "Institution", "data": "institution", "searchable": true },
                { "title": "Faculty", "data": "faculty", "searchable": true },
                { "title": "Department", "data": "department", "searchable": true },
                {
                    render: function (o, type, data) {
                        var links = "<a class='md-btn' id='edit' href='javascript:;' data-url-form='course/create' data-url-data='course/Edit/" + data.id;
                        links += "' data-id='" + data.id + "' onClick='return editRow(this)'><span class='fa fa-edit'></span></a>" + "&nbsp;";
                        links += "<a href='course/Delete/" + data.id + "'><span class='fa fa-trash'></span></a>";
                        return links;
                    }
                }
            ],
            "lengthMenu": [[10, 25, 50, 100], [10, 25, 50, 100]],
            "columnDefs": [
                { "width": "6%", "targets": -1, "sortable": false }
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
            duration: {
                validators: {
                    notEmpty: {
                        message: '<strong>Duration</strong> Required'
                    },
                    number: {
                        message: '<strong>Invalid </strong> Required'
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
                data: appObject.vm.course,
                url: $('#form').attr('action'),
                token: $("input[name = '__RequestVerificationToken']").val()
            });
            $(document).off('graduate.submitEvent').on('graduate.submitEvent', function () {
                if (appObject.vm.course.id === 0) {
                    appObject.vm.course = new course(null);
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


function validateInnerForm() {
    $('#form-inner').bootstrapValidator({
        container: '#messages',
        feedbackIcons: {
            valid: 'glyphicon glyphicon-ok',
            invalid: 'glyphicon glyphicon-remove',
            validating: 'glyphicon glyphicon-refresh'
        },
        fields: {
            duration: {
                validators: {
                    notEmpty: {
                        message: '<strong>Duration</strong> Required'
                    },
                    number: {
                        message: '<strong>Invalid </strong> Required'
                    }
                }
            },
            qualificationTypeId: {
                validators: {
                    notEmpty: {
                        message: '<strong>Qualification Type</strong> Required'
                    }
                }
            }
        },
        onSuccess: function (e) {
            let pgrm = Object.assign({}, appObject.vm.course.program);
            pgrm= getQualificationTypeName(pgrm);
            addProgram(pgrm);
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
    showModal(0, '/course/create', null);
    appObject.vm.course = new course(null);
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
        infoViewModel.qualificationTypes = new GetDataFromServer().loadData('/SetUpValue/GetSetUpValues', { 'setUpName': 'Qualification Type' }, 'setUpValues', false);
        infoViewModel.programDurationUnits = new GetDataFromServer().loadData('/SetUpValue/GetSetUpValues', { 'setUpName': 'Program Duration Unit' }, 'setUpValues', false);

        if (id > 0) {
            infoViewModel.course = new GetDataFromServer().loadData(dataURL, null, 'course', false);
            infoViewModel.faculties = new GetDataFromServer().loadData('/faculty/GetFacultiesByInstitutionId', { 'institutionId': infoViewModel.course.institutionId }, 'faculties', false);
            infoViewModel.departments = new GetDataFromServer().loadData('/department/GetDepartmentsByFacultyId', { 'facultyId': infoViewModel.course.facultyId }, 'departments', false);
        }

        appObject = new Vue({
            el: "#modal-body",
            data: {
                vm: infoViewModel
            },
            methods: {
                loadFaculties: function () {
                    infoViewModel.faculties = new GetDataFromServer().loadData('/faculty/GetFacultiesByInstitutionId', { 'institutionId': appObject.vm.course.institutionId }, 'faculties', false);
                    appObject.vm.course.departmentId = 0;
                    appObject.vm.course.facultyId = 0;
                },
                loadDepartments: function () {
                    infoViewModel.departments = new GetDataFromServer().loadData('/department/GetDepartmentsByFacultyId', { 'facultyId': appObject.vm.course.facultyId }, 'departments', false);
                }
            }
        });

        $('#modal-dialog').modal({
            show: true,
            keyboard: false,
            backdrop: 'static'
        });

        validate();
    });
}

function openProgramDialog() {
    $('#modal-dialog-inner').modal({
        show: true,
        keyboard: false,
        backdrop: 'static'
    });

    validateInnerForm();
}

//updating the Qualification Type Name and Program Duration Unit Name
//before adding to the program table
function getQualificationTypeName(obj) {
    let programUpdated = obj;
    let selectedPrgmName = null;
    let selectedUnitName = null;
    
    appObject.vm.qualificationTypes.forEach(function (value) {
        if (value.id === parseInt(obj.qualificationTypeId)) {
            selectedPrgmName = value.name;
        }
    });

    appObject.vm.programDurationUnits.forEach(function (value) {
        if (value.id === parseInt(obj.programDurationUnitId)) {
            selectedUnitName = value.name;
        }
    });
    programUpdated.qualificationTypeName = selectedPrgmName;
    programUpdated.programDurationUnitName = selectedUnitName;
    return programUpdated;
}

function dismissInnerModal() {
    $('#modal-dialog-inner').modal('hide');
}

function formSubmit() {
    $('#sub-btn').submit();
}

function formProgramSubmit() {
    $('#sub-btn-inner').submit();
}

//adds program person to programs Array after checking for duplicates or update request
function addProgram(obj) {
    let prg = JSON.parse(JSON.stringify(obj));
    if (programExist(prg)) {
        return;
    }
    //checking to update existing item in the array
    else if (prg.id !== 0) {
        appObject.vm.course.programs.forEach(function (value, index) {
            if (prg.id === value.id) {
                value = prg;
            }
        });
    }
    else {
        prg.id = Math.floor(1000 + Math.random() * 9000);
        appObject.vm.course.programs.push(prg);
        appObject.vm.course.program = new program(null);
    }
}

//checks to see if program already exist
function programExist(program) {
    var state = false;
    appObject.vm.course.programs.forEach(function (value, index) {
        if (program.id === '0'
            && program.duration === value.duration
            && program.qualificationTypeId === value.qualificationTypeId) {
            state = true;
        }
    });
    return state;
}

//sets up the contact person table display for delete operations using javascript's hooks
function deleteProgram(obj) {
   
    $(obj).confirmation({
        onConfirm: function () {
            let id = $(obj).attr('data-row-id');
            var arrayIndex = null;
            appObject.vm.course.programs.forEach(function (value, index) {
                if (parseInt(id) === parseInt(value.id)) {
                    arrayIndex = index;
                }
            });
            appObject.vm.course.programs.splice(arrayIndex, 1);
        }
    });
}

function editProgram(obj) {
    var id = $(obj).attr('data-row-id');
    appObject.vm.course.programs.forEach(function (value, index) {
        if (parseInt(id) === parseInt(value.id)) {
            appObject.vm.course.program = value;
        }
    });
    openProgramDialog();
}