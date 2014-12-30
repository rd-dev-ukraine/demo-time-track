var LanceTrack;
(function (LanceTrack) {
    var app = angular.module("lance-track", ["ui.router"]);

    app.config(function ($stateProvider, $urlRouterProvider) {
        $urlRouterProvider.otherwise("/track-time");

        $stateProvider.state("track-time", {
            url: "/track-time",
            templateUrl: urls.templates.trackTime
        });
    });
})(LanceTrack || (LanceTrack = {}));
//# sourceMappingURL=app.js.map
