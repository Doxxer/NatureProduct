$(document).ready(function () {
//    $('#buttonGetProductInfoStub').click(function () {
//        $.post('/gobo', { text: $('#sendText').val()}, function (data) {
//            $('#response').text(data);
//        });
//    });
    $('#buttonGetProductInfo').click(function () {
        $.post('/gobolive', { text: $('#sendText').val()}, function (data) {
            $('#response').text(data);
        });
    });
//    $('#buttonGetAdditiveInfo').click(function () {
//        $.post('/gete', { text: $('#sendText').val()}, function (data) {
//            $('#response').text(data);
//        });
//    });
});