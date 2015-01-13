module LanceTrack {
    export module TrackTime {

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
                        $scope.dates = dates.allDateInRange(r.startDate, r.endDate);
                    });
            }

            $scope.dateService = dates;
            $scope.date = dates.format($stateParams.at || dates.now());



            $scope.recalculateAll = deferredFunction.decorate(() => trackTimeService.recalculateAll());
            $scope.statistics = deferredFunction.decorate(() => trackTimeService.statistic());

            $scope.totalHoursAt = (date: any): number => {
                if (!$scope.projectTime)
                    return null;

                var allTime = _.chain($scope.projectTime.projects)
                    .map(t => t.time)
                    .flatten()
                    .value();

                var timeAtDate = _.filter(allTime, t => dates.eq(t.date, date));

                var result = <number>_.reduce(timeAtDate, (total: number, t: Api.TimeRecord) => total + (+t.hours), 0);

                if (!result)
                    return null;

                return result;
            };

            $scope.totalHoursForProject = (projectId: number): number => {
                if (!$scope.projectTime)
                    return null;

                var projTime = _.find($scope.projectTime.projects, e => e.projectId == projectId);
                var result = <number>_.reduce(projTime.time, (acc: number, t: Api.TimeRecord) => acc + (+t.hours), 0);

                if (!result)
                    return null;

                return result;
            };

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
            projectTime: Api.ProjectTimeInfoResult;
            dates: Date[];

            statistics: DeferredDecoratedFunction<Api.>;

            recalculateAll: DeferredDecoratedFunction<any>;
            totalHoursAt(date: any): number;
            totalHoursForProject(projectId: number): number;

            dateService: LanceTrack.Dates;
        }
    }
}
LanceTrack.TrackTime.trackTimeController.$inject = ["$scope", "$state", "$stateParams", "trackTimeService", "dates", "deferredFunction"];