module LanceTrack {
    export module Authorization {
        var app = angular.module("lance-track.authorization", ["ui.router", "ui.bootstrap", "lance-track.shared"]);


        app.service("authorizationService", authorizationServiceFactory);
        app.controller("authorizationController", authorizationController);

        app.config(($stateProvider: ng.ui.IStateProvider) => {

            $stateProvider.state("login", {
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
        });
    }
} 