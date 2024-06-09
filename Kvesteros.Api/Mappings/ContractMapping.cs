using Kvesteros.Api.Contracts.Requests;
using Kvesteros.Api.Contracts.Responses;
using Kvesteros.Application.Models;

namespace Kvesteros.Api.Mappings;

public static class ContractMapping
{
    public static Hike MapToHike(this CreateHikeRequest request)
    {
        return new Hike
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Description = request.Description,
            Distance = request.Distance,
            Route = request.Route
        };
    }

    public static Hike MapToHike(this UpdateHikeRequest request, Guid id)
    {
        return new Hike
        {
            Id = id,
            Name = request.Name,
            Description = request.Description,
            Distance = request.Distance,
            Route = request.Route
        };
    }

    public static HikeImage MapToHikeImage(this UploadImageRequest request, string filePath)
    {
        return new HikeImage
        {
            Id = Guid.NewGuid(),
            HikeId = request.HikeId,
            Title = request.Title,
            Path = filePath
        };
    }

    public static HikeResponse MapToResponse(this Hike hike)
    {
        return new HikeResponse
        {
            Id = hike.Id,
            Name = hike.Name,
            Description = hike.Description,
            Distance = hike.Distance,
            Route = hike.Route,
            Images = hike.Images.Select(i => i.MapToResponse()),
            Slug = hike.Slug
        };
    }

    public static HikesResponse MapToResponse(this IEnumerable<Hike> hikes)
    {
        return new HikesResponse
        {
            Items = hikes.Select(h => h.MapToResponse())
        };
    }

    public static HikeImageResponse MapToResponse(this HikeImage image)
    {
        return new HikeImageResponse
        {
            Id = image.Id,
            Path = image.Path,
            Title = image.Title,
            HikeId = image.HikeId
        };
    }
}