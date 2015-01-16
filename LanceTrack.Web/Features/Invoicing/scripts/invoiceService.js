var LanceTrack;
(function (LanceTrack) {
    (function (Invoicing) {
        function invoiceServiceFactory($q, $http) {
            return new InvoiceService($q, $http);
        }
        Invoicing.invoiceServiceFactory = invoiceServiceFactory;

        var InvoiceService = (function () {
            function InvoiceService($q, $http) {
                this.$q = $q;
                this.$http = $http;
            }
            InvoiceService.prototype.prepareInvoice = function (projectId) {
                var deferred = this.$q.defer();

                this.$http.get(urls.data.prepareInvoice + "/" + projectId).success(function (r) {
                    return deferred.resolve(r);
                }).error(function (err) {
                    return deferred.reject(err);
                });

                return deferred.promise;
            };

            InvoiceService.prototype.recalculateInvoice = function (projectId, invoiceDetails) {
                var deferred = this.$q.defer();

                this.$http.post(urls.data.recalculateInvoice, {
                    projectId: projectId,
                    invoiceUserRequests: _.map(invoiceDetails, function (d) {
                        return { userId: d.userId, hours: d.billingHours };
                    })
                }).success(function (r) {
                    return deferred.resolve(r);
                }).error(function (err) {
                    return deferred.reject(err);
                });

                return deferred.promise;
            };
            return InvoiceService;
        })();
        Invoicing.InvoiceService = InvoiceService;
    })(LanceTrack.Invoicing || (LanceTrack.Invoicing = {}));
    var Invoicing = LanceTrack.Invoicing;
})(LanceTrack || (LanceTrack = {}));
LanceTrack.Invoicing.invoiceServiceFactory.$inject = ["$q", "$http"];
//# sourceMappingURL=invoiceService.js.map
