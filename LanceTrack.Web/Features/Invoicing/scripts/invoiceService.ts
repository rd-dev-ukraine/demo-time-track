module LanceTrack {
    export module Invoicing {
        export function invoiceServiceFactory($q: ng.IQService, $http: ng.IHttpService) {
            return new InvoiceService($q, $http);
        }

        export class InvoiceService {
            constructor(
                private $q: ng.IQService,
                private $http: ng.IHttpService) {
            }

            prepareInvoice(projectId: number): ng.IPromise<Api.PrepareInvoiceModel> {
                var deferred = this.$q.defer();

                this.$http.get(urls.data.prepareInvoice + "/" + projectId)
                    .success(r => deferred.resolve(r))
                    .error(err => deferred.reject(err));

                return deferred.promise;
            }

            recalculateInvoice(projectId: number, invoiceDetails: Api.InvoiceRecalculationResult[]): ng.IPromise<Api.InvoiceRecalculationResult[]> {
                var deferred = this.$q.defer();

                this.$http.post(urls.data.recalculateInvoice, <Api.PrepareInvoiceParams>{
                    projectId: projectId,
                    invoiceUserRequests: _.map(invoiceDetails, d => <Api.InvoiceUserRequest>{ userId: d.userId, hours: d.billingHours })
                })
                    .success(r => deferred.resolve(r))
                    .error(err => deferred.reject(err));

                return deferred.promise;
            }

            bill(projectId: number, invoiceDetails: Api.InvoiceRecalculationResult[]): ng.IPromise<string> {
                var deferred = this.$q.defer();

                this.$http.post(urls.data.bill, <Api.PrepareInvoiceParams>{
                    projectId: projectId,
                    invoiceUserRequests: _.map(invoiceDetails, d => <Api.InvoiceUserRequest>{ userId: d.userId, hours: d.billingHours })
                })
                    .success(r => deferred.resolve(r))
                    .error(err => deferred.reject(err));

                return deferred.promise;
            }

            distributeEarnings(projectId: number, invoiceNum: string, earningsSum: number): ng.IPromise<Api.InvoiceModel> {
                var deferred = this.$q.defer();

                this.$http.post(urls.data.distributeInvoiceEarnings, <Api.DistributeInvoiceEarningsParam>{
                    earningsSum: earningsSum,
                    invoiceNum: invoiceNum,
                    projectId: projectId
                })
                    .success(r => deferred.resolve(r))
                    .error(err => deferred.reject(err));

                return deferred.promise;
            }

            details(invoiceNum: string): ng.IPromise<Api.InvoiceModel> {
                var deferred = this.$q.defer();

                this.$http.get(urls.data.invoiceDetails + "/" + invoiceNum)
                    .success(r => deferred.resolve(r))
                    .error(err => deferred.reject(err));

                return deferred.promise;
            }

            cancelInvoice(projectId: number, invoiceNum: string): ng.IPromise<Api.InvoiceModel> {
                var deferred = this.$q.defer();

                this.$http.post(urls.data.cancelInvoice, { projectId: projectId, invoiceNum: invoiceNum })
                    .success(r => deferred.resolve(r))
                    .error(err => deferred.reject(err));

                return deferred.promise;
            }

            markInvoiceAsPaid(projectId: number, invoiceNum: string): ng.IPromise<Api.InvoiceModel> {
                var deferred = this.$q.defer();

                this.$http.post(urls.data.markInvoiceAsPaid, { projectId: projectId, invoiceNum: invoiceNum })
                    .success(r => deferred.resolve(r))
                    .error(err => deferred.reject(err));

                return deferred.promise;
            }

            pendingInvoices(): ng.IPromise<Api.Invoice[]> {
                var deferred = this.$q.defer();

                this.$http.get(urls.data.pendingInvoices)
                    .success(r => deferred.resolve(r))
                    .error(err => deferred.reject(err));

                return deferred.promise;
            }

            archiveInvoices(): ng.IPromise<Api.Invoice[]> {
                var deferred = this.$q.defer();

                this.$http.get(urls.data.archiveInvoices)
                    .success(r => deferred.resolve(r))
                    .error(err => deferred.reject(err));

                return deferred.promise;
            }
        }
    }
}
LanceTrack.Invoicing.invoiceServiceFactory.$inject = ["$q", "$http"];