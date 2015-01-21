module LanceTrack {
    export module Shared {
        var app = angular.module("lance-track.shared", ["ui.bootstrap"]);

        app.directive("uiStateActiveExt", uiStateActiveExtDirective);

        app.filter("hours", LanceTrack.hoursFilter);
        app.filter("currency", LanceTrack.currencyFilter);
        app.filter("yesNo", LanceTrack.yesNoFilter);

        app.service("dates", LanceTrack.datesFactory);
        app.service("deferredFunction", LanceTrack.deferredFunctionServiceFactory);
    }
} 