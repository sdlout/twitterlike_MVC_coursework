using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace twitterlike_MVC_coursework.Models
{
    [Table("Users")]
    public class UserModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public ulong Id { get; set; }

        [Required]
        [MaxLength(255)]
        [Column("display_name")]
        public string DisplayName { get; set; }

        [Required]
        [MaxLength(255)]
        [Column("tag_name")]
        public string TagName { get; set; }

        [Required]
        [MaxLength(255)]
        [Column("email")]
        public string Email { get; set; }

        [Required]
        [MaxLength(255)]
        [Column("password")]
        public string Password { get; set; }

        [Required]
        [MaxLength(255)]
        [Column("avatar_uri")]
        public string AvatarUri { get; set; }

        public ICollection<PostModel> Posts { get; set; }
        public ICollection<CommentModel> Comments { get; set; }
    }
}