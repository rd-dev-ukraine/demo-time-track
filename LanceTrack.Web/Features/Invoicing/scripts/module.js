var LanceTrack;
(function (LanceTrack) {
    var Invoicing;
    (function (Invoicing) {
        var app = angular.module("lance-track.invoicing", ["ui.router", "lance-track.shared"]);
        app.service("invoiceService", Invoicing.invoiceServiceFactory);
        app.controller("billProjectController", Invoicing.billProjectController);
        app.controller("invoiceDetailsController", Invoicing.invoiceDetailsController);
        app.controller("invoiceListController", Invoicing.invoiceListController);
        app.config(function ($stateProvider) {
            $stateProvider.state(Invoicing.routes.billProject, {
                url: "/bill-project/{projectId:int}",
                templateUrl: urls.templates.billProject,
                controller: "billProjectController"
            }).state(Invoicing.routes.invoiceDetails, {
                url: "/invoice/{invoiceNum}",
                templateUrl: urls.templates.invoiceDetails,
                controller: "invoiceDetailsController"
            }).state(Invoicing.routes.invoiceList, {
                url: "/invoices/{mode}",
                templateUrl: urls.templates.invoiceList,
                controller: "invoiceListController"
            });
        });
        Invoicing.routes = {
            invoiceDetails: "invoiceDetails",
            billProject: "billProject",
            invoiceList: "invoiceList"
        };
    })(Invoicing = LanceTrack.Invoicing || (LanceTrack.Invoicing = {}));
})(LanceTrack || (LanceTrack = {}));
//# sourceMappingURL=module.js.map