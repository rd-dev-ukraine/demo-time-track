/*
 * Replacement of ui-sref-active directive.
 * Supports matching parent state, matching few states.
 * 
 * usage
 * <element ui-state-active-ext="<options>" />
 * options are json object literal without enclosing {} to simplify writing
 * <options> = 
 *     classes: "class1 class2 ... classN",      // CSS classes will be applied if states matched
 *     states: {                                 // States to match
 *        state1: {                              // Matches state with name 'state1'
 *            param1: value1                     // if parameter 'param1' has value 'value1'
 *        },
 *        state2: {},                            // Matches state with name 'state2' with any parameters
 *        state3: null,
 *        state4: '',                            // Falsish options state3 and state4 matches with any parameters   
 *        state5: [{ param1: 1}, { param1: 2}]   // State matches with two parameters set
 *     }
 *
 * don't require child elements with ui-sref directive.
 */
module LanceTrack {
    export function uiStateActiveExtDirective($state: ng.ui.IStateService) {
        return <ng.IDirective> {
            link: ($scope: ng.IScope, element, attr) => {
                var options = parseOptions(attr.uiStateActiveExt);

                function changeAppearance(currentState: ng.ui.IState, stateParameters: any) {
                    var isMatched = false;

                    for (var stateName in options.states) {
                        var stateOptions = options.states[stateName];

                        if ((currentState.name || "").indexOf(stateName) === 0) {
                            // Falsish state parameters always matches
                            if (!stateOptions)
                                isMatched = true;
                            else {
                                var opts = angular.isArray(stateOptions)
                                    ? <StateMatchOptions[]> <any> stateOptions
                                    : <StateMatchOptions[]> <any>[stateOptions];

                                if (isParameterMatched(opts, stateParameters))
                                    isMatched = true;
                            }
                        }
                    }

                    $(element).toggleClass(options.classes, isMatched);
                }

                $scope.$on(
                    "$stateChangeSuccess",
                    (event, toState, toParams, fromState, fromParams) => {
                        changeAppearance(toState, toParams);
                    });
            },
            restrict: "A"
        };
    }

    function parseOptions(optionsStr: string): UiStateActiveExtParams {
        var exp = "function f() { return { " + optionsStr + " }; }; f();";
        return eval(exp);
    }

    function isParameterMatched(stateMatchOptions: StateMatchOptions[], stateParams): boolean {
        var matchFound = false;

        angular.forEach(stateMatchOptions, (match: StateMatchOptions) => {
            angular.forEach(match, (paramValue, paramName) => {
                if ((!stateParams[paramName] && !paramValue) ||
                    stateParams[paramName] == paramValue)
                    matchFound = true;
            });
        });

        return matchFound;
    }

    export interface UiStateActiveExtParams {
        classes: string;
        states: Array<{
            [stateName: string] : any;
        }>;
    }

    export interface StateMatchOptions {
        [paramName: string] : any;
    }
}

LanceTrack.uiStateActiveExtDirective.$inject = ["$state"];