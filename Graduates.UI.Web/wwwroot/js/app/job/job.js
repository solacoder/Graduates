function job(job) {
    this.id = job !== null ? job.id : 0;
    this.company = job !== null ? job.company : null;
    this.title = job !== null ? job.title : '';
    this.location = job !== null ? job.location : '';
    this.requirements = job !== null ? job.requirements : null;
    this.responsibilities = job !== null ? job.responsibilities : '';
    this.howToApply = job !== null ? job.howToApply : '';
    this.email = job !== null ? job.email : '';
    this.phone = job !== null ? job.phone : '';
    this.startDate = job !== null ? job.startDate : '';
    this.endDate = job !== null ? job.endDate : '';
    this.status = job !== null ? job.status : '';
    this.isActive = job !== null ? job.isActive : true;
}

var infoViewModel = {
    job: new job(null),
    locations: []
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
                "url": "/job/approvedjobs/"
            },
            "columns": [
                { "title": "Id", "data": "id", "visible": false },
                { "title": "Company", "data": "company", "searchable": true },
                { "title": "Title", "data": "title", "searchable": true },
                { "title": "Location", "data": "location", "searchable": true },
                { "title": "Email", "data": "email", "searchable": true },
                { "title": "Phone", "data": "phone", "searchable": true },
                {
                    render: function (o, type, data) {
                        var links = "<a class='md-btn' id='edit' href='javascript:;' data-url-form='job/Create' data-url-data='job/Edit/" + data.id;
                        links += "' data-id='" + data.id + "' onclick='editRow(this)'><span class='fa fa-edit'></span></a>" + "&nbsp;";
                        links += "<a href='job/Delete/" + data.id + "'><span class='fa fa-trash'></span></a>";
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
        dt = $('#data-table').DataTable({
            "serverSide": true,
            "processing": true,
            "ajax": {
                "url": "/job/unapprovedjobs/"
            },
            "columns": [
                { "title": "Id", "data": "id", "visible": false },
                { "title": "Company", "data": "company", "searchable": true },
                { "title": "Title", "data": "title", "searchable": true },
                { "title": "Location", "data": "location", "searchable": true },
                { "title": "Email", "data": "email", "searchable": true },
                { "title": "Phone", "data": "phone", "searchable": true },
                {
                    render: function (o, type, data) {
                        var links = "<a class='md-btn' id='edit' href='javascript:;' data-url-form='job/Create' data-url-data='job/Edit/" + data.id;
                        links += "' data-id='" + data.id + "' onclick='editRow(this)'><span class='fa fa-edit'></span></a>" + "&nbsp;";
                        links += "<a href='job/Delete/" + data.id + "'><span class='fa fa-trash'></span></a>";
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
            appObject.vm.job.Description = CKEDITOR.instances.Description.getData();
            appObject.vm.job.requirements = CKEDITOR.instances.Requirements.getData();
            appObject.vm.job.responsibilities = CKEDITOR.instances.Responsibilities.getData();
            appObject.vm.job.howToApply = CKEDITOR.instances.HowToApply.getData();
            

            formHelper.saveData({
                data: appObject.vm.job,
                url: $('#form').attr('action'),
                token: $("input[name = '__RequestVerificationToken']").val()
            });
            $(document).off('graduate.submitEvent').on('graduate.submitEvent', function () {
                if (appObject.vm.job.id === 0) {
                    appObject.vm.job = new job(null);
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
    showModal(0, '/job/create', null);
    infoViewModel.job = new job(null);
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
        infoViewModel.job = new GetDataFromServer().loadData(dataURL, null, 'job', false);
    }

    appObject = new Vue({
        el: "#modal-body",
        data: {
            vm: infoViewModel
        }
    });

    //initializing the CK Editor
    CKEDITOR.replace('Description');
    CKEDITOR.replace('Requirements');
    CKEDITOR.replace('Responsibilities');
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

