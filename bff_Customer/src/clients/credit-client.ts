import { AxiosInstance, AxiosResponse } from "axios";
import { inject, injectable } from "inversify";
import { AxiosAccessor } from "../axios-accessor";
import TYPES from "../types";

@injectable()
export class CreditClient {
    private readonly _axios: AxiosInstance;

    constructor(@inject(TYPES.AxiosAccessor) axios: AxiosAccessor) {
        this._axios = axios.getCredit();
    }

    getMyCredits(page: number = 1): Promise<AxiosResponse<any, any>> {
        return this._axios.get("/api/credit/my", {
            params: new URLSearchParams({
                page: page.toString()
            })
        });
    }
}