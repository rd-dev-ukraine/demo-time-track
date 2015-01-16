var LanceTrack;
(function (LanceTrack) {
    (function (Invoicing) {
        function billProjectController($scope, invoiceService, $state, $stateParams) {
            $scope.projectId = $stateParams.projectId;
            invoiceService.prepareInvoice($scope.projectId).then(function (r) {
                return $scope.data = r;
            });
        }
        Invoicing.billProjectController = billProjectController;
    })(LanceTrack.Invoicing || (LanceTrack.Invoicing = {}));
    var Invoicing = LanceTrack.Invoicing;
})(LanceTrack || (LanceTrack = {}));
LanceTrack.Invoicing.billProjectController.$inject = ["$scope", "invoiceService", "$state", "$stateParams"];
//# sourceMappingURL=billProjectController.js.map
