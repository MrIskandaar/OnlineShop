namespace Root.Tables
{
    public class Product : BaseTable
    {
        [Required]
        [Column("product_name", TypeName = "varchar(255)")]
        public string ProductName { get; set; }

        [Required]
        [Column("price", TypeName = "numeric(100, 2)")]
        public decimal Price { get; set; }

        [ForeignKey(nameof(CategoryId))]
        public Category Category { get; set; }

        [Column("category_id")]
        public int CategoryId { get; set; }

        [ForeignKey(nameof(UserId))]
        public User User { get; set; }

        [Column("user_id")]
        public int UserId { get; set; }
    }
}
