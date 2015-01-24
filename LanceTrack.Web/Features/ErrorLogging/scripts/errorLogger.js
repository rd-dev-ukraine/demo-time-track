var LanceTrack;
(function (LanceTrack) {
    var ErrorLogging;
    (function (ErrorLogging) {
        function onError(error, fileUrl, lineNumber, colNumber, message) {
            $.post(urls.data.logJavascriptError, {
                message: error,
                scriptFileUrl: fileUrl,
                lineNumber: lineNumber,
                columnNumber: colNumber
            });
        }
        ;
        window["onerror"] = onError;
    })(ErrorLogging = LanceTrack.ErrorLogging || (LanceTrack.ErrorLogging = {}));
})(LanceTrack || (LanceTrack = {}));
//# sourceMappingURL=errorLogger.js.map