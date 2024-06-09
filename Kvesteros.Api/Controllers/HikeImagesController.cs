using Kvesteros.Api.Services;
using Kvesteros.Application.Models;
using Kvesteros.Application.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Kvesteros.Api.Controllers;

//TODO: Move to contracts, verify IFormFile makes sense in this case
public record ImageDto(Guid HikeId, IFormFile File, string Title);

[ApiController]
public class HikeImagesController(
        IImageStorageService storageService,
        IHikeImageRepository imageRepository,
        IHikeRepository hikeRepository,
        ILogger<HikeImagesController> logger) : ControllerBase
{
    private readonly IImageStorageService _storageService = storageService;
    private readonly IHikeImageRepository _imageRepository = imageRepository;
    private readonly IHikeRepository _hikeRepository = hikeRepository;
    private readonly ILogger<HikeImagesController> _logger = logger;

    [HttpPost(ApiEndpoints.HikeImages.Create)]
    public async Task<IActionResult> UploadImage([FromForm] ImageDto dto)
    {
        if (dto.File == null || dto.File.Length == 0)
        {
            return BadRequest("No file is provided.");
        }

        var hikeId = dto.HikeId;
        if (!await _hikeRepository.ExistsByIdAsync(hikeId))
        {
            return BadRequest("No hike found with the provided id.");
        }

        var filePath = await _storageService.StoreImageAsync(dto.File);
        var image = new HikeImage
        {
            Id = Guid.NewGuid(),
            HikeId = hikeId,
            Title = dto.Title,
            Path = filePath
        };
        try
        {
            await _imageRepository.CreateAsync(image);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while uploading image for hike ID: {HikeId}", dto.HikeId);
            await _storageService.DeleteImageAsync(filePath);
            return StatusCode(500, "An error occurred while saving the image.");
        }
        return CreatedAtAction(nameof(GetById), new { id = image.Id }, image);


    }

    [HttpGet(ApiEndpoints.HikeImages.Get)]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var image = await _imageRepository.GetByIdAsync(id);
        if (image == null)
        {
            return NotFound();
        }
        return Ok(image);
    }
}