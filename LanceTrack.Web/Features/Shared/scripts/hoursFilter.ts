module LanceTrack {
    export function hoursFilter() {
        return value => {
            if (!value)
                return "-";
            return value + " hrs";
        }
    }
}