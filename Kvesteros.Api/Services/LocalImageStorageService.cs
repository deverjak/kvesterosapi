namespace Kvesteros.Api.Services;

public class LocalImageStorageService(string localFolderPath) : IImageStorageService
{
    private readonly string _folderPath = localFolderPath;

    public Task DeleteImageAsync(string filePath)
    {
        return Task.CompletedTask;
    }

    public async Task<string> StoreImageAsync(IFormFile file)
    {
        var filePath = Path.Combine(_folderPath, file.FileName);

        Directory.CreateDirectory(_folderPath); // Ensure the directory exists

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        return filePath;
    }
}
