var LanceTrack;
(function (LanceTrack) {
    var Statistics;
    (function (Statistics) {
        var app = angular.module("lance-track.statistics", ["ui.router", "lance-track.shared", "ngAnimate"]);
        app.service("statisticsService", Statistics.statisticsServiceFactory);
        app.controller("statisticsController", Statistics.statisticsController);
    })(Statistics = LanceTrack.Statistics || (LanceTrack.Statistics = {}));
})(LanceTrack || (LanceTrack = {}));
//# sourceMappingURL=module.js.map