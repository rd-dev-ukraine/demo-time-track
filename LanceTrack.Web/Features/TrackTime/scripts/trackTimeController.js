var LanceTrack;
(function (LanceTrack) {
    var TrackTime;
    (function (TrackTime) {
        function trackTimeController($scope, $state, $stateParams, trackTimeService, dates) {
            function reload() {
                trackTimeService.loadTimeInfo($scope.at).then(function (r) {
                    $scope.data = r;
                    $scope.dates = dates.allDateInRange(r.startDate, r.endDate);
                });
            }
            $scope.mode = $stateParams.mode || TrackTimeMode.MyTime;
            $scope.dateService = dates;
            $scope.at = dates.format($stateParams.at || dates.now());
            $scope.cell = function (projectId, date, userId) {
                if (!$scope.data)
                    return null;
                userId = userId || $scope.data.currentUserId;
                return _.find($scope.data.time, function (t) { return t.projectId == projectId && dates.eq(t.date, date) && t.userId == userId; });
            };
            $scope.totalHours = function (of) {
                if (!$scope.data)
                    return null;
                var time = $scope.data.time;
                if (of.projectId)
                    time = _.filter(time, function (t) { return t.projectId == of.projectId; });
                if (of.userId)
                    time = _.filter(time, function (t) { return t.userId == of.userId; });
                if (of.at)
                    time = _.filter(time, function (t) { return dates.eq(t.date, of.at); });
                var result = _.reduce(time, function (acc, t) { return acc + (+t.totalHours); }, 0);
                if (!result)
                    return null;
                return result;
            };
            $scope.canBillProject = function (project) {
                return (project.permissions & 32) !== 0;
            };
            $scope.previousWeek = function () {
                $scope.at = dates.previousWeek($scope.at);
            };
            $scope.nextWeek = function () {
                $scope.at = dates.nextWeek($scope.at);
            };
            $scope.projectsForUser = function (userId) {
                if (!$scope.data)
                    return null;
                return _.filter($scope.data.projects, function (p) { return _.any($scope.data.time, function (t) { return t.userId == userId && t.projectId == p.id; }); });
            };
            $scope.usersForProject = function (projectId) {
                if (!$scope.data)
                    return null;
                return _.filter($scope.data.users, function (u) { return _.any($scope.data.time, function (t) { return t.userId == u.id && t.projectId == projectId; }); });
            };
            $scope.users = function () {
                if (!$scope.data)
                    return null;
                return _.filter($scope.data.users, function (u) { return $scope.mode != TrackTimeMode.MyTime || u.id == $scope.data.currentUserId; });
            };
            reload();
            _.forEach(["at", "mode"], function (property) {
                $scope.$watch(property, function (o, n) {
                    if (o == undefined || o == n)
                        return;
                    $state.go($state.current.name, {
                        at: dates.format($scope.at),
                        mode: $scope.mode
                    });
                });
            });
        }
        TrackTime.trackTimeController = trackTimeController;
        var TrackTimeMode = (function () {
            function TrackTimeMode() {
            }
            TrackTimeMode.MyTime = "my-time";
            TrackTimeMode.ByProject = "by-project";
            TrackTimeMode.ByUser = "by-user";
            return TrackTimeMode;
        })();
        TrackTime.TrackTimeMode = TrackTimeMode;
    })(TrackTime = LanceTrack.TrackTime || (LanceTrack.TrackTime = {}));
})(LanceTrack || (LanceTrack = {}));
LanceTrack.TrackTime.trackTimeController.$inject = ["$scope", "$state", "$stateParams", "trackTimeService", "dates"];
//# sourceMappingURL=trackTimeController.js.map