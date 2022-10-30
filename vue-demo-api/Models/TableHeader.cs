using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace vui_demo_api.Models
{
    [Table("TableHeader")]
    public class TableHeader
    {
        public Guid Id { get; set; }

        [ForeignKey("Table")]
        public Guid TableId { get; set; }

        [Required(ErrorMessage = "Header name is required")]
        public string? Name { get; set; }
        public bool IsActive { get; set; }

        [NotMapped]
        [SwaggerSchema(ReadOnly = true)]
        public DateTime CreatedAt { get; set; }

        [NotMapped]
        [SwaggerSchema(ReadOnly = true)]
        public DateTime? DeletedAt { get; set; }
    }
}
