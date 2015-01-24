module LanceTrack {
    export module Authorization {
        var app = angular.module("lance-track.authorization", ["ui.router", "ui.bootstrap", "lance-track.shared"]);


        app.factory("authorizationHttpInterceptor", authorizationHttpInterceptor);
        app.service("authorizationService", authorizationServiceFactory);
        app.controller("authorizationController", authorizationController);
        app.controller("currentUserController", currentUserController);

        app.config(($stateProvider: ng.ui.IStateProvider, $httpProvider: ng.IHttpProvider) => {

            $stateProvider.state(routes.login, {
                url: "^/login",
                onEnter: [
                    "$state",
                    "$modal",
                    ($state: ng.ui.IStateService, $modal: ng.ui.bootstrap.IModalService) => {

                        $modal.open({
                            backdrop: "static",
                            templateUrl: urls.templates.loginTemplate,
                            controller: "authorizationController"
                        }).result
                          .finally(() => $state.go(TrackTime.routes.trackTime));
                    }]
            });

            $httpProvider.interceptors.push("authorizationHttpInterceptor");
        });

        export var routes = {
            login: "login"
        };
    }
} 