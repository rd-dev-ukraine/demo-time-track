module LanceTrack {
    export module TrackTime {

        export function usersTimeController(
            $scope: UserTimeScope,
            $state: ng.ui.IStateService,
            $stateParams: { at: string },
            trackTimeService: TrackTimeService,
            dates: LanceTrack.Dates,
            deferredFunction: LanceTrack.DeferredFunctionService) {
        }

        export interface UserTimeScope extends TrackTimeBaseScope {
        }
    }
}
LanceTrack.TrackTime.usersTimeController.$inject = ["$scope", "$state", "$stateParams", "trackTimeService", "dates", "deferredFunction"];