module LanceTrack {
    export module Invoicing {
        var app = angular.module("lance-track.invoicing", ["ui.router", "lance-track.shared"]);

        app.service("invoiceService", invoiceServiceFactory);
        app.controller("billProjectController", billProjectController);
        app.controller("invoiceDetailsController", invoiceDetailsController);
        app.controller("invoiceListController", invoiceListController);

        app.config(($stateProvider: ng.ui.IStateProvider) => {

            $stateProvider
                .state(routes.billProject, {
                url: "/bill-project/{projectId:int}",
                templateUrl: urls.templates.billProject,
                controller: "billProjectController"
            })
                .state(routes.invoiceDetails, {
                url: "/invoice/{invoiceNum}",
                templateUrl: urls.templates.invoiceDetails,
                controller: "invoiceDetailsController"
            })
                .state(routes.invoiceList, {
                url: "/invoices/{mode}",
                templateUrl: urls.templates.invoiceList,
                controller: "invoiceListController"
            });
        });

        export var routes = {
            invoiceDetails: "invoiceDetails",
            billProject: "billProject",
            invoiceList: "invoiceList"
        };
    }
}