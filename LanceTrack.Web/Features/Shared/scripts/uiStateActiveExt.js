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
var LanceTrack;
(function (LanceTrack) {
    function uiStateActiveExtDirective($state) {
        return {
            link: function ($scope, element, attr) {
                var options = parseOptions(attr.uiStateActiveExt);
                function changeAppearance(currentState, stateParameters) {
                    var isMatched = false;
                    for (var stateName in options.states) {
                        var stateOptions = options.states[stateName];
                        if ((currentState.name || "").indexOf(stateName) === 0) {
                            // Falsish state parameters always matches
                            if (!stateOptions)
                                isMatched = true;
                            else {
                                var opts = angular.isArray(stateOptions) ? stateOptions : [stateOptions];
                                if (isParameterMatched(opts, stateParameters))
                                    isMatched = true;
                            }
                        }
                    }
                    $(element).toggleClass(options.classes, isMatched);
                }
                $scope.$on("$stateChangeSuccess", function (event, toState, toParams, fromState, fromParams) {
                    changeAppearance(toState, toParams);
                });
            },
            restrict: "A"
        };
    }
    LanceTrack.uiStateActiveExtDirective = uiStateActiveExtDirective;
    function parseOptions(optionsStr) {
        var exp = "function f() { return { " + optionsStr + " }; }; f();";
        return eval(exp);
    }
    function isParameterMatched(stateMatchOptions, stateParams) {
        var matchFound = false;
        angular.forEach(stateMatchOptions, function (match) {
            angular.forEach(match, function (paramValue, paramName) {
                if ((!stateParams[paramName] && !paramValue) || stateParams[paramName] == paramValue)
                    matchFound = true;
            });
        });
        return matchFound;
    }
})(LanceTrack || (LanceTrack = {}));
LanceTrack.uiStateActiveExtDirective.$inject = ["$state"];
//# sourceMappingURL=uiStateActiveExt.js.map