var LanceTrack;
(function (LanceTrack) {
    var Authorization;
    (function (Authorization) {
        function authorizationController($scope, authorizationService) {
            $scope.model = { login: "", password: "", rememberMe: true };
            $scope.login = function () {
                $scope.error = null;
                authorizationService.login($scope.model).then(function () {
                    $scope.$close();
                }).catch(function (err) { return $scope.error = err; });
            };
        }
        Authorization.authorizationController = authorizationController;
    })(Authorization = LanceTrack.Authorization || (LanceTrack.Authorization = {}));
})(LanceTrack || (LanceTrack = {}));
LanceTrack.Authorization.authorizationController.$inject = ["$scope", "authorizationService"];
//# sourceMappingURL=authorizationController.js.map