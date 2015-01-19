var LanceTrack;
(function (LanceTrack) {
    (function (Shared) {
        var app = angular.module("lance-track.shared", ["ui.bootstrap"]);

        app.filter("hours", LanceTrack.hoursFilter);
        app.filter("yesNo", LanceTrack.yesNoFilter);

        app.service("dates", LanceTrack.datesFactory);
        app.service("deferredFunction", LanceTrack.deferredFunctionServiceFactory);
    })(LanceTrack.Shared || (LanceTrack.Shared = {}));
    var Shared = LanceTrack.Shared;
})(LanceTrack || (LanceTrack = {}));
//# sourceMappingURL=module.js.map
