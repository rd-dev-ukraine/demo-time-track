var LanceTrack;
(function (LanceTrack) {
    var Invoicing;
    (function (Invoicing) {
        function billProjectController($scope, invoiceService, $state, $stateParams) {
            $scope.user = function (id) {
                if (!$scope.data)
                    return null;
                return _.find($scope.data.users, function (u) { return u.id == id; });
            };
            $scope.projectId = $stateParams.projectId;
            invoiceService.prepareInvoice($scope.projectId).then(function (r) { return $scope.data = r; });
            $scope.bill = function () {
                $scope.error = null;
                invoiceService.bill($scope.data.project.id, $scope.data.invoice).then(function (r) {
                    $state.go(Invoicing.routes.invoiceDetails, { invoiceNum: r });
                }).catch(function (err) { return $scope.error = err; });
            };
            $scope.isBillable = function () { return !!$scope.data && !!$scope.data.invoice.length; };
            $scope.totalHours = function () {
                if (!$scope.data)
                    return null;
                return _.reduce($scope.data.invoice, function (acc, i) { return ((+i.billingHours) + acc); }, 0);
            };
            $scope.totalSum = function () {
                if (!$scope.data)
                    return null;
                return _.reduce($scope.data.invoice, function (acc, i) { return ((+i.sum) + acc); }, 0);
            };
            $scope.$watch("data.invoice", function (o, n) {
                if (o === undefined || o == n)
                    return;
                $scope.error = null;
                invoiceService.recalculateInvoice($scope.data.project.id, $scope.data.invoice).then(function (r) { return $scope.data.invoice = r; });
            }, true);
        }
        Invoicing.billProjectController = billProjectController;
    })(Invoicing = LanceTrack.Invoicing || (LanceTrack.Invoicing = {}));
})(LanceTrack || (LanceTrack = {}));
LanceTrack.Invoicing.billProjectController.$inject = ["$scope", "invoiceService", "$state", "$stateParams"];
//# sourceMappingURL=billProjectController.js.map