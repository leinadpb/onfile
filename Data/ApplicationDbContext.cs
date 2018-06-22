using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OnFile.Models;

namespace OnFile.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<BoughtFile> BoughtFiles { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<OptionalPicture> OptionalPictures { get; set; }
        public DbSet<UploadedFile> UploadedFiles { get; set; }
        public DbSet<WishList> WishLists { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

            // ApplicationUser - BoughtFile (1 - *)
            builder.Entity<BoughtFile>()
                .HasOne(bf => bf.ApplicationUser)
                .WithMany(au => au.BoughtFiles)
                .HasForeignKey(bf => bf.ApplicationUserID);

            // ApplicationUser - WishList (1 - *)
            builder.Entity<WishList>()
                .HasOne(wl => wl.ApplicationUser)
                .WithMany(au => au.WishLists)
                .HasForeignKey(wl => wl.ApplicationUserID);

            // ApplicationUser - UploadedFile (1 - *)
            builder.Entity<UploadedFile>()
                .HasOne(uf => uf.ApplicationUser)
                .WithMany(au => au.UploadedFiles)
                .HasForeignKey(uf => uf.ApplicationUserID);

            // WishList - UploadedFile (1 - *)
            builder.Entity<UploadedFile>()
                .HasOne(uf => uf.WishList)
                .WithMany(wl => wl.UploadedFiles)
                .HasForeignKey(uf => uf.WishListID);

            // BoughtFile - UploadedFile (1 - 1)
            builder.Entity<BoughtFile>()
                .HasOne(bf => bf.UploadedFile)
                .WithOne(uf => uf.BoughtFile)
                .HasForeignKey<UploadedFile>(bf => bf.UploadedFileID);

            // UploadedFile - Comment (1 - *)
            builder.Entity<Comment>()
                .HasOne(cm => cm.UploadedFile)
                .WithMany(uf => uf.Comments)
                .HasForeignKey(cm => cm.UploadedFileID);

            // ApplicationUser - Comment (1 - *)
            builder.Entity<Comment>()
                .HasOne(cm => cm.ApplicationUser)
                .WithMany(au => au.Comments)
                .HasForeignKey(cm => cm.ApplicationUserID);

           
        }
    }
}
