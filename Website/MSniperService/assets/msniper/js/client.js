var signalr;
var tryingToReconnect = false;

$(document).ready(function () {
    signalr = $.connection.msniperHub;

    $.connection.hub.reconnecting(function () {
        tryingToReconnect = true;
        alert("connection lost, trying to reconnect");
        reConnect();
    });

    $.connection.hub.reconnected(function () {
        tryingToReconnect = false;
        alert("connected to server");
    });

    $.connection.hub.disconnected(function () {
        if (tryingToReconnect) {
            alert("connection lost, auto reconnect activated");
            tryingToReconnect = false;
            reConnect();
        }
    });

    signalr.exceptionHandler = function (error) {
        console.log('Error: ' + error);
        alert('Error: ' + error);
    };

    signalr.client.serverInfo = function (feeders, hunters) {
        var spanfound1 = ulfound.find("span#totalfeeder").get(0);
        spanfound1.innerText = feeders;

        var spanfound2 = ulfound.find("span#totalvisitor").get(0);
        spanfound2.innerText = hunters;
    };

    signalr.client.rate = function (data) {
        //top 6 snipped pokemons
        console.log(data);
        UpdateTop6Pokemons(data);
    };

    signalr.client.rareList = function (data) {
        //right menu items (significant pokemons)
        Rarelist = data;
    };

    signalr.client.newPokemons = function (obj) {
        //console.log(obj);
        for (var i = 0; i < obj.length; i++) {
            InsertJsonToPage(obj[i]);
        }
        $('#allPokemons').text($('#datatable-column-filter').DataTable().page.info().recordsTotal);
        //
    };

    $.connection.hub.start().done(function () {
        signalr.server.recvIdentity().done(function (obj) {
            console.log(obj);//connection established
        });
    });

    function reConnect() {
        setTimeout(function () {
            $.connection.hub.start().done(function () {
                signalr.server.recvIdentity().done(function (obj) {
                    console.log(obj);//connection established
                });
            });
        }, 30000); // Restart connection after 30 seconds.
    }

});
// This optional function html-encodes messages for display in the page.
function htmlEncode(value) {
    var encodedValue = $('<div />').text(value).html();
    return encodedValue;
}