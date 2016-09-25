///statistics
(function (i, s, o, g, r, a, m) {
    i['GoogleAnalyticsObject'] = r; i[r] = i[r] ||
        function () { (i[r].q = i[r].q || []).push(arguments); }, i[r].l = 1 * new Date();
    a = s.createElement(o), m = s.getElementsByTagName(o)[0]; a.async = 1; a.src = g;
    m.parentNode.insertBefore(a, m);
})(window, document, 'script', 'https://www.google-analytics.com/analytics.js', 'ga');

function gaa() {
    gaa(location.pathname + location.search + location.hash);
}

function gaa(urlparm) {
    ga('create', 'UA-74939359-4', { 'allowAnchor': true });
    ga('require', 'linkid');
    ga('send', 'pageview', { 'page': urlparm });
}

$(document).ready(function () {
    gaa();
    $('a')
        .click(function () {
            var x = $(this.parentNode.parentNode.parentNode).attr("data-status");
            //console.log(x);
            if (x !== "undefined") {
                gaa(location.pathname + "USED-" + event.target.innerText + "$" + x);
            }
        });

});