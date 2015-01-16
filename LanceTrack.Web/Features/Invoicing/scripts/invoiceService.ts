module  LanceTrack {
    export module Invoicing {
        export function invoiceServiceFactory($q: ng.IQService, $http: ng.IHttpService) {
            return new InvoiceService($q, $http);
        }

        export class InvoiceService {
            constructor(
                private $q: ng.IQService,
                private $http: ng.IHttpService) {
            }

            prepareInvoice(projectId: number): ng.IPromise<Api.InvoiceModel> {
                var deferred = this.$q.defer();

                this.$http.get(urls.data.prepareInvoice + "/" + projectId)
                    .success(r => deferred.resolve(r))
                    .error(err => deferred.reject(err));

                return deferred.promise;
            }
        }
    }
} 
LanceTrack.Invoicing.invoiceServiceFactory.$inject = ["$q", "$http"];