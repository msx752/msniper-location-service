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

    signalr.client.ImListener = function (data) {
        console.log(data);
    };

    signalr.client.serverInfo = function (feeders, hunters) {
        var lifound = findLi("feeders");
        if (lifound.length > 0) {
            var spanfound1 = lifound.find("span#totalfeeder").get(0);
            spanfound1.innerText = feeders;
        }
        lifound = findLi("hunters");
        if (lifound.length > 0) {
            var spanfound2 = lifound.find("span#totalvisitor").get(0);
            spanfound2.innerText = hunters;
        }
    };

    signalr.client.rate = function (data) {
        //top 6 snipped pokemons
        UpdateTop6Pokemons(data);
    };

    signalr.client.rareList = function (data) {
        //right menu items (significant pokemons)
        Rarelist = data;
    };

    //function initDataTable() {
    //    var dtt = $('#datatable-column-filter tbody#pokemon-content');
    //    console.log(dtt);
        //dtt.dataTable({
        //    "rowCallback": function (row, data, index) {
        //        console.log(row);

        //    }
        //});

        //dtt.DataTable({
        //    rowCallback: function (nRow) {

        //        console.log(nRow)
        //        //var tmr = $(nRow).find("#tilltime");
        //        //var expiretime = tmr.attr("expiration");

        //        //var firstValue = parseInt(expiretime);
        //        ////console.log(firstValue);
        //        //tmr.countdown(new Date(firstValue), setCardTime2);

        //        /* This is your code */
        //        // $(nRow).find('[countdown]').each(function () {
        //        //    var $this = $(this),
        //        //      finalDate = $(this).data('countdown');
        //        //    $this.countdown(finalDate, function (event) {
        //        //        $this.html(event.strftime('%D days %H:%M:%S'));
        //        //    });
        //        //}).on('finish.countdown', function (event) {
        //        //    $(this).addClass("label label-sm label-danger");
        //        //    $(this).html('This offer has expired!');
        //        //});
        //    }
        //});

    //};

    signalr.client.newPokemons = function (obj) {
        //console.log(obj);
        for (var i = 0; i < obj.length; i++) {
            InsertJsonToPage(obj[i]);
        }
        //initDataTable();
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