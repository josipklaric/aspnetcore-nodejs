var webshot = require('webshot');
var fs = require('fs');
var path = require('path');

module.exports = (callback, site, mobile, options) => {

    var filePath = './wwwroot/images/screenshot.png';

    if (mobile) {
        webshot(site, filePath, options);
    }
    else {
        webshot(site, filePath, function(err) {
            
        });
    }
    
    callback(null, filePath);
}