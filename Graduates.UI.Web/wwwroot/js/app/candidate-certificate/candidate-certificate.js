function candidateCert(candidateCert) {
    this.id = candidateCert !== null ? candidateCert.id : 0;
    this.candidateId = candidateCert !== null ? candidateCert.candidateId : null;
    this.candidateCertNo = candidateCert !== null ? candidateCert.candidateCertNo : null;
    this.candidate = new candidate(null),
    this.gradeId = candidateCert !== null ? candidateCert.gradeId : '';
    this.yearObtained = candidateCert !== null ? candidateCert.yearObtained : null;
    this.certificateTitle = candidateCert !== null ? candidateCert.certificateTitle : null;
    this.institutionId = candidateCert !== null ? candidateCert.institutionId : '';
    this.facultyId = candidateCert !== null ? candidateCert.facultyId : '';
    this.departmentId = candidateCert !== null ? candidateCert.departmentId : '';
    this.courseId = candidateCert !== null ? candidateCert.courseId : '';
    this.programId = candidateCert !== null ? candidateCert.programId : '';
}
function candidate(candidate) {
    this.id = candidate !== null ? candidate.id : 0;
    this.candidateNo = candidate !== null ? candidate.candidateNo : null;
    this.firstName = candidate !== null ? candidate.firstName : null;
    this.middleName = candidate !== null ? candidate.middleName : null;
    this.lastName = candidate !== null ? candidate.lastName : null;
    this.phone = candidate !== null ? candidate.phone : null;
    this.email = candidate !== null ? candidate.email : null;
    this.sexId = candidate !== null ? candidate.sexId : '';
    this.dateOfBirth = candidate !== null ? candidate.dateOfBirth : null;
    this.countyId = candidate !== null ? candidate.countyId : '';
}
var infoViewModel = {
    candidateCert: new candidateCert(null),
    institutions: [],
    faculties: [],
    departments: [],
    courses: [],
    programs: [],
    grades: [],
    isEntryDisabled: true,
    isFacultyEnabled: false,
    isDepartmentEnabled: false,
    isCourseEnabled: false,
    isProgramEnabled: false
};

var appObject = null;

var gridListVM, gridCandidateSearch;

