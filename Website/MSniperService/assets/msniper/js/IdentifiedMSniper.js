
var identifiedNecrobot = false;
var IdentityList = [];
$(document)
    .ready(function () {

        $(window).on('resize', function () {
            var win = $(this); //this = window
            //if (win.height() >= 820) {


            //}

            var table = $('#datatable-column-filter').DataTable();

            if (win.width() >= 1200) {
                table.column(2).visible(true);
                table.column(3).visible(true);
                table.column(4).visible(true);
                table.column(7).visible(true);
            }
            else if (win.width() >= 992) {
                table.column(2).visible(true);
                table.column(3).visible(true);
                table.column(4).visible(true);
                table.column(7).visible(true);
            }
            else if (win.width() >= 768) {
                table.column(2).visible(true);
                table.column(3).visible(true);
                table.column(4).visible(true);
                table.column(7).visible(false);
            }
            else if (win.width() < 768) {
                table.column(2).visible(false);
                table.column(3).visible(false);
                table.column(4).visible(false);
                table.column(7).visible(false);
            }

        });




        $("#idmsniper")
       .click(function myfunction() {
           //console.log("identified necrobbot is opened");

           if (identifiedNecrobot !== true)
               $('#idmsniper-stop').css("display", "none");
           //$('#idmsniper-start').css("display", "inline");
       }
       );

        $(document).on('click', '#idmsniper-guid', function () {
            //console.log(this.parentNode.innerText);
            console.log(IdentityList);
            IdentityList.pop(this.parentNode.innerText + 1);
            console.log(IdentityList);
            this.parentNode.remove();
        });


        $('#idmsniper-add').click(function () {
            var _identity = $('#idmsniper-selectlist').val();
            if (_identity.length != 36) {

                alert("identity lenght must be 36");
                return;
            }
            console.log(_identity);
            AddIdentityToList(_identity);

            $('#idmsniper-selectlist').val('');
        }
     );


        $('#idmsniper-start').click(function () {
            ChangeState2("started");
            identifiedNecrobot = true;

        });
        $('#idmsniper-stop').click(function () {
            ChangeState2("stopped");
            identifiedNecrobot = false;
        });

    });

function AddIdentityToList(identity) {

    var newdata = "<p ><button id=\"idmsniper-guid\"  style=\"color:red\" class=\"icon ion-close-round danger\"></button> " + identity + " <br/></p>";

    $('#idmsniper-selected-context').append(newdata);
    IdentityList.push(identity);

}

function ChangeState2(sate) {
    var autostate = "Identified Necrobot BETA ";
    if (sate === "stopped") {
        autostate += "<span class=\"label label-danger\" id=\"idmsniper-state\">Disable<\/span>";
        identifiedNecrobot = false;
        $('#idmsniper-start').css("display", "inline");
        $('#idmsniper-stop').css("display", "none");
        $('#idmsniper').text("Identified Necrobot [OFF]");
        $('#idmsniper').removeClass('btn-success');
        $('#idmsniper').addClass('btn-danger');
    } else {
        autostate += "<span class=\"label label-success\" id=\"idmsniper-state\">Enable<\/span>";
        $('#idmsniper-start').css("display", "none");
        $('#idmsniper-stop').css("display", "inline");
        $('#idmsniper').text("Identified Necrobot [ON]");
        $('#idmsniper').removeClass('btn-danger');
        $('#idmsniper').addClass('btn-success');
    }
    //console.log("changing state");
    $('#idmsniper-state').html(autostate);
}