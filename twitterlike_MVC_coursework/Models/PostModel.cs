using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace twitterlike_MVC_coursework.Models
{
    [Table("Posts")]
    public class PostModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public ulong Id { get; set; }

        [Required]
        [MaxLength(255)]
        [Column("image_uri")]
        public string ImageUri { get; set; }

        [Required]
        [Column("was_posted")]
        public DateTime WasPosted { get; set; }

        [MaxLength(255)]
        [Column("text")]
        public string Text { get; set; }

        [Required]
        [Column("user_id")]
        public ulong UserId { get; set; }

        public UserModel User { get; set; }

        [Required]
        [Column("likes")]
        public long Likes { get; set; }

        [Required]
        [Column("reposts")]
        public long Reposts { get; set; }

        [Required]
        [Column("views")]
        public long Views { get; set; }

        public ICollection<PostCommentModel> PostComments { get; set; }
    }
}