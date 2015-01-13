//module LanceTrack {
//    export module TrackTime {
//        export function timeCellController(
//            $scope: TimeCellScope,
//            trackTimeService: TrackTimeService,
//            deferredFunction: LanceTrack.DeferredFunctionService) {
//            $scope.trackTime = deferredFunction.decorate(() => {
//                return trackTimeService.trackTime($scope.cell.projectId, $scope.cell.date, $scope.cell.totalHours)
//                    .then(() => {
//                        $scope.lastHours = $scope.cell.totalHours;
//                        $scope.$root.$broadcast("TimeTracked");
//                    });
//            });
//            $scope.$watch("cell.hours", (oldVal, newVal) => {
//                if (oldVal == undefined || oldVal == newVal ||
//                    newVal == $scope.cell.totalHours)
//                    return;
//                $scope.trackTime();
//            });
//        }
//        export interface TimeCellScope extends TrackTimeScope {
//            lastHours: number;
//            cell: Api.ProjectDailyTime;
//            trackTime: LanceTrack.DeferredDecoratedFunction<any>;
//        }
//    }
//}
//LanceTrack.TrackTime.timeCellController.$inject = ["$scope", "trackTimeService", "deferredFunction"];
//# sourceMappingURL=timeCellController.js.map
