module LanceTrack {
    export module TrackTime {

        export function usersTimeController(
            $scope: UserTimeScope,
            $state: ng.ui.IStateService,
            $stateParams: { at: string },
            trackTimeService: TrackTimeService,
            dates: LanceTrack.Dates) {

            $scope.projectsForUser = (userId: number) => {
                if (!$scope.data)
                    return null;

                return _.filter(
                    $scope.data.projects,
                    (p: Api.Project) => _.any($scope.data.time, (t: Api.ProjectDailyTime) => t.userId == userId && t.projectId == p.id));
            };
        }

        export interface UserTimeScope extends TrackTimeBaseScope {
            projectsForUser(userId: number): Api.Project[];
        }
    }
}
LanceTrack.TrackTime.usersTimeController.$inject = ["$scope", "$state", "$stateParams", "trackTimeService", "dates"];