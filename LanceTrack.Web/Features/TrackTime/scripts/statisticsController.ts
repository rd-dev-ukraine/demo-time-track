module LanceTrack {
    export module TrackTime {
        export function statisticsController(
            $scope: StatisticsScope,
            trackTimeService: TrackTimeService,
            deferredFunction: LanceTrack.DeferredFunctionService) {

            $scope.statistics = deferredFunction.decorate(() => trackTimeService.statistic());

            $scope.$on("TimeTracked", () => $scope.statistics());

            $scope.statistics();
        }

        export interface StatisticsScope extends ng.IScope {
            statistics: DeferredDecoratedFunction<Api.StatisticsResult>;
        }
    }
} 
LanceTrack.TrackTime.statisticsController.$inject = ["$scope", "trackTimeService", "deferredFunction"];