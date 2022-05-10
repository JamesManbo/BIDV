var AjaxService = {};
AjaxService.GET = function (startUrl, objectJson, fn) {
    $.ajax({
        url: startUrl,
        type: "GET",
        //dataType: 'json',
        data: objectJson,
        success: function (resulst) {
            // 
            fn(resulst);
        },
    });
}
AjaxService.POST = function (startUrl, objectJson, fn) {
    $.ajax({
        url: startUrl,
        type: 'POST',
        //data: JSON.stringify(objectJson),
        data: objectJson,
        dataType: 'json',
        error: function (xhr, result) {
            fn(result);
            //console.log('Message: ' + xhr.statusText);
        },
        success: function (result) {
            fn(result);
        },
    });
}
AjaxService.POSTFORM = function (startUrl, fn) {
    $("form").submit(function (e) {
        e.preventDefault();
        var postData = $(this).serializeArray();
        AjaxService.POST(startUrl, postData, function (data) {
            fn(data);
        });
    });
}
