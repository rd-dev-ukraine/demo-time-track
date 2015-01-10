var LanceTrack;
(function (LanceTrack) {
    function deferredFunctionServiceFactory() {
        return new DeferredFunctionService();
    }
    LanceTrack.deferredFunctionServiceFactory = deferredFunctionServiceFactory;

    var DeferredFunctionService = (function () {
        function DeferredFunctionService() {
        }
        DeferredFunctionService.prototype.decorate = function (fn) {
            var code = function () {
                code.reset();

                code.isLoading = true;
                return fn().then(function (v) {
                    return code.value = v;
                }).catch(function (err) {
                    code.error = err;
                    code.isError = true;
                }).finally(function () {
                    return code.isLoading = false;
                });
            };

            code.reset = function () {
                code.value = null;
                code.error = null;
                code.isError = null;
                code.isLoading = false;
            };
            code.reset();

            return code;
        };
        return DeferredFunctionService;
    })();
    LanceTrack.DeferredFunctionService = DeferredFunctionService;
})(LanceTrack || (LanceTrack = {}));
//# sourceMappingURL=deferredFunctionService.js.map
