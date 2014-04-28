$(document).ready(function () {
    $('#buttonSend').click(function () {
        $.post('/gobo', { text: $('#sendText').val()}, function (data) {
            $('#response').text('You have input: ' + data);
        });
    });
});