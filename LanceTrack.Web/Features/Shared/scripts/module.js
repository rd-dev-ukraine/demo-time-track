var LanceTrack;
(function (LanceTrack) {
    (function (Shared) {
        var app = angular.module("lance-track.shared", []);

        app.service("dates", LanceTrack.datesFactory);
        app.service("deferredFunction", LanceTrack.deferredFunctionServiceFactory);
    })(LanceTrack.Shared || (LanceTrack.Shared = {}));
    var Shared = LanceTrack.Shared;
})(LanceTrack || (LanceTrack = {}));
//# sourceMappingURL=module.js.map
