var LanceTrack;
(function (LanceTrack) {
    (function (TrackTime) {
        function trackTimeController($scope, trackTimeService, dates) {
            function reload() {
                trackTimeService.load($scope.startDate, $scope.endDate).then(function (r) {
                    $scope.projectTime = r;
                });
            }

            $scope.startDate = dates.format(dates.startOfCurrentWeek());
            $scope.endDate = dates.format(dates.endOfCurrentWeek());

            $scope.dateRange = function () {
                return _.map(dates.dateRange($scope.startDate, $scope.endDate), function (d) {
                    return dates.format(d);
                });
            };

            reload();

            $scope.$watch("startDate", reload);
            $scope.$watch("endDate", reload);
        }
        TrackTime.trackTimeController = trackTimeController;
    })(LanceTrack.TrackTime || (LanceTrack.TrackTime = {}));
    var TrackTime = LanceTrack.TrackTime;
})(LanceTrack || (LanceTrack = {}));
LanceTrack.TrackTime.trackTimeController.$inject = ["$scope", "trackTimeService", "dates"];
//# sourceMappingURL=trackTimeController.js.map
