import { Router, response } from "express";
import { BaseRouter } from "../base_router";
import { inject, injectable } from "inversify";
import TYPES from "../../types";
import { AuthClient } from "../../clients/auth_client";
import { RegisterInfoDto } from "../../dto/register_user_info_dto";
import { CreditClient } from "../../clients/credit_client";
import { UserInfoWithRatingDto } from "../../dto/user_info_with_rating_dto";
import { UserInfoDto } from "../../dto/user_info_dto";
import { CreditRateDto } from "../../dto/credit_rate_dto";

@injectable()
export class AuthRouter extends BaseRouter {
    @inject(TYPES.AuthClient) private readonly _authClient: AuthClient;
    @inject(TYPES.CreditClient) private readonly _creditClient: CreditClient;

    protected mapRouterEndpoints(router: Router): void {
        //#region Auth
        router.get('/connect/authorize', async (req, res) => {
            const { redirectUri } = req.query;

            const response = await this._authClient.authorize(redirectUri as string);

            return res.redirect(response.request.res.responseUrl);
        });

        router.post('/connect/token', async (req, res) => {
            const { code, redirectUri } = req.body;

            const response = await this._authClient.token(code, redirectUri);

            return res.json(response.data);
        });

        router.post('/connect/refresh', async (req, res) => {
            const { refreshToken } = req.body;

            const response = await this._authClient.refresh(refreshToken);

            return res.json(response.data);
        });
        //#endregion

        //#region User
        router.post('/api/User/register', async (req, res) => {
            const body = req.body;

            const response = await this._authClient.registerUser(body as RegisterInfoDto);

            return res.send(response.status);
        });

        router.get('/api/User', async (req, res) => {
            const { page } = req.query;

            const response = await this._authClient.getUsers(page as string);

            return res.json(response.data);
        });

        router.get('/api/User/:userId', async (req, res) => {
            const userId = req.params.userId;

            const userResponse = await this._authClient.getUserInfo(userId);
            const creditResponse = await this._creditClient.getCreditRate(userId);

            const user = userResponse.data as UserInfoDto;
            const creditRate = creditResponse.data as CreditRateDto;
            const userInfo = new UserInfoWithRatingDto(
                user.id,
                user.email,
                user.name,
                user.roles,
                user.phone,
                user.isBanned,
                creditRate?.creditRating ?? 100,
            )

            return res.json(userInfo);
        });

        router.post('/api/User/:userId/ban', async (req, res) => {
            const userId = req.params.userId;

            const response = await this._authClient.banUser(userId);

            return res.send(response.status);
        });

        router.post('/api/User/:userId/unban', async (req, res) => {
            const userId = req.params.userId;

            const response = await this._authClient.unbanUser(userId);

            return res.send(response.status);
        });
        //#endregion
    }
}