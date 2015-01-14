var LanceTrack;
(function (LanceTrack) {
    (function (TrackTime) {
        var app = angular.module("lance-track.track-time", ["lance-track.shared"]);

        app.service("trackTimeService", TrackTime.trackTimeServiceFactory);

        app.controller("trackMyTimeController", TrackTime.trackMyTimeController);
        app.controller("timeCellController", TrackTime.timeCellController);
        app.controller("statisticsController", TrackTime.statisticsController);
    })(LanceTrack.TrackTime || (LanceTrack.TrackTime = {}));
    var TrackTime = LanceTrack.TrackTime;
})(LanceTrack || (LanceTrack = {}));
//# sourceMappingURL=module.js.map
