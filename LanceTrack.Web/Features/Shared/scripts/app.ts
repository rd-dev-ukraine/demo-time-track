module LanceTrack {
    var app = angular.module("lance-track", ["ui.router", "ui.bootstrap", "lance-track.shared", "lance-track.track-time", "lance-track.invoicing"]);


    app.config(($stateProvider: ng.ui.IStateProvider, $urlRouterProvider: ng.ui.IUrlRouterProvider) => {
        var dates = new Dates();

        $urlRouterProvider.otherwise("/track-time/" + dates.format(dates.now()) + "/my/");

    });

    app.run(["$rootScope", ($rootScope: AppRootScope) => {
        $rootScope.urls = urls;
        $rootScope.modelOptions = { updateOn: "default blur", debounce: { "default": 1000, "blur": 0 } };
    }]);

    export interface AppRootScope extends ng.IScope {
        urls: Api.Urls;
        modelOptions: any;
    }
}

var urls: Api.Urls;