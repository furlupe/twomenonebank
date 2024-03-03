using Microsoft.AspNetCore.Http;
using Bank.Attributes.Utils;
using Bank.Attributes.Attributes;

namespace Bank.Exceptions;

public class NotFoundException : WebApiException
{
    public NotFoundException(string? message = null)
        : base(
            ErrorTypes.NotFound,
            StatusCodes.Status404NotFound,
            "Requested resource was not found.",
            message
        ) { }

    public static NotFoundException ForModel<TEntity>(Guid id) =>
        new NotFoundException(
            $"{typeof(TEntity).GetAttribute<ModelNameAttribute>().Name} with identifier {id} was not found."
        );
}
