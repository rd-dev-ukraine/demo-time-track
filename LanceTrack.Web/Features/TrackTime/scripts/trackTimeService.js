var LanceTrack;
(function (LanceTrack) {
    (function (TrackTime) {
        function trackTimeServiceFactory($q, $http, dates) {
            return new TrackTimeService($q, $http, dates);
        }
        TrackTime.trackTimeServiceFactory = trackTimeServiceFactory;

        var TrackTimeService = (function () {
            function TrackTimeService($q, $http, dates) {
                this.$q = $q;
                this.$http = $http;
                this.dates = dates;
            }
            TrackTimeService.prototype.loadMyTime = function (date) {
                var deferred = this.$q.defer();

                this.loadTimeInfo(date).then(function (result) {
                    result.time = _.filter(result.time, function (time) {
                        return time.userId == result.currentUserId;
                    });
                    deferred.resolve(result);
                }).catch(function (err) {
                    return deferred.reject(err);
                });

                return deferred.promise;
            };

            TrackTimeService.prototype.loadTimeInfo = function (date) {
                var _this = this;
                var deferred = this.$q.defer();

                date = this.dates.parse(date);
                var url = urls.data.loadProjectTime + "/" + this.dates.format(date);

                this.$http.get(url).success(function (result) {
                    _this.addEmptyTimeSlots(result);
                    deferred.resolve(result);
                }).error(function (e) {
                    return deferred.reject(e);
                });

                return deferred.promise;
            };

            TrackTimeService.prototype.statistic = function () {
                var deferred = this.$q.defer();

                this.$http.get(urls.data.statistics).success(function (result) {
                    return deferred.resolve(result);
                }).error(function (err) {
                    return deferred.reject(err);
                });

                return deferred.promise;
            };

            TrackTimeService.prototype.trackTime = function (projectId, userId, at, hours) {
                var deferred = this.$q.defer();

                this.$http.post(urls.data.track, {
                    projectId: projectId,
                    userId: userId,
                    at: this.dates.format(at),
                    hours: hours
                }).success(function () {
                    return deferred.resolve();
                }).error(function (err) {
                    return deferred.reject(err);
                });

                return deferred.promise;
            };

            TrackTimeService.prototype.recalculateAll = function () {
                var deferred = this.$q.defer();

                this.$http.post(urls.data.recalculate, {}).success(function () {
                    return deferred.resolve();
                }).error(function (err) {
                    return deferred.reject(err);
                });

                return deferred.promise;
            };

            TrackTimeService.prototype.addEmptyTimeSlots = function (info) {
                var _this = this;
                var dateRange = this.dates.allDateInRange(info.startDate, info.endDate);

                _.forEach(info.projects, function (project) {
                    _.forEach(dateRange, function (date) {
                        _.forEach(info.users, function (user) {
                            var time = _.find(info.time, function (timeRecord) {
                                return timeRecord.userId == user.id && timeRecord.projectId == project.id && _this.dates.eq(timeRecord.date, date);
                            });

                            if (!time) {
                                info.time.push({
                                    date: _this.dates.format(date),
                                    projectId: project.id,
                                    totalHours: null,
                                    userId: user.id
                                });
                            }
                        });
                    });
                });
            };
            return TrackTimeService;
        })();
        TrackTime.TrackTimeService = TrackTimeService;
    })(LanceTrack.TrackTime || (LanceTrack.TrackTime = {}));
    var TrackTime = LanceTrack.TrackTime;
})(LanceTrack || (LanceTrack = {}));
LanceTrack.TrackTime.trackTimeServiceFactory.$inject = ["$q", "$http", "dates"];
//# sourceMappingURL=trackTimeService.js.map
