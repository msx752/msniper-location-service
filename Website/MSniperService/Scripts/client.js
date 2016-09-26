var signalr;
$(document).ready(function () {
    signalr = $.connection.msniperHub;

    signalr.client.ImListener = function (obj) {
        console.log(obj);
    };

    signalr.client.ServerInfo = function (feeders, hunters) {
        var lifound = findLi("feeders");
        if (lifound.length > 0) {
            var spanfound1 = lifound.find("span").get(0);
            spanfound1.innerText = feeders;
        }
        lifound = findLi("hunters");
        if (lifound.length > 0) {
            var spanfound2 = lifound.find("span").get(0);
            spanfound2.innerText = hunters;
        }
    };

    signalr.client.SendRate = function (obj) {
        //top 5 snipped pokemons
        console.log(obj);
    };

    signalr.client.SendRareList = function (obj) {
        //right menu items
        Rarelist = obj;
    };

    signalr.client.NewPokemons = function (obj) {
        for (var i = 0; i < obj.length; i++) {
            InsertJsonToPage(obj[i]);
        }
    };
    setInterval(function () {
        signalr.server.LPing();
    }, 15001);

    $.connection.hub.start().done(function () {
        signalr.server.SendImListener("ImListener");
    });

});
// This optional function html-encodes messages for display in the page.
function htmlEncode(value) {
    var encodedValue = $('<div />').text(value).html();
    return encodedValue;
}