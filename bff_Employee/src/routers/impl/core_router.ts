import { inject, injectable } from "inversify";
import { BaseRouter } from "../base_router";
import { Router } from "express";
import TYPES from "../../types";
import { CoreClient } from "../../clients/core_client";

@injectable()
export class CoreRouter extends BaseRouter {
    @inject(TYPES.CoreRouter) private readonly _client: CoreClient;
    
    protected mapRouterEndpoints(router: Router): void {
        router.get('/manage/accounts/:accountId', async (req, res) => {
            const accountid = req.params.accountId;

            const response = await this._client.getAccountInfo(accountid);

            return res.json(response.data);
        });

        router.get('/manage/accounts/of/:userId', async (req, res) => {
            const userId = req.params.userId;
            const { name, pageNumber, pageSize, sortingType } = req.query;

            const response = await this._client.getAccountsOfUser(
                userId, 
                name as string, 
                pageNumber as string, 
                pageSize as string, 
                sortingType as string
            );

            return res.json(response.data);
        });

        router.get('/manage/accounts/:accountId/history', async (req, res) => {
            const accountId = req.params.accountId;
            const { name, pageNumber, pageSize, sortingType, from, to } = req.query;
        
            const response = await this._client.getAccountHistory(
                accountId,
                name as string,
                pageNumber as string,
                pageSize as string,
                sortingType as string,
                from as string,
                to as string
            );

            return res.json(response.data);
        });
    }

}