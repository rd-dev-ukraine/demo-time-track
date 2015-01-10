module LanceTrack {
    export module TrackTime {
        import ProjectTimeInfo = Api.ProjectTimeInfo;

        export function trackTimeController(
            $scope: TrackTimeScope,
            $state: ng.ui.IStateService,
            $stateParams: { at: string },
            trackTimeService: TrackTimeService,
            dates: LanceTrack.Dates,
            deferredFunction: LanceTrack.DeferredFunctionService) {

            function reload() {
                trackTimeService.load($scope.date)
                    .then(r => {
                        $scope.projectTime = r;
                    });
            }

            
            $scope.date = dates.format($stateParams.at || dates.now());

            $scope.dateRange = () => _.map(dates.allDateInWeek($scope.date), d => dates.formatDay(d));

            $scope.recalculateAll = deferredFunction.decorate(() => trackTimeService.recalculateAll());
            $scope.statistics = deferredFunction.decorate(() => trackTimeService.statistic());

            reload();
            $scope.statistics();

            $scope.$watch("date", (o, n) => {
                if (o == undefined || o == n)
                    return;

                $state.go("track-time", { at: $scope.date });
            });

            $scope.$on("TimeTracked", () => $scope.statistics());
        }

        export interface TrackTimeScope extends ng.IScope {
            date: string;
            projectTime: ProjectTimeInfo[];
            statistics: DeferredDecoratedFunction<Api.StatisticsResult>;
            dateRange: () => string[];
            recalculateAll: DeferredDecoratedFunction<any>;
        }
    }
}
LanceTrack.TrackTime.trackTimeController.$inject = ["$scope", "$state", "$stateParams", "trackTimeService", "dates", "deferredFunction"];