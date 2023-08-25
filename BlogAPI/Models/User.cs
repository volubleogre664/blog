namespace BlogAPI.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Diagnostics.CodeAnalysis;

    public class User
    {
        [Key]
        [Column("pkUserId")]
        public int UserId { get; set; }

        [Required]
        public string IdentityAccountId { get; set; }

        [Required]
        [MaxLength(50)]
        public string Firstname { get; set; }

        [Required]
        [MaxLength(50)]
        public string Lastname { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(50)]
        public string Email { get; set; }

        [AllowNull]
        public string? Bio { get; set; }

        [Url]
        [AllowNull]
        public string? UserPicture { get; set; }
    }
}
