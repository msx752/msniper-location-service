
var identity;
var Rarelist =
[
"dragonite", "snorlax", "pikachu", "charmeleon", "vaporeon", "lapras", "gyarados",
"dragonair", "charizard", "blastoise", "magikarp", "dratini", "growlithe"
];
$(document)
    .ready(function () {
        $('.btn-filter')
            .on('click',
                function () {
                    var $target = $('.searchpkmn').val();
                    //console.log($target);
                    setFilter($target);
                });
        $('.mainpage')
           .on('click',
               function () {
                   setFilter("");
               });

        //fast snipping
        //$('.copier')
        //    .on('click',
        //        function () {
        //            var msnipe = $(this).find(".linkmsniper");
        //            var runurl = "http://" + (location.host + "/To#" + msnipe.get(0).href.substr(10));

        //            prompt("Share the msniper code", runurl);
        //            //console.log(runurl);
        //        });



    });

$(document)
    .ready(function setTimer() {


        $(".pokemon-card").each(function myfunction() {
            selectCard($(this));
            InsertToSideBar($(this).attr("data-status"));
        }
        );


    });
function selectCard(card) {
    var tmr = card.find("#tilltime");
    var expiretime = tmr.attr("expiration");
    var firstValue = parseInt(expiretime);


    tmr.countdown(new Date(firstValue), setCardTime);
};

function setCardTime(event) {
    var data = event.strftime('%M:%S');
    //console.log(data);
    if (data === "00:00") {
        //console.log(data);
        $(event.target.parentNode.parentNode)
            .fadeTo(5000, 0.0, function () {
                //$('#totalspawns').text((parseInt($('#totalspawns').text()) - 1));
                //console.log($(this).attr("data-status"));
                findPokemonSideBar(findLi($(this).attr("data-status")), false);
                $(this).remove();
            });
    } else {
        $(this).html(data);
    }
};
function RemoveSelf() {
    signalr.server.send("Rate", $(event.target.parentNode.parentNode.parentNode).attr("data-status"));
    event.target.parentNode.parentNode.parentNode.remove();
}

function sideBarFilter() {
    //console.log(event.target.parentNode.id);
    $(window).scrollTop(0);
    setFilter(event.target.parentNode.id);
}


function findLi(name) {
    return ulfound.find("#" + name);
}

function InsertToSideBar(name) {
    var lifound = findLi(name);

    if (lifound.length > 0) {
        findPokemonSideBar(lifound, true);
    } else {
        createSideBarPokemon(ulfound, name);
    }
}

var ulfound = $('#filterlist');


function checkImportantPokemon(name) {
    var inx = Rarelist.indexOf(name);
    if (inx === -1) {
        return false;
    }
    return true;

}

function createSideBarPokemon(ulfound, name) {

    if (checkImportantPokemon(name)) {
        var strVar = "";
        strVar += "<li id=\"" + name + "\"  class=\"list-group-item\">";
        strVar += "<a><span class=\"text\">" + name + "<\/span><span id=\"" + name + "\" style=\"float: right;\" class=\"badge bg-info\">1<\/span><\/a>";
        strVar += "<\/li>";

        //console.log($(strVar));
        //console.log($(ulfound));
        $(ulfound).append($(strVar));
    }
}

function findPokemonSideBar(lifound, increase) {
    if (lifound.length > 0) {
        //var afound = lifound.find(".url");
        var spanfound = lifound.find("span").get(0);
        //console.log(spanfound);
        if (increase) {
            spanfound.innerText = (parseInt(spanfound.innerText) + 1);
        } else {
            spanfound.innerText = (parseInt(spanfound.innerText) - 1);
            if (spanfound.innerText === "0") {
                $(lifound).remove();
            }
        }
        //console.log(spanfound);
        //console.log(ulfound);
        //console.log(afound);
        //console.log(spanfound);
    }
    DecreasePokemonCount();
}

function IncreatePokemonCount() {
    var lifound = findLi("all");
    if (lifound.length > 0) {
        var spanfound = lifound.find("span").get(0);
        spanfound.innerText = (parseInt(spanfound.innerText) + 1);
    }
}
function DecreasePokemonCount() {
    var lifound = findLi("all");
    if (lifound.length > 0) {
        var spanfound = lifound.find("span").get(0);
        spanfound.innerText = (parseInt(spanfound.innerText) - 1);
    }
}

