var LanceTrack;
(function (LanceTrack) {
    var TrackTime;
    (function (TrackTime) {
        var app = angular.module("lance-track.track-time", ["ui.router", "ui.bootstrap.datetimepicker", "lance-track.shared"]);
        app.service("trackTimeService", TrackTime.trackTimeServiceFactory);
        app.controller("trackTimeController", TrackTime.trackTimeController);
        app.controller("timeCellController", TrackTime.timeCellController);
        app.controller("timeCellController", TrackTime.timeCellController);
        app.controller("statisticsController", TrackTime.statisticsController);
        app.config(function ($stateProvider) {
            $stateProvider.state(TrackTime.routes.trackTime, {
                url: "/track-time/{at}/{mode}",
                templateUrl: urls.templates.trackTime,
                controller: "trackTimeController"
            });
        });
        TrackTime.routes = {
            trackTime: "track-time"
        };
    })(TrackTime = LanceTrack.TrackTime || (LanceTrack.TrackTime = {}));
})(LanceTrack || (LanceTrack = {}));
//# sourceMappingURL=module.js.map