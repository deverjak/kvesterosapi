namespace Kvesteros.Api.Contracts.Responses;

public class HikeImageResponse
{
    public required Guid Id { get; init; }
    public required string Path { get; init; }
    public required string Title { get; init; }
    public required Guid HikeId { get; init; }
}