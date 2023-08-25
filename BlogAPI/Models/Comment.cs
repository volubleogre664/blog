namespace BlogAPI.Models
{
    using Microsoft.EntityFrameworkCore.Metadata.Internal;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Comment
    {
        [Key]
        [Column("pkCommentId")]
        public int CommentId { get; set; }

        [Required]
        [Column("fkCommentAuthorId")]
        public int CommentAuthorId { get; set; }

        [Required]
        [Column("fkArticleId")]
        public int ArticleId { get; set; }

        [Required]
        public string CommentBody { get; set; }

        [Required]
        public DateTime CommentDate { get; set; }
    }
}
