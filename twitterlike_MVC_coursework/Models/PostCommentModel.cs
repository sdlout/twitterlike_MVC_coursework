using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace twitterlike_MVC_coursework.Models
{
    [Table("Post_Comments")]
    public class PostCommentModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public ulong Id { get; set; }

        [Required]
        [Column("comment_id")]
        public ulong CommentId { get; set; }

        public CommentModel Comment { get; set; }

        [Required]
        [Column("post_id")]
        public ulong PostId { get; set; }

        public PostModel Post { get; set; }
    }
}