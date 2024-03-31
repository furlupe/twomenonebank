import express from "express";
import 'reflect-metadata';
import { Container } from "inversify";
import { AxiosProvider } from "./network/axios_provider";
import TYPES from "./types";
import { StoreProvider } from "./common/store_provider";
import { HostsOptions } from "./common/hosts_options";
import { AuthClient } from "./clients/auth_client";
import { BaseRouter } from "./routers/base_router";
import { AuthRouter } from "./routers/impl/auth_router";
import { UserRepository } from "./common/user_repository";
import { CreditClient } from "./clients/credit_client";
import { CreditRouter } from "./routers/impl/credit_router";
import { CoreClient } from "./clients/core_client";
import { CoreRouter } from "./routers/impl/core_router";

const diContainer = new Container();

// - Bindings services
diContainer.bind<AxiosProvider>(TYPES.AxiosProvider).to(AxiosProvider).inSingletonScope();
diContainer.bind<StoreProvider>(TYPES.StoreProvider).to(StoreProvider).inSingletonScope();
diContainer.bind<HostsOptions>(TYPES.AppOptions).to(HostsOptions);

// Database repositories
diContainer.bind<UserRepository>(TYPES.UserRepository).to(UserRepository);

// Clients
diContainer.bind<AuthClient>(TYPES.AuthClient).to(AuthClient);
diContainer.bind<CreditClient>(TYPES.CreditClient).to(CreditClient);
diContainer.bind<CoreClient>(TYPES.CoreClient).to(CoreClient);

// Routers
diContainer.bind<BaseRouter>(TYPES.AuthRouter).to(AuthRouter);
diContainer.bind<BaseRouter>(TYPES.CreditRouter).to(CreditRouter);
diContainer.bind<BaseRouter>(TYPES.CoreRouter).to(CoreRouter);

// - Application
const app = express();
const port = process.env.Port || 5000;

// - Middlwares
app.use(express.json()); // to convert all request bodies to json
app.use((request, _response, next) => {
    const store = diContainer.get<StoreProvider>(TYPES.StoreProvider).get();
    store.run({ authorizationHeader: request.headers.authorization }, next)
});

// - Endpoints
app.use('/', diContainer.get<BaseRouter>(TYPES.AuthRouter).create());
app.use('/', diContainer.get<BaseRouter>(TYPES.CreditRouter).create());
app.use('/', diContainer.get<BaseRouter>(TYPES.CoreRouter).create());

// - Start
diContainer.get<UserRepository>(TYPES.UserRepository)
    .init()
    .then(() => {
        app.listen(port);
        console.log(`Yay server working omg on port: ${port}`);
    })
    .catch((e) => console.log(e));
