var LanceTrack;
(function (LanceTrack) {
    (function (Invoicing) {
        function invoiceDetailsController($scope, invoiceService, $state, $stateParams, dates) {
            $scope.dates = dates;

            invoiceService.details($stateParams.invoiceNum).then(function (invoice) {
                return $scope.model = invoice;
            });

            $scope.user = function (id) {
                if (!$scope.model)
                    return null;

                return _.find($scope.model.users, function (u) {
                    return u.id == id;
                });
            };
        }
        Invoicing.invoiceDetailsController = invoiceDetailsController;
    })(LanceTrack.Invoicing || (LanceTrack.Invoicing = {}));
    var Invoicing = LanceTrack.Invoicing;
})(LanceTrack || (LanceTrack = {}));
LanceTrack.Invoicing.invoiceDetailsController.$inject = ["$scope", "invoiceService", "$state", "$stateParams", "dates"];
//# sourceMappingURL=invoiceDetailsController.js.map
