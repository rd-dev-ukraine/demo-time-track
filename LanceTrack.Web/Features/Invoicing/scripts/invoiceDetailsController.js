var LanceTrack;
(function (LanceTrack) {
    var Invoicing;
    (function (Invoicing) {
        function invoiceDetailsController($scope, invoiceService, $state, $stateParams, dates) {
            $scope.dates = dates;
            invoiceService.details($stateParams.invoiceNum).then(function (invoice) {
                $scope.model = invoice;
                $scope.$watch("model.invoice.receivedSum", function (oldVal, newVal) {
                    if (oldVal == undefined || newVal == undefined || oldVal == newVal)
                        return;
                    $scope.error = null;
                    invoiceService.distributeEarnings($scope.model.project.id, $scope.model.invoice.invoiceNum, $scope.model.invoice.receivedSum).then(function (r) { return $scope.model = r; }).catch(function (err) { return $scope.error = err; });
                });
            });
            $scope.user = function (id) {
                if (!$scope.model)
                    return null;
                return _.find($scope.model.users, function (u) { return u.id == id; });
            };
        }
        Invoicing.invoiceDetailsController = invoiceDetailsController;
    })(Invoicing = LanceTrack.Invoicing || (LanceTrack.Invoicing = {}));
})(LanceTrack || (LanceTrack = {}));
LanceTrack.Invoicing.invoiceDetailsController.$inject = ["$scope", "invoiceService", "$state", "$stateParams", "dates"];
//# sourceMappingURL=invoiceDetailsController.js.map