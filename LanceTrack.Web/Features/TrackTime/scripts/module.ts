module LanceTrack {
    export module TrackTime {
        var app = angular.module("lance-track.track-time", []);

        app.service("dates", LanceTrack.datesFactory);
        app.service("trackTimeService", trackTimeServiceFactory);

        app.controller("trackTimeController", trackTimeController);
    }
}