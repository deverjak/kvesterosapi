using Kvesteros.Api.Contracts.Requests;
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
}