import axios, { AxiosInstance } from "axios";
import { HostsOptions } from "../common/hosts_options";
import { Store, StoreProvider } from "../common/store_provider";
import { inject, injectable } from "inversify";
import {AsyncLocalStorage} from 'node:async_hooks';
import TYPES from "../types";

@injectable()
export class AxiosProvider {
    private _axiosAuth: AxiosInstance | null = null;
    private _axiosCore: AxiosInstance | null = null;
    private _axiosCredit: AxiosInstance | null = null;

    private readonly _options: HostsOptions;
    private readonly _store: AsyncLocalStorage<Store>;

    constructor(
        @inject(TYPES.AppOptions) options: HostsOptions,
        @inject(TYPES.StoreProvider) storeAccessor: StoreProvider
    ) {
        this._options = options;
        this._store = storeAccessor.get();
    }

    public get auth(): AxiosInstance {
        if (this._axiosAuth == null) {
            this._axiosAuth = this._createAxiosInstance(this._options.authHost)
        }

        return this._axiosAuth
    }

    public get core(): AxiosInstance {
        if (this._axiosCore == null) {
            this._axiosCore = this._createAxiosInstance(this._options.coreHost)
        }

        return this._axiosCore
    }

    public get credit(): AxiosInstance {
        if (this._axiosCredit == null) {
            this._axiosCredit = this._createAxiosInstance(this._options.creditHost)
        }

        return this._axiosCredit
    }

    // Creating axios with interceptor for auth header
    private _createAxiosInstance(baseUrl: string): AxiosInstance {
        const instance = axios.create({baseURL: baseUrl});
        instance.interceptors.request.use(cfg => {
            const store = this._store.getStore();
            const authHeader = store.headers['authorization'];
            const idempotenceKey = store.headers['idempotenceKey'];

            cfg.headers.Authorization = authHeader;
            cfg.headers["Idempotence-Key"] = idempotenceKey;

            return cfg
        });

        return instance;
    }

}