import { Router, response } from "express";
import { BaseRouter } from "../base_router";
import { inject, injectable } from "inversify";
import TYPES from "../../types";
import { AuthClient } from "../../clients/auth_client";

@injectable()
export class AuthRouter extends BaseRouter {
    @inject(TYPES.AuthClient) private readonly _client: AuthClient;

    protected mapRouterEndpoints(router: Router): void {
        //#region Auth
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
        //#endregion

        //#region User
        router.post('/api/User/register', async (req, res) => {
            const { body } = req.body;

            const response = await this._client.registerUser(body);

            return res.send(response.status);
        });

        router.get('/api/User', async (req, res) => {
            const { page } = req.query;

            const response = await this._client.getUsers(page as string);

            return res.json(response.data);
        });

        router.get('/api/User/:userId', async (req, res) => {
            const userId = req.params.userId;

            const response = await this._client.getUserInfo(userId);

            return res.json(response.data);
        });

        router.post('/api/User/:userId/ban', async (req, res) => {
            const userId = req.params.userId;

            const response = await this._client.banUser(userId);

            return res.send(response.status);
        });

        router.post('/api/User/:userId/unban', async (req, res) => {
            const userId = req.params.userId;

            const response = await this._client.unbanUser(userId);

            return res.send(response.status);
        });
        //#endregion
    }
}