module LanceTrack {
    export module TrackTime {
        var app = angular.module("lance-track.track-time", ["ui.router", "ui.bootstrap.datetimepicker", "lance-track.shared"]);

        app.service("trackTimeService", trackTimeServiceFactory);

        app.controller("trackTimeBaseController", trackTimeBaseController);
        app.controller("myTimeController", myTimeController);
        app.controller("usersTimeController", usersTimeController);
        app.controller("timeCellController", timeCellController);
        app.controller("statisticsController", statisticsController);

        app.config(($stateProvider: ng.ui.IStateProvider) => {

            $stateProvider
                .state("track-time", {
                    abstract: true,
                    url: "^/track-time/:at/",
                    templateUrl: urls.templates.trackTimeBase,
                    controller: "trackTimeBaseController"
                })
                .state("track-time.my", {
                    url: "^/track-time/:at/my/",
                    templateUrl: urls.templates.trackMyTime,
                    controller: "myTimeController"
                })
                .state("track-time.users", {
                    url: "^/track-time/:at/users/",
                    templateUrl: urls.templates.usersTime,
                    controller: "usersTimeController"
                });
        });
    }
}