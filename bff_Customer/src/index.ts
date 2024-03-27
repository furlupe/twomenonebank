import "reflect-metadata";
import "express-async-errors";
import express from 'express';
import { AuthRouter } from './routers/auth-router';
import { Container } from 'inversify';
import { AuthClient } from './clients/auth-client';
import TYPES from './types';
import { AppOptions } from "./options/app-options";
import { VersionRouter } from "./routers/version-router";
import { UserRouter } from "./routers/user-router";
import { AxiosAccessor } from "./axios-accessor";
import { StoreAccessor } from "./store-accessor";
import { BaseRouter } from "./routers/base-router";
import { CreditRouter } from "./routers/credit-router";
import { CreditClient } from "./clients/credit-client";

const diContainer = new Container();

diContainer.bind<AppOptions>(TYPES.AppOptions).to(AppOptions);
diContainer.bind<AxiosAccessor>(TYPES.AxiosAccessor).to(AxiosAccessor).inSingletonScope();
diContainer.bind<StoreAccessor>(TYPES.StoreAccessor).to(StoreAccessor).inSingletonScope();

diContainer.bind<AuthClient>(TYPES.AuthClient).to(AuthClient);
diContainer.bind<CreditClient>(TYPES.CreditClient).to(CreditClient);

diContainer.bind<BaseRouter>(TYPES.AuthRouter).to(AuthRouter);
diContainer.bind<BaseRouter>(TYPES.VersionRouter).to(VersionRouter);
diContainer.bind<BaseRouter>(TYPES.UserRouter).to(UserRouter);
diContainer.bind<BaseRouter>(TYPES.CreditRouter).to(CreditRouter);

const app = express();
const port = 5000;

app.use((req, _res, next) => {
    const store = diContainer.get<StoreAccessor>(TYPES.StoreAccessor).get();

    store.run({ authorizationHeader: req.headers.authorization }, next);
})

app.use(express.json());

app.use('/', diContainer.get<BaseRouter>(TYPES.AuthRouter).router());
app.use('/api/version', diContainer.get<BaseRouter>(TYPES.VersionRouter).router());
app.use('/api/user', diContainer.get<BaseRouter>(TYPES.UserRouter).router());
app.use('/api/credit', diContainer.get<CreditRouter>(TYPES.CreditRouter).router());

app.listen(port);

console.log("Server launched, listening on ", port);