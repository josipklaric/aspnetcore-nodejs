var htmlPdf = require('html-pdf');

module.exports = function (result, options, data) {
    htmlPdf.create(data.html, options).toStream(function (error, stream) {
        if (error) {
            result(error);
        }
        return stream.pipe(result.stream);
    });
}