namespace KvesterosApi.Services
{
    public class LocalImageStorageService : IImageStorageService
    {
        private readonly string _folderPath;
        public LocalImageStorageService(string localFolderPath)
        {
            _folderPath = localFolderPath;
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
}