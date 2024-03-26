import { injectable } from "inversify";

@injectable()
export class AppOptions {
    public readonly hostname: string;
    public readonly authPort: number;

    constructor() {
        this.hostname = process.env.TWOMENONEBANK_HOST;
        this.authPort = parseInt(process.env.AUTH_PORT);
    }
}