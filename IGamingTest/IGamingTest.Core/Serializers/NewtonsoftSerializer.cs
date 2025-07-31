using IGamingTest.Core.Exceptions;
using Newtonsoft.Json;
using System.Text;

namespace IGamingTest.Core.Serializers;

public class NewtonsoftSerializer
    : ISerializer<string>,
    ISerializer<byte[]>
{
    #region string

    public string Serialize(object value)
    {
        var settings = new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore
        };

        return JsonConvert.SerializeObject(value, settings);
    }

    public TResult? Deserialize<TResult>(string value)
        => JsonConvert.DeserializeObject<TResult>(value);

    public static TResult? Deserialize<TResult>(string value, JsonSerializerSettings settings)
        => JsonConvert.DeserializeObject<TResult>(value, settings);

    public object? Deserialize(
        string value,
        Type type)
        => JsonConvert.DeserializeObject(value, type);

    public TResult ForceDeserialize<TResult>(string value)
        => Deserialize<TResult>(value)
            ?? throw new DeserializationException(value);

    public object ForceDeserialize(
        string value,
        Type type)
        => Deserialize(value, type)
            ?? throw new DeserializationException(value);

    public TResult ForceDeserialize<TResult>(
        string value,
        JsonSerializerSettings settings)
        => Deserialize<TResult>(value, settings)
        ?? throw new DeserializationException(value);

    #endregion string

    #region bytes

    byte[] ISerializer<byte[]>.Serialize(object value)
        => Encoding.UTF8.GetBytes(Serialize(value));

    public TResult? Deserialize<TResult>(byte[] value)
        => Deserialize<TResult>(Encoding.UTF8.GetString(value));

    public object? Deserialize(
        byte[] value,
        Type type)
        => Deserialize(Encoding.UTF8.GetString(value), type);

    public static TResult? Deserialize<TResult>(byte[] value, JsonSerializerSettings settings)
        => JsonConvert.DeserializeObject<TResult>(Encoding.UTF8.GetString(value), settings);

    public TResult ForceDeserialize<TResult>(byte[] value)
        => Deserialize<TResult>(value)
            ?? throw new DeserializationException(value);

    public object ForceDeserialize(
        byte[] value,
        Type type)
        => Deserialize(value, type)
            ?? throw new DeserializationException(value);

    public TResult ForceDeserialize<TResult>(
        byte[] value,
        JsonSerializerSettings settings)
        => Deserialize<TResult>(value, settings)
            ?? throw new DeserializationException(value);

    #endregion bytes
}
