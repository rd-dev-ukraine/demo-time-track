var LanceTrack;
(function (LanceTrack) {
    var Shared;
    (function (Shared) {
        var app = angular.module("lance-track.shared", ["ui.bootstrap"]);
        app.directive("uiStateActiveExt", LanceTrack.uiStateActiveExtDirective);
        app.filter("hours", LanceTrack.hoursFilter);
        app.filter("currency", LanceTrack.currencyFilter);
        app.filter("yesNo", LanceTrack.yesNoFilter);
        app.service("dates", LanceTrack.datesFactory);
        app.service("deferredFunction", LanceTrack.deferredFunctionServiceFactory);
    })(Shared = LanceTrack.Shared || (LanceTrack.Shared = {}));
})(LanceTrack || (LanceTrack = {}));
//# sourceMappingURL=module.js.map