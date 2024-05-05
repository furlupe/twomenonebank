import { injectable } from 'inversify';
import { AsyncLocalStorage } from 'node:async_hooks';

export type Store = {
    headers?: { [name: string]: string };
}

@injectable()
export class StoreProvider {
    private _store: AsyncLocalStorage<Store> | null = null;

    public get(): AsyncLocalStorage<Store> {
        if (this._store == null) {
            this._store = new AsyncLocalStorage<Store>();
        }

        return this._store;
    }
}