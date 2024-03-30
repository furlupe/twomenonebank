import { injectable } from "inversify";

@injectable()
export class AppOptions {
    public readonly authHost: string;
    public readonly coreHost: string;
    public readonly creditHost: string;
    public readonly transactionsHost: string;

    constructor() {
        this.authHost = process.env.AUTH_HOST
        this.coreHost = process.env.CORE_HOST
        this.creditHost = process.env.CREDIT_HOST
        this.transactionsHost = process.env.TRANSACTIONS_HOST
    }
}