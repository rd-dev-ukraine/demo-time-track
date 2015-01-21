var LanceTrack;
(function (LanceTrack) {
    var Statistics;
    (function (Statistics) {
        function statisticsServiceFactory($q, $http, dates) {
            return new StatisticsService($q, $http, dates);
        }
        Statistics.statisticsServiceFactory = statisticsServiceFactory;
        var StatisticsService = (function () {
            function StatisticsService($q, $http, dates) {
                this.$q = $q;
                this.$http = $http;
                this.dates = dates;
            }
            StatisticsService.prototype.statistic = function () {
                var deferred = this.$q.defer();
                this.$http.get(urls.data.statistics).success(function (result) { return deferred.resolve(result); }).error(function (err) { return deferred.reject(err); });
                return deferred.promise;
            };
            return StatisticsService;
        })();
        Statistics.StatisticsService = StatisticsService;
    })(Statistics = LanceTrack.Statistics || (LanceTrack.Statistics = {}));
})(LanceTrack || (LanceTrack = {}));
LanceTrack.Statistics.statisticsServiceFactory.$inject = ["$q", "$http", "dates"];
//# sourceMappingURL=statisticsService.js.map