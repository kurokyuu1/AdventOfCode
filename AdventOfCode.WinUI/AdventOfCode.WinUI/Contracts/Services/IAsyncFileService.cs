using System.Text.Json.Serialization.Metadata;
using System.Threading.Tasks;

namespace AdventOfCode.WinUI.Contracts.Services;

public interface IAsyncFileService
{
    Task<T?> ReadAsync<T>(string folder, string fileName, JsonTypeInfo<T>? typeInfo = default);
    Task SaveAsync<T>(string folder, string fileName, T data, JsonTypeInfo<T>? typeInfo = default);
    Task DeleteAsync(string folder, string? fileName);
}