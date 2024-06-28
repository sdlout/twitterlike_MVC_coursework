using Microsoft.EntityFrameworkCore;
using twitterlike_MVC_coursework.Models;

namespace twitterlike_MVC_coursework.DbContext
{
    public partial class twitterlikeDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public twitterlikeDbContext(DbContextOptions<twitterlikeDbContext> options)
            : base(options)
        {
        }

        public DbSet<CommentModel> Comments { get; set; }
        public DbSet<PostModel> Posts { get; set; }
        public DbSet<PostCommentModel> PostComments { get; set; }
        public DbSet<UserModel> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CommentModel>(entity =>
            {
                entity.ToTable("Comments");

                entity.HasIndex(e => e.UserId);

                entity.Property(e => e.Id).HasColumnType("bigint unsigned");

                entity.Property(e => e.Text).IsRequired().HasMaxLength(255);

                entity.Property(e => e.Likes).HasColumnType("bigint");

                entity.Property(e => e.Views).HasColumnType("bigint");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Comments_Users");
            });

            modelBuilder.Entity<PostModel>(entity =>
            {
                entity.ToTable("Posts");

                entity.HasIndex(e => e.UserId);

                entity.Property(e => e.Id).HasColumnType("bigint unsigned");

                entity.Property(e => e.ImageUri).IsRequired().HasMaxLength(255);

                entity.Property(e => e.WasPosted).HasColumnType("datetime");

                entity.Property(e => e.Text).HasMaxLength(255);

                entity.Property(e => e.Likes).HasColumnType("bigint");

                entity.Property(e => e.Reposts).HasColumnType("bigint");

                entity.Property(e => e.Views).HasColumnType("bigint");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Posts)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Posts_Users");
            });

            modelBuilder.Entity<PostCommentModel>(entity =>
            {
                entity.ToTable("Post_Comments");

                entity.HasIndex(e => e.CommentId);

                entity.HasIndex(e => e.PostId);

                entity.Property(e => e.Id).HasColumnType("bigint unsigned");

                entity.HasOne(d => d.Comment)
                    .WithMany(p => p.PostComments)
                    .HasForeignKey(d => d.CommentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PostComments_Comments");

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.PostComments)
                    .HasForeignKey(d => d.PostId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PostComments_Posts");
            });

            modelBuilder.Entity<UserModel>(entity =>
            {
                entity.ToTable("Users");

                entity.Property(e => e.Id).HasColumnType("bigint unsigned");

                entity.Property(e => e.DisplayName).IsRequired().HasMaxLength(255);

                entity.Property(e => e.TagName).IsRequired().HasMaxLength(255);

                entity.Property(e => e.Email).IsRequired().HasMaxLength(255);

                entity.Property(e => e.Password).IsRequired().HasMaxLength(255);

                entity.Property(e => e.AvatarUri).IsRequired().HasMaxLength(255);
            });

            OnModelCreatingPartial(modelBuilder);
        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}