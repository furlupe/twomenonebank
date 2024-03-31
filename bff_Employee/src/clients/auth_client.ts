import { AxiosInstance, AxiosResponse } from "axios";
import { inject, injectable } from "inversify";
import { AxiosProvider } from "../network/axios_provider";
import TYPES from "../types";
import { RegisterInfoDto } from "../dto/register_user_info_dto";

@injectable()
export class AuthClient {
    private readonly _axios: AxiosInstance;

    constructor(@inject(TYPES.AxiosProvider) axiosProvideer: AxiosProvider) {
        this._axios = axiosProvideer.auth
    }

    //#region Authentification
    async authorize(redirectUri: string) {
        return await this._axios.get("/connect/authorize", {
            params: new URLSearchParams({
                client_id: "amogus",
                response_type: "code",
                redirect_uri: redirectUri,
                scope: "offline_access"
            })
        });
    }

    async token(code: string, redirectUri: string) {
        return await this._axios.post("/connect/token", new URLSearchParams ({
            client_id: "amogus",
            grant_type: "authorization_code",
            code: code,
            redirect_uri: redirectUri
        }), {
            headers: {
                "Content-Type": "application/x-www-form-urlencoded"
            }
        });
    }

    async refresh(refreshToken: string) {
        return await this._axios.post("/connect/token", new URLSearchParams ({
            client_id: "amogus",
            grant_type: "refresh_token",
            refresh_token: refreshToken,
        }), {
            headers: {
                "Content-Type": "application/x-www-form-urlencoded"
            }
        });
    }
    //#endregion

    //#region User
    async registerUser(body: RegisterInfoDto) {
        return await this._axios.post('/api/User/register', body);
    }

    async getUsers(page: string = '1') {
        return await this._axios.get('/api/User', {
            params: new URLSearchParams({
                page: page
            })
        });
    }

    async getUserInfo(userId: string) {
        return await this._axios.get(`/api/User/${userId}`);
    }

    async banUser(userId: string) {
        return await this._axios.post(`/api/User/${userId}/ban`);
    }

    async unbanUser(userId: string) {
        return await this._axios.post(`/api/User/${userId}/unban`);
    }
    //#endregion

}