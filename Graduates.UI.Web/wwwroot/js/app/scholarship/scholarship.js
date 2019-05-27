function scholarship(scholarship) {
    this.id = scholarship !== null ? scholarship.id : 0;
    this.name = scholarship !== null ? scholarship.name : null;
    this.website = scholarship !== null ? scholarship.website : '';
    this.sponsor = scholarship !== null ? scholarship.sponsor : '';
    this.audience = scholarship !== null ? scholarship.audience : null;
    this.requirements = scholarship !== null ? scholarship.requirements : null;
    this.interests = scholarship !== null ? scholarship.interests : '';
    this.howToApply = scholarship !== null ? scholarship.howToApply : '';
    this.email = scholarship !== null ? scholarship.email : '';
    this.phone = scholarship !== null ? scholarship.phone : '';
    this.startDate = scholarship !== null ? scholarship.startDate : '';
    this.endDate = scholarship !== null ? scholarship.endDate : '';
    this.isActive = scholarship !== null ? scholarship.isActive : true;
}

var infoViewModel = {
    scholarship: new scholarship(null)
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
                "url": "/scholarship/approvedscholarships/"
            },
            "columns": [
                { "title": "Id", "data": "id", "visible": false },
                { "title": "Name", "data": "name", "searchable": true },
                { "title": "Website", "data": "website", "searchable": true },
                { "title": "Sponsor", "data": "sponsor", "searchable": true },
                { "title": "Email", "data": "email", "searchable": true },
                { "title": "Phone", "data": "phone", "searchable": true },
                {
                    render: function (o, type, data) {
                        var links = "<a class='md-btn' id='edit' href='javascript:;' data-url-form='scholarship/Create' data-url-data='scholarship/Edit/" + data.id;
                        links += "' data-id='" + data.id + "' onclick='editRow(this)'><span class='fa fa-edit'></span></a>" + "&nbsp;";
                        links += "<a href='scholarship/Delete/" + data.id + "'><span class='fa fa-trash'></span></a>";
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


gridListUnapprovedVM = {
    dt: null,
    init: function () {
        dt = $('#data-table-unapproved').DataTable({
            "serverSide": true,
            "processing": true,
            "ajax": {
                "url": "/scholarship/unapprovedscholarships/"
            },
            "columns": [
                { "title": "Id", "data": "id", "visible": false },
                { "title": "Name", "data": "name", "searchable": true },
                { "title": "Website", "data": "website", "searchable": true },
                { "title": "Sponsor", "data": "sponsor", "searchable": true },
                { "title": "Email", "data": "email", "searchable": true },
                { "title": "Phone", "data": "phone", "searchable": true },
                {
                    render: function (o, type, data) {
                        var links = "<a class='md-btn' id='edit' href='javascript:;' data-url-form='scholarship/Create' data-url-data='scholarship/Edit/" + data.id;
                        links += "' data-id='" + data.id + "' onclick='editRow(this)'><span class='fa fa-edit'></span></a>" + "&nbsp;";
                        links += "<a href='scholarship/Delete/" + data.id + "'><span class='fa fa-trash'></span></a>";
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
gridListUnapprovedVM.init();

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
            webiste: {
                validators: {
                    notEmpty: {
                        message: '<strong>Website</strong> Required'
                    }
                }
            },
            sponsor: {
                validators: {
                    notEmpty: {
                        message: '<strong>Sponsor</strong> Required'
                    }
                }
            },
            audience: {
                validators: {
                    notEmpty: {
                        message: '<strong>Audience</strong> Required'
                    }
                }
            },
            requirements: {
                validators: {
                    notEmpty: {
                        message: '<strong>Requirements</strong> Required'
                    }
                }
            }
        },
        onSuccess: function (e) {
            appObject.vm.scholarship.audience = CKEDITOR.instances.Audience.getData();
            appObject.vm.scholarship.requirements = CKEDITOR.instances.Requirements.getData();
            appObject.vm.scholarship.interests = CKEDITOR.instances.Interests.getData();
            appObject.vm.scholarship.howToApply = CKEDITOR.instances.HowToApply.getData();
            

            formHelper.saveData({
                data: appObject.vm.scholarship,
                url: $('#form').attr('action'),
                token: $("input[name = '__RequestVerificationToken']").val()
            });
            $(document).off('graduate.submitEvent').on('graduate.submitEvent', function () {
                if (appObject.vm.scholarship.id === 0) {
                    appObject.vm.scholarship = new scholarship(null);
                    $('#form').bootstrapValidator('resetForm', true);
                    CKEDITOR.instances.Audience.setData('');
                    CKEDITOR.instances.Requirements.setData('');
                    CKEDITOR.instances.Interests.setData('');
                    CKEDITOR.instances.HowToApply.setData('');
                    $('#form').trigger('reset');
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
    showModal(0, '/scholarship/create', null);
    infoViewModel.scholarship = new scholarship(null);
});

function editRow(sender) {
    var formURL = $(sender).attr('data-url-form');
    var id = $(sender).attr('data-id');
    var dataURL = $(sender).attr('data-url-data');
    showModal(id, formURL, dataURL);
}

function showModal(id, formURL, dataURL) {

    var loadHelper = new MyAjaxCall();
    loadHelper.load(formURL, '.modal-body', null, false);

    if (id > 0) {
        infoViewModel.scholarship = new GetDataFromServer().loadData(dataURL, null, 'scholarship', false);
    }

    appObject = new Vue({
        el: "#modal-body",
        data: {
            vm: infoViewModel
        }
    });

    //initializing the CK Editor
    CKEDITOR.replace('Audience');
    CKEDITOR.replace('Requirements');
    CKEDITOR.replace('Interests');
    CKEDITOR.replace('HowToApply');

    $('#modal-dialog').modal({
        show: true,
        keyboard: false,
        backdrop: 'static'
    });

    validate();
}

function formSubmit() {
    $('#sub-btn').submit();
}

