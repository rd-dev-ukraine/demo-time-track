var LanceTrack;
(function (LanceTrack) {
    (function (TrackTime) {
        function usersTimeController($scope) {
            $scope.byUserMode = false;

            $scope.projectsForUser = function (userId) {
                if (!$scope.data)
                    return null;

                return _.filter($scope.data.projects, function (p) {
                    return _.any($scope.data.time, function (t) {
                        return t.userId == userId && t.projectId == p.id;
                    });
                });
            };

            $scope.usersForProject = function (projectId) {
                if (!$scope.data)
                    return null;

                return _.filter($scope.data.users, function (u) {
                    return _.any($scope.data.time, function (t) {
                        return t.userId == u.id && t.projectId == projectId;
                    });
                });
            };
        }
        TrackTime.usersTimeController = usersTimeController;
    })(LanceTrack.TrackTime || (LanceTrack.TrackTime = {}));
    var TrackTime = LanceTrack.TrackTime;
})(LanceTrack || (LanceTrack = {}));
LanceTrack.TrackTime.usersTimeController.$inject = ["$scope"];
//# sourceMappingURL=usersTimeController.js.map
