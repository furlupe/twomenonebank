import { AxiosInstance, AxiosResponse } from "axios";
import { inject, injectable } from "inversify";
import { AxiosProvider } from "../network/axios_provider";
import TYPES from "../types";

@injectable()
export class AuthClient {
    private readonly _axios: AxiosInstance;

    constructor(@inject(TYPES.AxiosProvider) axiosProvideer: AxiosProvider) {
        this._axios = axiosProvideer.auth
    }

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

}