var currentFilter = "all";
function setFilter(target) {
    currentFilter = target;
    $('.pokemon-card').css('display', 'none').css('position', 'fixed');
    if (target !== '' && target !== 'all') {
        $('.pokemon-card[data-status="' + target + '"]').css('position', 'relative').fadeIn('slow');

    } else if (target === 'all' || target === '') {
        $('.pokemon-card').css('display', 'none').css('position', 'relative').fadeIn('slow');
    }


    var items = $('.pokemon-card:visible');
    items.sort(function (a, b) {

        var valueA = parseFloat($(a).find(".pokemo-iv").text().replace("(", "").replace(")", ""));
        var valueB = parseFloat($(b).find(".pokemo-iv").text().replace("(", "").replace(")", ""));
        //console.log(valueB);
        if (valueA > valueB) {
            return -1;
        }
        if (valueA < valueB) {
            return 1;
        }
        return 0;
    });

    //console.log(items.index);
    $('.pokemon-card:visible').remove();
    items.each(function myfunction() {
        selectCard($(this));
        $('#cards').append($(this));
    });
    //console.log(items);
}

setInterval(function () {
    var pos = $(window).scrollTop();

    $('#sidebar').css('top', (pos - 80) + 'px');
    //console.log(hih);

}, 10000);


function selectCard2(card) {
    var tmr = card.find("#tilltime");
    var expiretime = tmr.attr("expiration");

    var firstValue = parseInt(expiretime);
    //console.log(firstValue);
    tmr.countdown(new Date(firstValue), setCardTime2);
};

function setCardTime2(event) {
    var data = event.strftime('%M:%S');
    if (data === "00:00") {

        $(event.target.parentNode.parentNode)
            .fadeTo(5000, 0.0, function () {
                var pkname = $(this).find('#tilltime').attr("pokemonName");
                //console.log(pkname);
                findPokemonSideBar(findLi(pkname), false);

                $(this).addClass("deleted");
                $('#datatable-column-filter').DataTable().rows('.deleted').remove().draw();
            });
    } else {
        $(this).html(data);
    }
};


$('#datatable-column-filter').dataTable({
    "createdRow": function (row, data, dataIndex) {
        var tmr = $(row).find("#tilltime");
                var expiretime = tmr.attr("expiration");

                var firstValue = parseInt(expiretime);
                //console.log(firstValue);
                tmr.countdown(new Date(firstValue), setCardTime2);
    }
});

