import { Client, QueryResult } from 'pg';
import { injectable } from "inversify";

export class User {
    Id: string;
    DarkThemeEnabled: boolean = false;

    constructor(id: string, darkThemeEnabled: boolean = false) {
        this.Id = id;
        this.DarkThemeEnabled = darkThemeEnabled;
    }
};

@injectable()
export class UserRepository {
    private readonly _client: Client;
    private readonly _baseDbUrl: string;

    constructor() {
        this._baseDbUrl = process.env.DbConfiguration_BaseUrl;
        this._client = new Client({
            connectionString: `${this._baseDbUrl}/${process.env.DbConfiguration_BffDbName}`
        });
    }

    async init() {
        // in order to create DB you have to be connected to some DB (e.g., default 'postgres')
        const oneUseClient = new Client({ connectionString: `${this._baseDbUrl}/${process.env.DbConfiguration_DefaultDbName}` });

        const createDbOperation = async (client: Client) => {
            let result = await client.query(`\
                SELECT '1'\
                WHERE NOT EXISTS (SELECT FROM pg_database WHERE datname = '${process.env.DbConfiguration_BffDbName}')`);

            const baseNotExists = Boolean(Object.values(result.rows[0])[0]);

            if (baseNotExists) {
                result = await client.query(`CREATE DATABASE ${process.env.DbConfiguration_BffDbName}`);
            }

            return result;
        }
        await this.performOperationWithClient(createDbOperation, oneUseClient);

        const createTableOperation = async (client: Client) =>
            client.query('\
                CREATE TABLE IF NOT EXISTS\
                Users(\
                    Id UUID NOT NULL,\
                    DarkThemeEnabled BOOLEAN DEFAULT TRUE,\
                    PRIMARY KEY (Id))\
            ');

        await this.performOperation(createTableOperation);
    }

    public async create(user: User) {
        const operation = async (client: Client) =>
            await client.query('INSERT INTO Users (Id, DarkThemeEnabled) VALUES ($1, $2)', [user.Id, `${user.DarkThemeEnabled}`]);

        await this.performOperation(operation);
    }

    public async get(id: string) {
        const operation = async (client: Client) =>
            await client.query('SELECT * FROM Users WHERE Id = $1', [id]);

        return (await this.performOperation(operation)).rows;
    }

    public async update(user: User) {
        const operation = async (client: Client) =>
            await client.query('UPDATE Users SET DarkThemeEnabled = $1 WHERE Id = $2', [`${user.DarkThemeEnabled}`, user.Id]);

        await this.performOperation(operation);
    }

    public async delete(user: User) {
        const operation = async (client: Client) =>
            await client.query('DELETE FROM Users WHERE Id = $1', [user.Id]);

        await this.performOperation(operation);
    }

    private async performOperation(op: (client: Client) => Promise<QueryResult<any>>) {
        return await this.performOperationWithClient(op, this._client);
    }

    private async performOperationWithClient(op: (client: Client) => Promise<QueryResult<any>>, client: Client) {
        await client.connect();
        const result = await op(client);
        await client.end();

        return result;
    }
}