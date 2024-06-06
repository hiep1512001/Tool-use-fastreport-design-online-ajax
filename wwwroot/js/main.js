
$(document).ready(function () {

    LoadData()
});
var ListReport;
function validate() {
    var isValid = true;
    if ($('#idReport').val().trim() == "") {
        $('#idReport').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#idReport').css('border-color', 'lightgrey');
    }
    return isValid;
}
function KtIdTonTai(id) {
    var isValid = false;
    $.each(ListReport, function (key, item) {
        if (item.id == id) {
            isValid = true;
        }
    });
    return isValid;
}
function LoadData() {
    $.ajax({
        url: '/Home/GetListReport',
        type: 'get',
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            ListReport = result;
            var html = '';
            $.each(result, function (key, item) {
                html += '<option value=' + item.id + ' >' + item.name + '</option>';
            });
            $('#selectName').html(html);
            var select_box_element = document.querySelector('#selectName');
            dselect(select_box_element, {
                search: true
            });
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}
function changeReport() {
    var idReport = document.querySelector('#selectName').value;
    document.getElementById("idReport").value = idReport;
    $.each(ListReport, function (key, item) {
        if (item.id == idReport) {
            console.log(item);
            document.getElementById("Discription").value = item.description
        }
    });
};
function GetReportAjax() {
    var res = validate();
    if (res == false) {
        return false;
    }
    var id = $('#idReport').val();
    var kt = KtIdTonTai(id);
    if (kt == false) {
        alert("Không có report có id=" + id);
        return false;
    }
    $.ajax({
        url: '/Home/GetReportAjax/'+ id,
        type: 'get',
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            console.log(result.result.value)
            var html = '',
                html = html + result.result.value
            $('#results').html(html);
            $('#btnEdit').css({ "display": 'block' });
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}
function Update() {
    var content = $('#content').val();
    if (content == "") { 
        return false;
    }
    var data = new FormData();
    data.append("content", content);
    $.ajax({
        url: "/Home/GetContenFromXML",
        type: "POST",
        data: data,
        contentType: false,
        processData: false,
        success: function (result) {
            $('#myModal').modal('hide');
            console.log(result)
            var html = '',
                html = html + result.result.value
            $('#results').html(html);
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}
function loadContenReport() {
    $.ajax({
        url: '/Home/GetContent',
        type: 'get',
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            
            $('#content').val(result);
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}
//

