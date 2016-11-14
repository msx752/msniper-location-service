
var identity;
var Rarelist = [];
var ulfound = $('#filterlist');

$(document).ready(function () {
    $(document).on('click', '[data-filter-item]', function () {
        var nameWrapper = $(this).data('name-wrapper') || '.text';
        var pokemonName = $(this).children(nameWrapper).text();
        $("tr.row-filter th:first-child input.form-control.input-sm").val(pokemonName.toLowerCase()).change();
    });

    $(document).on('click', '#allPokemons', function () {
       

    });

    $(document).on('click', 'tbody#pokemon-content tr td a#link', function () {
        $(this.parentNode.parentNode).addClass("deleted");
        $('#datatable-column-filter').DataTable().rows('.deleted').remove().draw();

        signalr.server.rate($(this).attr("pName")).done(function (obj) {

        });
    });
});

setInterval(function () {
    $('#datatable-column-filter tbody tr').each(function () {
        SetTimer(this);
    });
}, 1000);

function SetTimer(row) {
    var tmr = $(row).find("#tilltime");
    var expiretime = tmr.attr("expiration");

    tmr.countdown(parseInt(expiretime), { elapse: true })
        .on('update.countdown',
            function (event) {
                if (event.elapsed) {
                    $(event.target.parentNode.parentNode).fadeTo(3000, 0.0, function () {
                        var pkname = $(this).find('#tilltime').attr("pokemonName");
                        //console.log(pkname);
                        $('#allPokemons').text($('#datatable-column-filter').DataTable().page.info().recordsTotal);
                        //findSideBarPokemon(findLi(pkname), false);
                        $(this).addClass("deleted");
                        $('#datatable-column-filter').DataTable().rows('.deleted').remove().draw();
                    });
                } else {
                    var data = event.strftime('%M:%S');
                    $(this).html(data);
                }
            });
}

function findLi(name) {
    return ulfound.find("span.badge." + name);
}


function InsertToSideBar(name) {
    var lifound = findLi(name);

    if (lifound.length > 0) {
        //findSideBarPokemon(lifound, true);
    } else {
        createSideBarPokemon(ulfound, name);
    }
}

function checkImportantPokemon(name) {
    var inx = Rarelist.indexOf(name);
    if (inx === -1) {
        return false;
    }
    return true;

}

function UpdateTop6Pokemons(data) {
    $('#top6pokemons').html("");
    for (var i = 0; i < data.length; i++) {
        var strVar = "";
        strVar += "<div class=\"col-xs-6 col-sm-2 text-center\"><div class=\"quick-info horizontal\">";
        strVar += "<img src=\"" + poimgs[ponms.indexOf(data[i].PokemonName.toString().toLowerCase())].toString() + "\" width=\"60\" alt=\"" + data[i].PokemonName + "\" class=\"media-object img-circle pull-left\">";
        strVar += "<p>" + (i + 1) + ". <span>" + data[i].PokemonName + "<\/span><\/p>";
        strVar += "<\/div><\/div>";
        $('#top6pokemons').append(strVar);
    }
}

function createSideBarPokemon(ulfound, name) {

    if (checkImportantPokemon(name)) {
        var strVar = "<a data-filter-item data-name-wrapper=\".text\" class=\"list-group-item\"><span class=\"text\">" + name + "<\/span><span style=\"float: right;\" class=\"badge " + name + "\">-<\/span><\/a>";

        $(ulfound).append($(strVar));
    }
}

function findSideBarPokemon(lifound, increase) {
    if (lifound.length > 0) {
        var spanfound = lifound.get(0);
        //console.log(spanfound);
        if (increase) {
            spanfound.innerText = (parseInt(spanfound.innerText) + 1);
        } else {
            if (parseInt(spanfound.innerText) === 0) {
                spanfound.parentNode.remove();
            } else {
                spanfound.innerText = (parseInt(spanfound.innerText) - 1);
            }
        }
    }
}

function InsertJsonToPage(received) {
    //console.log(received);
    var linkk1 = "msniper://" + received.PokemonName + "/" + received.EncounterId + "/" + received.SpawnPointId + "/" + received.Latitude + "," + received.Longitude + "/" + received.Iv;
    var linkk2 = "pokesniper2://" + received.PokemonName + "/" + received.Latitude + "," + received.Longitude;
    if (snipelist.indexOf(received.PokemonName.toString().toLowerCase()) !== -1) {
        //console.log(autosnipeON);
        //console.log(parseFloat(received.Iv));
        if (autosnipeON === true && parseFloat(received.Iv) >= minIv) {
            var x = window.open(linkk1, "window", 'height=100,width=100');
            x.close();
        }
    }

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
                pokemonName: received.PokemonName.toString().toLowerCase(),
                expiration: parseInt(received.Expiration)

            }).addClass('label label-danger').append("00:00").get(0).outerHTML,

            $('<a id="link" pname="' + received.PokemonName.toString().toLowerCase() + '" href="' + linkk1 + '" />', {}).addClass('btn btn-primary btn-xs').append("MSniper").get(0).outerHTML,
            $('<a id="link" pname="' + received.PokemonName.toString().toLowerCase() + '" href="' + linkk2 + '" />', {}).addClass('btn btn-default btn-xs').append("Pokesniper2").get(0).outerHTML
        ])
        .draw();
    InsertToSideBar(received.PokemonName.toString().toLowerCase());
}