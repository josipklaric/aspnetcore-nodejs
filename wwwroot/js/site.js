// Write your JavaScript code.

var screenShotIntervalHandle;

function takeScreenShot() {

    var mobileVersion = $('#mobileVersion').is(':checked');
    var url = $('#siteUrl').val();
    console.log('>> takeScreenShot -> url: ' + url + ' mobile: ' + mobileVersion);

    $('#screenshot').empty();

    $('#screenshot').empty().append('<img src="images/loading.gif" alt="screenshot">');

    $.ajax({
        type: "POST",
        url: "/Home/TakeScreenShot?url=" + url + "&mobile=" + mobileVersion,
        data: null, //JSON.stringify({ 'url': url, 'mobile': mobileVersion }),
        success: function (result) {
            console.log('Success!');

            screenShotIntervalHandle = setInterval(loadImage, 1000);

            //setTimeout(function () {
            //    $('#screenshot').empty().append('<div class="well"><img src="images/screenshot.png" alt="screenshot"></div>');
            //}, 5000);
        },
        error: function (req, status, error) {
            console.log('error: ' + status);
        }
    });
}

function loadImage() {

    var imgPath = "images/screenshot.png";
    console.log(">> loadImage");
    $('<img src="' + imgPath + '?' + new Date().getTime() + '">').load(function () {
        $('#screenshot').empty();
        $(this).appendTo('#screenshot');
        clearInterval(screenShotIntervalHandle);
    });
}

$(function () {
    $('#takeScreenShot').click(takeScreenShot);
    //console.log('takeScreenShot button: ' + $('#takeScreenShot').length);
});