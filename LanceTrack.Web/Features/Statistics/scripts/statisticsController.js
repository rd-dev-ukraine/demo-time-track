var LanceTrack;
(function (LanceTrack) {
    var Statistics;
    (function (Statistics) {
        function statisticsController($scope, statisticsService, deferredFunction) {
            $scope.statistics = deferredFunction.decorate(function () { return statisticsService.statistic(); });
            $scope.$on("StatisticsUpdated", function () { return $scope.statistics(); });
            $scope.statistics();
        }
        Statistics.statisticsController = statisticsController;
    })(Statistics = LanceTrack.Statistics || (LanceTrack.Statistics = {}));
})(LanceTrack || (LanceTrack = {}));
LanceTrack.Statistics.statisticsController.$inject = ["$scope", "statisticsService", "deferredFunction"];
//# sourceMappingURL=statisticsController.js.map