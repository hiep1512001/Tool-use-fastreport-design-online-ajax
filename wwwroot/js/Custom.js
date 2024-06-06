




//Tài liệu để kham khảo và test

$(document).ready(function () {

})
function loadData() {
    $.ajax({
        url: '/crudbasic/Get',
        type: 'get',
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            console.log(result);
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}
function loadDataFRX() {
    $.ajax({
        url: '/crudbasic/ReadfileFRX',
        type: 'get',
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            console.log(result);
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}
function SaveReport() {
    $.ajax({
        url: '/crudbasic/SaveReport',
        type: 'post',
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            console.log(result);
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}
function WriteFileFRX() {
    var id = $('#id').val();
    $.ajax({
        url: '/crudbasic/WriteFileFRX/' + id ,
        type: 'get',
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            console.log(result);
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}