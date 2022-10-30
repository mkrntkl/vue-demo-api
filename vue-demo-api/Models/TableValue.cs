using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace vui_demo_api.Models
{
    [Table("TableValue")]
    public class TableValue
    {
        [SwaggerSchema(ReadOnly = true)]
        public Guid Id { get; set; }
        [ForeignKey("TableHeader")]
        public Guid HeaderId { get; set; }
        [ForeignKey("Table")]
        public Guid TableId { get; set; }
        public Guid RowId { get; set; }
        public string? Value { get; set; }

        [SwaggerSchema(ReadOnly = true)]
        public DateTime CreatedAt { get; set; }

        [SwaggerSchema(ReadOnly = true)]
        public DateTime? DeletedAt { get; set; }
    }
}
