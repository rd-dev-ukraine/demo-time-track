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

            $scope.cell = (projectId: number, date: any, userId?: number) : Api.DailyTime => {
                if (!$scope.data)
                    return null;

                userId = userId || $scope.data.currentUserId;

                return _.find(
                    $scope.data.time,
                    (t: Api.DailyTime) => t.projectId == projectId && dates.eq(t.date, date) && t.userId == userId);
            };

            $scope.totalHours = (of: { projectId?: number; userId?: number; at?: string; }): number => {
                if (!$scope.data)
                    return null;

                var time = $scope.data.time;
                if (of.projectId)
                    time = _.filter(time, t => t.projectId == of.projectId);
                if (of.userId)
                    time = _.filter(time, t => t.userId == of.userId);
                if (of.at)
                    time = _.filter(time, t => dates.eq(t.date, of.at));


                var result = <number>_.reduce(time, (acc: number, t: Api.DailyTime) => acc + (+t.totalHours), 0);

                if (!result)
                    return null;

                return result;
            };

            $scope.canBillProject = (project: Api.Project) => {
                return (project.permissions & Api.ProjectPermissions.BillProject) !== 0;
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
            
            canBillProject(project: Api.Project): boolean;
            cell(projectId: number, date: any, userId?: number): Api.DailyTime;
            totalHours(of: { projectId?: number; userId?: number; at?: string; }): number;

            dateService: LanceTrack.Dates;
        }
    }
}
LanceTrack.TrackTime.trackTimeBaseController.$inject = ["$scope", "$state", "$stateParams", "trackTimeService", "dates"];