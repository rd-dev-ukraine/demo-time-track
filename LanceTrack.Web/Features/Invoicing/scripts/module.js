var LanceTrack;
(function (LanceTrack) {
    (function (Invoicing) {
        var app = angular.module("lance-track.invoicing", ["ui.router", "lance-track.shared"]);

        app.service("invoiceService", Invoicing.invoiceServiceFactory);
        app.controller("billProjectController", Invoicing.billProjectController);
        app.controller("invoiceDetailsController", Invoicing.invoiceDetailsController);

        app.config(function ($stateProvider) {
            $stateProvider.state(Invoicing.routes.billProject, {
                url: "/bill-project/{projectId:int}",
                templateUrl: urls.templates.billProject,
                controller: "billProjectController"
            }).state(Invoicing.routes.invoiceDetails, {
                url: "/invoices/{invoiceNum}",
                templateUrl: urls.templates.invoiceDetails,
                controller: "invoiceDetailsController"
            }).state("invoices", {
                abstract: true,
                url: "/invoices/",
                templateUrl: urls.templates.invoiceBase,
                controller: "invoiceBaseController"
            }).state("invoices.pending", {
                url: "/pending",
                templateUrl: urls.templates.trackMyTime,
                controller: "myTimeController"
            }).state("invoices.archive", {
                url: "/archive",
                templateUrl: urls.templates.usersTime,
                controller: "usersTimeController"
            });
        });

        Invoicing.routes = {
            invoiceDetails: "invoiceDetails",
            billProject: "billProject"
        };
    })(LanceTrack.Invoicing || (LanceTrack.Invoicing = {}));
    var Invoicing = LanceTrack.Invoicing;
})(LanceTrack || (LanceTrack = {}));
//# sourceMappingURL=module.js.map
