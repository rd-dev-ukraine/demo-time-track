module LanceTrack {
    export module TrackTime {
        export function trackTimeServiceFactory($q: ng.IQService, $http: ng.IHttpService, dates: LanceTrack.Dates) {
            return new TrackTimeService($q, $http, dates);
        }

        export class TrackTimeService {
            constructor(
                private $q: ng.IQService,
                private $http: ng.IHttpService,
                private dates: LanceTrack.Dates) {
            }

            load(startDate: string, endDate: string): ng.IPromise<ProjectTimeInfo[]> {
                var range = this.dates.getValidRange(startDate, endDate);

                var deferred = this.$q.defer();

                var url = urls.data.loadProjectTime + "/" + this.dates.format(range.start) + "/" + this.dates.format(range.end);

                this.$http.get(url)
                    .success((result: ProjectTimeInfo[]) => {
                        var model = this.createModel(result, range.start, range.end);

                        deferred.resolve(model);
                    })
                    .error(e => deferred.reject(e));

                return deferred.promise;
            }

            trackTime(projectId: number, at: any, hours: number): ng.IPromise<any> {
                var deferred = this.$q.defer();

                this.$http.post(urls.data.track, {
                    projectId: projectId,
                    at: this.dates.format(at),
                    hours: hours
                }).success(() => deferred.resolve())
                    .error((err) => deferred.reject(err));

                return deferred.promise;
            }

            recalculateAll(): ng.IPromise<any> {
                var deferred = this.$q.defer();

                this.$http.post(urls.data.recalculate, {})
                    .success(() => deferred.resolve())
                    .error(err => deferred.reject(err));

                return deferred.promise;
            }

            private createModel(data: ProjectTimeInfo[], startDate: any, endDate: any): ProjectTimeInfo[] {
                var range = this.dates.allDateInRange(startDate, endDate);

                return _.chain(data).map((project: ProjectTimeInfo) => {
                    var time = project.time;

                    project.time = _(range).map((date: Date) => {
                        var existingTime = _.find(time, (rec: TimeRecord) => this.dates.eq(rec.date, date));
                        if (existingTime)
                            return existingTime;

                        return {
                            hours: null,
                            date: this.dates.format(date)
                        };
                    });


                    return project;
                }).value();
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
LanceTrack.TrackTime.trackTimeServiceFactory.$inject = ["$q", "$http", "dates"];