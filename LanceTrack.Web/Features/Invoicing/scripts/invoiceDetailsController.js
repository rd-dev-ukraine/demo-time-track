var LanceTrack;
(function (LanceTrack) {
    var Invoicing;
    (function (Invoicing) {
        function invoiceDetailsController($scope, invoiceService, $state, $stateParams, dates) {
            $scope.dates = dates;
            $scope.isLoading = true;
            invoiceService.details($stateParams.invoiceNum).then(function (invoice) {
                $scope.model = invoice;
                $scope.$watch("model.invoice.receivedSum", function (oldVal, newVal) {
                    if (oldVal == undefined || newVal == undefined || oldVal == newVal)
                        return;
                    $scope.isEarningsDistributing = true;
                    $scope.error = null;
                    invoiceService.distributeEarnings($scope.model.project.id, $scope.model.invoice.invoiceNum, $scope.model.invoice.receivedSum).then(function (r) { return $scope.model = r; }).catch(function (err) { return $scope.error = err; }).finally(function () { return $scope.isEarningsDistributing = false; });
                });
            }).finally(function () { return $scope.isLoading = false; });
            $scope.user = function (id) {
                if (!$scope.model)
                    return null;
                return _.find($scope.model.users, function (u) { return u.id == id; });
            };
            $scope.canDistributeEarnings = function () {
                return $scope.model && !$scope.model.invoice.isPaid && !$scope.model.invoice.isCancelled;
            };
            $scope.markAsPaid = function () {
                $scope.isLoading = true;
                $scope.error = null;
                invoiceService.markInvoiceAsPaid($scope.model.invoice.projectId, $scope.model.invoice.invoiceNum).then(function (result) { return $scope.model = result; }).catch(function (err) { return $scope.error = err; });
            };
            $scope.cancelInvoice = function () {
                $scope.isLoading = true;
                $scope.error = null;
                invoiceService.cancelInvoice($scope.model.invoice.projectId, $scope.model.invoice.invoiceNum).then(function (result) { return $scope.model = result; }).catch(function (err) { return $scope.error = err; });
            };
        }
        Invoicing.invoiceDetailsController = invoiceDetailsController;
    })(Invoicing = LanceTrack.Invoicing || (LanceTrack.Invoicing = {}));
})(LanceTrack || (LanceTrack = {}));
LanceTrack.Invoicing.invoiceDetailsController.$inject = ["$scope", "invoiceService", "$state", "$stateParams", "dates"];
//# sourceMappingURL=invoiceDetailsController.js.map