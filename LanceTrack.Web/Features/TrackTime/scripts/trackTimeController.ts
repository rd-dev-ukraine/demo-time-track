module LanceTrack {
    export module TrackTime {
        export function trackTimeController($scope: TrackTimeScope, trackTimeService: TrackTimeService) {
            function reload() {
                trackTimeService.load($scope.startDate, $scope.endDate)
                    .then(r => {
                        $scope.projectTime = r;
                    });
            }

            $scope.startDate = moment().startOf("week").format(DateFormat);
            $scope.endDate = moment().endOf("week").format(DateFormat);

            reload();

            $scope.$watch("startDate", reload);
            $scope.$watch("endDate", reload);
        }

        export interface TrackTimeScope extends ng.IScope {
            startDate: string;
            endDate: string;

            projectTime: ProjectTimeInfo[];
        }
    }
} 
LanceTrack.TrackTime.trackTimeController.$inject = ["$scope", "trackTimeService"];