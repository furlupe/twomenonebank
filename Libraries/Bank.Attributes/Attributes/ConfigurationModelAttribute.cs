namespace Bank.Attributes.Attributes;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = false)]
public class ConfigurationModelAttribute : Attribute
{
    public string SectionKey { get; set; }

    public ConfigurationModelAttribute(string sectionKey)
    {
        SectionKey = sectionKey;
    }
}
