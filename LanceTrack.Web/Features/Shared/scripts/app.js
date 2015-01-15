var LanceTrack;
(function (LanceTrack) {
    var app = angular.module("lance-track", ["ui.router", "lance-track.shared", "lance-track.track-time"]);

    app.config(function ($stateProvider, $urlRouterProvider) {
        $urlRouterProvider.otherwise("/track-time/my/");
    });

    app.run([
        "$rootScope", function ($rootScope) {
            $rootScope.urls = urls;
        }]);
})(LanceTrack || (LanceTrack = {}));

var urls;
//# sourceMappingURL=app.js.map
