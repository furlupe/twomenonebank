import axios, { AxiosInstance, InternalAxiosRequestConfig } from "axios";
import { inject, injectable } from "inversify";
import TYPES from "./types";
import { AppOptions } from "./options/app-options";
import { Store, StoreAccessor } from "./store-accessor";
import {AsyncLocalStorage} from 'node:async_hooks';

@injectable()
export class AxiosAccessor {
    private _axiosAuth: AxiosInstance | null = null;
    private _axiosCredit: AxiosInstance | null = null;
    private _axiosCore: AxiosInstance | null = null;
    private _store: AsyncLocalStorage<Store> | null = null;
    
    private readonly _options: AppOptions;

    constructor(
        @inject(TYPES.AppOptions) options: AppOptions,
        @inject(TYPES.StoreAccessor) storeAccessor: StoreAccessor
    ) {
        this._options = options;
        this._store = storeAccessor.get();
    }

    public getAuth(): AxiosInstance {
        if (this._axiosAuth == null) {
            this._axiosAuth = this._createAxiosInstance(this._options.authHost);
        }

        return this._axiosAuth;
    }

    public getCredit(): AxiosInstance {
        if (this._axiosCredit == null) {
            this._axiosCredit = this._createAxiosInstance(this._options.creditHost);
        }

        return this._axiosCredit;
    }

    public getCore(): AxiosInstance {
        if (this._axiosCore == null) {
            this._axiosCore = this._createAxiosInstance(this._options.coreHost);
        }

        return this._axiosCore;
    }

    private _createAxiosInstance(baseUrl: string): AxiosInstance {
        const ax = axios.create({ baseURL: baseUrl});
        ax.interceptors.request.use(cfg => {
            const authHeader = this._store.getStore().authorizationHeader;
            cfg.headers.Authorization = authHeader
            return cfg
        });

        return ax;
    }
}