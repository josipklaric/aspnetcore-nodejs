var webshot = require('webshot');
var fs = require('fs');
var path = require('path');

module.exports = (callback, site, options) => {
    console.log('> webshot called -> site: ' + site);
    var filePath = './wwwroot/images/screenshot.png';

    webshot(site, filePath, options, function (err) {
        console.log('Error: ' + err);
    });
    
    callback(null, filePath);
}