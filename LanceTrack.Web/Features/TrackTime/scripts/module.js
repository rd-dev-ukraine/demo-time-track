var LanceTrack;
(function (LanceTrack) {
    (function (TrackTime) {
        var app = angular.module("lance-track.track-time", ["ui.router", "lance-track.shared"]);

        app.service("trackTimeService", TrackTime.trackTimeServiceFactory);

        app.controller("trackTimeBaseController", TrackTime.trackTimeBaseController);
        app.controller("myTimeController", TrackTime.myTimeController);
        app.controller("usersTimeController", TrackTime.usersTimeController);
        app.controller("timeCellController", TrackTime.timeCellController);
        app.controller("statisticsController", TrackTime.statisticsController);

        app.config(function ($stateProvider) {
            $stateProvider.state("track-time", {
                abstract: true,
                url: "/track-time",
                templateUrl: urls.templates.trackTimeBase,
                controller: "trackTimeBaseController"
            }).state("track-time.my", {
                url: "/my/:at",
                templateUrl: urls.templates.trackMyTime,
                controller: "myTimeController"
            }).state("track-time.users", {
                url: "/users/:at",
                templateUrl: urls.templates.usersTime,
                controller: "usersTimeController"
            });
        });
    })(LanceTrack.TrackTime || (LanceTrack.TrackTime = {}));
    var TrackTime = LanceTrack.TrackTime;
})(LanceTrack || (LanceTrack = {}));
//# sourceMappingURL=module.js.map
