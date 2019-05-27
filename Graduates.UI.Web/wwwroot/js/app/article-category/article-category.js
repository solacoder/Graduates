function articleCategory(articleCategory) {
    this.id = articleCategory !== null ? articleCategory.id : 0;
    this.name = articleCategory !== null ? articleCategory.name : null;
    this.description = articleCategory !== null ? articleCategory.description : null;
}

var infoViewModel = {
    articleCategory: new articleCategory(null)
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
                "url": "/articlecategory/data/"
            },
            "columns": [
                { "title": "Id", "data": "id", "visible": false },
                { "title": "Name", "data": "name", "searchable": true },
                { "title": "Description", "data": "description", "searchable": true },
                {
                    render: function (o, type, data) {
                        var links = "<a class='md-btn' id='edit' href='#' data-url-form='articlecategory/Create' data-url-data='articlecategory/Edit/" + data.id;
                        links += "' data-id='" + data.id + "' onClick='return editRow(this)'><span class='fa fa-edit'></span></a>" + "&nbsp;";
                        links += "<a href='articlecategory/Delete/" + data.id + "'><span class='fa fa-trash'></span></a>";
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
            description: {
                validators: {
                    notEmpty: {
                        message: '<strong>Description</strong> Required'
                    }
                }
            }
        },
        onSuccess: function (e) {
            formHelper.saveData({
                data: appObject.vm.articleCategory ,
                url: $('#form').attr('action'),
                token: $("input[name = '__RequestVerificationToken']").val()
            });
            $(document).off('graduate.submitEvent').on('graduate.submitEvent', function () {
                if (appObject.vm.articleCategory .id === 0) {
                    appObject.vm.articleCategory = new articleCategory (null);
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
    showModal(0, '/articlecategory/create', null);
    appObject.vm.articleCategory = new articleCategory(null);
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

        if (id > 0) {
            
            infoViewModel.articleCategory = new GetDataFromServer().loadData(dataURL, null, 'articleCategory', false);
            console.log(JSON.stringify(infoViewModel.articleCategory));
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