module LanceTrack {
    export module Shared {
        var app = angular.module("lance-track.shared", ["ui.bootstrap"]);

        app.service("dates", LanceTrack.datesFactory);
        app.service("deferredFunction", LanceTrack.deferredFunctionServiceFactory);
    }
} 