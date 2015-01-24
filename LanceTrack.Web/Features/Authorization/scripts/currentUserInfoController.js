var LanceTrack;
(function (LanceTrack) {
    var Authorization;
    (function (Authorization) {
        function currentUserController($scope, $state, authorizationService) {
            authorizationService.currentUser().then(function (usr) { return $scope.currentUser = usr; });
            $scope.logout = function () {
                authorizationService.logout().then(function () {
                    $state.go(Authorization.routes.login);
                });
            };
        }
        Authorization.currentUserController = currentUserController;
    })(Authorization = LanceTrack.Authorization || (LanceTrack.Authorization = {}));
})(LanceTrack || (LanceTrack = {}));
LanceTrack.Authorization.currentUserController.$inject = ["$scope", "$state", "authorizationService"];
//# sourceMappingURL=currentUserInfoController.js.map