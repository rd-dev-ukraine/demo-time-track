module LanceTrack {
    export module TrackTime {
        export function timeCellController($scope: TimeCellScope, trackTimeService: TrackTimeService) {

            $scope.$watch("cell.hours", (oldVal, newVal) => {
                if (oldVal == undefined || oldVal == newVal)
                    return;

                $scope.isLoading = true;

                trackTimeService.trackTime($scope.project.projectId, $scope.cell.date, $scope.cell.hours)
                    .then(() => {
                        $scope.$root.$broadcast("TimeTracked");
                    }).finally(() => $scope.isLoading = false);
            });
        }

        export interface TimeCellScope extends TrackTimeScope {
            project: ProjectTimeInfo;
            cell: TimeRecord;
            isLoading: boolean;
        }
    }
}
LanceTrack.TrackTime.timeCellController.$inject = ["$scope", "trackTimeService"];