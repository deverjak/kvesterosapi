using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kvesteros.Api.Models;

[Table("Hike")]
public class Hike
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    [Required]
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DifficultyLevel DifficultyLevel { get; set; } = DifficultyLevel.Uknown;
    public double Distance { get; set; }
    public string Route { get; set; } = string.Empty;
    public DateTime CreationDate { get; set; }
}
