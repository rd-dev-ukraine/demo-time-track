module LanceTrack {
    export module Authorization {
        export function authorizationHttpInterceptor($q: ng.IQService, $injector) {

            return {
                responseError: (rejection: JQueryXHR) => {
                     
                    if (rejection.status == 401) {

                        var $state: ng.ui.IStateService = $injector.get("$state");

                        if ($state.current.name != routes.login)
                            $state.go(routes.login);
                    }

                    return $q.reject(rejection);
                }
            };
        }
    }
}

LanceTrack.Authorization.authorizationHttpInterceptor.$inject = ["$q", "$injector"];