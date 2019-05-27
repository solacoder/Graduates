function newsCategory(newsCategory) {
    this.id = newsCategory !== null ? newsCategory.id : 0;
    this.name = newsCategory !== null ? newsCategory.name : null;
    this.description = newsCategory !== null ? newsCategory.description : null;
}

var infoViewModel = {
    newsCategory: new newsCategory(null)
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
                "url": "/newscategory/data/"
            },
            "columns": [
                { "title": "Id", "data": "id", "visible": false },
                { "title": "Name", "data": "name", "searchable": true },
                { "title": "Description", "data": "description", "searchable": true },
                {
                    render: function (o, type, data) {
                        var links = "<a class='md-btn' id='edit' href='#' data-url-form='newscategory/Create' data-url-data='newscategory/Edit/" + data.id;
                        links += "' data-id='" + data.id + "' onClick='return editRow(this)'><span class='fa fa-edit'></span></a>" + "&nbsp;";
                        links += "<a href='newscategory/Delete/" + data.id + "'><span class='fa fa-trash'></span></a>";
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
                data: appObject.vm.newsCategory ,
                url: $('#form').attr('action'),
                token: $("input[name = '__RequestVerificationToken']").val()
            });
            $(document).off('graduate.submitEvent').on('graduate.submitEvent', function () {
                if (appObject.vm.newsCategory .id === 0) {
                    appObject.vm.newsCategory = new newsCategory (null);
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
    showModal(0, '/newscategory/create', null);
    appObject.vm.newsCategory = new newsCategory(null);
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
            
            infoViewModel.newsCategory = new GetDataFromServer().loadData(dataURL, null, 'newsCategory', false);
            console.log(JSON.stringify(infoViewModel.newsCategory));
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