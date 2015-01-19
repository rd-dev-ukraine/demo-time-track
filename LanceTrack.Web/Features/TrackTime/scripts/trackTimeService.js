var LanceTrack;
(function (LanceTrack) {
    var TrackTime;
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
            TrackTimeService.prototype.loadTimeInfo = function (date) {
                var deferred = this.$q.defer();
                date = this.dates.parse(date);
                var url = urls.data.loadProjectTime + "/" + this.dates.format(date);
                this.$http.get(url).success(function (result) {
                    deferred.resolve(result);
                }).error(function (e) { return deferred.reject(e); });
                return deferred.promise;
            };
            TrackTimeService.prototype.statistic = function () {
                var deferred = this.$q.defer();
                this.$http.get(urls.data.statistics).success(function (result) { return deferred.resolve(result); }).error(function (err) { return deferred.reject(err); });
                return deferred.promise;
            };
            TrackTimeService.prototype.trackTime = function (projectId, userId, at, hours) {
                var deferred = this.$q.defer();
                this.$http.post(urls.data.track, {
                    projectId: projectId,
                    userId: userId,
                    at: this.dates.format(at),
                    hours: hours
                }).success(function () { return deferred.resolve(); }).error(function (err) { return deferred.reject(err); });
                return deferred.promise;
            };
            return TrackTimeService;
        })();
        TrackTime.TrackTimeService = TrackTimeService;
    })(TrackTime = LanceTrack.TrackTime || (LanceTrack.TrackTime = {}));
})(LanceTrack || (LanceTrack = {}));
LanceTrack.TrackTime.trackTimeServiceFactory.$inject = ["$q", "$http", "dates"];
//# sourceMappingURL=trackTimeService.js.map