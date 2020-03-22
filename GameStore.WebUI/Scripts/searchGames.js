
$.ui.autocomplete.prototype._renderItem = function (ul, item) {

    var term = this.term.trim();

    var re = new RegExp("(" + term + ")", "gi");

    var t = item.label.trim().replace(re, "<span style='background:lime'>$1</span>");

    return $("<li></li>")
        .data("item.autocomplete", item)
        .append("<a style='text-decoration:none'>" + t + "<a/>")
        .appendTo(ul);
};

$(document).ready(function () {

    $("#keyword").autocomplete({
        source: function (request, response) {
            $.ajax({
                url: '/Game/AutocompleteSearch',
                type: 'GET',
                cache: false,
                data: request,
                dataType: 'json',
                success: function (data) {
                    response($.map(data,
                        function (item) {
                            return {
                                gameId: item.id,
                                label: item.name
                            };
                        }));
                }
            });
        },
        minLength: 3,
        select: function (event, ui) {
            var id = ui.item.gameId;
            window.location.href = "/Games/Info/" + id;

            return true;
        }
    });
});

function search() {

    var name = document.getElementById("keyword").value.trim();

    if (name.length >= 3) {
        window.location.href = "/Games/?name=" + name;
    } else {
        window.location.href = "/Games";
    }
}

function enterHandler(event) {

    if (event.keyCode === 13) {
        search();
    }
}