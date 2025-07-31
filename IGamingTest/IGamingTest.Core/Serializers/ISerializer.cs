using Newtonsoft.Json;

namespace IGamingTest.Core.Serializers;

public interface ISerializer<T>
{
    T Serialize(object value);

    TResult? Deserialize<TResult>(T value);

    object? Deserialize(
        T value,
        Type type);

    TResult ForceDeserialize<TResult>(T value);

    TResult ForceDeserialize<TResult>(T value, JsonSerializerSettings settings);

    object ForceDeserialize(
        T value,
        Type type);
}
