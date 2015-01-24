module LanceTrack {
    export module ErrorLogging {

        function onError(error: string, fileUrl: string, lineNumber: number, colNumber: number, message: string) {

            $.post(urls.data.logJavascriptError, <Api.LogJavascriptErrorParams>{
                message: error,
                scriptFileUrl: fileUrl,
                lineNumber: lineNumber,
                columnNumber: colNumber
            });

        };

        window["onerror"] = <ErrorEventHandler><any>onError;
    }
}