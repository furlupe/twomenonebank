import { Router, response } from "express";
import { BaseRouter } from "../base_router";
import { inject, injectable } from "inversify";
import TYPES from "../../types";
import { AuthClient } from "../../clients/auth_client";

@injectable()
export class AuthRouter extends BaseRouter {
    @inject(TYPES.AuthClient) private readonly _client: AuthClient;

    protected mapRouterEndpoints(router: Router): void {
        router.get('/connect/authorize', async (req, res) => {
            const { redirectUri } = req.query;

            const response = await this._client.authorize(redirectUri as string);

            return res.redirect(response.request.res.responseUrl);
        });

        router.post('/connect/token', async (req, res) => {
            const { code, redirectUri } = req.body;

            const response = await this._client.token(code, redirectUri);

            return res.json(response.data);
        });

        router.post('/connect/refresh', async (req, res) => {
            const { refreshToken } = req.body;

            const response = await this._client.refresh(refreshToken);

            return res.json(response.data);
        });
    }
}