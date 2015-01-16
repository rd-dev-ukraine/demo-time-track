module LanceTrack {
    export module TrackTime {
        export function trackTimeServiceFactory(
            $q: ng.IQService,
            $http: ng.IHttpService,
            dates: LanceTrack.Dates) {
            return new TrackTimeService($q, $http, dates);
        }

        export class TrackTimeService {
            constructor(
                private $q: ng.IQService,
                private $http: ng.IHttpService,
                private dates: LanceTrack.Dates) {
            }

            loadTimeInfo(date: any): ng.IPromise<Api.ProjectTimeInfoResult> {
                var deferred = this.$q.defer();

                date = this.dates.parse(date);
                var url = urls.data.loadProjectTime + "/" + this.dates.format(date);

                this.$http.get(url)
                    .success((result: Api.ProjectTimeInfoResult) => {
                        deferred.resolve(result);
                    })
                    .error(e => deferred.reject(e));

                return deferred.promise;
            }

            statistic(): ng.IPromise<Api.StatisticsResult> {
                var deferred = this.$q.defer();

                this.$http.get(urls.data.statistics)
                    .success(result => deferred.resolve(result))
                    .error(err => deferred.reject(err));

                return deferred.promise;
            }

            trackTime(projectId: number, userId: number, at: any, hours: number): ng.IPromise<any> {
                var deferred = this.$q.defer();

                this.$http.post(urls.data.track, {
                    projectId: projectId,
                    userId: userId,
                    at: this.dates.format(at),
                    hours: hours
                }).success(() => deferred.resolve())
                    .error((err) => deferred.reject(err));

                return deferred.promise;
            }
        }
    }
}

LanceTrack.TrackTime.trackTimeServiceFactory.$inject = ["$q", "$http", "dates"];