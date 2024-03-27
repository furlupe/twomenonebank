import axios, { AxiosInstance } from "axios";
import { inject, injectable } from "inversify";
import TYPES from "./types";
import { AppOptions } from "./options/app-options";
import { Store, StoreAccessor } from "./store-accessor";
import {AsyncLocalStorage} from 'node:async_hooks';

@injectable()
export class AxiosAccessor {
    private _axios: AxiosInstance | null = null;
    private _store: AsyncLocalStorage<Store> | null = null;
    private _baseUrl: string;

    constructor(
        @inject(TYPES.AppOptions) options: AppOptions,
        @inject(TYPES.StoreAccessor) storeAccessor: StoreAccessor
    ) {
        this._baseUrl = `${options.hostname}:${options.authPort}`;
        this._store = storeAccessor.get();
    }

    public get(): AxiosInstance {
        if (this._axios == null) {
            const ax = axios.create({
                baseURL: this._baseUrl
            });

            ax.interceptors.request.use(config => {
                const authHeader = this._store.getStore().authorizationHeader;
                config.headers.Authorization = authHeader

                return config;
            });

            this._axios = ax;
        }
        
        return this._axios;
    }
}