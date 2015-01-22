var LanceTrack;
(function (LanceTrack) {
    var Authorization;
    (function (Authorization) {
        function authorizationHttpInterceptor($q, $injector) {
            return {
                responseError: function (rejection) {
                    if (rejection.status == 401) {
                        var $state = $injector.get("$state");
                        if ($state.current.name != Authorization.routes.login)
                            $state.go(Authorization.routes.login);
                    }
                    return $q.reject(rejection);
                }
            };
        }
        Authorization.authorizationHttpInterceptor = authorizationHttpInterceptor;
    })(Authorization = LanceTrack.Authorization || (LanceTrack.Authorization = {}));
})(LanceTrack || (LanceTrack = {}));
LanceTrack.Authorization.authorizationHttpInterceptor.$inject = ["$q", "$injector"];
//# sourceMappingURL=authorizationHttpInterceptor.js.map