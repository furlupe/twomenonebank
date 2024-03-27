import { injectable } from "inversify";

@injectable()
export class AppOptions {
    public readonly authHost: string;
    public readonly creditHost: string;
    public readonly coreHost: string = "";

    constructor() {
        this.authHost = process.env.TWOMENONEBANK_AUTH_HOST;
        this.creditHost = process.env.TWOMENONEBANK_CREDIT_HOST;
    }
}