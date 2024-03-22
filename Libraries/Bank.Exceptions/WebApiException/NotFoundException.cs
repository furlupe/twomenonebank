using Bank.Attributes.Attributes;
using Bank.Attributes.Utils;
using Microsoft.AspNetCore.Http;

namespace Bank.Exceptions.WebApiException;

public class NotFoundException : ProblemDetailsException
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

    public static NotFoundException ForModel<TEntity>() =>
        new NotFoundException(
            $"Requested {typeof(TEntity).GetAttribute<ModelNameAttribute>().Name} was not found."
        );
}
