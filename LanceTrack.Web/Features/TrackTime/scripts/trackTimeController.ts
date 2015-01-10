module LanceTrack {
    export module TrackTime {
        import ProjectTimeInfo = Api.ProjectTimeInfo;

        export function trackTimeController(
            $scope: TrackTimeScope,
            trackTimeService: TrackTimeService,
            dates: LanceTrack.Dates,
            deferredFunction: LanceTrack.DeferredFunctionService) {

            function reload() {
                trackTimeService.load($scope.date)
                    .then(r => {
                        $scope.projectTime = r;
                    });
            }

            $scope.date = dates.now();

            $scope.dateRange = () => _.map(dates.allDateInWeek($scope.date), d => dates.formatDay(d));

            $scope.recalculateAll = deferredFunction.decorate(() => trackTimeService.recalculateAll());

            reload();

            $scope.$watch("date", (o, n) => {
                if (o != undefined && o != n)
                    reload();
            });
        }

        export interface TrackTimeScope extends ng.IScope {
            date: string;
            projectTime: ProjectTimeInfo[];

            dateRange: () => string[];
            recalculateAll: DeferredDecoratedFunction<any>;
        }
    }
}
LanceTrack.TrackTime.trackTimeController.$inject = ["$scope", "trackTimeService", "dates", "deferredFunction"];