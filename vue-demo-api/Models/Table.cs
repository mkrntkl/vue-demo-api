using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace vui_demo_api.Models
{
    [Table("Table")]
    public class Table
    {
        public Guid Id { get; set; }

        [StringLength(50, ErrorMessage = "Table name can't be longer than 50 characters")]
        public string? Name { get; set; }

        [NotMapped]
        [Required]
        public List<TableHeader> Headers { get; set; }

        [NotMapped]
        [Required]
        public List<TableValue> Values { get; set; }

        [SwaggerSchema(ReadOnly = true)]
        public DateTime CreatedAt { get; set; }

        [SwaggerSchema(ReadOnly = true)]
        public DateTime? DeletedAt { get; set; }
    }
}
