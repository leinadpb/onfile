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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

            // ApplicationUser - BuyedFile (1 - *)
            builder.Entity<BuyedFile>()
                .HasOne(bf => bf.ApplicationUser)
                .WithMany(au => au.BuyedFiles)
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

            // BuyedFile - UploadedFile (1 - 1)
            builder.Entity<BuyedFile>()
                .HasOne(bf => bf.UploadedFile)
                .WithOne(uf => uf.BuyedFile)
                .HasForeignKey<UploadedFile>(bf => bf.UploadedFileID);

           
        }
    }
}
