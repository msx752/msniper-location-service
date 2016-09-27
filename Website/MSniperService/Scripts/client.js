﻿var signalr;
$(document).ready(function () {
    signalr = $.connection.msniperHub;

    signalr.client.ImListener = function (data) {
        console.log(data);
    };

    signalr.client.serverInfo = function (feeders, hunters) {
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

    signalr.client.rate = function (data) {
        //top 5 snipped pokemons
        console.log(data);
    };

    signalr.client.rareList = function (data) {
        //right menu items
        Rarelist = data;
    };

    signalr.client.newPokemons = function (obj) {
        //console.log(obj);
        for (var i = 0; i < obj.length; i++) {
            InsertJsonToPage(obj[i]);
        }
    };
    setInterval(function () {
        signalr.server.lPing()
            .done(function () {

        });
    }, 15001);

    $.connection.hub.start().done(function () {
        signalr.server.recvIdentity().done(function (obj) {
            console.log(obj);//connection established
        });
    });

});
// This optional function html-encodes messages for display in the page.
function htmlEncode(value) {
    var encodedValue = $('<div />').text(value).html();
    return encodedValue;
}