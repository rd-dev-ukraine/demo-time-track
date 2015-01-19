module LanceTrack {
    export function currencyFilter() {
        return value => {
            if (!value)
                return "-";
            return "$" + value;
        }
    }
}