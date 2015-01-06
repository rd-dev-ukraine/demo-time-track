var LanceTrack;
(function (LanceTrack) {
    (function (TrackTime) {
        function timeCellController($scope, trackTimeService) {
            $scope.$watch("cell.hours", function (oldVal, newVal) {
                if (oldVal == undefined || oldVal == newVal)
                    return;

                $scope.isLoading = true;

                trackTimeService.trackTime($scope.project.projectId, $scope.cell.date, $scope.cell.hours).then(function () {
                    $scope.$root.$broadcast("TimeTracked");
                }).finally(function () {
                    return $scope.isLoading = false;
                });
            });
        }
        TrackTime.timeCellController = timeCellController;
    })(LanceTrack.TrackTime || (LanceTrack.TrackTime = {}));
    var TrackTime = LanceTrack.TrackTime;
})(LanceTrack || (LanceTrack = {}));
LanceTrack.TrackTime.timeCellController.$inject = ["$scope", "trackTimeService"];
//# sourceMappingURL=timeCellController.js.map
