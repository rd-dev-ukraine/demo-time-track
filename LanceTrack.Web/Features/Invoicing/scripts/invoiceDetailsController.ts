module LanceTrack {
    export module Invoicing {
        export function invoiceDetailsController(
            $scope: InvoiceDetailsScope,
            invoiceService: InvoiceService,
            $state: ng.ui.IStateService,
            $stateParams: { invoiceNum: string },
            dates: Dates) {

            $scope.dates = dates;

            $scope.isLoading = true;
            invoiceService.details($stateParams.invoiceNum)
                .then(invoice => {
                    $scope.model = invoice;

                    $scope.$watch("model.invoice.receivedSum", (oldVal, newVal) => {
                        if (oldVal == undefined || newVal == undefined || oldVal == newVal)
                            return;

                        $scope.isEarningsDistributing = true;
                        $scope.error = null;
                        invoiceService.distributeEarnings(
                                $scope.model.project.id,
                                $scope.model.invoice.invoiceNum,
                                $scope.model.invoice.receivedSum)
                            .then(r => $scope.model = r)
                            .catch(err => $scope.error = err)
                            .finally(() => $scope.isEarningsDistributing = false);
                    });
                })
                .finally(() => $scope.isLoading = false);

            $scope.user = (id: number) => {
                if (!$scope.model)
                    return null;

                return _.find($scope.model.users, u => u.id == id);
            };

            $scope.canDistributeEarnings = () => {
                return $scope.model && !$scope.model.invoice.isPaid;
            };

            $scope.markAsPaid = () => {
                $scope.isLoading = true;
                $scope.error = null;

                invoiceService.markInvoiceAsPaid($scope.model.invoice.projectId, $scope.model.invoice.invoiceNum)
                    .then(result => $scope.model = result)
                    .catch(err => $scope.error = err);
            };
        }

        export interface InvoiceDetailsScope extends ng.IScope {
            model: Api.InvoiceModel;
            user(id: number): Api.UserAccount;
            dates: Dates;
            isLoading: boolean;
            isEarningsDistributing: boolean;

            canDistributeEarnings(): boolean;
            error: { message: string };
            cancel(): void;
            markAsPaid(): void;
        }
    }
}

LanceTrack.Invoicing.invoiceDetailsController.$inject = ["$scope", "invoiceService", "$state", "$stateParams", "dates"];