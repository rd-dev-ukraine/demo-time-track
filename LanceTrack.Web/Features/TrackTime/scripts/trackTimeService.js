var LanceTrack;
(function (LanceTrack) {
    (function (TrackTime) {
        function trackTimeServiceFactory($q, $http) {
            return new TrackTimeService($q, $http);
        }
        TrackTime.trackTimeServiceFactory = trackTimeServiceFactory;

        var TrackTimeService = (function () {
            function TrackTimeService($q, $http) {
                this.$q = $q;
                this.$http = $http;
            }
            TrackTimeService.prototype.load = function (startDate, endDate) {
                var deferred = this.$q.defer();

                var url = urls.data.trackTime + "/" + startDate + "/" + endDate;

                this.$http.get(url).success(function (result) {
                    return deferred.resolve(result);
                }).error(function (e) {
                    return deferred.reject(e);
                });

                return deferred.promise;
            };
            return TrackTimeService;
        })();
        TrackTime.TrackTimeService = TrackTimeService;
    })(LanceTrack.TrackTime || (LanceTrack.TrackTime = {}));
    var TrackTime = LanceTrack.TrackTime;
})(LanceTrack || (LanceTrack = {}));
LanceTrack.TrackTime.trackTimeServiceFactory.$inject = ["$q", "$http"];
//# sourceMappingURL=trackTimeService.js.map
