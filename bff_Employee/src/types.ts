export const routerTypes = {
    AuthRouter: Symbol.for("AuthRouter"),
    CoreRouter: Symbol.for("CoreRouter"),
    CreditRouter: Symbol.for("CreditRouter"),
    TransactionsRouter: Symbol.for("TransactionsRouter")
}

export const providerTypes = {
    AxiosProvider: Symbol.for("AxiosProvider"),
    StoreProvider: Symbol.for("StoreProvider")
}

export const clientTypes = {
    AuthClient: Symbol.for("AuthClient"),
    CoreClient: Symbol.for("CoreClient"),
    CreditClient: Symbol.for("CreditClient"),
    TransactionsClient: Symbol.for("TransactionsClient")
}

export const TYPES = {
    ...routerTypes,
    ...providerTypes,
    ...clientTypes,
    AppOptions: Symbol.for("AppOptions")
}

export default TYPES;