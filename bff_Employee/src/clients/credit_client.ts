import { AxiosInstance } from "axios";
import { inject, injectable } from "inversify";
import { AxiosProvider } from "../network/axios_provider";
import TYPES from "../types";
import { TariffDto } from "../dto/tariff_dto";

@injectable()
export class CreditClient {
    private readonly _axios: AxiosInstance;

    constructor(@inject(TYPES.AxiosProvider) axiosProvider: AxiosProvider) {
        this._axios = axiosProvider.core;
    }

    //#region Credit
    async getUserCredits(userId: string, page: string) {
        return await this._axios.get(`/api/manage/credits/of/${userId}`, {
            params: new URLSearchParams({
                page: page
            })
        });
    }

    async getCreditInfo(creditId: string) {
        return await this._axios.get(`/api/manage/credits/${creditId}`);
    }

    async getCreditOperations(creditId: string, page: string, types: string[]) {
        return await this._axios.get(`/api/manage/credits/${creditId}/operations`, {
            params: new URLSearchParams({
                page: page,
                types: types.map(it => `${it}`)
            })
        });
    }
    //#endregion

    //#region Tariff
    async getTraiffs(page: string) {
        return await this._axios.get(`/api/manage/Tariff`, {
            params: new URLSearchParams({
                page: page
            })
        });
    }

    async createTariff(body: TariffDto) {
        return await this._axios.post(`/api/manage/Tariff`, {
            body: body
        });
    }
    //#endregion

    async getCreditRate(userId: string) {
        return await this._axios.get(`/api/User/${userId}`);
    }
}