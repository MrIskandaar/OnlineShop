namespace Root.Tables
{
    public class Category : BaseTable
    {
        [Required]
        [Column("category_name", TypeName = "varchar(100)")]
        public string CategoryName { get; set; }
    }
}
