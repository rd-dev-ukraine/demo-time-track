var LanceTrack;
(function (LanceTrack) {
    (function (TrackTime) {
        function usersTimeController($scope, $state, $stateParams, trackTimeService, dates) {
            $scope.projectsForUser = function (userId) {
                if (!$scope.data)
                    return null;

                return _.filter($scope.data.projects, function (p) {
                    return _.any($scope.data.time, function (t) {
                        return t.userId == userId && t.projectId == p.id;
                    });
                });
            };
        }
        TrackTime.usersTimeController = usersTimeController;
    })(LanceTrack.TrackTime || (LanceTrack.TrackTime = {}));
    var TrackTime = LanceTrack.TrackTime;
})(LanceTrack || (LanceTrack = {}));
LanceTrack.TrackTime.usersTimeController.$inject = ["$scope", "$state", "$stateParams", "trackTimeService", "dates"];
//# sourceMappingURL=usersTimeController.js.map
