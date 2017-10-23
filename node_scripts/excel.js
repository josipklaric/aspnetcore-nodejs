var fs = require('fs');
var path = require('path');
var json2xls = require('json2xls');

module.exports = function (callback, data) {

    var json = JSON.parse(data);

    var xls = json2xls(json);

    var filePath = path.join('./files', 'users.xlsx');

    fs.writeFileSync(filePath, xls, 'binary');

    callback(null, filePath);
};