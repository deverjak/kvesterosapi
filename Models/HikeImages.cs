using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KvesterosAdminApi.Models
{
    [Table("HikeImages")]
    public class HikeImage
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string ImagePath { get; set; } = string.Empty;

        [Required]
        public int HikeId { get; set; }

        [ForeignKey("HikeId")]
        public virtual Hike? Hike { get; set; } // Assuming you have a Hike entity

        public DateTime UploadDate { get; set; }

        public string Description { get; set; } = string.Empty;

        // Constructor, if needed
    }
}