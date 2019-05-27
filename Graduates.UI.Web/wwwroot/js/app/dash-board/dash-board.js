function dashBoardItem(dashBoardItem) {
    this.id = dashBoardItem !== null ? dashBoardItem.id : 0;
    this.name = dashBoardItem !== null ? dashBoardItem.name : null;
    this.description = dashBoardItem !== null ? dashBoardItem.description : '';
    this.value = dashBoardItem !== null ? dashBoardItem.value : '';
}

var infoViewModel = {
    institutions: new dashBoardItem(null),
    faculties: new dashBoardItem(null),
    departments: new dashBoardItem(null),
    programs: new dashBoardItem(null),
    courses: new dashBoardItem(null),
    news: new dashBoardItem(null),
    articles: new dashBoardItem(null),
    candidates: new dashBoardItem(null)
};

var appObject = null; 
var items = new GetDataFromServer().loadData('/dashboard/index', null, 'dashBoardItems', false);
if (items !== null) {
    infoViewModel['institutions'] = items['institutions'];
    infoViewModel['faculties'] = items['faculties'];
    infoViewModel['departments'] = items['departments'];
    infoViewModel['courses'] = items['courses'];
    infoViewModel['programs'] = items['programs'];
    infoViewModel['candidates'] = items['candidates'];
    infoViewModel['news'] = items['news'];
    infoViewModel['articles'] = items['articles'];
}
appObject = new Vue({
    el: "#modal-body",
    data: {
        vm: infoViewModel
    }
});

console.log(JSON.stringify(infoViewModel));
