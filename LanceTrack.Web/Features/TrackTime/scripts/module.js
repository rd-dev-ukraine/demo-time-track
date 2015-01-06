var LanceTrack;
(function (LanceTrack) {
    (function (TrackTime) {
        var app = angular.module("lance-track.track-time", ["lance-track.shared"]);

        app.service("trackTimeService", TrackTime.trackTimeServiceFactory);

        app.controller("trackTimeController", TrackTime.trackTimeController);
        app.controller("timeCellController", TrackTime.timeCellController);
    })(LanceTrack.TrackTime || (LanceTrack.TrackTime = {}));
    var TrackTime = LanceTrack.TrackTime;
})(LanceTrack || (LanceTrack = {}));
//# sourceMappingURL=module.js.map
