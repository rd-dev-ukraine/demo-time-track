var LanceTrack;
(function (LanceTrack) {
    var Invoicing;
    (function (Invoicing) {
        var pendingMode = "pending";
        function invoiceListController($scope, invoiceService, $state, $stateParams, dates) {
            $scope.dates = dates;
            $scope.mode = $stateParams.mode || pendingMode;
            $scope.displayStatus = $scope.mode != pendingMode;
            function reload() {
                $scope.isLoading = true;
                ($scope.mode == pendingMode ? invoiceService.pendingInvoices() : invoiceService.archiveInvoices()).then(function (data) { return $scope.data = data; });
            }
            $scope.$watch("mode", function () {
                reload();
            });
        }
        Invoicing.invoiceListController = invoiceListController;
    })(Invoicing = LanceTrack.Invoicing || (LanceTrack.Invoicing = {}));
})(LanceTrack || (LanceTrack = {}));
LanceTrack.Invoicing.invoiceListController.$inject = ["$scope", "invoiceService", "$state", "$stateParams", "dates"];
//# sourceMappingURL=invoiceListController.js.map