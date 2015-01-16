module LanceTrack {
    export module Invoicing {
        var app = angular.module("lance-track.invoicing", ["ui.router", "lance-track.shared"]);

        app.service("invoiceService", invoiceServiceFactory);
        app.controller("billProjectController", billProjectController);

        app.config(($stateProvider: ng.ui.IStateProvider) => {

            $stateProvider
                .state("billProject", {
                    url: "/bill-project/{projectId:int}",
                    templateUrl: urls.templates.billProject,
                    controller: "billProjectController"
                })
                .state("invoices", {
                    abstract: true,
                    url: "/invoices/",
                    templateUrl: urls.templates.invoiceBase,
                    controller: "invoiceBaseController"
                })
                .state("invoices.pending", {
                    url: "/pending",
                    templateUrl: urls.templates.trackMyTime,
                    controller: "myTimeController"
                })
                .state("invoices.archive", {
                    url: "/archive",
                    templateUrl: urls.templates.usersTime,
                    controller: "usersTimeController"
                });
        });
    }
}