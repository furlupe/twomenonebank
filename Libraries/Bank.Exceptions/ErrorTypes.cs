namespace Bank.Exceptions;

public static class ErrorTypes
{
    private const string _namespace = "urn:twomenonebank:";

    public const string ValidationError = _namespace + "validation-error";
    public const string AccessDenied = _namespace + "access-denied";
    public const string NotFound = _namespace + "not-found";
    public const string ConflictingChanges = _namespace + "conflicting-changes";
    public const string InternalServerError = _namespace + "internal-server-error";
}
