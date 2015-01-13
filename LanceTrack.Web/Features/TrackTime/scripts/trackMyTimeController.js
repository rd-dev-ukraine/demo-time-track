var LanceTrack;
(function (LanceTrack) {
    (function (TrackTime) {
        function trackMyTimeController($scope, $state, $stateParams, trackTimeService, dates, deferredFunction) {
            function reload() {
                trackTimeService.loadMyTime($scope.at).then(function (r) {
                    $scope.data = r;
                    $scope.dates = dates.allDateInRange(r.startDate, r.endDate);
                });
            }

            $scope.dateService = dates;
            $scope.at = dates.format($stateParams.at || dates.now());

            $scope.recalculateAll = deferredFunction.decorate(function () {
                return trackTimeService.recalculateAll();
            });
            $scope.statistics = deferredFunction.decorate(function () {
                return trackTimeService.statistic();
            });

            $scope.projectTime = function (projectId) {
                if (!$scope.data)
                    return null;

                return _.filter($scope.data.time, function (t) {
                    return t.projectId == projectId;
                });
            };

            $scope.totalHoursAt = function (date) {
                if (!$scope.data)
                    return null;

                var timeAtDate = _.filter($scope.data.time, function (t) {
                    return dates.eq(t.date, date);
                });

                var result = _.reduce(timeAtDate, function (total, t) {
                    return total + (+t.totalHours);
                }, 0);

                if (!result)
                    return null;

                return result;
            };

            $scope.totalHoursForProject = function (projectId) {
                if (!$scope.data)
                    return null;

                var projectTime = _.filter($scope.data.time, function (e) {
                    return e.projectId == projectId;
                });
                var result = _.reduce(projectTime, function (acc, t) {
                    return acc + (+t.totalHours);
                }, 0);

                if (!result)
                    return null;

                return result;
            };

            reload();

            $scope.statistics();

            $scope.$watch("at", function (o, n) {
                if (o == undefined || o == n)
                    return;

                $state.go("track-time", { at: $scope.at });
            });

            $scope.$on("TimeTracked", function () {
                return $scope.statistics();
            });
        }
        TrackTime.trackMyTimeController = trackMyTimeController;
    })(LanceTrack.TrackTime || (LanceTrack.TrackTime = {}));
    var TrackTime = LanceTrack.TrackTime;
})(LanceTrack || (LanceTrack = {}));
LanceTrack.TrackTime.trackMyTimeController.$inject = ["$scope", "$state", "$stateParams", "trackTimeService", "dates", "deferredFunction"];
//# sourceMappingURL=trackMyTimeController.js.map
