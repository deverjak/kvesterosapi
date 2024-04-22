namespace Kvesteros.Application.Models;

public class HikeImage
{
    public Guid Id { get; init; }
    public required string Path { get; set; }
    public required string Title { get; set; }
    public Guid HikeId { get; set; }
}
