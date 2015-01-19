module LanceTrack {
    export module Invoicing {

        export function billProjectController(
            $scope: BillingScope,
            invoiceService: InvoiceService,
            $state: ng.ui.IStateService,
            $stateParams: { projectId: number }
            ) {

            $scope.user = (id) => {
                if (!$scope.data)
                    return null;

                return _.find($scope.data.users, u=> u.id == id);
            };

            $scope.projectId = $stateParams.projectId;
            invoiceService.prepareInvoice($scope.projectId)
                .then(r => $scope.data = r);

            $scope.bill = () => {
                $scope.error = null;

                invoiceService.bill($scope.data.project.id, $scope.data.invoice)
                    .then((r) => {
                        $state.go(routes.invoiceDetails, { invoiceNum: r });
                    })
                    .catch(err => $scope.error = err);
            };

            $scope.isBillable = () => !!$scope.data && !!$scope.data.invoice.length;

            $scope.totalHours = () => {
                if (!$scope.data)
                    return null;

                return <number>_.reduce($scope.data.invoice,
                    (acc: number, i: Api.InvoiceRecalculationResult) => ((+i.billingHours) + acc), 0);
            };

            $scope.totalSum = () => {
                if (!$scope.data)
                    return null;

                return <number>_.reduce($scope.data.invoice,
                    (acc: number, i: Api.InvoiceRecalculationResult) => ((+i.sum) + acc), 0);
            };

            $scope.$watch("data.invoice",(o, n) => {
                if (o === undefined || o == n)
                    return;

                $scope.error = null;

                invoiceService.recalculateInvoice($scope.data.project.id, $scope.data.invoice)
                    .then(r => $scope.data.invoice = r);
            }, true);
        }

        export interface BillingScope extends ng.IScope {
            projectId: number;
            data: Api.PrepareInvoiceModel;
            error: string;
            user(id: number): Api.UserAccount;
            totalHours(): number;
            totalSum(): number;
            isBillable(): boolean;
            bill(): void;
        }
    }
}
LanceTrack.Invoicing.billProjectController.$inject = ["$scope", "invoiceService", "$state", "$stateParams"];