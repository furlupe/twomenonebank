export class UserInfoWithRatingDto {
    id: string;
    email: string;
    name: string;
    roles: string[];
    phone: string;
    isBanned: boolean;
    creditRating: number;

    constructor(
        id: string,
        email: string,
        name: string,
        roles: string[],
        phone: string,
        isBanned: boolean,
        creditRating: number
    ) {
        this.id = id;
        this.email = email;
        this.name = name;
        this.roles = roles;
        this.phone = phone;
        this.isBanned = isBanned;
        this.creditRating = creditRating;
    }
}