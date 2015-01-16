module LanceTrack {
    export module Invoicing {

        export function billProjectController(
            $scope: BillingScope,
            invoiceService: InvoiceService,
            $state: ng.ui.IStateService,
            $stateParams: { projectId: number }
            ) {
            
            $scope.projectId = $stateParams.projectId;
            invoiceService.prepareInvoice($scope.projectId)
                .then(r => $scope.data = r);
        }

        export interface BillingScope extends ng.IScope {
            projectId: number;
            data: Api.InvoiceModel;
        }
    }
} 
LanceTrack.Invoicing.billProjectController.$inject = ["$scope", "invoiceService", "$state", "$stateParams"];