function institution(institution) {
    this.id = institution !== null ? institution.id : 0;
    this.name = institution !== null ? institution.name : null;
    this.email = institution !== null ? institution.email : null;
    this.phone = institution !== null ? institution.phone : null;
    this.address = institution !== null ? institution.address : null;
    this.yearEstablished = institution !== null ? institution.yearEstablished : null;
    this.webSite = institution !== null ? institution.webSite : null;
    this.typeId = institution !== null ? institution.typeId : 0;
    this.countyId = institution !== null ? institution.countyId : 0;
    this.ownerShipTypeId = institution !== null ? institution.ownerShipTypeId : 0;
}

var infoViewModel = {
    institution: new institution(null),
    types: [],
    ownerShipTypes: [],
    counties: []
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
                "url": "/institution/data/"
            },
            "columns": [
                { "title": "Id", "data": "id", "visible": false },
                { "title": "Name", "data": "name", "searchable": true },
                { "title": "Email", "data": "email", "searchable": true },
                { "title": "Phone", "data": "phone", "searchable": true },
                { "title": "Year Established", "data": "yearEstablished", "searchable": true },
                { "title": "Ownership Type", "data": "ownershipType", "searchable": true },
                { "title": "County", "data": "county", "searchable": true },
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
}
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
            typeId: {
                validators: {
                    notEmpty: {
                        message: '<strong>Type</strong> Required'
                    }
                }
            },
            ownerShipTypeId: {
                validators: {
                    notEmpty: {
                        message: '<strong>Ownership Type</strong> Required'
                    }
                }
            },
            address: {
                validators: {
                    notEmpty: {
                        message: '<strong>Address</strong> Required'
                    }
                }
            },
            yearEstablished: {
                validators: {
                    notEmpty: {
                        message: '<strong>Year Established</strong> Required'
                    }
                }
            }
        },
        onSuccess: function (e) {
            formHelper.saveData({
                data: appObject.vm.institution,
                url: $('#form').attr('action'),
                token: $("input[name = '__RequestVerificationToken']").val()
            });
            $(document).off('graduate.submitEvent').on('graduate.submitEvent', function () {
                if (appObject.vm.institution.id === 0) {
                    appObject.vm.institution = new institution(null);
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
    showModal(0, '/institution/create', null);
    appObject.vm.institution = new institution(null);
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
        infoViewModel.ownerShipTypes = new GetDataFromServer().loadData('/SetUpValue/GetSetUpValues', { 'setUpName': 'OwnerShip Type' }, 'setUpValues', false);
        infoViewModel.types = new GetDataFromServer().loadData('/SetUpValue/GetSetUpValues', { 'setUpName': 'Institution Type' }, 'setUpValues', false);

        if (id > 0) {
            
            infoViewModel.institution = new GetDataFromServer().loadData(dataURL, null, 'institution', false);
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