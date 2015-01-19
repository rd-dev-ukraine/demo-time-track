var LanceTrack;
(function (LanceTrack) {
    var TrackTime;
    (function (TrackTime) {
        function timeCellController($scope, trackTimeService, deferredFunction) {
            $scope.trackTime = deferredFunction.decorate(function () {
                return trackTimeService.trackTime($scope.cell.projectId, $scope.cell.userId, $scope.cell.date, $scope.cell.totalHours).then(function () {
                    $scope.$root.$broadcast("TimeTracked");
                });
            });
            $scope.$watch("cell.totalHours", function (oldVal, newVal) {
                if (oldVal == undefined || oldVal == newVal || newVal == $scope.cell.totalHours)
                    return;
                $scope.trackTime();
            });
        }
        TrackTime.timeCellController = timeCellController;
    })(TrackTime = LanceTrack.TrackTime || (LanceTrack.TrackTime = {}));
})(LanceTrack || (LanceTrack = {}));
LanceTrack.TrackTime.timeCellController.$inject = ["$scope", "trackTimeService", "deferredFunction"];
//# sourceMappingURL=timeCellController.js.map