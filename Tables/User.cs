namespace Root.Tables
{
    public class User : BaseTable
    {
        [Required]
        [Column("user_name", TypeName = "varchar(50)")]
        public string UserName { get; set; }

        [Required]
        [Column("password")]
        public string Password { get; set; }

        [Column("first_name", TypeName = "varchar(50)")]
        public string? FirstName { get; set; }

        [Column("last_name", TypeName = "varchar(50)")]
        public string? LastName { get; set; }
    }
}
