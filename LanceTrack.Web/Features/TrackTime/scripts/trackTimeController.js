var LanceTrack;
(function (LanceTrack) {
    (function (TrackTime) {
        function trackTimeController($scope, trackTimeService, dates, deferredFunction) {
            function reload() {
                trackTimeService.load($scope.date).then(function (r) {
                    $scope.projectTime = r;
                });
            }

            $scope.date = dates.now();

            $scope.dateRange = function () {
                return _.map(dates.allDateInWeek($scope.date), function (d) {
                    return dates.formatDay(d);
                });
            };

            $scope.recalculateAll = deferredFunction.decorate(function () {
                return trackTimeService.recalculateAll();
            });

            reload();

            $scope.$watch("date", reload);
        }
        TrackTime.trackTimeController = trackTimeController;
    })(LanceTrack.TrackTime || (LanceTrack.TrackTime = {}));
    var TrackTime = LanceTrack.TrackTime;
})(LanceTrack || (LanceTrack = {}));
LanceTrack.TrackTime.trackTimeController.$inject = ["$scope", "trackTimeService", "dates", "deferredFunction"];
//# sourceMappingURL=trackTimeController.js.map
