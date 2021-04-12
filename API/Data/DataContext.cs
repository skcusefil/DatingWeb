using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //give a key 
            builder.Entity<UserLike>()
            .HasKey(k => new { k.LikedUserId, k.SourceUserId });

            //คนที่กดไลค์ 1 คน สามารถไลค์ได้หลายคน
            builder.Entity<UserLike>()
            .HasOne(s => s.SourceUser)
            .WithMany(l => l.LikedUser)
            .HasForeignKey(s => s.SourceUserId)
            .OnDelete(DeleteBehavior.Cascade);

            //คนที่ถูกไลค์ 1 คน สามารถได้รับไลค์ได้จากหลายคน
            builder.Entity<UserLike>()
            .HasOne(s => s.LikedUser)
            .WithMany(l => l.LikedByUser)
            .HasForeignKey(s => s.LikedUserId)
            .OnDelete(DeleteBehavior.Cascade);

            //*Important: SQL-Server gonna need to set DeleteBehavior, otherwise will get an error
        }

        public DbSet<AppUser> Users { get; set; }
        public DbSet<UserLike> Likes { get; set; }
    }

}