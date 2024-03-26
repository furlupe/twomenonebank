import axios, { AxiosResponse } from "axios";
import { inject, injectable } from "inversify";
import TYPES from "../types";
import { AppOptions } from "../options/app-options";

export interface IAuthClient {
    token(code: string, redirectUri: string): Promise<AxiosResponse<any, any>>;
    authorize(redirectUri: string): Promise<AxiosResponse<any, any>>;
    me(token: string): Promise<AxiosResponse<any, any>>;
}

@injectable()
export class AuthClient implements IAuthClient {
    private readonly baseUrl: string;

    constructor(@inject(TYPES.AppOptions) appOptions: AppOptions) {
        this.baseUrl = `${appOptions.hostname}:${appOptions.authPort}`
    }

    async token(code: string, redirectUri: string): Promise<AxiosResponse<any, any>> {
        const response = await axios.post(`${this.baseUrl}/connect/token`, new URLSearchParams({
            client_id: "amogus",
            grant_type: "authorization_code",
            code: code,
            redirect_uri: redirectUri
        }), {
            headers: {
                "Content-Type": "application/x-www-form-urlencoded"
            }
        });
        
        return response;
    }

    async authorize(redirect_uri: string): Promise<AxiosResponse<any, any>> {
        const response = await axios.get(`${this.baseUrl}/connect/authorize`, {
            params: new URLSearchParams({
                client_id: "amogus",
                response_type: "code",
                redirect_uri: redirect_uri,
                scope: "offline_access"
            })
        });

        return response;
    }

    async me(token: string): Promise<AxiosResponse<any, any>> {
        const response = await axios.get(`${this.baseUrl}/api/user/me`, {
            headers: {
                'Authorization': `Bearer ${token}`
            }
        });

        return response;
    }
}
