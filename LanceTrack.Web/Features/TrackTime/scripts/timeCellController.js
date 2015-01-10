var LanceTrack;
(function (LanceTrack) {
    (function (TrackTime) {
        function timeCellController($scope, trackTimeService, deferredFunction) {
            $scope.trackTime = deferredFunction.decorate(function () {
                return trackTimeService.trackTime($scope.project.projectId, $scope.cell.date, $scope.cell.hours).then(function () {
                    $scope.lastHours = $scope.cell.hours;
                    $scope.$root.$broadcast("TimeTracked");
                }).catch(function () {
                    $scope.cell.hours = $scope.lastHours;
                });
            });

            $scope.$watch("cell.hours", function (oldVal, newVal) {
                if (oldVal == undefined || oldVal == newVal)
                    return;

                $scope.trackTime();
            });
        }
        TrackTime.timeCellController = timeCellController;
    })(LanceTrack.TrackTime || (LanceTrack.TrackTime = {}));
    var TrackTime = LanceTrack.TrackTime;
})(LanceTrack || (LanceTrack = {}));
LanceTrack.TrackTime.timeCellController.$inject = ["$scope", "trackTimeService", "deferredFunction"];
//# sourceMappingURL=timeCellController.js.map
