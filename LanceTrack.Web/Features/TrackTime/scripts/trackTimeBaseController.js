﻿var LanceTrack;
(function (LanceTrack) {
    (function (TrackTime) {
        function trackTimeBaseController($scope, $state, $stateParams, trackTimeService, dates) {
            function reload() {
                trackTimeService.loadTimeInfo($scope.at).then(function (r) {
                    $scope.data = r;
                    $scope.dates = dates.allDateInRange(r.startDate, r.endDate);
                });
            }

            $scope.dateService = dates;
            $scope.at = dates.format($stateParams.at || dates.now());

            $scope.cell = function (projectId, date, userId) {
                if (!$scope.data)
                    return null;

                userId = userId || $scope.data.currentUserId;

                return _.find($scope.data.time, function (t) {
                    return t.projectId == projectId && dates.eq(t.date, date) && t.userId == userId;
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

            $scope.$watch("at", function (o, n) {
                if (o == undefined || o == n)
                    return;

                $state.go($state.current.name, { at: $scope.at });
            });
        }
        TrackTime.trackTimeBaseController = trackTimeBaseController;
    })(LanceTrack.TrackTime || (LanceTrack.TrackTime = {}));
    var TrackTime = LanceTrack.TrackTime;
})(LanceTrack || (LanceTrack = {}));
LanceTrack.TrackTime.trackTimeBaseController.$inject = ["$scope", "$state", "$stateParams", "trackTimeService", "dates"];
//# sourceMappingURL=trackTimeBaseController.js.map