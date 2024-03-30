import { inject, injectable } from "inversify";
import { BaseRouter } from "../base_router";
import { Router } from "express";
import TYPES from "../../types";
import { CreditClient as CreditClient } from "../../clients/credit_client";
import { TariffDto } from "src/dto/tariff_dto";

@injectable()
export class CreditRouter extends BaseRouter {
    @inject(TYPES.CreditClient) private readonly _client: CreditClient;

    protected mapRouterEndpoints(router: Router): void {
        //#region Credit
        router.get('/api/manage/credits/of/:userId', async (req, res) => {
            const userId = req.params.userId;
            const { page } = req.query;
            
            const response = await this._client.getUserCredits(userId, page as string);

            return res.json(response.data);
        });

        router.get('/api/manage/credits/:creditId', async (req, res) => {
            const creditId = req.params.creditId;
            
            const response = await this._client.getCreditInfo(creditId);

            return res.json(response.data);
        });

        router.get('/api/manage/credits/:creditId/operations', async (req, res) => {
            const creditId = req.params.creditId;
            const { page, types } = req.query;
            
            const response = await this._client.getCreditOperations(creditId, page as string, types as string[]);

            return res.json(response.data);
        });
        //#endregion

        //#region Tariff
        router.get('/api/manage/Tariff', async (req, res) => {
            const { page } = req.query;

            const response = await this._client.getTraiffs(page as string);

            return res.json(response.data);
        });

        router.post('/apimanage/Tariff', async (req, res) => {
            const body = req.body as TariffDto;

            const response = await this._client.createTariff(body);

            return res.send(response.status)
        });
        //#endregion

        router.get('/api/User/:userId', async (req, res) => {
            const userId = req.params.userId;

            const response = await this._client.getCreditRate(userId);

            return res.json(response.data);
        });
    }
}