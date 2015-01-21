var LanceTrack;
(function (LanceTrack) {
    var app = angular.module("lance-track", ["ui.router", "ui.bootstrap", "ui.bootstrap.datetimepicker", "lance-track.shared", "lance-track.track-time", "lance-track.invoicing"]);
    app.config(function ($stateProvider, $urlRouterProvider) {
        var dates = new LanceTrack.Dates();
        $urlRouterProvider.otherwise("/track-time/" + dates.format(dates.now()) + "/my-time/");
    });
    app.run(["$rootScope", function ($rootScope) {
        $rootScope.urls = urls;
        $rootScope.modelOptions = { updateOn: "default blur", debounce: { "default": 1000, "blur": 0 } };
    }]);
})(LanceTrack || (LanceTrack = {}));
var urls;
//# sourceMappingURL=app.js.map