import { Router } from "express";
import { inject, injectable } from "inversify";
import { IAuthClient } from "src/clients/auth-client";
import TYPES from "../types";
import { BaseRouter } from "./base-router";

@injectable()
export class UserRouter extends BaseRouter {
    @inject(TYPES.IAuthClient) private readonly _authClient: IAuthClient;

    protected mapRouterEndpoints(router: Router): void {
        router.get("/me", async (req, res) => {
            const response = await this._authClient.me();
            return res.json(response.data);
        });
    }
}