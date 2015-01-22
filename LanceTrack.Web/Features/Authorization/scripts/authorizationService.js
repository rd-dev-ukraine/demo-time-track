var LanceTrack;
(function (LanceTrack) {
    var Authorization;
    (function (Authorization) {
        function authorizationServiceFactory($q, $http) {
            return new AuthorizationService($q, $http);
        }
        Authorization.authorizationServiceFactory = authorizationServiceFactory;
        var AuthorizationService = (function () {
            function AuthorizationService($q, $http) {
                this.$q = $q;
                this.$http = $http;
            }
            AuthorizationService.prototype.login = function (loginData) {
                var deferred = this.$q.defer();
                this.$http.post(urls.data.login, loginData).success(function (r) { return deferred.resolve(r); }).error(function (err) { return deferred.reject(err); });
                return deferred.promise;
            };
            return AuthorizationService;
        })();
        Authorization.AuthorizationService = AuthorizationService;
    })(Authorization = LanceTrack.Authorization || (LanceTrack.Authorization = {}));
})(LanceTrack || (LanceTrack = {}));
LanceTrack.Authorization.authorizationServiceFactory.$inject = ["$q", "$http"];
//# sourceMappingURL=authorizationService.js.map