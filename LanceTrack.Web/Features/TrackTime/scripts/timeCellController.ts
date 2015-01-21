module LanceTrack {
    export module TrackTime {

        export function timeCellController(
            $scope: TimeCellScope,
            trackTimeService: TrackTimeService,
            deferredFunction: LanceTrack.DeferredFunctionService) {

            $scope.trackTime = deferredFunction.decorate(() => {
                return trackTimeService.trackTime(
                    $scope.cell.projectId,
                    $scope.cell.userId,
                    $scope.cell.date,
                    $scope.cell.totalHours).then(() => {
                        $scope.$root.$broadcast("StatisticsUpdated");
                    });

            });

            $scope.$watch("cell.totalHours", (oldVal, newVal) => {
                if (oldVal == undefined || oldVal == newVal ||
                    newVal == $scope.cell.totalHours)
                    return;

                $scope.trackTime();
            });
        }

        export interface TimeCellScope extends ng.IScope {
            cell: Api.DailyTime;

            trackTime: LanceTrack.DeferredDecoratedFunction<any>;
        }
    }
}
LanceTrack.TrackTime.timeCellController.$inject = ["$scope", "trackTimeService", "deferredFunction"];