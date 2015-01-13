//module LanceTrack {
//    export module TrackTime {
//        export function trackTimeServiceFactory($q: ng.IQService, $http: ng.IHttpService, dates: LanceTrack.Dates) {
//            return new TrackTimeService($q, $http, dates);
//        }
//        export class TrackTimeService {
//            constructor(
//                private $q: ng.IQService,
//                private $http: ng.IHttpService,
//                private dates: LanceTrack.Dates) {
//            }
//            load(date: any): ng.IPromise<Api.ProjectTimeInfoResult> {
//                var deferred = this.$q.defer();
//                date = this.dates.parse(date);
//                var url = urls.data.loadProjectTime + "/" + this.dates.format(date);
//                this.$http.get(url)
//                    .success((result: Api.ProjectTimeInfoResult) => {
//                        result.projects = this.addEmptyTimeSlots(result.projects, this.dates.parse(result.startDate), this.dates.parse(result.endDate));
//                        deferred.resolve(result);
//                    })
//                    .error(e => deferred.reject(e));
//                return deferred.promise;
//            }
//            statistic(): ng.IPromise<Api.StatisticsResult> {
//                var deferred = this.$q.defer();
//                this.$http.get(urls.data.statistics)
//                    .success(result => deferred.resolve(result))
//                    .error(err => deferred.reject(err));
//                return deferred.promise;
//            }
//            trackTime(projectId: number, at: any, hours: number): ng.IPromise<any> {
//                var deferred = this.$q.defer();
//                this.$http.post(urls.data.track, {
//                    projectId: projectId,
//                    at: this.dates.format(at),
//                    hours: hours
//                }).success(() => deferred.resolve())
//                    .error((err) => deferred.reject(err));
//                return deferred.promise;
//            }
//            recalculateAll(): ng.IPromise<any> {
//                var deferred = this.$q.defer();
//                this.$http.post(urls.data.recalculate, {})
//                    .success(() => deferred.resolve())
//                    .error(err => deferred.reject(err));
//                return deferred.promise;
//            }
//            private addEmptyTimeSlots(data: Api.ProjectTimeInfo[], startDate: any, endDate: any): Api.ProjectTimeInfo[] {
//                var range = this.dates.allDateInRange(startDate, endDate);
//                return _.chain(data).map((project: Api.ProjectTimeInfo) => {
//                    var time = project.time;
//                    project.time = _(range).map((date: Date) => {
//                        var existingTime = _.find(time, (rec: Api.TimeRecord) => this.dates.eq(rec.date, date));
//                        if (existingTime) {
//                            if (existingTime.hours == 0)
//                                existingTime.hours = null;
//                            return existingTime;
//                        }
//                        return {
//                            hours: null,
//                            date: this.dates.format(date)
//                        };
//                    });
//                    return project;
//                }).value();
//            }
//        }
//    }
//}
//LanceTrack.TrackTime.trackTimeServiceFactory.$inject = ["$q", "$http", "dates"];
//# sourceMappingURL=trackTimeService.js.map
