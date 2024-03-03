using Bank.Attributes.Utils;

namespace Bank.Attributes.Attributes;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = false)]
public class ModelNameAttribute : Attribute
{
    public string Name { get; set; }

    public ModelNameAttribute(string name)
    {
        Name = name;
    }
}

public static class ModelNameAttributeExtensions
{
    public static string GetUserFriendlyName<TEntity>() =>
        typeof(TEntity).GetAttribute<ModelNameAttribute>().Name;
}
