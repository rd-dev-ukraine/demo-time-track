﻿var LanceTrack;
(function (LanceTrack) {
    (function (TrackTime) {
        function timeCellController($scope, trackTimeService, deferredFunction) {
            $scope.trackTime = deferredFunction.decorate(function () {
                return trackTimeService.trackTime($scope.cell.projectId, $scope.cell.date, $scope.cell.totalHours).then(function () {
                    $scope.lastHours = $scope.cell.totalHours;
                    $scope.$root.$broadcast("TimeTracked");
                });
            });

            $scope.$watch("cell.hours", function (oldVal, newVal) {
                if (oldVal == undefined || oldVal == newVal || newVal == $scope.cell.totalHours)
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
