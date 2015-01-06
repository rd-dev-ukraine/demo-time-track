module LanceTrack {
    export module TrackTime {
        export function trackTimeController(
            $scope: TrackTimeScope,
            trackTimeService: TrackTimeService,
            dates: LanceTrack.Dates,
            deferredFunction: LanceTrack.DeferredFunctionService) {

            function reload() {
                trackTimeService.load($scope.startDate, $scope.endDate)
                    .then(r => {
                        $scope.projectTime = r;
                    });
            }


            $scope.startDate = dates.format(dates.startOfCurrentWeek());
            $scope.endDate = dates.format(dates.endOfCurrentWeek());

            $scope.dateRange = () => _.map(dates.allDateInRange($scope.startDate, $scope.endDate), d => dates.format(d));

            $scope.recalculateAll = deferredFunction.decorate(() => trackTimeService.recalculateAll());

            reload();

            $scope.$watch("startDate", reload);
            $scope.$watch("endDate", reload);
        }

        export interface TrackTimeScope extends ng.IScope {
            startDate: string;
            endDate: string;
            projectTime: ProjectTimeInfo[];

            dateRange: () => string[];
            recalculateAll: DeferredDecoratedFunction<any>;
        }
    }
}
LanceTrack.TrackTime.trackTimeController.$inject = ["$scope", "trackTimeService", "dates", "deferredFunction"];