function news(news) {
    this.id = news !== null ? news.id : 0;
    this.title = news !== null ? news.title : null;
    this.subTitle = news !== null ? news.subTitle : '';
    this.content = news !== null ? news.content : '';
    this.thumbNailPath = news !== null ? news.thumbNailPath : null;
    this.bigImagePath = news !== null ? news.bigImagePath : null;
    this.newsCategoryId = news !== null ? news.newsCategoryId : '';
    this.thumbNailImg = news !== null ? news.thumbNailImg : '';
    this.bigImageImg = news !== null ? news.bigImageImg : '';
    this.bigImageFileType = news !== null ? news.bigImageFileType : '';
    this.thumbNailFileType = news !== null ? news.thumbNailFileType : '';
}

var infoViewModel = {
    news: new news(null),
    newsCategories: []
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
                "url": "/news/approvednews/"
            },
            "columns": [
                { "title": "Id", "data": "id", "visible": false },
                { "title": "Title", "data": "title", "searchable": true },
                { "title": "Sub Title", "data": "subTitle", "searchable": true },
                { "title": "Category", "data": "category", "searchable": true },
                {
                    render: function (o, type, data) {
                        var links = "<a class='md-btn' id='edit' href='javascript:;' data-url-form='news/Create' data-url-data='news/Edit/" + data.id;
                        links += "' data-id='" + data.id + "' onclick='editRow(this)'><span class='fa fa-edit'></span></a>" + "&nbsp;";
                        links += "<a href='news/Delete/" + data.id + "'><span class='fa fa-trash'></span></a>";
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


//unapproved news grid
gridListUnapprovedVM = {
    dt: null,
    init: function () {
        dt = $('#data-table-unapproved').DataTable({
            "serverSide": true,
            "processing": true,
            "ajax": {
                "url": "/news/unapprovednews/"
            },
            "columns": [
                { "title": "Id", "data": "id", "visible": false },
                { "title": "Title", "data": "title", "searchable": true },
                { "title": "Sub Title", "data": "subTitle", "searchable": true },
                { "title": "Category", "data": "category", "searchable": true },
                {
                    render: function (o, type, data) {
                        var links = "<a class='md-btn' id='edit' href='javascript:;' data-url-form='news/Create' data-url-data='news/Edit/" + data.id;
                        links += "' data-id='" + data.id + "' onclick='editRow(this)'><span class='fa fa-edit'></span></a>" + "&nbsp;";
                        links += "<a href='news/Delete/" + data.id + "'><span class='fa fa-trash'></span></a>";
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
            title: {
                validators: {
                    notEmpty: {
                        message: '<strong>Title</strong> Required'
                    }
                }
            },
            subTitle: {
                validators: {
                    notEmpty: {
                        message: '<strong>Sub Title</strong> Required'
                    }
                }
            },
            thumbNailURL: {
                validators: {
                    notEmpty: {
                        message: '<strong>ThumbNail URL</strong> Required'
                    }
                }
            },
            bigImageURL: {
                validators: {
                    notEmpty: {
                        message: '<strong>Big Image URL</strong> Required'
                    }
                }
            },
            newsCategoryId: {
                validators: {
                    notEmpty: {
                        message: '<strong>News Category</strong> Required'
                    }
                }
            }
        },
        onSuccess: function (e) {
            appObject.vm.news.content = CKEDITOR.instances.Content.getData();
            
            formHelper.saveData({
                data: appObject.vm.news ,
                url: $('#form').attr('action'),
                token: $("input[name = '__RequestVerificationToken']").val()
            });
            $(document).off('graduate.submitEvent').on('graduate.submitEvent', function () {
                if (appObject.vm.news.id === 0) {
                    appObject.vm.news = new news (null);
                    $('#form').bootstrapValidator('resetForm', true);
                    CKEDITOR.instances.Content.setData('');
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
    showModal(0, '/news/create', null);
    infoViewModel.news = new news(null);
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

    infoViewModel.newsCategories = new GetDataFromServer().loadData('/newscategory/getAll', null, 'newsCategories', false);

    if (id > 0) {

        infoViewModel.news = new GetDataFromServer().loadData(dataURL, null, 'news', false);
    }

    appObject = new Vue({
        el: "#modal-body",
        data: {
            vm: infoViewModel
        }
    });

    //initializing the CK Editor
    CKEDITOR.replace('Content');

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

function getBigImageURL(event) {
    let file = event.target.files[0];
    setImage(file, 'bigImage');
}

function getThumbNail(event) {
    let file = event.target.files[0];
    setImage(file, 'thumbNail');
}

function openThumbNailDialog() {
    let imgTag = `<img style="max-width:300;" src="${appObject.vm.news.thumbNailImg}" alt="my image" />`;
    document.getElementById('image-display').innerHTML = imgTag;
    showInnerModal();
}

function openBigImageDialog() {
    let imgTag = `<img style="max-width:500;" src="${appObject.vm.news.bigImageImg}" alt="my image" />`;
    document.getElementById('image-display').innerHTML = imgTag;
    showInnerModal();
}

function showInnerModal() {
    $('#modal-dialog-inner').modal({
        show: true,
        keyboard: false,
        backdrop: 'static'
    });
}

function dismissImageModal() {
    $('#modal-dialog-inner').modal('hide');
}

function setImage(file, imageType) {
    let dataURL = null;

    if (file) {
        if (file.type === 'image/png' ||
            file.type === 'image/jpg' ||
            file.type === 'image/jpeg') {

            var reader = new FileReader();

            reader.onload = function (evt) {
                
                if (imageType === "bigImage") {
                    appObject.vm.news.bigImageImg = evt.target.result;
                    appObject.vm.news.bigImageFileType = file.type;
                }
                else if (imageType === "thumbNail") {
                    appObject.vm.news.thumbNailImg = evt.target.result;
                    appObject.vm.news.thumbNailFileType = file.type;
                }
                alert("Image succefully loaded");
            };

            reader.onerror = function (evt) {
                console.error("An error ocurred reading the file", evt);
            };

            reader.readAsDataURL(file);

        } else {
            alert("Please provide a png or jpg image.");
            return false;
        }
    }

    return dataURL;
}