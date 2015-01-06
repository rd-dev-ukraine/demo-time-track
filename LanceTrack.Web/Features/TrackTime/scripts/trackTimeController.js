var LanceTrack;
(function (LanceTrack) {
    (function (TrackTime) {
        function trackTimeController($scope, trackTimeService) {
            function reload() {
                trackTimeService.load($scope.startDate, $scope.endDate).then(function (r) {
                    $scope.projectTime = r;
                });
            }

            $scope.startDate = moment().startOf("week").format(LanceTrack.DateFormat);
            $scope.endDate = moment().endOf("week").format(LanceTrack.DateFormat);

            reload();

            $scope.$watch("startDate", reload);
            $scope.$watch("endDate", reload);
        }
        TrackTime.trackTimeController = trackTimeController;
    })(LanceTrack.TrackTime || (LanceTrack.TrackTime = {}));
    var TrackTime = LanceTrack.TrackTime;
})(LanceTrack || (LanceTrack = {}));
LanceTrack.TrackTime.trackTimeController.$inject = ["$scope", "trackTimeService"];
//# sourceMappingURL=trackTimeController.js.map
