module LanceTrack {
    export module TrackTime {
        import ProjectTimeInfo = Api.ProjectTimeInfo;
        import TimeRecord = Api.TimeRecord;

        export function timeCellController(
            $scope: TimeCellScope,
            trackTimeService: TrackTimeService,
            deferredFunction: LanceTrack.DeferredFunctionService) {

            $scope.trackTime = deferredFunction.decorate(() => {
                return trackTimeService.trackTime($scope.project.projectId, $scope.cell.date, $scope.cell.hours)
                    .then(() => {
                        $scope.lastHours = $scope.cell.hours;
                        $scope.$root.$broadcast("TimeTracked");
                    });

            });

            $scope.$watch("cell.hours", (oldVal, newVal) => {
                if (oldVal == undefined || oldVal == newVal || newVal == $scope.cell.hours)
                    return;

                $scope.trackTime();
            });
        }

        export interface TimeCellScope extends TrackTimeScope {
            lastHours: number;
            project: ProjectTimeInfo;
            cell: TimeRecord;

            trackTime: LanceTrack.DeferredDecoratedFunction<any>;
        }
    }
}
LanceTrack.TrackTime.timeCellController.$inject = ["$scope", "trackTimeService", "deferredFunction"];