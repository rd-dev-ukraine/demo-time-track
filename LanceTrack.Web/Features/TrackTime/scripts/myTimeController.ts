module LanceTrack {
    export module TrackTime {

        export function myTimeController(
            $scope: MyTimeScope,
            $state: ng.ui.IStateService,
            $stateParams: { at: string },
            trackTimeService: TrackTimeService,
            dates: LanceTrack.Dates) {
            
        }

        export interface MyTimeScope extends TrackTimeBaseScope {
        }
    }
}
LanceTrack.TrackTime.myTimeController.$inject = ["$scope", "$state", "$stateParams", "trackTimeService", "dates"];