namespace KvesterosApi.Services
{
    public interface IImageStorageService
    {
        public Task<string> StoreImageAsync(IFormFile file);
    }
}