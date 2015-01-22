var LanceTrack;
(function (LanceTrack) {
    var Authorization;
    (function (Authorization) {
        var app = angular.module("lance-track.authorization", ["ui.router", "ui.bootstrap", "lance-track.shared"]);
        app.factory("authorizationHttpInterceptor", Authorization.authorizationHttpInterceptor);
        app.service("authorizationService", Authorization.authorizationServiceFactory);
        app.controller("authorizationController", Authorization.authorizationController);
        app.config(function ($stateProvider, $httpProvider) {
            $stateProvider.state(Authorization.routes.login, {
                url: "^/login",
                onEnter: [
                    "$state",
                    "$modal",
                    function ($state, $modal) {
                        $modal.open({
                            backdrop: "static",
                            templateUrl: urls.templates.loginTemplate,
                            controller: "authorizationController"
                        }).result.finally(function () { return $state.go(LanceTrack.TrackTime.routes.trackTime); });
                    }
                ]
            });
            $httpProvider.interceptors.push("authorizationHttpInterceptor");
        });
        Authorization.routes = {
            login: "login"
        };
    })(Authorization = LanceTrack.Authorization || (LanceTrack.Authorization = {}));
})(LanceTrack || (LanceTrack = {}));
//# sourceMappingURL=module.js.map