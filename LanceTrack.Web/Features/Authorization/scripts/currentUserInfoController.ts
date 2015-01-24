module LanceTrack {
    export module Authorization {
        export function currentUserController(
            $scope: CurrentUserScope,
            $state: ng.ui.IStateService,
            authorizationService: AuthorizationService) {

            authorizationService.currentUser().then(usr => $scope.currentUser = usr);

            $scope.logout = () => {

                authorizationService.logout().then(() => {
                    $state.go(routes.login);
                });
            };
        }

        export interface CurrentUserScope extends ng.IScope {
            currentUser: Api.UserAccount;
            logout();
        }
    }
}

LanceTrack.Authorization.currentUserController.$inject = ["$scope", "$state", "authorizationService"];