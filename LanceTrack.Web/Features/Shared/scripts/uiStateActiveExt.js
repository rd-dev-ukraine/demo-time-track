var LanceTrack;
(function (LanceTrack) {
    function uiStateActiveExtDirective($state) {
        return {
            link: function ($scope, element, attr) {
                var matchedState = attr.uiStateMatch;
                var targetClasses = attr.uiStateActiveExt;
                function changeAppearance(currentState) {
                    var matches = false;
                    var states = matchedState.split(" ");
                    for (var i = 0; i < states.length; i++) {
                        matches = matches || (currentState.name || "").indexOf(states[i]) === 0;
                    }
                    $(element).toggleClass(targetClasses, matches);
                }
                $scope.$on("$stateChangeStart", function (event, toState, toParams, fromState, fromParams) {
                    changeAppearance(toState);
                });
            },
            restrict: "A"
        };
    }
    LanceTrack.uiStateActiveExtDirective = uiStateActiveExtDirective;
})(LanceTrack || (LanceTrack = {}));
LanceTrack.uiStateActiveExtDirective.$inject = ["$state"];
//# sourceMappingURL=uiStateActiveExt.js.map