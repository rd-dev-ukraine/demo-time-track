var LanceTrack;
(function (LanceTrack) {
    (function (TrackTime) {
        function trackTimeController($scope, $state, $stateParams, trackTimeService, dates, deferredFunction) {
            function reload() {
                trackTimeService.load($scope.date).then(function (r) {
                    $scope.projectTime = r;
                    $scope.dates = dates.allDateInRange(r.startDate, r.endDate);
                });
            }

            $scope.dateService = dates;
            $scope.date = dates.format($stateParams.at || dates.now());

            $scope.recalculateAll = deferredFunction.decorate(function () {
                return trackTimeService.recalculateAll();
            });
            $scope.statistics = deferredFunction.decorate(function () {
                return trackTimeService.statistic();
            });

            $scope.totalHoursAt = function (date) {
                if (!$scope.projectTime)
                    return null;

                var allTime = _.chain($scope.projectTime.projects).map(function (t) {
                    return t.time;
                }).flatten().value();

                var timeAtDate = _.filter(allTime, function (t) {
                    return dates.eq(t.date, date);
                });

                var result = _.reduce(timeAtDate, function (total, t) {
                    return total + (+t.hours);
                }, 0);

                if (result == 0)
                    return null;

                return result;
            };

            reload();

            $scope.statistics();

            $scope.$watch("date", function (o, n) {
                if (o == undefined || o == n)
                    return;

                $state.go("track-time", { at: $scope.date });
            });

            $scope.$on("TimeTracked", function () {
                return $scope.statistics();
            });
        }
        TrackTime.trackTimeController = trackTimeController;
    })(LanceTrack.TrackTime || (LanceTrack.TrackTime = {}));
    var TrackTime = LanceTrack.TrackTime;
})(LanceTrack || (LanceTrack = {}));
LanceTrack.TrackTime.trackTimeController.$inject = ["$scope", "$state", "$stateParams", "trackTimeService", "dates", "deferredFunction"];
//# sourceMappingURL=trackTimeController.js.map
