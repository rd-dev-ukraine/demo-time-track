var LanceTrack;
(function (LanceTrack) {
    var app = angular.module("lance-track", ["ui.router", "lance-track.shared", "lance-track.track-time"]);

    app.config(function ($stateProvider, $urlRouterProvider) {
        var dates = new LanceTrack.Dates();

        $urlRouterProvider.otherwise("/track-time/" + dates.format(dates.now()) + "/my/");
    });

    app.run([
        "$rootScope", function ($rootScope) {
            $rootScope.urls = urls;
        }]);
})(LanceTrack || (LanceTrack = {}));

var urls;
//# sourceMappingURL=app.js.map
