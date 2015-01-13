module LanceTrack {
    export module TrackTime {
        var app = angular.module("lance-track.track-time", ["lance-track.shared"]);

        app.service("trackTimeService", trackTimeServiceFactory);

        app.controller("trackMyTimeController", trackMyTimeController);
        app.controller("timeCellController", timeCellController);
    }
}