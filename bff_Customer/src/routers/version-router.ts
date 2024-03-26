import { Router } from "express";
import { injectable } from "inversify";

@injectable()
export class VersionRouter {
    router(): Router {
        const router = Router();

        router.get('/', (req, res) => res.json("1.0.0"))

        return router;
    }
}