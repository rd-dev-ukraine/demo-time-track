var LanceTrack;
(function (LanceTrack) {
    var TrackTime;
    (function (TrackTime) {
        var app = angular.module("lance-track.track-time", ["ui.router", "ui.bootstrap.datetimepicker", "lance-track.shared"]);
        app.service("trackTimeService", TrackTime.trackTimeServiceFactory);
        app.controller("trackTimeBaseController", TrackTime.trackTimeBaseController);
        app.controller("myTimeController", TrackTime.myTimeController);
        app.controller("usersTimeController", TrackTime.usersTimeController);
        app.controller("timeCellController", TrackTime.timeCellController);
        app.controller("statisticsController", TrackTime.statisticsController);
        app.config(function ($stateProvider) {
            $stateProvider.state("track-time", {
                abstract: true,
                url: "^/track-time/:at/",
                templateUrl: urls.templates.trackTimeBase,
                controller: "trackTimeBaseController"
            }).state("track-time.my", {
                url: "^/track-time/:at/my/",
                templateUrl: urls.templates.trackMyTime,
                controller: "myTimeController"
            }).state("track-time.users", {
                url: "^/track-time/:at/users/",
                templateUrl: urls.templates.usersTime,
                controller: "usersTimeController"
            });
        });
    })(TrackTime = LanceTrack.TrackTime || (LanceTrack.TrackTime = {}));
})(LanceTrack || (LanceTrack = {}));
//# sourceMappingURL=module.js.map