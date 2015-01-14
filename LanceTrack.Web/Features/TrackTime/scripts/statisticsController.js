var LanceTrack;
(function (LanceTrack) {
    (function (TrackTime) {
        function statisticsController($scope, trackTimeService, deferredFunction) {
            $scope.statistics = deferredFunction.decorate(function () {
                return trackTimeService.statistic();
            });

            $scope.$on("TimeTracked", function () {
                return $scope.statistics();
            });

            $scope.statistics();
        }
        TrackTime.statisticsController = statisticsController;
    })(LanceTrack.TrackTime || (LanceTrack.TrackTime = {}));
    var TrackTime = LanceTrack.TrackTime;
})(LanceTrack || (LanceTrack = {}));
LanceTrack.TrackTime.statisticsController.$inject = ["$scope", "trackTimeService", "deferredFunction"];
//# sourceMappingURL=statisticsController.js.map
