module LanceTrack {
    export module TrackTime {

        export function usersTimeController(
            $scope: UserTimeScope) {

            $scope.byUserMode = false;

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

        }

        export interface UserTimeScope extends TrackTimeBaseScope {
            byUserMode: boolean;

            projectsForUser(userId: number): Api.Project[];
            usersForProject(projectId: number): Api.UserAccount[];
        }
    }
}
LanceTrack.TrackTime.usersTimeController.$inject = ["$scope"];