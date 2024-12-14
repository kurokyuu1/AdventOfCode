#region "Usings"

using System.Text.Json.Serialization.Metadata;
using AdventOfCode.WinUI.Contracts.Services;
using AdventOfCode.WinUI.Json;

#endregion

namespace AdventOfCode.WinUI.Services;

/// <summary>
///     Provides file storage services, including reading, saving, and deleting files.
///     This class supports both synchronous and asynchronous operations.
/// </summary>
public sealed class FileService : IFileService, IAsyncFileService
{
    #region "Interface Methods"

    /// <summary>
    ///     Asynchronously reads the content of a file and deserializes it into an object of type T.
    /// </summary>
    /// <typeparam name="T">The type of the object to deserialize to.</typeparam>
    /// <param name="folder">The folder where the file is located.</param>
    /// <param name="fileName">The name of the file to read.</param>
    /// <param name="typeInfo">Optional type information for custom serialization settings.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the deserialized object of type T.</returns>
    public async Task<T?> ReadAsync<T>(string folder, string fileName, JsonTypeInfo<T>? typeInfo = default)
    {
        var path = Path.Combine(folder, fileName);
        if (!File.Exists(path))
        {
            return default;
        }

        var json = await File.ReadAllTextAsync(path);
        return await JsonHelper.DeserializeAsync(json, typeInfo);
    }

    /// <summary>
    ///     Asynchronously saves an object of type T by serializing it to JSON and writing it to a file.
    /// </summary>
    /// <typeparam name="T">The type of the object to serialize.</typeparam>
    /// <param name="folder">The folder where the file will be saved.</param>
    /// <param name="fileName">The name of the file to save.</param>
    /// <param name="data">The object to serialize and save.</param>
    /// <param name="typeInfo">Optional type information for custom serialization settings.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public async Task SaveAsync<T>(string folder, string fileName, T data, JsonTypeInfo<T>? typeInfo = default)
    {
        if (!Directory.Exists(folder))
        {
            Directory.CreateDirectory(folder);
        }

        var path = Path.Combine(folder, fileName);
        await using var json = File.Create(path);
        await JsonHelper.SerializeAsync(json, data, typeInfo);
    }

    /// <summary>
    ///     Asynchronously deletes a file.
    /// </summary>
    /// <param name="folder">The folder where the file is located.</param>
    /// <param name="fileName">The name of the file to delete. If null, no action is taken.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public Task DeleteAsync(string folder, string? fileName)
    {
        if (fileName is { } && File.Exists(Path.Combine(folder, fileName)))
        {
            File.Delete(Path.Combine(folder, fileName));
        }

        return Task.CompletedTask;
    }

    /// <summary>
    ///     Reads the content of a file and deserializes it into an object of type T.
    /// </summary>
    /// <typeparam name="T">The type of the object to deserialize to.</typeparam>
    /// <param name="folder">The folder where the file is located.</param>
    /// <param name="fileName">The name of the file to read.</param>
    /// <param name="typeInfo">Optional type information for custom serialization settings.</param>
    /// <returns>The deserialized object of type T.</returns>
    public T? Read<T>(string folder, string fileName, JsonTypeInfo<T>? typeInfo = default)
    {
        var path = Path.Combine(folder, fileName);
        if (!File.Exists(path))
        {
            return default;
        }

        var json = File.ReadAllText(path);

        return JsonHelper.Deserialize(json, typeInfo);
    }

    /// <summary>
    ///     Saves an object of type T by serializing it to JSON and writing it to a file.
    /// </summary>
    /// <typeparam name="T">The type of the object to serialize.</typeparam>
    /// <param name="folder">The folder where the file will be saved.</param>
    /// <param name="fileName">The name of the file to save.</param>
    /// <param name="data">The object to serialize and save.</param>
    /// <param name="typeInfo">Optional type information for custom serialization settings.</param>
    public void Save<T>(string folder, string fileName, T data, JsonTypeInfo<T>? typeInfo = default)
    {
        if (!Directory.Exists(folder))
        {
            Directory.CreateDirectory(folder);
        }

        var path = Path.Combine(folder, fileName);
        var json = JsonHelper.Serialize(data, typeInfo);

        File.WriteAllText(path, json);
    }

    /// <summary>
    ///     Deletes a file.
    /// </summary>
    /// <param name="folder">The folder where the file is located.</param>
    /// <param name="fileName">The name of the file to delete. If null, no action is taken.</param>
    public void Delete(string folder, string? fileName)
    {
        if (fileName is { } && File.Exists(Path.Combine(folder, fileName)))
        {
            File.Delete(Path.Combine(folder, fileName));
        }
    }

    #endregion
}
