import { AxiosInstance } from "axios";
import { inject, injectable } from "inversify";
import { AxiosProvider } from "../network/axios_provider";
import TYPES from "../types";

@injectable()
export class CoreClient {
    private readonly _axios: AxiosInstance;

    constructor(@inject(TYPES.AxiosProvider) axiosProvider: AxiosProvider) {
        this._axios = axiosProvider.core;
    }

    async getAccountInfo(accountId: string) {
        return await this._axios.get(`/manage/accounts/${accountId}`);
    }

    async getAccountsOfUser(
        userId: string,
        name: string = "",
        pageNumber: string = "1",
        pageSize: string = "30",
        sortingType: string = "Ascending"
    ) {
        return await this._axios.get(`/manage/accounts/of/${userId}`, {
            params: new URLSearchParams({
                Name: name,
                PageNumber: pageNumber,
                PageSize: pageSize,
                SortingType: sortingType
            })
        });
    }

    async getAccountHistory(
        accountId: string, 
        name: string = "",
        pageNumber: string = "1",
        pageSize: string = "30",
        sortingType: string = "Ascending",
        from: string,
        to: string
    ) {
        return await this._axios.get(`/manage/accounts/${accountId}/history`, {
            params: new URLSearchParams({
                Name: name,
                PageNumber: pageNumber,
                PageSize: pageSize,
                SortingType: sortingType,
                From: from,
                To: to
            })
        });
    }
}