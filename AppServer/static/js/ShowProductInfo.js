$(document).ready(function () {
    $('#buttonGetProductInfo').click(function () {
        $.post('/gobo', { text: $('#sendText').val()}, function (data) {
            $('#response').text('You have input: ' + data);
        });
    });
    $('#buttonGetAdditiveInfo').click(function () {
        $.post('/gete', { text: $('#sendText').val()}, function (data) {
            $('#response').text('You have input: ' + data);
        });
    });
});