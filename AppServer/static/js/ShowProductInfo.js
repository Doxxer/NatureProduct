$(document).ready(function () {
    $('#buttonGetProductInfo').click(function () {
        $.post('/gobolive', { text: $('#sendText').val()}, printAdditives)

    });

    var printAdditives =
        function (data) {
            var ul = $('<ul>');
            ul.append('<li>Название товара: ' + data.name + '</li>');
            data.additives.sort(function (a, b) {
                return a.severity - b.severity;
            });
            for (var i = 0; i < data.additives.length; i++) {
                var additive = data.additives[i];
                var s = '<li>'
                    + '<b>E' + additive.id + ': ' + additive.names.join(', ') + '. ' + '</b>'
                    + '</p>'
                    + 'Уровень опасности: ' + additive.severity
                    + '</p>';
                if (additive.comment != '')
                    s += ''
                        + 'Комментарий: ' + additive.comment;

                s += '</li>';
                ul.append(s);
            }

            $('#response').html(ul.html());
        }
});