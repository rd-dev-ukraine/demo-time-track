module LanceTrack {
    export module Statistics {
        var app = angular.module("lance-track.statistics",
            ["ui.router", "lance-track.shared", "ngAnimate"]);

        app.service("statisticsService", statisticsServiceFactory);
        app.controller("statisticsController", statisticsController);
    }
}