import { injectable } from "inversify";

@injectable()
export class HostsOptions {
    public readonly authHost: string;
    public readonly coreHost: string;
    public readonly creditHost: string;

    constructor() {
        this.authHost = process.env.MicroservicesHosts_Auth
        this.coreHost = process.env.MicroservicesHosts_Core
        this.creditHost = process.env.MicroservicesHosts_Credit
    }
}