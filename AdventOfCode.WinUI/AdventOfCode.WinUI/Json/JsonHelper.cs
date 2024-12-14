using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;

namespace AdventOfCode.WinUI.Json;

/// <summary>
///     Provides utility methods for serializing to and deserializing from JSON.
///     This class supports both synchronous and asynchronous operations and allows
///     for customization of serialization settings via <see cref="JsonTypeInfo{T}" />.
/// </summary>
public static class JsonHelper
{
    #region "Methods"

    /// <summary>
    ///     Deserializes the JSON string to an object of type T.
    /// </summary>
    /// <typeparam name="T">The type of the object to deserialize to.</typeparam>
    /// <param name="json">The JSON string to deserialize.</param>
    /// <param name="typeInfo">Optional type information for custom serialization settings.</param>
    /// <returns>The deserialized object of type T.</returns>
    public static T? Deserialize<T>(string json, JsonTypeInfo<T>? typeInfo = default) =>
        typeInfo is { }
            ? JsonSerializer.Deserialize(json, typeInfo)
            : JsonSerializer.Deserialize<T>(json, JsonSerializerOptions.Default);

    /// <summary>
    ///     Asynchronously deserializes the JSON string to an object of type <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">The type of the object to deserialize to.</typeparam>
    /// <param name="json">The JSON string to deserialize.</param>
    /// <param name="typeInfo">Optional type information for custom serialization settings.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the deserialized object of type <typeparamref name="T"/>.</returns>
    public static Task<T?> DeserializeAsync<T>(string json, JsonTypeInfo<T>? typeInfo = default)
    {
        var stream = new MemoryStream(Encoding.UTF8.GetBytes(json));

        return DeserializeAsync(stream, typeInfo);
    }

    /// <summary>
    ///     Asynchronously deserializes the JSON from a stream to an object of type <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">The type of the object to deserialize to.</typeparam>
    /// <param name="stream">The stream containing the JSON to deserialize.</param>
    /// <param name="typeInfo">Optional type information for custom serialization settings.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the deserialized object of type <typeparamref name="T"/>.</returns>
    public static async Task<T?> DeserializeAsync<T>(Stream stream, JsonTypeInfo<T>? typeInfo = default) =>
        typeInfo is { }
            ? await JsonSerializer.DeserializeAsync(stream, typeInfo)
            : await JsonSerializer.DeserializeAsync<T>(stream, JsonSerializerOptions.Default);

    /// <summary>
    ///     Serializes an object of type <typeparamref name="T"/> to a JSON string.
    /// </summary>
    /// <typeparam name="T">The type of the object to serialize.</typeparam>
    /// <param name="value">The object to serialize.</param>
    /// <param name="typeInfo">Optional type information for custom serialization settings.</param>
    /// <returns>The serialized JSON string.</returns>
    public static string Serialize<T>(T value, JsonTypeInfo<T>? typeInfo = default) =>
        typeInfo is { }
            ? JsonSerializer.Serialize(value, typeInfo)
            : JsonSerializer.Serialize(value, JsonSerializerOptions.Default);

    /// <summary>
    ///     Asynchronously serializes an object of type <typeparamref name="T"/> to a JSON string and writes it to a stream.
    /// </summary>
    /// <typeparam name="T">The type of the object to serialize.</typeparam>
    /// <param name="value">The object to serialize.</param>
    /// <param name="typeInfo">Optional type information for custom serialization settings.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public static Task<string> SerializeAsync<T>(T value, JsonTypeInfo<T>? typeInfo = default)
    {
        return Task.Run(() => Serialize(value, typeInfo));
    }

    /// <summary>
    ///     Asynchronously serializes an object of type <typeparamref name="T"/> and writes it to a stream.
    /// </summary>
    /// <typeparam name="T">The type of the object to serialize.</typeparam>
    /// <param name="stream">The stream to write the serialized JSON to.</param>
    /// <param name="value">The object to serialize.</param>
    /// <param name="typeInfo">Optional type information for custom serialization settings.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public static async Task<Stream> SerializeAsync<T>(Stream stream, T value, JsonTypeInfo<T>? typeInfo = default)
    {
        if (typeInfo is { })
        {
            await JsonSerializer.SerializeAsync(stream, value, typeInfo);
        }
        else
        {
            await JsonSerializer.SerializeAsync(stream, value, JsonSerializerOptions.Default);
        }

        return stream;
    }

    #endregion
}
