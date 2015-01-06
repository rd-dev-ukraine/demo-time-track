module LanceTrack {
    export function deferredFunctionServiceFactory() {
        return new DeferredFunctionService();
    }

    export class DeferredFunctionService {
        decorate<T>(fn: () => ng.IPromise<T>): DeferredDecoratedFunction<T> {

            var code: any = () => {
                code.isLoading = true;
                return fn().finally(() => code.isLoading = false);
            };

            code.isLoading = false;

            return code;
        }
    }

    export interface DeferredDecoratedFunction<T> {
        isLoading: boolean;

        (): ng.IPromise<T>;
    }
} 