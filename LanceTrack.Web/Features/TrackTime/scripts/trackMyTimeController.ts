module LanceTrack {
    export module TrackTime {

        export function trackMyTimeController(
            $scope: TrackMyTimeScope,
            $state: ng.ui.IStateService,
            $stateParams: { at: string },
            trackTimeService: TrackTimeService,
            dates: LanceTrack.Dates,
            deferredFunction: LanceTrack.DeferredFunctionService) {

            function reload() {
                trackTimeService.loadMyTime($scope.at)
                    .then(r => {
                        $scope.data = r;
                        $scope.dates = dates.allDateInRange(r.startDate, r.endDate);
                    });
            }

            $scope.dateService = dates;
            $scope.at = dates.format($stateParams.at || dates.now());



            $scope.recalculateAll = deferredFunction.decorate(() => trackTimeService.recalculateAll());
            $scope.statistics = deferredFunction.decorate(() => trackTimeService.statistic());

            $scope.cell = (projectId: number, date: any) : Api.ProjectDailyTime => {
                if (!$scope.data)
                    return null;

                return _.find(
                    $scope.data.time,
                    (t: Api.ProjectDailyTime) => t.projectId == projectId && dates.eq(t.date, date));
            };

            $scope.totalHoursAt = (date: any): number => {
                if (!$scope.data)
                    return null;

                var timeAtDate = _.filter($scope.data.time, t => dates.eq(t.date, date));

                var result = <number>_.reduce(timeAtDate, (total: number, t: Api.ProjectDailyTime) => total + (+t.totalHours), 0);

                if (!result)
                    return null;

                return result;
            };

            $scope.totalHoursForProject = (projectId: number): number => {
                if (!$scope.data)
                    return null;

                var projectTime = _.filter($scope.data.time, e => e.projectId == projectId);
                var result = <number>_.reduce(projectTime, (acc: number, t: Api.ProjectDailyTime) => acc + (+t.totalHours), 0);

                if (!result)
                    return null;

                return result;
            };

            reload();

            $scope.statistics();

            $scope.$watch("at", (o, n) => {
                if (o == undefined || o == n)
                    return;

                $state.go("my-time", { at: $scope.at });
            });

            $scope.$on("TimeTracked", () => $scope.statistics());
        }

        export interface TrackMyTimeScope extends ng.IScope {
            data: Api.ProjectTimeInfoResult;

            at: string;
            dates: Date[];

            statistics: DeferredDecoratedFunction<Api.StatisticsResult>;
            
            cell(projectId: number, date: any): Api.ProjectDailyTime;
            recalculateAll: DeferredDecoratedFunction<any>;
            totalHoursAt(date: any): number;
            totalHoursForProject(projectId: number): number;

            dateService: LanceTrack.Dates;
        }
    }
}
LanceTrack.TrackTime.trackMyTimeController.$inject = ["$scope", "$state", "$stateParams", "trackTimeService", "dates", "deferredFunction"];