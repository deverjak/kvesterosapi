namespace Kvesteros.Api.Services;

public interface IImageStorageService
{
    Task DeleteImageAsync(string filePath);
    public Task<string> StoreImageAsync(IFormFile file);
}
