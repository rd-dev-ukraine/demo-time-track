﻿module LanceTrack {
    var app = angular.module("lance-track", ["ui.router", "lance-track.shared", "lance-track.track-time"]);
    

    app.config(($stateProvider: ng.ui.IStateProvider, $urlRouterProvider: ng.ui.IUrlRouterProvider) => {
        $urlRouterProvider.otherwise("/track-time/my/");
        
    });

    app.run(["$rootScope", ($rootScope: IAppRootScope) => {
        $rootScope.urls = urls;
    }]);

    export interface IAppRootScope extends ng.IScope {
        urls: Api.Urls;
    }
}

var urls: Api.Urls;