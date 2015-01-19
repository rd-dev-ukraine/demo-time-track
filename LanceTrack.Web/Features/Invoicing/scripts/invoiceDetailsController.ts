module LanceTrack {
    export module Invoicing {
        export function invoiceDetailsController(
            $scope: InvoiceDetailsScope,
            invoiceService: InvoiceService,
            $state: ng.ui.IStateService,
            $stateParams: { invoiceNum: string },
            dates: Dates) {

            $scope.dates = dates;

            invoiceService.details($stateParams.invoiceNum)
                .then(invoice => {
                $scope.model = invoice;

                $scope.$watch("model.invoice.receivedSum", (oldVal, newVal) => {
                    if (oldVal == undefined || newVal == undefined || oldVal == newVal)
                        return;

                    $scope.error = null;
                    invoiceService.distributeEarnings(
                            $scope.model.project.id,
                            $scope.model.invoice.invoiceNum,
                            $scope.model.invoice.receivedSum)
                        .then(r => $scope.model = r)
                        .catch(err => $scope.error = err);
                });
            });

            $scope.user = (id: number) => {
                if (!$scope.model)
                    return null;

                return _.find($scope.model.users, u => u.id == id);
            };
        }

        export interface InvoiceDetailsScope extends ng.IScope {
            model: Api.InvoiceModel;
            user(id: number): Api.UserAccount;
            dates: Dates;
            error: { message: string };
        }
    }
}
LanceTrack.Invoicing.invoiceDetailsController.$inject = ["$scope", "invoiceService", "$state", "$stateParams", "dates"];