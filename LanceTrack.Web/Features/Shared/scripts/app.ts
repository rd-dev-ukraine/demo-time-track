module LanceTrack {
    var app = angular.module("lance-track", ["ui.router", "lance-track.shared", "lance-track.track-time"]);
    

    app.config(($stateProvider: ng.ui.IStateProvider, $urlRouterProvider: ng.ui.IUrlRouterProvider) => {
        $urlRouterProvider.otherwise("/my-time/");

        $stateProvider
            .state("my-time", {
                url: "/my-time/:at",
                templateUrl: urls.templates.trackMyTime, 
                controller: "trackMyTimeController"
            });
    });
}

var urls: Api.Urls;