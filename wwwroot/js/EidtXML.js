function Update() {

    var content = $('#content').val();

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
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });

    window.location = "/Home/LoadReportEitXML";
}
function loadContenReport() { 
    $.ajax({
        url: '/Home/GetContent',
        type: 'get',
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            console.log(result)
            $('#content').val(result);
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}