gridListVM = {
    dt: null,
    init: function () {
        dt = $('#data-table').DataTable({
            "serverSide": true,
            "processing": true,
            "ajax": {
                "url": "/candidateCertificate/data/"
            },
            "columns": [
                { "title": "Id", "data": "id", "visible": false },
                { "title": "Cert. No", "data": "certificateNo", "searchable": true },
                { "title": "Cert. Title", "data": "certificateTitle", "searchable": true },
                { "title": "Year Obtained", "data": "yearObtained", "searchable": true },
                { "title": "Grade", "data": "grade", "searchable": true },
                { "title": "Student No", "data": "studentNo", "searchable": true },
                {
                    render: function (o, type, data) {
                        var links = "<a class='md-btn' id='edit' href='javascript:;' data-url-form='candidateCertificate/create' data-url-data='candidateCertificate/edit/" + data.id;
                        links += "' data-id='" + data.id + "' onClick='return editRow(this)'><span class='fa fa-edit'></span></a>" + "&nbsp;";
                        links += "<a href='candidateCert/Delete/" + data.id + "'><span class='fa fa-trash'></span></a>";
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

gridCandidateSearch = {
    dt: null,
    init: function () {
        dt = $('#data-table-candidate-search').DataTable({
            "serverSide": true,
            "processing": true,
            "ajax": {
                "url": "/candidate/data/"
            },
            "columns": [
                { "title": "Id", "data": "id", "visible": false },
                { "title": "Candidate No", "data": "candidateNo", "searchable": true, "width":"10%" },
                { "title": "Name", "data": "name", "searchable": true, "width": "35%" },
                { "title": "Email", "data": "email", "searchable": true, "width": "35%" },
                { "title": "Phone", "data": "phone", "searchable": true, "width": "5%" },
                { "title": "DOB", "data": "dateOfBirth", "searchable": true, "width": "10%" },
                { "title": "Sex", "data": "sex", "searchable": true, "width": "5%" },
                {
                    render: function (o, type, data) {
                        var links = "<a class='md-btn' id='edit' href='#' data-url-form='candidate/create' data-url-data='candidate/edit/" + data.id;
                        links += "' data-id='" + data.id + "' onClick='return selectCandidate(this)' onmouseover='return selectCandidate(this)'>Select</a>" + "&nbsp;";
                        
                        return links;
                    }
                }
            ],
            "lengthMenu": [[10, 25, 50, 100], [10, 25, 50, 100]]
            //,
            //"columnDefs": [
            //    { "width": "8%", "targets": -1, "sortable": false }
            //]
        });
    }
};

function validate() {
    $('#form').bootstrapValidator({
        container: '#messages',
        feedbackIcons: {
            valid: 'glyphicon glyphicon-ok',
            invalid: 'glyphicon glyphicon-remove',
            validating: 'glyphicon glyphicon-refresh'
        },
        fields: {
            firstName: {
                validators: {
                    notEmpty: {
                        message: '<strong>First Name</strong> Required'
                    }
                }
            },
            lastName: {
                validators: {
                    notEmpty: {
                        message: '<strong>First Name</strong> Required'
                    }
                }
            },
            middleName: {
                validators: {
                    notEmpty: {
                        message: '<strong>First Name</strong> Required'
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
            },
            countyId: {
                validators: {
                    notEmpty: {
                        message: '<strong>County</strong> Required'
                    }
                }
            },
            sexId: {
                validators: {
                    notEmpty: {
                        message: '<strong>Sex</strong> Required'
                    }
                }
            }
        },
        onSuccess: function (e) {
            formHelper.saveData({
                data: appObject.vm.candidateCert,
                url: $('#form').attr('action'),
                token: $("input[name = '__RequestVerificationToken']").val()
            });
            $(document).off('graduate.submitEvent').on('graduate.submitEvent', function () {
                if (appObject.vm.candidateCert.id === 0) {
                    appObject.vm.candidateCert = new candidateCert(null);
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
    showModal(0, '/candidateCertificate/create', null);
    appObject.vm.candidateCert = new candidateCert(null);
});

function editRow(sender) {
    var formURL = $(sender).attr('data-url-form');
    var id = $(sender).attr('data-id');
    var dataURL = $(sender).attr('data-url-data');
    infoViewModel.isEntryDisabled = false;
    showModal(id, formURL, dataURL);
}

function showModal(id, formURL, dataURL) {

    $('.modal-body').load(formURL, function (result) {

        infoViewModel.institutions = new GetDataFromServer().loadData('/institution/getAll', null, 'institutions', false);
        infoViewModel.grades = new GetDataFromServer().loadData('/SetUpValue/GetSetUpValues', { 'setUpName': 'Grade' }, 'setUpValues', false);
        
        if (id > 0) {
            infoViewModel.candidateCert = new GetDataFromServer().loadData(dataURL, null, 'candidateCertificate', false);
            infoViewModel.faculties = new GetDataFromServer().loadData('/faculty/GetFacultiesByInstitutionId', { 'institutionId': infoViewModel.candidateCert.institutionId }, 'faculties', false);
            infoViewModel.departments = new GetDataFromServer().loadData('/department/GetDepartmentsByFacultyId', { 'facultyId': infoViewModel.candidateCert.facultyId }, 'departments', false);
            infoViewModel.courses = new GetDataFromServer().loadData('/course/GetCoursesByDepartmentId', { 'departmentId': infoViewModel.candidateCert.departmentId }, 'courses', false);
            infoViewModel.programs = new GetDataFromServer().loadData('/program/GetProgramsByCourseId', { 'courseId': infoViewModel.candidateCert.courseId }, 'programs', false);
        }

        appObject = new Vue({
            el: "#modal-body",
            data: {
                vm: infoViewModel
            },
            methods: {
                loadFaculties: function () {
                    infoViewModel.faculties = new GetDataFromServer().loadData('/faculty/GetFacultiesByInstitutionId', { 'institutionId': appObject.vm.candidateCert.institutionId }, 'faculties', false);
                },
                loadDepartments: function () {
                    infoViewModel.departments = new GetDataFromServer().loadData('/department/GetDepartmentsByFacultyId', { 'facultyId': appObject.vm.candidateCert.facultyId }, 'departments', false);
                },
                loadCourses: function () {
                    infoViewModel.courses = new GetDataFromServer().loadData('/course/GetCoursesByDepartmentId', { 'departmentId': appObject.vm.candidateCert.departmentId }, 'courses', false);
                },
                loadPrograms: function () {
                    infoViewModel.programs = new GetDataFromServer().loadData('/program/GetProgramsByCourseId', { 'courseId': appObject.vm.candidateCert.courseId }, 'programs', false);
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

function formSubmit() {
    $('#sub-btn').submit();
}

function openSearchDialog() {
    // initialize the datatables
    gridCandidateSearch.init();

    $('#modal-dialog-inner').modal({
        show: true,
        keyboard: false,
        backdrop: 'static'
    });
}

function selectCandidate(obj) {
    
    $(obj).confirmation({
        onConfirm: function () {
            var id = $(obj).attr('data-id');
            infoViewModel.candidateCert.candidate = new GetDataFromServer().loadData('/candidate/edit'
                , { 'id': id }
                , 'candidate', false);
            infoViewModel.candidateCert.candidateId = infoViewModel.candidateCert.candidate.id;
            infoViewModel.isEntryDisabled = false;
            $('#modal-dialog-inner').modal('hide');
            return false;
            
        }
    });
}

function dismissInnerModal() {
    $('#modal-dialog-inner').modal('hide');
}

//sets up the contact person table display for delete operations using javascript's hooks
function selectItem(obj) {
    var id = $(obj).attr('data-select-url');
    $(obj).confirmation({
        onConfirm: function () {
            var id = $(obj).attr('data-delete-url');
            alert(id);
        }
    });
}