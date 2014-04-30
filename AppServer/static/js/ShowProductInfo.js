$(document).ready(function () {
    $('#buttonGetProductInfo').click(function () {
        $.post('/gobolive', { text: $('#sendText').val()}, printAdditives)

    });

    var printAdditives =
        function (data) {
            var ul = $('<ul>');
            switch (data.name) {
                case '1':
                    ul.append('<span>Ошибка ввода или сервер недоступен. Так или иначе, все очень плохо</span></p><img src=\'/static/img/duck.jpg\'>');
                    break;
                case '2':
                    ul.append('<span>Это весовой товар</span>');
                    break;
                case '3':
                    ul.append('<span>Штрихкод не найден</span>');
                    break;
                default:
                    ul.append('<li>Название товара: ' + data.name + '</li>');
                    data.additives.sort(function (a, b) {
                        return a.severity - b.severity;
                    });
                    for (var i = 0; i < data.additives.length; i++) {
                        var additive = data.additives[i];
                        var s = "<li><b>E" + additive.id + ': ' + additive.names.join(', ') + '. ' + '</b>'
                            + '</p>'
                            + 'Уровень опасности: ' + additive.severity
                            + '</p>';
                        if (additive.comment != '')
                            s += 'Комментарий: ' + additive.comment;
                        s += '</li>';
                        ul.append(s);
                    }
                    break;
            }
            $('#response').html(ul.html());
        }
});