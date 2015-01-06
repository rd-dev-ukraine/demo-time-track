module LanceTrack {
    var app = angular.module("lance-track", ["ui.router", "lance-track.track-time"]);

    app.config(($stateProvider: ng.ui.IStateProvider, $urlRouterProvider: ng.ui.IUrlRouterProvider) => {
        $urlRouterProvider.otherwise("/track-time");

        $stateProvider
            .state("track-time", {
                url: "/track-time",
                templateUrl: urls.templates.trackTime,
                controller: "trackTimeController"
            });
    });

    export var DateFormat = "YYYY-MM-DD";
}