var LanceTrack;
(function (LanceTrack) {
    var Authorization;
    (function (Authorization) {
        var app = angular.module("lance-track.authorization", ["ui.router", "ui.bootstrap", "lance-track.shared"]);
        app.service("authorizationService", Authorization.authorizationServiceFactory);
        app.controller("authorizationController", Authorization.authorizationController);
        app.config(function ($stateProvider) {
            $stateProvider.state("login", {
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
        });
    })(Authorization = LanceTrack.Authorization || (LanceTrack.Authorization = {}));
})(LanceTrack || (LanceTrack = {}));
//# sourceMappingURL=module.js.map