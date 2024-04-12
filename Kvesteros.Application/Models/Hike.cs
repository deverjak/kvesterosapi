using System.Text.RegularExpressions;

namespace Kvesteros.Application.Models;

public partial class Hike
{
    public required Guid Id { get; init; }
    public required string Name { get; set; }
    public string Description { get; set; } = string.Empty;
    public required double Distance { get; set; }
    public string Route { get; set; } = string.Empty;
    public string Slug => GenerateSlug();

    private string GenerateSlug()
    {
        var sluggedTitle = SlugRegex().Replace(Name, string.Empty)
            .ToLower().Replace(" ", "-");
        return $"{sluggedTitle}";
    }

    [GeneratedRegex("[^0-9A-Za-z _-]", RegexOptions.NonBacktracking, 5)]
    private static partial Regex SlugRegex();
}
