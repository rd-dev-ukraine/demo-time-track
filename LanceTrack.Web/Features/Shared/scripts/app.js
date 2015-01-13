var LanceTrack;
(function (LanceTrack) {
    var app = angular.module("lance-track", ["ui.router", "lance-track.shared", "lance-track.track-time"]);

    app.config(function ($stateProvider, $urlRouterProvider) {
        $urlRouterProvider.otherwise("/track-time/");

        $stateProvider.state("track-time", {
            url: "/track-time/:at",
            templateUrl: "",
            controller: "trackTimeController"
        });
    });
})(LanceTrack || (LanceTrack = {}));
//# sourceMappingURL=app.js.map
