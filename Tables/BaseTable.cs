global using System.ComponentModel.DataAnnotations;
global using System.ComponentModel.DataAnnotations.Schema;

namespace Root.Tables
{
    public class BaseTable
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("created_at", TypeName = "timestamp")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [Column("updated_at", TypeName = "timestamp")]
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        [Column("is_deleted")]
        public bool IsDeleted { get; set; } = false;
    }
}
