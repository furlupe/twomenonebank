import "reflect-metadata";
import express from 'express';
import { AuthRouter } from './routers/auth-router';
import { Container } from 'inversify';
import { AuthClient, IAuthClient } from './clients/auth-client';
import TYPES from './types';
import { AppOptions } from "./options/app-options";
import { VersionRouter } from "./routers/version-router";
import { UserRouter } from "./routers/user-router";
import { AxiosAccessor } from "./axios-accessor";
import { StoreAccessor } from "./store-accessor";
import { BaseRouter } from "./routers/base-router";

const diContainer = new Container();

diContainer.bind<AppOptions>(TYPES.AppOptions).to(AppOptions);
diContainer.bind<AxiosAccessor>(TYPES.AxiosAccessor).to(AxiosAccessor).inSingletonScope();
diContainer.bind<StoreAccessor>(TYPES.StoreAccessor).to(StoreAccessor).inSingletonScope();


diContainer.bind<IAuthClient>(TYPES.IAuthClient).to(AuthClient);

diContainer.bind<BaseRouter>(TYPES.AuthRouter).to(AuthRouter);
diContainer.bind<BaseRouter>(TYPES.VersionRouter).to(VersionRouter);
diContainer.bind<BaseRouter>(TYPES.UserRouter).to(UserRouter);

const app = express();
const port = 5000;

app.use((req, _res, next) => {
    const store = diContainer.get<StoreAccessor>(TYPES.StoreAccessor).get();

    store.run({ authorizationHeader: req.headers.authorization }, next);
})

app.use(express.json());

app.use('/auth', diContainer.get<BaseRouter>(TYPES.AuthRouter).router());
app.use('/version', diContainer.get<BaseRouter>(TYPES.VersionRouter).router());
app.use('/user', diContainer.get<BaseRouter>(TYPES.UserRouter).router());

app.listen(port);