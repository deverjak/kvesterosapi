using Microsoft.AspNetCore.Http;

namespace Kvesteros.Api.Contracts.Requests;

public class UploadImageRequest()
{
    public required Guid HikeId { get; init; }
    public required string Title { get; init; }
    public required IFormFile File { get; init; }
}
