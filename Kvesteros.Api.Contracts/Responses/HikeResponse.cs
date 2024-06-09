namespace Kvesteros.Api.Contracts.Responses;

public class HikeResponse
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }
    public required string Description { get; init; }
    public required double Distance { get; init; }
    public required string Route { get; init; }
    public required IEnumerable<HikeImageResponse> Images { get; init; }
    public required string Slug { get; init; }
}
