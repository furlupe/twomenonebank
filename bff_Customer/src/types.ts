const routerTypes = {
    AuthRouter: Symbol.for("AuthRouter"),
    UserRouter: Symbol.for("UserRouter"),
    VersionRouter: Symbol.for("VersionRouter"),
}

const accessorTypes = {
    AxiosAccessor: Symbol.for("AxiosAccessor"),
    StoreAccessor: Symbol.for("StoreAccessor")
}

const TYPES = {
    ...routerTypes,
    ...accessorTypes,
    IAuthClient: Symbol.for("IAuthClient"),
    AppOptions: Symbol.for("AppOptions"),
}

export default TYPES;