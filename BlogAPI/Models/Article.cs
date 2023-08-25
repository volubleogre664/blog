namespace BlogAPI.Models
{
    using System.ComponentModel.DataAnnotations;

    using System.ComponentModel.DataAnnotations.Schema;
    using Microsoft.EntityFrameworkCore.Metadata.Internal;

    public class Article
    {
        [Key]
        [Column("pkArticleId")]
        public int ArticleId { get; set; }

        [Required]
        [Column("fkAuthorId")]
        public int AuthorId { get; set; }

        [Required]
        [MaxLength(100)]
        public string ArticleTitle { get; set; }

        [Required]
        public string ArticleBody { get; set; }

        [Required]
        public DateTime? ArticlePublishDate { get; set; }

        [Url]
        public string? ArticleCoverImage { get; set; }
    }
}
