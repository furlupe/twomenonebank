import { AxiosInstance } from "axios";
import { inject, injectable } from "inversify";
import { AxiosAccessor } from "../axios-accessor";
import TYPES from "../types";

@injectable()
export class CreditClient {
    private readonly _axios: AxiosInstance;

    constructor(@inject(TYPES.AxiosAccessor) axios: AxiosAccessor) {
        this._axios = axios.getCreditAxios();
    }

    getMyCredits(page: number = 1) {
        return this._axios.get("/api/credit/my", {
            params: new URLSearchParams({
                page: page.toString()
            })
        });
    }

    getMyCreditById(id: string){
        return this._axios.get(`/api/credit/my/${id}`);
    }

    getMyCreditOperations(id: string, page: number = 1) {
        return this._axios.get(`/api/credit/my/${id}/operations`, {
            params: new URLSearchParams({
                page: page.toString()
            })
        });
    }

    getTariffs(page: number = 1) {
        return this._axios.get('/api/manage/tariff', {
            params: new URLSearchParams({
                page: page.toString()
            })
        });
    }
}