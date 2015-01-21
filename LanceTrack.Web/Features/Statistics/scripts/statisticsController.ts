module LanceTrack {
    export module Statistics {
        export function statisticsController(
            $scope: StatisticsScope,
            statisticsService: StatisticsService,
            deferredFunction: LanceTrack.DeferredFunctionService) {

            $scope.statistics = deferredFunction.decorate(() => statisticsService.statistic());

            $scope.$on("StatisticsUpdated", () => $scope.statistics());

            $scope.statistics();
        }

        export interface StatisticsScope extends ng.IScope {
            statistics: DeferredDecoratedFunction<Api.StatisticsResult>;
        }
    }
} 
LanceTrack.Statistics.statisticsController.$inject = ["$scope", "statisticsService", "deferredFunction"];