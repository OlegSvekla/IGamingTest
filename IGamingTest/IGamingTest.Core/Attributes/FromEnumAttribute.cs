namespace IGamingTest.Core.Attributes;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Parameter)]
public class FromEnumAttribute : Attribute
{
    public Type EnumType { get; }

    public FromEnumAttribute(Type enumType)
    {
        if (!enumType.IsEnum)
        {
            throw new ArgumentException("The type must be an enum.");
        }

        EnumType = enumType;
    }
}
