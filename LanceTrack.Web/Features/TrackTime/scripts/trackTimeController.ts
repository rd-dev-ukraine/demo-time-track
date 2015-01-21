module LanceTrack {
    export module TrackTime {

        export function trackTimeController(
            $scope: TrackTimeScope,
            $state: ng.ui.IStateService,
            $stateParams: { at: string; mode: string },
            trackTimeService: TrackTimeService,
            dates: LanceTrack.Dates) {

            function reload() {
                trackTimeService.loadTimeInfo($scope.at)
                    .then(r => {
                        $scope.data = r;
                        $scope.dates = dates.allDateInRange(r.startDate, r.endDate);
                    });
            }

            $scope.mode = $stateParams.mode || TrackTimeMode.MyTime;
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
                return (project.permissions & 32 /* Bill project */) !== 0;
            };

            $scope.previousWeek = () => {
                $scope.at = dates.previousWeek($scope.at);
            };

            $scope.nextWeek = () => {
                $scope.at = dates.nextWeek($scope.at);
            };

            $scope.projectsForUser = (userId: number) => {
                if (!$scope.data)
                    return null;

                return _.filter(
                    $scope.data.projects,
                    (p: Api.Project) => _.any($scope.data.time,
                        (t: Api.DailyTime) => t.userId == userId && t.projectId == p.id));
            };

            $scope.usersForProject = (projectId: number) => {
                if (!$scope.data)
                    return null;

                return _.filter(
                    $scope.data.users,
                    (u: Api.UserAccount) => _.any($scope.data.time,
                        (t: Api.DailyTime) => t.userId == u.id && t.projectId == projectId));
            };

            $scope.users = () => {
                if (!$scope.data)
                    return null;

                return _.filter($scope.data.users, u=> $scope.mode != TrackTimeMode.MyTime || u.id == $scope.data.currentUserId);
            };

            reload();

            _.forEach(["at", "mode"], property => {

                $scope.$watch(property,(o, n) => {
                    if (o == undefined || o == n)
                        return;

                    $state.go($state.current.name, {
                        at: dates.format($scope.at),
                        mode: $scope.mode
                    });
                });
            });
        }

        export interface TrackTimeScope extends ng.IScope {
            at: string;
            mode: string;

            data: Api.ProjectTimeInfoResult;
            dates: Date[];
            dateService: LanceTrack.Dates;
            
            previousWeek(): void;
            nextWeek(): void;

            canBillProject(project: Api.Project): boolean;
            cell(projectId: number, date: any, userId?: number): Api.DailyTime;
            totalHours(of: { projectId?: number; userId?: number; at?: string; }): number;

            projectsForUser(userId: number): Api.Project[];
            usersForProject(projectId: number): Api.UserAccount[];

            users(): Api.UserAccount[];
        }

        export class TrackTimeMode {
            static MyTime = "my-time";
            static ByProject = "by-project";
            static ByUser = "by-user";
        }
    }
}
LanceTrack.TrackTime.trackTimeController.$inject = ["$scope", "$state", "$stateParams", "trackTimeService", "dates"];