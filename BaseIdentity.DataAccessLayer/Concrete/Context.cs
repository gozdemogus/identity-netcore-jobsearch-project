using System;
using System.Data;
using System.Reflection.Emit;
using BaseIdentity.EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BaseIdentity.DataAccessLayer.Concrete
{
    public class Context : IdentityDbContext<AppUser, AppRole, int>
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=tcp:127.0.0.1,1433;Database=IdentityPrjct;MultipleActiveResultSets=true;User=SA;Password=MyPass@word;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            modelBuilder.Entity<Message>()
                 .HasOne(x => x.SenderUser)
                 .WithMany(y => y.UserSender)
                 .HasForeignKey(z => z.SenderID)
                 .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<Message>()
                .HasOne(x => x.ReceiverUser)
                .WithMany(y => y.UserReceiver)
                .HasForeignKey(z => z.ReceiverID)
                .OnDelete(DeleteBehavior.ClientSetNull);
            base.OnModelCreating(modelBuilder);


            //Ad - Company one to many
            modelBuilder.Entity<Ad>()
         .HasOne(a => a.Company)
         .WithMany(c => c.Ads)
         .HasForeignKey(a => a.CompanyId)
                          .OnDelete(DeleteBehavior.ClientSetNull);

            //many to many, bir kullanıcı birden cok ilana basvurabilir, bir ilanda birden cok basvuru olabilir

            modelBuilder.Entity<AdAppUser>()
          .HasKey(x => new { x.AppUserId, x.AdId });

            modelBuilder.Entity<AdAppUser>()
                .HasOne(x => x.AppUser)
                .WithMany(x => x.AdAppUsers)
                .HasForeignKey(x => x.AppUserId).OnDelete(DeleteBehavior.ClientSetNull);


            modelBuilder.Entity<AdAppUser>()
                .HasOne(x => x.Ad)
                .WithMany(x => x.AdAppUsers)
                .HasForeignKey(x => x.AdId).OnDelete(DeleteBehavior.ClientSetNull);



            //one to many, bir ilanın bir tane experience'ı olabilir, bir experience birden cok ilan barındırabilir

            modelBuilder.Entity<Ad>()
      .HasOne(a => a.Experience)
      .WithMany(e => e.Ads)
      .HasForeignKey(a => a.ExperienceId).OnDelete(DeleteBehavior.ClientSetNull);


            //arkadaslar icin
            modelBuilder.Entity<Friendship>()
              .HasKey(f => new { f.AppUserId, f.FriendId });


            modelBuilder.Entity<Friendship>()
                .HasOne(f => f.AppUser)
                .WithMany(a => a.Friendships)
                .HasForeignKey(f => f.AppUserId);
               

          
            modelBuilder.Entity<Friendship>()
                .HasOne(f => f.Friend)
                .WithMany()
                .HasForeignKey(f => f.FriendId).OnDelete(DeleteBehavior.Restrict);

        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Ad> Ads { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<AdAppUser> AppUserAds { get; set; }
        public DbSet<Experience> Experiences { get; set; }
        public DbSet<Friendship> Friendships { get; set; }





    }
}

