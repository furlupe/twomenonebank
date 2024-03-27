import { AxiosInstance, AxiosResponse } from "axios";
import { inject, injectable } from "inversify";
import TYPES from "../types";
import { AxiosAccessor } from "../axios-accessor";

export interface IAuthClient {
    token(code: string, redirectUri: string): Promise<AxiosResponse<any, any>>;
    authorize(redirectUri: string): Promise<AxiosResponse<any, any>>;
    me(): Promise<AxiosResponse<any, any>>;
}

@injectable()
export class AuthClient implements IAuthClient {
    private readonly _axios: AxiosInstance;

    constructor(@inject(TYPES.AxiosAccessor) axiosAccessor: AxiosAccessor) {
        this._axios = axiosAccessor.get();
    }

    async token(code: string, redirectUri: string): Promise<AxiosResponse<any, any>> {
        const response = await this._axios.post(`/connect/token`, new URLSearchParams({
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
        const response = await this._axios.get(`/connect/authorize`, {
            params: new URLSearchParams({
                client_id: "amogus",
                response_type: "code",
                redirect_uri: redirect_uri,
                scope: "offline_access"
            })
        });

        return response;
    }

    async me(): Promise<AxiosResponse<any, any>> {
        const response = await this._axios.get(`/api/user/me`);
        return response;
    }
}
