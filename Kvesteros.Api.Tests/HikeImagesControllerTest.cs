using NSubstitute;
using Kvesteros.Api.Services;
using Kvesteros.Application.Repositories;
using Kvesteros.Api.Controllers;
using Microsoft.AspNetCore.Http;
using Kvesteros.Application.Models;

namespace Kvesteros.Api.Tests;

public class HikeImagesControllerTest
{
    [Fact]
    public async Task WhenImageUploadFailes_ShouldRollbackDatabaseEntry()
    {

        var storageService = Substitute.For<IImageStorageService>();
        var imageRepository = Substitute.For<IHikeImageRepository>();
        var hikeRepository = Substitute.For<IHikeRepository>();

        var hikeId = Guid.NewGuid();

        hikeRepository.ExistsByIdAsync(hikeId).Returns(true);
        storageService.StoreImageAsync(Arg.Any<IFormFile>()).Returns(Task.FromException<string>(new Exception()));

        var dto = new ImageDTO(hikeId, new FormFile(Stream.Null, 0, 0, "file", "file.jpg"), "title");

        var controller = new HikeImagesController(storageService, imageRepository, hikeRepository);

        // Act
        await controller.UploadImage(dto);

        // Assert
        await imageRepository.Received(1).CreateAsync(Arg.Any<HikeImage>());
        //TODO: Verify that the image is not stored in the database
    }
}