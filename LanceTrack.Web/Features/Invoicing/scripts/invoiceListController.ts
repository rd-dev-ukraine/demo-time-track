module LanceTrack {
    export module Invoicing {
        var pendingMode = "pending";

        export function invoiceListController(
            $scope: InvoiceListScope,
            invoiceService: InvoiceService,
            $state: ng.ui.IStateService,
            $stateParams: { mode: string },
            dates: Dates) {

            $scope.dates = dates;
            $scope.mode = $stateParams.mode || pendingMode;
            $scope.displayStatus = $scope.mode != pendingMode;

            function reload() {
                $scope.isLoading = true;

                ($scope.mode == pendingMode
                    ? invoiceService.pendingInvoices()
                    : invoiceService.archiveInvoices())
                    .then(data => $scope.data = data);
            }

            $scope.$watch("mode", () => {
                reload();
            });
        }

        export interface InvoiceListScope extends ng.IScope {
            data: Api.Invoice[];
            displayStatus: boolean;

            isLoading: boolean;
            mode: string;

            dates: Dates;
        }

        
    }
}

LanceTrack.Invoicing.invoiceListController.$inject = ["$scope", "invoiceService", "$state", "$stateParams", "dates"];