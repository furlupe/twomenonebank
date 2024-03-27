import { Router } from "express";
import { inject, injectable } from "inversify";
import { AuthClient } from "../clients/auth-client";
import TYPES from "../types";
import { BaseRouter } from "./base-router";

@injectable()
export class UserRouter extends BaseRouter {
    @inject(TYPES.AuthClient) private readonly _authClient: AuthClient;

    protected mapRouterEndpoints(router: Router): void {
        router.get("/me", async (req, res) => {
            const response = await this._authClient.me();
            return res.json(response.data);
        });
    }
}