using System.Text;

namespace IGamingTest.Core.Helpers;

public static class TypeHelper
{
    private static readonly Dictionary<Type, string> primitivesFriendlyNameDict
        = new Dictionary<Type, string>
    {
    { typeof(string), "string" },
    { typeof(object), "object" },
    { typeof(bool), "bool" },
    { typeof(byte), "byte" },
    { typeof(char), "char" },
    { typeof(decimal), "decimal" },
    { typeof(double), "double" },
    { typeof(short), "short" },
    { typeof(int), "int" },
    { typeof(long), "long" },
    { typeof(sbyte), "sbyte" },
    { typeof(float), "float" },
    { typeof(ushort), "ushort" },
    { typeof(uint), "uint" },
    { typeof(ulong), "ulong" },
    { typeof(void), "void" }
    };

    public static string GetFriendlyShortName(this Type type)
    {
        if (primitivesFriendlyNameDict.TryGetValue(type, out var primitiveName))
        {
            return primitiveName;
        }

        if (type.IsArray)
        {
            var arrayType = type.GetElementType()!.GetFriendlyShortName();
            return $"{arrayType}[]";
        }

        if (!type.IsGenericType || type.IsGenericParameter)
        {
            return type.Name;
        }

        var nameSpan = type.Name.AsSpan();
        var genericSeparator = nameSpan.IndexOf('`');
        var genericTypeNameChars = nameSpan[..genericSeparator];

        var builder = new StringBuilder();
        builder.Append(genericTypeNameChars);
        builder.Append('<');

        var genericArgs = type.GetGenericArguments();
        var genericParameters = type.GetGenericArguments().Select(x => x.GetFriendlyShortName());
        builder.Append(string.Join(", ", genericParameters));

        builder.Append('>');
        return builder.ToString();
    }
}
