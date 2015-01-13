module LanceTrack {
    var app = angular.module("lance-track", ["ui.router", "lance-track.shared", "lance-track.track-time"]);
    

    app.config(($stateProvider: ng.ui.IStateProvider, $urlRouterProvider: ng.ui.IUrlRouterProvider) => {
        $urlRouterProvider.otherwise("/track-time/");

        $stateProvider
            .state("track-time", {
                url: "/track-time/:at",
                templateUrl: urls.templates.trackTime, 
                controller: "trackTimeController"
            });
    });
}

var urls: Api.Urls;