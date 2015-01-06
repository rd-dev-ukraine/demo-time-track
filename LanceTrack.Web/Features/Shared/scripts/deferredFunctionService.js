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
                code.isLoading = true;
                return fn().finally(function () {
                    return code.isLoading = false;
                });
            };

            code.isLoading = false;

            return code;
        };
        return DeferredFunctionService;
    })();
    LanceTrack.DeferredFunctionService = DeferredFunctionService;
})(LanceTrack || (LanceTrack = {}));
//# sourceMappingURL=deferredFunctionService.js.map
