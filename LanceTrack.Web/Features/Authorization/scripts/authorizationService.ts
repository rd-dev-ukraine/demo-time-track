module LanceTrack {
    export module Authorization {
        export function authorizationServiceFactory($q: ng.IQService, $http: ng.IHttpService) {
            return new AuthorizationService($q, $http);
        }

        export class AuthorizationService {
            constructor(
                private $q: ng.IQService,
                private $http: ng.IHttpService) {

            }

            login(loginData: Api.LoginModel): ng.IPromise<any> {
                var deferred = this.$q.defer();

                this.$http.post(urls.data.login, loginData)
                    .success(r => deferred.resolve(r))
                    .error(err => deferred.reject(err));

                return deferred.promise;
            }

            logout(): ng.IPromise<any> {
                var deferred = this.$q.defer();

                this.$http.post(urls.data.logout, {})
                    .success(r => deferred.resolve(r))
                    .error(err => deferred.reject(err));

                return deferred.promise;
            }

            currentUser(): ng.IPromise<Api.UserAccount> {
                var deferred = this.$q.defer();

                this.$http.get(urls.data.currentUser)
                    .success(r => deferred.resolve(r))
                    .error(err => deferred.reject(err));

                return deferred.promise;
            }
        }
    }
}
LanceTrack.Authorization.authorizationServiceFactory.$inject = ["$q", "$http"];