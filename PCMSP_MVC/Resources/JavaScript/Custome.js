function change() {


    $.ajax({
        url: "/Media/Search?st="+$("#sText").val(),
        type: "get",
        success: function (response) {
            $("#searchResult").html(response);
              
            alert(response);
        }
    })
    return false;
}