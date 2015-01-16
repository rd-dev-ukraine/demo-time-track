module LanceTrack {
    export module Invoicing {

        export function billProjectController(
            $scope: BillingScope,
            invoiceService: InvoiceService,
            $state: ng.ui.IStateService,
            $stateParams: { projectId: number }
            ) {

        }

        export interface BillingScope extends ng.IScope {
            data: Api.InvoiceModel;
        }
    }
} 
LanceTrack.Invoicing.billProjectController.$inject = ["$scope", "invoiceService", "$state", "$stateParams"];