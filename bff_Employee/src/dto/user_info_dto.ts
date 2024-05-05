export class UserInfoDto {
    id: string;
    email: string;
    name: string;
    roles: string[];
    phone: string;
    isBanned: boolean;

    constructor(
        id: string,
        email: string,
        name: string,
        roles: string[],
        phone: string,
        isBanned: boolean,
    ) {
        this.id = id;
        this.email = email;
        this.name = name;
        this.roles = roles;
        this.phone = phone;
        this.isBanned = isBanned;
    }
}