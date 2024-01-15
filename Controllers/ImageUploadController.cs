using KvesterosAdminApi.Configuration;
using KvesterosAdminApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.IO;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class ImageUploadController : ControllerBase
{
    private readonly IImageStorageService _storageService;

    public ImageUploadController(IImageStorageService storageService)
    {
        _storageService = storageService;
    }

    [HttpPost]
    public async Task<IActionResult> UploadImage(IFormFile file, int hikeId)
    {
        if (file == null || file.Length == 0)
        {
            return BadRequest("No file is provided.");
        }

        var filePath = await _storageService.StoreImageAsync(file);

        return Ok(new { FilePath = filePath, HikeId = hikeId });
    }
}