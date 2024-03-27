const routerTypes = {
    AuthRouter: Symbol.for("AuthRouter"),
    UserRouter: Symbol.for("UserRouter"),
    VersionRouter: Symbol.for("VersionRouter"),
    CreditRouter: Symbol.for("CreditRouter")
}

const accessorTypes = {
    AxiosAccessor: Symbol.for("AxiosAccessor"),
    StoreAccessor: Symbol.for("StoreAccessor")
}

const clientTypes = {
    AuthClient: Symbol.for("AuthClient"),
    CreditClient: Symbol.for("CreditClient")
}

const TYPES = {
    ...routerTypes,
    ...accessorTypes,
    ...clientTypes,
    AppOptions: Symbol.for("AppOptions"),
}

export default TYPES;