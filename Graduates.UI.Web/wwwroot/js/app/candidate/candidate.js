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
    candidate: new candidate(null),
    counties: [],
    sexes: []
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
                "url": "/candidate/data/"
            },
            "columns": [
                { "title": "Id", "data": "id", "visible": false },
                { "title": "Candidate No", "data": "candidateNo", "searchable": true },
                { "title": "Name", "data": "name", "searchable": true },
                { "title": "Email", "data": "email", "searchable": true },
                { "title": "Phone", "data": "phone", "searchable": true },
                { "title": "Date Of Birth", "data": "dateOfBirth", "searchable": true },
                { "title": "Sex", "data": "sex", "searchable": true },
                {
                    render: function (o, type, data) {
                        var links = "<a class='md-btn' id='edit' href='#' data-url-form='candidate/create' data-url-data='candidate/edit/" + data.id;
                        links += "' data-id='" + data.id + "' onClick='return editRow(this)'><span class='fa fa-edit'></span></a>" + "&nbsp;";
                        links += "<a href='candidate/Delete/" + data.id + "'><span class='fa fa-trash'></span></a>";
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
                data: appObject.vm.candidate,
                url: $('#form').attr('action'),
                token: $("input[name = '__RequestVerificationToken']").val()
            });
            $(document).off('graduate.submitEvent').on('graduate.submitEvent', function () {
                if (appObject.vm.candidate.id === 0) {
                    appObject.vm.candidate = new candidate(null);
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
    showModal(0, '/candidate/create', null);
    appObject.vm.candidate = new candidate(null);
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

        infoViewModel.counties = new GetDataFromServer().loadData('/SetUpValue/GetSetUpValues', { 'setUpName': 'County' }, 'setUpValues', false);
        infoViewModel.sexes = new GetDataFromServer().loadData('/SetUpValue/GetSetUpValues', { 'setUpName': 'Sex' }, 'setUpValues', false);
        
        if (id > 0) {

            infoViewModel.candidate = new GetDataFromServer().loadData(dataURL, null, 'candidate', false);
        }

        appObject = new Vue({
            el: "#modal-body",
            data: {
                vm: infoViewModel
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