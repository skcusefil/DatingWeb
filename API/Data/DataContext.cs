using API.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class DataContext : IdentityDbContext<AppUser, 
    AppRole, 
    int, 
    IdentityUserClaim<int>, 
    AppUserRole, 
    IdentityUserLogin<int>,
    IdentityRoleClaim<int>,
    IdentityUserToken<int>>
    {
        public DataContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<AppUser>()
            .HasMany(ur => ur.UserRoles)
            .WithOne(u => u.User)
            .HasForeignKey(ur => ur.UserId)
            .IsRequired();

            builder.Entity<AppRole>()
            .HasMany(ur => ur.UserRoles)
            .WithOne(u => u.Role)
            .HasForeignKey(ur => ur.RoleId)
            .IsRequired();

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

            //ผู้รับข้อความ 1 คน สามารถรับข้อความจากผู้ส่งได้หลายคน
            builder.Entity<Message>()
            .HasOne(u => u.Recipient)
            .WithMany(m => m.MessagesRecieve)
            .OnDelete(DeleteBehavior.Restrict);

            //ผู้ส่งข้อความ 1 คน สามารถสงข้อความให้กับผู้รับได้หลายคน
            builder.Entity<Message>()
            .HasOne(u => u.Sender)
            .WithMany(m => m.MessagesSend)
            .OnDelete(DeleteBehavior.Restrict);


        }

        //public DbSet<AppUser> Users { get; set; }
        public DbSet<UserLike> Likes { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Connection> Connections { get; set; }
    }

}