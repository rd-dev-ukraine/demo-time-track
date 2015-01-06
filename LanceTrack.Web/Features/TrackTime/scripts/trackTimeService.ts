module LanceTrack {
    export module TrackTime {
        export function trackTimeServiceFactory($q: ng.IQService, $http: ng.IHttpService) {
            return new TrackTimeService($q, $http);
        }

        export class TrackTimeService {
            constructor(
                private $q: ng.IQService,
                private $http: ng.IHttpService) {
            }

            load(startDate: string, endDate: string): ng.IPromise<ProjectTimeInfo[]> {
                var deferred = this.$q.defer();

                var url = urls.data.trackTime + "/" + startDate + "/" + endDate;

                this.$http.get(url)
                    .success((result) => deferred.resolve(result))
                    .error(e => deferred.reject(e));

                return deferred.promise;
            }
        }

        export interface ProjectTimeInfo {
            projectId: number;
            projectTitle: string;
            time: TimeRecord[];
        }

        export interface TimeRecord {
            date: string;
            hours: number;
        }
    }
}
LanceTrack.TrackTime.trackTimeServiceFactory.$inject = ["$q", "$http"];