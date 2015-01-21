module LanceTrack {
    export function uiStateActiveExtDirective($state: ng.ui.IStateService) {
        return <ng.IDirective> {
            link: ($scope: ng.IScope, element, attr) => {

                var matchedState = attr.uiStateMatch;
                var targetClasses = attr.uiStateActiveExt;

                function changeAppearance(currentState: ng.ui.IState) {
                    var matches = false;
                    var states = matchedState.split(" ");

                    for (var i = 0; i < states.length; i++) {
                        matches = matches ||
                                (currentState.name || "").indexOf(states[i]) === 0;
                    }

                    $(element).toggleClass(targetClasses, matches);
                }

                $scope.$on(
                    "$stateChangeStart",
                    (event, toState, toParams, fromState, fromParams) => {
                        changeAppearance(toState);
                    });
            },
            restrict: "A"
        };
    }
}

LanceTrack.uiStateActiveExtDirective.$inject = ["$state"];