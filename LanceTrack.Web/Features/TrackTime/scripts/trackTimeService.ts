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

            loadMyTime(date: any): ng.IPromise<Api.ProjectTimeInfoResult> {
                var deferred = this.$q.defer();

                this.loadTimeInfo(date)
                    .then((result: Api.ProjectTimeInfoResult) => {
                        result.time = _.filter(result.time, time => time.userId == result.currentUserId);
                        deferred.resolve(result);
                    })
                    .catch(err => deferred.reject(err));

                return deferred.promise;
            }

            loadTimeInfo(date: any): ng.IPromise<Api.ProjectTimeInfoResult> {
                var deferred = this.$q.defer();

                date = this.dates.parse(date);
                var url = urls.data.loadProjectTime + "/" + this.dates.format(date);

                this.$http.get(url)
                    .success((result: Api.ProjectTimeInfoResult) => {
                        this.addEmptyTimeSlots(result);
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

            recalculateAll(): ng.IPromise<any> {
                var deferred = this.$q.defer();

                this.$http.post(urls.data.recalculate, {})
                    .success(() => deferred.resolve())
                    .error(err => deferred.reject(err));

                return deferred.promise;
            }

            private addEmptyTimeSlots(info: Api.ProjectTimeInfoResult): void {
                var dateRange = this.dates.allDateInRange(info.startDate, info.endDate);

                _.forEach(info.projects, (project: Api.Project) => {
                    _.forEach(dateRange, (date: Date) => {
                        _.forEach(info.users, (user: Api.UserAccount) => {
                            var time = _.find(info.time, (timeRecord: Api.ProjectDailyTime) => {
                                return timeRecord.userId == user.id &&
                                    timeRecord.projectId == project.id &&
                                    this.dates.eq(timeRecord.date, date);
                            });

                            if (!time) {
                                info.time.push({
                                    date: this.dates.format(date),
                                    projectId: project.id,
                                    totalHours: null,
                                    userId: user.id
                                });
                            }
                        });
                    });
                });
            }
        }
    }
}
LanceTrack.TrackTime.trackTimeServiceFactory.$inject = ["$q", "$http", "dates"];