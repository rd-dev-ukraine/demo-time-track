module LanceTrack {
    export module Shared {
        var app = angular.module("lance-track.shared", ["ui.bootstrap"]);

        app.filter("hours", LanceTrack.hoursFilter);

        app.service("dates", LanceTrack.datesFactory);
        app.service("deferredFunction", LanceTrack.deferredFunctionServiceFactory);
    }
} 