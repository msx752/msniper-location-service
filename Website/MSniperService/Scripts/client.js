var signalr;
$(document).ready(function () {
    signalr = $.connection.msniperHub;

    signalr.client.ImListener = function (data) {
        console.log(data);
    };

    signalr.client.ServerInfo = function (feeders,hunters) {
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

    signalr.client.SendRate = function (data) {
        //top 5 snipped pokemons
        console.log(data);
    };

    signalr.client.SendRareList = function (data) {
        //right menu items
        Rarelist = data;
    };

    signalr.client.NewPokemons = function (data) {
        var obj = $.parseJSON(data);
        //console.log(obj);
        for (var i = 0; i < obj.data.length; i++) {
            InsertJsonToPage(obj.data[i]);
        }
    };
    setInterval(function () {
        signalr.server.send("LPing", null);
    }, 15001);

    $.connection.hub.start().done(function () {
        signalr.server.send("ImListener", null);
    });

});
// This optional function html-encodes messages for display in the page.
function htmlEncode(value) {
    var encodedValue = $('<div />').text(value).html();
    return encodedValue;
}