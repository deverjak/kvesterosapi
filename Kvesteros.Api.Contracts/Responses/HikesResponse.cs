namespace Kvesteros.Api.Contracts.Responses;

public class HikesResponse
{
    public required IEnumerable<HikeResponse> Items { get; init; } = [];
}
