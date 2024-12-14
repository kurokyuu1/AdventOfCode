using System.Text.Json.Serialization.Metadata;

namespace AdventOfCode.WinUI.Contracts.Services;

public interface IFileService
{
    T? Read<T>(string folder, string fileName, JsonTypeInfo<T>? typeInfo = default);
    void Save<T>(string folder, string fileName, T data, JsonTypeInfo<T>? typeInfo = default);
    void Delete(string folder, string fileName);
}