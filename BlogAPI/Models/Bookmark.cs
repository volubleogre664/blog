namespace BlogAPI.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Bookmark
    {
        [Key]
        [Column("pkBookmarkId")]
        public int BookmarkId { get; set; }

        [Required]
        [Column("fkArticleId")]
        public int ArticleId { get; set; }

        [Required]
        [Column("fkBookmarkOwner")]
        public int BookmarkOwnerId { get; set; }
    }
}
