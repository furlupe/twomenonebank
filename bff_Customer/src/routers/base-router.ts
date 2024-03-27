import { Router } from "express";
import { injectable } from "inversify";

@injectable()
export abstract class BaseRouter {
    router(): Router {
        const router = Router();

        this.mapRouterEndpoints(router);

        return router;
    }

    protected abstract mapRouterEndpoints(router: Router): void;
}