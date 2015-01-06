var LanceTrack;
(function (LanceTrack) {
    var app = angular.module("lance-track", ["ui.router", "lance-track.track-time"]);

    app.config(function ($stateProvider, $urlRouterProvider) {
        $urlRouterProvider.otherwise("/track-time");

        $stateProvider.state("track-time", {
            url: "/track-time",
            templateUrl: urls.templates.trackTime,
            controller: "trackTimeController"
        });
    });

    LanceTrack.DateFormat = "YYYY-MM-DD";
})(LanceTrack || (LanceTrack = {}));
//# sourceMappingURL=app.js.map
