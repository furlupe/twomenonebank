import { Router } from "express";
import { injectable } from "inversify";

@injectable()
export abstract class BaseRouter {
    protected abstract mapRouterEndpoints(router: Router): void;

    create(): Router {
        const router = Router();

        this.mapRouterEndpoints(router);

        return router;
    }
}