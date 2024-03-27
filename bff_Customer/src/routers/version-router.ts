import { Router } from "express";
import { injectable } from "inversify";
import { BaseRouter } from "./base-router";

@injectable()
export class VersionRouter extends BaseRouter {
    protected mapRouterEndpoints(router: Router): void {
        router.get("/", (_, res) => res.json("1.0.0"));
    }
}