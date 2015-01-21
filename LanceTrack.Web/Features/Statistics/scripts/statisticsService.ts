module LanceTrack {
    export module Statistics {
        export function statisticsServiceFactory(
            $q: ng.IQService,
            $http: ng.IHttpService,
            dates: LanceTrack.Dates) {
            return new StatisticsService($q, $http, dates);
        }

        export class StatisticsService {
            constructor(
                private $q: ng.IQService,
                private $http: ng.IHttpService,
                private dates: LanceTrack.Dates) {
            }

            statistic(): ng.IPromise<Api.StatisticsResult> {
                var deferred = this.$q.defer();

                this.$http.get(urls.data.statistics)
                    .success(result => deferred.resolve(result))
                    .error(err => deferred.reject(err));

                return deferred.promise;
            }
        }
    }
}

LanceTrack.Statistics.statisticsServiceFactory.$inject = ["$q", "$http", "dates"];