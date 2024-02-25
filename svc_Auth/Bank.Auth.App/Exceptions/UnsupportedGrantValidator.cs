namespace Bank.Auth.App.Exceptions
{
    public class UnsupportedGrantValidator : Exception
    {
        public UnsupportedGrantValidator()
            : base("unsupported_grant_type") { }
    }
}
