import { Router } from "express";
import { inject, injectable } from "inversify";
import { IAuthClient } from "src/clients/auth-client";
import TYPES from "../types";

@injectable()
export class AuthRouter {
    @inject(TYPES.IAuthClient) private readonly _authClient: IAuthClient;

    router(): Router {
        const router = Router();

        router.post('/connect/token', async (req, res) => {
            const { code, redirectUri } = req.body;

            const response = await this._authClient.token(code, redirectUri);

            return res.json(response.data);
        })

        router.get('/connect/authorize', async (req, res) => {
            const { redirect_uri  } = req.query;

            const response = await this._authClient.authorize(redirect_uri as string);

            return res.redirect(response.request.res.responseUrl);
        })

        router.get('/user/me', async (req, res) => {
            const response = await this._authClient.me(req.headers.authorization);

            return res.json(response.data);
        });

        return router;
    }
}