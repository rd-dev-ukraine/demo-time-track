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
                var deferred = this.$q.defer();

                var url = urls.data.trackTime + "/" + startDate + "/" + endDate;

                this.$http.get(url)
                    .success((result: ProjectTimeInfo[]) => {
                        var model = this.createModel(result, startDate, endDate);

                        deferred.resolve(model);
                    })
                    .error(e => deferred.reject(e));

                return deferred.promise;
            }

            private createModel(data: ProjectTimeInfo[], startDate: string, endDate: string): ProjectTimeInfo[]{
                var range = this.dates.dateRange(startDate, endDate);

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