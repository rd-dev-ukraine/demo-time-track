module LanceTrack {
    export module TrackTime {
        var app = angular.module("lance-track.track-time",
            ["ui.router", "ui.bootstrap.datetimepicker", "lance-track.shared", "ngAnimate"]);

        app.service("trackTimeService", trackTimeServiceFactory);

        app.controller("trackTimeController", trackTimeController);
        app.controller("timeCellController", timeCellController);
        app.controller("timeCellController", timeCellController);
        app.controller("statisticsController", statisticsController);

        app.config(($stateProvider: ng.ui.IStateProvider) => {

            $stateProvider
                .state(routes.trackTime, {
                url: "/track-time/{at}/{mode}",
                templateUrl: urls.templates.trackTime,
                controller: "trackTimeController"
            });
        });

        export var routes = {
            trackTime: "track-time"
        };
    }
}