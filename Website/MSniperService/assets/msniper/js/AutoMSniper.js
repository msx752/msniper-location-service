/*        auto snipe          */
var autosnipeON = false;
var panelisvisible = false;
var snipelist = ["dragonite", "snorlax"];
var minIv = 100.0;
$(document).ready(function () {

    $("#autosnipe")
        .click(function myfunction() {

            //autosnipeON = false;
            panelisvisible = true;
            //console.log("auto snipe opened");
            PokemonFillToList();
            //ChangeState("stopped");
            if (autosnipeON !== true)
                $('#autosnipe-stop').css("display", "none");
            //$('#autosnipe-start').css("display", "inline");
            $('autosnipe-iv').val(minIv);
        }
        );

    $(document).on('hide.bs.modal', '#autosnipe-panel', function () {
        //panelisvisible = false;
        //autosnipeON = false;
        ////console.log("auto snipe closed");
        //ChangeState("");
        //$('#autosnipe').text("Auto MSniper BETA");
    });

    $('#autosnipe-close').click(function () {
        $('#autosnipe-stop').trigger("click");

        autosnipeON = false;
    }
        );


    $('#autosnipe-start').click(function () {
        //console.log("auto snipe on");
        ChangeState("started");
        autosnipeON = true;
        snipelist.forEach(function (item) {
            var lengitgdf = $('.pokemon-card[data-status="' + item + '"]');
            if (lengitgdf.length > 0) {
                $('.pokemon-card[data-status="' + item + '"]').each(function (key, value) {

                    var link1 = $(value).find('.linkmsniper').get(0).href;
                    var xiv = $(value).find('.pokemo-iv').text();
                    if (snipelist.indexOf($(value).attr("data-status")) !== -1) {
                        if (autosnipeON === true && parseFloat(xiv.replace("(", "").replace(")", "")) >= minIv) {
                            var x = window.open(link1, "window", 'height=100,width=100');
                            x.close();
                            $(value).focus();
                            $(value).remove();
                        }
                        //SnipePokemon(received.link1);
                    }
                });
            }


        });
    }
        );

    $('#autosnipe-stop').click(function () {
        //console.log("auto snipe off");
        ChangeState("stopped");
        autosnipeON = false;
    }
       );

    $('#autosnipe-add').click(function () {
        var pokemonname = $('#autosnipe-selectlist').val();
        PokemonAddToList(pokemonname);
        $('#autosnipe-selectlist').val('');
    }
       );

}
);
function ChangeState(sate) {
    var autostate = "Auto MSniper BETA  ";
    if (sate === "stopped") {
        autostate += "<span class=\"tag tag-danger\" id=\"autosnipe-state\">Stopped<\/span>";
        autosnipeON = false;
        $('#autosnipe-start').css("display", "inline");
        $('#autosnipe-stop').css("display", "none");
        $('#autosnipe').text("Auto MSniper [OFF]");
    } else {
        autostate += "<span class=\"tag tag-success\" id=\"autosnipe-state\">Running<\/span>";
        $('#autosnipe-start').css("display", "none");
        $('#autosnipe-stop').css("display", "inline");
        $('#autosnipe').text("Auto MSniper [ON]");
        var str = $('#autosnipe-iv').val();
        minIv = parseFloat(str);
        if (minIv < 0) {
            minIv = 0;
        } else if (minIv > 100) {
            minIv = 100.0;
        }
    }
    //console.log("changing state");
    $('#autosnipe-state').html(autostate);
}

function PokemonFillToList() {
    $('#autosnipe-selected-context').html("");

    snipelist.forEach(function (item) {
        if (item.toString() !== "all")
            PokemonAddToList(item);
    });
}

function PokemonAddToList(pokemonName) {
    if (pokemonName.toString() !== "all" && pokemonName.toString() !== "") {
        var content = $('#autosnipe-selected-context .' + pokemonName);
        if (content.length === 0) {
            //console.log(pokemonName + " added to list");
            var newdata = "<img id=\"pokemon-listed\" alt=\"" + pokemonName + "\" class=\"" + pokemonName +
                "\" data-toggle=\"tooltip\" title=\"" + pokemonName + "\"" +
                "style=\"width: 60px; height: 60px;\" src=\"" + poimgs[ponms.indexOf(pokemonName)] + "\"/>";

            $('#autosnipe-selected-context').append(newdata);

            $('#autosnipe-selected-context .' + pokemonName).click(
                function () {
                    PokemonRemoveFromList(this.alt);
                }
                );

            $('#autosnipe-selected-context .' + pokemonName).tooltip();
            if (snipelist.indexOf(pokemonName) === -1)
                snipelist.push(pokemonName);
        }
    } else {
        if (pokemonName.toString() === "all") {
            alert("if you need to catch all pokemon, you are crazy :)");
        } else {
            alert("select any pokemon from the list");
        }
    }
}

function PokemonRemoveFromList(pokemonName) {
    var content = $('#autosnipe-selected-context .' + pokemonName);
    if (content.length > 0) {
        $(content).tooltip("hide");
        content.remove();
    }
    if (snipelist.indexOf(pokemonName) !== -1)
        snipelist.splice(snipelist.indexOf(snipelist.indexOf(pokemonName), 1));
}

