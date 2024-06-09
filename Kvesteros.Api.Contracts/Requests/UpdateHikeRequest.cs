namespace Kvesteros.Api.Contracts.Requests;

public class UpdateHikeRequest
{
    public required string Name { get; init; }
    public string Description { get; init; } = string.Empty;
    public required double Distance { get; init; }
    public string Route { get; init; } = string.Empty;
}
