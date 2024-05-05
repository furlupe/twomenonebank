export class IsDarkThemeDto {
    isDark: boolean;

    constructor(enabled: boolean) {
        this.isDark = enabled;
    }
}