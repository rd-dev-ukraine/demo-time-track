var LanceTrack;
(function (LanceTrack) {
    (function (TrackTime) {
        var app = angular.module("lance-track.track-time", []);

        app.service("dates", LanceTrack.datesFactory);
        app.service("trackTimeService", TrackTime.trackTimeServiceFactory);

        app.controller("trackTimeController", TrackTime.trackTimeController);
    })(LanceTrack.TrackTime || (LanceTrack.TrackTime = {}));
    var TrackTime = LanceTrack.TrackTime;
})(LanceTrack || (LanceTrack = {}));
//# sourceMappingURL=module.js.map
