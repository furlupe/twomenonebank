using System.Reflection;

namespace Bank.Attributes.Utils;

public static class AttributeExtensions
{
    public static TAttribute GetAttribute<TAttribute>(this Type type)
        where TAttribute : Attribute =>
        type.GetCustomAttribute(typeof(TAttribute)) as TAttribute
        ?? throw new Exception($"Missing attribute: {typeof(TAttribute).Name} is required");

    public static TAttribute? GetAttributeIfExists<TAttribute>(this Type type)
        where TAttribute : Attribute => type.GetCustomAttribute(typeof(TAttribute)) as TAttribute;
}
