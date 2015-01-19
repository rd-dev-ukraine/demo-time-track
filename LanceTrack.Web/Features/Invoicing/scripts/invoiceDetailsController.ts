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
                .then(invoice => $scope.model = invoice);

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
        }
    }
} 
LanceTrack.Invoicing.invoiceDetailsController.$inject = ["$scope", "invoiceService", "$state", "$stateParams", "dates"];