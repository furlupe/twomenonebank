import { inject, injectable } from "inversify";
import { BaseRouter } from "./base-router";
import { Router } from "express";
import TYPES from "../types";
import { CreditClient } from "../clients/credit-client";

@injectable()
export class CreditRouter extends BaseRouter {
    @inject(TYPES.CreditClient) private readonly _creditClient: CreditClient;

    protected mapRouterEndpoints(router: Router): void {
        router.get("/my", async (req, res) => {
            const { page } = req.query;
            const response = await this._creditClient.getMyCredits(parseInt(page as string ?? '1'));

            return response.data;
        })
    }
}