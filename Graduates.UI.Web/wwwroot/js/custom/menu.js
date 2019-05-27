(function () {

    //Cache the DOM
    var body = $('body');

    //Cache the parent links that has sub-links on the menu
    var sidebarParentLink = body.find('ul#sidemenuNav > li > a');

    //Cache the sub-links
    var sidebarChildLink = body.find('ul#sidemenuNav > li > a + ul > li > a');

    //TODO: Placing parent link text on the page breadcrumb 
    sidebarParentLink.click(function () {
        var activeParentLink = $(this).find('span').text();
        body.find('#activeParentLink').text(activeParentLink);
        body.find('#activeParentHeader').text(activeParentLink);
    });

    /*
        TODO: Placing sub-link text on the page breadcrumb and 
        getting the html content for that page dynamically base
        on the url attribute.
    */
    sidebarChildLink.click(function (e) {

        e.preventDefault();
        console.log(e.target);

        $('ul#sidemenuNav > li > a + ul > li > a').removeClass('active-sub-link')
        $(e.target).addClass('active-sub-link');

        var activeChildLink = $(this).text();
        var mainContent = body.find('main');
        var url = $(this).attr('href');

        body.find('#activeChildLink').text(activeChildLink);
        body.find('main').addClass('active').removeClass('auto-height');

        body.find('.page-header').addClass('adjust-margin-bottom');

        $.ajax({
            url: url,
            dataType: "html",
            beforeSend: function () {
                mainContent.empty();
                mainContent.html('<img id="loader-img" alt="" src="assets/img/Ajax2.gif" width="50" height="50" style="position: absolute; right: 50%; top: 30vh;" />');
            },
            success: function (data) {
                $('main').html(data);
            }
        });
    });
}());