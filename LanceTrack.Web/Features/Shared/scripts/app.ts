module LanceTrack {
    var app = angular.module("lance-track", ["ui.router"]);

    app.config(($stateProvider: ng.ui.IStateProvider, $urlRouterProvider: ng.ui.IUrlRouterProvider) => {
        $urlRouterProvider.otherwise("/track-time");

        $stateProvider
            .state("track-time", {
                url: "/track-time",
                templateUrl: urls.templates.trackTime
            });
    });
}