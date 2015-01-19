module LanceTrack {
    export function yesNoFilter() {
        return value => {
            return value ? "Yes" : "No";
        }
    }
}