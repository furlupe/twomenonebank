import express from "express";
import 'reflect-metadata';
import { Container } from "inversify";
import { AxiosProvider } from "./network/axios_provider";
import TYPES from "./types";
import { StoreProvider } from "./common/store_provider";
import { AppOptions } from "./common/app_options";
import { AuthClient } from "./clients/auth_client";
import { BaseRouter } from "./routers/base_router";
import { AuthRouter } from "./routers/impl/auth_router";
import { CreditClient } from "./clients/credit_client";
import { CreditRouter } from "./routers/impl/credit_router";

const diContainer = new Container();

// - Bindings services
diContainer.bind<AxiosProvider>(TYPES.AxiosProvider).to(AxiosProvider).inSingletonScope();
diContainer.bind<StoreProvider>(TYPES.StoreProvider).to(StoreProvider).inSingletonScope();
diContainer.bind<AppOptions>(TYPES.AppOptions).to(AppOptions);

// Clients
diContainer.bind<AuthClient>(TYPES.AuthClient).to(AuthClient);
diContainer.bind<CreditClient>(TYPES.CreditClient).to(CreditClient);

// Routers
diContainer.bind<BaseRouter>(TYPES.AuthRouter).to(AuthRouter);
diContainer.bind<BaseRouter>(TYPES.CreditRouter).to(CreditRouter);

// - Application
const app = express();
const port = process.env.BFF_PORT || 5000;

// - Middlwares
app.use(express.json()); // to convert all request bodies to json
app.use((request, _response, next) => {
    const store = diContainer.get<StoreProvider>(TYPES.StoreProvider).get();
    store.run({ authorizationHeader: request.headers.authorization }, next)
});

// - Endpoints
app.use('/', diContainer.get<BaseRouter>(TYPES.AuthRouter).create());

// - Start
app.listen(port);
console.log(`Yay server working omg on port: ${port}`);
