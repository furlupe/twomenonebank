using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Common.Constants;

/// <summary>
/// Copy of <see cref="Microsoft.Extensions.Hosting.Environments"/> using constants.
/// </summary>
public static class Environments
{
    /// <inheritdoc cref="Microsoft.Extensions.Hosting.Environments.Development"/>
    public const string Development = "Development";

    /// <inheritdoc cref="Microsoft.Extensions.Hosting.Environments.Staging"/>
    public const string Staging = "Staging";

    /// <inheritdoc cref="Microsoft.Extensions.Hosting.Environments.Production"/>
    public const string Production = "Production";
}
