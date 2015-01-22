module LanceTrack {
    export module Authorization {
        export function authorizationController(
            $scope: AuthorizationScope,
            authorizationService: AuthorizationService) {

            $scope.model = { login: "", password: "", rememberMe: true };
            $scope.login = () => {
                $scope.error = null;
                authorizationService.login($scope.model)
                    .then(() => {
                        $scope.$close();
                    })
                    .catch(err => $scope.error = err);
            };
        }

        export interface AuthorizationScope extends ng.ui.bootstrap.IModalScope {
            model: Api.LoginModel;
            error: any;
            login();
        }
    }
}

LanceTrack.Authorization.authorizationController.$inject = ["$scope", "authorizationService"];