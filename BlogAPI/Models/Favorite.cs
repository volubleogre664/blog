namespace BlogAPI.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Favorite
    {
        [Key]
        [Column("pkFavoriteId")]
        public int FavoriteId { get; set; }

        [Required]
        [Column("fkAuthorId")]
        public int AuthorId { get; set; }

        [Required]
        [Column("fkArticleId")]
        public int ArticleId { get; set; }
    }
}
