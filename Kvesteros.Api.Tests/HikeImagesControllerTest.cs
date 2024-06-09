using NSubstitute;
using Kvesteros.Api.Services;
using Kvesteros.Application.Repositories;
using Kvesteros.Api.Controllers;
using Microsoft.AspNetCore.Http;
using Kvesteros.Application.Models;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;

namespace Kvesteros.Api.Tests;

public class HikeImagesControllerTest
{
    [Fact]
    public async Task WhenHikeImageDatabaseWriteFails_ShouldDeleteTheImageFromStorage()
    {

        var storageService = Substitute.For<IImageStorageService>();
        var imageRepository = Substitute.For<IHikeImageRepository>();
        var hikeRepository = Substitute.For<IHikeRepository>();
        var logger = Substitute.For<ILogger<HikeImagesController>>();

        var hikeId = Guid.NewGuid();

        hikeRepository.ExistsByIdAsync(hikeId).Returns(true);
        imageRepository.CreateAsync(Arg.Any<HikeImage>()).Returns(Task.FromException<bool>(new Exception()));
        storageService.StoreImageAsync(Arg.Any<IFormFile>()).Returns(Task.FromResult("path"));

        var dto = new ImageDto(hikeId, new FormFile(new MemoryStream(), 0, 10, "file", "file.jpg"), "title");
        var controller = new HikeImagesController(storageService, imageRepository, hikeRepository, logger);

        var response = await controller.UploadImage(dto);

        await storageService.Received(1).StoreImageAsync(Arg.Any<IFormFile>());
        await storageService.Received(1).DeleteImageAsync(Arg.Any<string>());
        var statusCodeResult = Assert.IsType<ObjectResult>(response);
        Assert.Equal(500, statusCodeResult.StatusCode);

    }
}