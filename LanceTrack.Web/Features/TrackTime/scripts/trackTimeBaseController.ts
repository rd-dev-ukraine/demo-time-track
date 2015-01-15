module LanceTrack {
    export module TrackTime {

        export function trackTimeBaseController(
            $scope: TrackTimeBaseScope,
            $state: ng.ui.IStateService,
            $stateParams: { at: string },
            trackTimeService: TrackTimeService,
            dates: LanceTrack.Dates) {

            function reload() {
                trackTimeService.loadTimeInfo($scope.at)
                    .then(r => {
                        $scope.data = r;
                        $scope.dates = dates.allDateInRange(r.startDate, r.endDate);
                    });
            }

            $scope.dateService = dates;
            $scope.at = dates.format($stateParams.at || dates.now());

            $scope.cell = (projectId: number, date: any, userId?: number) : Api.ProjectDailyTime => {
                if (!$scope.data)
                    return null;

                userId = userId || $scope.data.currentUserId;

                return _.find(
                    $scope.data.time,
                    (t: Api.ProjectDailyTime) => t.projectId == projectId && dates.eq(t.date, date) && t.userId == userId);
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
            
            $scope.$watch("at", (o, n) => {
                if (o == undefined || o == n)
                    return;

                $state.go($state.current.name, { at: $scope.at });
            });
        }

        export interface TrackTimeBaseScope extends ng.IScope {
            data: Api.ProjectTimeInfoResult;

            at: string;
            dates: Date[];
            
            cell(projectId: number, date: any, userId?: number): Api.ProjectDailyTime;

            totalHoursAt(date: any): number;
            totalHoursForProject(projectId: number): number;

            dateService: LanceTrack.Dates;
        }
    }
}
LanceTrack.TrackTime.trackTimeBaseController.$inject = ["$scope", "$state", "$stateParams", "trackTimeService", "dates"];