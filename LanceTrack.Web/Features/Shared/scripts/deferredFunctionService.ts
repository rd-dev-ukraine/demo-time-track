module LanceTrack {
    export function deferredFunctionServiceFactory() {
        return new DeferredFunctionService();
    }

    export class DeferredFunctionService {
        decorate<T>(fn: () => ng.IPromise<T>): DeferredDecoratedFunction<T> {

            var code: any = () => {
                code.value = null;
                code.error = null;
                code.isError = false;

                code.isLoading = true;
                return fn().then(v => code.value = v)
                    .catch(err => {
                        code.error = err;
                        code.isError = true;
                    })
                    .finally(() => code.isLoading = false);
            };

            code.isLoading = false;

            return code;
        }
    }

    export interface DeferredDecoratedFunction<T> {
        isLoading: boolean;
        isError: boolean;
        error: any;
        value: T;

        (): ng.IPromise<T>;
    }
}