function InsertJsonToPage(received) {
    //console.log(received);
    var linkk1 = "msniper://" + received.PokemonName + "/" + received.EncounterId + "/" + received.SpawnPointId + "/" + received.Latitude + "," + received.Longitude + "/" + received.Iv;
    var linkk2 = "pokesniper2://" + received.PokemonName + "/" + received.Latitude + "," + received.Longitude;



    //console.log();


    $('#datatable-column-filter')
        .DataTable()
        .row.add([
            $('<img />',
            {
                id: received.PokemonName.toString().toLowerCase(),
                src: poimgs[ponms.indexOf(received.PokemonName.toString().toLowerCase())].toString(),
                alt: received.PokemonName.toString().toLowerCase()
            })
            .addClass("avatar pull-left")
            .get(0)
            .outerHTML +
            $('<a />', {})
            .addClass("pull-left")
            .append(received.PokemonName.toString())
            .get(0)
            .outerHTML,
            "<div class=\"progress\"><div class=\"progress-bar progress-bar-success\" data-transitiongoal=\"" +
            received.Iv.toString() +
            "\" aria-valuenow=\"" +
            received.Iv.toString() +
            "\" style=\"width: " +
            received.Iv.toString() +
            "%;\">" +
            received.Iv.toString() +
            "%</div></div>",
            $('<span/>').addClass('label label-default').append(received.Move1).get(0).outerHTML,
            $('<span/>').addClass('label label-default').append(received.Move2).get(0).outerHTML,
            $('<span/>').addClass('label label-default').append('UNDEFINED').get(0).outerHTML,
            $('<span/>',
            {
                id: "tilltime",
                pokemonName:received.PokemonName.toString().toLowerCase(),
                expiration: parseInt(received.Expiration)

            }).addClass('label label-danger').append("00:00").get(0).outerHTML,

            $('<a />', { href: linkk1 }).addClass('btn btn-primary btn-xs').append("MSniper").get(0).outerHTML,
            $('<a />', { href: linkk2 }).addClass('btn btn-default btn-xs').append("Pokesniper2").get(0).outerHTML
        ])
        .draw(false);
      InsertToSideBar(received.PokemonName.toString().toLowerCase());
    //<tr id="pokemon-snipe" pokemon="abra" expiration="1234125123">
    //                           <td>
    //                               <img src="http://msniper.com/pkmn1x/abra.png" alt="Avatar" class="avatar"> <a href="#">FAbra</a>
    //                           </td>
    //                           <td>
    //                               <div class="progress"><div class="progress-bar progress-bar-success" data-transitiongoal="95" aria-valuenow="95" style="width: 95%;">90%</div></div>
    //                           </td>
    //                           <td><span class="label label-default">ConfisuonFastFast</span></td>
    //                           <td><span class="label label-default">ConfisuonFastFast</span></td>

    //                           <td><span class="label label-default">UNDEFINED</span></td>
    //                           <td><span class="label label-danger">00:34</span></td>
    //                           <td>
    //                               <a class="btn btn-primary btn-xs">MSniper</a>
    //                           </td>
    //                           <td>
    //                               <a class="btn btn-default btn-xs">Pokesniper2</a>
    //                           </td>
    //                       </tr>
    return;
    var pokemoncardNew = "";
    pokemoncardNew += " <div class=\"pokemon-card col-xs-12 col-lg-3\" data-status=\"" +
        received.PokemonName.toString().toLowerCase() +
        "\">";
    pokemoncardNew += "<div class=\"left-box col-xs-5 col-lg-3\">";
    pokemoncardNew += " <img src=\"" + poimgs[ponms.indexOf(received.PokemonName.toString().toLowerCase())] + "\" class=\"media-photo\" alt=\"\">";
    pokemoncardNew += " <span class=\"pokemo-iv\">";
    pokemoncardNew += "  (" + received.Iv.toString() + ")";
    pokemoncardNew += " <\/span>";
    pokemoncardNew += " <span class=\"pokemon-expire pull-left\" id=\"tilltime\" expiration=\"" +
        parseInt(received.Expiration) +
        "\">";
    pokemoncardNew += "  Loading.";
    pokemoncardNew += " <\/span>";
    pokemoncardNew += " <a href=\"" +
        linkk1 +
        "\" class=\"linkmsniper\" style=\"display: none; position: absolute\"><\/a>";
    pokemoncardNew += "<\/div>";
    pokemoncardNew += "<div class=\"right-box pokemon-info col-xs-7 col-lg-7\">";
    pokemoncardNew += " <h4 class=\"title\">";
    pokemoncardNew += "  (" + received.PokemonName.toString() + ")";
    pokemoncardNew += " <\/h4>";
    pokemoncardNew += " <span class=\"move\"><b>M1:<\/b>" + received.Move1 + "<\/span>";
    pokemoncardNew += " <br\/>";
    pokemoncardNew += " <span class=\"move\"><b>M2:<\/b>" + received.Move2 + "<\/span>";
    pokemoncardNew += "<\/div>";
    pokemoncardNew += "<div class=\"col-lg-12\">";
    pokemoncardNew += " <div class=\"pull-right\">";
    pokemoncardNew += "  <a   href=\"" + linkk1 + "\" onclick=\"RemoveSelf();\" class=\"btn btn-secondary btn-sm pull-left\">msniper<\/a>";
    pokemoncardNew += "  <a  href=\"" +
        linkk2 +
        "\" onclick=\"RemoveSelf();\" class=\"btn btn-secondary btn-sm pull-right\">pokesniper2<\/a>";
    pokemoncardNew += " <\/div>";
    pokemoncardNew += "<\/div>";
    pokemoncardNew += "<\/div>";


    var checker = $('.linkmsniper[href="' + received.link1 + '"]');
    if (checker.length === 0) {

        $("#cards").append($(pokemoncardNew));

        IncreatePokemonCount();
        var expiration = parseInt(received.Expiration);
        var snc = $('.pokemon-expire[expiration="' + expiration + '"]');

        //console.log($($(snc).get(0).parentNode.parentNode));
        selectCard2($($(snc).get(0).parentNode.parentNode));

        //console.log(currentFilter);
        $($(snc).get(0).parentNode.parentNode).css('display', 'none').css('position', 'fixed');
        if (currentFilter === '' || currentFilter === 'all') {
            $($(snc).get(0).parentNode.parentNode).css('position', 'relative').fadeIn('slow');
        } else {
            if (currentFilter === received.PokemonName.toString().toLowerCase()) {
                $($(snc).get(0).parentNode.parentNode).css('position', 'relative').fadeIn('slow');
            }
        }
    } else {
        //console.log("x");
    }
}