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
            TrackTimeService.prototype.load = function (date) {
                var _this = this;
                var deferred = this.$q.defer();

                date = this.dates.parse(date);
                var url = urls.data.loadProjectTime + "/" + this.dates.format(date);

                this.$http.get(url).success(function (result) {
                    var model = _this.createModel(result, _this.dates.startOfWeek(date), _this.dates.endOfWeek(date));
                    deferred.resolve(model);
                }).error(function (e) {
                    return deferred.reject(e);
                });

                return deferred.promise;
            };

            TrackTimeService.prototype.trackTime = function (projectId, at, hours) {
                var deferred = this.$q.defer();

                this.$http.post(urls.data.track, {
                    projectId: projectId,
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

            TrackTimeService.prototype.createModel = function (data, startDate, endDate) {
                var _this = this;
                var range = this.dates.allDateInRange(startDate, endDate);

                return _.chain(data).map(function (project) {
                    var time = project.time;

                    project.time = _(range).map(function (date) {
                        var existingTime = _.find(time, function (rec) {
                            return _this.dates.eq(rec.date, date);
                        });
                        if (existingTime)
                            return existingTime;

                        return {
                            hours: null,
                            date: _this.dates.format(date)
                        };
                    });

                    return project;
                }).value();
            };
            return TrackTimeService;
        })();
        TrackTime.TrackTimeService = TrackTimeService;
    })(LanceTrack.TrackTime || (LanceTrack.TrackTime = {}));
    var TrackTime = LanceTrack.TrackTime;
})(LanceTrack || (LanceTrack = {}));
LanceTrack.TrackTime.trackTimeServiceFactory.$inject = ["$q", "$http", "dates"];
//# sourceMappingURL=trackTimeService.js.map
