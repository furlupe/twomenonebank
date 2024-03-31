import { AxiosInstance } from "axios";
import { inject, injectable } from "inversify";
import { AxiosProvider } from "../network/axios_provider";
import TYPES from "../types";
import { TariffDto } from "../dto/tariff_dto";

@injectable()
export class CreditClient {
    private readonly _axios: AxiosInstance;

    constructor(@inject(TYPES.AxiosProvider) axiosProvider: AxiosProvider) {
        this._axios = axiosProvider.credit;
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
        const params = types == null || types.length == 0 ? 
        new URLSearchParams({
            page: page,
        }): new URLSearchParams({
            page: page,
            types: types.map(it => `${it}`)
        })

        return await this._axios.get(`/api/manage/credits/${creditId}/operations`, {
            params: params
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
        return await this._axios.post(`/api/manage/Tariff`, body);
    }
    //#endregion

    async getCreditRate(userId: string) {
        return await this._axios.get(`/api/User/${userId}`);
    }
}