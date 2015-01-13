var LanceTrack;
(function (LanceTrack) {
    var app = angular.module("lance-track", ["ui.router", "lance-track.shared", "lance-track.track-time"]);

    app.config(function ($stateProvider, $urlRouterProvider) {
        $urlRouterProvider.otherwise("/my-time/");

        $stateProvider.state("my-time", {
            url: "/my-time/:at",
            templateUrl: urls.templates.trackMyTime,
            controller: "trackMyTimeController"
        });
    });
})(LanceTrack || (LanceTrack = {}));

var urls;
//# sourceMappingURL=app.js.map
